using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;

namespace StrategyLanguageParser.MetaModel
{
    public abstract class Expression : AstNode
    {
        public abstract Type Type { get;  }
        public abstract object Eval();
    }
}
