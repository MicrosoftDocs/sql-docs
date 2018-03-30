---
title: "DataSource Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "DataSource Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataSource"
helpviewer_keywords: 
  - "DataSource element"
ms.assetid: 113fba1c-2679-4d06-9339-90a4a76f9b31
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DataSource Element (ASSL)
  Defines a data source in a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataSources>  
   <DataSource xsi:type="RelationalDataSource">...</DataSource>  
   <!-- or -->  
   <DataSource xsi:type="OlapDataSource">...</DataSource>  
   <!-- or -->  
   <DataSource xsi:type="PushedDataSource">...</DataSource>  
</DataSources>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[RelationalDataSource](../../../2014/analysis-services/dev-guide/relationaldatasource-data-type-assl.md), [OlapDataSource](../../../2014/analysis-services/dev-guide/olapdatasource-data-type-assl.md), [PushedDataSource](../../../2014/analysis-services/dev-guide/pusheddatasource-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DataSources](../../../2014/analysis-services/dev-guide/datasources-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSource>.  
  
## See Also  
 [Database Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/database-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  