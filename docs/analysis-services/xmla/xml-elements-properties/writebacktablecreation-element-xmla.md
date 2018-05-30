---
title: "WritebackTableCreation Element (XMLA) | Microsoft Docs"
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
# WritebackTableCreation Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Determines whether a writeback table is created during the [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) operation.  
  
## Syntax  
  
```xml  
  
<Process>  
...  
   <WritebackTableCreation>...</WritebackTableCreation>  
...  
</Process>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For more information about processing options available to objects on an Analysis Services instance, see [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
 The value of the **WritebackTableCreation** element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Create*|Create a new writeback table, if one does not exist. If a writeback table already exists, an error occurs.|  
|*CreateAlways*|Create a new writeback table, overwriting any existing writeback table.|  
|*UseExisting*|Use the existing writeback table, if one already exists. If one does not exist, an error occurs.|  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
