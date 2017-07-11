---
title: "KeyErrorLimit Element (ASSL) | Microsoft Docs"
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
  - "KeyErrorLimit Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "KeyErrorLimit"
helpviewer_keywords: 
  - "KeyErrorLimit element"
ms.assetid: c91d3bd8-2ad7-416f-a860-2599e4a4dbee
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# KeyErrorLimit Element (ASSL)
  Contains the number of acceptable errors during processing.  
  
## Syntax  
  
```xml  
  
<ErrorConfiguration>  
   ...  
      <KeyErrorLimit>...</KeyErrorLimit>  
   ...  
</ErrorConfiguration>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Long|  
|Default value|**0**|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ErrorConfiguration](../../../analysis-services/scripting/objects/errorconfiguration-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **KeyErrorLimit** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ErrorConfiguration>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  