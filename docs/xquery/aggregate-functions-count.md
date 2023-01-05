---
description: "Aggregate Functions - count"
title: "count Function (XQuery) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: xml
ms.topic: "language-reference"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "fn:count function"
  - "count function [XQuery]"
ms.assetid: a9f7131f-23e1-4d4d-a36c-180447543926
author: "rothja"
ms.author: "jroth"
---
# Aggregate Functions - count
[!INCLUDE [SQL Server Azure SQL Database ](../includes/applies-to-version/sqlserver.md)]

  Returns the number of items that are contained in the sequence specified by *$arg*.  
  
## Syntax  
  
```  
  
fn:count($arg as item()*) as xs:integer  
```  
  
## Arguments  
 *$arg*  
 Items to count.  
  
## Remarks  
 Returns 0 if *$arg* is an empty sequence.  
  
## Examples  
 This topic provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.  
  
### A. Using the count() XQuery function to count the number of work center locations in the manufacturing of a product model  
 The following query counts the number of work center locations in the manufacturing process of a product model (ProductModelID=7).  
  
```  
SELECT Production.ProductModel.ProductModelID,   
       Production.ProductModel.Name,   
       Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
       <NoOfWorkStations>  
          { count(/AWMI:root/AWMI:Location) }  
       </NoOfWorkStations>  
') as WorkCtrCount  
FROM Production.ProductModel  
WHERE Production.ProductModel.ProductModelID=7  
```  
  
 Note the following from the previous query:  
  
-   The **namespace** keyword in [XQuery Prolog](../xquery/modules-and-prologs-xquery-prolog.md) defines a namespace prefix. The prefix is then used in the XQuery body.  
  
-   The query constructs XML that includes the <`NoOfWorkStations`> element.  
  
-   The **count()** function in the XQuery body counts the number of <`Location`> elements.  
  
 This is the result:  
  
```  
ProductModelID   Name                 WorkCtrCount       
-------------- ---------------------------------------------------  
7             HL Touring Frame  <NoOfWorkStations>6</NoOfWorkStations>     
```  
  
 You can also construct the XML to include the product model ID and name, as shown in the following query:  
  
```  
SELECT Instructions.query('  
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
       <NoOfWorkStations  
             ProductModelID= "{ sql:column("Production.ProductModel.ProductModelID") }"   
             ProductModelName = "{ sql:column("Production.ProductModel.Name") }" >  
          { count(/AWMI:root/AWMI:Location) }  
       </NoOfWorkStations>  
') as WorkCtrCount  
FROM Production.ProductModel  
WHERE Production.ProductModel.ProductModelID= 7  
```  
  
 This is the result:  
  
```  
<NoOfWorkStations ProductModelID="7"   
                  ProductModelName="HL Touring Frame">6</NoOfWorkStations>  
```  
  
 Instead of XML, you may return these values as non-xml type, as shown in the following query. The query uses the [value() method (xml data type)](../t-sql/xml/value-method-xml-data-type.md) to retrieve the work center location count.  
  
```  
SELECT  ProductModelID,   
        Name,   
        Instructions.value('declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
           count(/AWMI:root/AWMI:Location)', 'int' ) as WorkCtrCount  
FROM Production.ProductModel  
WHERE ProductModelID=7  
```  
  
 This is the result:  
  
```  
ProductModelID    Name            WorkCtrCount  
-------------- ---------------------------------  
7              HL Touring Frame        6     
```  
  
## See Also  
 [XQuery Functions against the xml Data Type](../xquery/xquery-functions-against-the-xml-data-type.md)  
  
  
