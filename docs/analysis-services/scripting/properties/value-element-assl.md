---
title: "Value Element (ASSL) | Microsoft Docs"
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
# Value Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the value of the parent element.  
  
## Syntax  
  
```xml  
  
<AlgorithmParameter> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Value>...</Value>  
   ...  
</AlgorithmParameter>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|See the table below.|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[AlgorithmParameter](../../../analysis-services/scripting/objects/algorithmparameter-element-assl.md)|Any simpleType|  
|[ServerProperty](../../../analysis-services/scripting/objects/serverproperty-element-assl.md)|Any simpleType|  
|All others|String|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AlgorithmParameter](../../../analysis-services/scripting/objects/algorithmparameter-element-assl.md), [Annotation](../../../analysis-services/scripting/objects/annotation-element-assl.md), [Kpi](../../../analysis-services/scripting/objects/kpi-element-assl.md), [ReportFormatParameter](../../../analysis-services/scripting/objects/reportformatparameter-element-asl.md), [ReportParameter](../../../analysis-services/scripting/objects/reportparameter-element-assl.md), [ServerProperty](../../../analysis-services/scripting/objects/serverproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **Value** element contains the value associated with the parent object. The expected value of the **Value** element varies depending on the parent element, as described in the following table.  
  
|Parent element|Expected value|  
|--------------------|--------------------|  
|[AlgorithmParameter](../../../analysis-services/scripting/objects/algorithmparameter-element-assl.md)|The value of the algorithm parameter.|  
|[Annotation](../../../analysis-services/scripting/objects/annotation-element-assl.md)|The value of the annotation.|  
|[Kpi](../../../analysis-services/scripting/objects/kpi-element-assl.md)|A Multidimensional Expressions (MDX) expression used to calculate the value of the key performance indicator (KPI).|  
|[ReportFormatParameter](../../../analysis-services/scripting/objects/reportformatparameter-element-asl.md)|The value of the report format parameter.|  
|[ReportParameter](../../../analysis-services/scripting/objects/reportparameter-element-assl.md)|An MDX expression used to calculate the value of the report parameter.|  
|[ServerProperty](../../../analysis-services/scripting/objects/serverproperty-element-assl.md)|The value of the server property for the currently running instance.|  
  
 The elements that correspond to the parents of **Value** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AlgorithmParameter>, <xref:Microsoft.AnalysisServices.Annotation>, <xref:Microsoft.AnalysisServices.Kpi>, <xref:Microsoft.AnalysisServices.ReportParameter>, and <xref:Microsoft.AnalysisServices.ServerProperty>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
