---
title: "NamingTemplateTranslation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "NamingTemplateTranslation Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "NamingTemplateTranslation"
helpviewer_keywords: 
  - "NamingTemplateTranslation element"
ms.assetid: 4a97a31d-23bc-4afd-a4dc-bc0ad7121f08
author: minewiskan
ms.author: owend
manager: craigg
---
# NamingTemplateTranslation Element (ASSL)
  Provides a localized translation of the [NamingTemplate](../properties/namingtemplate-element-assl.md) element for a parent [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md) data type.  
  
## Syntax  
  
```xml  
  
<NamingTemplateTranslations>  
   <NamingTemplateTranslation xsi:type="Translation">...</NamingTemplateTranslation>  
</NamingTemplateTranslations>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Translation](translation-element-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[NamingTemplateTranslations](../collections/translations-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `NamingTemplateTranslation` element is used only by parent attributes (in other words, the value of the [Usage](../properties/usage-element-dimensionattribute-assl.md) element of the `DimensionAttribute` parent is set to *Parent*) to store the localized translation of the `NamingTemplate` value for a given language.  
  
 The element that corresponds to the parent of `NamingTemplateTranslations` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [NamingTemplate Element &#40;ASSL&#41;](../properties/namingtemplate-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
