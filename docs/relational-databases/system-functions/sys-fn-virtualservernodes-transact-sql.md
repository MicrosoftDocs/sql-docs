---
title: "sys.fn_virtualservernodes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "fn_virtualservernodes"
  - "fn_virtualservernodes_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "nodes [Faillover Clustering], virtual servers"
  - "nodes [Faillover Clustering]"
  - "virtual servers [Faillover Clustering]"
  - "failover clustering [SQL Server], nodes"
  - "fn_virtualservernodes function"
  - "sys.fn_virtualservernodes function"
ms.assetid: 257f3b8d-93c0-4444-87f1-ea211bd8cad0
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_virtualservernodes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]

  Returns a list of failover clustered instance nodes on which an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can run. This information is useful in failover clustering environments.  
  
> [!IMPORTANT]
>  This [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] system function is included for backward compatibility. We recommend that you use [sys.dm_os_cluster_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-cluster-nodes-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
