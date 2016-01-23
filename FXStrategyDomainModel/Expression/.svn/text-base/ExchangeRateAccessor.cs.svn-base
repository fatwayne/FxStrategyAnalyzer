using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    /// <summary>
    /// for retrieve exchange rate
    /// </summary>
    public class ExchangeRateAccessor : TimeDataSetAccessor
    {
        public ExchangeRateAccessor(Expression baseCurrencyCode, Expression variableCurrencyCode)
        {
            _baseCurrencyCode = baseCurrencyCode;
            _variableCurrencyCode = variableCurrencyCode;
        }

        private Expression _baseCurrencyCode;

        public Expression BaseCurrencyCode
        {
            get { return _baseCurrencyCode; }
        }

        private Expression _variableCurrencyCode;

        public Expression VariableCurrencyCode
        {
            get { return _variableCurrencyCode; }
        }

        public override object Eval(EvaluationContext.Context evaluationContext)
        {
            string baseCurrencyCode = _baseCurrencyCode.Eval(evaluationContext) as string;
            string variableCurrencyCode = _variableCurrencyCode.Eval(evaluationContext) as string;

            return evaluationContext.CurrencyDataSource.GetCurrencyPairData(baseCurrencyCode, variableCurrencyCode).ExchangeRates;
        }
    }
}
