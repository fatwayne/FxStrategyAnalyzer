using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Parsing;
using Irony.Interpreter;

namespace Irony.Ast.PrimitiveNodes
{
    public class CollectionNode : AstNode
    {
        public Symbol Symbol;

        public override void Init(ParsingContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Symbol = treeNode.Token.Symbol;
            AsString = Symbol.Text;
        }

        public IEnumerable<object> GetCollectionData(EvaluationContext context, DateTime dateTime)
        {
            return context.StrategyDataContainer.GetData(Symbol.Text, dateTime);
        }
    }
}
