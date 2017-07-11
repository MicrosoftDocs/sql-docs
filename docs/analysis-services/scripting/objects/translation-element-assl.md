---
title: "Translation Element (ASSL) | Microsoft Docs"
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
  - "Translation Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Translation"
helpviewer_keywords: 
  - "Translation element"
ms.assetid: fe715bab-050d-49e6-8ba6-801d0fa379a4
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Translation Element (ASSL)
  Provides a localized translation for the parent of the [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md) collection.  
  
## Syntax  
  
```xml  
  
<Translations>  
   <Translation xsi:type="Translation">...</Translation>  
   <!-- or -->  
   <Translation xsi:type="AttributeTranslation">...</Translation>  
</Translations>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Translation](../../../analysis-services/scripting/data-type/translation-data-type-assl.md), [AttributeTranslation](../../../analysis-services/scripting/data-type/attributetranslation-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Translations](../../../analysis-services/scripting/collections/translations-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Translation>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  