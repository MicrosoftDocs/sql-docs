---
title: "AttributeRelationship Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
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
|Parent elements|[AttributeRelationships](../collections/relationships-element-assl.md)|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [AttributeID](../properties/id-element-assl.md), [Cardinality](../properties/cardinality-element-assl.md), [Name](../properties/name-element-assl.md), [Optionality](../properties/optionality-element-assl.md), [OverrideBehavior](../properties/overridebehavior-element-assl.md), [RelationshipType](../properties/relationshiptype-element-assl.md), [Translations](../collections/translations-element-assl.md), [Visible](../properties/visible-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeRelationship>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
