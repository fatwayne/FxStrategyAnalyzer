using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public class PortfolioRuntime
    {
        private Dictionary<string, PositionSetRunTime> _positionSets;


        public PortfolioRuntime()
        {
            _positionSets = new Dictionary<string, PositionSetRunTime>();
        }

        public List<PositionRuntime> GetPositionByName(string positionSetName)
        {
            return _positionSets[positionSetName].Positions;
        }

        public void CreatePositionSet(string positionSetName, Currency baseCurrency, int capacity, PositionType type)
        {
            _positionSets.Add(positionSetName, new PositionSetRunTime(capacity, type));
        }
    }
}
