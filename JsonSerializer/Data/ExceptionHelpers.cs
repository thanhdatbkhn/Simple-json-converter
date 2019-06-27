using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSerializer.Data
{
    public class ExceptionHelpers
    {
        public static Exception MakeJsonErrorException(string json, int indexError)
        {
            return new Exception($"Can not parse json string. Json error at index: {indexError}");
        }

        public static Exception MakeJsonErrorException(JsonStream jsonStream)
        {
            return new Exception($"Can not parse json string. Json error at index: {jsonStream.CurrentIndex}");
        }

        public static Exception MakeCastJsonException(Type type)
        {
            return new Exception($"Can not cast with type: " + type.Name);
        }
    }
}
