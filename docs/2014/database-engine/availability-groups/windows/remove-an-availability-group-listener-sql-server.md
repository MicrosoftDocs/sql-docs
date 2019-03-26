---
title: "Remove an Availability Group Listener (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.availabilitygroup.removeaglistener.default.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], listeners"
ms.assetid: fd9bba9a-d29f-4c23-8ecd-aaa049ed5f1b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Remove an Availability Group Listener (SQL Server)
  This topic describes how to remove an availability group listener from an AlwaysOn availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To remove a listener, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [PowerShell](#PowerShellProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   You must be connected to the server instance that hosts the primary replica.  
  
###  <a name="Recommendations"></a> Recommendations  
 Before you delete an availability group listener, we recommend that you ensure that no applications are using it.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To remove an availability group listener**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and click the server name to expand the server tree.  
  
2.  Expand the **AlwaysOn High Availability** node and the **Availability Groups** node.  
  
3.  Expand the node of the availability group, and expand the **Availability Groups Listeners** node.  
  
4.  Right-click the listener to be removed, and select the **Delete** command.  
  
5.  This opens the **Remove Listener from Availability Group** dialog box. For more information, see [Remove Listener from Availability Group](#AgListenerPropertiesDialog), later in this topic.  
  
###  <a name="AgListenerPropertiesDialog"></a> Remove Listener from Availability Group (Dialog Box)  
 **Name**  
 The name of the listener to be removed.  
  
 **Result**  
 Displays a link, either **Success** or **Error**, which you can click for more information.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To remove an availability group listener**  
  
1.  Connect to the server instance that hosts the primary replica.  
  
2.  Use the [ALTER AVAILABILITY GROUP](/sql/t-sql/statements/alter-availability-group-transact-sql) statement, as follows:  
  
     ALTER AVAILABILITY GROUP *group_name* REMOVE LISTENER **'*`dns_name`*'**  
  
     where *group_name* is the name of the availability group and *dns_name* is the DNS name of the availability group listener.  
  
     The following example deletes the listener of the `AccountsAG` availability group. The DNS name is AccountsAG_Listener.  
  
    ```  
    ALTER AVAILABILITY GROUP AccountsAG REMOVE LISTENER 'AccountsAG_Listener';  
    ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To remove an availability group listener**  
  
1.  Set default (`cd`) to the server instance that hosts the primary replica.  
  
2.  Use the built in `Remove-Item` cmdlet to remove a listener. For example, the following command removes a listener named `MyListener` from an availability group named `MyAg`.  
  
    ```  
    Remove-Item `   
    SQLSERVER:\Sql\PrimaryServer\InstanceName\AvailabilityGroups\MyAg\AGListeners\MyListener  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [View Availability Group Listener Properties &#40;SQL Server&#41;](view-availability-group-listener-properties-sql-server.md)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../listeners-client-connectivity-application-failover.md)  
  
  
