using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace SBaier.DI.Tests
{
    public class QuitDetectorTests
    {
        private TestQuitDetector _quitDetector;
        private bool _onQuitIsCalled = false;

        [TearDown]
        public void TearDown()
		{
            GameObject.Destroy(_quitDetector.gameObject);
            _onQuitIsCalled = true;
            _quitDetector.OnQuit -= OnQuit;
        }

        [UnityTest]
        public IEnumerator ApplicationIsQuitting_IsTrueOnQuitting()
        {
            GivenADefaultSetup();
            yield return 0;
            WhenIsQuitting();
            ThenApplicationIsQuittingIs(true);
        }

        [UnityTest]
        public IEnumerator ApplicationIsQuitting_IsFalseByDefault()
        {
            GivenADefaultSetup();
            yield return 0;
            ThenApplicationIsQuittingIs(false);
        }

        [UnityTest]
        public IEnumerator OnQuit_IsCalledOnQuit()
		{
			GivenADefaultSetup();
            GivenOnQuitListener();
            yield return 0;
			WhenIsQuitting();
			ThenIsOnQuitIsInvoked();
		}

		private void GivenADefaultSetup()
		{
            GameObject obj = new GameObject();
            _quitDetector = obj.AddComponent<TestQuitDetector>();
        }

        private void GivenOnQuitListener()
        {
            _quitDetector.OnQuit += OnQuit;
        }

        private void WhenIsQuitting()
        {
            _quitDetector.SetQuitting();
        }

        private void ThenApplicationIsQuittingIs(bool shallQuitting)
        {
            Assert.AreEqual(shallQuitting, _quitDetector.ApplicationIsQuitting,
                $"{nameof(QuitDetector.ApplicationIsQuitting)} of {nameof(QuitDetector)} should be {shallQuitting}");
        }

        private void ThenIsOnQuitIsInvoked()
        {
            Assert.IsTrue(_onQuitIsCalled,
                $"{nameof(QuitDetector.OnQuit)} of {nameof(QuitDetector)} is not invoked like expected");
        }

        private void OnQuit()
		{
            _onQuitIsCalled = true;
        }
    }
}
