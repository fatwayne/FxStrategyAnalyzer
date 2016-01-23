using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;

namespace StrategyLanguageParser.MetaModel
{
    public abstract class TimeIntervalDefinition : AstNode
    {

    }

    public class ConcreteTimeDefinition : TimeIntervalDefinition
    {
        public DateTime DateTime { get; set; }
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            DateTime = (DateTime) treeNode.Token.Value;

        }

    }

    public class PeriodicTimeDefinition : TimeIntervalDefinition
    {
        public Constant Value { get; set; }
        public PeriodicType PeriodicType { get; set; }
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Value = treeNode.FirstChild.AstNode as Constant;
            string periodType = treeNode.ChildNodes[1].FindTokenAndGetText().ToLower();
            switch (periodType)
            {
                case "month":
                case "months": PeriodicType = MetaModel.PeriodicType.Month; break;
                case "days" :
                case "day": PeriodicType = MetaModel.PeriodicType.Day; break;
                default:
                    throw new ArgumentException("Unregconized period type:" + periodType);
            }
        }
    }

    public class WeekDayTimeDefinition : TimeIntervalDefinition
    {
        public Weekday Weekday { get; set; }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Weekday = treeNode.FirstChild.AstNode as Weekday;
           
        }
    }

    

    public enum PeriodicType
    {
        Month,
        Day
    }

}
