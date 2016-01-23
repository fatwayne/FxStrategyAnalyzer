using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;

namespace Irony.Ast
{
    public class TradingStrategyAstNode : AstNode
    {

        public List<TradingRuleAstNode> TradingRules{get;set;}
        
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            TradingRules = new List<TradingRuleAstNode>();
            foreach (var node in treeNode.ChildNodes[1].ChildNodes)
            {
                TradingRules.Add(node.AstNode as TradingRuleAstNode);
            }

            PositionSets = new List<DefinePositionSetAstNode>();
            StrategyParameters = new List<StrategyParameterAstNode>();

            AstNode targetAstNode;
            foreach (var node in treeNode.ChildNodes[0].ChildNodes)
            {
                targetAstNode = node.FirstChild.AstNode as AstNode;
                if (targetAstNode is DefinePositionSetAstNode)
                    PositionSets.Add(targetAstNode as DefinePositionSetAstNode);
                else if (targetAstNode is StrategyParameterAstNode)
                    StrategyParameters.Add(targetAstNode as StrategyParameterAstNode);
            }

        }

        public override void EvaluateNode(Interpreter.EvaluationContext context, AstMode mode, DateTime time)
        {
            foreach (var rule in TradingRules)
            {
                rule.EvaluateNode(context, AstMode.None, time);
            }
        }

        //public IEnumerable<TradingRule> TradingRules
        //{
        //    get
        //    {                
        //        foreach (var tradingRule in base.ChildNodes[1].ChildNodes)
        //        {
        //            yield return tradingRule as TradingRule;
        //        }
        //    }
        //}

        public List<DefinePositionSetAstNode> PositionSets { get; set; }
        public List<StrategyParameterAstNode> StrategyParameters { get; set; }
    }
}
