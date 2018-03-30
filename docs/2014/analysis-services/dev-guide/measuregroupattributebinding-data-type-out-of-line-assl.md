---
title: "MeasureGroupAttributeBinding Data Type (out-of-line) (ASSL) | Microsoft Docs"
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
  - "MeasureGroupAttributeBinding Data Type (out-of-line)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "MeasureGroupAttributeBinding data type"
ms.assetid: bfe09a95-4e04-4f93-9389-7dd0b4c8f5e4
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MeasureGroupAttributeBinding Data Type (out-of-line) (ASSL)
  Defines a derived data type that represents an out-of-line binding for an attribute in a dimension included in a measure group.  
  
## Syntax  
  
```xml  
  
<MeasureGroupAttributeBinding>  
   <!-- The following elements extend Binding -->  
   <DatabaseID>...</DatabaseID>  
   <CubeID>...</CubeID>  
   <MeasureGroupID>...</MeasureGroupID>  
   <GranularityAttributeID>...</GranularityAttributeID>  
   <Source>...</Source>  
</MeasureGroupAttributeBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[CubeID](../../../2014/analysis-services/dev-guide/cubeid-element-assl.md), [DatabaseID](../../../2014/analysis-services/dev-guide/databaseid-element-xmla.md), [MeasureGroupID](../../../2014/analysis-services/dev-guide/measuregroupid-element-assl.md), [GranularityAttributeID](../../../2014/analysis-services/dev-guide/granularityattributeid-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|  
|Derived elements|[Binding](../../../2014/analysis-services/dev-guide/binding-element-xmla.md) ([Bindings](../../../2014/analysis-services/dev-guide/attributes-element-assl.md) collection of XML for Analysis (XMLA) [Batch](../../../2014/analysis-services/dev-guide/batch-element-xmla.md) and [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md) commands)|  
  
## Remarks  
 For more information about out-of-line bindings, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../2014/analysis-services/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  