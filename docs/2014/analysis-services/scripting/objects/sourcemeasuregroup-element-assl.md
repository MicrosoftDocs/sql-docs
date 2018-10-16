---
title: "SourceMeasureGroup Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "SourceMeasureGroup Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "SourceMeasureGroup"
helpviewer_keywords: 
  - "SourceMeasureGroup element"
ms.assetid: aaa7cc0b-162a-4c31-ab03-a90f81eeca00
author: minewiskan
ms.author: owend
manager: craigg
---
# SourceMeasureGroup Element (ASSL)
  Identifies the measure group that serves as the data source for a mining structure column.  
  
## Syntax  
  
```xml  
  
<MiningStructureColumn xsi:type="TableMiningStructureColumn">  
   ...  
   <SourceMeasureGroup xsi:type="MeasureGroupBinding">...</SourceMeasureGroup>  
   ...  
</MiningStructureColumn>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[MeasureGroupBinding](../data-type/binding-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md) of type [TableMiningStructureColumn](../data-type/tableminingstructurecolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For more information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../data-type/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The elements that correspond to the parents of `SourceMeasureGroup` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.MiningStructureColumn> and <xref:Microsoft.AnalysisServices.TableMiningStructureColumn>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
