using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSerializer.Data
{
    public class JValue : JToken
    {
        internal object m_data;

        internal JValue(object obj)
        {
            m_data = obj;
        }

        public static implicit operator string(JValue s)
        {
            return s.m_data as string;
        }

        public static explicit operator JValue(string s)
        {
            return new JValue(s);
        }

        public static implicit operator int(JValue s)
        {
            return (int)s.m_data;
        }

        public static explicit operator JValue(int s)
        {
            return new JValue(s);
        }

        public static implicit operator long(JValue s)
        {
            return (long)s.m_data;
        }

        public static explicit operator JValue(long s)
        {
            return new JValue(s);
        }

        public static implicit operator double(JValue s)
        {
            return (double)s.m_data;
        }

        public static explicit operator JValue(double s)
        {
            return new JValue(s);
        }

        public static implicit operator float(JValue s)
        {
            return (float)s.m_data;
        }

        public static explicit operator JValue(float s)
        {
            return new JValue(s);
        }

        public static implicit operator decimal(JValue s)
        {
            return (decimal)s.m_data;
        }

        public static explicit operator JValue(decimal s)
        {
            return new JValue(s);
        }

        public static implicit operator bool(JValue s)
        {
            return (bool)s.m_data;
        }

        public static explicit operator JValue(bool s)
        {
            return new JValue(s);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public override string ToString(bool isPretty)
        {
            var sb = new StringBuilder();
            if (m_data == null)
            {
                sb.Append("null");
                return sb.ToString();
            }
            else if (m_data is string)
            {
                sb.Append('"');
                sb.Append(m_data);
                sb.Append('"');
            }
            else if (m_data is Boolean)
            {
                sb.Append(m_data.ToString().ToLower());
            }
            else
            {
                sb.Append(m_data.ToString());
            }
            return sb.ToString();
        }
    }
}
