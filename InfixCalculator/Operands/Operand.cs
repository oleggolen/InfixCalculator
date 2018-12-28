using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfixCalculator.Interfaces;

namespace InfixCalculator.Operands
{
    class Operand : IOperand
    {
        public Operand()
        {
            
        }
        public Operand(float value)
        {
            Value = value;
        }
        public float Value { get; set; }
    }
}
