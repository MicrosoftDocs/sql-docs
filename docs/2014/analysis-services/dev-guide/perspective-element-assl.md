---
title: "Perspective Element (ASSL) | Microsoft Docs"
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
  - "Perspective Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Perspective"
helpviewer_keywords: 
  - "Perspective element"
ms.assetid: 0442334c-8b00-4451-ad81-02e58c735e8f
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Perspective Element (ASSL)
  Defines details for a perspective of a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Perspectives>  
   <<Perspective>  
      <Name>...</Name>  
      <ID>...</ID>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <Description>...</Description>  
      <Translations>...</Translations>  
      <DefaultMeasure>...</DefaultMeasure>  
      <Dimensions>...</Dimensions>  
            <MeasureGroups>...</MeasureGroups>  
      <Calculations>...</Calculations>  
      <Kpis>...</Kpis>  
            <Actions>...</Actions>  
      <Annotations>...</Annotations>  
   </Perspective>  
</Perspectives>  
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
|Parent elements|[Perspectives](../../../2014/analysis-services/dev-guide/perspectives-element-assl.md)|  
|Child elements|[Actions](../../../2014/analysis-services/dev-guide/actions-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Calculations](../../../2014/analysis-services/dev-guide/calculations-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [DefaultMeasure](../../../2014/analysis-services/dev-guide/defaultmeasure-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Kpis](../../../2014/analysis-services/dev-guide/kpis-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [MeasureGroups](../../../2014/analysis-services/dev-guide/measuregroups-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
  
## Remarks  
 A perspective provides a subset of a cube, selecting the dimensions, hierarchies, attributes, and other details that are to be included, and defining the slice of data to be included. A perspective is owned by a single cube. It is not possible to override or add objects within a perspective; all dimensions, hierarchies, and other details must exist in the underlying cube. It is not possible to include objects and mark them as not visible.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Perspective>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  