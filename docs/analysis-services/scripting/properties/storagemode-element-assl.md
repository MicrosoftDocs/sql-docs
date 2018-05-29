---
title: "StorageMode Element (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# StorageMode Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Determines the storage mode for the parent element.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Dimension, MeasureGroup, Partition -->  
   ...  
   <StorageMode>...</StorageMode>  
   ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*MOLAP*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cube Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/cube-element-assl.md), [Dimension Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/dimension-element-assl.md), [MeasureGroup Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/measuregroup-element-assl.md), [Partition Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*MOLAP*|The parent uses multidimensional OLAP (MOLAP) storage.|  
|*ROLAP*|The parent uses relational OLAP (ROLAP) storage.|  
|*HOLAP*|The parent uses hybrid OLAP (HOLAP) storage.<br /><br /> Note: This value is not valid for [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md) parent elements.|  
|*InMemory*|The parent uses IMBI storage.|  
  
 The enumeration that corresponds to the allowed values for **StorageMode** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.StorageMode>.  
  
 The elements that correspond to the parents of **StorageMode** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.Dimension>, <xref:Microsoft.AnalysisServices.MeasureGroup>, and <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
