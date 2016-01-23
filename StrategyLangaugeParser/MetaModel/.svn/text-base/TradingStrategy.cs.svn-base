using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;

namespace StrategyLanguageParser.MetaModel
{
    public class TradingStrategy : AstNode
    {

        public List<TradingRule> TradingRules{get;set;}
        
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            TradingRules = new List<TradingRule>();
            foreach (var node in treeNode.ChildNodes[1].ChildNodes)
            {
                TradingRules.Add(node.AstNode as TradingRule);
            }

            PositionSets = new List<PositionSet>();
            StrategyParameters = new List<StrategyParameter>();

            AstNode targetAstNode;
            foreach (var node in treeNode.ChildNodes[0].ChildNodes)
            {
                targetAstNode = node.FirstChild.AstNode as AstNode;
                if (targetAstNode is PositionSet)
                    PositionSets.Add(targetAstNode as PositionSet);
                else if (targetAstNode is StrategyParameter)
                    StrategyParameters.Add(targetAstNode as StrategyParameter);
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

        public List<PositionSet> PositionSets { get; set; }
        public List<StrategyParameter> StrategyParameters { get; set; }
    }
}
