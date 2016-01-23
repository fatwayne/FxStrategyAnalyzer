using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.MetaModel
{
    /// <summary>
    /// Return property of elements in a set
    /// </summary>
    public class CollectionPropertiesAccessor : FXStrategy.MetaModel.Expression
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection">Only accept type which is implemented IEnumerable interface</param>
        /// <param name="propertyName">Statements name of the element in the set</param>
        public CollectionPropertiesAccessor(Expression collection, string propertyName)
        {
            _collection = collection;
            if (!collection.Type.GetInterfaces().Any(t => t.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>))))
                throw new Exception("Only collection data is accepted when using SetPropertiesAccessor");

            _propertyName = propertyName;
        }

        private Expression _collection;

        public Expression Collection
        {
            get { return _collection; }
        }

        private string _propertyName;

        public string PropertyName
        {
            get { return _propertyName; }
        }

        public override Type Type
        {
            get
            {
                Type elementType = null;
                List<string> values = new List<string>();
                Type[] interfaces = values.GetType().GetInterfaces();
                foreach (Type i in interfaces)
                    if (i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>)))
                        elementType = i.GetGenericArguments()[0];
                return elementType;
            }
        }

        public override object Eval(FXStrategy.EvaluationContext.Context evaluationContext)
        {
            IEnumerable<object> listPosition = (IEnumerable<object>)Collection.Eval(evaluationContext);

            List<object> result = new List<object>();
            foreach (var position in listPosition)
            {
                var propertyInfo = position.GetType().GetProperty(PropertyName);
                if (propertyInfo != null)
                     result.Add(propertyInfo.GetValue(position, null));
                else
                    throw new Exception(String.Format("The property ({0}) of this collection type {1} is not valid: ", PropertyName, Collection.Type));
            }

            return result;
        }
    }
}
