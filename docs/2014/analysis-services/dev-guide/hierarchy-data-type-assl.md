---
title: "Hierarchy Data Type (ASSL) | Microsoft Docs"
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
  - "Hierarchy Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Hierarchy data type"
ms.assetid: 2e05917e-7e5d-4dd1-817b-4ff5647747ff
caps.latest.revision: 17
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Hierarchy Data Type (ASSL)
  Defines a primitive data type that represents a hierarchy in a dimension.  
  
## Syntax  
  
```xml  
  
<Hierarchy>  
   <Name>...</Name>  
   <ID>...</ID>  
   <Description>...</Description>  
   <DisplayFolder>...</DisplayFolder>  
   <Translations>...</Translations>  
   <AllMemberName>...</AllMemberName>  
   <AllMemberTranslations>...</AllMemberTranslation>  
   <MemberNamesUnique>...</MemberNamesUnique>  
   <AllowDuplicateNames>...</AllowDuplicateNames>  
   <Levels>...</Levels>  
   <Annotations>...</Annotation>  
</Hierarchy>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[AllMemberName](../../../2014/analysis-services/dev-guide/allmembername-element-assl.md), [AllMemberTranslations](../../../2014/analysis-services/dev-guide/allmembertranslations-element-assl.md), [AllowDuplicateNames](../../../2014/analysis-services/dev-guide/allowduplicatenames-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [DisplayFolder](../../../2014/analysis-services/dev-guide/displayfolder-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Levels](../../../2014/analysis-services/dev-guide/levels-element-assl.md), [MemberNamesUnique](../../../2014/analysis-services/dev-guide/membernamesunique-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
|Derived elements|[Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md)|  
  
## Remarks  
 The *MemberNamesUnique* element is not supported under DevelopmentMode 1 or 2 for SharePoint or Tabular server modes, respectively.  
  
 The *MemberKeysUnique* element is not supported under DevelopmentMode 1 or 2 for SharePoint or Tabular server modes, respectively.  
  
 The *AllowDuplicateNames* element is not supported under DevelopmentMode 1 or 2 for SharePoint or Tabular server modes, respectively.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Hierarchy>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  