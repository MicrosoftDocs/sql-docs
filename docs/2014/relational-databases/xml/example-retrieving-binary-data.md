---
title: "Example: Retrieving Binary Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "RAW mode, retrieving binary data example"
ms.assetid: 5cea5d49-58ac-403a-a933-c4fd91de400b
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Example: Retrieving Binary Data
  The following query returns the product photo stored in a `varbinary(max)` type column. The `BINARY BASE64` option is specified in the query to return the binary data in base64-encoded format.  
  
## Example  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductPhotoID, ThumbNailPhoto  
FROM Production.ProductPhoto  
WHERE ProductPhotoID=1  
FOR XML RAW, BINARY BASE64 ;  
GO  
```  
  
 This is the result:  
  
```  
<row ProductModelID="1" ThumbNailPhoto="base64 encoded binary data"/>  
```  
  
## See Also  
 [Use RAW Mode with FOR XML](use-raw-mode-with-for-xml.md)  
  
  
