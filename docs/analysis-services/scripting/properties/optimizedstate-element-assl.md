---
title: "OptimizedState Element (ASSL) | Microsoft Docs"
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
  - "OptimizedState Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "OptimizedState"
helpviewer_keywords: 
  - "OptimizedState element"
ms.assetid: 120dcc4c-8fe8-4471-bbd6-99ad534364f0
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# OptimizedState Element (ASSL)
  Determines the level of optimization that is applied to the hierarchy.  
  
## Syntax  
  
```xml  
  
<CubeHierarchy>  
   ...  
   <OptimizedState>...</OptimizedState>  
   ...  
</CubeHierarchy>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*FullyOptimized*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CubeHierarchy](../../../analysis-services/scripting/data-type/cubehierarchy-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*FullyOptimized*|The instance builds indexes for the hierarchy to improve query performance.|  
|*NotOptimized*|The instance does not build additional indexes.|  
  
 The enumeration that corresponds to the allowed values for **OptimizedState** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.OptimizationType>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  