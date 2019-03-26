---
title: "Operators in Expressions (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: d22dc8b6-4353-40e7-91a1-65e8dae6325d
author: markingmyname
ms.author: maghan
---
# Operators in Expressions (Report Builder and SSRS)
  An operator is a symbol that represents actions applied to one or more terms in an expression. The following categories of operators are supported in an expression: arithmetic, comparison, concatenation, logical or bitwise, and bit shift.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### Arithmetic  
 Arithmetic operators perform mathematical operations on two numeric terms in an expression.  
  
|Operator|Description|  
|--------------|-----------------|  
|^|Raises a number to the power of another number.|  
|*|Multiplies two numbers.|  
|/|Divides two numbers and returns a floating-point result.|  
|\|Divides two numbers and returns an integer result.|  
|Mod|Returns the integer remainder of a division. For example, 7 Mod 5 = 2 because the remainder of 7 divided by 5 is 2.|  
|+|Adds two numbers together.|  
|-|Returns the difference between two numbers or indicates the negative value of a numeric term.|  
  
### Comparison  
 Comparison operators test whether two expressions are the same.  
  
|Operator|Description|  
|--------------|-----------------|  
|<|Less than.|  
|\<=|Less than or equal to.|  
|>|Greater than.|  
|>=|Greater than or equal to.|  
|=|Equal to.|  
|<>|Not equal to.|  
|Like|Determines whether a specific character string matches a specified pattern. A pattern can include regular characters and wildcard characters. During pattern matching, regular characters must exactly match the characters specified in the character string. However, wildcard characters can be matched with arbitrary fragments of the character string. Using wildcard characters makes the LIKE operator more flexible than using the = and != string comparison operators.<br /><br /> The following table lists characters that can be used as wildcards:<br /><br /> %: Any string of zero or more characters.<br /><br /> _: Any single character.<br /><br /> [ ]: Any single character within the specified range (for example, [a-f]) or set (for example, [aeiou]).<br /><br /> [^]: Any single character not within the specified range (for example, [^a-f]) or set (for example, [^aeiou])|  
|Is|Compares two object references.|  
  
### String Concatenation  
 String concatenation appends the second string to the first string in an expression. For other string operations, use built-in functions.  
  
|Operator|Description|  
|--------------|-----------------|  
|&|Concatenates two strings|  
|+|Concatenates two strings|  
  
### Logical and Bitwise  
 Logical and bitwise operators perform logical manipulations between two integer terms in an expression.  
  
|Operator|Description|  
|--------------|-----------------|  
|And|Performs a logical conjunction on two Boolean expressions, or bitwise conjunction on two numeric expressions.|  
|Not|Performs logical negation on a Boolean expression, or bitwise negation on a numeric expression.|  
|Or|Performs a logical disjunction on two Boolean expressions, or bitwise disjunction on two numeric values.|  
|Xor|Performs a logical exclusion operation on two Boolean expressions, or a bitwise exclusion on two numeric expressions.|  
|AndAlso|Performs logical conjunction on two expressions.|  
|OrElse|Performs logical disjunction on two expressions.|  
  
### Bit Shift  
 Bitwise operators perform bit manipulations between two integer terms in an expression.  
  
|Operator|Description|  
|--------------|-----------------|  
|<\<|Performs an arithmetic left-shift on a bit pattern.|  
|>>|Performs an arithmetic right-shift on a bit pattern.|  
  
## See Also  
 [Expression Dialog Box](https://msdn.microsoft.com/library/e6c74ccb-4594-4d4f-b958-618d710e34eb)   
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/data-types-in-expressions-report-builder-and-ssrs.md)   
 [Expression Dialog Box &#40;Report Builder&#41;](https://msdn.microsoft.com/library/e89c4d97-5d41-4b55-8695-79329edac15d)  
  
  
