---
title: "TableMiningStructureColumn Data Type (ASSL) | Microsoft Docs"
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
  - "TableMiningStructureColumn Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TableMiningStructureColumn"
helpviewer_keywords: 
  - "TableMiningStructureColumn data type"
ms.assetid: 350358b0-f2fc-43c3-957d-884c59fa879e
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# TableMiningStructureColumn Data Type (ASSL)
  Defines a derived data type that represents a [MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md) element that contains nested tables, as opposed to the scalar values associated with the [ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md) element that contains scalar values.  
  
## Syntax  
  
```xml  
  
<TableMiningStructureColumn>  
   <!-- The following elements extend MiningStructureColumn -->  
   <ForeignKeyColumn>..</ForeignKeyColumn>  
   <SourceMeasureGroup>..</SourceMeasureGroup>  
   <Columns>..</Columns>  
   <Translations>..</Translations>  
</TableMiningStructureColumn>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md), [ForeignKeyColumn](../../../2014/analysis-services/dev-guide/foreignkeycolumn-element-assl.md), [SourceMeasureGroup](../../../2014/analysis-services/dev-guide/sourcemeasuregroup-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
|Derived elements|[Column](../../../2014/analysis-services/dev-guide/column-element-assl.md) ([Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md) collection of [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TableMiningStructureColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  