---
title: "Columns with a Name Specified as a Wildcard Character | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "names [SQL Server], columns with"
ms.assetid: d9551df1-5bb4-4c0b-880a-5bb049834884
author: MightyPen
ms.author: genemi
manager: craigg
---
# Columns with a Name Specified as a Wildcard Character
  If the column name specified is a wildcard character (\*), the content of that column is inserted as if there is no column name specified. If this column is a non-`xml` type column, the column content is inserted as a text node, as shown in the following example:  
  
```  
USE AdventureWorks2012;  
GO  
SELECT E.BusinessEntityID "@EmpID",   
       FirstName "*",   
       MiddleName "*",   
       LastName "*"  
FROM   HumanResources.Employee AS E  
INNER JOIN Person.Person AS P  
    ON E.BusinessEntityID = P.BusinessEntityID  
WHERE E.BusinessEntityID=1  
FOR XML PATH;  
```  
  
 This is the result:  
  
 `<row EmpID="1">KenJS??nchez</row>`  
  
 If the column is of `xml` type, the corresponding XML tree is inserted. For example, the following query specifies "*" for the column name that contains the XML returned by the XQuery against the Instructions column.  
  
```  
SELECT   
       ProductModelID,  
       Name,  
       Instructions.query('declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"  
                /MI:root/MI:Location   
              ') as "*"  
FROM Production.ProductModel  
WHERE ProductModelID=7  
FOR XML PATH;   
GO  
```  
  
 This is the result. The XML returned by XQuery is inserted without a wrapping element.  
  
 `<row>`  
  
 `<ProductModelID>7</ProductModelID>`  
  
 `<Name>HL Touring Frame</Name>`  
  
 `<MI:Location LocationID="10">...</MI:Location>`  
  
 `<MI:Location LocationID="20">...</MI:Location>`  
  
 `...`  
  
 `</row>`  
  
## See Also  
 [Use PATH Mode with FOR XML](use-path-mode-with-for-xml.md)  
  
  
