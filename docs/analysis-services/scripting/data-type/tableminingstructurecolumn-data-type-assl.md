---
title: "TableMiningStructureColumn Data Type (ASSL) | Microsoft Docs"
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
  - "TableMiningStructureColumn Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "TableMiningStructureColumn"
helpviewer_keywords: 
  - "TableMiningStructureColumn data type"
ms.assetid: 350358b0-f2fc-43c3-957d-884c59fa879e
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# TableMiningStructureColumn Data Type (ASSL)
  Defines a derived data type that represents a [MiningStructureColumn](../../../analysis-services/scripting/data-type/miningstructurecolumn-data-type-assl.md) element that contains nested tables, as opposed to the scalar values associated with the [ScalarMiningStructureColumn](../../../analysis-services/scripting/data-type/scalarminingstructurecolumn-data-type-assl.md) element that contains scalar values.  
  
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
|Base data types|[MiningStructureColumn](../../../analysis-services/scripting/data-type/miningstructurecolumn-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Columns](../../../analysis-services/scripting/collections/columns-element-assl.md), [ForeignKeyColumn](../../../analysis-services/scripting/objects/foreignkeycolumn-element-assl.md), [SourceMeasureGroup](../../../analysis-services/scripting/objects/sourcemeasuregroup-element-assl.md), [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md)|  
|Derived elements|[Column](../../../analysis-services/scripting/objects/column-element-assl.md) ([Columns](../../../analysis-services/scripting/collections/columns-element-assl.md) collection of [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TableMiningStructureColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  