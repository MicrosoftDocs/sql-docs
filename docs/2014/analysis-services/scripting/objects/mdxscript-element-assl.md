---
title: "MdxScript Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MdxScript Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MdxScript"
helpviewer_keywords: 
  - "MdxScript element"
ms.assetid: 0c59a550-dc95-4d50-948a-bb99348bdd86
author: minewiskan
ms.author: owend
manager: craigg
---
# MdxScript Element (ASSL)
  Contains information about a Multidimensional Expressions (MDX) script that is associated with a [Cube](cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MdxScripts>  
   <MdxScript>  
      <Name>...</Name>  
      <ID>...</ID>  
      <Description>...</Description>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <Annotations>...</Annotations>  
      <Commands>...</Commands>  
      <DefaultScript>...</DefaultScript>  
      <CalculationProperties>...</CalculationProperties>  
   </MdxScript>  
</MdxScripts>  
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
|Parent elements|[MdxScripts](../collections/mdxscripts-element-assl.md)|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [CalculationProperties](../collections/calculationproperties-element-assl.md), [Commands](../collections/commands-element-assl.md), [CreatedTimestamp](../properties/createdtimestamp-element-assl.md), [DefaultScript](../properties/defaultscript-element-assl.md), [Description](../properties/description-element-assl.md), [ID](../properties/id-element-assl.md), [LastSchemaUpdate](../properties/lastschemaupdate-element-assl.md), [Name](../properties/name-element-assl.md)|  
  
## Remarks  
 A script's `DefaultScript` element is set to `True` by default. Setting `DefaultScript` to `True` for a particular script sets `DefaultScript` to `False` for all other scripts.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MdxScript>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
