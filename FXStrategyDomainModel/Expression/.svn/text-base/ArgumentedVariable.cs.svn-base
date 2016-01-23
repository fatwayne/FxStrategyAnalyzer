using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.MetaModel.DataType;
using FXStrategy.DataAccess;

namespace FXStrategy.MetaModel
{
    /// <summary>
    /// Variable with arugments in constructor
    /// </summary>
    public class ArgumentedVariable : FXStrategy.MetaModel.Variable
    {
        public ArgumentedVariable(string name, Type type, Expression[] arguments)
            : base(name, type)
        {
            _arguments = arguments;
        }

        private Expression[] _arguments;
        public Expression[] Arguments
        {
            get { return _arguments; }
            set { _arguments = value; }
        }

        public override object Eval(EvaluationContext.Context evaluationContext)
        {
            if (Type == typeof(FXStrategy.MetaModel.DataType.ExchangeRate))
            {
                if (Arguments.Count() != 2)
                    throw new Exception("Arguments of the type exchange rate must have two arguments.");

                return evaluationContext.CurrencyDataSource.GetCurrencyPairData((string)Arguments[0].Eval(evaluationContext), (string)Arguments[1].Eval(evaluationContext));
            }
            else
                throw new Exception("This argumented type is not handled");
        }

    }
}
