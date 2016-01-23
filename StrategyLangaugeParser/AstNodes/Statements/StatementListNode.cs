#region License
/* **********************************************************************************
 * Copyright (c) Roman Ivantsov
 * This source code is subject to terms and conditions of the MIT License
 * for Irony. A copy of the license can be found in the License.txt file
 * at the root of this distribution. 
 * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
 * MIT License.
 * You must not remove this notice from this software.
 * **********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Interpreter;
using Irony.Parsing;

namespace FXStrategy.LanguageParsing.AstNodes{

  public class StatementListNode : StatementAstNode {

      private List<StatementAstNode> _statementList;

      public List<StatementAstNode> StatementList
      {
          get { return _statementList; }
      }

    public override void Init(ParsingContext context, ParseTreeNode treeNode) {
      base.Init(context, treeNode);
      _statementList = new List<StatementAstNode>();
      foreach (var child in treeNode.ChildNodes) {
        //don't add if it is null; it can happen that "statement" is a comment line and statement's node is null.
        // So to make life easier for language creator, we just skip if it is null
          if (child.AstNode != null)
          {
              AddChild(string.Empty, child);
              _statementList.Add(child.AstNode as StatementAstNode);
          }
      }
      AsString = "Statement List";
    }

    public override void EvaluateNode(EvaluationContext context, Irony.Ast.AstMode mode, DateTime time)
    {
      if (ChildNodes.Count == 0) return;
      ChildNodes[ChildNodes.Count - 1].Flags |= Irony.Ast.AstNodeFlags.IsTail;
      int iniCount = context.Data.Count; 
      foreach(var stmt in ChildNodes) {
          stmt.Evaluate(context, Irony.Ast.AstMode.Read, time);
        //restore position, in case a statement left something (like standalone expression vs assignment) 
        context.Data.PopUntil(iniCount);
      }
      context.Data.Push(context.LastResult); //push it back again
    }
    
  }//class

}//namespace
