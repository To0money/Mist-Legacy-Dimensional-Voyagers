namespace LK.SimpleFramework
{
    /// <summary>
    /// 单例模板基类
    /// </summary>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {

        #region Public or protected fields and properties
        /// <summary>
        ///  静态实例对象
        /// </summary>
        public static T Instance 
        { 
            get
            {
                if(m_Instance is null)
                {
                    m_Instance = new T();
                }
                return m_Instance;
            }
        }
        #endregion

        #region Private fields and properties
        private static T m_Instance;
        #endregion
    
    }
}
