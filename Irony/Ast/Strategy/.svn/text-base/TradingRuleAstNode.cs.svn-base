using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;

namespace Irony.Ast
{
    public class TradingRuleAstNode : AstNode
    {
        public StatementListNode Statements { get; set; }
        public string Name { get; set; }
        public TimeIntervalDefinitionAstNode ExecuteFrequency { get; set; }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Name = treeNode.ChildNodes[0].FindTokenAndGetText();
           
            //Statements = treeNode.ChildNodes[2].ChildNodes.Select(c => c.AstNode as Statement);
            ExecuteFrequency = AddChild("executeFrequency", treeNode.ChildNodes[1]) as TimeIntervalDefinitionAstNode;
            Statements = (StatementListNode)AddChild("statements", treeNode.ChildNodes[2]);
        }

        public override void EvaluateNode(Interpreter.EvaluationContext context, AstMode mode, DateTime time)
        {
            if (ExecuteFrequency.CanExecute(context.StartDate, time))
            {
                Statements.EvaluateNode(context, AstMode.None, time);
            }
        }
        
    }
}
