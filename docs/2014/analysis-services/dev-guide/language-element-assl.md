---
title: "Language Element (ASSL) | Microsoft Docs"
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
  - "Language Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "LANGUAGE"
helpviewer_keywords: 
  - "Language element"
ms.assetid: 4d745d23-6b1f-4a85-97cf-d034cc41356f
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Language Element (ASSL)
  Contains the language identifier of the parent element.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Database, Dimension, MiningModel, MiningStructure, Translation -->  
   ...  
   <Language>...</Language>  
   ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|None|  
  
 **Cardinality**  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md)|1-1: Required element that occurs once and only once.|  
|All others|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `Language` element contains the default language identifier that is used by the parent element, or the specific language identifier for a `Translation` element. The language should be defined by using locale identifier (LCID) codes. For instance, LCID 1033 is used to indicate the English (U.S.) language.  
  
 The elements that correspond to the parents of the `Language` element in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.Database>, <xref:Microsoft.AnalysisServices.Dimension>, <xref:Microsoft.AnalysisServices.MiningModel>, <xref:Microsoft.AnalysisServices.MiningStructure>, and <xref:Microsoft.AnalysisServices.Translation>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  