using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class LoadedSceneInitializer : MonoBehaviour, Injectable
    {
		private DIContext _context;

		public void Inject(Resolver resolver)
		{
            _context = resolver.Resolve<DIContext>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

		private void OnDestroy()
		{
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InitSceneContextOf(scene);
        }

        private void InitSceneContextOf(Scene scene)
        {
            GameObject[] sceneRootObjects = scene.GetRootGameObjects();
            List<SceneContext> contexts = new List<SceneContext>();
            foreach (GameObject sceneRootObject in sceneRootObjects)
                contexts.AddRange(sceneRootObject.GetComponentsInChildren<SceneContext>());
            Validate(contexts, scene);
            SceneContext sceneContext = contexts[0];
            if (!sceneContext.Initialized)
                sceneContext.Init(_context.GetResolver());
        }

        private void Validate(List<SceneContext> contexts, Scene scene)
        {
            int count = contexts.Count;
            if (count <= 0)
                Debug.LogWarning($"There is no {nameof(SceneContext)} present within scene {scene.name}. " +
                    $"DIContexts of this scene won't be initialized.");
            else if (count > 1)
                throw new MultipleSceneContextsException(scene.name);
        }
    }
}
