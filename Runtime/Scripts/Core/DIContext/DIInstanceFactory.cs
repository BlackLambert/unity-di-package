using System;
using UnityEngine;

namespace  SBaier.DI
{
    public class DIInstanceFactory
    {
         public TInstance Create<TInstance>(Resolver resolver, InstantiationInfo instantiationInfo)
        {
            switch (instantiationInfo.CreationMode)
            {
                case InstanceCreationMode.FromNew:
                case InstanceCreationMode.FromMethod:
                case InstanceCreationMode.FromInstance:
                case InstanceCreationMode.FromNewComponentOn:
                case InstanceCreationMode.FromResources:
                case InstanceCreationMode.FromNewComponentOnNewGameObject:
                    return (TInstance)instantiationInfo.ProvideInstanceFunction();
                case InstanceCreationMode.FromFactory:
                    return CreateByFactory<TInstance>(resolver);
                case InstanceCreationMode.FromPrefabInstance:
                    return CreatePrefabInstance<TInstance>(instantiationInfo);
                case InstanceCreationMode.FromResourcePrefabInstance:
                    return CreatePrefabInstanceFromRessources<TInstance>(instantiationInfo);
                case InstanceCreationMode.Undefined:
                    throw new ArgumentException($"Creation of {typeof(TInstance)} failed. " +
                        $"Can't create an instance with creation Mode {instantiationInfo.CreationMode}");
                default:
                    throw new NotImplementedException();
            }
        }

		private TInstance CreateByFactory<TInstance>(Resolver resolver)
        {
            Factory<TInstance> factory = resolver.Resolve<Factory<TInstance>>();
            return factory.Create();
        }

        private TInstance CreatePrefabInstance<TInstance>(InstantiationInfo instantiationInfo)
        {
            GameObject prefab = instantiationInfo.ProvideInstanceFunction() as GameObject;
            return CreatePrefabInstance<TInstance>(prefab);
        }

        private TInstance CreatePrefabInstanceFromRessources<TInstance>(InstantiationInfo instantiationInfo)
        {
            string path = instantiationInfo.ProvideInstanceFunction() as string;
            return CreatePrefabInstance<TInstance>(Resources.Load<GameObject>(path));
        }

        private TInstance CreatePrefabInstance<TInstance>(GameObject prefab)
		{
            GameObject instance = GameObject.Instantiate(prefab);
            return instance.GetComponent<TInstance>();
        }
    }

}
