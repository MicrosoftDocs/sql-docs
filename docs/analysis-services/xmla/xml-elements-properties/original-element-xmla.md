---
title: "Original Element (XMLA) | Microsoft Docs"
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
# Original Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains the original file system storage location used by a [Folder](../../../analysis-services/xmla/xml-elements-properties/folder-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Folder>  
   ...  
   <Original>...</Original>  
   ...  
</Folder>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Folder](../../../analysis-services/xmla/xml-elements-properties/folder-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Original** element contains a UNC path to be replaced with the value of the [New](../../../analysis-services/xmla/xml-elements-properties/new-element-xmla.md) element contained by the parent **Folder** element for all objects restored or synchronized, respectively, during a [Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md) or [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md) command. The value of this element is compared to the value of the [StorageLocation](../../../analysis-services/scripting/properties/storagelocation-element-assl.md) element for each cube, measure group, or partition and, if a match is found, the value of the **New** element is used to update the **StorageLocation** of the object during restoration or synchronization.  
  
 For more information about backing up and restoring objects, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
