---
title: "RelationshipEndTranslation Element (ASSL) | Microsoft Docs"
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
ms.assetid: 04e09370-fdfe-4051-9998-4a6859ce8c54
caps.latest.revision: 4
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# RelationshipEndTranslation Element (ASSL)
  Defines a primitive data type that represents a localized translation for a [RelationshipEnd](../../../2014/analysis-services/dev-guide/relationshipend-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<RelationshipEndTranslation>  
   <Language>...</Language>  
   <Caption>...</Caption>  
   <CollectionCaption>...</Caption>  
   <Description>...</Description>  
   <DisplayFolder>...</DisplayFolder>  
   <Annotations>...</Annotations>  
</RelationshipEndTranslation>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[AttributeTranslation](../../../2014/analysis-services/dev-guide/attributetranslation-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Caption](../../../2014/analysis-services/dev-guide/caption-element-assl.md), [CollectionCaption](../../../2014/analysis-services/dev-guide/caption-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [DisplayFolder](../../../2014/analysis-services/dev-guide/displayfolder-element-assl.md), [Language](../../../2014/analysis-services/dev-guide/language-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Translation>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  