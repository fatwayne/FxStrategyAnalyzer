using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;
using Irony.StrategyData;

namespace Irony.Ast
{
    public abstract class TimeIntervalDefinitionAstNode : AstNode
    {
        public virtual bool CanExecute(DateTime startDate, DateTime currentDate)
        {
            var daysInBetween = DateTimeHelper.GetWeekdaysDate(startDate, currentDate);
            if (!daysInBetween.Contains(currentDate))
                return false;
            else
                return true;
        }
    }

    public class ConcreteTimeDefinition : TimeIntervalDefinitionAstNode
    {
        public DateTime ExecuteTime { get; set; }
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            ExecuteTime = (DateTime) treeNode.Token.Value;
        }

        public override bool CanExecute(DateTime startDate, DateTime currentDate)
        {
            return (ExecuteTime == currentDate);
        }

    }

    public class PeriodicTimeDefinition : TimeIntervalDefinitionAstNode
    {
        public object Value { get; set; }
        public PeriodicType PeriodicType { get; set; }
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Value = treeNode.FirstChild.Token.Value;
            string periodType = treeNode.ChildNodes[1].FindTokenAndGetText().ToLower();
            switch (periodType)
            {
                case "month":
                case "months": PeriodicType = PeriodicType.Month; break;
                case "days" :
                case "day": PeriodicType = PeriodicType.Day; break;
                default:
                    throw new ArgumentException("Unregconized period type:" + periodType);
            }
        }

        public override bool CanExecute(DateTime startDate, DateTime currentDate)
        {
            int length;

            if (int.TryParse((string)Value, out length))
            {

                var daysInBetween = DateTimeHelper.GetWeekdaysDate(startDate, currentDate);
                if (!daysInBetween.Contains(currentDate))
                    return false;

                switch (PeriodicType)
                {
                    case PeriodicType.Month:
                        // execute exclude saturaday & sunday
                        for (DateTime tempDate = startDate; tempDate <= currentDate; tempDate = tempDate.AddMonths(length))
                        {
                            if (currentDate == tempDate)
                                return true;
                        }
                        return false;
                    case PeriodicType.Day:
                        for (DateTime tempDate = startDate; tempDate <= currentDate; tempDate = tempDate.AddDays(length))
                        {
                            if (currentDate == tempDate)
                                return true;
                        }
                        return false;
                    default:
                        throw new Exception("Unregistered period type: " + PeriodicType.ToString());
                }
            }
            else
            {
                throw new Exception("Invalid period value: " + Value.ToString());
            }
            
        }
    }

    public class WeekDayTimeDefinition : TimeIntervalDefinitionAstNode
    {
        public Weekday Weekday { get; set; }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Weekday = treeNode.FirstChild.AstNode as Weekday;
           
        }

        public override bool CanExecute(DateTime startDate, DateTime currentDate)
        {
            var daysInBetween = DateTimeHelper.GetWeekdaysDate(startDate, currentDate);
            if (!daysInBetween.Contains(currentDate))
                return false;

            return (currentDate.DayOfWeek == Weekday.DayOfWeek);

        }
    }

    

    public enum PeriodicType
    {
        Month,
        Day
    }

}
