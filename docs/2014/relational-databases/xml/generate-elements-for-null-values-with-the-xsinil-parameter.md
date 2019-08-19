---
title: "Generate Elements for NULL Values with the XSINIL Parameter | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "FOR XML clause, null values"
  - "null values [SQL Server], XML"
  - "ELEMENTS directive"
  - "XSINIL parameter"
ms.assetid: 2dbc4e48-1cae-4d83-b371-3265da9687cc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Generate Elements for NULL Values with the XSINIL Parameter
  The **ELEMENTS** directive constructs XML in which each column value maps to an element in the XML. If the column value is NULL, no element is added. By specifying the optional **XSINIL** parameter on the ELEMENTS directive, you can request that an element also be created for the NULL value. In this case, an element that has the **xsi:nil** attribute set to TRUE is returned for each NULL column value.  
  
## See Also  
 [Use RAW Mode with FOR XML](use-raw-mode-with-for-xml.md)  
  
  
