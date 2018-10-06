---
title: "ColumnID Element (ColumnBinding) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ColumnID Element (ColumnBinding)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ColumnID"
helpviewer_keywords: 
  - "ColumnID element"
ms.assetid: f4edf532-7e40-4ee2-9b5e-48b3c3de7a74
author: minewiskan
ms.author: owend
manager: craigg
---
# ColumnID Element (ColumnBinding) (ASSL)
  Contains the identifier (ID) of the column within the table to which the data item is bound.  
  
## Syntax  
  
```xml  
  
<ColumnBinding>  
   ...  
   <ColumnID>...</ColumnID>  
   ...  
</ColumnBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ColumnBinding](../data-type/binding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `ColumnID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ColumnBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
