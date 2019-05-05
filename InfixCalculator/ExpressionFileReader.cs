using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InfixCalculator
{
    /// <summary>
	/// класс-читатель выражения из файла
	/// </summary>
	/// Autor:Oleg Golenischev
    internal class ExpressionFileReader
    {
        /// <summary>
        /// Поле содержащее, коллекцию допустимых символов для записи выражений
        /// </summary>
        private readonly HashSet<char> _symbols;
        /// <summary>
        /// Поле содержащее коллекцию символов, являющихся операциям
        /// </summary>
        private readonly HashSet<char> _operations;
        /// <summary>
        /// Метод, который осуществляет проверку выражения на корректность
        /// </summary>
        /// <param name="expression">Искомое выражение, которое надо проверить на корректность</param>
        /// <exception cref="ArgumentNullException">происходит в случае, если строка равна null или пуста</exception>
        /// <exception cref="ArgumentException">Происходит если:
        /// <list type="bullet">
        /// <item>
        /// <term>Строка не является правильной скобочной последовательностью</term>
        /// <description>Количество открывающихся и закрывающихся скобок в строке не равны</description>
        /// </item>
        /// <item>
        /// <term>Содержит некорректные сиволы</term>
        /// <description>Содрержит символы, не являющиеся десятичными цифрами, круглыми скобками, сиволы допустимых операций</description>
        /// </item>
        /// <item>
        /// <term>Начинается с операции, кроме унарного минуса или открывающейся круглой скобки</term>
        /// </item>
        /// <item>
        /// <term>содержит число перед открывающейся круглой скобкой</term>
        /// </item>
        /// <item>
        /// <term>содержит число после закрывающейся круглой скобки</term>
        /// </item>
        /// <item>
        ///<term>содержит две бинарных операции подряд</term>
        /// </item>
        /// </list>
        /// </exception>
        private void ValidateExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                throw new ArgumentNullException(nameof(expression), "Строка не может быть null или пустой");
            var depth = 0;
            foreach (var t in expression)
            {
                switch (t)
                {
                    case '(':
                        depth++;
                        break;
                    case ')':
                        depth--;
                        if(depth<0) throw new ArgumentException("Строка должна быть правильной скобочной последовательностью");
                        break;
                    default:
                        if(!_symbols.Contains(t))
                            throw new ArgumentException("строка содержит некорректные симбволы");
                        break;
                }
            }
            if(depth!=0) throw new ArgumentException("Строка должна быть правильной скобочной последовательностью");
            if(_operations.Contains(expression[0]) && expression[0]!='-' && expression[0]!='(')
                throw new ArgumentException("Строка не может начинаться с операций кроме - или (");
            while (expression.Length > 1)
            {
                var index = expression.IndexOfAny(_operations.ToArray());
                if(index==-1) return;
                if(index-1>=0 && expression[index]=='(' && !_operations.Contains(expression[index-1]))
                    throw new Exception("перед ( может содержаться только операция");
                if(index+1<expression.Length && expression[index]=='(' && expression[index+1]!='(' && expression[index+1]!='-' && (expression[index+1]<'0' || expression[index+1]>'9'))
                    throw new ArgumentException("Выражение не может содержать двух операций подряд");
                if(index+1<expression.Length && expression[index]!='(' && expression[index]!=')' && expression[index+1]!='(' && _operations.Contains(expression[index+1]))
                    throw new ArgumentException("Выражение не может содержать двух операций подряд");
                if(index+1<expression.Length && expression[index]==')' && !_operations.Contains(expression[index+1]))
                    throw new ArgumentException("поcле ) может быть только операция");
                expression = index + 1 < expression.Length ? expression.Substring(index + 1) : "";
            }

        }

        public ExpressionFileReader()
        {
            _symbols= new HashSet<char>();
            for(var i=0;i<=9;i++)
                _symbols.Add((char)('0'+i));
            _symbols.Add('+');
            _symbols.Add('-');
            _symbols.Add('*');
            _symbols.Add('/');
            _symbols.Add('(');
            _symbols.Add(')');
            _symbols.Add('s');
            _symbols.Add('i');
            _symbols.Add('n');
            _symbols.Add(',');
            _symbols.Add('c');
            _symbols.Add('o');
            _operations = new HashSet<char>
            {
                '+',
                '-',
                '*',
                '/',
                '(',
                ')',
                'n',
                's'
            };
        }
        /// <summary>
        /// Метод чтения выражения из файла
        /// </summary>
        /// <param name="filename">имя файла, из которого нужно прочитать выражение</param>
        /// <returns>строка представляющая собой выражение</returns>
        /// <exception cref="Exception">Возникает в случае ошибки чтения или некорректности выражения</exception>
        public string ReadExpressionFromFile(string filename)
        {
            string expression = null;
            try
            {
                using (var stream = new FileStream(filename, FileMode.Open))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        expression = reader.ReadToEnd();
                    }
                }

            }
            catch (FileNotFoundException)
            {
                throw new Exception("Заданного файла не существует");
            }
            catch (DirectoryNotFoundException)
            {
               throw new Exception("Заданной папки не существует!");
            }
            catch (ArgumentNullException)
            {
                throw new Exception("Заданная строка не может быть пустой");
            }
            catch (ArgumentException)
            {
               throw new Exception("Некорректное имя файла");
            }
            catch (PathTooLongException)
            {
                throw new Exception("Имя файла слишком длинное!");
            }
            ValidateExpression(expression);
            return expression;
        }
    }
}