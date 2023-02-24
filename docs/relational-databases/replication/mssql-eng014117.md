---
description: "MSSQL_ENG014117"
title: "MSSQL_ENG014117 | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG014117 error"
ms.assetid: e5906a76-9511-4c47-8826-8c765b58a39d
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG014117
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14117|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|'%s' is not configured as a distribution database.|  
  
## Explanation  
 This error can occur if one or both of the following are true:  
  
-   The entry for the specified distribution database is missing from **msdb..MSdistributiondbs**.  
  
-   There is not an entry for the local server in the **master** database, or the entry that is there is incorrect.  
  
     Replication expects all servers in a topology to be registered using the computer name with an optional instance name (in the case of a clustered instance, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] virtual server name with the optional instance name). For replication to function properly, the value returned by `SELECT @@SERVERNAME` for each server in the topology should match the computer name or virtual server name with the optional instance name.  
  
     Replication is not supported if you have registered any of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances by IP address or by Fully Qualified Domain Name (FQDN). If you had any of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances registered by IP address or by FQDN in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] when you configured replication, this error could be raised.  
  
## User Action  
 Verify that the Distributor instance is registered properly. If the network name of the computer and the name of the SQL Server instance differ, either:  
  
-   Add the SQL Server instance name as a valid network name. One method to set an alternative network name is to add it to the local hosts file. The local hosts file is located by default at `\Windows\system32\drivers\etc` or `WINNT\system32\drivers\etc`. For more information, see the Windows documentation.  
  
     For example, if the computer name is comp1 and the computer has an IP address of 10.193.17.129, and the instance name is inst1/instname, add the following entry to the hosts file:  
  
     10.193.17.129 inst1  
  
-   Disable distribution, register the instance, and then reestablish distribution. If the value of @@SERVERNAME is not correct for a nonclustered instance, follow these steps:  
  
    ```  
    sp_dropserver '<old_name>', 'droplogins'  
    go  
    sp_addserver '<new_name>', 'local'  
    go  
    ```  
  
     After you execute the [sp_addserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md) stored procedure, you must restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service for the change to @@SERVERNAME to take effect.  
  
     If the value of @@SERVERNAME is not correct for a clustered instance, you must change the name using Cluster Administrator. For more information, see [Always On Failover Cluster Instances (SQL Server)](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md).  
  
 After verifying that the Distributor instance is registered properly, verify that the distribution database is listed in **msdb..MSdistributiondbs**. If it is not listed:  
  
1.  Script out the distribution configuration. For more information, see [Scripting Replication](../../relational-databases/replication/scripting-replication.md).  
  
2.  Disable distribution and then re-enable it. For more information, see [Configure Distribution](../../relational-databases/replication/configure-distribution.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
