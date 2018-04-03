---
title: "UnknownMemberTranslations Element (ASSL) | Microsoft Docs"
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
  - "UnknownMemberTranslations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "UnknownMemberTranslations"
helpviewer_keywords: 
  - "UnknownMemberTranslations element"
ms.assetid: 72920843-2d43-4ff4-b38e-19c9a7451cb2
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# UnknownMemberTranslations Element (ASSL)
  Contains the collection of translations for the caption of the [UnknownMember](../../../2014/analysis-services/dev-guide/unknownmember-element-assl.md) element of a dimension.  
  
## Syntax  
  
```xml  
  
<Dimension>  
   ...  
   <UnknownMemberTranslations>  
      <UnknownMemberTranslation xsi:type="Translation ">...</UnknownMemberTranslation>  
      </UnknownMemberTranslations>  
   ...  
</Dimension>  
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
|Parent elements|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md)|  
|Child elements|[UnknownMemberTranslation](../../../2014/analysis-services/dev-guide/unknownmembertranslation-element-assl.md) of type [Translation](../../../2014/analysis-services/dev-guide/translation-data-type-assl.md)|  
  
## Remarks  
 The element that corresponds to the parent of `UnknownMemberTranslations` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Translation Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/translation-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  