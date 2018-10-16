---
title: "ClearCache Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ClearCache Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ClearCache"
  - "urn:schemas-microsoft-com:xml-analysis#ClearCache"
  - "microsoft.xml.analysis.clearcache"
helpviewer_keywords: 
  - "ClearCache command"
ms.assetid: e154b489-e443-469a-9490-43c62da62e11
author: minewiskan
ms.author: owend
manager: craigg
---
# ClearCache Element (XMLA)
  Clears the memory cache for the specified object on a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <ClearCache>  
      <Object>...</Object>  
   </ClearCache>  
</Command>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Object](../xml-elements-properties/object-element-xmla.md)|  
  
## Remarks  
 The `ClearCache` command flushes the cache for a specified database, dimension, cube, measure group, or partition on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. If an object other than a database, dimension, cube, measure group, or partition is specified in the `Object` element, an error occurs.  
  
## See Also  
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
