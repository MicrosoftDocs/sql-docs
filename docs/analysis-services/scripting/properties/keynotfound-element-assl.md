---
title: "KeyNotFound Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "KeyNotFound Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "KeyNotFound"
helpviewer_keywords: 
  - "KeyNotFound element"
ms.assetid: 2a93bbfa-2409-4e94-8b68-926532895a4c
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# KeyNotFound Element (ASSL)
  Specifies how [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] responds when it encounters a referential integrity error.  
  
## Syntax  
  
```xml  
  
<ErrorConfiguration>  
   ...  
      <KeyNotFound>...</KeyNotFound>  
   ...  
</ErrorConfiguration>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*ReportAndContinue*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ErrorConfiguration](../../../analysis-services/scripting/objects/errorconfiguration-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Referential integrity errors occur when a foreign key value in a dependent table does not have a corresponding entry in the parent table. This error occurs when [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] processes a dimension in which the fact table references a foreign key value that does not exist in the dimension table for that dimension, or when [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] processes a partition when the dimension main table for a dimension that is included in the partition references a key value that does not exist in another associated dimension table. (In the case of dimensions with parent-child hierarchies and parent attributes, this can also occur when the dimension main table for a dimension that is included in the partition references a key value that does not exist in the same dimension table.)  
  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*IgnoreError*|Processing should ignore the error and continue.|  
|*ReportAndContinue*|Processing should report the error and continue.|  
|*ReportAndStop*|Processing should report the error and stop.|  
  
 The enumeration that corresponds to the allowed values for **KeyNotFound** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ErrorOption>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  