---
title: "AttributeTranslation Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AttributeTranslation Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AttributeTranslation"
helpviewer_keywords: 
  - "AttributeTranslation data type"
ms.assetid: a0e29941-ef08-42ad-ab9c-b2efd7910895
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributeTranslation Data Type (ASSL)
  Defines a derived data type that represents a translation associated with an [Attribute](../objects/attribute-element-assl.md) element  
  
## Syntax  
  
```xml  
  
<AttributeTranslation>  
   <!-- The following elements extend Translation -->  
   <CaptionColumn>...</CaptionColumn>  
   <MembersWithDataCaption>...</MembersWithDataCaption>  
</AttributeTranslation>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Translation](translation-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[CaptionColumn](../objects/column-element-assl.md), [MembersWithDataCaption](../properties/caption-element-assl.md)|  
|Derived elements|See [Translation](../objects/translation-element-assl.md) ([Translations](../collections/translations-element-assl.md) collection of [DimensionAttribute](dimensionattribute-data-type-assl.md) or [ScalarMiningStructureColumn](miningstructurecolumn-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeTranslation>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
