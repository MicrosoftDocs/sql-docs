---
title: "Folder Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Folder Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.folder"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Folder"
  - "urn:schemas-microsoft-com:xml-analysis#Folder"
helpviewer_keywords: 
  - "Folder element"
ms.assetid: 87b305b0-57e3-4ec3-9d80-f1bcf3ce7540
author: minewiskan
ms.author: owend
manager: craigg
---
# Folder Element (XMLA)
  Contains a file system storage location to be updated for a [Location](location-element-xmla.md) element during a [Restore](../xml-elements-commands/restore-element-xmla.md) or [Synchronize](../xml-elements-commands/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Folders>  
   ...  
   <Folder>  
      <Original>...</Original>  
      <New>...</New>  
   </Folder>  
   ...  
</Folders>  
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
|Parent elements|[Folders](folders-element-xmla.md)|  
|Child elements|[New](new-element-xmla.md), [Original](original-element-xmla.md)|  
  
## Remarks  
 The `Folder` element, if specified, changes the storage locations of objects contained either by the backup file (for `Restore` commands) or the database on the source instance (for `Synchronize` commands) that match the value of the `Original` element to the value of the `New` element.  
  
 For more information about backing up and restoring objects, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [StorageLocation Element &#40;ASSL&#41;](../../scripting/properties/storagelocation-element-assl.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
