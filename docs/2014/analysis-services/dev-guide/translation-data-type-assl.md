---
title: "Translation Data Type (ASSL) | Microsoft Docs"
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
  - "Translation Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Translation data type"
ms.assetid: d40039e1-3ff6-4e20-8d8b-5baf501f726f
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Translation Data Type (ASSL)
  Defines a primitive data type that represents a localized translation.  
  
## Syntax  
  
```xml  
  
<Translation>  
   <Language>...</Language>  
   <Caption>...</Caption>  
   <Description>...</Description>  
   <DisplayFolder>...</DisplayFolder>  
   <Annotations>...</Annotations>  
</Translation>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[AttributeTranslation](../../../2014/analysis-services/dev-guide/attributetranslation-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Caption](../../../2014/analysis-services/dev-guide/caption-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [DisplayFolder](../../../2014/analysis-services/dev-guide/displayfolder-element-assl.md), [Language](../../../2014/analysis-services/dev-guide/language-element-assl.md)|  
|Derived elements|[AllMemberTranslation](../../../2014/analysis-services/dev-guide/allmembertranslation-element-assl.md), [AttributeAllMemberTranslation](../../../2014/analysis-services/dev-guide/attributeallmembertranslation-element-assl.md), [NamingTemplateTranslation](../../../2014/analysis-services/dev-guide/namingtemplatetranslation-element-assl.md), [UnknownMemberTranslation](../../../2014/analysis-services/dev-guide/unknownmembertranslation-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Translation>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  