using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;


namespace Irony.Ast
{
    public class DefinePositionSetAstNode : AstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Name = treeNode.FirstChild.FindTokenAndGetText();
            string positionType = treeNode.ChildNodes[1].FindTokenAndGetText();
            if (positionType.ToLower() == "long")
                PositionType = PositionType.Long;
            else if (positionType.ToLower() == "short")
                PositionType = PositionType.Short;
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
