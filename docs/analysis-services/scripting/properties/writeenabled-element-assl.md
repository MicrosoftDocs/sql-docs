---
title: "WriteEnabled Element (ASSL) | Microsoft Docs"
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
  - "WriteEnabled Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "WriteEnabled"
helpviewer_keywords: 
  - "WriteEnabled element"
ms.assetid: 681290b3-ae8f-4659-9b17-a26d401a3fb0
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# WriteEnabled Element (ASSL)
  Indicates whether dimension writebacks are available (subject to security permissions).  
  
## Syntax  
  
```xml  
  
<Dimension>  
      ...  
   <WriteEnabled>...</WriteEnabled>  
   ...  
</Dimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **WriteEnabled** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  