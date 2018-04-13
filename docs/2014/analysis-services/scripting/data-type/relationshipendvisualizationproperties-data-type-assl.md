---
title: "RelationshipEndVisualizationProperties Data Type (ASSL) | Microsoft Docs"
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
ms.assetid: 11f9a10f-d36c-4faf-b595-3fe969d1935e
caps.latest.revision: 5
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# RelationshipEndVisualizationProperties Data Type (ASSL)
  Defines a primitive data type that represents a relationship end in a relationship.  
  
## Syntax  
  
```xml  
  
<RelationshipEnd>  
   <FolderPosition>...</FolderPosition>  
   <ContextualNameRule>...</ContextualNameRule>  
   <DefaultDetailsPosition>...</DefaultDetailsPosition>  
   <DisplayKeyPosition>...</DisplayKeyPosition>  
   <CommonIdentifierPosition>...</CommonIdentifierPosition>  
   <IsDefaultMeasure>...</IsDefaultMeasure>  
   <IsDefaultImage>...</IsDefaultImage>  
   <SortPropertiesPosition>...</SortPropertiesPosition>  
</RelationshipEnd>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[RelationshipEnd](relationshipend-data-type-assl.md)|  
|Child elements|[FolderPosition](../../dev-guide/folderposition-element-xml.md), [ContextualNameRule](../../dev-guide/contextualnamerule-element-xml.md), [DefaultDetailsPosition](../../dev-guide/defaultdetailsposition-element-xml.md), [DisplayKeyPosition](../../dev-guide/displaykeyposition-element-xml.md), [CommonIdentifierPosition](../../dev-guide/commonidentifierposition-element-xml.md), [IsDefaultMeasure](../../dev-guide/isdefaultmeasure-element-xml.md), [IsDefaultImage](../../dev-guide/isdefaultimage-element-xml.md), [SortPropertiesPosition](../../dev-guide/sortpropertiesposition-element-xml.md)|  
|Derived elements||  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RelationshipEnd>.  
  
  