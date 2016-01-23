using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class ElementIsInSetNode : ExpressionAstNode
    {
        private ExpressionAstNode _element;

        public ExpressionAstNode Element
        {
            get { return _element; }
        }

        private CollectionAstNode _set;

        public CollectionAstNode Set
        {
            get { return _set; }
        }
        private bool _isIn;

        public bool IsIn
        {
            get { return _isIn; }
        }
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            if (treeNode.ChildNodes[1].FindTokenAndGetText() == "not")
            {
                _isIn = false;
            }
            else
                _isIn = true;

            _element = (ExpressionAstNode)AddChild("element", treeNode.ChildNodes[0]);
            _set = (CollectionAstNode) AddChild("set", treeNode.ChildNodes[2]);

        }

        public override Type GetType()
        {
            return typeof(bool);
        }
    }
}
