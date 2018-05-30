---
title: "SourceColumnID Element (ASSL) | Microsoft Docs"
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
# SourceColumnID Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the identifier (ID) of the source mining structure column in the ancestor [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningModelColumn>  
   ...  
   <SourceColumnID>...</SourceColumnID>  
   ...  
</MiningModelColumn>  
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
|Parent element|[MiningModelColumn](../../../analysis-services/scripting/data-type/miningmodelcolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **SourceColumnID** element matches the identifier of a mining structure column in the [Columns](../../../analysis-services/scripting/collections/columns-element-assl.md) collection of the parent **MiningStructure**.  
  
 The element that corresponds to the parent of **SourceColumnID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelColumn>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
