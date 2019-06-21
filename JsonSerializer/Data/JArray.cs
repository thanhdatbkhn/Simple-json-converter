using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JsonSerializer.Data
{
    public class JArray : JToken
    {
        private JToken[] m_items;
        public JArray()
        {

        }

        public new static JArray Parse(string json)
        {
            var result = new JArray();

            return result;
        }
    }
}
