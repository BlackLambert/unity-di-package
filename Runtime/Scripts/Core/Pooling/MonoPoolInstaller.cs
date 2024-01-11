using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class MonoPoolInstaller<TItem> : MonoInstaller where TItem : Component
    {
        [SerializeField]
        private TItem _fooPrefab;

        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<TItem>>()
                .And<Factory<TItem, Transform>>()
                .ToNew<PrefabFactory<TItem>>()
                .WithArgument(_fooPrefab);
            
            binder.Bind<Pool<TItem>>()
                .ToNew<MonoPool<TItem>>()
                .WithArgument(_fooPrefab)
                .AsSingle();
        }
    }
    
    public class MonoPoolInstaller<TItem, TArgument> : MonoInstaller where TItem : Component
    {
        [SerializeField]
        private TItem _fooPrefab;

        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<TItem, TArgument>>()
                .And<Factory<TItem, TArgument, Transform>>()
                .ToNew<PrefabFactory<TItem, TArgument>>()
                .WithArgument(_fooPrefab);
            
            binder.Bind<Pool<TItem, TArgument>>()
                .ToNew<MonoPool<TItem, TArgument>>()
                .WithArgument(_fooPrefab)
                .AsSingle();
        }
    }
}
