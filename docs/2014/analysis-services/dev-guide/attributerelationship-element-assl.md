---
title: "AttributeRelationship Element (ASSL) | Microsoft Docs"
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
  - "AttributeRelationship Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MemberProperty"
helpviewer_keywords: 
  - "AttributeRelationship element"
ms.assetid: 2e786109-b8bf-4295-b0fe-9c1997349993
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AttributeRelationship Element (ASSL)
  Provides details about the relationship between two attributes.  
  
## Syntax  
  
```xml  
  
<AttributeRelationships>  
      <AttributeRelationship>  
      <AttributeID>...</AttributeID>  
            <RelationshipType>...</RelationshipType>  
      <Cardinality>...</Cardinality>  
      <Optionality >...</Optionality>  
      <OverrideBehavior >...</OverrideBehavior>  
            <Annotations>...</Annotations>  
      <Name>...</Name>  
      <Visible>...</Visible>  
      <Translations>...</Translations>  
   </AttributeRelationship>  
</AttributeRelationships>  
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
|Parent elements|[AttributeRelationships](../../../2014/analysis-services/dev-guide/attributerelationships-element-assl.md)|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [AttributeID](../../../2014/analysis-services/dev-guide/attributeid-element-assl.md), [Cardinality](../../../2014/analysis-services/dev-guide/cardinality-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Optionality](../../../2014/analysis-services/dev-guide/optionality-element-assl.md), [OverrideBehavior](../../../2014/analysis-services/dev-guide/overridebehavior-element-assl.md), [RelationshipType](../../../2014/analysis-services/dev-guide/relationshiptype-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Visible](../../../2014/analysis-services/dev-guide/visible-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeRelationship>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  