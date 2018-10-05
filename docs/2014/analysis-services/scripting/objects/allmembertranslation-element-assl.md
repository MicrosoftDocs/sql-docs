---
title: "AllMemberTranslation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AllMemberTranslation Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AllMemberTranslation"
helpviewer_keywords: 
  - "AllMemberTranslation element"
ms.assetid: 31ec0c44-8f1d-457c-9e8b-61dd5bc468f7
author: minewiskan
ms.author: owend
manager: craigg
---
# AllMemberTranslation Element (ASSL)
  Contains a translation for the caption of the All member of a [Hierarchy](hierarchy-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AllMemberTranslations>  
   <AllMemberTranslation xsi:type="Translation">...  
   </AllMemberTranslation>  
</AllMemberTranslations>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Translation](translation-element-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AllMemberTranslations](../collections/translations-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of the `AllMemberTranslations` collection in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Hierarchy>.  
  
## See Also  
 [Translation Element &#40;ASSL&#41;](translation-element-assl.md)   
 [Hierarchy Element &#40;ASSL&#41;](hierarchy-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
