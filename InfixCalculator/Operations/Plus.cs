﻿using System;
using System.Collections.Generic;
using System.Linq;
using InfixCalculator.Operands;

namespace InfixCalculator.Operations
{
    public class Plus : IOperation
    {
        public int Priority => 2;
        public IList<IOperand> Operands { get; }

        public int OperandsCount => 2;

        public Plus()
        {
            Operands = new List<IOperand>();
        }
        public IOperand Execute()
        {
            if(Operands==null || !Operands.Any()) throw new InvalidOperationException("Нельзя выполнить операцию без операндов");
            if(Operands[0]==null || Operands[1]==null) throw new InvalidOperationException("Один из операндов пуст или не существует");
            return new Operand(Operands[0].Value + Operands[1].Value);
        }
    }
}
