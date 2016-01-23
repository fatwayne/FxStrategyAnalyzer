using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class ForAllStatementAstNode : StatementAstNode
    {
        public VariableAstNode Iterator;
        public CollectionAstNode Set;
        public StatementListNode StatementList;
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Iterator = (VariableAstNode)AddChild("iterator", treeNode.ChildNodes[0]);

            Set = (CollectionAstNode)AddChild("set", treeNode.ChildNodes[1]);
            StatementList = (StatementListNode)AddChild("statementList", treeNode.ChildNodes[2]);
        }
    }
}
