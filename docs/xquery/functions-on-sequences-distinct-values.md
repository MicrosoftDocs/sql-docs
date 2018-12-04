---
title: "distinct-values Function (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "distinct-values function"
  - "fn:distinct-values function"
ms.assetid: f4c2bb53-2bec-4f1a-9c00-cf997fb7ae5b
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Functions on Sequences - distinct-values
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes duplicate values from the sequence specified by *$arg*. If *$arg* is an empty sequence, the function returns the empty sequence.  
  
## Syntax  
  
```  
  
fn:distinct-values($arg as xdt:anyAtomicType*) as xdt:anyAtomicType*  
```  
  
## Arguments  
 *$arg*  
 Sequence of atomic values.  
  
## Remarks  
 All types of the atomized values that are passed to **distinct-values()** have to be subtypes of the same base type. Base types that are accepted are the types that support the **eq** operation. These types include the three built-in numeric base types, the date/time base types, xs:string, xs:boolean, and xdt:untypedAtomic. Values of type xdt:untypedAtomic are cast to xs:string. If there is  a mixture of these types, or if other values of other types are passed, a static error is raised.  
  
 The result of **distinct-values()** receives the base type of the passed in types, such as xs:string in the case of xdt:untypedAtomic, with the original cardinality. If the input is statically empty, empty is implied and a static error is raised.  
  
 Values of type xs:string are compared to the XQuery default Unicode Codepoint Collation.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the distinct-values() function to remove duplicate values from the sequence  
 In this example, an XML instance that contains telephone numbers is assigned to an **xml** type variable. The XQuery specified against this variable uses the **distinct-values()** function to compile a list of telephone numbers that do not contain duplicates.  
  
```  
declare @x xml  
set @x = '<PhoneNumbers>  
 <Number>111-111-1111</Number>  
 <Number>111-111-1111</Number>  
 <Number>222-222-2222</Number>  
</PhoneNumbers>'  
-- 1st select  
select @x.query('  
  distinct-values( data(/PhoneNumbers/Number) )  
') as result  
```  
  
 This is the result:  
  
```  
111-111-1111 222-222-2222    
```  
  
 In the following query, a sequence of numbers (1, 1, 2) is passed in to the **distinct-values()** function. The function then removes the duplicate in the sequence and returns the other two.  
  
```  
declare @x xml  
set @x = ''  
select @x.query('  
  distinct-values((1, 1, 2))  
') as result  
```  
  
 The query returns 1 2.  
  
### Implementation Limitations  
 These are the limitations:  
  
-   The **distinct-values()** function maps integer values to xs:decimal.  
  
-   The **distinct-values()** function only supports the previously mentioned types and does not support the mixture of base types.  
  
-   The **distinct-values()** function on xs:duration values is not supported.  
  
-   The syntactic option that provides a collation is not supported.  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
