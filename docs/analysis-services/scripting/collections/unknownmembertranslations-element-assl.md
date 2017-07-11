---
title: "UnknownMemberTranslations Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "UnknownMemberTranslations Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "UnknownMemberTranslations"
helpviewer_keywords: 
  - "UnknownMemberTranslations element"
ms.assetid: 72920843-2d43-4ff4-b38e-19c9a7451cb2
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# UnknownMemberTranslations Element (ASSL)
  Contains the collection of translations for the caption of the [UnknownMember](../../../analysis-services/scripting/properties/unknownmember-element-assl.md) element of a dimension.  
  
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
|Parent elements|[Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md)|  
|Child elements|[UnknownMemberTranslation](../../../analysis-services/scripting/objects/unknownmembertranslation-element-assl.md) of type [Translation](../../../analysis-services/scripting/data-type/translation-data-type-assl.md)|  
  
## Remarks  
 The element that corresponds to the parent of **UnknownMemberTranslations** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Translation Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/translation-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  