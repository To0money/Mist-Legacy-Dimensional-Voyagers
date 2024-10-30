using UnityEngine;
using UnityEngine.Assertions;

namespace LK.SimpleFramework
{
    public class UIViewBase : MonoBehaviour
    {

        #region Public or protected methods
        /// <summary>
        /// 获取指定类型的数据模型
        /// </summary>
        protected T GetModel<T>() where T : ModelBase, new()
        {
            T res = ModelSingleton.Instance.GetModel<T>();

#if UNITY_ASSERTIONS
            Assert.IsNotNull(res);
#endif

            return res;
        }

        /// <summary>
        /// 获取指定类型的控制器
        /// </summary>
        protected T GetController<T>() where T : ControllerBase, new()
        {
            T res = ControllerSingleton.Instance.GetController<T>();

#if UNITY_ASSERTIONS
            Assert.IsNotNull(res);
#endif

            return res;
        }
        #endregion

    }
}
