using System;
using System.Linq;
using FXStrategy.MetaModel;
using FXStrategy.LanguageParsing.AstNodes;
using System.Collections.Generic;
using FXStrategy.MetaModel.DataType;

namespace FXStrategy.ASTToModelTransform
{
   /// <summary>
   /// Responsible for transform a abstract syntax tree to strategy language model
   /// </summary>
    public class AstTreeToModelTransformer 
    {
        /// <summary>
        /// Dictionary contains existing variables during transformation,
        /// it is used for cross-referencing of variable
        /// </summary>
        private Dictionary<string, Variable> _variableDict;

        public AstTreeToModelTransformer()
        {
            _variableDict = new Dictionary<string, Variable>();
        }

        /// <summary>
        /// Start transform an abstract syntax tree into the model
        /// </summary>
        /// <param name="tradingStrategyAstNode">the node on abstract syntax represents trading strategy</param>
        /// <returns>model of a trading strategy</returns>
        public TradingStrategy Transform(TradingStrategyAstNode tradingStrategyAstNode)
        {
            if (tradingStrategyAstNode == null)
                throw new ArgumentException("tradingStrategyAstNode cannot be null.");

            TradingStrategy tradingStrategyModel = new TradingStrategy();

            AddGlobalIdentifierToModel(tradingStrategyAstNode, tradingStrategyModel);

            AddPositionSetDefToModel(tradingStrategyAstNode, tradingStrategyModel);

            AddPortfolioParamToModel(tradingStrategyAstNode, tradingStrategyModel);

            AddTradingRuleToModel(tradingStrategyAstNode, tradingStrategyModel);

            return tradingStrategyModel;
        }

        /// <summary>
        /// Handles global identifier
        /// </summary>
        /// <param name="tradingStrategyAstNode"></param>
        /// <param name="tradingStrategyModel"></param>
        private void AddGlobalIdentifierToModel(TradingStrategyAstNode tradingStrategyAstNode, TradingStrategy tradingStrategyModel)
        {
            foreach (var globalVariable in tradingStrategyAstNode.GlobalIdentifierDef)
            {
                var exprAst = globalVariable.ExpressionAstNode;
                tradingStrategyModel.ConstantVariableDefinitions.Add(
                    new GlobalIdentifier()
                    {
                        Variable = CreateVariable(globalVariable.VariableName, exprAst.GetType()),
                        Constant = ConvertExpressionAst(exprAst)
                    }
                    );
            }
        }

        /// <summary>
        /// Create a trading rule from AST to the model
        /// </summary>
        /// <param name="tradingStrategyAstNode"></param>
        /// <param name="tradingStrategyModel"></param>
        private void AddTradingRuleToModel(TradingStrategyAstNode tradingStrategyAstNode, TradingStrategy tradingStrategyModel)
        {
            foreach (var tradingRuleAst in tradingStrategyAstNode.TradingRules)
            {
                if (tradingRuleAst is ConditionalRuleAstNode)
                {
                    var stopLossAstRule = tradingRuleAst as ConditionalRuleAstNode;
                    tradingStrategyModel.TradingRules.Add(new StopLossRule(
                        stopLossAstRule.Name,
                        ConvertExecutionFrequency(stopLossAstRule.ExecuteFrequency),
                        new Variable("position"),
                        ConvertStatementAst(stopLossAstRule.Statements),
                        (BooleanExpression)ConvertExpressionAst(stopLossAstRule.Condition),
                        CreateVariable(stopLossAstRule.PositionSet.PositionSetName)));
                }
                else if (tradingRuleAst is GeneralRuleAstNode)
                {
                    tradingStrategyModel.TradingRules.Add(new TradingRule(
                        tradingRuleAst.Name,
                        ConvertExecutionFrequency(tradingRuleAst.ExecuteFrequency),
                        ConvertStatementAst(tradingRuleAst.Statements))
                        );
                }
               
            }
        }

        /// <summary>
        /// Handles transformation of position set
        /// </summary>
        /// <param name="tradingStrategyAstNode"></param>
        /// <param name="tradingStrategyModel"></param>
        private void AddPositionSetDefToModel(TradingStrategyAstNode tradingStrategyAstNode, TradingStrategy tradingStrategyModel)
        {
            foreach (var positionSetDef in tradingStrategyAstNode.PositionSets)
            {
                CreateVariable(positionSetDef.Name, typeof(PositionSet));
                tradingStrategyModel.Portfolio.PositionSets.Add(new PositionSet()
                {
                    Name = positionSetDef.Name,
                 Number = ConvertExpressionAst(positionSetDef.Number),
                    PositionType = ConvertPositionTypeAst(positionSetDef.PositionType)
                });
            }
        }

        /// <summary>
        /// Create or retrieve a variable from variable table
        /// with no type specified
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Variable CreateVariable(string name)
        {
            return CreateVariable(name, null);
        }

        /// <summary>
        /// Create or retrieve a variable from the variable table
        /// </summary>
        /// <param name="name">name of the variable</param>
        /// <param name="type">type of the variable</param>
        /// <returns></returns>
        private Variable CreateVariable(string name, Type type)
        {
            Variable result;
            if (!_variableDict.TryGetValue(name, out result))
            {
                if (type != null)
                    _variableDict[name] = new Variable(name, type);
                else
                    _variableDict[name] = new Variable(name);
            }

            return _variableDict[name];
        }


        /// <summary>
        /// Transform execute frequency from rule ast node to model
        /// </summary>
        /// <param name="tradingRuleAst"></param>
        /// <returns></returns>
        private TimeIntervalDefinition ConvertExecutionFrequency(ExecutonFrequencyAstNode executeFrequency)
        {
            if (executeFrequency is ConcreteTimeDefinitionAstNode)
            {
                return new ConcreteTimeDefinition()
                {
                    ExecuteTime = ((ConcreteTimeDefinitionAstNode)executeFrequency).ExecuteTime
                };
            }
            else if (executeFrequency is PeriodicTimeDefinitionAstNode)
            {
                PeriodicTimeDefinitionAstNode periodicTimeDefAst = executeFrequency as PeriodicTimeDefinitionAstNode;
                return new PeriodicTimeDefinition(
                    ConvertExpressionAst(periodicTimeDefAst.Value),
                    PeriodicTypeConvert(periodicTimeDefAst.PeriodicType)
                    );
            }
            else if (executeFrequency is WeekDayTimeDefinitionAstNode)
            {
                return new WeekDayTimeDefinition()
                {
                    DayOfWeek = ((WeekDayTimeDefinitionAstNode)executeFrequency).Weekday.DayOfWeek
                };
            }
            else
                throw new TypeAccessException("The type of ExecuteFrequency is not handled. Type of ExecuteFrequency: " + executeFrequency.GetType());
        }

        /// <summary>
        /// Create Statement node from AST to the model
        /// </summary>
        /// <param name="astStatement">The statement node in AST</param>
        /// <returns>Statement node from the meta model</returns>
        private Statement ConvertStatementAst(StatementAstNode astStatement)
        {
            if (astStatement == null)
                return null;

            if (astStatement is StatementListNode)
            {
                var statmentListNode = astStatement as StatementListNode;
                return new CompositeStatement(
                    statmentListNode.StatementList.Select(s => ConvertStatementAst(s)).ToList()
                    );
            }
            else if (astStatement is ForAllStatementAstNode)
            {
                var forAllAstNode = astStatement as ForAllStatementAstNode;
                return new ForAllStatement(
                    CreateVariable(forAllAstNode.Iterator.Name),
                    ConvertExpressionAst(forAllAstNode.Set),
                    ConvertStatementAst(forAllAstNode.StatementList));
            }
            else if (astStatement is IfStatementAstNode)
            {
                var ifAstNode = astStatement as IfStatementAstNode;
                return new IfStatement(
                    ConvertExpressionAst(ifAstNode.Condition),
                    ConvertStatementAst(ifAstNode.IfTrueStatments),
                    ConvertStatementAst(ifAstNode.IfFalseStatements));
            }
            else if (astStatement is OpenPositionAstNode)
            {
                var openOpAst = astStatement as OpenPositionAstNode;
                return new OpenPosition(
                    CreateVariable(openOpAst.PositionSetAstNode.PositionSetName),
                    ConvertExpressionAst(openOpAst.ExpressionAstNode));
            }
            else if (astStatement is ClosePositionAstNode)
            {
                var closeOpAst = astStatement as ClosePositionAstNode;
                return new ClosePosition(
                    ConvertExpressionAst(closeOpAst.Variable)
                    );
            }
            else if (astStatement is AssignmentExprAstNode)
            {
                var assignmentAst = astStatement as AssignmentExprAstNode;
                return new Assignment(
                    CreateVariable(assignmentAst.Variable.Name),
                    ConvertExpressionAst(assignmentAst.Expression));
            }
            else if (astStatement is DeclarationAstNode)
            {
                var declarationAst = astStatement as DeclarationAstNode;
                return new Assignment(
                    CreateVariable(declarationAst.Variable.Name, declarationAst.Type),
                    ConvertExpressionAst(declarationAst.Initialization));
            }
           
            else
            {
                throw new Exception("The derived type of TradingRuleAstNode is not handled. Type name: " + astStatement.GetType().ToString());
            }
        }

        /// <summary>
        /// Create Expression node from the expression node on abstract syntax tree
        /// </summary>
        /// <param name="astExpr"></param>
        /// <returns></returns>
        private Expression ConvertExpressionAst(ExpressionAstNode astExpr)
        {
            if (astExpr is IntegerAstNode)
            {
                IntegerAstNode intAst = astExpr as IntegerAstNode;
                return new Constant(typeof(int), intAst.Value);
            }
            else if (astExpr is StringAstNode)
            {
                StringAstNode stringAst = astExpr as StringAstNode;
                return new Constant(typeof(string), stringAst.Value);
            }
            else if (astExpr is DecimalAstNode)
            {
                DecimalAstNode decimalAst = astExpr as DecimalAstNode;
                return new Constant(typeof(decimal), decimalAst.Value);
            }
            else if (astExpr is PropertyAccessAstNode)
            {
                PropertyAccessAstNode propertyAccessAst = astExpr as PropertyAccessAstNode;
                return new PropertyAccessor(
                    CreateVariable(propertyAccessAst.Variable.Name),
                    propertyAccessAst.Property);
            }
            else if (astExpr is ElementIsInSetNode)
            {
                ElementIsInSetNode elementInSetAst = astExpr as ElementIsInSetNode;

                if (elementInSetAst.IsIn)
                {
                    return new IsInSet()
                    {
                        LeftExpression = ConvertExpressionAst(elementInSetAst.Element),
                        RightExpression = ConvertExpressionAst(elementInSetAst.Set)
                    };
                }
                else
                {
                    return new IsNotInSet()
                    {
                        LeftExpression = ConvertExpressionAst(elementInSetAst.Element),
                        RightExpression = ConvertExpressionAst(elementInSetAst.Set)
                    };
                }
            }
            else if (astExpr is VariableAstNode)
            {
                VariableAstNode variable = astExpr as VariableAstNode;
                return CreateVariable(variable.Name);
            }
            else if (astExpr is PropertyElementCollectionNode)
            {
                PropertyElementCollectionNode pecNode = astExpr as PropertyElementCollectionNode;
                return new CollectionPropertiesAccessor(
                    ConvertExpressionAst(pecNode.Collection),
                    pecNode.Property);
            }
            else if (astExpr is PositionSetAstNode)
            {
                PositionSetAstNode postionSetAstNode = astExpr as PositionSetAstNode;
                return CreateVariable(postionSetAstNode.PositionSetName, typeof(PositionSet));
            }
            else if (astExpr is PredefinedSetAstNode)
            {
                PredefinedSetAstNode predefinedSetAstNode = astExpr as PredefinedSetAstNode;
                return CreateVariable(predefinedSetAstNode.SetName, typeof(PredefinedDataSet));
            }
            else if (astExpr is OperationExprAstNode)
            {
                OperationExprAstNode opAst = astExpr as OperationExprAstNode;

                Expression leftExpression = ConvertExpressionAst(opAst.LeftExpression);
                Expression rightExpression = ConvertExpressionAst(opAst.RightExpression);

                switch (opAst.Op.ToLower())
                {
                    case "+": return new Sum { LeftExpression = leftExpression, RightExpression = rightExpression }; 
                    case "-": return new Subtract { LeftExpression = leftExpression, RightExpression = rightExpression }; 
                    case "*": return new Multiply { LeftExpression = leftExpression, RightExpression = rightExpression }; 
                    case "/": return new Divide { LeftExpression = leftExpression, RightExpression = rightExpression };
                    case ">": return new GreaterThan{LeftExpression = leftExpression, RightExpression = rightExpression };
                    case "<": return new LessThan{LeftExpression = leftExpression, RightExpression = rightExpression };
                    case ">=": return new GreaterThanOrEq{LeftExpression = leftExpression, RightExpression = rightExpression };
                    case "<=": return new LessThanOrEq { LeftExpression = leftExpression, RightExpression = rightExpression };
                    case "and": return new And { LeftExpression = leftExpression, RightExpression = rightExpression };
                    case "or": return new Or { LeftExpression = leftExpression, RightExpression = rightExpression };
                    default:
                        throw new Exception("Unknown operator" + opAst.Op);

                }
            }
            else if (astExpr is SimpleMovingAverageAstNode)
            {
                var movAstExpr = astExpr as SimpleMovingAverageAstNode;

                return new MovingAverage(
                    (ExchangeRateAccessor) ConvertExpressionAst(movAstExpr.ExchangeRateAccessor),
                    (PeriodicTimeDefinition) ConvertExecutionFrequency(movAstExpr.LengthOfPeriod),
                    ConvertExpressionAst(movAstExpr.DateExpr));
            }
            else if (astExpr is AtTimeAstNode)
            {
                var atTimeAst = astExpr as AtTimeAstNode;
                return new AtTime(
                     (ExchangeRateAccessor)ConvertExpressionAst(atTimeAst.ExchangeRateAccessor),
                    ConvertExpressionAst(atTimeAst.DateExpr));
            }
            else if (astExpr is ExchangeRateAccessorAstNode)
            {
                var exRateAccessor = astExpr as ExchangeRateAccessorAstNode;
                return new ExchangeRateAccessor(
                    ConvertExpressionAst(exRateAccessor.BaseCurrencyExpr),
                    ConvertExpressionAst(exRateAccessor.VariableCurrencyExpr));
            }
            else if (astExpr is DateAstNode)
            {
                var dateExpr = astExpr as DateAstNode;

                return new Constant(typeof(DateTime), dateExpr.Value);
            }
            else
                throw new Exception("The derived type of ExpressionAstNode is not handled. Type name: " + astExpr.GetType().ToString());
        }



        /// <summary>
        /// Convert periodic type in ast node to the type in model
        /// </summary>
        /// <param name="periodicTypeAst"></param>
        /// <returns></returns>
        private PeriodicType PeriodicTypeConvert(PeriodicTypeAst periodicTypeAst)
        {
            switch (periodicTypeAst)
            {
                case  PeriodicTypeAst.Day:
                    return PeriodicType.Day;
                case PeriodicTypeAst.Week:
                    return PeriodicType.Week;
                case PeriodicTypeAst.Month:
                    return PeriodicType.Month;
                default:
                    throw new ArgumentException("Unexpected periodic type from AST node: " + periodicTypeAst.ToString());
            }
        }

        /// <summary>
        /// handles position type
        /// </summary>
        /// <param name="positionTypeAst"></param>
        /// <returns></returns>
        private PositionType ConvertPositionTypeAst(PositionTypeAst positionTypeAst)
        {
            switch (positionTypeAst)
            {
                case PositionTypeAst.Long:
                    return PositionType.Long;
                case PositionTypeAst.Short:
                    return PositionType.Short;
                default:
                    throw new ArgumentException("Unexpected position type from AST node: " + positionTypeAst.ToString());
            }
        }

        /// <summary>
        /// Handles parameters in the trading strategy AST node
        /// </summary>
        /// <param name="tradingStrategyAstNode"></param>
        /// <param name="tradingStrategyModel"></param>
        private void AddPortfolioParamToModel(TradingStrategyAstNode tradingStrategyAstNode, TradingStrategy tradingStrategyModel)
        {

            foreach (var parameter in tradingStrategyAstNode.StrategyParameters)
            {
                Type type = null;
                object value = null;
                string parameterName = null;

                if (parameter.Value is StringAstNode)
                {
                    var strAst = parameter.Value as StringAstNode;
                    type = typeof(string);
                    value = strAst.Value;
                    parameterName = parameter.Name;
                }

                if (parameterName != null)
                {
                    if (parameterName.ToLower() == "homecurrency")
                        tradingStrategyModel.Portfolio.HomeCurrency = (string)value;
                    else
                    {
                        throw new Exception("Unknown strategy parameter: " + parameterName);
                    }
                }
            }
        }
    }
}

