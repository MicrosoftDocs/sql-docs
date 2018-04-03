---
title: "Locations Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Locations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Locations"
  - "urn:schemas-microsoft-com:xml-analysis#Locations"
  - "microsoft.xml.analysis.locations"
helpviewer_keywords: 
  - "Locations element"
ms.assetid: 630929cb-f0dc-472a-86bc-28b67e907c3d
caps.latest.revision: 10
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Locations Element (XMLA)
  Contains a collection of [Location](../../../2014/analysis-services/dev-guide/query-element-xmla.md) elements used by the parent [Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md) , or [Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
< Backup> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Locations>  
      <Location>...</Location>  
   </Locations>  
   ...  
</Backup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md) , or [Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md)|  
|Child elements|[Location](../../../2014/analysis-services/dev-guide/location-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  