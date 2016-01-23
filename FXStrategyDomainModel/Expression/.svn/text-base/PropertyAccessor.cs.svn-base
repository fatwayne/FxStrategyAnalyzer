using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FXStrategy.EvaluationContext;

namespace FXStrategy.MetaModel
{
    public class PropertyAccessor : Expression
    {
        public PropertyAccessor(Variable variable, string propertyName)
        {
            _variable = variable;
            _propertyName = propertyName;
        }

        private Variable _variable;
        public Variable Variable
        {
            get { return _variable; }
            set { _variable = value; }
        }

        private string _propertyName;
        public string PropertyName
        {
            get { return _propertyName; }
        }


        public override System.Type Type
        {
            get
            {
                var propertyInfo = Variable.GetType().GetProperty(_propertyName);
                if (propertyInfo != null)
                    return propertyInfo.GetType();
                else
                    throw new Exception(String.Format("The property ({0}) of this variable {1} is not valid: ",
                        _propertyName, Variable.Name));
            }
        }

        public override object Eval(Context evaluationContext)
        {
            object target = Variable.Eval(evaluationContext);

            // property access through reflection
            var propertyInfo = target.GetType().GetProperty(_propertyName);
            if (propertyInfo != null)
                return propertyInfo.GetValue(target, null);
            else
                throw new Exception(String.Format("The property ({0}) of this variable {1} is not valid: ",
                    _propertyName, Variable.Name));
        }
    }
}
