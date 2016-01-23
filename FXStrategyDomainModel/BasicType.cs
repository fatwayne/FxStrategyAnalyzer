﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategyMetaModel
{
    public abstract class BasicType : Type
    {
    }

    public class Decimal
    {
        public override string Name
        {
            get { return "Decimal"; }
        }
   
    }

    public class Integer : BasicType
    {
        public override string Name
        {
            get { return "Integer"; }
        }
    }

    public class Bool : BasicType
    {
        public override string Name
        {
            get { return "Bool"; }
        }
    }
}
