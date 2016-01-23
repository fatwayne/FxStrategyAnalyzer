using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class PropertyAccessAstNode : FXStrategy.LanguageParsing.AstNodes.ExpressionAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _variable = treeNode.ChildNodes[0].AstNode as VariableAstNode;
            _property = treeNode.ChildNodes[1].FindTokenAndGetText();
        }

        private string _property;
        public string Property
        {
            get
            {
                return _property;
            }
        }

        private VariableAstNode _variable;
        public VariableAstNode Variable
        {
            get
            {
                return _variable;
            }
        }

        public override Type GetType()
        {
            throw new NotImplementedException();
        }
    }
}
