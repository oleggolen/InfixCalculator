using System;
using System.Collections.Generic;
using System.Linq;
using InfixCalculator.Operands;

namespace InfixCalculator.Operations
{
    public class Divide : IOperation
    {
        public int Priority => 3;
        public int OperandsCount => 2;

        public IList<IOperand> Operands { get; }

        public Divide()
        {
            Operands= new List<IOperand>();
        }

        public IOperand Execute()
        {
            if (Operands == null || !Operands.Any()) throw new InvalidOperationException("Нельзя выполнить операцию без операндов");
            if (Operands[0] == null || Operands[1] == null) throw new InvalidOperationException("Один из операндов пуст или не существует");
            if(Operands[0].Value==0) throw new DivideByZeroException("Второй операнд не может быть нулём");
            return new Operand(Operands[1].Value / Operands[0].Value);
        }
    }
}
