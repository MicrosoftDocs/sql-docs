---
title: "sum Function (XQuery) | Microsoft Docs"
description: Learn about the XQuery function sum() that returns the sum of a sequence of numbers.
ms.custom: ""
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "sum function [XQuery]"
  - "fn:sum function"
ms.assetid: 12288f37-b54c-4237-b75e-eedc5fe8f96d
author: "rothja"
ms.author: "jroth"
---
# Aggregate Functions - sum
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns the sum of a sequence of numbers.  
  
## Syntax  
  
```  
  
fn:sum($arg as xdt:anyAtomicType*) as xdt:anyAtomicType  
```  
  
## Arguments  
 *$arg*  
 Sequence of atomic values whose sum is to be computed.  
  
## Remarks  
 All types of the atomized values that are passed to **sum()** have to be subtypes of the same base type. Base types that are accepted are the three built-in numeric base types or xdt:untypedAtomic. Values of type xdt:untypedAtomic are cast to xs:double. If there is a mixture of these types, or if other values of other types are passed, a static error is raised.  
  
 The result of **sum()** receives the base type of the passed in types such as xs:double in the case of xdt:untypedAtomic, even if the input is optionally the empty sequence. If the input is statically empty, the result is 0 with the static and dynamic type of xs:integer.  
  
 The **sum()** function returns the sum of the numeric values. If an xdt:untypedAtomic value cannot be cast to xs:double, the value is ignored in the input sequence, *$arg*. If the input is a dynamically calculated empty sequence, the value 0 of the used base type is returned.  
  
 The function returns a runtime error when an overflow or out of range exception occurs.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
### A. Using the sum() XQuery function to find the total combined number of labor hours for all work center locations in the manufacturing process  
 The following query finds the total labor hours for all work center locations in the manufacturing process of all product models for which manufacturing instructions are stored.  
  
```  
SELECT Instructions.query('         
   declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";         
  <ProductModel PMID= "{ sql:column("Production.ProductModel.ProductModelID") }"         
  ProductModelName = "{ sql:column("Production.ProductModel.Name") }" >         
   <TotalLaborHrs>         
     { sum(//AWMI:Location/@LaborHours) }         
   </TotalLaborHrs>         
 </ProductModel>         
    ') as Result         
FROM Production.ProductModel         
WHERE Instructions is not NULL         
```  
  
 This is the partial result.  
  
```  
<ProductModel PMID="7" ProductModelName="HL Touring Frame">  
   <TotalLaborHrs>12.75</TotalLaborHrs>  
</ProductModel>  
<ProductModel PMID="10" ProductModelName="LL Touring Frame">  
  <TotalLaborHrs>13</TotalLaborHrs>  
</ProductModel>  
...  
```  
  
 Instead of returning the result as XML, you can write the query to generate relational results, as shown in the following query:  
  
```  
SELECT ProductModelID,         
        Name,         
        Instructions.value('declare namespace   
      AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";         
    sum(//AWMI:Location/@LaborHours)', 'float') as TotalLaborHours         
FROM Production.ProductModel         
WHERE Instructions is not NULL          
```  
  
 This is a partial result:  
  
```  
ProductModelID Name                 TotalLaborHours         
-------------- -------------------------------------------------  
7              HL Touring Frame           12.75                   
10             LL Touring Frame           13                      
43             Touring Rear Wheel         3                       
...  
```  
  
### Implementation Limitations  
 These are the limitations:  
  
-   Only the single argument version of **sum()** is supported.  
  
-   If the input is a dynamically calculated empty sequence, the value 0 of the used base type is returned instead of type xs:integer.  
  
-   The **sum()** function maps all integers to xs:decimal.  
  
-   The **sum()** function on values of type xs:duration is not supported.  
  
-   Sequences that mix types across base type boundaries are not supported.  
  
-   The sum((xs:double("INF"), xs:double("-INF"))) raises a domain error.  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
