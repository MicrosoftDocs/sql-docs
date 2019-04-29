---
title: "DROP AVAILABILITY GROUP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_AVAILABILITY_GROUP_TSQL"
  - "DROP AVAILABILITY GROUP"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], removing"
  - "Availability Groups [SQL Server], Transact-SQL statements"
  - "DROP AVAILABILITY GROUP statement"
  - "Availability Groups [SQL Server], dropping"
ms.assetid: c1600289-c990-454a-b279-dba0ebd5d63e
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: craigg
---
# DROP AVAILABILITY GROUP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Removes the specified availability group and all of its replicas. If a server instance that hosts one of the availability replicas is offline when you delete an availability group, after coming online, the server instance will drop the local availability replica. Dropping an availability group also deletes the associated availability group listener, if any.  
  
> [!IMPORTANT]  
>  If possible, remove the availability group only while connected to the server instance that hosts the primary replica. When the availability group is dropped from the primary replica, changes are allowed in the former primary databases (without high availability protection). Deleting an availability group from a secondary replica leaves the primary replica in the **RESTORING** state, and changes are not allowed on the databases.  
  
 For information about alternative ways to drop an availability group, see [Remove an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/remove-an-availability-group-sql-server.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP AVAILABILITY GROUP group_name   
[ ; ]  
```  
  
## Arguments  
 *group_name*  
 Specifies the name of the availability group to be dropped.  
  
## Limitations and Recommendations  
  
-   Executing **DROP AVAILABILITY GROUP** requires that the Always On Availability Groups feature is enabled on the server instance. For more information, see [Enable and Disable AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).  
  
-   **DROP AVAILABILITY GROUP** cannot be executed as part of batches or within transactions. Also, expressions and variables are not supported.  
  
-   You can drop an availability group from any Windows Server Failover Clustering (WSFC) node that possesses the correct security credentials for the availability group. This enables you to delete an availability group when none of its availability replicas remain.  
  
    > [!IMPORTANT]  
    >  Avoid dropping an availability group when the Windows Server Failover Clustering (WSFC) cluster has no quorum. If you must drop an availability group while the cluster lacks quorum, the metadata availability group that is stored in the cluster is not removed. After the cluster regains quorum, you will need to drop the availability group again to remove it from the WSFC cluster.  
  
-   On a secondary replica, **DROP AVAILABILITY GROUP** should only be used only for emergency purposes. This is because dropping an availability group takes the availability group offline. If you drop the availability group from a secondary replica, the primary replica cannot determine whether the **OFFLINE** state occurred because of quorum loss, a forced failover, or a **DROP AVAILABILITY GROUP** command. The primary replica transitions to the **RESTORING** state to prevent a possible split-brain situation. For more information, see [How It Works: DROP AVAILABILITY GROUP Behaviors](https://blogs.msdn.com/b/psssql/archive/2012/06/13/how-it-works-drop-availability-group-behaviors.aspx) (CSS SQL Server Engineers blog).  
  
## Security  
  
### Permissions  
 Requires **ALTER AVAILABILITY GROUP** permission on the availability group, **CONTROL AVAILABILITY GROUP** permission, **ALTER ANY AVAILABILITY GROUP** permission, or **CONTROL SERVER** permission. To drop an availability group that is not hosted by the local server instance you need **CONTROL SERVER** permission or **CONTROL** permission on that availability group.  
  
## Examples  
 The following example drops the `AccountsAG` availability group.  
  
```  
DROP AVAILABILITY GROUP AccountsAG;  
```  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [How It Works: DROP AVAILABILITY GROUP Behaviors](https://blogs.msdn.com/b/psssql/archive/2012/06/13/how-it-works-drop-availability-group-behaviors.aspx) (CSS SQL Server Engineers blog)  
  
## See Also  
 [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md)   
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-availability-group-transact-sql.md)   
 [Remove an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/remove-an-availability-group-sql-server.md)  
  
  
