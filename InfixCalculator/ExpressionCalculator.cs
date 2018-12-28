using System.Collections.Generic;
using System.Text;
using InfixCalculator.Brackets;
using InfixCalculator.Factory;
using InfixCalculator.Interfaces;
using InfixCalculator.Operands;
using InfixCalculator.Operations;

namespace InfixCalculator
{
    class ExpressionCalculator
    {
        private readonly IFactory _factory;
        public ExpressionCalculator()
        {
            _factory = new Factory.Factory();
        }
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
                    if (expression[i] == '-' && ((i == 0) || (i > 0 && expression[i - 1] == '(') ||
                                                 (i > 0 && (expression[i - 1] < '0' && expression[i] > '9') &&
                                                  expression[i - 1] != ')')))
                        operation = _factory.CreateOperation("u" + expression[i]);
                    else if (i + 2 < expression.Length && expression[i] == 's' &&
                             expression[i + 1] == 'i' && expression[i + 2] == 'n')
                    {
                        operation = _factory.CreateOperation("sin");
                        expression = expression.Replace("sin", "");
                    }
                    else operation = _factory.CreateOperation(expression[i].ToString());
                    elements.Add(operation);

                }

            }
            if (builder.Length > 0)
                elements.Add(_factory.CreateOperand(builder.ToString()));
            return elements;

        }
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
        public float Calculate(string expression)
        {
            var infix = ConvertToInfixExpression(expression);
            return CalculateExpression(infix);
        }
    }
}