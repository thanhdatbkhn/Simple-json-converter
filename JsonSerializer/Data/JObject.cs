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

        }

        public void Add(string key, JToken value)
        {

        }

        public new static JObject Parse(string json)
        {
            var l_json = json.TrimStart().TrimEnd();
            if (!json.StartsWith("{") || !json.EndsWith("}"))
                throw new Exception("can not parse json from string: " + json);
            var propJson = l_json.Substring(1, l_json.Length - 2);
            
            var result = new JObject();

            return result;
        }

        private static List<KeyValuePair<string, JToken>> GetJsonProperties(String json, int start, int end)
        {
            List<KeyValuePair<string, JToken>> answers = new List<KeyValuePair<string, JToken>>();
        }
    }
}
