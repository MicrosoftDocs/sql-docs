---
title: "Columns without a Name | Microsoft Docs"
description: Learn how SQL Server treats columns without a name when generating XML.
ms.custom: ""
ms.date: "08/01/2020"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "names [SQL Server], columns without"
ms.assetid: 440de44e-3a56-4531-b4e4-1533ca933cac
author: rothja
ms.author: jroth
---
# Columns without a Name
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  Any column without a name will be inlined. For example, computed columns or nested scalar queries that do not specify column alias will generate columns without any name. If the column is of **xml** type, the content of that data type instance is inserted. Otherwise, the column content is inserted as a text node.  
  
```sql
SELECT 2+2  
FOR XML PATH  
```  
  
 Produce this XML. By default, for each row in the rowset, a `<row>` element is generated in the resulting XML. This is the same as RAW mode.  
  
 `<row>4</row>`  
  
 The following query returns a three-column rowset. The third column without a name has XML data. The PATH mode inserts an instance of the xml type.  
  
```sql
USE AdventureWorks2012;  
GO  
SELECT ProductModelID,  
       Name,  
       Instructions.query(
           'declare namespace MI="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";  
            /MI:root/MI:Location   
           ')   
FROM Production.ProductModel  
WHERE ProductModelID=7  
FOR XML PATH ;  
GO  
```  
  
 This is the partial result:  
  
```xml
<row>
  <ProductModelID>7</ProductModelID>
  <Name>HL Touring Frame</Name>
  <MI:Location ...LocationID="10" ...></MI:Location>
  <MI:Location ...LocationID="20" ...></MI:Location>
  ...
</row>
```

## See Also  
 [Use PATH Mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md)  
  
  
