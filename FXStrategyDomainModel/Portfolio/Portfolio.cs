using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class Portfolio
    {
        private List<PositionSet> _positionSets;

        public List<PositionSet> PositionSets
        {
            get {
                if (_positionSets == null)
                    _positionSets = new List<PositionSet>();
                return _positionSets; }
            set { _positionSets = value; }
        }

        private string _baseCurrency;

        public string HomeCurrency
        {
            get {
                if (_baseCurrency == null)
                    _baseCurrency = "";
                return _baseCurrency; }
            set { _baseCurrency = value; }
        }
      
    }
}
