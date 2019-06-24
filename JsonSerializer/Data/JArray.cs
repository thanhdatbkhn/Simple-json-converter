using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JsonSerializer.Data
{
    public class JArray : JToken
    {
        private JToken[] m_items;
        public JArray(JToken[] array)
        {
            m_items = new JToken[array.Length];
            Array.Copy(array, 0, m_items, 0, array.Length);
        }

        //public new static JArray Parse(string json)
        //{
        //    var result = new JArray();

        //    return result;
        //}

        internal new static JArray Parse(string json, int start, out int endIndex)
        {
            endIndex = start;
            int indexPointer;
            if (!json.TryNextChar(start, out indexPointer))
                throw ExceptionHelpers.MakeJsonErrorException(json, start);
            List<JToken> items = new List<JToken>();
            while (true)
            {
                int endArrayIndex;
                if (IsEndArray(json, indexPointer, out endArrayIndex))
                {
                    indexPointer = endArrayIndex;
                    break;
                }
                int nextObjEndIndex;
                var nextObj = JToken.Parse(json, indexPointer, out nextObjEndIndex);
                indexPointer = nextObjEndIndex;
                items.Add(nextObj);

                if (!json.TryNextChar(indexPointer, out indexPointer))
                    throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);

                int nextCharIndex;
                var nextChar = json.FindCharNotIsSpaceFollowByIndex(indexPointer, out nextCharIndex);
                if (nextCharIndex == -1)
                    throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);
                indexPointer = nextCharIndex;
                if (','.Equals(nextChar))
                {
                    if (!json.TryNextChar(indexPointer, out indexPointer))
                        throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);
                }
                else
                {
                    if (IsEndArray(json, indexPointer, out endArrayIndex))
                    {
                        indexPointer = endArrayIndex;
                        break;
                    }
                    throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);
                }
            }
            endIndex = indexPointer;
            return new JArray(items.ToArray());
        }

        private static bool IsEndArray(string json, int start, out int endIndex)
        {
            int nextCharIndex;
            endIndex = start;
            var nextChar = json.FindCharNotIsSpaceFollowByIndex(start, out nextCharIndex);
            return nextCharIndex != -1 && ']'.Equals(nextChar);
        }

        public JToken this[int index] 
        {
            get
            {
                return m_items[index];
            }

            internal set
            {
                m_items[index] = value;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string ToString(bool isPretty)
        {
            var sb = new StringBuilder();
            sb.Append('[');
            foreach (var item in m_items)
            {
                sb.Append(item.ToString(isPretty));
                sb.Append(',');
            }
            if (m_items.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            return sb.ToString();
        }
    }
}
