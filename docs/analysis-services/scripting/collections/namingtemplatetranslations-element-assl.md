---
title: "NamingTemplateTranslations Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "NamingTemplateTranslations Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "NamingTemplateTranslations"
helpviewer_keywords: 
  - "NamingTemplateTranslations element"
ms.assetid: fde65778-1fa3-490a-9874-8bf2052ef25c
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# NamingTemplateTranslations Element (ASSL)
  Provides a collection of localized translations for the [NamingTemplate](../../../analysis-services/scripting/properties/namingtemplate-element-assl.md) element of the parent, [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md).  
  
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
|Parent elements|[Attribute](../../../analysis-services/scripting/objects/attribute-element-assl.md) of type [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|[NamingTemplateTranslation](../../../analysis-services/scripting/objects/namingtemplatetranslation-element-assl.md) of type [Translation](../../../analysis-services/scripting/objects/translation-element-assl.md)|  
  
## Remarks  
 The value of the **NamingTemplateTranslation** element is used only by parent attributes (in other words, the value of the [Usage](../../../analysis-services/scripting/properties/usage-element-dimensionattribute-assl.md) element of the parent **DimensionAttribute** is set to *Parent*.)  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  