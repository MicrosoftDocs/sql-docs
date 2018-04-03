---
title: "CalculationProperty Element (ASSL) | Microsoft Docs"
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
  - "CalculationProperty Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CalculationProperty"
helpviewer_keywords: 
  - "CalculationProperty element"
ms.assetid: 5f0b4cfc-7d25-4c01-a517-cc2e89859be3
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# CalculationProperty Element (ASSL)
  Contains a collection of user interface properties for a calculation used in an [MdxScript](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CalculationProperties>  
   <CalculationProperty>  
      <CalculationReference>...</CalculationReference>  
      <CalculationType>...</CalculationType>  
      <Translations>...</Translations>  
      <Description>...</Description>  
      <Visible>...</Visible>  
      <SolveOrder>...</SolveOrder>  
      <FormatString>...</FormatString>  
      <ForeColor>...</ForeColor>  
      <BackColor>...</BackColor>  
            <FontName>...</FontName>  
            <FontSize>...</FontSize>  
            <FontFlags>...</FontFlags>  
            <NonEmptyBehavior>...</NonEmptyBehavior>  
      <AssociatedMeasureGroupID>...</AssociatedMeasureGroupID>  
      <DisplayFolder>...</DisplayFolder>  
   </CalculationProperty>  
</CalculationProperties>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CalculationProperties](../../../2014/analysis-services/dev-guide/calculationproperties-element-assl.md)|  
|Child elements|[AssociatedMeasureGroupID](../../../2014/analysis-services/dev-guide/associatedmeasuregroupid-element-assl.md), [BackColor](../../../2014/analysis-services/dev-guide/backcolor-element-assl.md), [CalculationReference](../../../2014/analysis-services/dev-guide/calculationreference-element-assl.md), [CalculationType](../../../2014/analysis-services/dev-guide/calculationtype-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [DisplayFolder](../../../2014/analysis-services/dev-guide/displayfolder-element-assl.md), [FontFlags](../../../2014/analysis-services/dev-guide/fontflags-element-assl.md), [FontName](../../../2014/analysis-services/dev-guide/fontname-element-assl.md), [FontSize](../../../2014/analysis-services/dev-guide/fontsize-element-assl.md), [ForeColor](../../../2014/analysis-services/dev-guide/forecolor-element-assl.md), [FormatString](../../../2014/analysis-services/dev-guide/formatstring-element-assl.md), [NonEmptyBehavior](../../../2014/analysis-services/dev-guide/nonemptybehavior-element-assl.md), [SolveOrder](../../../2014/analysis-services/dev-guide/solveorder-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Visible](../../../2014/analysis-services/dev-guide/visible-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CalculationProperty>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  