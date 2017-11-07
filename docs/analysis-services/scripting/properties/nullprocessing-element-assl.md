---
title: "NullProcessing Element (ASSL) | Microsoft Docs"
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
  - "NullProcessing Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "NullProcessing"
helpviewer_keywords: 
  - "NullProcessing element"
ms.assetid: 697be5c6-e9a6-4f74-9ff4-5f31400c2178
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent element|[DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Preserve*|Preserves the null value.<br /><br /> Note: This value is not supported for distinct count measures.|  
|*Error*|Raises a null key error. The value of [NullKeyNotAllowed](../../../analysis-services/scripting/properties/nullkeynotallowed-element-assl.md) determines how the instance reacts to the error.<br /><br /> Note: This value is not supported for measures.|  
|*UnknownMember*|Generates an unknown member and raises a null conversion error. The value of [NullKeyConvertedToUnknown](../../../analysis-services/scripting/properties/nullkeyconvertedtounknown-element-assl.md) determines how the instance reacts to the error.<br /><br /> Note: This value is not supported for columns associated with measures.|  
|*ZeroOrBlank*|Converts the null value to zero (for numeric data items) or a blank string (for string data items).<br /><br /> Note: This value provides compatibility with previous versions of [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|*Automatic*|Uses the default processing appropriate for the element:<br /><br /> *ZeroOrBlank* for OLAP data items.<br /><br /> *UnknownMember* for data mining data items.|  
  
 The enumeration that corresponds to the allowed values for **NullProcessing** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.NullProcessing>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  