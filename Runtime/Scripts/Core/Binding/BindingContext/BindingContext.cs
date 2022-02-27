using System;
using UnityEngine;

namespace SBaier.DI
{
    public class BindingContext<C1> : BindingContext<C1, C1, C1, C1, C1, C1, C1, C1>
    {
        public BindingContext(BindingArguments arguments) : base(arguments) { }

        public BindingContext<C1, TContract2> And<TContract2>(IComparable iD = default)
		{
            _bindingStorage.AddBinding<TContract2>(_binding, iD);
            return new BindingContext<C1, TContract2>(_arguments);
        }
    }

    public class BindingContext<C1, C2> : BindingContext<C1, C2, C2, C2, C2, C2, C2, C2>
    {
        public BindingContext(BindingArguments arguments) : base(arguments) { }

        public BindingContext<C1, C2, TContract3> And<TContract3>(IComparable iD = default)
        {
            _bindingStorage.AddBinding<TContract3>(_binding, iD);
            return new BindingContext<C1, C2, TContract3>(_arguments);
        }
    }

    public class BindingContext<C1, C2, C3> : BindingContext<C1, C2, C3, C3, C3, C3, C3, C3>
    {
        public BindingContext(BindingArguments arguments) : base(arguments) { }

        public BindingContext<C1, C2, C3, TContract4> And<TContract4>(IComparable iD = default)
        {
            _bindingStorage.AddBinding<TContract4>(_binding, iD);
            return new BindingContext<C1, C2, C3, TContract4>(_arguments);
        }
    }

    public class BindingContext<C1, C2, C3, C4> : BindingContext<C1, C2, C3, C4, C4, C4, C4, C4>
    {
        public BindingContext(BindingArguments arguments) : base(arguments) { }
    }

    public class BindingContext<C1, C2, C3, C4, C5, C6, C7, C8> : BindingContextBase
    {
        public BindingContext(BindingArguments arguments) : base(arguments) { }

        public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : C1, C2, C3, C4, C5, C6, C7, C8
        {
            return new ToBindingContext<TConcrete>(_arguments);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : C1, C2, C3, C4, C5, C6, C7, C8, new()
        {
            return new FromNewBindingContext<TConcrete>(_arguments);
        }

        public ToComponentBindingContext<TConcrete> ToComponent<TConcrete>() where TConcrete : Component, C1, C2, C3, C4, C5, C6, C7, C8
        {
            return new ToComponentBindingContext<TConcrete>(_arguments);
        }

        public ToObjectBindingContext<TConcrete> ToObject<TConcrete>() where TConcrete : UnityEngine.Object, C1, C2, C3, C4, C5, C6, C7, C8
        {
            return new ToObjectBindingContext<TConcrete>(_arguments);
        }
    }
}