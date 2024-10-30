using System;
using System.Collections.Generic;

namespace LK.SimpleFramework
{
    /// <summary>
    /// 控制值在指定的下限和上限之间的类型
    /// </summary>
    public class Limited<T>
    {

        #region Private fields and properties
        private T m_Value;
        private T m_MaxValue;
        private T m_MinValue;
        private bool m_HasUpper;
        private bool m_HasLower;
        private readonly IComparer<T> m_Comparer;
        #endregion

        #region Public or protected fields and properties
        /// <summary>
        /// 获取或设置 <see cref="Limited{T}"/> 的值。
        /// </summary>
        public T Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
                Fix();
            }
        }

        /// <summary>
        /// 获取或设置 <see cref=" Limited{T}"/> 的上限值。若没有上限值，则返回 <see cref="Value"/>
        /// </summary>
        public T UpperLimitValue
        {
            get
            {
                if (m_HasUpper) return m_MaxValue;
                else return m_Value;
            }
            set
            {
                m_MaxValue = value;
                m_HasUpper = true;
                Fix();
            }
        }

        /// <summary>
        /// 获取或设置 <see cref="Limited{T}"/> 下限值。
        /// </summary>
        public T LowerLimitValue
        {
            get
            {
                if (HasLowerLimit) return m_MinValue;
                else return m_Value;
            }
            set
            {
                m_MinValue = value;
                m_HasLower = true;
                Fix();
            }
        }

        /// <summary>
        /// 若 <see cref="Limited{T}"/> 有上限值，则返回 true。
        /// </summary>
        public bool HasUpperLimit => m_HasUpper;

        /// <summary>
        /// 若 <see cref="Limited{T}"/> 有下限值，则返回 true。
        /// </summary>
        public bool HasLowerLimit => m_HasLower;

        /// <summary>
        /// 获取此 <see cref="Limited{T}"/> 所使用的比较器。
        /// </summary>
        public IComparer<T> Comparer => m_Comparer ?? Comparer<T>.Default;
        #endregion

        #region Public or protected methods
        
        public Limited() : this(default(T), null)
        {

        }

        public Limited(T value) : this(value, null)
        {

        }
        
        public Limited(T value, T lower, T upper) : this(value, lower, upper, null)
        {

        }

        public Limited(T value, IComparer<T> comparer)
        {
            m_Value = value;
            m_MaxValue = default(T);
            m_MinValue = default(T);
            m_HasUpper = false;
            m_HasLower = false;
            m_Comparer = comparer;
        }

        public Limited(T value, T lower, T upper, IComparer<T> comparer)
        {
            if(comparer.Compare(value, upper) > 0 || comparer.Compare(value, lower) < 0)
            {
                throw new ArgumentException($"{nameof(value)} is greater than {nameof(upper)} of less than {nameof(lower)}.");
            }
            m_Value = value;
            m_MaxValue = lower;
            m_MinValue = upper;
            m_HasUpper = true;
            m_HasLower = true;
            m_Comparer = comparer;
        }

        /// <summary>
        /// 创建一个具有上限值的 <see cref="Limited{T}"/>。
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static Limited<T> CreateUpperLimitValue(T value, T upper)
        {
            if(Comparer<T>.Default.Compare(value, upper) > 0)
            {
                throw new ArgumentException($"{nameof(value)} is greater than {nameof(upper)}.");
            }
            Limited<T> result = new Limited<T>(value);
            result.m_MaxValue = upper;
            result.m_HasUpper = true;
            return result;
        }

        /// <summary>
        /// 创建一个使用指定的比较器并且具有上限值的 <see cref="Limited{T}"/>。
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static Limited<T> CreateUpperLimitValue(T value, T upper, IComparer<T> comparer)
        {
            if (comparer.Compare(value, upper) > 0)
            {
                throw new ArgumentException($"{nameof(value)} is greater than {nameof(upper)}.");
            }
            Limited<T> result = new Limited<T>(value, comparer);
            result.m_MaxValue = upper;
            result.m_HasUpper = true;
            return result;
        }

        /// <summary>
        /// 创建一个具有下限值的 <see cref="Limited{T}"/>。
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static Limited<T> CreateLowerLimitValue(T value, T lower)
        {
            if (Comparer<T>.Default.Compare(value, lower) < 0)
            {
                throw new ArgumentException($"{nameof(value)} is less than {nameof(lower)}.");
            }
            Limited<T> result = new Limited<T>(value);
            result.m_MinValue = lower;
            result.m_HasLower = true;
            return result;
        }

        /// <summary>
        /// 创建一个使用指定的比较器并且具有下限值的 <see cref="Limited{T}"/>。
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static Limited<T> CreateLowerLimitValue(T value, T lower, IComparer<T> comparer)
        {
            if (comparer.Compare(value, lower) < 0)
            {
                throw new ArgumentException($"{nameof(value)} is greater than {nameof(lower)}.");
            }
            Limited<T> result = new Limited<T>(value, comparer);
            result.m_MinValue = lower;
            result.m_HasLower = true;
            return result;
        }

        public static implicit operator T(Limited<T> limitedValue) => limitedValue.Value;

        public override string ToString() => m_Value.ToString();
        
        #endregion

        #region Private methods
        private void Fix()
        {
            if(m_Comparer is null)
            {
                DefaultComparerFix();
            }
            else
            {
                CustomComparerFix();
            }
        }

        private void DefaultComparerFix()
        {
            IComparer<T> comp = Comparer<T>.Default;
            if (m_HasUpper && comp.Compare(m_Value, m_MaxValue) > 0)
            {
                m_Value = m_MaxValue;
            }
            else if (m_HasLower && comp.Compare(m_Value, m_MinValue) < 0)
            {
                m_Value = m_MinValue;
            }
        }

        private void CustomComparerFix()
        {
            if (m_HasUpper && m_Comparer.Compare(m_Value, m_MaxValue) > 0)
            {
                m_Value = m_MaxValue;
            }
            else if (m_HasLower && m_Comparer.Compare(m_Value, m_MinValue) < 0)
            {
                m_Value = m_MinValue;
            }
        }
        #endregion

    }
}
