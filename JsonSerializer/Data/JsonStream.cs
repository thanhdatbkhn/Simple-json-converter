using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSerializer.Data
{
    public class JsonStream
    {
        private string json;

        private int currentIndex;
        public char CurrentChar
        {
            get
            {
                return json[currentIndex];
            }
        }

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
        }

        public JsonStream(string json)
        {
            this.json = json + ' ';
        }

        public void Move()
        {
            Move(1);
        }

        public void Move(int step)
        {
            if (currentIndex + step >= json.Length)
                throw ExceptionHelpers.MakeJsonErrorException(json, currentIndex);
            currentIndex += step;
        }

        public bool HasNextContent()
        {
            for (int i = currentIndex; i < json.Length; i++)
            {
                if (IsContentChar(json[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public char MoveToNextContent()
        {
            for (int i = currentIndex; i < json.Length; i++)
            {
                if (IsContentChar(json[i]))
                {
                    currentIndex = i;
                    return json[i];
                }
            }

            throw ExceptionHelpers.MakeJsonErrorException(json, currentIndex);
        }

        public bool HasNullFollow()
        {
            if (json.Length <= currentIndex + 3)
                return false;
            return json[currentIndex] == 'n' && json[currentIndex + 1] == 'u' && json[currentIndex + 2] == 'l' && json[currentIndex + 3] == 'l';
        }

        public void MoveBehindNull()
        {
            Move(4);
        }

        public bool HasTrueFollow()
        {
            if (json.Length <= currentIndex + 3)
                return false;
            return json[currentIndex] == 't' && json[currentIndex + 1] == 'r' && json[currentIndex + 2] == 'u' && json[currentIndex + 3] == 'e';
        }

        public void MoveBehindTrue()
        {
            Move(4);
        }

        public bool HasFalseFollow()
        {
            if (json.Length <= currentIndex + 4)
                return false;
            return json[currentIndex] == 'f' && json[currentIndex + 1] == 'a' && json[currentIndex + 2] == 'l'
                && json[currentIndex + 3] == 's' && json[currentIndex + 4] == 'e';
        }

        public void MoveBehindFalse()
        {
            Move(5);
        }

        public bool IsStartOfNumber()
        {
            return '.'.Equals(CurrentChar) || ('0' <= CurrentChar && '9' >= CurrentChar);
        }

        private int FindNumberLength()
        {
            int length = 0;
            bool hasDot = false;
            for (int i = currentIndex; i < json.Length; i++)
            {
                var c = json[i];
                if ('.'.Equals(c))
                {
                    if (hasDot)
                        throw ExceptionHelpers.MakeJsonErrorException(json, i);
                    hasDot = true;
                    length++;
                }
                else if ('0' <= c && '9' >= c)
                {
                    length++;
                }
                else
                {
                    break;
                }
            }

            return length;
        }

        public string MoveBehindNumberAndGet()
        {
            var numberLength = FindNumberLength();
            var number = json.Substring(currentIndex, numberLength);
            Move(numberLength);
            return number;
        }

        public bool IsStartOfString()
        {
            return '\''.Equals(CurrentChar) || '"'.Equals(CurrentChar);
        }

        public string MoveBehindStringAndGet()
        {
            var stringMark = CurrentChar;
            if (!IsStartOfString())
                throw ExceptionHelpers.MakeJsonErrorException(json, currentIndex);
            Move();
            int endStringIndex = 0;
            for (int i = currentIndex; i < json.Length; i++)
            {
                var c = json[i];
                if ('\\'.Equals(c))
                {
                    i++;
                    continue;
                }
                if (stringMark.Equals(c))
                {
                    endStringIndex = i;
                    break;
                }
            }
            if (endStringIndex == 0)
                throw ExceptionHelpers.MakeJsonErrorException(json, currentIndex);
            var stringLength = endStringIndex - currentIndex;
            var content = json.Substring(currentIndex, stringLength);
            Move(stringLength + 1);
            return content;
        }

        private bool IsContentChar(char c)
        {
            return !' '.Equals(c);
        }
    }
}
