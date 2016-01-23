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

namespace FXStrategy.LanguageParsing.AstNodes
{

  //A node representing expression list - for example, list of argument expressions in function call
    public class ExpressionListNode : Irony.Ast.AstNode
    {
     
    public override void Init(ParsingContext context, ParseTreeNode treeNode) {
      base.Init(context, treeNode);
      foreach (var child in treeNode.ChildNodes) {
          AddChild("expr", child); 
      }
      AsString = "Expression list";
    }

    //public override void EvaluateNode(EvaluationContext context, Irony.Ast.AstMode mode, DateTime time) {
    //  var result = new ValuesList();
    //  foreach (var expr in ChildNodes) {
    //      expr.Evaluate(context, Irony.Ast.AstMode.Read, time);
    //    result.Add(context.Data.Pop());
    //  }
    //  //Push list on the stack
    //  context.Data.Push(result); 
    //}

  }//class

}//namespace
