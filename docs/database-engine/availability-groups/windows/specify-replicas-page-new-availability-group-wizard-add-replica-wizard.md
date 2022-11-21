---
title: "Availability Group Wizard: Specify Replicas Page"
description: Describes the options of the Specify Replicas page of the New Availability Group Wizard in SQL Server Management Studio (SSMS).
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.newagwizard.listeners.f1"
  - "sql13.swb.addreplicawizard.specifyreplicas.f1"
  - "sql13.swb.newagwizard.specifyreplicas.f1"
---
# Specify Replicas Page (New Availability Group Wizard: Add Replica Wizard)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes the options of the **Specify Replicas** page. This page applies to the **[!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)]** and the **[!INCLUDE[ssAoAddRepWiz](../../../includes/ssaoaddrepwiz-md.md)]**. Use the **Specify Replicas** page to specify and configure one or more availability replicas to add the availability group. This page contains four tabs, which are introduced in the following table. Click the name of a tab in the table to go to the corresponding section, later in this topic.  
  
|Tab|Brief Description|  
|---------|-----------------------|  
|[Replicas](#ReplicasTab)|Use this tab to specify each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that will host or currently hosts a secondary replica. Note that the server instance to which you are currently connected must host the primary replica.<br /><br /> Finish specifying all the replicas on the **Replicas** tab before starting the other tabs.<br/><br/> Note **Automatic failover** is disabled if the cluster type is **NONE**. SQL Server only supports manual failover when an availability group is not in a cluster. <br/><br/> When cluster type is EXTERNAL, failover mode is **External**. <br/><br/> When you are adding a replica, all new replicas must be hosted on the same operating system type as the existing replicas. <br/><br/>When adding a replica, if the primary replica is on a WSFC, the secondary replicas must be in the same cluster.|
|[Endpoints](#EndpointsTab)|Use this tab to verify any existing database mirroring endpoints and also, if this endpoint is lacking on a server instance whose service accounts use Windows Authentication, to create the endpoint automatically.|  
|[Backup Preferences](#BackupPreferencesTab)|Use this tab to specify your backup preference for the availability group as a whole and your backup priorities for the individual availability replicas.|  
|[Listener](#Listener)|Use this tab, if available, to create an availability group listener. By default, a listener is not created.<br /><br /> This tab is available only if you are running the [!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)].<br/><br/>DHCP is disabled when the cluster type is either EXTERNAL or NONE. |  
  
##  <a name="ReplicasTab"></a> Replicas Tab  
 **Server Instance**  
 Displays the name of the server instance that will host the availability replica.  
  
 If a server instance that you plan to use to host a secondary replica is not listed by the **Availability Replicas** grid, click the **Add Replica** button. If you are configuring an availability group in a hybrid-IT environment (see [High Availability and Disaster Recovery for SQL Server in Azure Virtual Machines](/previous-versions/azure/jj870962(v=azure.100))), you can click the **Add Azure Replica** button to create virtual machines with secondary replicas in Azure.  
  
 **Initial Role**  
 Indicates the role that the new replica will initially perform: **Primary** or **Secondary**.  
  
 **Automatic Failover (Up to 3)**  
 Select this checkbox only if you want this availability replica to be an automatic-failover partner. To configure automatic failover, you must choose this option for the initial primary replica and for one secondary replica. Both of these replicas will use the synchronous-commit availability mode. Only three replicas can support automatic failover.  
  
 For information about the synchronous-commit availability mode, see [Availability Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md). For information about automatic failover, see [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).  
  
 **Synchronous Commit (Up to 3)**  
 If you selected **Automatic Failover (Up to 3)** for the replica, **Synchronous Commit (Up to 3)** is also selected. If the check box is blank, select it only if you want this replica to use synchronous-commit mode with only planned manual failover. Only three replicas can use synchronous-commit mode.  
  
 If you want this replica to use asynchronous-commit availability mode, leave this checkbox blank. The replica will support only forced manual failover (with possible data loss). For information about the asynchronous-commit availability mode, see [Availability Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md). For information about planned manual failover and forced manual failover, see [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).  
  
 **Readable Secondary Role**  
 Select a value from the **Readable secondary** drop list, as follows:  
  
 **No**  
 No direct connections are allowed to secondary databases of this replica. They are not available for read access. This is the default setting.  
  
 **Read-intent only**  
 Only direct read-only connections are allowed to secondary databases of this replica. The secondary database(s) are all available for read access.  
  
 **Yes**  
 All connections are allowed to secondary databases of this replica, but only for read access. The secondary database(s) are all available for read access.  
  
 **Add Replica**  
 Click to add a secondary replica to the availability group.  
  
 **Add Azure Replica**  
 Click to create an Azure virtual machine that is running a secondary replica in the availability group. This option is applicable only for an availability group in hybrid IT that contains on-premises replicas. For more information, see [High Availability and Disaster Recovery for SQL Server in Azure Virtual Machines](/previous-versions/azure/jj870962(v=azure.100)).  
  
 **Remove Replica**  
 Click to remove the selected secondary replica from the availability group.  
  
##  <a name="EndpointsTab"></a> Endpoints Tab  
 For each server instance that will host an availability replica, the **Endpoints** tab displays actual values of the existing database mirroring endpoint, if any, or suggested values for a potential new endpoint that would use Windows Authentication. For both existing and potential endpoints, the Endpoint values grid displays the following information:  
  
 **Server Name**  
 Displays the name of a server instance that will host an availability replica.  
  
 **Endpoint URL**  
 Displays the actual or proposed URL of the database mirroring endpoint. For a proposed new endpoint, you can change this value. For information the format of these URLs, see [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md).  
  
 **Port Number**  
 Displays the actual or proposed port number of the endpoint. For a proposed new endpoint, you can change this value.  
  
 **Endpoint Name**  
 Displays the actual or proposed name of the endpoint. For a proposed new endpoint, you can change this value.  
  
 **Encrypt Data**  
 Indicates whether data sent over this endpoint is encrypted. For a proposed new endpoint, you can change this setting.  
  
 **SQL Server Service Account**  
 Username of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account.  
  
 For a server instance to use an endpoint that uses Windows Authentication, its [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account must be a domain account.  
  
 This requirement determines your next configuration step, as follows:  
  
-   If every server instance is running under a domain service account, that is, if the **SQL Server Service Account** column displays a domain service account for every server instance, click **Next**.  
  
-   If any server instance is running under a non-domain service account, you need to do make a manual change to your server instance before you can proceed in the wizard. In this case, clicking **Next** brings up a warning dialog box; you should click **No**, which returns you to the**Endpoints** tab. While leaving the wizard on the **Specify Replicas** page, make one of the following changes to each server instance for which the **SQL Server Service Account** column displays a nondomain service account, either:  
  
    -   Use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager to change the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account to a domain account. For more information, see [Change the Service Startup Account for SQL Server &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/scm-services-change-the-service-startup-account.md).  
  
    -   Use [!INCLUDE[tsql](../../../includes/tsql-md.md)] or PowerShell to manually create a database mirroring endpoint that uses a certificate. For more information, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../../t-sql/statements/create-endpoint-transact-sql.md) or [Create a Database Mirroring Endpoint for Always On Availability Groups &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md).  
  
     If you leave the **Specify Availability Replicas** page open while you configure endpoints, return to the **Endpoints** tab and click **Refresh** to update the **Endpoint values** grid.  
  
##  <a name="BackupPreferencesTab"></a> Backup Preferences Tab  
 To specify where backups should occur, choose one of the following options:  
  
 **Prefer Secondary**  
 Specifies that backups should occur on a secondary replica except when the primary replica is the only replica online. In that case, the backup should occur on the primary replica. This is the default option.  
  
 **Secondary only**  
 Specifies that backups should never be performed on the primary replica. If the primary replica is the only replica online, the backup should not occur.  
  
 **Primary**  
 Specifies that the backups should always occur on the primary replica. This option is useful if you need backup features, such as creating differential backups, that are not supported when backup is run on a secondary replica.  
  
 **Any Replica**  
 Specifies that you prefer that backup jobs ignore the role of the availability replicas when choosing the replica to perform backups. Note backup jobs might evaluate other factors such as backup priority of each availability replica in combination with its operational state and connected state.  
  
> [!IMPORTANT]  
>  There is no enforcement of the backup-preference setting. The interpretation of this preference depends on the logic, if any, that you script into back jobs for the databases in a given availability group. For more information, see [Active Secondaries: Backup on Secondary Replicas &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md).  
  
### Replica backup priorities grid  
 Use the **Replica backup priorities** grid to specify your backup priorities for each of replicas of the availability group. This grid contains the following columns:  
  
 **Server Instance**  
 Displays the name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts the availability replica.  
  
 **Backup Priority (Lowest=1, Highest=100)**  
 Assign the priority for backups being performed on this replica relative to the other replicas in the same availability group. The default value is 50. You can select any other integer in the range of 0..100. 1 indicates the lowest priority, and 100 indicates the highest priority. If you set **Backup Priority** to 1, the availability replica will be chosen for performing backups only if no higher priority availability replica is currently available.  
  
 **Exclude Replica**  
 To prevent this availability replica from ever being be chosen for performing backups. This is useful, for example, for a remote availability replica to which you never want backups to fail over.  
  
##  <a name="Listener"></a> Listener Tab  
 Specify your preference for an[availability group listener](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)that will provide a client connection point, one of:  
  
 **Do not create an availability group listener now.**  
 Select to skip this step. You can create a listener later. For more information, see [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).  
  
 **Create an availability group listener.**  
 Specify your listener preferences for this availability group, as follows:  
  
 **Listener DNS Name**  
 Specify the network name of the listener. This name must be unique on the domain and can contain only alphanumeric characters, dashes (**-**), and hyphens (**_**), in any order. When specified by using the **Listener** tab, the DNS name can up to 15 characters long.  
  
> [!IMPORTANT]  
>  If you enter an invalid DNS listener name (or port number) on the **Listener** tab, the **Next** button is disabled on the **Specify Replicas** page.  
  
 **Port**  
 Specify the TPC port used by this listener.  
  
> [!NOTE]  
>  If you enter an invalid port number (or DNS listener name) on the **Listener** tab, the **Next** button is disabled on the **Specify Replicas** page.  
  
 **Network Mode**  
 Use the drop list to select the network mode to be used by this listener, one of:  
  
 **Static IP**  
 Select if you want the listener to listen on more than one subnet. To use the static IP network mode, an availability group listener must listen on every subnet that hosts an availability replica for the availability group. For each subnet, click **Add** to select a subnet address and to specify an IP address.  
  
 If **Static IP** is selected as the network mode (this is the default selection), a grid displays the **Subnet** and **IP Address** columns, and the associated **Add** and **Remove** buttons are displayed. The grid is empty until you add the first subnet.  
  
 **Subnet** column  
 Displays the subnet address that you selected for each subnet you have added for the listener.  
  
 **IP Address** column  
 Displays the IPv4 or IPv6 address that you specified for a given subnet.  
  
 **Add**  
 Click to add a subnet to this listener. This opens the **Add IP Address** dialog box. For more information, see the [Add IP Address Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/add-ip-address-dialog-box-sql-server-management-studio.md) help topic.  
  
 **Remove**  
 Click to remove the subnet that is currently selected in the grid.  
  
 **DHCP**  
 Select if you want the listener to listen on a single subnet and to use a dynamic IPv4 address that is assigned by a server running the Dynamic Host Configuration Protocol (DHCP). DHCP is limited to a single subnet that is common to every server instance that host an availability replica for the availability group. DHCP is not available for cluster type external or none.  
  
> [!IMPORTANT]  
>  We do not recommend DHCP in production environment. If there is a down time and the DHCP IP lease expires, extra time is required to register the new DHCP network IP address that is associated with the listener DNS name and impact the client connectivity. However, DHCP is good for setting up your development and testing environment to verify basic functions of availability groups and for integration with your applications.  
  
 When **DHCP** is selected, the **Subnet** field is displayed.  
  
 **Subnet**  
 If you selected **DHCP** as the network mode, use the **Subnet** drop list to select an address for the subnet that hosts the availability replicas of the availability group.  
  
> [!IMPORTANT]
>  After you define an availability group listener, we strongly recommend that you do the following:  
> 
>  -   Ask your network administrator to reserve the listener's IP address for its exclusive use. Give the listener's DNS host name to application developers to use in connection strings when requesting client connections to this availability group.  
> -   Give the listener's DNS host name to application developers to use in connection strings when requesting client connections to this availability group.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the Add Replica to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
-   [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../../t-sql/statements/create-endpoint-transact-sql.md)  
  
-   [Create a Database Mirroring Endpoint for Always On Availability Groups &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../../t-sql/statements/create-availability-group-transact-sql.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)  
  
