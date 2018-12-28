using InfixCalculator.Brackets;
using InfixCalculator.Operands;
using InfixCalculator.Operations;

namespace InfixCalculator.Factory
{
    public interface IFactory
    {
        IOperand CreateOperand(string operand);
        IOperation CreateOperation(string operation);
        IBracket CreateBracket(char type);
    }
}
