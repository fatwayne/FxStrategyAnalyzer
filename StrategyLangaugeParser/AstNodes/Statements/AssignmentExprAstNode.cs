using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class AssignmentExprAstNode : StatementAstNode
    {
        private VariableAstNode _variable;

        public VariableAstNode Variable
        {
            get { return _variable; }
        }
        private ExpressionAstNode _expression;

        public ExpressionAstNode Expression
        {
            get { return _expression; }
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _variable = AddChild("variable", treeNode.ChildNodes[0]) as VariableAstNode;
            _expression = AddChild("expression", treeNode.ChildNodes[1]) as ExpressionAstNode;
        }
    }
}
