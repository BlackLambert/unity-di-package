using UnityEngine;

namespace SBaier.DI
{
    public class MonoPoolInstaller<TItem> : MonoInstaller where TItem : Component
    {
        [SerializeField]
        private TItem _prefab;

        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<TItem>>()
                .And<Factory<TItem, Transform>>()
                .ToNew<PrefabFactory<TItem>>()
                .WithArgument(_prefab);
            
            binder.Bind<Pool<TItem>>()
                .ToNew<MonoPool<TItem>>()
                .WithArgument(_prefab)
                .AsSingle();
        }
    }
    
    public class MonoPoolInstaller<TItem, TArgument> : MonoInstaller where TItem : Component
    {
        [SerializeField]
        private TItem _prefab;

        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<TItem, TArgument>>()
                .And<Factory<TItem, TArgument, Transform>>()
                .ToNew<PrefabFactory<TItem, TArgument>>()
                .WithArgument(_prefab);
            
            binder.Bind<Pool<TItem, TArgument>>()
                .ToNew<MonoPool<TItem, TArgument>>()
                .WithArgument(_prefab)
                .AsSingle();
        }
    }
}
