---
title: "Enable and Disable AlwaysOn Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], server instance"
  - "Availability Groups [SQL Server], deploying"
  - "Availability Groups [SQL Server], disabling"
  - "Availability Groups [SQL Server], enabling"
ms.assetid: 7c326958-5ae9-4761-9c57-905972276a8f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Enable and Disable AlwaysOn Availability Groups (SQL Server)
  Enabling [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] is a prerequisite for a server instance to use availability groups. Before you can create and configure any availability group, the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature must have been enabled on the each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that will host an availability replica for one or more availability groups.  
  
> [!IMPORTANT]  
>  If you delete and re-create a WSFC cluster, you must disable and re-enable the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature on each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosted an availability replica on the original WSFC cluster.  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **How To:**  
  
    -   [Determine Whether AlwaysOn Availability Groups is Enabled](#IsEnabled)  
  
    -   [Enable AlwaysOn Availability Groups](#EnableAOAG)  
  
    -   [Disable AlwaysOn Availability Groups](#DisableAOAG)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites for Enabling AlwaysOn Availability Groups  
  
-   The server instance must reside on a Windows Server Failover Clustering (WSFC) node.  
  
-   The server instance must be running an edition of SQL Server that supports [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. For more information, see [Features Supported by the Editions of SQL Server 2014](../../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
-   Enable AlwaysOn Availability Groups on only one server instance at a time. After enabling AlwaysOn Availability Groups, wait until the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service has restarted before you proceed to another server instance.  
  
 For information about additional prerequisites for creating and configuring availability groups, see [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-restrictions-recommendations-always-on-availability.md).  
  
###  <a name="Security"></a> Security  
 While AlwaysOn Availability Groups is enabled on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the server instance has full control on the WSFC cluster.  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the **Administrator** group on the local computer and full control on the WSFC cluster. When enabling AlwaysOn by using PowerShell, open the Command Prompt window using the **Run as administrator** option.  
  
 Requires Active Directory Create Objects and Manage Objects permissions.  
  
##  <a name="IsEnabled"></a> Determine Whether AlwaysOn Availability Groups is Enabled  
  
-   [SQL Server Management Studio](#SSMS1Procedure)  
  
-   [Transact-SQL](#Tsql1Procedure)  
  
-   [PowerShell](#PowerShell1Procedure)  
  
###  <a name="SSMS1Procedure"></a> Using SQL Server Management Studio  
 **To determine whether AlwaysOn Availability Groups is enabled**  
  
1.  In Object Explorer, right-click the server instance, and  click **Properties**.  
  
2.  In the **Server Properties** dialog box, click the **General** page. The **Is HADR Enabled** property displays one of the following values:  
  
    -   **True**, if AlwaysOn Availability Groups is enabled  
  
    -   **False**, if AlwaysOn Availability Groups is disabled.  
  
###  <a name="Tsql1Procedure"></a> Using Transact-SQL  
 **To determine whether AlwaysOn Availability Groups is enabled**  
  
1.  Use the following [SERVERPROPERTY](/sql/t-sql/functions/serverproperty-transact-sql) statement:  
  
    ```  
    SELECT SERVERPROPERTY ('IsHadrEnabled');  
    ```  
  
     The setting of the `IsHadrEnabled` server property indicates whether an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is enabled for AlwaysOn Availability Groups, as follows:  
  
    -   If `IsHadrEnabled` = 1, AlwaysOn Availability Groups is enabled.  
  
    -   If `IsHadrEnabled` = 0, AlwaysOn Availability Groups is disabled.  
  
    > [!NOTE]  
    >  For more information about the `IsHadrEnabled` server property, see [SERVERPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/serverproperty-transact-sql).  
  
###  <a name="PowerShell1Procedure"></a> Using PowerShell  
 **To determine whether AlwaysOn Availability Groups is enabled**  
  
1.  Set default (`cd`) to the server instance on which you want to determine whether [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] is enabled.  
  
2.  Enter the following PowerShell `Get-Item` command:  
  
    ```  
    PS SQLSERVER:\SQL\NODE1\DEFAULT> get-item . | select IsHadrEnabled  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
##  <a name="EnableAOAG"></a> Enable AlwaysOn Availability Groups  
 **To enable AlwaysOn, using:**  
  
-   [SQL Server Configuration Manager](#SQLCM2Procedure)  
  
-   [PowerShell](#PScmd2Procedure)  
  
###  <a name="SQLCM2Procedure"></a> Using SQL Server Configuration Manager  
 **To enable AlwaysOn Availability Groups**  
  
1.  Connect to the Windows Server Failover Clustering (WSFC) node that hosts the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance where you want to enable AlwaysOn Availability Groups.  
  
2.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and  click **SQL Server Configuration Manager**.  
  
3.  In **SQL Server Configuration Manager**, click **SQL Server Services**, right-click SQL Server (**<*`instance name`*>)**, where **<*`instance name`*>** is the name of a local server instance for which you want to enable AlwaysOn Availability Groups, and click **Properties.**  
  
4.  Select the **AlwaysOn High Availability** tab.  
  
5.  Verify that **Windows failover cluster name** field contains the name of the local failover cluster. If this field is blank, this server instance currently does not support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. Either the local computer is not a cluster node, the WSFC cluster has been shut down, or this edition of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] that does not support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)].  
  
6.  Select the **Enable AlwaysOn Availability Groups** check box, and click **OK**.  
  
     [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager saves your change. Then, you must manually restart the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service. This enables you to choose a restart time that is best for your business requirements. When the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service restarts, AlwaysOn will be enabled, and the `IsHadrEnabled` server property will be set to 1.  
  
###  <a name="PScmd2Procedure"></a> Using SQL Server PowerShell  
 **To enable AlwaysOn**  
  
1.  Change directory (`cd`) to a server instance that you want to enable for AlwaysOn Availability Groups.  
  
2.  Use the `Enable-SqlAlwaysOn` cmdlet to enable AlwaysOn Availability Groups.  
  
     To view the syntax of a cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
    > [!NOTE]  
    >  For information about how to control whether the `Enable-SqlAlwaysOn` cmdlet restarts the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service, see [When Does a Cmdlet Restart the SQL Server Service?](#WhenCmdletRestartsSQL), later in this topic.  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
####  <a name="ExmplEnable-SqlHadrServic"></a> Example: Enable-SqlAlwaysOn  
 The following PowerShell command enables [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] on an instance of SQL Server (*Computer*\\*Instance*).  
  
```  
Enable-SqlAlwaysOn -Path SQLSERVER:\SQL\Computer\Instance  
```  
  
##  <a name="DisableAOAG"></a> Disable AlwaysOn Availability Groups  
  
-   **Before you disable AlwaysOn:**  
  
     [Recommendations](#Recommendations)  
  
-   **To disable AlwaysOn, using:**  
  
    -   [SQL Server Configuration Manager](#SQLCM3Procedure)  
  
    -   [PowerShell](#PScmd3Procedure)  
  
-   **Follow Up:**  [After Disabling AlwaysOn](#FollowUp)  
  
> [!IMPORTANT]  
>  Disable AlwaysOn on only one server instance at a time. After disabling AlwaysOn Availability Groups, wait until the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service has restarted before you proceed to another server instance.  
  
###  <a name="Recommendations"></a> Recommendations  
 Before you disable AlwaysOn on a server instance, we recommend that you do the following:  
  
1.  If the server instance is currently hosting the primary replica of an availability group that you want to keep, we recommend that you manually fail over the availability group to a synchronized secondary replica, if possible. For more information, see [Perform a Planned Manual Failover of an Availability Group &#40;SQL Server&#41;](perform-a-planned-manual-failover-of-an-availability-group-sql-server.md).  
  
2.  Remove all local secondary replicas. For more information, see [Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](remove-a-secondary-replica-from-an-availability-group-sql-server.md).  
  
###  <a name="SQLCM3Procedure"></a> Using SQL Server Configuration Manager  
 **To disable AlwaysOn**  
  
1.  Connect to the Windows Server Failover Clustering (WSFC) node that hosts the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance where you want to disable AlwaysOn Availability Groups.  
  
2.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and click **SQL Server Configuration Manager**.  
  
3.  In **SQL Server Configuration Manager**, click **SQL Server Services**, right-click SQL Server (**<*`instance name`*>)**, where **<*`instance name`*>** is the name of a local server instance for which you want to disable AlwaysOn Availability Groups, and click **Properties**.  
  
4.  On the**AlwaysOn High Availability**tab, deselect the **Enable AlwaysOn Availability Groups** check box, and click **OK**.  
  
     [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager saves your change and restarts the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service. When the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service restarts, AlwaysOn will be disabled, and the `IsHadrEnabled` server property will be set to 0, to indicate that AlwaysOn Availability Groups is disabled.  
  
5.  We recommend that you read the information in [Follow Up: After Disabling AlwaysOn](#FollowUp), later in this topic.  
  
###  <a name="PScmd3Procedure"></a> Using SQL Server PowerShell  
 **To disable AlwaysOn**  
  
1.  Change directory (`cd`) to a currently-enabled server instance that you want to disenable for AlwaysOn Availability Groups.  
  
2.  Use the `Disable-SqlAlwaysOn` cmdlet to enable AlwaysOn Availability Groups.  
  
     For example, the following command disables AlwaysOn Availability Groups on an instance of SQL Server (*Computer*\\*Instance*).  This command requires restarting the instance, and you will be prompted to confirm this restart.  
  
    ```  
    Disable-SqlAlwaysOn -Path SQLSERVER:\SQL\Computer\Instance  
    ```  
  
    > [!IMPORTANT]  
    >  For information about how to control whether the `Disable-SqlAlwaysOn` cmdlet restarts the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service, see [When Does a Cmdlet Restart the SQL Server Service?](#WhenCmdletRestartsSQL), later in this topic.  
  
     To view the syntax of a cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
###  <a name="FollowUp"></a> Follow Up: After Disabling AlwaysOn  
 After you disable AlwaysOn Availability Groups, the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] must be restarted. SQL Configuration Manager restarts the server instance automatically. However, if you used the `Disable-SqlAlwaysOn` cmdlet, you will need to restart the server instance manually. For more information, see [sqlservr Application](../../../tools/sqlservr-application.md).  
  
 On the restarted server instance:  
  
-   Availability databases do not start up at SQL Server startup, making them inaccessible.  
  
-   The only supported AlwaysOn [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement is [DROP AVAILABILITY GROUP](/sql/t-sql/statements/drop-availability-group-transact-sql). CREATE AVAILABILITY GROUP, ALTER AVAILABILITY GROUP, and the SET HADR options of ALTER DATABASE are not supported.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] metadata and [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] configuration data in WSFC are unaffected by disabling AlwaysOn Availability Groups.  
  
 If you permanently disable AlwaysOn Availability Groups on every server instance that hosts an availability replica for one or more availability groups, we recommend that you complete the following steps:  
  
1.  If you did not remove the local availability replicas before disabling AlwaysOn, delete (drop) each availability group for which the server instance is hosting an availability replica. For information about deleting an availability group, see [Remove an Availability Group &#40;SQL Server&#41;](remove-an-availability-group-sql-server.md).  
  
2.  To remove the metadata left behind, delete (drop) each affected availability group on a server instance that is part of the original WSFC cluster.  
  
3.  Any primary databases continue to be accessible to all connections but the data synchronization between the primary and secondary databases stops.  
  
4.  The secondary databases enter the RESTORING state. You can delete them, or you can restore them by using RESTORE WITH RECOVERY. However, restored databases are no longer participating in availability-group data synchronization.  
  
##  <a name="WhenCmdletRestartsSQL"></a> When Does a Cmdlet Restart the SQL Server Service?  
 On a server instance that is currently running, using `Enable-SqlAlwaysOn` or `Disable-SqlAlwaysOn` to change the current AlwaysOn setting could cause the SQL Server service to restart. The restart behavior on depends on the following conditions:  
  
|-NoServiceRestart parameter specified|-Force parameter specified|Is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service restarted?|  
|--------------------------------------------|---------------------------------|---------------------------------------------------------|  
|No|No|By default. But the cmdlet prompts you as follows:<br /><br /> **To complete this action, we must restart the SQL Server service for server instance '<instance_name>'. Do you want to continue?**<br /><br /> **[Y] Yes  [N] No  [S] Suspend  [?] Help (default is "Y"):**<br /><br /> If you specify **N** or **S**, the service is not restarted.|  
|No|Yes|Service is restarted.|  
|Yes|No|Service is not restarted.|  
|Yes|Yes|Service is not restarted.|  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [SERVERPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/serverproperty-transact-sql)  
  
  
