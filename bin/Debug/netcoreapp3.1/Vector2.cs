using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment
{
   public class Vector2
    {
        private float _x;
        private float _y;

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y);
            }
        }
        
        public Vector2 Normalized
        {
            get
            {
                return Normalize(this);
            }
        }

        public Vector2()
        {
            _x = 0;
            _y = 0;
        }

        public Vector2 (float x , float y)
        {
            _x = x;
            _y = y;
        }

        public static Vector2 Normalize(Vector2 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector2();

            return vector / vector.Magnitude;
        }

        public static float DotProduct(Vector2 Ihs, Vector2 rhs)
        {
            return (Ihs.X * rhs.X) + (Ihs.Y * rhs.Y);
        }

        public static Vector2 operator +(Vector2 Ihs, Vector2 rhs)
        {
            return new Vector2(Ihs.X + rhs.X, Ihs.Y + rhs.Y);
        }

        public static Vector2 operator -(Vector2 Ihs, Vector2 rhs)
        {
            return new Vector2(Ihs.X - rhs.X, Ihs.Y + rhs.Y);
        }

        public static Vector2 operator *(Vector2 Ihs, float scalar)
        {
            return new Vector2(Ihs.X * scalar, Ihs.Y * scalar);
        }

        public static Vector2 operator /(Vector2 Ihs, float scalar)
        {
            return new Vector2(Ihs.X / scalar, Ihs.Y / scalar);
        }
    }
}
