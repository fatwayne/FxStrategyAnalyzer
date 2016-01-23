using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.LanguageParsing.AstNodes;

namespace FXStrategy.LanguageParsing.AstNodes
{

    public class WeekdayAstNode : Irony.Ast.AstNode
    {
        public DayOfWeek DayOfWeek { get; set; }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            string weekday =  treeNode.FindTokenAndGetText().ToLower();
            switch (weekday)
            {
                case "monday": DayOfWeek = System.DayOfWeek.Monday; break;
                case "tuesday": DayOfWeek = System.DayOfWeek.Tuesday; break;
                case "wednesday": DayOfWeek = System.DayOfWeek.Wednesday; break;
                case "thursday": DayOfWeek = System.DayOfWeek.Thursday; break;
                case "friday": DayOfWeek = System.DayOfWeek.Friday; break;
                case "saturaday":
                case "sunday": throw new Exception("No operation can be performed on Saturaday or Sunday");
                default:
                    throw new Exception("Unrecognized day:" + weekday);

            }
        }
    }
}
