using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculation.Interfaces
{
    interface IExpressionValidator
    {
        bool ValidateExpression(string expression);
    }
}
