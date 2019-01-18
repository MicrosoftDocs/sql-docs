---
title: "ceiling Function (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: sql
ms.reviewer: ""
ms.technology: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:ceiling function"
  - "ceiling function [XQuery]"
ms.assetid: 594f1dd0-3c27-41b3-b809-9ce6714c5a97
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Numeric Values Functions - ceiling 
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns the smallest number without a fractional part and that is not less than the value of its argument. If the argument is an empty sequence, it returns the empty sequence.  
  
## Syntax  
  
```  
  
fn:ceiling ( $arg as numeric?) as numeric?  
```  
  
## Arguments  
 *$arg*  
 Number to which the function is applied.  
  
## Remarks  
 If the type of *$arg* is one of the three numeric base types, **xs:float**, **xs:double**, or **xs:decimal**, the return type is the same as the *$arg* type.  
  
 If the type of *$arg* is a type that is derived from one of the numeric types, the return type is the base numeric type.  
  
 If the input to the fn:floor, fn:ceiling, or fn:round functions is **xdt:untypedAtomic**, it is implicitly cast to **xs:double**.  
  
 Any other type generates a static error.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the ceiling() XQuery function  
 For product model 7, this query returns a list of the work center locations in the manufacturing process of the product model. For each work center location, the query returns the location ID, labor hours, and lot size, if documented. The query uses the **ceiling** function to return the labor hours as values of type **decimal**.  
  
```  
SELECT ProductModelID, Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";   
     for $i in /AWMI:root/AWMI:Location  
     return   
       <Location LocationID="{ $i/@LocationID }"   
                   LaborHrs="{ ceiling($i/@LaborHours) }" >  
                    {   
                      $i/@LotSize  
                    }    
       </Location>  
') AS Result  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 Note the following from the previous query:  
  
-   The AWMI namespace prefix stands for Adventure Works Manufacturing Instructions. This prefix refers to the same namespace used in the document being queried.  
  
-   **Instructions** is an **xml** type column. Therefore, the [query() method (XML data type)](../t-sql/xml/query-method-xml-data-type.md) is used to specify XQuery. The XQuery statement is specified as the argument to the query method.  
  
-   **for ... return** is a loop construct. In the query, the **for** loop identifies a list of \<Location> elements. For each work center location, the **return** statement in the **for** loop describes the XML to be generated:  
  
    -   A \<Location> element that has LocationID and LaborHrs attributes. The corresponding expression inside the braces ({ }) retrieves the required values from the document.  
  
    -   The { $i/@LotSize } expression retrieves the LotSize attribute from the document, if present.  
  
    -   This is the result:  
  
```  
ProductModelID Result    
-------------- ------------------------------------------------------  
7      <Location LocationID="10" LaborHrs="3" LotSize="100"/>  
       <Location LocationID="20" LaborHrs="2" LotSize="1"/>     
       <Location LocationID="30" LaborHrs="1" LotSize="1"/>     
       <Location LocationID="45" LaborHrs="1" LotSize="20"/>  
       <Location LocationID="60" LaborHrs="3" LotSize="1"/>     
       <Location LocationID="60" LaborHrs="4" LotSize="1"/>  
```  
  
### Implementation Limitations  
 These are the limitations:  
  
-   The **ceiling()** function maps all integer values to xs:decimal.  
  
## See Also  
 [floor Function &#40;XQuery&#41;](../xquery/numeric-values-functions-floor.md)   
 [round Function &#40;XQuery&#41;](../xquery/numeric-values-functions-round.md)  
  
  
