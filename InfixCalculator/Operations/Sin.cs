﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfixCalculator.Operands;

namespace InfixCalculator.Operations
{
    public class Sin : IOperation
    {
        public int Priority => 2;

        public int OperandsCount => 1;

        public IList<IOperand> Operands { get; }

        public Sin()
        {
            Operands= new List<IOperand>();
        }

        public IOperand Execute()
        {
            if (Operands == null || !Operands.Any()) throw new InvalidOperationException("Нельзя выполнить операцию без операндов");
            if (Operands[0] == null) throw new InvalidOperationException("Один из операндов пуст или не существует");
            return new Operand((float)Math.Sin(Operands[0].Value));
        }
    }
}