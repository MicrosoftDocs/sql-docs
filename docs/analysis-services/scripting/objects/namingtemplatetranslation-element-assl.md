---
title: "NamingTemplateTranslation Element (ASSL) | Microsoft Docs"
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
  - "NamingTemplateTranslation Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "NamingTemplateTranslation"
helpviewer_keywords: 
  - "NamingTemplateTranslation element"
ms.assetid: 4a97a31d-23bc-4afd-a4dc-bc0ad7121f08
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# NamingTemplateTranslation Element (ASSL)
  Provides a localized translation of the [NamingTemplate](../../../analysis-services/scripting/properties/namingtemplate-element-assl.md) element for a parent [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md) data type.  
  
## Syntax  
  
```xml  
  
<NamingTemplateTranslations>  
   <NamingTemplateTranslation xsi:type="Translation">...</NamingTemplateTranslation>  
</NamingTemplateTranslations>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Translation](../../../analysis-services/scripting/objects/translation-element-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[NamingTemplateTranslations](../../../analysis-services/scripting/collections/namingtemplatetranslations-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **NamingTemplateTranslation** element is used only by parent attributes (in other words, the value of the [Usage](../../../analysis-services/scripting/properties/usage-element-dimensionattribute-assl.md) element of the **DimensionAttribute** parent is set to *Parent*) to store the localized translation of the **NamingTemplate** value for a given language.  
  
 The element that corresponds to the parent of **NamingTemplateTranslations** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [NamingTemplate Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/namingtemplate-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  