---
title: "Remove an Availability Group (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.availabilitygroup.deleteag.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], removing"
  - "Availability Groups [SQL Server], dropping"
ms.assetid: 4b7f7f62-43a3-49db-a72e-22d4d7c2ddbb
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Remove an Availability Group (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to delete (drop) an Always On availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. If a server instance that hosts one of the availability replicas is offline when you delete an availability group, after coming online, the server instance will drop the local availability replica. Dropping an availability group deletes any associated availability group listener.  
  
 Note that, if necessary, you can drop an availability group from any Windows Server Failover Clustering (WSFC) node that possesses the correct security credentials for the availability group. This enables you to delete an availability group when none of its availability replicas remain.  
  
> [!IMPORTANT]  
>  If possible, remove the availability group only while connected to the server instance that hosts the primary replica. When the availability group is dropped from the primary replica, changes are allowed in the former primary databases (without high availability protection). Deleting an availability group from a secondary replica leaves the primary replica in the RESTORING state, and changes are not allowed on the databases.  

  
## <a name="Restrictions"></a> Limitations and Recommendations  
  
-   When the availability group is online, deleting it from a secondary replica causes the primary replica to transition to the RESTORING state. Therefore, if possible, remove the availability group only from the server instance that hosts the primary replica.    
-   If you delete an availability group from a computer that has been removed or evicted from the WSFC failover cluster, the availability group is only deleted locally. 
-   Avoid dropping an availability group when the Windows Server Failover Clustering (WSFC) cluster has no quorum. If you must drop an availability group while the cluster lacks quorum, the metadata availability group that is stored in the cluster is not removed. After the cluster regains quorum, you will need to drop the availability group again to remove it from the WSFC cluster.    
-   On a secondary replica, DROP AVAILABILITY GROUP should only be used only for emergency purposes. This is because dropping an availability group takes the availability group offline. If you drop the availability group from a secondary replica, the primary replica cannot determine whether the OFFLINE state occurred because of quorum loss, a forced failover, or a DROP AVAILABILITY GROUP command. The primary replica transitions to the RESTORING state to prevent a possible split-brain situation. For more information, see [How It Works: DROP AVAILABILITY GROUP Behaviors](https://blogs.msdn.com/b/psssql/archive/2012/06/13/how-it-works-drop-availability-group-behaviors.aspx) (CSS SQL Server Engineers blog).  
  
##  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission. To drop an availability group that is not hosted by the local server instance you need CONTROL SERVER permission or CONTROL permission on that Availability Group.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To delete an availability group**  
  
1.  In Object Explorer, connect to the server instance that hosts primary replica, if possible, or connect to another server instance that is enabled for Always On Availability Groups on a WSFC node that possess the correct security credentials for the availability group. Expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  This step depends on whether you want to delete multiple availability groups or only one availability group, as follows:  
  
    -   To delete multiple availability groups (whose primary replicas are on the connected server instance), use the **Object Explorer Details** pane to view and select all the availability groups that you want to delete. For more information, see [Use the Object Explorer Details to Monitor Availability Groups &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-object-explorer-details-to-monitor-availability-groups.md).  
  
    -   To delete a single availability group, select it in either the **Object Explorer** pane or the **Object Explorer Details** pane.  
  
4.  Right-click the selected availability group or groups, and select the **Delete** command.  
  
5.  In the **Remove Availability Group** dialog box, to delete all the listed availability groups, click **OK**. If you do not want to remove all the listed availability groups, click **Cancel**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To delete an availability group**  
  
1.  Connect to the server instance that hosts the primary replica, if possible, or connect to another server instance that is enabled for Always On Availability Groups on a WSFC node that possess the correct security credentials for the availability group.  
  
2.  Use the [DROP AVAILABILITY GROUP](../../../t-sql/statements/drop-availability-group-transact-sql.md) statement, as follows  
  
     DROP AVAILABILITY GROUP *group_name*  
  
     where *group_name* is the name of the availability group to be dropped.  
  
     The following example deletes the `MyAG` availability group.  
  
    ```  
    DROP AVAILABILITY GROUP MyAG;  
    ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To delete an availability group**  
  
 In the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell provider:  
  
1.  Change directory (**cd**) to the server instance that hosts the primary replica, if possible, or connect to another server instance that is enabled for Always On Availability Groups on a WSFC node that possess the correct security credentials for the availability group.  
  
2.  Use the **Remove-SqlAvailabilityGroup** cmdlet.  
  
     For example, the following command removes the availability group named `MyAg`. This command can be executed on any server instance that hosts an availability replica for the availability group.  
  
    ```  
    Remove-SqlAvailabilityGroup `   
    -Path SQLSERVER:\Sql\Computer\Instance\AvailabilityGroups\MyAg  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [How It Works: DROP AVAILABILITY GROUP Behaviors](https://blogs.msdn.com/b/psssql/archive/2012/06/13/how-it-works-drop-availability-group-behaviors.aspx) (CSS SQL Server Engineers blog)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)  
  
  
