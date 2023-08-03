---
title: "sys.fn_servershareddrives (Transact-SQL)"
description: "sys.fn_servershareddrives (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fn_servershareddrives"
  - "fn_servershareddrives_TSQL"
helpviewer_keywords:
  - "fn_servershareddrives function"
  - "shared drives [SQL Server]"
  - "names [SQL Server], shared drives"
  - "sys.fn_serversharedrives function"
dev_langs:
  - "TSQL"
---
# sys.fn_servershareddrives (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the names of shared drives used by the clustered server.  
  
> [!IMPORTANT]  
>  This  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system function is included for backward compatibility. We recommend that you use [sys.dm_io_cluster_valid_path_names &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-cluster-valid-path-names-transact-sql.md) instead.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_servershareddrives()  
```  
  
## Tables Returned  
 If the current server is a clustered server, **fn_servershareddrives** returns the drive name of the shared drives.  
  
 If the current server instance is not a clustered server, **fn_servershareddrives** returns an empty rowset.  
  
## Remarks  
 `fn_servershareddrives` returns a list of shared drives used by this clustered server. These shared drives belong to the same cluster group as the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource. Further, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource is dependent on these drives.  
  
 This function is helpful in identifying drives available to users.  
  
## Permissions  
 The user must have VIEW SERVER STATE permission for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
## Examples  
 The following example uses `fn_servershareddrives` to query on a clustered server instance:  
  
```  
SELECT * FROM fn_servershareddrives();  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 DriveName  
  
 -------\-  
  
 m  
  
 n  
  
## See Also  
 [sys.dm_io_cluster_valid_path_names &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-cluster-valid-path-names-transact-sql.md)   
 [sys.dm_io_cluster_shared_drives &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-cluster-shared-drives-transact-sql.md)   
 [sys.fn_virtualservernodes &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-virtualservernodes-transact-sql.md)  
  
  
