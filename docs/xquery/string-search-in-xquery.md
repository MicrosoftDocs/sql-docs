---
title: "String Search in XQuery"
description: Learn how to search text in XML documents by viewing an example of string search in XQuery.
author: "rothja"
ms.author: "jroth"
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "language-reference"
helpviewer_keywords:
  - "strings [SQL Server], search"
  - "XML [SQL Server], searching text"
  - "searches [SQL Server], XML documents"
  - "XQuery, string search"
dev_langs:
  - "XML"
---
# String Search in XQuery
[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sqlserver.md)]

  This topic provides sample queries that show how to search text in XML documents.  
  
## Examples  
  
### A. Find feature descriptions that contain the word "maintenance" in the product catalog  
  
```  
SELECT CatalogDescription.query('  
     declare namespace p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";  
    for $f in /p1:ProductDescription/p1:Features/*  
     where contains(string($f), "maintenance")  
     return  
           $f ') as Result  
FROM Production.ProductModel  
WHERE ProductModelID=19  
```  
  
 In the previous query, the `where` in the FLOWR expression filters the result of the `for` expression and returns only elements that satisfy the **contains()** condition.  
  
 This is the result:  
  
```  
<p1:Maintenance     
      xmlns:p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain">  
 <p1:NoOfYears>10</p1:NoOfYears>  
 <p1:Description>maintenance contact available through your   
               dealer or any AdventureWorks retail store.</p1:Description>  
</p1:Maintenance>  
```  
  
## See Also  
 [XML Data &#40;SQL Server&#41;](../relational-databases/xml/xml-data-sql-server.md)   
 [XQuery Language Reference &#40;SQL Server&#41;](../xquery/xquery-language-reference-sql-server.md)  
  
  
