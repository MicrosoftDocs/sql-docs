---
title: "AttributeAllMemberTranslation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AttributeAllMemberTranslation Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AttributeAllMemberTranslation"
helpviewer_keywords: 
  - "AttributeAllMemberTranslation element"
ms.assetid: 4b0c61dd-6666-4bf4-9b23-c9d8e315c414
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributeAllMemberTranslation Element (ASSL)
  Contains a translation for the caption of the `All` member of a [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AttributeAllMemberTranslations>  
   <AttributeAllMemberTranslation xsi:type="Translation">...</AttributeAllMemberTranslation>  
</AttributeAllMemberTranslations>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Translation](../data-type/translation-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AttributeAllMemberTranslations](../collections/translations-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of the `AttributeAllMemberTranslations` collection in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Translation Element &#40;ASSL&#41;](translation-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
