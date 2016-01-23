using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;


namespace StrategyLanguageParser.MetaModel
{
    public class PositionSet : AstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Name = treeNode.FirstChild.FindTokenAndGetText();
            string positionType = treeNode.ChildNodes[1].FindTokenAndGetText();
            if (positionType.ToLower() == "long")
                PositionType = MetaModel.PositionType.Long;
            else if (positionType.ToLower() == "short")
                PositionType = MetaModel.PositionType.Short;
            string numberStr = treeNode.ChildNodes[2].FindTokenAndGetText();
            int numberInt;
            if (!Int32.TryParse(numberStr, out numberInt))
            {
                throw new Exception("A position number can only be an integer.");
            }
            Number = numberInt;
        }

        public string Name { get; set; }

        public int Number { get; set; }

        public PositionType PositionType { get; set; }
    }
}
