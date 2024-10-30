using LK.SimpleFramework;
using UnityEngine.Assertions;

namespace LK.SimpleFramework
{
    public abstract class StateBase : IState
    {
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();

        protected T GetController<T>() where T : ControllerBase, new()
        {
            T res = ControllerSingleton.Instance.GetController<T>();

#if UNITY_ASSERTIONS
            Assert.IsNotNull(res);
#endif

            return res;
        }
    }
}
