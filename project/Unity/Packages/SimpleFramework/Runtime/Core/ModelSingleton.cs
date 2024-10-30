using System.Collections.Generic;
using System;

namespace LK.SimpleFramework
{
    internal class ModelSingleton : Singleton<ModelSingleton>
    {

        #region Private fields and properties
        private Dictionary<Type, ModelBase> m_Dictionary;
        #endregion

        #region Public or protected methods
        public ModelSingleton()
        {
            m_Dictionary = new Dictionary<Type, ModelBase> ();
        }

        /// <summary>
        /// 从缓存中获取一个数据模型，如果缓存中不存在指定的数据，则会新实例化一个数据模型
        /// </summary>
        public T GetModel<T>() where T : ModelBase, new()
        {
            Type type = typeof(T);
            if(m_Dictionary.ContainsKey(type))
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
