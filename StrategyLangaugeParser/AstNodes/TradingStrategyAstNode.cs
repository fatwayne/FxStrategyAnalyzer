using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.LanguageParsing.AstNodes;

namespace FXStrategy.LanguageParsing.AstNodes
{
    /// <summary>
    /// the root node in the abstract syntax tree.
    /// The node represents a trading strategy
    /// </summary>
    public class TradingStrategyAstNode : Irony.Ast.AstNode
    {

        public List<GeneralRuleAstNode> TradingRules{get;set;}

        private List<GlobalIdentifierAstNode> _globalParamDef;

        public List<GlobalIdentifierAstNode> GlobalIdentifierDef
        {
            get { return _globalParamDef; }
        }
        public List<DefinePositionSetAstNode> PositionSets { get; set; }
        public List<StrategyParameterAstNode> StrategyParameters { get; set; }
        
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);


            _globalParamDef = new List<GlobalIdentifierAstNode>();

            foreach (var node in treeNode.ChildNodes[0].ChildNodes)
            {
                _globalParamDef.Add((GlobalIdentifierAstNode)node.AstNode );
            }


            PositionSets = new List<DefinePositionSetAstNode>();
            StrategyParameters = new List<StrategyParameterAstNode>();
            Irony.Ast.AstNode targetAstNode;
            foreach (var node in treeNode.ChildNodes[1].ChildNodes)
            {
                targetAstNode = node.FirstChild.AstNode as Irony.Ast.AstNode;
                if (targetAstNode is DefinePositionSetAstNode)
                    PositionSets.Add(targetAstNode as DefinePositionSetAstNode);
                else if (targetAstNode is StrategyParameterAstNode)
                    StrategyParameters.Add(targetAstNode as StrategyParameterAstNode);
            }

            TradingRules = new List<GeneralRuleAstNode>();
            foreach (var node in treeNode.ChildNodes[2].ChildNodes)
            {
                TradingRules.Add(node.AstNode as GeneralRuleAstNode);
            }

        }

      
    }
}
