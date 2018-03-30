---
title: "Level Element (ASSL) | Microsoft Docs"
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
  - "Level Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "LEVEL"
helpviewer_keywords: 
  - "Level element"
ms.assetid: 66ee2c16-d6b8-4dd3-879f-1f2b6923bc43
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Level Element (ASSL)
  Defines a level in a [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Levels>  
      <Level>  
      <Name>...</Name>  
      <ID>...</ID>  
      <Description>...</Description>  
      <SourceAttributeID>...</SourceAttributeID>  
      <HideMemberIf>...</HideMemberIf>  
      <Translations>...</Translations>  
      <Annotations>...</Annotations>  
   </Level>  
</Levels>  
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
|Parent element|[Levels](../../../2014/analysis-services/dev-guide/levels-element-assl.md)|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [HideMemberIf](../../../2014/analysis-services/dev-guide/hidememberif-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [SourceAttributeID](../../../2014/analysis-services/dev-guide/sourceattributeid-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
  
## Remarks  
 This data type has no restrictions under any deployment mode (1-Multidimensional and Data Mining, 2-SharePoint, and 3-Tabular).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Level>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  