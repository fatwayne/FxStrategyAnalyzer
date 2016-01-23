using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.LanguageParsing.AstNodes;


namespace FXStrategy.LanguageParsing.AstNodes
{
    public class DefinePositionSetAstNode : Irony.Ast.AstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Name = treeNode.FirstChild.FindTokenAndGetText();
            string positionType = treeNode.ChildNodes[2].FindTokenAndGetText();
            if (positionType.ToLower() == "long")
                PositionType = PositionTypeAst.Long;
            else if (positionType.ToLower() == "short")
                PositionType = PositionTypeAst.Short;
            _number = (ExpressionAstNode) AddChild("number",treeNode.ChildNodes[4]);
            //int numberInt;
            //if (!Int32.TryParse(numberStr, out numberInt))
            //{
            //    throw new Exception("A position number can only be an integer.");
            //}
        }

        public string Name { get; set; }

        private ExpressionAstNode _number;
        public ExpressionAstNode Number
        {
            get { return _number; }
        }
        

        public PositionTypeAst PositionType { get; set; }
    }
}
