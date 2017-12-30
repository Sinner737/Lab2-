using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyString
{
    public class MyString
    {
        private char[] stringValue;

        public int Length { get { return stringValue.Length; } }

        public char this[int index] { get { return stringValue[index]; } }

        // Перегрузка оператора сложения строк - конкатенация
        #region Перегрузка операторов
        public static MyString operator +(MyString str1, MyString str2)
        {
            var arr = new char[str1.Length + str2.Length];

            for(int i=0;i<str1.Length;i++)
            {
                arr[i] = str1[i];
            }
            for(int i=str1.Length,j=0;i<str1.Length+str2.Length;i++,j++)
            {
                arr[i] = str2[j];
            }
            return new MyString(arr);
        }

        // Перегрузка операторов сравнения:
        // Строки равны, если их длина и все символы равны
        // Сравнение происходит по-символьно
        // Если все символы совпадают, то меньше та строка, которая имеет меньшую длину
        public static bool operator >(MyString str1, MyString str2)
        {
            if (str1 == str2) return false;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str2.Length -1  < i) return true;
                if (str1[i] > str2[i])
                {
                    return true;
                }
                if (str1[i] < str2[i])
                {
                    return false;
                }
            }
            return false;
        }
        public static bool operator <(MyString str1, MyString str2)
        {
            if (str1 == str2) return false;
            return !(str1 > str2);
        }
        public static bool operator ==(MyString str1, MyString str2)
        {
            if (str1.Length == str2.Length)
            {
                for (int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] != str2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static bool operator !=(MyString str1, MyString str2)
        {
            return !(str1 == str2);
        }

        public static bool operator >=(MyString str1, MyString str2)
        {
            if (str1 == str2 || str1 > str2) return true;
            return false;
        }

        public static bool operator <=(MyString str1, MyString str2)
        {
            if (str1 == str2 || str1 < str2) return true;
            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var str = (MyString)obj;
            return str == this;
        }
#endregion

        #region Конструкторы
        // Конструктор по умолчанию
        public MyString()
        {
            // создание "пустой" строки
            stringValue = new char[0];
        }

        // Конструктор создание строки n элементов
        public MyString(int n)
        {
            stringValue = new char[n];
        }
        
        // Конструктор создания из массива элементов
        public MyString(char[] value)
        {
            stringValue = new char[value.Length];
            value.CopyTo(stringValue,0);
        }

        public MyString(string value)
        {
            stringValue = new char[value.Length];
            value.CopyTo(0, stringValue, 0, value.Length);
        }
        #endregion

        #region Методы
        // Возращает подстроку между позициями indexFirst и indexLast
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexFirst"></param>
        /// <param name="indexLast"></param>
        /// <returns></returns>
        public MyString SubString(int indexFirst, int indexLast)
        {
            if(indexFirst<0 || indexFirst>= Length || indexLast<=indexFirst ||indexLast>=Length)
            {
                throw new ArgumentException("index");
            }
            // создание массива символов и копирование от indexFirst до indexLast
            var arr = new char[indexLast - indexFirst + 1];
            int j = 0;
            for (int i = indexFirst; i < indexLast + 1; i++,j++)
            {
                arr[j] = stringValue[i];
            }
            return new MyString(arr);
        }
        // Возращает подстроку, начиная с позиции indexFirst
        public MyString SubString(int indexFirst)
        {
            return SubString(indexFirst, Length - 1);
        }
        // Поиск подстроки в строке
        public int IndexOf(MyString str)
        {
            return IndexOf(str, 0);
        }
        // Поиск подстроки в строке, начиная с позиции startIndex
        public int IndexOf(MyString str, int startIndex)
        {
            bool isFind = false;
            for (int i = startIndex; i < Length - str.Length + 1; i++)
            {
                isFind = false;
                for (int j = 0; j < str.Length; j++)
                {
                    if (stringValue[i + j] != str[j])
                    {
                        isFind = false;
                        break;
                    }
                    else
                    {
                        isFind = true;
                    }
                }
                if (isFind) return i;
            }
            return -1;
        }
        // Замена символов oldChar на newChar
        public MyString Replace(char oldChar, char newChar)
        {
            var arr = new char[Length];
            for(int i=0;i<Length;i++)
            {
                if(stringValue[i]==oldChar)
                {
                    arr[i] = newChar;
                }else
                {
                    arr[i] = stringValue[i];
                }
            }
            return new MyString(arr);
        }
        // Замена подстроки oldValue на newValue
        public MyString Replace(MyString oldValue, MyString newValue)
        {
            if (oldValue.Length != newValue.Length)
            {
                throw new Exception("Length must be equals");
            }
            char[] arr = new char[Length];
            int index = IndexOf(oldValue);
            if (index == -1)
            {
                // подстрока не найдена, вернем исходную строку
                return new MyString(arr);
            }
            for (int i = 0; i < index; i++)
            {
                arr[i] = stringValue[i];
            }
            for (int i = index, j = 0; i < index+oldValue.Length; i++, j++)
            {
                arr[i] = newValue[j];
            }
            for (int i = index + oldValue.Length, j = 0; i < Length; i++, j++)
            {
                arr[i] = stringValue[j];
            }
            return new MyString(arr);
        }
        // Вставка строки value на место startIndex
        public MyString Insert(int startIndex, MyString value)
        {
            if(startIndex<0 || startIndex>=Length)
            {
                throw new ArgumentException("startIndex");
            }
            var arr = new char[Length + value.Length];
            for(int i = 0; i < startIndex ; i++)
            {
                arr[i] = stringValue[i];
            }
            for(int i=startIndex,j=0;i<startIndex+value.Length;i++,j++)
            {
                arr[i] = value[j];
            }
            for(int i=startIndex,j= startIndex + value.Length; i<Length;i++,j++)
            {
                arr[j] = stringValue[i];
            }
            return new MyString(arr);
        }
        // Удаление подстроки
        public MyString Remove(int startIndex)
        {
            if (startIndex < 0 || startIndex >= Length)
            {
                throw new ArgumentException("startIndex");
            }
            var arr = new char[startIndex];
            for(int i=0;i<startIndex;i++)
            {
                arr[i] = stringValue[i];
            }
            return new MyString(arr);
        }

        public MyString Remove(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= Length)
            {
                throw new ArgumentException("startIndex");
            }
            var arr = new char[Length - count];
            for (int i = 0; i < startIndex; i++)
            {
                arr[i] = stringValue[i];
            }
            for (int i = startIndex + count, j = startIndex; i < Length; i++, j++)
            {
                arr[j] = stringValue[i];
            }
            return new MyString(arr);
        }
        #endregion


        #region Преобразования типов
         // нявное преобразование в массив символов
        public static implicit operator char[](MyString str)
        {
            return str.stringValue;
        }
        // нявное преобразование в строку
        public static implicit operator string (MyString str)
        {
            return new string(str.stringValue);
        }
        public static implicit operator MyString(string str)   // в строку типа MyString
        {
            MyString newStr = new MyString();
            int i = 0;
            for (i = 0; i < str.Length-1; i++)
            {
                newStr += Convert.ToChar(str.Substring(i,1));
            }
            return newStr;
        }
        // явное преобразоване в целочисленное значение
        public static explicit operator int(MyString str)
        {
            int number = 0;int p = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if(!char.IsDigit(str[i])) // проверка что символ - цифра
                {
                    throw new InvalidCastException("Not digit!");
                }
                number += (p * (int)char.GetNumericValue(str[i]));  // суммруем со следущим разрядом
                p *= 10; // увеличиваем разряд
            }
            return number;
        }
        #endregion
    }
}
