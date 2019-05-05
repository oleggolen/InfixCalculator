using System.Collections.Generic;
using System.Text;
using ExpressionCalculation.Brackets;
using ExpressionCalculation.Interfaces;

namespace ExpressionCalculation
{
    /// <summary>
    /// Класс, реализующий логику вычисления выражения
    /// </summary>
    public class ExpressionCalculator : IExpressionCalculator
    {
        /// <summary>
        /// Фабрика объектов, которая создаёт объекты-операции
        /// </summary>
        private readonly IFactory _factory;
        /// <summary>
        /// Конструктор, которая инициализирует объекты класса внутри которого создаёт фабрику объектов
        /// </summary>
        public ExpressionCalculator()
        {
            _factory = new Factory.Factory();
        }
        /// <summary>
        /// Метод, который производит вычисление на стеке выражения, записанного в объектной форме и в обратной польской записи
        /// </summary>
        /// <param name="infix">Коллекция объектов-элементов выражения, которая представляет собой запись выражения в обратной польской записи</param>
        /// <returns>Целое число - результат вычисления выражения</returns>
        private float CalculateExpression(IEnumerable<IExpressionElement> infix)
        {
            var stack = new Stack<IOperand>();
            foreach (var el in infix)
            {
                switch (el)
                {
                    case IOperand _:
                        stack.Push(el as IOperand);
                        break;
                    case IOperation _:
                        if (el is IOperation operation)
                        {
                            for (var i = 0; i < operation.OperandsCount; i++)
                                operation.Operands.Add(stack.Pop());
                            var result = operation.Execute();
                            stack.Push(result);
                        }
                        break;
                }
            }
            return stack.Pop().Value;
        }
        /// <summary>
        /// Метод, который переводит строковое выражение в объектную форму
        /// </summary>
        /// <param name="expression">искомое выражение, которое надо перевести в объектную форму</param>
        /// <returns>Коллекцию объектов элементов выражения, представляющую запись выражения в объектной форме</returns>
        private IEnumerable<IExpressionElement> ToObjectForm(string expression)
        {
            var builder = new StringBuilder();
            var elements = new List<IExpressionElement>();
            for (int i = 0; i < expression.Length; i++)
            {
                if ((expression[i] >= '0' && expression[i] <= '9') || (expression[i] == ','))
                    builder.Append(expression[i]);
                else if (expression[i] == '(' || expression[i] == ')')
                {
                    if (builder.Length > 0)
                    {
                        var operand = _factory.CreateOperand(builder.ToString());
                        elements.Add(operand);
                        builder.Clear();
                    }
                    var bracket = _factory.CreateBracket(expression[i]);
                    elements.Add(bracket);
                }
                else
                {
                    if (builder.Length > 0)
                    {
                        var operand = _factory.CreateOperand(builder.ToString());
                        elements.Add(operand);
                        builder.Clear();
                    }
                    IOperation operation = null;
                    if (expression[i] == '-' && ((i == 0) || (i > 0 && expression[i - 1] == '(') ||(i > 0 && (expression[i - 1] < '0' && expression[i] > '9') && expression[i - 1] != ')')))
                        operation = _factory.CreateOperation("u" + expression[i]);
                    else if (i + 2 < expression.Length && expression[i] == 's' && expression[i + 1] == 'i' && expression[i + 2] == 'n')
                    {
                        operation = _factory.CreateOperation("sin");
                        expression = expression.Replace("sin", "");
                    }
                    else if (i + 2 < expression.Length && expression[i] == 'c' && expression[i + 1] == 'o' && expression[i + 2] == 's')
                    {
                        operation = _factory.CreateOperation("cos");
                        expression = expression.Replace("cos", "");
                    }
                    else operation = _factory.CreateOperation(expression[i].ToString());
                    elements.Add(operation);
                }
            }
            if (builder.Length > 0)
                elements.Add(_factory.CreateOperand(builder.ToString()));
            return elements;
        }
        /// <summary>
        /// Метод, переводящий выражение в объектную форму, записанную в обратной польской записи
        /// </summary>
        /// <param name="expression">Выражение, которое надо перевести в обратную польскую запись и в объектную форму</param>
        /// <returns>Коллекция объектов элментов выражение, представляющую сосбой выражение записанное в обратной польской записи</returns>
        private IEnumerable<IExpressionElement> ConvertToInfixExpression(string expression)
        {
            var elements = ToObjectForm(expression);
            var stack = new Stack<IExpressionElement>();
            var infix = new List<IExpressionElement>();
            foreach (var element in elements)
            {
                if (element is IOperand)
                    infix.Add(element);
                else if (element is OpenBracket)
                    stack.Push(element);
                else if (element is CloseBracket && stack.Count > 0)
                {
                    while (stack.Count > 0 && !(stack.Peek() is OpenBracket))
                        infix.Add(stack.Pop());
                    if (stack.Count > 0)
                        stack.Pop();
                }
                else if (element is IOperation)
                {
                    while (stack.Count > 0 && stack.Peek() is IOperation &&
                           (stack.Peek() as IOperation)?.Priority >= (element as IOperation).Priority)
                        infix.Add(stack.Pop());
                    stack.Push(element);
                }
            }
            while (stack.Count > 0)
            {
                infix.Add(stack.Pop());
            }
            return infix;
        }
        /// <summary>
        /// Метод, который производит вычисление выраждения
        /// </summary>
        /// <param name="expression">Выражение, которое необходимо вычислить</param>
        /// <returns>Вещественное число - результат вычисления выражения</returns>
        public float Calculate(string expression)
        {
            var infix = ConvertToInfixExpression(expression);
            return CalculateExpression(infix);
        }
    }
}