---
title: "MasterDatasourceID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MasterDatasourceID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MasterDatasourceID"
helpviewer_keywords: 
  - "MasterDatasourceID element"
ms.assetid: a9cbd3a9-581f-4a08-93d8-e1eea8757ce9
author: minewiskan
ms.author: owend
manager: craigg
---
# MasterDatasourceID Element (ASSL)
  Contains the master data source identifier (ID) for a [Database](../objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Database>  
   ...  
   <MasterDatasourceID>...</MasterDatasourceID>  
   ...  
</Database>  
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
|Parent elements|[Database](../objects/database-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For databases on remote instances of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that contain remote partitions, the `MasterDatasourceID` element contains the data source ID of the data source used to identify the master instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that manages the remote partitions.  
  
 The element that corresponds to the parent of `MasterDatasourceID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Database>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
