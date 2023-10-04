---
title: "floor Function (XQuery)"
description: Learn about the XQuery floor() function that returns the largest number with no fraction part that is not greater than the value of its argument.
author: "rothja"
ms.author: "jroth"
ms.date: "03/09/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "language-reference"
helpviewer_keywords:
  - "floor function [XQuery]"
  - "fn:floor function"
dev_langs:
  - "XML"
---
# Numeric Values Functions - floor
[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sqlserver.md)]

  Returns the largest number with no fraction part that is not greater than the value of its argument. If the argument is an empty sequence, it returns the empty sequence.  
  
## Syntax  
  
```  
  
fn:floor ($arg as numeric?) as numeric?  
```  
  
## Arguments  
 *$arg*  
 Number to which the function is applied.  
  
## Remarks  
 If the type of *$arg* is one of the three numeric base types, **xs:float**, **xs:double**, or **xs:decimal**, the return type is same as the *$arg* type. If the type of *$arg* is a type that is derived from one of the numeric types, the return type is the base numeric type.  
  
 If input to the fn:floor, fn:ceiling, or fn:round functions is **xdt:untypedAtomic**, untyped data, it is implicitly cast to **xs:double**. Any other type generates a static error.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks sample database.  
  
 You can use the working sample in the [ceiling function (XQuery)](../xquery/numeric-values-functions-ceiling.md) for the **floor()** XQuery function. All you have to do is replace the **ceiling()** function in the query with the **floor()** function.  
  
## Implementation Limitations  
 These are the limitations:  
  
-   The **floor()** function maps all integer values to xs:decimal.  
  
## See Also  
 [ceiling Function &#40;XQuery&#41;](../xquery/numeric-values-functions-ceiling.md)   
 [round Function &#40;XQuery&#41;](../xquery/numeric-values-functions-round.md)   
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
