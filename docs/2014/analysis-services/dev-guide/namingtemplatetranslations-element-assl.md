---
title: "NamingTemplateTranslations Element (ASSL) | Microsoft Docs"
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
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# NamingTemplateTranslations Element (ASSL)
  Provides a collection of localized translations for the [NamingTemplate](../../../2014/analysis-services/dev-guide/namingtemplate-element-assl.md) element of the parent, [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md).  
  
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
|Parent elements|[Attribute](../../../2014/analysis-services/dev-guide/attribute-element-assl.md) of type [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|  
|Child elements|[NamingTemplateTranslation](../../../2014/analysis-services/dev-guide/namingtemplatetranslation-element-assl.md) of type [Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md)|  
  
## Remarks  
 The value of the `NamingTemplateTranslation` element is used only by parent attributes (in other words, the value of the [Usage](../../../2014/analysis-services/dev-guide/usage-element-dimensionattribute-assl.md) element of the parent `DimensionAttribute` is set to *Parent*.)  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  