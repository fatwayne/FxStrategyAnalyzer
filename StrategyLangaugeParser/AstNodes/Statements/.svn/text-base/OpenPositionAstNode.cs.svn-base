using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class OpenPositionAstNode : PortfolioOperationAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            _positionSetAstNode = (PositionSetAstNode)AddChild("PositionSetAstNode", treeNode.ChildNodes[0]);
            _expressionAstNode = (ExpressionAstNode)AddChild("ExpressionAstNode", treeNode.ChildNodes[1]);
        }

        private PositionSetAstNode _positionSetAstNode;

        public PositionSetAstNode PositionSetAstNode
        {
            get { return _positionSetAstNode; }
        }

        private ExpressionAstNode _expressionAstNode;

        public ExpressionAstNode ExpressionAstNode
        {
            get { return _expressionAstNode; }
        }
       
    }
}
