using System.Collections;
using UnityEngine;

namespace SBaier.DI
{
    public class GameObjectInjector
    {
        public void InjectIntoHierarchy(Transform root, Resolver resolver)
        {
            InjectInto(root, resolver);
            InjectIntoChildren(root, resolver);
        }

        public void InjectIntoContextHierarchy(Transform root, Resolver resolver)
        {
            if (root.TryGetComponent(out GameObjectContext gameObjectContext))
                gameObjectContext.Init(resolver);
            else
                InjectIntoHierarchy(root, resolver);
        }

        private void InjectIntoChildren(Transform root, Resolver resolver)
        {
            IEnumerator iterator = root.GetEnumerator();
            iterator.Reset();
            while (iterator.MoveNext())
            {
                InjectIntoContextHierarchy(iterator.Current as Transform, resolver);
            }
        }

        private void InjectInto(Transform root, Resolver resolver)
        {
            Injectable[] injectables = root.GetComponents<Injectable>();
            foreach (Injectable injectable in injectables)
                InjectInto(injectable, resolver);
        }

        private void InjectInto(Injectable injectable, Resolver resolver)
        {
            if (injectable is Installer)
                return;
            injectable.Inject(resolver);
        }
    }
}