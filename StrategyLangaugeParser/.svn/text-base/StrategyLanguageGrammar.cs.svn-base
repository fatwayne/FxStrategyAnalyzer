using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Parsing;
using FXStrategy.LanguageParsing.AstNodes;

namespace FXStrategy.LanguageParsing
{
    /// <summary>
    /// Defines grammar of FXSL concrete syntax
    /// </summary>
    public class StrategyLanguageGrammar : Irony.Parsing.Grammar
    {
        public StrategyLanguageGrammar() : base(false) // the grammar is non-case-senesitive
        {
            // Declaration of NonTerminal nodes

            // Declare non-terminal nodes for potfolio structure
            var globalVarDefinition = new NonTerminal("globalVarDefinition");
            var globalIdentifierDeclarations = new NonTerminal("globalIdentifierDeclarations");
            var globalIdentifierDeclaration = new NonTerminal("globalIdentifierDeclaration", typeof(GlobalIdentifierAstNode));
            var definePortfolio = new NonTerminal("definePortfolio");
            var portfolioSettings = new NonTerminal("portfolioSettings");
            var portfolioSetting = new NonTerminal("portfolioSetting");
            var definePositionSet = new NonTerminal("definePositionSet", typeof(DefinePositionSetAstNode));
            var positionType = new NonTerminal("positionType");
            var strategy = new NonTerminal("strategy", typeof(TradingStrategyAstNode));
            var setStatement = new NonTerminal("setStatement", typeof(StrategyParameterAstNode));
            var tradingRules = new NonTerminal("ruleDefList", typeof(RuleStatementsNode));
            var tradingRule = new NonTerminal("ruleDef");
            var generalRule = new NonTerminal("generalRule", typeof(GeneralRuleAstNode));
            var conditionalRule = new NonTerminal("conditionalRule", typeof(ConditionalRuleAstNode));
            var ruleType = new NonTerminal("ruleTypes");
            var timeDef = new NonTerminal("timeDef");
            var periodInterval = new NonTerminal("periodInterval", typeof(PeriodicTimeDefinitionAstNode));
            var weekDayTimeDef = new NonTerminal("weekDayTimeDef", typeof(WeekDayTimeDefinitionAstNode));
            var weekDay = new NonTerminal("weekDay", typeof(WeekdayAstNode));
            
            // declare non-terminal nodes for statements
            var statements = new NonTerminal("statements", typeof(StatementListNode));
            var statement = new NonTerminal("statment");
            var positionOperation = new NonTerminal("portfolioOperations");
            var openPosition = new NonTerminal("openPosition", typeof(OpenPositionAstNode));
            var closePosition = new NonTerminal("closePosition", typeof(ClosePositionAstNode));
            var ifStatement = new NonTerminal("ifStatement", typeof(IfStatementAstNode));
            var forAllStatement = new NonTerminal("forAllStatement",typeof(ForAllStatementAstNode));
            var collection = new NonTerminal("collection");
            var currencyPredifinedSource = new NonTerminal("currencyPredifinedSource", typeof(PredefinedSetAstNode));
            var propertyElementExpr = new NonTerminal("propertyElementExpr", typeof(PropertyElementCollectionNode));
            var assignment = new NonTerminal("assignment", typeof(AssignmentExprAstNode));
            var declaration = new NonTerminal("declaration", typeof(DeclarationAstNode));
            var type = new NonTerminal("types");
            var tradeType = new NonTerminal("tradeType");
            var positionSet = new NonTerminal("positionSet", typeof(PositionSetAstNode));
           
            // Declaration of non-terminal nodes
            var expression = new NonTerminal("expression");
            var opExpr = new NonTerminal("opExpr", typeof(OperationExprAstNode));
            var op = new NonTerminal("op");
            var propertyAccess = new NonTerminal("propertyAccess", typeof(PropertyAccessAstNode));
            var exchageRateAccessor = new NonTerminal("exchageRateAccessor", typeof(ExchangeRateAccessorAstNode));
            var elementInSetExpr = new NonTerminal("elementInSetExpr", typeof(ElementIsInSetNode));
            var optionalNot = new NonTerminal("optionalNot");
            var movingAVG = new NonTerminal("movingAVG", typeof(SimpleMovingAverageAstNode));
            var minMaxExRate = new NonTerminal("minMaxExRate");
            var minMax = new NonTerminal("minMax");
            var atTime = new NonTerminal("atTime", typeof(AtTimeAstNode));

            // define all the terminals
            var variable = new NonTerminal("variable", typeof(VariableAstNode));
            var date = new NonTerminal("date", typeof(DateAstNode));

            var variableTerminal = new IdentifierTerminal("variableTerminal" );
            var integer = new NumberLiteral("integer", NumberOptions.IntOnly | NumberOptions.NoDotAfterInt, typeof(IntegerAstNode));
            var decimalNum = new NumberLiteral("decimalNum", NumberOptions.None, typeof(DecimalAstNode));
            var currency = new IdentifierTerminal("currency");
            var ruleName = new IdentifierTerminal("ruleName");
            var dateLiteral = new QuotedValueLiteral("dateLiteral", "#", TypeCode.DateTime);
            var portfolioVariable = new IdentifierTerminal("portfolioVariable");
            var propertyName = new IdentifierTerminal("propertyName");
            var stringLit = new StringLiteral("string", "'", StringOptions.AllowsAllEscapes | StringOptions.None, typeof(StringAstNode));
            var methodName = new IdentifierTerminal("methodName");

            // rule definition in bnf

            // rule for portfolio and strategy structure
            globalVarDefinition.Rule = ToTerm("global") + ToTerm("identifiers") + "{" + globalIdentifierDeclarations + "}";
            globalIdentifierDeclarations.Rule = MakeStarRule(globalIdentifierDeclarations, null, globalIdentifierDeclaration);
            globalIdentifierDeclaration.Rule = ToTerm("define") + variableTerminal + ToTerm("as") + expression + ToTerm(";");
            definePortfolio.Rule = ToTerm("define") + ToTerm("Portfolio")
                                    + ToTerm("{") + portfolioSettings + ToTerm("}");
            portfolioSettings.Rule = MakeStarRule(portfolioSettings, null, portfolioSetting);
            portfolioSetting.Rule = definePositionSet | setStatement;
            definePositionSet.Rule = portfolioVariable + ToTerm(":") + ToTerm("Position")
                                            + "<" + positionType + ">" + "[" + expression + "]" + ";";
            positionType.Rule = ToTerm("Long") | ToTerm("Short");
            strategy.Rule = globalVarDefinition + definePortfolio + tradingRules;
            setStatement.Rule = ToTerm("set") + variableTerminal + ToTerm("to") + expression + ToTerm(";");
            tradingRules.Rule = MakeStarRule(tradingRules, tradingRule);
            tradingRule.Rule = generalRule | conditionalRule;
            generalRule.Rule = ToTerm("rule") + ruleName + ToTerm("executes") + ToTerm("on")
                + timeDef + ToTerm("{") + statements + ToTerm("}");
            conditionalRule.Rule = ToTerm("stop-loss") + ToTerm("rule") + ruleName + ToTerm("executes") 
                                 +ToTerm("on") + timeDef + ToTerm("{") + "for" + positionSet +
                                 "when" + opExpr + "where" +  statements + ToTerm("}");
            ruleType.Rule = ToTerm("stop-loss") | ToTerm("stop-loss re-entry") | ToTerm("take-profit") | ToTerm("take-profit re-entry");
            timeDef.Rule = ToTerm("every") + periodInterval | ToTerm("every") + weekDayTimeDef;
            periodInterval.Rule = expression + ToTerm("Month") | expression + ToTerm("Day") | expression + ToTerm("Months") | expression + ToTerm("Days") | ToTerm("day");
            weekDayTimeDef.Rule = weekDay;
            weekDay.Rule = ToTerm("monday") | "tuesday" | "wednesday" | "thursday" | "friday";

            // rule for statements
            statements.Rule = MakeStarRule(statements, statement);
            statement.Rule = ifStatement | forAllStatement | positionOperation | assignment| declaration;
            forAllStatement.Rule = ToTerm("for") + ToTerm("all") + variable + ToTerm("in")
                + collection + ToTerm("{") + statements + ToTerm("}");
            collection.Rule = propertyElementExpr | currencyPredifinedSource | positionSet  ;
            assignment.Rule = variable + "=" + expression + ";";
            declaration.Rule = type + variable + "=" + expression + ";";
            positionOperation.Rule = closePosition | openPosition;
            openPosition.Rule = ToTerm("Open") + positionSet + "with" + expression + ";";
            closePosition.Rule = ToTerm("Close") + expression + ";";
            positionSet.Rule = ToTerm("Portfolio") + "." + portfolioVariable;
            propertyElementExpr.Rule = propertyName + ToTerm("of") + collection;
            type.Rule = ToTerm("string") | ToTerm("int") | ToTerm("decimal");
            ifStatement.Rule = ToTerm("if") + expression + ToTerm(":") + statements;


            // rule for expressions
            expression.Rule = date | exchageRateAccessor | variable | movingAVG | opExpr | elementInSetExpr |
                                periodInterval | stringLit | integer |
                                propertyAccess | decimalNum | atTime  ;
            elementInSetExpr.Rule = expression + "is" + optionalNot + "in" + collection;
            optionalNot.Rule = ToTerm("not") | "";
            atTime.Rule = exchageRateAccessor + "at" + expression;
            movingAVG.Rule = periodInterval + ToTerm("SMA") + ToTerm("of") + exchageRateAccessor + "at" + expression;
            minMaxExRate.Rule = minMax + "of" + exchageRateAccessor + "at" + expression;
            minMax.Rule = ToTerm("minima") | "maxima";
            exchageRateAccessor.Rule = ToTerm("[") + expression + "/" + expression + "]";
            currencyPredifinedSource.Rule = ToTerm("Top3Currencies") | ToTerm("Bottom3Currencies");
            propertyAccess.Rule = variable + "." + propertyName;
            opExpr.Rule = expression + op + expression;
            op.Rule = ToTerm("and") | ToTerm("or") | "<" | ">" | ">=" | "<=" | ToTerm("==") | ToTerm("+") | "-" | "*" | "/";
          
           
            date.Rule = dateLiteral;
            variable.Rule = variableTerminal;

            // defines the symbols that can be skipped in a parse tree
            MarkPunctuation("set", "to", "define", "portfolio", "{", "}", ";", ":", "Position","[","]", 
                "every","rule","executes","loop","through","on","is", "in","if","for","all", "with",".","of",
                "close", "open", "(", ")","as", "global", "variables", "=", "where", "when", "stop-loss", "SMA", "at");

            // indicates the NonTerminal that is not necessary to be a node in a parse tree
            MarkTransient(definePortfolio, portfolioSettings, timeDef, expression,
                collection, statement, positionOperation,globalVarDefinition, globalIdentifierDeclarations
                , tradingRules, tradingRule);
            this.RegisterOperators(1, "+", "-");
            this.RegisterOperators(2, "*", "/");

            this.Root = strategy;

            // Create AST node
            this.LanguageFlags = LanguageFlags.CreateAst;
        }
    }
}
