---
title: "Arithmetic Expressions (XQuery) | Microsoft Docs"
description: Learn about arithmetic expressions in XQuery and which arithmetic operators are supported.
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "expressions [XQuery], arithmetic"
  - "arithmetic expressions"
ms.assetid: 90d675bf-56da-459a-9771-8cd13920a9fc
author: "rothja"
ms.author: "jroth"
---
# Arithmetic Expressions (XQuery)
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  All arithmetic operators are supported, except for **idiv**. The following examples illustrate the basic use of arithmetic operators:  
  
```  
DECLARE @x xml  
SET @x=''  
SELECT @x.query('2 div 2')  
SELECT @x.query('2 * 2')  
```  
  
 Because **idiv** is not supported, a solution is to use the **xs:integer()** constructor:  
  
```  
DECLARE @x xml  
SET @x=''  
-- Following will not work  
-- SELECT @x.query('2 idiv 2')  
-- Workaround   
SELECT @x.query('xs:integer(2 div 3)')  
```  
  
 The resulting type from an arithmetic operator is based on the types of the input values. If the operands are different types, either one or both when required  will be cast to a common primitive base type according to the type hierarchy. For information about type hierarchy, see [Type Casting Rules in XQuery](../xquery/type-casting-rules-in-xquery.md).  
  
 Numeric type promotion occurs if the two operations are different numeric base types. For example, adding an **xs:decimal** to an **xs:double** would first change the decimal value to a double . Next, addition would be performed that would result in a double value.  
  
 Untyped atomic values are cast to the other operand's numeric base type, or to **xs:double** if the other operand is also untyped.  
  
## Implementation Limitations  
 These are the limitations:  
  
-   Arguments for the arithmetic operators must be of numeric type or **untypedAtomic**.  
  
-   Operations on **xs:integer** values result in a value of type **xs:decimal** instead of **xs:integer**.  
  
  
