---
title: "UnknownMemberTranslation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "UnknownMemberTranslation Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "UnknownMemberTranslation"
helpviewer_keywords: 
  - "UnknownElementTranslation element"
ms.assetid: a4b8cdac-b065-4a44-b251-c5ac1cfe5e6f
author: minewiskan
ms.author: owend
manager: craigg
---
# UnknownMemberTranslation Element (ASSL)
  Contains a translation for the caption of the [UnknownMember](member-element-assl.md) element for a [Dimension](dimension-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<UnknownMemberTranslations>  
   <UnknownMemberTranslation xsi:type="Translation">...</UnknownMemberTranslation>  
</UnknownMemberTranslations>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Translation](../data-type/translation-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[UnknownMemberTranslations](../collections/translations-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `UnknownMemberTranslation` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Translation Element &#40;ASSL&#41;](translation-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
