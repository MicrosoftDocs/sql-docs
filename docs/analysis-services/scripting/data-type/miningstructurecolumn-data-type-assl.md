---
title: "MiningStructureColumn Data Type (ASSL) | Microsoft Docs"
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
  - "MiningStructureColumn Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MiningStructureColumn"
helpviewer_keywords: 
  - "MiningStructureColumn data type"
ms.assetid: b6d6e7a5-9c48-40c4-b147-8fcd5e429ae3
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MiningStructureColumn Data Type (ASSL)
  Defines an abstract primitive data type that represents information about a column in a [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningStructureColumn>  
   <Name>...</Name>  
   <ID>...</ID>  
   <Description>...</Description>  
   <Type>...</Type>  
   <Annotations>...</Annotations>  
</MiningStructureColumn>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[ScalarMiningStructureColumn](../../../analysis-services/scripting/data-type/scalarminingstructurecolumn-data-type-assl.md), [TableMiningStructureColumn](../../../analysis-services/scripting/data-type/tableminingstructurecolumn-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|None|  
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [ID](../../../analysis-services/scripting/properties/id-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md), [Type](../../../analysis-services/scripting/properties/type-element-miningstructurecolumn-assl.md)|  
|Derived elements|[Column](../../../analysis-services/scripting/objects/column-element-assl.md) ([Columns](../../../analysis-services/scripting/collections/columns-element-assl.md) collection of [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructureColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  