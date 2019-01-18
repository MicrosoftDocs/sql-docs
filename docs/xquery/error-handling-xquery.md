---
title: "Error Handling (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "static errors"
  - "errors [XQuery]"
  - "XQuery, error handling"
  - "dynamic errors [XQuery]"
ms.assetid: 7dee3c11-aea0-4d10-9126-d54db19448f2
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Error Handling (XQuery)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  The W3C specification allows type errors to be raised statically or dynamically, and defines static, dynamic, and type errors.  
  
## Compilation and Error Handling  
 Compilation errors are returned from syntactically incorrect Xquery expressions and XML DML statements. The compilation phase checks static type correctness of XQuery expressions and DML statements, and uses XML schemas for type inferences for typed XML. It raises static type errors if an expression could fail at run time because of a type safety violation. Examples of static error are the addition of a string to an integer and querying for a nonexistent node for typed data.  
  
 As a deviation from the W3C standard, XQuery run-time errors are converted into empty sequences. These sequences may propagate as empty XML or NULL to the query result, depending upon the invocation context.  
  
 Explicit casting to the correct type allows users to work around static errors, although run-time cast errors will be transformed to empty sequences.  
  
## Static Errors  
 Static errors are returned by using the [!INCLUDE[tsql](../includes/tsql-md.md)] error mechanism. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], XQuery type errors are returned statically. For more information, see [XQuery and Static Typing](../xquery/xquery-and-static-typing.md).  
  
## Dynamic Errors  
 In XQuery, most dynamic errors are mapped to an empty sequence ("()"). However, these are the two exceptions: Overflow conditions in XQuery aggregator functions and XML-DML validation errors. Note that most dynamic errors are mapped to an empty sequence. Otherwise, query execution that takes advantages of the XML indexes may raise unexpected errors. Therefore, to provide an efficient execution without generating unexpected errors, [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] maps dynamic errors to ().  
  
 Frequently, in the situation where the dynamic error would occur inside a predicate, not raising the error is not changing the semantics, because () is mapped to False. However, in some cases, returning () instead of a dynamic error may cause unexpected results. The following are examples that illustrate this.  
  
### Example: Using the avg() Function with a String  
 In the following example, the [avg function](../xquery/aggregate-functions-avg.md) is called to compute the average of the three values. One of these values is a string. Because the XML instance in this case is untyped, all the data in it is of untyped atomic type. The **avg()** function first casts these values to **xs:double** before computing the average. However, the value, `"Hello"`, cannot be cast to **xs:double** and creates a dynamic error. In this case, instead of returning a dynamic error, the casting of `"Hello"` to **xs:double** causes an empty sequence. The **avg()** function ignores this value, computes the average of the other two values, and returns 150.  
  
```  
DECLARE @x xml  
SET @x=N'<root xmlns:myNS="test">  
 <a>100</a>  
 <b>200</b>  
 <c>Hello</c>  
</root>'  
SELECT @x.query('avg(//*)')  
```  
  
### Example: Using the not Function  
 When you use the [not function](../xquery/functions-on-boolean-values-not-function.md) in a predicate, for example, `/SomeNode[not(Expression)]`, and the expression causes a dynamic error, an empty sequence will be returned instead of an error. Applying **not()** to the empty sequence returns True, instead of an error.  
  
### Example: Casting a String  
 In the following example, the literal string "NaN" is cast to xs:string, then to xs:double. The result is an empty rowset. Although the string "NaN" cannot successfully be cast to xs:double, this cannot be determined until runtime because the string is first cast to xs:string.  
  
```  
DECLARE @x XML  
SET @x = ''  
SELECT @x.query(' xs:double(xs:string("NaN")) ')  
GO  
```  
  
 In this example, however, a static type error occurs.  
  
```  
DECLARE @x XML  
SET @x = ''  
SELECT @x.query(' xs:double("NaN") ')  
GO  
```  
  
#### Implementation Limitations  
 The **fn:error()** function is not supported.  
  
## See Also  
 [XQuery Language Reference &#40;SQL Server&#41;](../xquery/xquery-language-reference-sql-server.md)   
 [XQuery Basics](../xquery/xquery-basics.md)  
  
  
