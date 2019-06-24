using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JsonSerializer.Data
{
    public static class JsonStringExtension
    {
        public static int FindNextCharIndexFromIndex(this string json, char c, int start)
        {
            for (int i = start; i < json.Length; i++)
            {
                if ('\\'.Equals(json[i]))
                {
                    if (i < json.Length - 1)
                        i++;
                    continue;
                }
                if (c.Equals(json[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool HasStringFollowByIndex(this string json, string compare, int start)
        {
            var str = json.Skip(start).Take(compare.Length).ToArray();
            return compare.Equals(new string(str));
        }

        public static string FindNumberFollowByIndex(this string json, int start)
        {
            bool hasDot = false;
            int length = 0;
            for (int i = start; i < json.Length; i++)
            {
                var c = json[i];
                if ('.'.Equals(c) && !hasDot)
                {
                    hasDot = true;
                }
                else if (c >= '0' && c <= '9')
                {
                }
                else
                {
                    break;
                }
                length++;
            }
            if (length == 0 || (length == 1 && '.'.Equals(json[start])))
                throw new Exception("Cannot get number string follow by index: " + start);
            return json.Substring(start, length);
        }

        public static char? FindCharNotIsSpaceFollowByIndex(this string json, int start, out int endIndex)
        {
            Char? c = null;
            int index = -1;
            for (int i = start; i < json.Length; i++)
            {
                if (!' '.Equals(json[i]))
                {
                    c = json[i];
                    index = i;
                    break;
                }
            }
            endIndex = index;
            return c;
        }

        public static bool TryNextChar(this string json, int start, out int endIndex)
        {
            endIndex = start;
            if (json.Length - 1 > start)
            {
                endIndex++;
                return true;
            }
            return false;
        }
    }
}
