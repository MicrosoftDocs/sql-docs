---
title: "Type Element (Partition) (ASSL) | Microsoft Docs"
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
  - "Type Element (Partition)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: 61c022fe-8c41-4f62-9808-c386e05eb547
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Type Element (Partition) (ASSL)
  Contains the type of the [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Partition>  
      ...  
   <Type>...</Type>  
   ...  
</Partition>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Data*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Partition](../../../analysis-services/scripting/objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Data*|The partition contains fact table data.|  
|*Writeback*|The partition contains writeback table data.|  
  
 The enumeration that corresponds to the allowed values for **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PartitionType>.  
  
 The element that corresponds to the parent of **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  