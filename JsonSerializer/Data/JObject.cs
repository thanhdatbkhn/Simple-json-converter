using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSerializer.Data
{
    public class JObject : JToken
    {
        private IDictionary<string, JToken> m_items;

        public JObject()
        {
            m_items = new Dictionary<string, JToken>();
        }

        public void Add(string key, JToken value)
        {
            m_items.Add(key, value);
        }

        public void Add(KeyValuePair<string, JToken> keyValuePair)
        {
            m_items.Add(keyValuePair);
        }

        //public new static JObject Parse(string json)
        //{
        //    var l_json = json.TrimStart().TrimEnd();
        //    if (!json.StartsWith("{") || !json.EndsWith("}"))
        //        throw new Exception("can not parse json from string: " + json);
        //    var propJson = l_json.Substring(1, l_json.Length - 2);

        //    var result = new JObject();

        //    return result;
        //}

        internal new static JObject Parse(string json, int start, out int endIndex)
        {
            var result = new JObject();
            endIndex = start;
            int indexPointer = start;
            if (!json.TryNextChar(indexPointer, out indexPointer))
                throw ExceptionHelpers.MakeJsonErrorException(json, start);

            while (true)
            {
                int endJObject;
                if (IsEndObject(json, indexPointer, out endJObject))
                {
                    indexPointer = endJObject;
                    break;
                }

                int endProperty;
                var prop = GetNextProperty(json, indexPointer, out endProperty);
                result.Add(prop);
                indexPointer = endProperty;

                if (!json.TryNextChar(indexPointer, out indexPointer))
                    throw ExceptionHelpers.MakeJsonErrorException(json, start);

                int nextCharIndex;
                var nextChar = json.FindCharNotIsSpaceFollowByIndex(indexPointer, out nextCharIndex);
                if (nextCharIndex == -1)
                    throw ExceptionHelpers.MakeJsonErrorException(json, endProperty);
                indexPointer = nextCharIndex;
                if (','.Equals(nextChar))
                {
                    if (!json.TryNextChar(indexPointer, out indexPointer))
                        throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);
                }
                else
                {
                    if (IsEndObject(json, indexPointer, out endJObject))
                    {
                        indexPointer = endJObject;
                        break;
                    }
                    throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);
                }
            }

            endIndex = indexPointer;
            return result;
        }

        private static bool IsEndObject(string json, int start, out int endIndex)
        {
            int nextCharIndex;
            endIndex = start;
            var nextChar = json.FindCharNotIsSpaceFollowByIndex(start, out nextCharIndex);
            return nextCharIndex != -1 && '}'.Equals(nextChar);
        }

        private static KeyValuePair<String, JToken> GetNextProperty(string json, int start, out int endIndex)
        {
            int indexPointer;
            int endPropName;
            var name = GetNextPropertyName(json, start, out endPropName);
            indexPointer = endPropName;

            if (!json.TryNextChar(indexPointer, out indexPointer))
                throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);

            int nextCharIndex;
            var nextChar = json.FindCharNotIsSpaceFollowByIndex(indexPointer, out nextCharIndex);
            if (nextCharIndex == -1 || !':'.Equals(nextChar))
                throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);

            indexPointer = nextCharIndex;

            if (!json.TryNextChar(indexPointer, out indexPointer))
                throw ExceptionHelpers.MakeJsonErrorException(json, indexPointer);

            var propValue = JToken.Parse(json, indexPointer, out endIndex);
            return new KeyValuePair<string, JToken>(name, propValue);
        }

        private static string GetNextPropertyName(string json, int start, out int endIndex)
        {
            for (int i = start; i < json.Length; i++)
            {
                var c = json[start];
                if (' '.Equals(c))
                {
                    continue;
                }
                else if ('"'.Equals(c) || '\''.Equals(c))
                {
                    var index = json.FindNextCharIndexFromIndex(c, i + 1);
                    var name = json.Substring(i + 1, index - i - 1);
                    endIndex = index;
                    return name;
                }
                else
                {
                    throw ExceptionHelpers.MakeJsonErrorException(json, i);
                }
            }

            throw ExceptionHelpers.MakeJsonErrorException(json, start);
        }



        private static Dictionary<string, JToken> GetJsonProperties(String json, int start, int end)
        {
            Dictionary<string, JToken> answers = new Dictionary<string, JToken>();
            for (int i = start; i < end; i++)
            {

            }
            return answers;
        }

        public JToken this[string key]
        {
            get
            {
                return m_items[key];
            }

            internal set
            {
                m_items[key] = value;
            }
        }

        public override string ToString()
        {
            return this.ToString(true);
        }

        public override string ToString(bool isPretty)
        {
            var sb = new StringBuilder();
            sb.Append('{');
            foreach (var item in m_items)
            {
                sb.Append(" \"");
                sb.Append(item.Key);
                sb.Append("\"");
                sb.Append(" : ");
                sb.Append(item.Value.ToString(isPretty));
                sb.Append(',');
            }
            if (m_items.Count > 0)
                sb.Remove(sb.Length - 1, 1);
            sb.Append(" }");

            return sb.ToString();
        }
    }
}
