---
title: "AllMemberTranslations Element (ASSL) | Microsoft Docs"
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
  - "AllMemberTranslations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AllMemberTranslations"
helpviewer_keywords: 
  - "AllMemberTranslations element"
ms.assetid: 982ee2bf-c88d-4da5-a679-7a6b08a48a0d
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AllMemberTranslations Element (ASSL)
  Contains the collection of [Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md) elements for the caption of the All member of a [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Hierarchy>  
   ...  
   <AllMemberTranslations>  
      <AllMemberTranslation xsi:type="Translation">...  
            </AllMemberTranslation>  
   </AllMemberTranslations>  
      ...  
</Hierarchy>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None (collection)|  
|Default value|None (collection)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md)|  
|Child elements|[AllMemberTranslation](../../../2014/analysis-services/dev-guide/allmembertranslation-element-assl.md)|  
  
## Remarks  
 The element corresponding to the parent of `AllMemberTranslations` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Hierarchy>.  
  
## See Also  
 [Translation Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/translation-element-assl.md)   
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  