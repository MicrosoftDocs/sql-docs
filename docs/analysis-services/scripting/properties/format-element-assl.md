---
title: "Format Element (ASSL) | Microsoft Docs"
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
  - "Format Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Format"
helpviewer_keywords: 
  - "Format element"
ms.assetid: 881ea707-52a7-46f7-ba16-ac2ec44eca22
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Format Element (ASSL)
  Contains the required format of the [DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataItem>  
   ...  
   <Format>...</Format>  
      ...  
</DataItem>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Allowed values for the **Format** element are Microsoft Office Excel formats, and the strings *TrimRight*, *TrimLeft*, *TrimAll*, and *TrimNone*. For trimming, *TrimRight* is the default.  
  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|Any Excel format string|Data is formatted according to the specified named or custom format string. Any format string that Excel supports can be supplied.|  
|*TrimAll*|Data is trimmed on the left and right.|  
|*TrimLeft*|Data is trimmed on the left.|  
|*TrimNone*|Data is not trimmed.|  
|*TrimRight*|Data is trimmed on the right.|  
  
 The element that corresponds to the parent of **Format** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataItem>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  