---
title: "RelationalDataSource Data Type (ASSL) | Microsoft Docs"
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
  - "RelationalDataSource Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "RelationalDataSource"
helpviewer_keywords: 
  - "RelationalDataSource data type"
ms.assetid: 2b99d7d0-731d-4506-8c37-678a5dc29c8a
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# RelationalDataSource Data Type (ASSL)
  Defines a derived data type that represents a [DataSource](../../../analysis-services/scripting/objects/datasource-element-assl.md) element based on a relational data source.  
  
## Syntax  
  
```xml  
  
<RelationalDataSource>  
   <!-- Child elements are only those inherited from DataSource -->  
</RelationalDataSource>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[DataSource](../../../analysis-services/scripting/data-type/datasource-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|[DataSource](../../../analysis-services/scripting/objects/datasource-element-assl.md) ([DataSources](../../../analysis-services/scripting/collections/datasources-element-assl.md) collection of [Database](../../../analysis-services/scripting/objects/database-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RelationalDataSource>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  