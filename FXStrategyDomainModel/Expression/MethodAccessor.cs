using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    public class MethodAccessor : Expression
    {
        public MethodAccessor(Variable variable, string methodName, Expression[] parameters)
        {
            _variable = variable;
            _parameters = parameters;
            _methodName = methodName;
        }

        private string _methodName;

        public string MethodName
        {
            get { return _methodName; }
        }

        private Expression[] _parameters;

        public Expression[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private Variable _variable;

        public Variable Variable
        {
            get { return _variable; }
            set { _variable = value; }
        }

        public override Type Type
        {
            get {
                var methodInfo = _variable.GetType().GetMethods().Where(m => m.Name == MethodName).FirstOrDefault();
                if (methodInfo != null)
                    return methodInfo.ReturnType;
                else
                    throw new Exception(String.Format("The method ({0}) of this variable {1} is not valid: ", _methodName, Variable.Name));
            }
        }

        public override object Eval(EvaluationContext.Context evaluationContext)
        {
            var target = _variable.Eval(evaluationContext);

            var methodInfo = target.GetType().GetMethods().Where(m => m.Name == MethodName).FirstOrDefault();
            if (methodInfo != null)
                return methodInfo.Invoke(
                    target,
                    _parameters.Select(p => p.Eval(evaluationContext)).ToArray()
                    );
            else
                throw new Exception(String.Format("The method ({0}) of this variable {1} is not valid: ", _methodName, Variable.Name));
        }
    }
}
