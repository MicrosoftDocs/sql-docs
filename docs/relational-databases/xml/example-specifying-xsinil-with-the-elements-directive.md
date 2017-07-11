---
title: "Example: Specifying XSINIL with the ELEMENTS Directive | Microsoft Docs"
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
  - "RAW mode, specifying XSINIL example"
ms.assetid: 07c873ff-1f9d-480e-8536-862c39eb8249
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Example: Specifying XSINIL with the ELEMENTS Directive
  The following query specifies the `ELEMENTS` directive to generate element-centric XML from the query result.  
  
## Example  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductID, Name, Color  
FROM Production.Product  
FOR XML RAW, ELEMENTS;  
GO  
```  
  
 This is the partial result.  
  
```  
<row>  
  <ProductID>1</ProductID>  
  <Name>Adjustable Race</Name>  
</row>  
...  
<row>  
  <ProductID>317</ProductID>  
  <Name>LL Crankarm</Name>  
  <Color>Black</Color>  
</row>  
```  
  
 Because the `Color` column has null values for some products, the resulting XML will not generate the corresponding <`Color`> element. By adding the `XSINIL` directive with `ELEMENTS`, you can generate the <`Color`> element even for NULL color values in the result set.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT ProductID, Name, Color  
FROM Production.Product  
FOR XML RAW, ELEMENTS XSINIL ;  
```  
  
 This is the partial result:  
  
```  
<row xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
  <ProductID>1</ProductID>  
  <Name>Adjustable Race</Name>  
  <Color xsi:nil="true" />  
</row>  
...  
<row>  
  <ProductID>317</ProductID>  
  <Name>LL Crankarm</Name>  
  <Color>Black</Color>  
</row>  
```  
  
## See Also  
 [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)  
  
  