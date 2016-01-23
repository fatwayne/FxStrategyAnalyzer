using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.MetaModel.DataType;

namespace FXStrategy.MetaModel
{
    public class PositionSet : IEnumerable<Position>
    {
        public string Name
        {
            get;
            set;
        }

        public Expression Number
        {
            get;
            set;
        }

        public PositionType PositionType
        {
            get;
            set;
        }

        public List<Position> Positions { get; set; }

        #region IEnumerable<Position> Members

        public IEnumerator<Position> GetEnumerator()
        {
            foreach (var position in Positions)
                yield return position;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
