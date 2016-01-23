using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class GlobalIdentifierAstNode : StatementAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _variableName = treeNode.ChildNodes[0].FindTokenAndGetText();
            _expressionAstNode = AddChild("expressionAstNode", treeNode.ChildNodes[1]) as ExpressionAstNode;
        }

        private ExpressionAstNode _expressionAstNode;

        public ExpressionAstNode ExpressionAstNode
        {
            get { return _expressionAstNode; }
        }
        private string _variableName;

        public string VariableName
        {
            get { return _variableName; }
        }
    }
}
