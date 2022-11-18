---
description: "MSSQL_ENG014114"
title: "MSSQL_ENG014114 | Microsoft Docs"
ms.custom: ""
ms.date: "08/26/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG014114 error"
ms.assetid: f5f04590-e1c6-40d8-ab2b-98c791a0fc44
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG014114
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14114|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|'%s' is not configured as a Distributor.|  
  
## Explanation  
 If the error message specifies a particular instance, rather than 'null', the instance specified has not been properly configured to be recognized as a Distributor.  
  
 If the message specifies 'null' as a Distributor, there is no entry for the local server in **master** database, or the entry is incorrect (perhaps because the computer was renamed). Replication expects all servers in a topology to be registered using the computer name with an optional instance name (in the case of a clustered instance, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] virtual server name with the optional instance name). For replication to function properly, the value returned by `SELECT @@SERVERNAME` for each server in the topology should match the computer name or virtual server name with the optional instance name.  
  
 Replication is not supported if you have registered any of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances by IP address or by Fully Qualified Domain Name (FQDN). If you had any of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances registered by IP address or by FQDN in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] when you configured replication, this error could be raised.  
  
## User Action  
 If the error message specifies a particular instance, configure the server as a Distributor. For more information, see [Configure Distribution](../../relational-databases/replication/configure-distribution.md).  
  
 If the message does not specify a particular instance ('null'), verify that the Distributor instance is registered properly. If the network name of the computer and the name of the SQL Server instance differ, either:  
  
-   Add the SQL Server instance name as a valid network name. One method to set an alternative network name is to add it to the local hosts file. The local hosts file is located by default at `\Windows\system32\drivers\etc` or `\WINNT\system32\drivers\etc`. For more information, see the Windows documentation.  
  
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
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
