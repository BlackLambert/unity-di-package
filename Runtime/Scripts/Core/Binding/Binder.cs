using System;
using UnityEngine;

namespace SBaier.DI
{
    public interface Binder
    {
        public BindingContext<TContract> Bind<TContract>(IComparable iD = default);
        public AsBindingContext BindInstance<TContract>(TContract instance, IComparable iD = null);
        public ToBindingContext<TContract> BindToSelf<TContract>(IComparable iD = default);
        public FromNewBindingContext<TContract> BindToNewSelf<TContract>(IComparable iD = default) where TContract : new();
        public ToComponentBindingContext<TContract> BindComponent<TContract>(IComparable iD = default) where TContract : Component;
        public ToObjectBindingContext<TContract> BindObject<TContract>(IComparable iD = default) where TContract : UnityEngine.Object;
        public NonResolvableBindingContext CreateNonResolvableInstance();
    }
}