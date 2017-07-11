---
title: "ConnectionString Element (ASSL) | Microsoft Docs"
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
  - "ConnectionString Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ConnectionString"
helpviewer_keywords: 
  - "ConnectionString element"
ms.assetid: f74181c4-7df7-4fbd-94dd-e4ad03dffe14
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ConnectionString Element (ASSL)
  Contains the encrypted connection string for a [DataSource](../../../analysis-services/scripting/objects/datasource-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataSource>  
   ...  
   <ConnectionString>...</ConnectionString>  
   ...  
</DataSource>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataSource](../../../analysis-services/scripting/objects/datasource-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **ConnectionString** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSource>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  