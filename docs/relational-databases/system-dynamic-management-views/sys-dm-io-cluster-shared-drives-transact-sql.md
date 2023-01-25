---
title: "sys.dm_io_cluster_shared_drives (Transact-SQL)"
description: sys.dm_io_cluster_shared_drives (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_io_cluster_shared_drives_TSQL"
  - "sys.dm_io_cluster_shared_drives"
  - "dm_io_cluster_shared_drives_TSQL"
  - "dm_io_cluster_shared_drives"
helpviewer_keywords:
  - "sys.dm_io_cluster_shared_drives dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: c8fcced8-c780-49dc-99bd-6beb3ca532c4
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_io_cluster_shared_drives (Transact-SQL)
[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  This view returns the drive name of each of the shared drives if the current server instance is a clustered server. If the current server instance is not a clustered instance it returns an empty rowset.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_io_cluster_shared_drives**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**DriveName**|**nchar(2)**|The name of the drive (the drive letter) that represents an individual disk taking part in the cluster shared disk array. Column is not nullable.|  
|**pdw_node_id**|**int**|**Applies to**: ssPDW<br /><br /> The identifier for the node that this distribution is on.|  
  
## Remarks  
 When clustering is enabled, the failover cluster instance requires data and log files to be on shared disks so that they may be accessed after the instance fails over to another node. Each of the rows in this view represents a single shared disk which is used by this clustered [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Only disks listed by this view can be used to store data or log files for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The disks listed in this view are those that are in the cluster resource group associated with the instance.  
  
> [!NOTE]  
>  This view will be deprecated in a future release. We recommend that you use [sys.dm_io_cluster_valid_path_names &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-cluster-valid-path-names-transact-sql.md) instead.  
  
## Permissions  
 The user must have VIEW SERVER STATE permission for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
## Examples  
 The following example uses sys.dm_io_cluster_shared_drives to determine the shared drives on a clustered server instance:  
  
```  
SELECT * FROM sys.dm_io_cluster_shared_drives;  
```  
  
 This is the result set:  
  
 DriveName  
  
 --------\-  
  
 m  
  
 n  
  
## See Also  
 [sys.dm_io_cluster_valid_path_names &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-cluster-valid-path-names-transact-sql.md)   
 [sys.dm_os_cluster_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-cluster-nodes-transact-sql.md)   
 [sys.fn_servershareddrives &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-servershareddrives-transact-sql.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
  
  
