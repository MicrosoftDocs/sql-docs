---
title: "MeasureGroupAttribute Data Type (ASSL) | Microsoft Docs"
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
  - "MeasureGroupAttribute Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureGroupAttribute"
helpviewer_keywords: 
  - "MeasureGroupAttribute data type"
ms.assetid: dc7f71e6-3755-4d99-9fcd-5830e10eb653
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MeasureGroupAttribute Data Type (ASSL)
  Defines a primitive data type that represents the relationship between an attribute and a measure group.  
  
## Syntax  
  
```xml  
  
<MeasureGroupAttribute>  
   <AttributeID>...</AttributeID>  
      <KeyColumns>...</KeyColumns>  
   <Type>...</Type>  
   <Annotations>...</Annotations>  
</MeasureGroupAttribute>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [AttributeID](../../../2014/analysis-services/dev-guide/attributeid-element-assl.md), [KeyColumns](../../../2014/analysis-services/dev-guide/keycolumns-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-measuregroupattribute-assl.md)|  
|Derived elements|[Attribute](../../../2014/analysis-services/dev-guide/attribute-element-assl.md) ([Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md) collection of [RegularMeasureGroupDimension](../../../2014/analysis-services/dev-guide/regularmeasuregroupdimension-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroupAttribute>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  