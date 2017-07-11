---
title: "EstimatedSize Element (ASSL) | Microsoft Docs"
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
  - "EstimatedSize Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "EstimatedSize"
helpviewer_keywords: 
  - "EstimatedSize element"
ms.assetid: a9c63a22-d424-4f27-a186-5372f7b0224d
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# EstimatedSize Element (ASSL)
  Contains the read-only estimated size, in bytes, of the parent element.  
  
## Syntax  
  
```xml  
  
<Database> <!-- or MeasureGroup, Partition -->  
   ...  
   <EstimatedSize>...</EstimatedSize>  
   ...  
</Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Long|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](../../../analysis-services/scripting/objects/database-element-assl.md), [MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md), [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements corresponding to the parents of  in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Database>, <xref:Microsoft.AnalysisServices.MeasureGroup>, and <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  