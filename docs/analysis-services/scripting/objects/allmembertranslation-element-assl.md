---
title: "AllMemberTranslation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AllMemberTranslation Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "AllMemberTranslation"
helpviewer_keywords: 
  - "AllMemberTranslation element"
ms.assetid: 31ec0c44-8f1d-457c-9e8b-61dd5bc468f7
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AllMemberTranslation Element (ASSL)
  Contains a translation for the caption of the All member of a [Hierarchy](../../../analysis-services/scripting/objects/hierarchy-element-assl.md) element.  
  
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
|Data type and length|[Translation](../../../analysis-services/scripting/objects/translation-element-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AllMemberTranslations](../../../analysis-services/scripting/collections/allmembertranslations-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of the **AllMemberTranslations** collection in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Hierarchy>.  
  
## See Also  
 [Translation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/translation-element-assl.md)   
 [Hierarchy Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/hierarchy-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  