---
title: "min Function (XQuery) | Microsoft Docs"
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
  - "fn:min function"
  - "min function [XQuery]"
ms.assetid: db0b7d94-3fa6-488f-96d6-6a9a7d6eda23
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Aggregate Functions - min
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns from a sequence of atomic values, *$arg*, the one item whose value is less than that of all the others.  
  
## Syntax  
  
```  
  
fn:min($arg as xdt:anyAtomicType*) as xdt:anyAtomicType?  
```  
  
## Arguments  
 *$arg*  
 Sequence of items from which to return the minimum value.  
  
## Remarks  
 All types of the atomized values that are passed to **min()** have to be subtypes of the same base type. Base types that are accepted are the types that support the **gt** operation. These types include the three built-in numeric base types, the date/time base types, xs:string, xs:boolean, and xdt:untypedAtomic. Values of type xdt:untypedAtomic are cast to xs:double. If there is a mixture of these types, or if other values of other types are passed, a static error is raised.  
  
 The result of **min()** receives the base type of the passed in types, such as xs:double in the case of xdt:untypedAtomic. If the input is statically empty, empty is implied and a static error is returned.  
  
 The **min()** function returns the one value in the sequence that is smaller than any other in the input sequence. For xs:string values, the default Unicode Codepoint Collation is being used. If an xdt:untypedAtomic value cannot be cast to xs:double, the value is ignored in the input sequence, *$arg*. If the input is a dynamically calculated empty sequence, the empty sequence is returned.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the min() XQuery function to find the work center location that has the fewest labor hours  
 The following query retrieves all work center locations in the manufacturing process of the product model (ProductModelID=7) that have the fewest labor hours. Generally, as shown in the following, a single location is returned. If multiple locations had an equal number of minimum labor hours, they would all be returned.  
  
```  
select ProductModelID, Name, Instructions.query('  
  declare namespace AWMI=  
    "https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
  for   $Location in /AWMI:root/AWMI:Location  
  where $Location/@LaborHours =  
          min( /AWMI:root/AWMI:Location/@LaborHours )  
return  
  <Location WCID=     "{ $Location/@LocationID }"   
              LaborHrs= "{ $Location/@LaborHours }" />  
  ') as Result   
FROM  Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 Note the following from the previous query:  
  
-   The **namespace** keyword in the XQuery prolog defines a namespace prefix. This prefix is then used in the XQuery body.  
  
 The XQuery body constructs the XML that has a \<Location> element with WCID and **LaborHrs** attributes.  
  
-   The query also retrieves the ProductModelID and name values.  
  
 This is the result:  
  
```  
ProductModelID   Name              Result  
---------------  ----------------  ---------------------------------  
7                HL Touring Frame  <Location WCID="45" LaborHrs="0.5"/>   
```  
  
## Implementation Limitations  
 These are the limitations:  
  
-   The **min()** function maps all integers to xs:decimal.  
  
-   The **min()** function on values of type xs:duration is not supported.  
  
-   Sequences that mix types across base type boundaries are not supported.  
  
-   The syntactic option that provides a collation is not supported.  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
