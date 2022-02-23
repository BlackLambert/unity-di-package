using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI.Examples
{
    public class SceneOnStartLoader : MonoBehaviour
    {
		[SerializeField]
		private string _sceneName;

		private void Start()
		{
			SceneManager.LoadScene(_sceneName, LoadSceneMode.Additive);
		}
	}
}
