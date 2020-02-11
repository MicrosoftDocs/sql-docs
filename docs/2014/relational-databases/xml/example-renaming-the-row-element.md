---
title: "Example: Renaming the &lt;row&gt; Element | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "RAW mode, renaming <row> example"
ms.assetid: b042292a-0b6e-40a3-b254-71c06e626706
author: MightyPen
ms.author: genemi
manager: craigg
---
# Example: Renaming the &lt;row&gt; Element
  For each row in the result set, the RAW mode generates an element `<row>`. You can optionally specify another name for this element by specifying an optional argument to the RAW mode, as shown in this query. The query returns a <`ProductModel`> element for each row in the rowset.  
  
## Example  
  
```  
SELECT ProductModelID, Name   
FROM Production.ProductModel  
WHERE ProductModelID=122  
FOR XML RAW ('ProductModel'), ELEMENTS  
GO  
```  
  
 This is the result. Because the `ELEMENTS` directive is added in the query, the result is element-centric.  
  
```  
<ProductModel>  
  <ProductModelID>122</ProductModelID>  
  <Name>All-Purpose Bike Stand</Name>  
</ProductModel>   
```  
  
## See Also  
 [Use RAW Mode with FOR XML](use-raw-mode-with-for-xml.md)  
  
  
