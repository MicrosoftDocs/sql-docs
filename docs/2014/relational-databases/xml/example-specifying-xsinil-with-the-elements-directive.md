---
title: "Example: Specifying XSINIL with the ELEMENTS Directive | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "RAW mode, specifying XSINIL example"
ms.assetid: 07c873ff-1f9d-480e-8536-862c39eb8249
author: douglaslMS
ms.author: douglasl
manager: craigg
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
 [Use RAW Mode with FOR XML](use-raw-mode-with-for-xml.md)  
  
  
