---
title: "avg Function (XQuery) | Microsoft Docs"
description: Learn about the XQuery function avg() that returns the average of a specified sequence of numbers.
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:avg function"
  - "avg function [XQuery]"
ms.assetid: 0cc60267-3c56-4a88-8ad7-bb07f0255d56
author: "rothja"
ms.author: "jroth"
---
# Aggregate Functions - avg
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

  Returns the average of a sequence of numbers.  
  
## Syntax  
  
```  
  
fn:avg($arg as xdt:anyAtomicType*) as xdt:anyAtomicType?  
```  
  
## Arguments  
 *$arg*  
 The sequence of atomic values whose average is computed.  
  
## Remarks  
 All the types of the atomized values that are passed to **avg()** have to be a subtype of exactly one of the three built-in numeric base types or xdt:untypedAtomic. They cannot be a mixture. Values of type xdt:untypedAtomic are treated as xs:double. The result of **avg()** receives the base type of the passed in types, such as xs:double in the case of xdt:untypedAtomic.  
  
 If the input is statically empty, empty is implied and a static error is raised.  
  
 The **avg()** function returns the average of the numbers computed. For example:  
  
 **sum(** *$arg* **) div count(** *$arg* **)**  
  
 If *$arg* is an empty sequence, the empty sequence is returned.  
  
 If an xdt:untypedAtomic value cannot be cast to xs:double, the value is disregarded in the input sequence, *$arg*.  
  
 In all other cases, the function returns a static error.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the avg() XQuery function to find work center locations in the manufacturing process in which labor hours are greater than the average for all work center locations.  
 You can rewrite the query provided in [min function (XQuery)](../xquery/aggregate-functions-min.md) to use the **avg()** function.  
  
## Implementation Limitations  
 These are the limitations:  
  
-   The **avg()** function maps all integers to xs:decimal.  
  
-   The **avg()** function on values of type xs:duration is not supported.  
  
-   Sequences that mix types across base type boundaries are not supported.  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
