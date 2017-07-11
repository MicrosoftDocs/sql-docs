---
title: "Example: Retrieving Binary Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-xml"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "RAW mode, retrieving binary data example"
ms.assetid: 5cea5d49-58ac-403a-a933-c4fd91de400b
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Example: Retrieving Binary Data
  The following query returns the product photo stored in a **varbinary(max)** type column. The `BINARY BASE64` option is specified in the query to return the binary data in base64-encoded format.  
  
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
 [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)  
  
  