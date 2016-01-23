using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.LanguageParsing.AstNodes;
using Irony.StrategyData;

namespace FXStrategy.LanguageParsing.AstNodes
{
    /// <summary>
    /// Ast node contains information about execution frequency
    /// </summary>
    public abstract class ExecutonFrequencyAstNode : ExpressionAstNode
    {
        //public virtual bool CanExecute(DateTime startDate, DateTime currentDate)
        //{
        //    var daysInBetween = DateTimeHelper.GetWeekdaysDate(startDate, currentDate);
        //    if (!daysInBetween.Contains(currentDate))
        //        return false;
        //    else
        //        return true;
        //}
    }

    public class ConcreteTimeDefinitionAstNode : ExecutonFrequencyAstNode
    {
        public DateTime ExecuteTime { get; set; }
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            ExecuteTime = (DateTime) treeNode.Token.Value;
        }

        //public override bool CanExecute(DateTime startDate, DateTime currentDate)
        //{
        //    return (ExecuteTime == currentDate);
        //}


        public override Type GetType()
        {
            throw new NotImplementedException();
        }
    }

    public class PeriodicTimeDefinitionAstNode : ExecutonFrequencyAstNode
    {
        public ExpressionAstNode Value { get; set; }
        public PeriodicTypeAst PeriodicType { get; set; }
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            if (treeNode.ChildNodes.Count == 1)
            {
                Value = new IntegerAstNode()
                {
                    Value = 1
                };
                PeriodicType = PeriodicTypeAst.Day;
            }
            else
            {

                Value = AddChild("value", treeNode.FirstChild) as ExpressionAstNode;
                string periodType = treeNode.ChildNodes[1].FindTokenAndGetText().ToLower();
                switch (periodType)
                {
                    case "month":
                    case "months": PeriodicType = PeriodicTypeAst.Month; break;
                    case "days":
                    case "day": PeriodicType = PeriodicTypeAst.Day; break;
                    default:
                        throw new ArgumentException("Unregconized period type:" + periodType);
                }
            }
        }

        public override Type GetType()
        {
            throw new NotImplementedException();
        }
    }

    public class WeekDayTimeDefinitionAstNode : ExecutonFrequencyAstNode
    {
        public WeekdayAstNode Weekday { get; set; }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Weekday = treeNode.FirstChild.AstNode as WeekdayAstNode;
           
        }


        public override Type GetType()
        {
            throw new NotImplementedException();
        }
    }

    

    public enum PeriodicTypeAst
    {
        Month,
        Day,
        Week
    }

}
