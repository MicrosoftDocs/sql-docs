---
title: "Syntax (SSIS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "expressions [Integration Services], syntax"
  - "syntax [Integration Services]"
ms.assetid: 61c053c5-1182-4ad0-b804-51cbd19aa0ba
author: janinezhang
ms.author: janinez
manager: craigg
---
# Syntax (SSIS)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expression syntax is similar to the syntax that the C and C# languages use. Expressions include elements such as identifiers (columns and variables), literals, operators, and functions. This topic summarizes the unique requirements of the expression evaluator syntax as they apply to different expression elements.  
  
> [!NOTE]  
>  In previous releases of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], there was a 4000-character limit on the evaluation result of an expression when the result had the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type DT_WSTR or DT_STR. This limit has been removed.  
  
 For sample expressions that use specific operators and functions, see the topic about each operator and function in the topics: [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md) and [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md).  
  
 For sample expressions that use multiple operators and functions as well as identifiers and literals, see [Examples of Advanced Integration Services Expressions](../../integration-services/expressions/examples-of-advanced-integration-services-expressions.md).  
  
 For sample expressions to use in property expressions, see [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md).  
  
## Identifiers  
 Expressions can include column and variable identifiers. The columns can originate in the data source or can be created by transformations in the data flow. Expressions can use lineage identifiers to refer to columns. Lineage identifiers are numbers that uniquely identify package elements. Lineage identifiers, referenced in an expression, must include the pound (#) prefix. For example, the lineage identifier 138 is referenced using #138.  
  
 Expressions can include the system variables that [!INCLUDE[ssIS](../../includes/ssis-md.md)] provides and custom variables. Variables, when referenced in an expression, must include the \@ prefix. For example, the `Counter` variable is referenced using \@Counter. The \@ character is not part of the variable name; it only indicates to the expression evaluator that the identifier is a variable. For more information, see [Identifiers &#40;SSIS&#41;](../../integration-services/expressions/identifiers-ssis.md).  
  
## Literals  
 Expressions can include numeric, string, and Boolean literals. String literals used in expressions must be enclosed in quotation marks. Numeric and Boolean literals do not take quotation marks. The expression language includes escape sequences for characters that are frequently escaped. For more information, see [Literals &#40;SSIS&#41;](../../integration-services/expressions/numeric-string-and-boolean-literals.md).  
  
## Operators  
 The expression evaluator provides a set of operators that provides functionality similar to the set of operators in languages such as Transact-SQL, C++, and C#. However, the expression language includes additional operators and uses different symbols than those you may be familiar with. For more information, see [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md).  
  
### Namespace Resolution Operator  
 Expressions use the namespace resolution operator (::) to disambiguate variables that have the same name. By using the namespace resolution operator, you can qualify the variable with its namespace, which makes it possible to use multiple variables with the same name in a package.  
  
#### Cast Operator  
 The cast operator converts expression results, column values, variable values, and constants from one data type to another. The cast operator provided by the expression language is similar to the one provided by the C and C# languages. In Transact-SQL, the CAST and CONVERT functions provide this functionality. The syntax of the cast operator is different from ones used by CAST and CONVERT in the following ways:  
  
-   It can use an expression as an argument.  
  
-   Its syntax does not include the CAST keyword.  
  
-   Its syntax does not include the AS keyword.  
  
##### Conditional Operator  
 The conditional operator returns one of two expressions, based on the evaluation of a Boolean expression. The conditional operator provided by the expression language is similar to the one provided by the C and C# languages. In multidimensional expressions (MDX), the IIF function provides similar functionality.  
  
###### Logical Operators  
 The expression language supports the ! character for the logical NOT operator. In Transact-SQL, the ! operator is built into the set of relational operators. For example, Transact-SQL provides the > and the !> operators. The [!INCLUDE[ssIS](../../includes/ssis-md.md)] expression language does not support the combination of the ! operator and other operators. For example, it is not valid to combine ! and > into !>. However, the expression language does support a built-in != combination of characters for the not-equal-to comparison.  
  
###### Equality Operators  
 The expression evaluator grammar provides the == equality operator. This operator is the equivalent of the = operator in Transact-SQL and the == operator in C#.  
  
## Functions  
 The expression language includes date and time functions, mathematical functions, and string functions that are similar to Transact-SQL functions and C# methods.  
  
 A few functions have the same names as Transact-SQL functions, but have subtly different functionality in the expression evaluator.  
  
-   In Transact-SQL, the ISNULL function replaces null values with a specified value, whereas the expression evaluator ISNULL function returns a Boolean based on whether an expression is null.  
  
-   In Transact-SQL, the ROUND function includes an option to truncate the result set, whereas the expression evaluator ROUND function does not.  
  
 For more information, see [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md).  
  
## Related Tasks  
 [Use an Expression in a Data Flow Component](https://msdn.microsoft.com/library/9181b998-d24a-41fb-bb3c-14eee34f910d)  
  
## Related Content  
  
-   Technical article, [SSIS Expression Cheat Sheet](https://go.microsoft.com/fwlink/?LinkId=746575), on pragmaticworks.com  
  
-   Technical article, [SSIS Expression Examples](https://go.microsoft.com/fwlink/?LinkId=220761), on social.technet.microsoft.com  
  
  
