---
title: "NullKeyNotAllowed Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "NullKeyNotAllowed Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "NullKeyNotAllowed"
helpviewer_keywords: 
  - "NullKeyNotAllowed element"
ms.assetid: 4ece99eb-954b-4da1-add4-dd9efd5fff0a
author: minewiskan
ms.author: owend
manager: craigg
---
# NullKeyNotAllowed Element (ASSL)
  Determines how the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] processing engine handles a null key error encountered during processing.  
  
## Syntax  
  
```xml  
  
<ErrorConfiguration>  
   ...  
   <NullKeyNotAllowed>...</NullKeyNotAllowed>  
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
|Parent element|[ErrorConfiguration](../objects/errorconfiguration-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Null key errors occur when a null value is encountered in a key column in which null values are not allowed, forcing the record to be discarded during processing. However, this error occurs only if the [NullProcessing](nullprocessing-element-assl.md) element for the `DataItem` ancestor of the `ErrorConfiguration` parent element is set to *Error*.  
  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*IgnoreError*|Processing ignores the error and continues.|  
|*ReportAndContinue*|Processing reports the error and continues.|  
|*ReportAndStop*|Processing reports the error and stops.|  
  
 The enumeration that corresponds to the allowed values for `NullKeyNotAllowed` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ErrorOption>.  
  
## See Also  
 [ErrorConfiguration Element &#40;ASSL&#41;](../objects/errorconfiguration-element-assl.md)  
  
  
