using System;
using UnityEngine;

namespace SBaier.DI
{
    public interface Binder
    {
        public ConcreteBindingContext<TContract> Bind<TContract>(IComparable iD = default);
        public FromInstanceBindingContext BindInstance<TContract>(TContract instance, IComparable iD = null);
        public CreationModeBindingContext<TContract> BindToSelf<TContract>(IComparable iD = default);
        public FromNewBindingContext<TContract> BindToNewSelf<TContract>(IComparable iD = default) where TContract : new();
        public ComponentCreationModeBindingContext<TContract> BindComponent<TContract>(IComparable iD = default) where TContract : Component;
        public ObjectCreationModeBindingContext<TContract> BindObject<TContract>(IComparable iD = default) where TContract : UnityEngine.Object;
    }
}