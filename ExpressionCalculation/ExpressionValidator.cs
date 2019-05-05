using ExpressionCalculation.Interfaces;

namespace ExpressionCalculation
{
    public class ExpressionValidator : IExpressionValidator
    {
        public ExpressionValidator()
        {
            
        }
        public bool ValidateExpression(string expression)
        {
            return true;
        }
    }
}
