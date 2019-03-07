---
title: "MSSQL_ENG014010 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG014010 error"
ms.assetid: 6ea84f2f-e7a2-4028-9ea9-af0d2eba660e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG014010
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14010|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The server '%s' is not defined as a subscription server.|  
  
## Explanation  
 Replication expects all servers in a topology to be registered using the computer name with an optional instance name (in the case of a clustered instance, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] virtual server name with the optional instance name). For replication to function properly, the value returned by `SELECT @@SERVERNAME` for each server in the topology should match the computer name or virtual server name with the optional instance name.  
  
 Replication is not supported if you have registered any of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances by IP address or by Fully Qualified Domain Name (FQDN). If you have any of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances registered by IP address or by FQDN in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] when you configured replication, this error could be raised.  
  
## User Action  
 Verify that all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances in the topology are registered properly. If the network name of the computer and the name of the SQL Server instance differ, either:  
  
-   Add the SQL Server instance name as a valid network name. One method to set an alternative network name is to add it to the local hosts file. The local hosts file is located by default at WINDOWS\system32\drivers\etc or WINNT\system32\drivers\etc. For more information, see the Windows documentation.  
  
     For example, if the computer name is comp1 and the computer has an IP address of 10.193.17.129, and the instance name is inst1/instname, add the following entry to the hosts file:  
  
     10.193.17.129 inst1  
  
-   Remove replication, register each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, and then reestablish replication. If the value of @@SERVERNAME is not correct for a non-clustered instance, follow these steps:  
  
    ```  
    sp_dropserver '<old_name>', 'droplogins'  
    go  
    sp_addserver '<new_name>', 'local'  
    go  
    ```  
  
     After you execute the [sp_addserver &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addserver-transact-sql) stored procedure, you must restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service for the change to @@SERVERNAME to take effect.  
  
     If the value of @@SERVERNAME is not correct for a clustered instance, you must change the name using Cluster Administrator. For more information, see [AlwaysOn Failover Cluster Instances &#40;SQL Server&#41;](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md).  
  
## See Also  
 [@@SERVERNAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/servername-transact-sql)   
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)  
  
  
