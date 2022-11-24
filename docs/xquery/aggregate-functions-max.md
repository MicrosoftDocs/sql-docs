---
title: "max Function (XQuery) | Microsoft Docs"
description: Learn about the XQuery max() function that returns the one item in a sequence whose value is greater than that of all the others.
ms.custom: ""
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "max function [XQuery]"
  - "fn:max function"
ms.assetid: 5ee625c0-044a-4cda-b210-02b64e619d65
author: "rothja"
ms.author: "jroth"
---
# Aggregate Functions - max
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns from a sequence of atomic values, *$arg*, the one item whose value is greater than that of all the others.  
  
## Syntax  
  
```  
  
fn:max($arg as xdt:anyAtomicType*) as xdt:anyAtomicType?  
```  
  
## Arguments  
 *$arg*  
 Sequence of atomic values from which to return the maximum value.  
  
## Remarks  
 All types of the atomized values that are passed to **max()** have to be subtypes of the same base type. Base types that are accepted are the types that support the **gt** operation. These types include the three built-in numeric base types, the date/time base types, xs:string, xs:boolean, and xdt:untypedAtomic. Values of type xdt:untypedAtomic are cast to xs:double. If there is a mixture of these types, or if other values of other types are passed, a static error is raised.  
  
 The result of **max()** receives the base type of the passed in types, such as xs:double in the case of xdt:untypedAtomic. If the input is statically empty, empty is implied and a static error is raised.  
  
 The **max()** function returns the one value in the sequence that is greater than any other in the input sequence. For xs:string values, the default Unicode Codepoint Collation is being used. If an xdt:untypedAtomic value cannot be cast to xs:double, the value is ignored in the input sequence, *$arg*. If the input is a dynamically calculated empty sequence, the empty sequence is returned.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
### A. Using the max() XQuery function to find work center locations in the manufacturing process that have the most labor hours  
 The query provided in [min function (XQuery)](../xquery/aggregate-functions-min.md) can be rewritten to use the **max()** function.  
  
## Implementation Limitations  
 These are the limitations:  
  
-   The **max(**) function maps all integers to xs:decimal.  
  
-   The **max()** function on values of type xs:duration is not supported.  
  
-   Sequences that mix types across base type boundaries are not supported.  
  
-   The syntactic option that provides a collation is not supported.  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
