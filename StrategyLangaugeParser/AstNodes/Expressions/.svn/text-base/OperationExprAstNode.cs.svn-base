using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    /// <summary>
    /// Represents a binary operation node on abstract syntax tree
    /// </summary>
    public class OperationExprAstNode : ExpressionAstNode
    {
        private ExpressionAstNode _leftExpression;

        public ExpressionAstNode LeftExpression
        {
            get { return _leftExpression; }
            set { _leftExpression = value; }
        }
        private ExpressionAstNode _rightExpression;

        public ExpressionAstNode RightExpression
        {
            get { return _rightExpression; }
            set { _rightExpression = value; }
        }
        private string _op;

        public string Op
        {
            get { return _op; }
            set { _op = value; }
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _leftExpression = AddChild("leftExpression", treeNode.ChildNodes[0]) as ExpressionAstNode;
            _op = treeNode.ChildNodes[1].FindTokenAndGetText();
            _rightExpression = AddChild("rightExpression", treeNode.ChildNodes[2]) as ExpressionAstNode;
        }

        public override Type GetType()
        {
            switch (Op.ToLower())
            {
                case "+":
                case "-": 
                case "*": 
                case "/": return ArithmetricType(); 
                case ">" :
                case "<":
                case ">=":
                case "<=":
                case "and": 
                case "or": return typeof(bool);
                default: throw new Exception("Unhandled operation " + Op);
            }
        }

        private Type ArithmetricType()
        {
            if (_leftExpression.GetType() == typeof(decimal) || _rightExpression.GetType() == typeof(decimal))
                return typeof(decimal);
            else if (_leftExpression.GetType() == typeof(int) || _rightExpression.GetType() == typeof(int))
                return typeof(int);
            else if (_leftExpression.GetType() == typeof(string) || _rightExpression.GetType() == typeof(string))
                return typeof(string);
            else
                throw new Exception(String.Format("Operation ('{0}') on type '{1}' and '{2}' is not handled"
                    , Op, _leftExpression.GetType().ToString(), _rightExpression.GetType().ToString()));
        }
    }
}
