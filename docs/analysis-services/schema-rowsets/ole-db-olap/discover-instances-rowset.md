---
title: "DISCOVER_INSTANCES Rowset | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: schema-rowsets
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DISCOVER_INSTANCES Rowset
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
  
  
