---
title: "RelationshipEndTranslation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: 04e09370-fdfe-4051-9998-4a6859ce8c54
caps.latest.revision: 5
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# RelationshipEndTranslation Element (ASSL)
  Defines a primitive data type that represents a localized translation for a [RelationshipEnd](../../../analysis-services/scripting/data-type/relationshipend-data-type-assl.md) element.  
  
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
|Derived data types|[AttributeTranslation](../../../analysis-services/scripting/data-type/attributetranslation-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Translations](../../../analysis-services/scripting/collections/translations-element-assl.md)|  
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [Caption](../../../analysis-services/scripting/properties/caption-element-assl.md), [CollectionCaption](../../../analysis-services/scripting/properties/caption-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [DisplayFolder](../../../analysis-services/scripting/properties/displayfolder-element-assl.md), [Language](../../../analysis-services/scripting/properties/language-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Translation>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  