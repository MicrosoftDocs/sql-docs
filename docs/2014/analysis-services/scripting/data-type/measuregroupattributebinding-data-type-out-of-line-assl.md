---
title: "MeasureGroupAttributeBinding Data Type (out-of-line) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
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
|Base data types|[Binding](binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[CubeID](../properties/id-element-assl.md), [DatabaseID](../../xmla/xml-elements-properties/id-element-xmla.md), [MeasureGroupID](../properties/measuregroupid-element-assl.md), [GranularityAttributeID](../properties/attributeid-element-assl.md), [Source](../properties/source-element-binding-assl.md)|  
|Derived elements|[Binding](../../xmla/xml-elements-properties/binding-element-xmla.md) ([Bindings](../collections/attributes-element-assl.md) collection of XML for Analysis (XMLA) [Batch](../../xmla/xml-elements-commands/batch-element-xmla.md) and [Process](../../xmla/xml-elements-commands/process-element-xmla.md) commands)|  
  
## Remarks  
 For more information about out-of-line bindings, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
