---
title: "Folder Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Folder Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains a file system storage location to be updated for a [Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md) element during a [Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md) or [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md) command.  
  
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Folders](../../../analysis-services/xmla/xml-elements-properties/folders-element-xmla.md)|  
|Child elements|[New](../../../analysis-services/xmla/xml-elements-properties/new-element-xmla.md), [Original](../../../analysis-services/xmla/xml-elements-properties/original-element-xmla.md)|  
  
## Remarks  
 The **Folder** element, if specified, changes the storage locations of objects contained either by the backup file (for **Restore** commands) or the database on the source instance (for **Synchronize** commands) that match the value of the **Original** element to the value of the **New** element.  
  
 For more information about backing up and restoring objects, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See also
 [StorageLocation Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/storagelocation-element-assl.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
