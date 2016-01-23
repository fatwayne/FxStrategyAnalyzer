using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class DeclarationAstNode : StatementAstNode
    {
        private Type _type;


        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private VariableAstNode _variable;

        public VariableAstNode Variable
        {
            get { return _variable; }
            set { _variable = value; }
        }
        private ExpressionAstNode _initialization;

        public ExpressionAstNode Initialization
        {
            get { return _initialization; }
            set { _initialization = value; }
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            string type = treeNode.ChildNodes[0].FindTokenAndGetText();
            switch (type.ToLower())
            {
                case "string": _type = typeof(string); break;
                case "int": _type = typeof(int); break;
                case "decimal": _type = typeof(decimal); break;
                default:
                    throw new Exception("Unhanled type: " + type);
            }

            _variable = AddChild("variable", treeNode.ChildNodes[1]) as VariableAstNode;
            _initialization = AddChild("initialization", treeNode.ChildNodes[2]) as ExpressionAstNode;

        }
    }
}
