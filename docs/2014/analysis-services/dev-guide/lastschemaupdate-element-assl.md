---
title: "LastSchemaUpdate Element (ASSL) | Microsoft Docs"
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
  - "LastSchemaUpdate Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "LastSchemaUpdate"
helpviewer_keywords: 
  - "LastSchemaUpdate element"
ms.assetid: 0634c105-91cc-4882-87be-97ca29a251a6
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# LastSchemaUpdate Element (ASSL)
  Contains the read-only metadata update timestamp of the parent element.  
  
## Syntax  
  
```xml  
  
<Assembly> <!-- or one of the elements that are listed in the Element Relationships table -->  
   ...  
   <LastSchemaUpdate>...</LastSchemaUpdate>  
   ...  
</Assembly>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|DateTime|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Assembly](../../../2014/analysis-services/dev-guide/assembly-element-assl.md), [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md), [DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md), [DataSourceView](../../../2014/analysis-services/dev-guide/datasourceview-element-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [MdxScript](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md), [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md), [Permission](../../../2014/analysis-services/dev-guide/permission-data-type-assl.md), [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `LastSchemaUpdate` element contains a read-only `DateTime` value that represents the date and time that the metadata for an object was changed on a given instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 The elements that correspond to the parents of `LastSchemaUpdate` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Assembly>, <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.Database>, <xref:Microsoft.AnalysisServices.DataSource>, <xref:Microsoft.AnalysisServices.DataSourceView>, <xref:Microsoft.AnalysisServices.Dimension>, <xref:Microsoft.AnalysisServices.MdxScript>, <xref:Microsoft.AnalysisServices.MeasureGroup>, <xref:Microsoft.AnalysisServices.MiningModel>, <xref:Microsoft.AnalysisServices.MiningStructure>, <xref:Microsoft.AnalysisServices.Partition>, <xref:Microsoft.AnalysisServices.Permission>, and <xref:Microsoft.AnalysisServices.Perspective>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  