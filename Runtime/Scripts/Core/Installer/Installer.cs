using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public interface Installer
    {
        public void InstallBindings(Binder binder);
    }
}