using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JsonSerializer.Data
{
    public class JToken
    {
        protected JToken()
        {

        }

        public static JToken Parse(string json)
        {
            int endIndex;
            var answer = Parse(json, 0, out endIndex);
            if (json.TryNextChar(endIndex, out endIndex))
            {
                int nextCharIndex;
                var nextChar = json.FindCharNotIsSpaceFollowByIndex(endIndex, out nextCharIndex);
                if (nextCharIndex != -1) throw ExceptionHelpers.MakeJsonErrorException(json, nextCharIndex);
            }

            return answer;
        }

        protected static JToken Parse(string json, int start, out int endIndex)
        {
            JToken answer = null;
            endIndex = start;
            for (int i = start; i < json.Length; i++)
            {
                if (' '.Equals(json[i]))
                    continue;
                var c = json[i];
                if ('{'.Equals(c))
                {
                    answer = JObject.Parse(json, i, out endIndex);
                    break;
                }
                else if ('['.Equals(c))
                {
                    answer = JArray.Parse(json, i, out endIndex);
                    break;
                }
                else if ('"'.Equals(c) || '\''.Equals(c))
                {
                    var nextIndex = json.FindNextCharIndexFromIndex(c, i + 1);
                    if (nextIndex == -1)
                        throw ExceptionHelpers.MakeJsonErrorException(json, i);
                    var content = json.Substring(i + 1, nextIndex - i - 1);
                    answer = (JToken)content;
                    endIndex = nextIndex;
                    break;
                }
                else if ('.'.Equals(c) || (c >= '0' && c <= '9'))
                {
                    var number = json.FindNumberFollowByIndex(i);
                    endIndex = i + number.Length - 1;
                    answer = (JToken)double.Parse(number);
                    break;
                }
                else if (json.HasStringFollowByIndex("null", i))
                {
                    answer = null;
                    endIndex = i + 3;
                    break;
                }
                else if (json.HasStringFollowByIndex("true", i))
                {
                    answer = (JToken)true;
                    endIndex = i + 3;
                    break;
                }
                else if (json.HasStringFollowByIndex("false", i))
                {
                    answer = (JToken)false;
                    endIndex = i + 4;
                    break;
                }
                else
                {
                    throw ExceptionHelpers.MakeJsonErrorException(json, i);
                }
            }
            return answer;
        }

        public static implicit operator string(JToken s)
        {
            if (s is JValue)
            {
                return ((JValue)s).m_data as string;
            }
            throw ExceptionHelpers.MakeCastJsonException(typeof(string));
        }

        public static explicit operator JToken(string s)
        {
            return new JValue(s);
        }

        public static implicit operator int(JToken s)
        {
            if (s is JValue)
            {
                return (int)((JValue)s).m_data;
            }
            throw ExceptionHelpers.MakeCastJsonException(typeof(string));
        }

        public static explicit operator JToken(int s)
        {
            return new JValue(s);
        }

        public static implicit operator long(JToken s)
        {
            if (s is JValue)
            {
                return (long)((JValue)s).m_data;
            }
            throw ExceptionHelpers.MakeCastJsonException(typeof(string));
        }

        public static explicit operator JToken(long s)
        {
            return new JValue(s);
        }

        public static implicit operator double(JToken s)
        {
            if (s is JValue)
            {
                return (double)((JValue)s).m_data;
            }
            throw ExceptionHelpers.MakeCastJsonException(typeof(string));
        }

        public static explicit operator JToken(double s)
        {
            return new JValue(s);
        }

        public static implicit operator float(JToken s)
        {
            if (s is JValue)
            {
                return (float)((JValue)s).m_data;
            }
            throw ExceptionHelpers.MakeCastJsonException(typeof(string));
        }

        public static explicit operator JToken(float s)
        {
            return new JValue(s);
        }

        public static implicit operator decimal(JToken s)
        {
            if (s is JValue)
            {
                return  (decimal)((JValue)s).m_data;
            }
            throw ExceptionHelpers.MakeCastJsonException(typeof(string));
        }

        public static explicit operator JToken(decimal s)
        {
            return new JValue(s);
        }

        public static implicit operator bool(JToken s)
        {
            if (s is JValue)
            {
                return (bool)((JValue)s).m_data;
            }
            throw ExceptionHelpers.MakeCastJsonException(typeof(string));
        }

        public static explicit operator JToken(bool s)
        {
            return new JValue(s);
        }

        public virtual string ToString(bool isPretty)
        {
            return "";
        }
    }
}
