---
title: "DisplayFolder Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "DisplayFolder Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DisplayFolder"
helpviewer_keywords: 
  - "DisplayFolder element"
ms.assetid: 55184c02-03e7-4d6c-b87a-d4d34bc11d0e
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DisplayFolder Element (ASSL)
  Specifies the folder in which to list the parent element. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] applications for developers and administrators may support the use of display folders to categorize multiple elements visually.  
  
## Syntax  
  
```xml  
  
<CalculationProperty> <!-- or Hierarchy, Kpi, Measure, Translation -->  
   ...  
   <DisplayFolder>...</DisplayFolder>  
   ...  
</CalculationProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md), [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md), [Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md), [Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 In larger cubes, there may be hundreds of measures and hierarchies. The `DisplayFolder` property defines user appearance on the client. The value of the `DisplayFolder` property can contain any one of the following options:  
  
-   Be empty, denoting that the measure does not belong to a folder.  
  
-   Contain a single folder name, denoting that the measure should be rendered as belonging to a folder with the same name.  
  
-   Contain multiple folder names separated by a backslash (\\), denoting an embedded folder hierarchy.  
  
 The `DisplayFolder` property applies to `CalculationProperty` elements only if the value of [CalculationType](../../../2014/analysis-services/dev-guide/calculationtype-element-assl.md) is set to *Member*.  
  
 The elements that correspond to the parents of `DisplayFolder` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty>, <xref:Microsoft.AnalysisServices.Hierarchy>, <xref:Microsoft.AnalysisServices.Kpi>, <xref:Microsoft.AnalysisServices.Measure>, and <xref:Microsoft.AnalysisServices.Translation>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  