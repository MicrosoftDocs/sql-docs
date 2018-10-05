---
title: "DISCOVER_INSTANCES Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DISCOVER_INSTANCES"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DISCOVER_INSTANCES rowset"
ms.assetid: e0842e63-089d-468d-869f-634da343d9fb
author: minewiskan
ms.author: owend
manager: craigg
---
# DISCOVER_INSTANCES Rowset
  Describes the instances on the server.  
  
## Rowset Columns  
 The `DISCOVER_INSTANCES` rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|`INSTANCE_NAME`|`DBTYPE_WSTR`||The name of the instance.|  
|`INSTANCE_PORT_NUMBER`|`DBTYPE_I4`||The port number the instance listens on.|  
|`INSTANCE_STATE`|`DBTYPE_I4`||The state of the server instance:<br /><br /> -   `Started`<br />-   `Stopped`<br />-   `Starting`<br />-   `Stopping`<br />-   `Paused`|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The `DISCOVER_INSTANCES` rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|`INSTANCE_NAME`|`DBTYPE_WSTR`|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](ole-db-for-olap-schema-rowsets.md)  
  
  
