---
title: "DISCOVER_INSTANCES Rowset | Microsoft Docs"
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
  - "DISCOVER_INSTANCES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_INSTANCES rowset"
ms.assetid: e0842e63-089d-468d-869f-634da343d9fb
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_INSTANCES Rowset
  Describes the instances on the server.  
  
## Rowset Columns  
 The **DISCOVER_INSTANCES** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**INSTANCE_NAME**|**DBTYPE_WSTR**||The name of the instance.|  
|**INSTANCE_PORT_NUMBER**|**DBTYPE_I4**||The port number the instance listens on.|  
|**INSTANCE_STATE**|**DBTYPE_I4**||The state of the server instance:<br /><br /> **Started**<br /><br /> **Stopped**<br /><br /> **Starting**<br /><br /> **Stopping**<br /><br /> **Paused**|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_INSTANCES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**INSTANCE_NAME**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  