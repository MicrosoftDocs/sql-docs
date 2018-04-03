---
title: "Kpi Element (ASSL) | Microsoft Docs"
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
  - "Kpi Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Kpi"
helpviewer_keywords: 
  - "Kpi element"
ms.assetid: 1979a58f-97a8-4c1a-aa65-dcfb6d2404cf
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Kpi Element (ASSL)
  Defines a key performance indicator (KPI) within a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element or a [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Kpis>  
   <Kpi> <!-- ancestor: Cube -->  
            <Name>...</Name>  
      <ID>...</ID>  
      <Description>...</Description>  
      <Translations>...</Translations>  
      <DisplayFolder>...</DisplayFolder>  
      <AssociatedMeasureGroupID>...</AssociatedMeasureGroupID>  
      <Value>...</Value>  
            <Goal>...</Goal>  
      <Status>...</Status>  
      <Trend>...</Trend>  
      <TrendGraphic>...</TrendGraphic>  
      <StatusGraphic>...</StatusGraphic>  
      <CurrentTimeMember>...</CurrentTimeMember>  
      <Annotations>...</Annotations>  
   </Kpi>  
   <!-- or -->  
   <Kpi xsi:type="PerspectiveKpi">...</Kpi> <!-- ancestor: Perspective -->  
   </Kpi>  
</Kpis>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|None|  
|[Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|[PerspectiveKpi](../../../2014/analysis-services/dev-guide/perspectivekpi-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Kpis](../../../2014/analysis-services/dev-guide/kpis-element-assl.md)|  
  
## Child Elements  
  
|Ancestor or Parent|Child elements|  
|------------------------|--------------------|  
|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [AssociatedMeasureGroupID](../../../2014/analysis-services/dev-guide/associatedmeasuregroupid-element-assl.md), [CurrentTimeMember](../../../2014/analysis-services/dev-guide/currenttimemember-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [DisplayFolder](../../../2014/analysis-services/dev-guide/displayfolder-element-assl.md), [Goal](../../../2014/analysis-services/dev-guide/goal-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Status](../../../2014/analysis-services/dev-guide/status-element-assl.md), [StatusGraphic](../../../2014/analysis-services/dev-guide/statusgraphic-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Trend](../../../2014/analysis-services/dev-guide/trend-element-assl.md), [TrendGraphic](../../../2014/analysis-services/dev-guide/trendgraphic-element-assl.md), [Value](../../../2014/analysis-services/dev-guide/value-element-assl.md)|  
|[Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|None|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Kpi> and <xref:Microsoft.AnalysisServices.PerspectiveKpi>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  