using UnityEngine;

namespace LK.SimpleFramework
{  
    /// <summary>
    /// 由圆心和半径定义的一个2D圆形
    /// </summary>
    public struct Circle
    {

        #region Public or protected fields and properties
        /// <summary>
        /// 表示一个圆心在（0，0），半径为0的圆
        /// </summary>
        public static Circle zero = new Circle { center = Vector2.zero, radius = 0f };
        /// <summary>
        /// 表示一个圆心在（0，0），半径为1的单位圆
        /// </summary>
        public static Circle unit = new Circle { center = Vector2.zero, radius = 1f};
        /// <summary>
        /// 表示一个圆心在（0，0），半径为-1的单位圆
        /// </summary>
        public static Circle nagative = new Circle { center = Vector2.zero, radius = -1f };
        /// <summary>
        /// 圆的圆心
        /// </summary>
        public Vector2 center { get; set; }
        /// <summary>
        /// 圆的半径
        /// </summary>
        public float radius { get; set; }
        /// <summary>
        /// 圆的直径
        /// </summary>
        public float diameter 
        {
            get => this.radius * 2;
            set => this.radius = value / 2;
        }
        /// <summary>
        /// 圆的面积
        /// </summary>
        public float area 
        {
            get => Mathf.PI * this.radius * this.radius;
            set => this.radius = Mathf.Sqrt(value / Mathf.PI);
        }
        /// <summary>
        /// 圆的周长
        /// </summary>
        public float perimeter
        {
            get => Mathf.PI * this.radius * 2;
            set => this.radius = value / Mathf.PI / 2;
        }
        #endregion

        #region Public or protected methods
        public Circle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
        public Circle(float centerX, float centerY, float radius)
        {
            this.center = new Vector2(centerX, centerY);
            this.radius = radius;
        }
        /// <summary>
        /// 设置圆的圆心和半径
        /// </summary>
        public void Set(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
        /// <summary>
        /// 如果给定的点在此圆内，则返回 true。
        /// </summary>
        public bool Contains(Vector2 point)
        {
            float distance = Vector2.Distance(point, this.center);
            if(distance > this.radius)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 如果给定的点在此圆内，则返回 true。
        /// </summary>
        public bool Contains(Vector3 point)
        {
            Vector2 _point = new Vector2 { x = point.x, y = point.y};
            float distance = Vector2.Distance(_point, this.center);
            if (distance > this.radius)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 如果另一个圆与此圆重叠，则返回 true。
        /// </summary>
        public bool Overlaps(Circle other)
        {
            float distance = Vector2.Distance(other.center, this.center);
            if(distance > other.radius + this.radius)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            return $"({this.center.x},{this.center.y},{this.radius})";
        }
        #endregion
    
    }
}

