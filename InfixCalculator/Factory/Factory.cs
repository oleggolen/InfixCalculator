using System;
using InfixCalculator.Brackets;
using InfixCalculator.Operands;
using InfixCalculator.Operations;

namespace InfixCalculator.Factory
{
    public class Factory : IFactory
    {
        public Factory()
        {
            
        }
        public IBracket CreateBracket(char type)
        {
            return type == '(' ? (IBracket)new OpenBracket() : new CloseBracket();
        }

        public IOperand CreateOperand(string operand)
        {
            float c = 0;
            if(!float.TryParse(operand,out c)) throw new ArgumentException("операнд не соответствует числу");
            return new Operand(float.Parse(operand));

        }

        public IOperation CreateOperation(string operation)
        {
            switch (operation)
            {
                case "+": return new Plus();
                case "-": return new Minus();
                case "*": return new Multiply();
                case "/": return new Divide();
                case "u-": return new UnaryMinus();
                case "sin": return new Sin();
                default: throw  new InvalidOperationException("operation не является операцией");
            }
            
        }
    }
}
