using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;
using FXStrategy.DataAccess;
namespace FXStrategy.Interpreter
{
    public class PortfolioRuntime
    {
        private Dictionary<string, PositionSetRuntime> _positionSets;


        public PortfolioRuntime()
        {
            _positionSets = new Dictionary<string, PositionSetRuntime>();
        }

        public List<PositionRuntime> GetPositionByName(string positionSetName)
        {
            return _positionSets[positionSetName].Positions;
        }

        public void CreatePositionSet(string positionSetName, Currency baseCurrency, int capacity, PositionType type)
        {
            _positionSets.Add(positionSetName, new PositionSetRuntime(capacity, type,baseCurrency));
        }
    }
}
