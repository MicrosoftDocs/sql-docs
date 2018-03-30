---
title: "Translation Element (XMLA) | Microsoft Docs"
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
  - "Translation Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Translation"
  - "microsoft.xml.analysis.translation"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Translation"
helpviewer_keywords: 
  - "Translation element"
ms.assetid: ce962d4b-dda9-4a16-a56c-ff7a5275c48a
caps.latest.revision: 12
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Translation Element (XMLA)
  Defines a translation for an attribute member.  
  
## Syntax  
  
```xml  
  
<Translations>  
   ...  
   <Translation>  
      <Language>...</Language>  
      <Name>...</Name>  
   </Translation>  
   ...  
</Translations>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Translations](../../../2014/analysis-services/dev-guide/translations-element-xmla.md)|  
|Child elements|[Language](../../../2014/analysis-services/dev-guide/language-element-xmla.md), [Name](../../../2014/analysis-services/dev-guide/name-element-xmla.md)|  
  
## Remarks  
 A `Translation` element defines the information needed to associate an attribute member to a translation defined for a given attribute during an [Insert](../../../2014/analysis-services/dev-guide/insert-element-xmla.md) or [Update](../../../2014/analysis-services/dev-guide/update-element-xmla.md) command.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  