---
title: "ReportFormatParameters Element (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ReportFormatParameters Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the collection of [ReportFormatParameter](../../../analysis-services/scripting/objects/reportformatparameter-element-asl.md) elements for a [ReportAction](../../../analysis-services/scripting/data-type/reportaction-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Action xsi:type="ReportAction">  
   ...  
   <ReportFormatParameters>  
      <ReportFormatParameter>...</ReportFormatParameter>  
   </ReportFormatParameters>  
   ...  
</Action>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Action](../../../analysis-services/scripting/objects/action-element-assl.md) of type [ReportAction](../../../analysis-services/scripting/data-type/reportaction-data-type-assl.md)|  
|Child elements|[ReportFormatParameter](../../../analysis-services/scripting/objects/reportformatparameter-element-asl.md)|  
  
## Remarks  
 The element that corresponds to the parent of **ReportFormatParameters** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportAction>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  
