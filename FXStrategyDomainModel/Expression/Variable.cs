using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.EvaluationContext;
using FXStrategy.MetaModel.DataType;

namespace FXStrategy.MetaModel
{
    public class Variable : Expression
    {
        public Variable(string name)
        {
            Name = name;
        }

        public Variable(string name, Type type)
        {
            Name = name;
            _type = type;
        }

        public string Name
        {
            get;
            set;
        }

        private Type _type;
        public override Type Type
        {
            get{
                return _type;   
            }
        }

        public void SetType(Type type)
        {
            _type = type;
        }
       

        /// <summary>
        /// for pre-defined set, they are Top3Currencies or Bottom3Currencies
        /// </summary>
        /// <param name="evaluationContext"></param>
        /// <returns></returns>
        public object PredefinedDataSetEval(Context evaluationContext)
        {
            var result = evaluationContext.PredefinedDataContainer.GetData(Name, evaluationContext.CurrentDate);
            return result;
        }



        public override object Eval(Context evaluationContext)
        {
            if (Type == typeof(PredefinedDataSet))
                return PredefinedDataSetEval(evaluationContext);
                
            return evaluationContext.ValuesTable[this.Name];
        }

    }
}
