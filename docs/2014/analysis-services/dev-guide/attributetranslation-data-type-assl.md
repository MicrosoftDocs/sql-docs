---
title: "AttributeTranslation Data Type (ASSL) | Microsoft Docs"
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
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AttributeTranslation Data Type (ASSL)
  Defines a derived data type that represents a translation associated with an [Attribute](../../../2014/analysis-services/dev-guide/attribute-element-assl.md) element  
  
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
|Base data types|[Translation](../../../2014/analysis-services/dev-guide/translation-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[CaptionColumn](../../../2014/analysis-services/dev-guide/captioncolumn-element-assl.md), [MembersWithDataCaption](../../../2014/analysis-services/dev-guide/memberswithdatacaption-element-assl.md)|  
|Derived elements|See [Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md) ([Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md) collection of [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md) or [ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeTranslation>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  