using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class PropertyElementCollectionNode : FXStrategy.LanguageParsing.AstNodes.CollectionAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _property = treeNode.ChildNodes[0].FindTokenAndGetText();
            _collection = AddChild("collection",treeNode.ChildNodes[1]) as CollectionAstNode;
        }

        private CollectionAstNode _collection;

        public CollectionAstNode Collection
        {
            get { return _collection; }
        }


        private string _property;
        public string Property
        {
            get { return _property; }
        }

    }
}
