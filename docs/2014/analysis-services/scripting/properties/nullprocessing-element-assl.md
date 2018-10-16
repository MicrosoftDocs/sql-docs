---
title: "NullProcessing Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "NullProcessing Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "NullProcessing"
helpviewer_keywords: 
  - "NullProcessing element"
ms.assetid: 697be5c6-e9a6-4f74-9ff4-5f31400c2178
author: minewiskan
ms.author: owend
manager: craigg
---
# NullProcessing Element (ASSL)
  Defines how null values are processed.  
  
## Syntax  
  
```xml  
  
<DataItem>  
   ...  
   <NullProcessing>...</NullProcessing>  
   ...  
</DataItem>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Automatic*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataItem](../data-type/dataitem-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Preserve*|Preserves the null value. **Note:**  This value is not supported for distinct count measures.|  
|*Error*|Raises a null key error. The value of [NullKeyNotAllowed](nullkeynotallowed-element-assl.md) determines how the instance reacts to the error. **Note:**  This value is not supported for measures.|  
|*UnknownMember*|Generates an unknown member and raises a null conversion error. The value of [NullKeyConvertedToUnknown](nullkeyconvertedtounknown-element-assl.md) determines how the instance reacts to the error. **Note:**  This value is not supported for columns associated with measures.|  
|*ZeroOrBlank*|Converts the null value to zero (for numeric data items) or a blank string (for string data items). **Note:**  This value provides compatibility with previous versions of [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|*Automatic*|Uses the default processing appropriate for the element:<br /><br /> -   *ZeroOrBlank* for OLAP data items.<br />-   *UnknownMember* for data mining data items.|  
  
 The enumeration that corresponds to the allowed values for `NullProcessing` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.NullProcessing>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
