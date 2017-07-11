---
title: "MeasureGroupAttribute Data Type (ASSL) | Microsoft Docs"
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
  - "MeasureGroupAttribute Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MeasureGroupAttribute"
helpviewer_keywords: 
  - "MeasureGroupAttribute data type"
ms.assetid: dc7f71e6-3755-4d99-9fcd-5830e10eb653
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [AttributeID](../../../analysis-services/scripting/properties/attributeid-element-assl.md), [KeyColumns](../../../analysis-services/scripting/collections/keycolumns-element-assl.md), [Type](../../../analysis-services/scripting/properties/type-element-measuregroupattribute-assl.md)|  
|Derived elements|[Attribute](../../../analysis-services/scripting/objects/attribute-element-assl.md) ([Attributes](../../../analysis-services/scripting/collections/attributes-element-assl.md) collection of [RegularMeasureGroupDimension](../../../analysis-services/scripting/data-type/regularmeasuregroupdimension-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroupAttribute>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  