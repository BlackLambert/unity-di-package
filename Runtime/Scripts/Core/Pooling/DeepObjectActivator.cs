using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class DeepObjectActivator : ObjectActivator
    {
        private Stack<Transform> _stack = new Stack<Transform>();

        public void Activate(GameObject gameObject)
        {
            AddToEnableStack(gameObject.transform);

            while (_stack.Count > 0)
            {
                _stack.Pop().gameObject.SetActive(true);
            }

            _stack.Clear();
        }

        public void Disable(GameObject gameObject)
        {
            AddToDisableStack(gameObject.transform);

            while (_stack.Count > 0)
            {
                _stack.Pop().gameObject.SetActive(false);
            }

            _stack.Clear();
        }

        private void AddToEnableStack(Transform transform)
        {
            foreach (Transform child in transform)
            {
                AddToEnableStack(child);
            }

            _stack.Push(transform);
        }

        private void AddToDisableStack(Transform transform)
        {
            _stack.Push(transform);

            foreach (Transform child in transform)
            {
                AddToDisableStack(child);
            }
        }
    }
}