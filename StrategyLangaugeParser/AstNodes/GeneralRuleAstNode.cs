using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.LanguageParsing.AstNodes;

namespace FXStrategy.LanguageParsing.AstNodes
{
    /// <summary>
    /// Represents a general trading rule on abstract syntax tree
    /// </summary>
    public class GeneralRuleAstNode : Irony.Ast.AstNode
    {
        public StatementListNode Statements { get; set; }
        public string Name { get; set; }
        public ExecutonFrequencyAstNode ExecuteFrequency { get; set; }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Name = treeNode.ChildNodes[0].FindTokenAndGetText();
           
            ExecuteFrequency = AddChild("executeFrequency", treeNode.ChildNodes[1]) as ExecutonFrequencyAstNode;
            Statements = (StatementListNode)AddChild("statements", treeNode.ChildNodes[2]);
        }        
    }
}
