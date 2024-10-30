using System;

namespace LK.SimpleFramework
{
    /// <summary>
    /// 一个会随着值改变而触发事件的简单封装类
    /// </summary>
    public class EventProperty<T>
    {

        #region Public or protected fields and properties
        /// <summary>
        /// <para>获取或设置 <see cref="EventProperty{T}"/> 的值。</para>
        /// <para>set访问器会检查值是否相等，如果相等，则不会触发事件（若想跳过检查，请使用<see cref="SetValueWithoutCheck"/>）。</para>
        /// </summary>
        public T Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                SetValue(value);
            }
        }
        #endregion

        #region Private fields and properties
        private T m_Value;
        private Action<T> m_Action;
        #endregion

        #region Public or protected methods
        /// <summary>
        /// 以给定的值初始化 <see cref="EventProperty{T}"/>。
        /// </summary>
        public EventProperty(T value)
        {
            m_Value = value;
        }

        /// <summary>
        /// 设置 <see cref="EventProperty{T}"/> 的值，此方法会检查值是否相等，如果相等，则不会触发事件。
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(T value)
        {
            if (m_Value.Equals(value)) return;
            m_Value = value;
            m_Action?.Invoke(m_Value);
        }

        /// <summary>
        /// 设置 <see cref="EventProperty{T}"/> 的值，此方法会跳过值检查，即使值未发生变动，也会触发一次事件。
        /// </summary>
        /// <param name="value"></param>
        public void SetValueWithoutCheck(T value)
        {
            m_Value = value;
            m_Action?.Invoke(m_Value);
        }

        /// <summary>
        /// 设置 <see cref="EventProperty{T}"/> 的值，此方法不会触发事件。
        /// </summary>
        public void SetValueWithoutEvent(T value)
        {
            m_Value = value;
        }

        /// <summary>
        /// 注册一个事件到 <see cref="EventProperty{T}"/>。
        /// </summary>
        public void Register(Action<T> action)
        {
            m_Action += action;
        }

        /// <summary>
        /// 从 <see cref="EventProperty{T}"/> 移除一个事件。
        /// </summary>
        public void Remove(Action<T> action)
        {
            m_Action -= action;
        }

        public override string ToString()
        {
            return m_Value.ToString();
        }
        #endregion

    }
}
