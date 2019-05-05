using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExpressionCalculation.Interfaces;

namespace ExpressionCalculation
{
    /// <summary>
	/// класс-читатель выражения из файла
	/// </summary>
	/// Autor:Oleg Golenischev
    public class ExpressionFileReader : IExpressionFileReader
    {
        private readonly IExpressionValidator _expressionValidator;

        public ExpressionFileReader()
        {
            _expressionValidator = new ExpressionValidator();

        }

        public ExpressionFileReader(IExpressionValidator expressionValidator)
        {
            _expressionValidator = expressionValidator;

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
            _expressionValidator.ValidateExpression(expression);
            return expression;
        }
    }
}