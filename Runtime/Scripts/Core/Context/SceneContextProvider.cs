using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class SceneContextProvider: Injectable
    {
        private const string alreadyAddedExceptionMessage = "The scene with iD {0} has already been added to the provider.";
        private const string notAddedExceptionMessage = "The scene with iD {0} has not been added to the provider. Therefore removing it is not possible";
        private const string getExceptionMessage = "The scene with iD {0} has not been added to the provider. Therefore getting it is not possible";
        private const string activeSceneDependenciesExceptionMessage = "The scene with iD {0} is parent to another scene. Unload dependant scenes before removing.";

        private Dictionary<string, SceneContext> _sceneContexts = new Dictionary<string, SceneContext>();
        private Dictionary<string, int> _sceneToDependencyAmount = new Dictionary<string, int>();

        private QuitDetector _quitDetector;

        private bool IsAppQuitting => _quitDetector.ApplicationIsQuitting;

        public void Inject(Resolver resolver)
        {
            _quitDetector = resolver.Resolve<QuitDetector>();
        }

        public void Add(SceneContext context)
        {
            if (string.IsNullOrEmpty(context.ID))
                return;
            ValidateAdd(context.ID);
            _sceneContexts[context.ID] = context;
            AddParentDependency(context);
        }

		public void Remove(SceneContext context)
        {
            if (string.IsNullOrEmpty(context.ID))
                return;
            ValidateRemove(context.ID);
            _sceneContexts.Remove(context.ID);
            RemoveParentDependency(context);
        }

		public SceneContext Get(string iD)
        {
            ValidateGet(iD);
            return _sceneContexts[iD];
        }

        private void AddParentDependency(SceneContext context)
        {
            string parentContextID = context.ParentContextID;
            if (string.IsNullOrEmpty(parentContextID))
                return;
            if (!_sceneToDependencyAmount.ContainsKey(parentContextID))
                _sceneToDependencyAmount.Add(parentContextID, 0);
            _sceneToDependencyAmount[parentContextID]++;
        }

        private void RemoveParentDependency(SceneContext context)
        {
            string parentContextID = context.ParentContextID;
            if (string.IsNullOrEmpty(parentContextID))
                return;
            _sceneToDependencyAmount[parentContextID]--;
        }

        private void ValidateAdd(string iD)
        {
            if (_sceneContexts.ContainsKey(iD))
                throw new AlreadyAddedException(iD);
        }

        private void ValidateRemove(string iD)
        {
            if (!_sceneContexts.ContainsKey(iD))
                throw new AlreadyAddedException(iD);
            if (!IsAppQuitting && IsDependantSceneContext(iD))
                throw new ActiveSceneDependenciesException(iD);
        }

        private bool IsDependantSceneContext(string iD)
		{
            return _sceneToDependencyAmount.ContainsKey(iD) &&
                _sceneToDependencyAmount[iD] > 0;
        }

        private void ValidateGet(string iD)
        {
            if (!_sceneContexts.ContainsKey(iD))
                throw new GetException(iD);
        }

		public class AlreadyAddedException : Exception
        {
            public AlreadyAddedException(string iD) : base(string.Format(alreadyAddedExceptionMessage, iD)) { }
        }

        public class NotAddedException : Exception
        {
            public NotAddedException(string iD) : base(string.Format(notAddedExceptionMessage, iD)) { }
        }

        public class GetException : Exception
        {
            public GetException(string iD) : base(string.Format(getExceptionMessage, iD)) { }
        }

        public class ActiveSceneDependenciesException : Exception
        {
            public ActiveSceneDependenciesException(string iD) : base(string.Format(activeSceneDependenciesExceptionMessage, iD)) { }
        }
    }
}
