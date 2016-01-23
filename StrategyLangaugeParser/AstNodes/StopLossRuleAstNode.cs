using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class ConditionalRuleAstNode : GeneralRuleAstNode
    {
        private SpecialRuleType _ruleType;

        public SpecialRuleType RuleType
        {
            get { return _ruleType; }
        }

        private PositionSetAstNode _positionSet;

        public PositionSetAstNode PositionSet
        {
            get { return _positionSet; }
        }

        private OperationExprAstNode _condition;

        public OperationExprAstNode Condition
        {
            get { return _condition; }
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            Name = treeNode.ChildNodes[0].FindTokenAndGetText();

            ExecuteFrequency = AddChild("executeFrequency", treeNode.ChildNodes[1]) as ExecutonFrequencyAstNode;
            _positionSet = AddChild("positionSet", treeNode.ChildNodes[2]) as PositionSetAstNode;
            _condition = AddChild("stopLossCondition", treeNode.ChildNodes[3]) as OperationExprAstNode;

            Statements = (StatementListNode)AddChild("statements", treeNode.ChildNodes[4]);

        }
    }
}
