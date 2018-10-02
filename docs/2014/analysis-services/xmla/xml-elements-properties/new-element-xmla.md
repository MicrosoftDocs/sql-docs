---
title: "New Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "New Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#New"
  - "microsoft.xml.analysis.new"
  - "urn:schemas-microsoft-com:xml-analysis#New"
helpviewer_keywords: 
  - "New element"
ms.assetid: 1321adcb-67f7-40f0-8f20-b17c1d3e3f17
author: minewiskan
ms.author: owend
manager: craigg
---
# New Element (XMLA)
  Contains the new file system storage location used by a [Folder](folder-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Folder>  
   ...  
   <New>...</New>  
   ...  
</Folder>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Folder](folder-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `New` element contains a UNC path that replaces the value of the `Original` element contained by the parent `Folder` element for all objects restored or synchronized, respectively, during a `Restore` or `Synchronize` command. The value of the `Original` element is compared to the value of the `StorageLocation` element for each cube, measure group, or partition and, if a match is found, the value of this element is used to update the `StorageLocation` of the object during restoration or synchronization.  
  
 For more information about backing up and restoring objects, see [Backing Up and Restoring Objects (XMLA)](../../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [Original Element &#40;XMLA&#41;](original-element-xmla.md)   
 [Restore Element &#40;XMLA&#41;](../xml-elements-commands/restore-element-xmla.md)   
 [StorageLocation Element &#40;ASSL&#41;](../../scripting/properties/storagelocation-element-assl.md)   
 [Synchronize Element &#40;XMLA&#41;](../xml-elements-commands/synchronize-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
