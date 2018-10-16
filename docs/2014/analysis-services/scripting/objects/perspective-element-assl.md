---
title: "Perspective Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# Perspective Element (ASSL)
  Defines details for a perspective of a [Cube](cube-element-assl.md) element.  
  
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
|Parent elements|[Perspectives](../collections/perspectives-element-assl.md)|  
|Child elements|[Actions](../collections/actions-element-assl.md), [Annotations](../collections/annotations-element-assl.md), [Calculations](../collections/calculations-element-assl.md), [CreatedTimestamp](../properties/createdtimestamp-element-assl.md), [DefaultMeasure](measure-element-assl.md), [Description](../properties/description-element-assl.md), [Dimensions](../collections/dimensions-element-assl.md), [ID](../properties/id-element-assl.md), [Kpis](../collections/kpis-element-assl.md), [LastSchemaUpdate](../properties/lastschemaupdate-element-assl.md), [MeasureGroups](../collections/groups-element-assl.md), [Name](../properties/name-element-assl.md), [Translations](../collections/translations-element-assl.md)|  
  
## Remarks  
 A perspective provides a subset of a cube, selecting the dimensions, hierarchies, attributes, and other details that are to be included, and defining the slice of data to be included. A perspective is owned by a single cube. It is not possible to override or add objects within a perspective; all dimensions, hierarchies, and other details must exist in the underlying cube. It is not possible to include objects and mark them as not visible.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Perspective>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
