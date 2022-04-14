using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneObjectsDisabler
    {
        public void DisableObjectsOf(Scene scene)
        {
            foreach (GameObject sceneObject in scene.GetRootGameObjects())
                sceneObject.SetActive(false);
        }
    }
}
