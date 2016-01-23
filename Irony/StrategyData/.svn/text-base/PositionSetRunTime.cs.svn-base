using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.StrategyData
{
    public class PositionSetRunTime
    {
        private List<PositionRuntime> _positions;
        public List<PositionRuntime> Positions
        {
            get { return _positions; }
        }

        public int Capacity;

        public PositionSetRunTime(int capacity, PositionType positionType)
        {
            Capacity = capacity;
            PositionType = positionType;
            _positions = new List<PositionRuntime>(capacity);
            for(int i = 0; i < Capacity; i++){
                _positions.Add(new PositionRuntime(positionType));
            }

        }

        public string Name { get; set; }

        public PositionType PositionType { get; set; }
    }
}
