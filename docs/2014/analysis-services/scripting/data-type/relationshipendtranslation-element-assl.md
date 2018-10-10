---
title: "RelationshipEndTranslation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 04e09370-fdfe-4051-9998-4a6859ce8c54
author: minewiskan
ms.author: owend
manager: craigg
---
# RelationshipEndTranslation Element (ASSL)
  Defines a primitive data type that represents a localized translation for a [RelationshipEnd](relationshipend-data-type-assl.md) element.  
  
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
|Derived data types|[AttributeTranslation](translation-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Translations](../collections/translations-element-assl.md)|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [Caption](../properties/caption-element-assl.md), [CollectionCaption](../properties/caption-element-assl.md), [Description](../properties/description-element-assl.md), [DisplayFolder](../properties/displayfolder-element-assl.md), [Language](../properties/language-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Translation>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
