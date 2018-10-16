---
title: "CaptionColumn Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CaptionColumn Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CaptionColumn"
helpviewer_keywords: 
  - "CaptionColumn element"
ms.assetid: bdb1b9b8-b5d5-4d91-81c7-8de8635bbb83
author: minewiskan
ms.author: owend
manager: craigg
---
# CaptionColumn Element (ASSL)
  Defines the column that provides the caption for the attribute.  
  
## Syntax  
  
```xml  
  
<AttributeTranslation>  
   ...  
   <CaptionColumn xsi:type="DataItem">...</CaptionColumn>  
   ...  
</AttributeTranslation>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataItem](../data-type/dataitem-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AttributeTranslation](../data-type/translation-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For more information about the `DataItem` type, including a table of Analysis Services Scripting Language (ASSL) objects and properties of the `DataItem` type, see [DataItem Data Type &#40;ASSL&#41;](../data-type/dataitem-data-type-assl.md).  
  
 The element that corresponds to the parent of `CaptionColumn` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeTranslation>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
