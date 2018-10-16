---
title: "Folders Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Folders Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.folders"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Folders"
  - "urn:schemas-microsoft-com:xml-analysis#Folders"
helpviewer_keywords: 
  - "Folders element"
ms.assetid: fefb0469-22ea-4804-8dc3-9c49825b32f1
author: minewiskan
ms.author: owend
manager: craigg
---
# Folders Element (XMLA)
  Contains a collection of [Folder](folder-element-xmla.md) elements used by the parent [Location](location-element-xmla.md) element during a [Restore](../xml-elements-commands/restore-element-xmla.md) or [Synchronize](../xml-elements-commands/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Location>  
   ...  
   <Folders>  
      <Folder>...</Folder>  
   </Folders>  
   ...  
</Location>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None (collection)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Location](location-element-xmla.md)|  
|Child elements|[Folder](folder-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Restore Element &#40;XMLA&#41;](../xml-elements-commands/restore-element-xmla.md)   
 [Synchronize Element &#40;XMLA&#41;](../xml-elements-commands/synchronize-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
