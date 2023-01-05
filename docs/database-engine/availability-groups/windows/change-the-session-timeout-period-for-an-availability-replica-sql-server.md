---
title: "Modify session-timeout for an availability group replica"
description: "Describes how to configure the session-timeout period of a replica within an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], session timeout"
  - "session timeout [SQL Server]"
---
# Modify the session-timeout period for an availability group replica
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to configure the session-timeout period of an Always On availability replica by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)]. The session-timeout period is a replica property that controls how many seconds (in seconds) that an availability replica waits for a ping response from a connected replica before considering the connection to have failed. By default, a replica waits 10 seconds for a ping response. This replica property applies only the connection between a given secondary replica and the primary replica of the availability group. For more information about the session-timeout period, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
   
##  <a name="Prerequisites"></a> Prerequisites  
  
-   You must be connected to the server instance that hosts the primary replica.  
  
##  <a name="Recommendations"></a> Recommendations  
 We recommend that you keep the time-out period at 10 seconds or greater. Setting the value to less than 10 seconds creates the possibility of a heavily loaded system missing PINGs and declaring a false failure.  
  
  
## <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To change the session-timeout period for an availability replica**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Click the availability group whose availability replica you want to configure.  
  
4.  Right-click the replica to be configured, and click **Properties**.  
  
5.  In the **Availability Replica Properties** dialog box, use the **Session timeout (seconds)** field to change the number of seconds for the session-timeout period on this replica.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To change the session-timeout period for an availability replica**  
  
1.  Connect to the server instance that hosts the primary replica.  
  
2.  Use the [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) statement, as follows:  
  
     ALTER AVAILABILITY GROUP *group_name*  
  
     MODIFY REPLICA ON '*instance_name*' WITH ( SESSION_TIMEOUT =*seconds* )  
  
     where *group_name* is the name of the availability group, *instance_name* is the name of the server instance that hosts the availability replica to be modified, and *seconds* specifies the minimum number of seconds that the replica must wait before applying log to databases when acting as a secondary replica. The default is 0 seconds, which indicates that there is no apply delay.  
  
     The following example, entered on the primary replica of the `AccountsAG` availability group, changes the session-timeout value to `15` seconds for the replica located on the `INSTANCE09` server instance.  
  
    ```  
    ALTER AVAILABILITY GROUP AccountsAG   
       MODIFY REPLICA ON 'INSTANCE09' WITH (SESSION_TIMEOUT = 15);  
    ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To change the session-timeout period for an availability replica**  
  
1.  Change directory (**cd**) to the server instance that hosts the primary replica.  
  
2.  Use the **Set-SqlAvailabilityReplica** cmdlet with the **SessionTimeout** parameter to change the number of seconds for the session-timeout period on a specified availability replica.  
  
     For example, the following command sets the session-timeout period to 15 seconds.  
  
    ```  
    Set-SqlAvailabilityReplica -SessionTimeout 15 `   
    -Path SQLSERVER:\Sql\PrimaryServer\InstanceName\AvailabilityGroups\MyAg\AvailabilityReplicas\MyReplica  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
