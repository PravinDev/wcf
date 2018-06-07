using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SelcommOSSSingtelTest
{
    public static class TestCategories
    {
#pragma warning disable CS0618 //ignore obsolete

        /// <summary>This test is expected to usually leave the database in a different state to when the test began; or have a permanent impact on external systems.
        /// The execution sequence of HasSideEffects tests MAY impact other tests, although test overlap should be planned such that the side effects SHOULD NOT change the result of other tests.</summary>
        /// <remarks>This implies <see cref="Mutate"/></remarks>
        public const string HasSideEffects = nameof(HasSideEffects);
        /// <summary>If this test passes, then the database will be restored to the same state as it was before the test. If it fails, it SHOULD restore the data, but it MAY leave it modified.
        /// Incrementing Serial key Identity counters does not count as a Side effect, so long as any inserted records are deleted.
        /// The execution sequence of NoSideEffects tests WILL NOT impact other tests.</summary>
        public const string NoSideEffects = nameof(NoSideEffects);


        /// <summary>This method relies on using the database. It is expected to fail if unable to reach the database</summary>
        public const string Database = nameof(Database);
        /// <summary>Database outages should not effect this test</summary>
        public const string NoDatabase = nameof(NoDatabase);


        #region Invokes 3rd party
        /// <summary>Invokes 3rd party (Veda credit checks)</summary>
        public const string CallsVeda = nameof(CallsVeda);
        /// <summary>Invokes Veda webservices, calling functions which take a long time to complate, compared to other veda calls</summary>
        public const string ExpensiveVeda = nameof(ExpensiveVeda);
        /// <summary>Invokes 3rd party (Cybersource Payment Gateway)</summary>
        public const string CallsCyberSource = nameof(CallsCyberSource);
        /// <summary>Invokes 3rd party (MRTM telco recharges, through SelcommOSS)</summary>
        public const string CallsMrtmViaOSS = nameof(CallsMrtmViaOSS);
        /// <summary>Invokes 3rd party (Addressify address parser)</summary>
        public const string CallsAddressify = nameof(CallsAddressify);
        /// <summary>Invokes 3rd party (AAPT, through SelcommOSS)</summary>
        public const string CallsAAPT = nameof(CallsAAPT);
        /// <summary>Invokes 3rd party (Touchcorp, through Pivotel payments platform)</summary>
        public const string CallsTouchCorpViaPivotel = nameof(CallsTouchCorpViaPivotel);




        #endregion

        /// <summary>This test creates recharge-related events which will be seen by the MRTM daemon and cause processing on them.
        /// (This category can be used to selectively disable tests which have side effects on UA testing)</summary>
        public const string CreatesRechargeEvents = nameof(CreatesRechargeEvents);


        /// <summary>in most builds, these tests dont get run (but do in some builds)</summary>
        public const string Skipped = nameof(Skipped);
        /// <summary>like skipped, but these tests still get run; but their failures show up separately</summary>
        public const string Muted = nameof(Muted);//
        /// <summary>tests that typically take over 5 seconds; or takes over 1 second and is not important to always run every time.</summary>
        public const string Slow = nameof(Slow);
        /// <summary>tests that are not needed on every build or pull request, but should still run in daily snapshot (works like slow, but for tests which aren't slow, or are to be avoided for reasons other than speed)</summary>
        public const string NoGPR = nameof(NoGPR);
        /// <summary>Alias for NoGPR - for tests which should not be run too often, but are ok for daily build, etc</summary>
        public const string Occasional = nameof(NoGPR);





        /// <summary>
        /// indicates this test has intermediate modification of the database or other persistent values during the test.
        /// If is also is <see cref="NoSideEffects"/>, then the test should change the persistent data back after completion.
        /// This means the test is unsuitable for paralellizing with other tests. This implies <see cref="Database"/> unless explicitly marked with <see cref="NoDatabase"/>.
        /// </summary>
        public const string Mutate = nameof(Mutate);
        /// <summary>This test does not change any data at any point, including external interfaces (or at least, any data changed, must not be read in any other test);
        /// AND it is suitable to be parallelized with all other <see cref="ReadOnly"/> tests (and any external interfaces it uses are also suitable for parallelizing).</summary>
        /// <remarks>This implies <see cref="NoSideEffects"/></remarks>
        public const string ReadOnly = nameof(ReadOnly);
        /// <summary>Alias for ReadOnly (paralellizable tests cannot modify intermediate data, otherwise other tests will see a change and be incorrect when comparing before/after)</summary>
        public const string Parallelizable = nameof(ReadOnly);




        /// <summary>Do NOT apply this directly! - Used by <see cref="Selcomm.Test.Support.SuppressUntil"/> to denote a test that is 
        /// currently supressed until a certain date and should not block builds yet.</summary>
        [Obsolete("Do NOT apply the suppression categories directly")]
        public const String SuppressedUntilValid = nameof(SuppressedUntilValid);
        /// <summary>Do NOT apply this directly! - Used by <see cref="Selcomm.Test.Support.SuppressUntil"/> to denote a test that has been
        /// suppressed until a date that has passed. This should block builds.</summary>
        [Obsolete("Do NOT apply the suppression categories directly")]
        public const String SuppressedUntilExpired = nameof(SuppressedUntilExpired);
        /// <summary>Do NOT apply this directly! - Used by <see cref="Selcomm.Test.Support.SuppressUntil"/> to denote a test that has been requested to be
        /// supressed to a date that is either too far in the future or is not correctly formatted. This should block builds.</summary>
        [Obsolete("Do NOT apply the suppression categories directly")]
        public const String SuppressedUntilInvalid = nameof(SuppressedUntilInvalid);



        /// <summary>
        /// This test creates a session in the local instance and uses direct method calls.
        /// </summary>
        public const string UsesLocalSession = nameof(UsesLocalSession);

        /// <summary>
        /// This test uses transport tests, and to work properly the tested endpoint must be the local instance - i.e. it uses a mix of direct method calls, and transport tests, and assumes they go to the same place.
        /// This test is not expected to work normally in remote transport tests, regardless of environment;
        /// This does not apply when the test creates a local session, and a transport session, but either keeps them properly separated or doesn't require separation for the test to work normally.
        /// </summary>
        /// <remarks>
        /// This applies to anything which modifies instance state before invoking transport tests (e.g. invoking a transport test within <see cref="Selcomm.Extensions.MailExtensions.RunTestWithEmailCapture"/>), or the Thing Under Test in any way expects a test context.
        /// E.g.
        /// -uses direct methods to create a session (e.g. Helpers.NewSession), then sends that session key to a transport test (e.g. <see cref="Selcomm.Test.Service{TChannel}.For{TImplementation}(TestContext, string)"/>, or vice-versa any mixed combination of direct/transport
        /// -uses transport tests and also uses DataManager (such as GetMany or BeginTransaction), without specifically separating the SessionKey used for transport tests from the session used for DataManager in the current thread
        /// -begins a transaction and uses transport tests in it (although this tag can be left off if the transaction is not important, e.g. if it rolls back just to keep it less spammy, but it's acceptable to sometimes commit)
        /// </remarks>
        public const string ReliesOnLocalInstance = nameof(ReliesOnLocalInstance);

        /// <summary>This test is not expected to work properly if the endpoint under test is not the same environment (qa/ua/prod) as the environment where the test is being run. But it should work on a different instance in the same environment.</summary>
        /// <remarks>
        /// Only apply this tag directly if <see cref="ReliesOnLocalInstance"/> doesn't apply. (i.e. that it can be run in qa, and test against a remote qa).
        /// This should be applied e.g. if it uses transport to test it, then uses datamanager to verify it (and keeps the sessions properly separated - otherwise use <see cref="ReliesOnLocalInstance"/>)
        /// </remarks>
        public const string ReliesOnLocalEnvironment = nameof(ReliesOnLocalEnvironment);

        /// <summary>This test uses transport tests, and is expected to work correctly when the transport uses a remote endpoint.
        /// It MAY also have <see cref="ReliesOnLocalEnvironment"/>, but if not, then all assertions are verified by transport responses (either responses from the Thing Under Test, or responses from other queries over transport; or it is internally aware of the remote environment).
        /// It does not rely on transactions, and it either it does not use direct method calls, or it properly separates sessions, etc from any transport tests.
        /// Any test without this tag, either does not use transport tests, or should be assumed to have <see cref="ReliesOnLocalInstance"/> or <see cref="NoDatabase"/>. This tag is for correct tests verified that they are expected to work without assumptions.
        /// </summary>
        public const string RemoteTransportCompatible = nameof(RemoteTransportCompatible);

        /// <summary>
        /// This test uses transport, and can be run safely against ANY environment, including a production environment, and should be run for PVT testing after deployments.
        /// This should be ReadOnly/NoSideEffects, or should only have side effects which are acceptable to the client being tested (and for Mutate tests, it should also consider side effects if something fails during the test)
        /// This implies RemoteTransportCompatible, and should not have ReliesOnLocalEnvironment (or ReliesOnLocalInstance).
        /// </summary>
        [Obsolete("Are you sure this can run safely on production? (this isn't obsolete, it's just to warn against accidental usage)", false)]
        public const string RunForPVT_This_Is_Safe_To_Run_Against_Production = nameof(RunForPVT_This_Is_Safe_To_Run_Against_Production);



#pragma warning restore CS0618
    }
}
