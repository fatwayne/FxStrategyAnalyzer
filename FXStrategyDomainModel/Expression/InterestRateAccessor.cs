using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class InterestRateAccessor : TimeDataSetAccessor
    {
        public InterestRateAccessor(Expression currencyCode)
        {
            _currencyCode = currencyCode;
        }

        private Expression _currencyCode;

        public Expression CurrencyCode
        {
            get { return _currencyCode; }
            set { _currencyCode = value; }
        }

        public override object Eval(EvaluationContext.Context evaluationContext)
        {
            string currencyCode = _currencyCode.Eval(evaluationContext) as string;
            return evaluationContext.CurrencyDataSource.GetInterestRateData(currencyCode);
        }
    }
}
