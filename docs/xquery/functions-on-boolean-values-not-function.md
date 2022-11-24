---
title: "not Function (XQuery) | Microsoft Docs"
description: Learn how the XQuery not() function is used with Boolean values.
ms.custom: ""
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "effective Boolean value [XQuery]"
  - "fn:not function"
  - "not function [XQuery]"
  - "EBV"
ms.assetid: 93dfc377-45f1-4384-9392-560d9331a915
author: "rothja"
ms.author: "jroth"
---
# Functions on Boolean Values - not Function 
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns TRUE if the effective Boolean value of *$arg* is false, and returns FALSE if the effective Boolean value of *$arg* is true.  
  
## Syntax  
  
```  
  
fn:not($arg as item()*) as xs:boolean  
```  
  
## Arguments  
 *$arg*  
 A sequence of items for which there is an effective Boolean value.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the not() XQuery function to find product models whose catalog descriptions do not include the \<Specifications> element.  
 The following query constructs XML that contains product model IDs for product models whose catalog descriptions do not include the <`Specifications`> element.  
  
```  
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS pd)  
SELECT ProductModelID, CatalogDescription.query('  
       <Product   
           ProductModelID="{ sql:column("ProductModelID") }"  
        />  
') as Result  
FROM Production.ProductModel  
WHERE CatalogDescription.exist('  
     /pd:ProductDescription[not(pd:Specifications/*)]  '  
     ) = 0  
```  
  
 Note the following from the previous query:  
  
-   Because the document uses namespaces, the sample uses the WITH NAMESPACES statement. Another option is to use the **declare namespace** keyword in the [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md) to define the prefix.  
  
-   The query then constructs the XML that includes the <`Product`> element and its **ProductModelID** attribute.  
  
-   The WHERE clause uses the [exist() method (XML data type)](../t-sql/xml/exist-method-xml-data-type.md) to filter the rows. The **exist()** method returns True if there are \<ProductDescription> elements that do not have \<Specification> child elements. Note the use of the **not()** function.  
  
 This result set is empty, because each product model catalog description includes the \<Specifications> element.  
  
### B. Using the not() XQuery function to retrieve work center locations that do not have a MachineHours attribute  
 The following query is specified against the Instructions column. This column stores manufacturing instructions for the product models.  
  
 For a particular product model, the query retrieves work center locations that do not specify MachineHours. That is, the attribute **MachineHours** is not specified for the \<Location> element.  
  
```  
SELECT ProductModelID, Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" ;  
     for $i in /AWMI:root/AWMI:Location[not(@MachineHours)]  
     return  
       <Location LocationID="{ $i/@LocationID }"   
                   LaborHrs="{ $i/@LaborHours }" >  
        </Location>  
') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=7   
```  
  
 In the preceding query, note the following:  
  
-   The **declarenamespace** in [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md) defines the Adventure Works manufacturing instructions namespace prefix. It represents the same namespace used in the manufacturing instructions document.  
  
-   In the query, the **not(@MachineHours)** predicate returns True if there is no **MachineHours** attribute.  
  
 This is the result:  
  
```  
ProductModelID Result   
-------------- --------------------------------------------  
7              <Location LocationID="30" LaborHrs="1"/>  
               <Location LocationID="50" LaborHrs="3"/>  
               <Location LocationID="60" LaborHrs="4"/>  
```  
  
### Implementation Limitations  
 These are the limitations:  
  
-   The **not()** function only supports arguments of type xs:boolean, or node()*, or the empty sequence.  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
