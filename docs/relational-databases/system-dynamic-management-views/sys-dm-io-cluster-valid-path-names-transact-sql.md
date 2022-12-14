---
title: "sys.dm_io_cluster_valid_path_names (Transact-SQL)"
description: sys.dm_io_cluster_valid_path_names (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_io_cluster_valid_path_names"
  - "dm_io_cluster_valid_path_names_TSQL"
  - "sys.dm_io_cluster_valid_path_names_TSQL"
  - "dm_io_cluster_valid_path_names"
helpviewer_keywords:
  - "dm_io_cluster_valid_path_names"
  - "sys.dm_io_cluster_valid_path_names"
  - "cluster valid path names"
  - "csv name"
  - "cluster shared volume names"
dev_langs:
  - "TSQL"
ms.assetid: 5bc8a0e5-6c72-425b-8c58-f276eb9add2c
---
# sys.dm_io_cluster_valid_path_names (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Returns information on all valid shared disks, including clustered shared volumes, for a SQL Server failover cluster instance. If the instance is not clustered, an empty rowset is returned.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**path_name**|**Nvarchar(512)**|Volume mount point or drive path that can be used as a root directory for database and log files. Is not nullable.|  
|**cluster_owner_node**|**Nvarchar(64)**|Current owner of the drive. For cluster shared volumes (CSV), the owner is the node which is hosting the MetaData Server. Is not nullable.|  
|**is_cluster_shared_volume**|**Bit**|Returns 1 if the drive on which this path is located is a cluster shared volume; otherwise, returns 0.|  
  
## Remarks  
 A SQL Server failover cluster instance (FCI) must use shared storage between all nodes of the FCI for data and log file storage. The disks listed in this view are those that are in the cluster resource group associated with the instance and are the only disks that can be used for data or log file storage.  
  
> [!NOTE]  
>  This view will replace [sys.dm_io_cluster_shared_drives &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-cluster-shared-drives-transact-sql.md) in a future release.  
  
## Permissions  
 The user must have VIEW SERVER STATE permission for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
## Examples  
 The following example uses sys.dm_io_cluster_valid_path_names to determine the shared drives on a clustered server instance:  
  
```  
SELECT * FROM sys.dm_io_cluster_valid_path_names;  
```  
  
## See Also  
 [sys.dm_os_cluster_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-cluster-nodes-transact-sql.md)   
 [sys.dm_io_cluster_shared_drives &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-cluster-shared-drives-transact-sql.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
  
  

