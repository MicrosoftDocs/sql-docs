---
title: "RemoteDatasourceID Element (ASSL) | Microsoft Docs"
ms.custom: ""
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
  - "RemoteDatasourceID Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "RemoteDatasourceID"
helpviewer_keywords: 
  - "RemoteDatasourceID element"
ms.assetid: 2eaf0b9c-8c2d-4dc6-9bad-1db70a4b04b1
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# RemoteDatasourceID Element (ASSL)
  Specifies the identifier (ID) of the OLAP data source that points to the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that stores the remote partition.  
  
## Syntax  
  
```xml  
  
<Partition>  
      ...  
   <RemoteDatasourceID>...</RemoteDatasourceID>  
   ...  
</Partition>  
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
|Parent element|[Partition](../../../analysis-services/scripting/objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If **RemoteDatasourceID** is null, then the partition is local.  
  
 The element that corresponds to the parent of **RemoteDatasourceID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  