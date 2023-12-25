using UnityEngine;

namespace SBaier.DI
{
    public class BasicObjectActivator : ObjectActivator
    {
        public void Activate(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
        
        public void Disable(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
