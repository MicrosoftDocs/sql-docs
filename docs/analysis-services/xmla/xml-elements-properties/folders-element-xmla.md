---
title: "Folders Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Folders Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.folders"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Folders"
  - "urn:schemas-microsoft-com:xml-analysis#Folders"
helpviewer_keywords: 
  - "Folders element"
ms.assetid: fefb0469-22ea-4804-8dc3-9c49825b32f1
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Folders Element (XMLA)
  Contains a collection of [Folder](../../../analysis-services/xmla/xml-elements-properties/folder-element-xmla.md) elements used by the parent [Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md) element during a [Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md) or [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md) command.  
  
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
|Parent elements|[Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md)|  
|Child elements|[Folder](../../../analysis-services/xmla/xml-elements-properties/folder-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Restore Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md)   
 [Synchronize Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  