# Build Info Writer

## Introduction

This script provides a simple way to add basic build info to the bin folder of your `msbuild` (eg: `*.csproj`, `*.vbproj`) projects. This is useful for tracking releases, especially ad-hoc releases so that what is deployed can be more easily determined.


## Including into your project

To add this to your project you will need to do the following:

 1. Include this into your `git` repo in one of two ways:
    * Add this repo as a remote and then merge the `master` branch into your branch.
       1. First add the remote [http://net4dev.selectsoftware.com.au:8080/tfs/Net4Dev/Development/_git/write-build-info](http://net4dev.selectsoftware.com.au:8080/tfs/Net4Dev/Development/_git/write-build-info) we will using the name `write-build-info` for the remote.
       2. Checkout the branch you want to add this to.
       3. Open the command-line in your git repo. (You **CANNOT** do this through the Visual Studio GUI).
       4. Run the command `git merge --commit --allow-unrelated-histories remotes/write-build-info/master`.
    * Add this repo as submodule. If you know how to do this you can, however, note that it can be a PitA for a lot of tools, including Visual Studio to use them.
 2. Ensure you have finished any commits from the previous step.
 3. Open the `*.*proj` (`*.csproj*)`, `*.vbproj`) in a text editor (you can do this by unloading the project in Visual Studio, right clicking it and selecting edit) and add the following to end of the file (just before the closing tag of the root element) (you may need to change the directory depending on where the that folder is relative to your `*.*proj` file):
    ```xml
    <Import Project="../../write-build-info.exclude/write-build-info.targets" />
    ```
  4. Build to ensure you have done this correctly.
  5. Commit your changes.
## Submiting changes

Please submit all pull requests and changes to [http://net4dev.selectsoftware.com.au:8080/tfs/Net4Dev/Development/_git/write-build-info](http://net4dev.selectsoftware.com.au:8080/tfs/Net4Dev/Development/_git/write-build-info). Please make sure any changes you make are not done with the history of your project in as that will not be accepted as-is.