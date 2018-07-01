---
title: "ConnectionString Element (XMLA) | Microsoft Docs"
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
# ConnectionString Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains a connection string used by the parent [Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md) or [Source](../../../analysis-services/xmla/xml-elements-properties/source-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Location> <!-- or Source -->  
   ...  
   <ConnectionString>...</ConnectionString>  
   ...  
</Location>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|See the table below.|  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md)|1-1: Required element that occurs once and only once.|  
|[Source](../../../analysis-services/xmla/xml-elements-properties/source-element-xmla.md)|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md), [Source](../../../analysis-services/xmla/xml-elements-properties/source-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For **Location** elements, the **ConnectionString** element contains the connection string used by the **Restore** or **Synchronize** command to either update a local data source or connect to a remote instance.  
  
 For **Source** elements, the **ConnectionString** element contains the connection string used by the **Synchronize** command to connect to the source instance.  
  
 For more information about backing up and restoring objects, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See also
 [Restore Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md)   
 [Synchronize Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
