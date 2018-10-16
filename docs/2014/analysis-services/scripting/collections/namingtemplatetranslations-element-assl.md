---
title: "NamingTemplateTranslations Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "NamingTemplateTranslations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "NamingTemplateTranslations"
helpviewer_keywords: 
  - "NamingTemplateTranslations element"
ms.assetid: fde65778-1fa3-490a-9874-8bf2052ef25c
author: minewiskan
ms.author: owend
manager: craigg
---
# NamingTemplateTranslations Element (ASSL)
  Provides a collection of localized translations for the [NamingTemplate](../properties/namingtemplate-element-assl.md) element of the parent, [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md).  
  
## Syntax  
  
```xml  
  
<Attribute xsi:type="DimensionAttribute">  
   ...  
   <NamingTemplateTranslations>  
            <NamingTemplateTranslation xsi:type="Translation">...</NamingTemplateTranslation>  
...</NamingTemplateTranslations>  
   ...  
</Attribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](../objects/attribute-element-assl.md) of type [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|[NamingTemplateTranslation](../objects/translation-element-assl.md) of type [Translation](translations-element-assl.md)|  
  
## Remarks  
 The value of the `NamingTemplateTranslation` element is used only by parent attributes (in other words, the value of the [Usage](../properties/usage-element-dimensionattribute-assl.md) element of the parent `DimensionAttribute` is set to *Parent*.)  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
