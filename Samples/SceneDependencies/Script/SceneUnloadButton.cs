using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SBaier.DI.Examples.SceneDependencies
{
    public class SceneUnloadButton : MonoBehaviour, Injectable
    {
		private const string _labelBase = "Unload '{0}'";
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Text _label;

        private Scene _scene;

		public void Inject(Resolver resolver)
		{
            _scene = resolver.Resolve<Scene>();
        }

		private void Start()
		{
			_label.text = string.Format(_labelBase, _scene.name);
			_button.onClick.AddListener(UnloadScene);
        }

		private void OnDestroy()
		{
			_button.onClick.RemoveListener(UnloadScene);
		}

		private void UnloadScene()
		{
			SceneManager.UnloadSceneAsync(_scene);
		}
	}
}
