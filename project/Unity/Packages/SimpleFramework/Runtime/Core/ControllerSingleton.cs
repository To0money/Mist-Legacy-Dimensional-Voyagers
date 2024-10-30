using System;
using System.Collections.Generic;

namespace LK.SimpleFramework
{
    internal class ControllerSingleton : Singleton<ControllerSingleton>
    {
        #region Private fields and properties
        private Dictionary<Type, ControllerBase> m_Dictionary;
        #endregion

        #region Public or protected methods
        public ControllerSingleton()
        {
            m_Dictionary = new Dictionary<Type, ControllerBase> ();
        }

        /// <summary>
        /// 从缓存中获取一个控制器，如果缓存中不存在指定的控制器，则会新实例化一个控制器
        /// </summary>
        public T GetController<T>() where T : ControllerBase, new()
        {
            Type type = typeof(T);
            if (m_Dictionary.ContainsKey(type))
            {
                return m_Dictionary[type] as T;
            }
            else
            {
                T res = new T();
                res.Init();
                m_Dictionary.Add(type, res);
                return res;
            }
        }
        #endregion
    }
}
