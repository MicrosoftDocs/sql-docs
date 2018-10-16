---
title: "UnknownMemberTranslations Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# UnknownMemberTranslations Element (ASSL)
  Contains the collection of translations for the caption of the [UnknownMember](../objects/member-element-assl.md) element of a dimension.  
  
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
|Parent elements|[Dimension](../objects/dimension-element-assl.md)|  
|Child elements|[UnknownMemberTranslation](../objects/translation-element-assl.md) of type [Translation](../data-type/translation-data-type-assl.md)|  
  
## Remarks  
 The element that corresponds to the parent of `UnknownMemberTranslations` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Translation Data Type &#40;ASSL&#41;](../data-type/translation-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
