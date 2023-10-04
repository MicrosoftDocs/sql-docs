---
title: "sys.fn_virtualservernodes (Transact-SQL)"
description: "sys.fn_virtualservernodes (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fn_virtualservernodes"
  - "fn_virtualservernodes_TSQL"
helpviewer_keywords:
  - "nodes [Faillover Clustering], virtual servers"
  - "nodes [Faillover Clustering]"
  - "virtual servers [Faillover Clustering]"
  - "failover clustering [SQL Server], nodes"
  - "fn_virtualservernodes function"
  - "sys.fn_virtualservernodes function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_virtualservernodes (Transact-SQL)
[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  Returns a list of failover clustered instance nodes on which an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can run. This information is useful in failover clustering environments.  
  
> [!IMPORTANT]
>  This [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] system function is included for backward compatibility. We recommend that you use [sys.dm_os_cluster_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-cluster-nodes-transact-sql.md) instead.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_virtualservernodes()  
```  
  
## Tables Returned  
 If the current server is a clustered server, **fn_virtualservernodes** returns a list of failover clustered instance nodes on which this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has been defined.  
  
 If the current server instance is not a clustered server, **fn_virtualservernodes** returns an empty rowset.  
  
## Permissions  
 The user must have VIEW SERVER STATE permission for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Examples  
 The following example uses `fn_virtualservernodes` to query on a clustered server instance:  
  
```  
SELECT * FROM fn_virtualservernodes();  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 NodeName  
  
 -------\-  
  
 SS3-CLUSN1  
  
 SS3-CLUSN2  
  
## See Also  
 [sys.dm_os_cluster_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-cluster-nodes-transact-sql.md)   
 [sys.fn_servershareddrives &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-servershareddrives-transact-sql.md)  
  
  
