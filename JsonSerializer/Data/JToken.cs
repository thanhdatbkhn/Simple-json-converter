using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSerializer.Data
{
    public class JToken
    {
        private object m_data;

        protected JToken()
        {

        }
        private JToken(object obj)
        {
            m_data = obj;
        }

        public static JToken Parse(string json)
        {
            var jsonStr = json.TrimStart().TrimEnd();
            JToken answer;
            if ("null".Equals(jsonStr))
            {
                answer = new JToken();
            }
            else if (jsonStr.StartsWith("{"))
            {
                answer = JObject.Parse(jsonStr);
            }
            else if (jsonStr.StartsWith("["))
            {
                answer= JArray.Parse(jsonStr);
            }
            else if ("true".Equals(jsonStr) || "false".Equals(jsonStr))
            {
                var boolean = bool.Parse(jsonStr);
                answer = (JToken)boolean;
            }
            else
            {
                decimal number;
                if (decimal.TryParse(jsonStr, out number))
                {
                    answer = (JToken)number;
                }
                else
                {
                    throw new Exception("can not parse json from string: " + jsonStr);
                }
            }
            return answer;
        }

        public static implicit operator string(JToken s)
        {
            return s.m_data as string;
        }

        public static explicit operator JToken(string s)
        {
            return new JToken(s);
        }

        public static implicit operator int(JToken s)
        {
            return (int)s.m_data;
        }

        public static explicit operator JToken(int s)
        {
            return new JToken(s);
        }

        public static implicit operator long(JToken s)
        {
            return (long)s.m_data;
        }

        public static explicit operator JToken(long s)
        {
            return new JToken(s);
        }

        public static implicit operator double(JToken s)
        {
            return (double)s.m_data;
        }

        public static explicit operator JToken(double s)
        {
            return new JToken(s);
        }

        public static implicit operator float(JToken s)
        {
            return (float)s.m_data;
        }

        public static explicit operator JToken(float s)
        {
            return new JToken(s);
        }

        public static implicit operator decimal(JToken s)
        {
            return (decimal)s.m_data;
        }

        public static explicit operator JToken(decimal s)
        {
            return new JToken(s);
        }

        public static implicit operator bool(JToken s)
        {
            return (bool)s.m_data;
        }

        public static explicit operator JToken(bool s)
        {
            return new JToken(s);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (m_data == null)
            {
                sb.Append("null");
                return sb.ToString();
            }

            if (m_data is string)
            {
                sb.Append("'");
                sb.Append(m_data);
                sb.Append("'");
            }
            else
            {
                sb.Append(m_data.ToString());
            }

            return sb.ToString();
        }
    }
}
