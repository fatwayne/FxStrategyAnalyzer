using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategyRunTime;
using FXDataAccess;
using FXStrategy.EvaluationContext;

namespace FXStrategy.CalculationEngine
{
    public class CalculationEngine
    {
        ExchangeRateAdapter _exRateAdapter;
        InterestRateAdapter _inRateAdapter;

        public CalculationEngine(ExchangeRateAdapter exRateAdapter, InterestRateAdapter inRateAdapter)
        {
            _exRateAdapter = exRateAdapter;
            _inRateAdapter = inRateAdapter;    
        }

        public void Analyze(List<PositionRuntime> positionRuntTimes)
        {

        }
 
        public List<TimeSeriesData> 
    }
}
