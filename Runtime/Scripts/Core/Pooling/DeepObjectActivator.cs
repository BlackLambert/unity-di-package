using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class DeepObjectActivator : ObjectActivator

    {
    private Stack<Transform> _stack = new Stack<Transform>();

    public void Activate(GameObject gameObject)
    {
        EnableInternal(gameObject.transform);

        _stack.Clear();
    }

    public void Disable(GameObject gameObject)
    {
        DisableInternal(gameObject.transform);

        _stack.Clear();
    }

    private void EnableInternal(Transform transform)
    {
        foreach (Transform child in transform)
        {
            DisableInternal(child);
        }

        _stack.Push(transform);
    }

    private void DisableInternal(Transform transform)
    {
        _stack.Push(transform);

        foreach (Transform child in transform)
        {
            DisableInternal(child);
        }
    }
    }
}
