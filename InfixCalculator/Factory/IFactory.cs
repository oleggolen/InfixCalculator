using InfixCalculator.Brackets;
using InfixCalculator.Operands;
using InfixCalculator.Operations;

namespace InfixCalculator.Factory
{
    /// <summary>
    /// Интерфейс, представляющий интерфейс фабрики объектов
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Метод создающий объект-операнд
        /// </summary>
        /// <param name="operand">строка представляющая собой операнд</param>
        /// <returns>Объект-операнд, представляющий собой объект хранящий целое число</returns>
        IOperand CreateOperand(string operand);
        /// <summary>
        /// Метод, создающий объект-операцию
        /// </summary>
        /// <param name="operation">Выражение, представляющее собой операцию</param>
        /// <returns>Объект, представляющей собой реализацию математической операции</returns>
        IOperation CreateOperation(string operation);
        /// <summary>
        /// Метод, возвращающий объект-скобку
        /// </summary>
        /// <param name="type">Тип скобки '(" - открывающаяся скобка, ')' - закрывающаяся скобка</param>
        /// <returns>Объект, представляющий собой действие открывающийся скобки</returns>
        IBracket CreateBracket(char type);
    }
}
