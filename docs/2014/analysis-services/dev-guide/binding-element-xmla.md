---
title: "Binding Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Binding Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.binding"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Binding"
  - "urn:schemas-microsoft-com:xml-analysis#Binding"
helpviewer_keywords: 
  - "Binding element"
ms.assetid: d5acd8d4-8551-449a-ae30-d0ba828cc02d
caps.latest.revision: 13
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Binding Element (XMLA)
  Defines an out-of-line binding for an [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object, such as an attribute in a dimension, for the [Bindings](../../../2014/analysis-services/dev-guide/bindings-element-xmla.md) collection of a [Batch](../../../2014/analysis-services/dev-guide/batch-element-xmla.md) or [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Bindings>  
   <Binding xsi:type="DimensionAttributeBinding">...</Binding>  
   <!-- or -->  
   <Binding xsi:type="MeasureGroupAttributeBinding">...</Binding>  
   <!-- or -->  
   <Binding xsi:type="PartitionBinding">...</Binding>  
</Bindings>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DimensionAttributeBinding](../../../2014/analysis-services/dev-guide/dimensionattributebinding-data-type-out-of-line-assl.md), [MeasureGroupAttributeBinding](../../../2014/analysis-services/dev-guide/measuregroupattributebinding-data-type-out-of-line-assl.md), [PartitionBinding](../../../2014/analysis-services/dev-guide/partitionbinding-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Bindings](../../../2014/analysis-services/dev-guide/bindings-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 `Binding` elements define out-of-line bindings, other than data sources and data source views, for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects to be processed by a `Batch` or `Process` command. For more information about processing objects, see [Processing Objects &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/processing-objects-xmla.md).  
  
 For more information about out-of-line bindings, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  