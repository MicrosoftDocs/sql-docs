---
title: "Troubleshoot Always On Availability Groups Configuration (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "troubleshooting [SQL Server], deploying"
  - "Availability Groups [SQL Server], troubleshooting"
  - "Availability Groups [SQL Server], configuring"
ms.assetid: 8c222f98-7392-4faf-b7ad-5fb60ffa237e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Troubleshoot Always On Availability Groups Configuration (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic provides information to help you troubleshoot typical problems with configuring server instances for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. Typical configuration problems include [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] is disabled, accounts are incorrectly configured, the database mirroring endpoint does not exist, the endpoint is inaccessible (SQL Server Error 1418), network access does not exist, and a join database command fails (SQL Server Error 35250).  
  
> [!NOTE]  
>  Ensure that you are meeting the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] prerequisites. For more information, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
 **In This Topic:**  
  
|Section|Description|  
|-------------|-----------------|  
|[Always On Availability Groups Is Not Enabled](#IsHadrEnabled)|If an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is not enabled for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], the instance does not support availability group creation and cannot host any availability replicas.|  
|[Accounts](#Accounts)|Discusses requirements for correctly configuring the accounts under which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running.|  
|[Endpoints](#Endpoints)|Discusses how to diagnose issues with the database mirroring endpoint of a server instance.|  
|[System name](#SystemName)|Summarizes the alternatives for specifying the system name of a server instance in an endpoint URL.|  
|[Network access](#NetworkAccess)|Documents the requirement that each server instance that is hosting an availability replica must be able to access the port of each of the other server instances over TCP.|  
|[Endpoint Access (SQL Server Error 1418)](#Msg1418)|Contains information about this [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] error message.|  
|[Join Database Fails (SQL Server Error 35250)](#JoinDbFails)|Discusses the possible causes and resolution of a failure to join secondary databases to an availability group because the connection to the primary replica is not active.|  
|[Read-Only Routing is Not Working Correctly](#ROR)||  
|[Related Tasks](#RelatedTasks)|Contains a list of task-oriented topics in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] Books Online that are particularly relevant to troubleshooting an availability group configuration.|  
|[Related Content](#RelatedContent)|Contains a list of relevant resources that are external to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.|  
  
##  <a name="IsHadrEnabled"></a> Always On Availability Groups Is Not Enabled  
 The [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature must be enabled on each of the instances of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. For more information, see [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).  
  
##  <a name="Accounts"></a> Accounts  
 The accounts under which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running must be correctly configured.  
  
1.  Do the accounts have the correct permissions?  
  
    1.  If the partners run as the same domain user account, the correct user logins exist automatically in both **master** databases. This simplifies the security configuration the database and is recommended.  
  
    2.  If two server instances run as different accounts, the login each account must be created in **master** on the remote server instance, and that login must be granted CONNECT permissions to connect to the database mirroring endpoint of that server instance. For more information, see[Set Up Login Accounts for Database Mirroring or Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/database-mirroring/set-up-login-accounts-database-mirroring-always-on-availability.md).  
  
2.  If [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running as a built-in account, such as Local System, Local Service, or Network Service, or a nondomain account, you must use certificates for endpoint authentication. If your service accounts are using domain accounts in the same domain, you can choose to grant CONNECT access for each service account on all the replica locations or you can use certificates. For more information, see[Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md).  
  
##  <a name="Endpoints"></a> Endpoints  
 Endpoints must be correctly configured.  
  
1.  Make sure that each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that is going to host an availability replica (each *replica location*) has a database mirroring endpoint. To determine whether a database mirroring endpoint exists on a given server instance, use the [sys.database_mirroring_endpoints](../../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md) catalog view. For more information, see either [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md) or [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md).  
  
2.  Check that the port numbers are correct.  
  
     To identify the port currently associated with database mirroring endpoint of a server instance, use the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT type_desc, port FROM sys.tcp_endpoints;  
    GO  
    ```  
  
3.  For [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] setup issues that are difficult to explain, we recommend that you inspect each server instance to determine whether it is listening on the correct ports.  
  
4.  Make sure that the endpoints are started (STATE=STARTED). On each server instance, use the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT state_desc FROM sys.database_mirroring_endpoints  
    ```  
  
     For more information about the **state_desc** column, see [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md).  
  
     To start an endpoint, use the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement:  
  
    ```  
    ALTER ENDPOINT Endpoint_Mirroring   
    STATE = STARTED   
    AS TCP (LISTENER_PORT = <port_number>)  
    FOR database_mirroring (ROLE = ALL);  
    GO  
    ```  
  
     For more information, see [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-endpoint-transact-sql.md).  
  
5.  Make sure that the login from the other server has CONNECT permission. To determine who has CONNECT permission for an endpoint, on each server instance use the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT 'Metadata Check';  
    SELECT EP.name, SP.STATE,   
       CONVERT(nvarchar(38), suser_name(SP.grantor_principal_id))   
          AS GRANTOR,   
       SP.TYPE AS PERMISSION,  
       CONVERT(nvarchar(46),suser_name(SP.grantee_principal_id))   
          AS GRANTEE   
       FROM sys.server_permissions SP , sys.endpoints EP  
       WHERE SP.major_id = EP.endpoint_id  
       ORDER BY Permission,grantor, grantee;   
    GO  
  
    ```  
  
##  <a name="SystemName"></a> System Name  
 For the system name of a server instance in an endpoint URL, you can use any name that unambiguously identifies the system. The server address can be a system name (if the systems are in the same domain), a fully qualified domain name, or an IP address (preferably, a static IP address). Using the fully qualified domain name is guaranteed to work. For more information, see [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md).  
  
##  <a name="NetworkAccess"></a> Network Access  
 Each server instance that is hosting an availability replica must be able to access the port of each of the other server instance over TCP. This is especially important if the server instances are in different domains that do not trust each other (untrusted domains).  
  
##  <a name="Msg1418"></a> Endpoint Access (SQL Server Error 1418)  
 This [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] message indicates that the server network address specified in the endpoint URL cannot be reached or does not exist, and it suggests that you verify the network address name and reissue the command.  
  
##  <a name="JoinDbFails"></a> Join Database Fails (SQL Server Error 35250)  
 This section discusses the possible causes and resolution of a failure to join secondary databases to the availability group because the connection to the primary replica is not active.  
  
 **Resolution:**  
  
1.  Check the firewall setting to see if whether allows the endpoint port communication between the server instances that host primary replica and the secondary replica (port 5022 by default).  
  
2.  Check whether the network service account has connect permission to the endpoint.  
  
##  <a name="ROR"></a> Read-Only Routing is Not Working Correctly  
 Verify the following configuration values settings and correct them if necessary.  
  
||On...|Action|Comments|Link|  
|------|---------|------------|--------------|----------|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Current primary replica|Ensure that the availability group listener is online.|**To verify whether the listener is online:**<br /><br /> `SELECT * FROM sys.dm_tcp_listener_states;`<br /><br /> **To restart an offline listener:**<br /><br /> `ALTER AVAILABILITY GROUP myAG RESTART LISTENER 'myAG_Listener';`|[sys.dm_tcp_listener_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-tcp-listener-states-transact-sql.md)<br /><br /> [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-availability-group-transact-sql.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Current primary replica|Ensure that the READ_ONLY_ROUTING_LIST contains only server instances that are hosting a readable secondary replica.|**To identify readable secondary replicas:** sys.availability_replicas  (**secondary_role_allow_connections_desc** column)<br /><br /> **To view a read-only routing list:** sys.availability_read_only_routing_lists<br /><br /> **To change a read-only routing list:** ALTER AVAILABILITY GROUP|[sys.availability_replicas &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)<br /><br /> [sys.availability_read_only_routing_lists &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-read-only-routing-lists-transact-sql.md)<br /><br /> [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-availability-group-transact-sql.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Every replica in the read_only_routing_list|Ensure that the Windows firewall is not blocking the READ_ONLY_ROUTING_URL port.|-|[Configure a Windows Firewall for Database Engine Access](../../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Every replica in the read_only_routing_list|In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager, verify that:<br /><br /> SQL Server remote connectivity is enabled.<br /><br /> TCP/IP is enabled.<br /><br /> The IP addresses are configured correctly.|-|[View or Change Server Properties &#40;SQL Server&#41;](../../../database-engine/configure-windows/view-or-change-server-properties-sql-server.md)<br /><br /> [Configure a Server to Listen on a Specific TCP Port &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Every replica in the read_only_routing_list|Ensure that the READ_ONLY_ROUTING_URL (TCP<strong>://</strong>*system-address*<strong>:</strong>*port*) contains the correct fully-qualified domain name (FQDN) and port number.|-|[Calculating read_only_routing_url for Always On](https://blogs.msdn.com/b/mattn/archive/2012/04/25/calculating-read-only-routing-url-for-Always%20On.aspx)<br /><br /> [sys.availability_replicas &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)<br /><br /> [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-availability-group-transact-sql.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Client system|Verify that the client driver supports read-only routing.|-|[Always On Client Connectivity &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-client-connectivity-sql-server.md)|  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
-   [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   [Troubleshoot a Failed Add-File Operation &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
  
-   [Management of Logins and Jobs for the Databases of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/logins-and-jobs-for-availability-group-databases.md)  
  
-   [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [View Events and Logs for a Failover Cluster](https://technet.microsoft.com/library/cc772342\(WS.10\).aspx)  
  
-   [Get-ClusterLog Failover Cluster Cmdlet](https://technet.microsoft.com/library/ee461045.aspx)  
  
-   [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)  
  
## See Also  
 [Transport Security for Database Mirroring and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md)   
 [Client Network Configuration](../../../database-engine/configure-windows/client-network-configuration.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)  
  
  
