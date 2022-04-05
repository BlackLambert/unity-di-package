using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public interface Context
    {
        public void Init(Resolver baseResolver);
    }
}

