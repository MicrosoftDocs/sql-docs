---
title: "Operators (MDX Syntax) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Operators (MDX Syntax)


  In Multidimensional Expressions (MDX), operators let you perform the following actions:  
  
-   Change data, either permanently or temporarily.  
  
-   Search for values or objects that meet a specified condition.  
  
-   Implement a decision between values or expressions.  
  
-   Test for specific conditions before beginning or committing a transaction, or before executing specific statements.  
  
 MDX supports the operators listed in the following table:  
  
|To perform this type of operation|Use|  
|---------------------------------------|---------|  
|Assigns a value to a variable, or associates a result set column with an alias.|[Assignment Operators](../mdx/assignment-operators.md)|  
|Addition, subtraction, multiplication, division.|[Arithmetic Operators](../mdx/arithmetic-operators.md)|  
|Test for the truth of a condition, such as AND, OR, NOT, and XOR.|[Bitwise Operators](../mdx/bitwise-operators.md)|  
|Compare a value against another value or an expression.|[Comparison Operators](../mdx/comparison-operators.md)|  
|Either permanently or temporarily combine two strings into one string.|[Concatenation Operators](../mdx/concatenation-operators.md)|  
|Either permanently or temporarily combine two set expressions into a single set.|[Set Operators](../mdx/set-operators.md)|  
|Performs an operation on one operand.|[Unary Operators](../mdx/unary-operators.md)|  
  
> [!NOTE]  
>  In queries, anyone who can see the data in the cube to be used with some type of operator can perform operations. However, you need the appropriate permissions before you can successfully change the data.  
  
 When using multiple operators, the order in which MDX evaluates the operators is important. Similarly, the user of operators may require that one data type be converted into another data type before the operators can be evaluated.  
  
## Evaluating Complex Expressions  
 You can build an expression by using operators to combine several smaller expressions. In these complex expressions, MDX evaluates the operators in order based on the definition of operator precedence used by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. MDX performs operators with higher precedence before performing operators with lower precedence.  
  
### Understanding Operator Precedence  
 The following list shows operator precedence, from highest to lowest. Operators in the same line are equal in precedence, and are evaluated from left to right unless otherwise forced by parenthesis:  
  
-   IS  
  
-   AS  
  
-   DISTINCT  
  
-   :  
  
-   ^  
  
-   /, *  
  
-   +, -  
  
-   EXISTING  
  
-   <>, >=, =, \<=, >, <  
  
-   NOT  
  
-   AND  
  
-   XOR  
  
-   OR  
  
 For more information about operators in MDX, see [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md).  
  
### Determining Results  
 When you combine simple expressions to form a complex expression, the rules for the operators combined with the rules for data type precedence determine the data type of the resulting value.  
  
 If the result is a character or Unicode value, the rules for the operators combined with the rules for collation precedence determines the collation of the result. For more information about collations, see [Languages and Collations &#40;Analysis Services&#41;](../analysis-services/languages-and-collations-analysis-services.md).  
  
 There are also rules that determine the precision, scale, and length of the result based on the precision, scale, and length of the simple expressions.  
  
## Converting Data Types  
 MDX implicitly converts an object to a different type when that object is used in an expression that requires a different type. The following table defines the conversion rules for each object.  
  
|Original Type|Type Needed|Conversion|  
|-------------------|-----------------|----------------|  
|Level|Set|\<level>.members|  
|Hierarchy|Member|\<hierarchy>.defaultmember|  
|Member|Tuple|(\<Member>)|  
|Tuple|Member|\<tuple>.item(0)|  
|Tuple|Scalar|\<tuple>.value|  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)   
 [MDX Syntax Elements &#40;MDX&#41;](../mdx/mdx-syntax-elements-mdx.md)  
  
  
