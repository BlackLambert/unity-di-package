using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI.Examples.SceneDependencies
{
    public class SceneOnStartLoader : MonoBehaviour
    {
		[SerializeField]
		private SceneName _sceneName;
		[SerializeField]
		private LoadSceneMode _loadMode = LoadSceneMode.Additive;

		private void Start()
		{
			SceneManager.LoadScene(_sceneName.ToString(), _loadMode);
		}
	}
}
