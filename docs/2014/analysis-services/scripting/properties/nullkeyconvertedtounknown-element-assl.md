---
title: "NullKeyConvertedToUnknown Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "NullKeyConvertedToUnknown Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "NullKeyConvertedToUnknown"
helpviewer_keywords: 
  - "NullKeyConvertedToUnknown element"
ms.assetid: 1a6cde33-01ba-4095-b464-16d1ad3c6905
author: minewiskan
ms.author: owend
manager: craigg
---
# NullKeyConvertedToUnknown Element (ASSL)
  Specifies the action to be taken when a null conversion error is encountered.  
  
## Syntax  
  
```xml  
  
<ErrorConfiguration>  
   ...  
      <NullKeyConvertedToUnknown>...</NullKeyConvertedToUnknown>  
   ...  
</ErrorConfiguration>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*IgnoreError*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ErrorConfiguration](../objects/errorconfiguration-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Null conversion errors occur when a null value is encountered in a key column and interpreted as the `Unknown` member. However, this error occurs only if the [NullProcessing](nullprocessing-element-assl.md) element for the [DataItem](../data-type/dataitem-data-type-assl.md) ancestor of the `ErrorConfiguration` parent element is set to *UnknownMember*.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*IgnoreError*|Processing ignores the error and continues.|  
|*ReportAndContinue*|Processing reports the error and continues.|  
|*ReportAndStop*|Processing reports the error and stops.|  
  
 The enumeration that corresponds to the allowed values for `NullKeyConvertedToUnknown` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ErrorOption>.  
  
## See Also  
 [ErrorConfiguration Element &#40;ASSL&#41;](../objects/errorconfiguration-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
