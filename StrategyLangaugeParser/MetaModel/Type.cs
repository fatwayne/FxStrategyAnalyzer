using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;

namespace StrategyLanguageParser.MetaModel
{
    public abstract class Type : AstNode
    {
        
        public abstract string Name{get;}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(typeof(object)!=typeof(Type))
                return false;
            return this.Name == ((Type) obj).Name;
        }

        public static Integer Integer
        {
            get
            {
                return new Integer();
            }
        }

        public static Decimal Decimal
        {
            get
            {
                return new Decimal();
            }
        }

        public static Bool Bool
        {
            get
            {
                return new Bool();
            }
        }

    }
   
}
