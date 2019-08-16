---
title: "Columns without a Name | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "names [SQL Server], columns without"
ms.assetid: 440de44e-3a56-4531-b4e4-1533ca933cac
author: MightyPen
ms.author: genemi
manager: craigg
---
# Columns without a Name
  Any column without a name will be inlined. For example, computed columns or nested scalar queries that do not specify column alias will generate columns without any name. If the column is of `xml` type, the content of that data type instance is inserted. Otherwise, the column content is inserted as a text node.  
  
```  
SELECT 2+2  
FOR XML PATH  
```  
  
 Produce this XML. By default, for each row in the rowset, a <`row`> element is generated in the resulting XML. This is the same as RAW mode.  
  
 `<row>4</row>`  
  
 The following query returns a three-column rowset. The third column without a name has XML data. The PATH mode inserts an instance of the xml type.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductModelID,  
       Name,  
       Instructions.query('declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
                /MI:root/MI:Location   
              ')   
FROM Production.ProductModel  
WHERE ProductModelID=7  
FOR XML PATH ;  
GO  
```  
  
 This is the partial result:  
  
 `<row>`  
  
 `<ProductModelID>7</ProductModelID>`  
  
 `<Name>HL Touring Frame</Name>`  
  
 `<MI:Location ...LocationID="10" ...></MI:Location>`  
  
 `<MI:Location ...LocationID="20" ...></MI:Location>`  
  
 `...`  
  
 `</row>`  
  
## See Also  
 [Use PATH Mode with FOR XML](use-path-mode-with-for-xml.md)  
  
  
