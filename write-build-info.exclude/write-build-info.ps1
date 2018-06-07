#
# write_build_info.ps1
#


Param (
    [parameter(Mandatory=$true)]
    [string]
    $projectDir,
    [parameter(Mandatory=$true)]
    [string]
    $outDir
)
$runDateIso = Get-Date -Format o;
function CombinePath{
    [OutputType([string])]
    Param (        
        [string[]]
        $paths
    )
    [System.IO.Path]::Combine($paths)
}
function Write-File{
    [OutputType([void])]
    Param (
        [object[]]
        [parameter(ValueFromPipeline)]
        $Content,
        [string]
        [parameter(Mandatory)]
        $FilePath,
        [switch]
        $UseClrf,
        [switch]
        $Append
    )
    $dir = [System.IO.Path]::GetDirectoryName($FilePath);
    if (![System.IO.Directory]::Exists($dir)){
        [System.IO.Directory]::CreateDirectory($dir) | Out-Null;
    }
    $outStream = New-Object System.IO.StreamWriter($FilePath, $Append);
    try{
        if ($UseClrf){
            $outStream.NewLine = "`r`n";
        }
        else {
            $outStream.NewLine = "`n";
        }
        $first = $true;
        Foreach ($line in $Content) {
            if ($first){
                $first = $false
            }
            else {
                $outStream.WriteLine();
            }
            $outStream.Write($line);
        }
        if ($Append) {
            $outStream.WriteLine();
        }
    }
    finally {
        $outStream.Dispose();
    }
}
function ToString([parameter(ValueFromPipeline)]$value){
    if ($value -eq $null){
        $null;
    }
    "$value";
}
$sourceDir = CombinePath(@((Get-Location)) + @("$projectDir"));
$baseOutDir = CombinePath(@((Get-Location)) + @("$projectDir", "$outDir"));
$tfsOutDir = CombinePath(@((Get-Location)) + @("$baseOutDir", "tfs"));
$gitOutDir = CombinePath(@((Get-Location)) + @("$baseOutDir", "git"));
Write-File -FilePath (CombinePath($baseOutDir, "README.txt")) -UseClrf -Content "This folder is to assist with tracking deployments, especially if they're not automated through TFS. Do NOT delete this folder or any of its contents and ensure that if you are deploying this application that you keep this folder with it.";
Write-File -FilePath (CombinePath($baseOutDir, "DO NOT DELETE.txt")) -UseClrf -Content "Read the README.txt";
Write-File -FilePath (CombinePath($baseOutDir, "system-info.json")) -UseClrf -Content (ConvertTo-Json -Compress -Depth 5 @{
    ComputerName = $env:COMPUTERNAME | ToString;
    ProjectDir = $projectDir | ToString;
    OutDir = $outDir | ToString;
    VisualStudioVersion = $env:VisualStudioVersion | ToString;
    UserName = $env:USERNAME | ToString;
    UserDomain = $env:USERDOMAIN | ToString;
})
Push-Location -Path $sourceDir
try {
    Write-Host "Writing Build Data to: $baseOutDir";
    [string] $tfsInfoFileName;
    if (-Not $env:TF_BUILD) {
        Write-Host 'Not a TFS build';
        $tfsInfoFileName = "non-tfs-build.json";
    }
    else {
        Write-Host 'Is a TFS build';
        $tfsInfoFileName = "tfs-build.json";
    }
    Write-File -FilePath (CombinePath($tfsOutDir, $tfsInfoFileName)) -UseClrf -Content (ConvertTo-Json -Compress -Depth 5 @{
        build = @{
            buildId = $env:BUILD_BUILDID;
            buildNumber = $env:BUILD_BUILDNUMBER;
            definitionName = $env:BUILD_DEFINITIONNAME;
            definitionVersion = $env:BUILD_DEFINITIONVERSION;
            buildUri = $env:BUILD_BUILDURI;
            sourceVersion = $env:BUILD_SOURCEVERSION;
            sourceBranch = $env:BUILD_SOURCEBRANCH;
            repository = @{
                name = $env:BUILD_REPOSITORY_NAME;
                uri = $env:BUILD_REPOSITORY_URI;
            };
        }
    });
    $hasGit = (Get-Command "git" -ErrorAction SilentlyContinue) -ne $null;
    $isInGitRepo = $hasGit -and ((git rev-parse --is-inside-work-tree 2> $null) -Or $false);
    
    $gitErrorLog = CombinePath($gitOutDir, "git-error.log");
    if ((Get-Command "git" -ErrorAction SilentlyContinue) -eq $null) {
        # Note that we don't have git available and prevent clobbering the git data.
        Write-File -FilePath (CombinePath @($gitOutDir, "no-git README.txt")) -Append -UseClrf -Content "This code has been build whilst git was not installed (or not in PATH), the git details are as of the last time it was built. Refer to ``$gitErrorLog`` for times on builds.";
        Write-File -FilePath $gitErrorLog -Append -UseClrf -Content "[$runDateIso]`tGit not available";
        Write-Error '``git`` is either not installed or not available in the ``PATH``, please make sure git is installed correctly before continuing.';
    }
    elseif(-Not $isInGitRepo) {
        # Note that we are not in a git repo and prevent clobbering the git data.
        Write-File -FilePath (CombinePath @($gitOutDir, "no-repo README.txt")) -Append -UseClrf -Content "This code has been build whilst not in its git repo, the git details are as of the last time it was built. Refer to ``$gitErrorLog`` for times on builds.";
        Write-File -FilePath $gitErrorLog -Append -UseClrf -Content "[$runDateIso]`tNot in Repo";
        Write-Error 'This code is NOT in a git repo, please do not attempt to build and deploy this code without the repo. Ideally the deployment should come from a TFS artifact.';
    }
    else {
        Write-Host 'Has git and in repo';
        Remove-Item -Path $gitOutDir -Recurs -ErrorAction SilentlyContinue;
        Write-File -FilePath (CombinePath($gitOutDir, "git log --max-count 20 --oneline --graph.txt")) -UseClrf -Content (git log --max-count 20 --oneline --graph);
        
        $gitRoot = git rev-parse --show-toplevel;
        Write-Host "git root is at $gitRoot"
        Push-Location -Path $gitRoot;
        try {
            Write-File -FilePath (CombinePath($gitOutDir, "git status --untracked-files=all.txt")) -UseClrf -Content (git status --untracked-files=all);

            $diffDirName = "diff";

            # Write a diff containing the staged stuff.
            Write-File -FilePath (CombinePath($gitOutDir, $diffDirName, "000 staged.diff")) -Content (git diff --binary --cached);
            # Write a diff containing the file names of the tracked unstaged stuff.
            Write-File -FilePath (CombinePath($gitOutDir, $diffDirName, "001 unstaged.diff")) -Content (git diff --binary);
            # Write a diff containing of the untracked stuff, this gets a bit messy since we actually need to add the untracked stuff
            $untrackedFiles = git ls-files --others --exclude-standard;
            if ($untrackedFiles -eq $null -or $untrackedFiles.LongLength -eq 0) {
                Write-File -FilePath (CombinePath($gitOutDir, $diffDirName, "002 untracked.diff"));
            }
            else {
                try {
                    # We need to add the untracked files otherwise it's really hard to make them show up in the diff.
                    git add --verbose $untrackedFiles
                    # Write the diff containing the untracked stuff.
                    Write-File -FilePath (CombinePath($gitOutDir, $diffDirName, "002 untracked.diff")) -Content (git diff --binary --cached -- $untrackedFiles);
                }
                finally{
                    # Try to clean up.
                    git rm --cached --ignore-unmatch $untrackedFiles;
                }
            }
        }
        finally{
            Pop-Location
        }
    }
}
finally{
    Pop-Location
}