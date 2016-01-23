using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Interpreter;
using Irony.Parsing;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class IfStatementAstNode : StatementAstNode
    {
        public ExpressionAstNode Condition;
        public StatementListNode IfTrueStatments;
        public StatementListNode IfFalseStatements;

        public IfStatementAstNode() { }

        public override void Init(ParsingContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Condition = AddChild("Condition", treeNode.ChildNodes[0]) as ExpressionAstNode;
            IfTrueStatments = AddChild("IfTrueStatments", treeNode.ChildNodes[1]) as StatementListNode;
            if (treeNode.ChildNodes.Count > 2)
                IfFalseStatements = AddChild("IfFalseStatements", treeNode.ChildNodes[2]) as StatementListNode;
        }
    }
}
