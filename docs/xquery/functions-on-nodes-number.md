---
title: "number Function (XQuery) | Microsoft Docs"
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
  - "number function"
  - "fn:number function"
ms.assetid: dff6d19b-765c-4df9-afff-9a0e7be9b91b
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Functions on Nodes - number
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns the numeric value of the node that is indicated by *$arg*.  
  
## Syntax  
  
```  
  
fn:number() as xs:double?   
fn:number($arg as node()?) as xs:double?  
```  
  
## Arguments  
 *$arg*  
 Node whose value will be returned as a number.  
  
## Remarks  
 If *$arg* is not specified, the numeric value of the context node, converted to a double, is returned. In SQL Server, **fn:number()** without an argument can only be used in the context of a context-dependent predicate. Specifically, it can only be used inside brackets ([ ]). For example, the following expression returns the <`ROOT`> element.  
  
```  
declare @x xml  
set @x='<ROOT>111</ROOT>'  
select @x.query('/ROOT[number()=111]')  
```  
  
 If the value of the node is not a valid lexical representation of a numeric simple type, as defined in **XML Schema Part 2:Datatypes, W3C Recommendation**, the function returns an empty sequence. NaN is not supported.  
  
## Examples  
 This topic provides XQuery examples against XML instances stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the number() XQuery function to retrieve the numeric value of an attribute  
 The following query retrieves the numeric value of the lot size attribute from the first work center location in the manufacturing process of Product Model 7.  
  
```  
SELECT ProductModelID, Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" ;  
     for $i in (//AWMI:root//AWMI:Location)[1]  
     return   
       <Location LocationID="{ ($i/@LocationID) }"   
                   LotSizeA="{  $i/@LotSize }"  
                   LotSizeB="{  number($i/@LotSize) }"  
                   LotSizeC="{ number($i/@LotSize) + 1 }" >  
  
       </Location>  
') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 Note the following from the previous query:  
  
-   The **number()** function is not required, as shown by the query for the **LotSizeA** attribute. This is an XPath 1.0 function and is included primarily for backward compatibility reasons.  
  
-   The XQuery for **LotSizeB** specifies the number function and is redundant.  
  
-   The query for **LotSizeD** illustrates the use of a number value in an arithmetic operation.  
  
 This is the result:  
  
```  
ProductModelID   Result  
----------------------------------------------  
7              <Location LocationID="10"   
                         LotSizeA="100"   
                         LotSizeB="100"   
                         LotSizeC="101" />  
```  
  
### Implementation Limitations  
 These are the limitations:  
  
-   The **number()** function only accepts nodes. It does not accept atomic values.  
  
-   When values cannot be returned as a number, the **number()** function returns the empty sequence instead of NaN.  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
