using System;
using UnityEngine;

namespace SBaier.DI
{
    public class ConcreteBindingContext<Contract1> : ConcreteBindingContext<Contract1, Contract1, Contract1, Contract1, Contract1, Contract1, Contract1, Contract1>
    {
        public ConcreteBindingContext(IComparable iD, BindingArguments arguments) : base(arguments) 
        {
            AddContract<Contract1>(iD);
        }

        public ConcreteBindingContext<Contract1, TContract2> And<TContract2>(IComparable iD = default)
		{
            AddContract<TContract2>(iD);
            return new ConcreteBindingContext<Contract1, TContract2>(_arguments);
        }
    }

    public class ConcreteBindingContext<C1, C2> : ConcreteBindingContext<C1, C2, C2, C2, C2, C2, C2, C2>
    {
        public ConcreteBindingContext(BindingArguments arguments) : base(arguments) { }

        public ConcreteBindingContext<C1, C2, TContract3> And<TContract3>(IComparable iD = default)
        {
            AddContract<TContract3>(iD);
            return new ConcreteBindingContext<C1, C2, TContract3>(_arguments);
        }
    }

    public class ConcreteBindingContext<C1, C2, C3> : ConcreteBindingContext<C1, C2, C3, C3, C3, C3, C3, C3>
    {
        public ConcreteBindingContext(BindingArguments arguments) : base(arguments) { }

        public ConcreteBindingContext<C1, C2, C3, TContract4> And<TContract4>(IComparable iD = default)
        {
            AddContract<TContract4>(iD);
            return new ConcreteBindingContext<C1, C2, C3, TContract4>(_arguments);
        }
    }

    public class ConcreteBindingContext<C1, C2, C3, C4> : ConcreteBindingContext<C1, C2, C3, C4, C4, C4, C4, C4>
    {
        public ConcreteBindingContext(BindingArguments arguments) : base(arguments) { }
    }

    public class ConcreteBindingContext<C1, C2, C3, C4, C5, C6, C7, C8> : BindingContextBase
    {
        public ConcreteBindingContext(BindingArguments arguments) : base(arguments) { }

        public CreationModeBindingContext<TConcrete> To<TConcrete>() where TConcrete : C1, C2, C3, C4, C5, C6, C7, C8
        {
            return new CreationModeBindingContext<TConcrete>(_arguments);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : C1, C2, C3, C4, C5, C6, C7, C8, new()
        {
            return new FromNewBindingContext<TConcrete>(_arguments);
        }

        public ComponentCreationModeBindingContext<TConcrete> ToComponent<TConcrete>() where TConcrete : Component, C1, C2, C3, C4, C5, C6, C7, C8
        {
            return new ComponentCreationModeBindingContext<TConcrete>(_arguments);
        }

        public ObjectCreationModeBindingContext<TConcrete> ToObject<TConcrete>() where TConcrete : UnityEngine.Object, C1, C2, C3, C4, C5, C6, C7, C8
        {
            return new ObjectCreationModeBindingContext<TConcrete>(_arguments);
        }

        protected void AddContract<TContract>(IComparable iD)
		{
            _arguments.Keys.Add(new BindingKey(typeof(TContract), iD));
            _bindingStorage.AddBinding<TContract>(_binding, iD);
        }
    }
}