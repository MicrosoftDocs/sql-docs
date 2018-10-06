---
title: "StorageLocation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "StorageLocation Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "StorageLocation"
helpviewer_keywords: 
  - "StorageLocation element"
ms.assetid: ecf8852f-56a1-4fcf-b0d8-d7eebb75e4ed
author: minewiskan
ms.author: owend
manager: craigg
---
# StorageLocation Element (ASSL)
  Contains the file system storage location for the contents of the parent element.  
  
## Syntax  
  
```xml  
  
<Cube><!-- or MeasureGroup, Partition -->  
      ...  
   <StorageLocation>...</StorageLocation>  
   ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
|Ancestor or Parent|Default Value|  
|------------------------|-------------------|  
|[Cube](../objects/cube-element-assl.md)|None|  
|[MeasureGroup](../objects/group-element-assl.md)|Value of `StorageLocation` from the `Cube` parent element.|  
|[Partition](../objects/partition-element-assl.md)|Value of `StorageLocation` from the `MeasureGroup` parent element.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cube](../objects/cube-element-assl.md), [MeasureGroup](../objects/group-element-assl.md), [Partition](../objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `StorageLocation` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.MeasureGroup>, and <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
