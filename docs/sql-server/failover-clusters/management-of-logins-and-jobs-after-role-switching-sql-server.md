---
title: "Manage logins & jobs after mirror failover"
description: Learn how to manage logins & jobs after failing over your mirrored database from the primary to the secondary database. 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: high-availability
ms.topic: how-to
helpviewer_keywords: 
  - "role switching [SQL Server]"
ms.assetid: fc2fc949-746f-40c7-b5d4-3fd51ccfbd7b
author: MashaMSFT
ms.author: mathoma
---
# Management of Logins and Jobs After Role Switching (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  When deploying a high-availability or disaster-recovery solution for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, it is important to reproduce relevant information that is stored for the database in the **master** or **msdb** databases. Typically, the relevant information includes the jobs of the primary/principal database and the logins of users or processes that need to connect to the database. You should duplicate this information on any instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts a secondary/mirror database. If possible after roles are switched, it is best to programmatically reproduce the information on the new primary/principal database.  
  
## Logins  
 On every server instances that hosts a copy of the database, you should reproduce the logins that have permission to access the principal database. When the primary/principal role switches, only users whose logins exist on the new primary/principal server instance can access the new primary/principal database. Users whose logins are not defined on the new primary/principal server instance are orphaned and cannot access the database.  
  
 If a user is orphaned, create the login on the new primary/principal server instance and run [sp_change_users_login](../../relational-databases/system-stored-procedures/sp-change-users-login-transact-sql.md). For more information, see [Troubleshoot Orphaned Users &#40;SQL Server&#41;](../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md).  
  
###  <a name="SSauthentication"></a> Logins Of Applications That Use SQL Server Authentication or a Local Windows Login  
 If an application uses SQL Server Authentication or a local Windows login, mismatched SIDs can prevent the application's login from resolving on a remote instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The mismatched SIDs cause the login to become an orphaned user on the remote server instance. This issue can occur when an application connects to a mirrored or log shipping database after a failover or to a replication subscriber database that was initialized from a backup.  
  
 To prevent this issue, we recommend that you take preventative measures when you set up such an application to use a database that is hosted by a remote instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Prevention involves transferring the logins and the passwords from the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the remote instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about how to prevent this issue, see KB article 918992 -[How to transfer logins and passwords between instances of SQL Server](https://support.microsoft.com/kb/918992/)).  
  
> [!NOTE]  
>  This problem affects Windows local accounts on different computers. However, this problem does not occur for domain accounts because the SID is the same on each of the computers.  
  
 For more information, see [Orphaned Users with Database Mirroring and Log Shipping](/archive/blogs/sqlserverfaq/orphaned-users-with-database-mirroring-and-log-shipping) (a Database Engine blog).  
  
## Jobs  
 Jobs, such as backup jobs, require special consideration. Typically, after a role switch, the database owner or system administrator must re-create the jobs for the new primary/principal database.  
  
 When the former primary/principal server instance is available, you should delete the original jobs on that instanceof [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Note that jobs on the current mirror database fail because it is in the RESTORING state, making it unavailable.  
  
> [!NOTE]  
>  Different instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might be configured differently, with different drive letters or such. The jobs for each partner must allow for any such differences.  
  
## See Also  
 [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md)   
 [Troubleshoot Orphaned Users &#40;SQL Server&#41;](../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md)  
  
