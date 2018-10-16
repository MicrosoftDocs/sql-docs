---
title: "Original Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Original Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.original"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Original"
  - "urn:schemas-microsoft-com:xml-analysis#Original"
helpviewer_keywords: 
  - "Original element"
ms.assetid: c98a3700-ac19-4341-85d9-5afedf662601
author: minewiskan
ms.author: owend
manager: craigg
---
# Original Element (XMLA)
  Contains the original file system storage location used by a [Folder](folder-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Folder>  
   ...  
   <Original>...</Original>  
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
 The `Original` element contains a UNC path to be replaced with the value of the [New](new-element-xmla.md) element contained by the parent `Folder` element for all objects restored or synchronized, respectively, during a [Restore](../xml-elements-commands/restore-element-xmla.md) or [Synchronize](../xml-elements-commands/synchronize-element-xmla.md) command. The value of this element is compared to the value of the [StorageLocation](../../scripting/properties/storagelocation-element-assl.md) element for each cube, measure group, or partition and, if a match is found, the value of the `New` element is used to update the `StorageLocation` of the object during restoration or synchronization.  
  
 For more information about backing up and restoring objects, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
