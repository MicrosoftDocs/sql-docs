---
title: "Common issues & resolutions with availability groups"
description: Troubleshoot typical problems with configuring server instances for Always On availability groups in SQL Server.
ms.custom: seo-lt-2019
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: availability-groups
ms.topic: conceptual
helpviewer_keywords: 
  - "troubleshooting [SQL Server], deploying"
  - "Availability Groups [SQL Server], troubleshooting"
  - "Availability Groups [SQL Server], configuring"
ms.assetid: 8c222f98-7392-4faf-b7ad-5fb60ffa237e
author: cawrites
ms.author: chadam
---
# Troubleshoot Always On Availability Groups Configuration (SQL Server)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  This topic provides information to help you troubleshoot typical problems with configuring server instances for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. Typical configuration problems include [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] is disabled, accounts are incorrectly configured, the database mirroring endpoint does not exist, the endpoint is inaccessible (SQL Server Error 1418), network access does not exist, and a join database command fails (SQL Server Error 35250).  
  
> [!NOTE]  
>  Ensure that you are meeting the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] prerequisites. For more information, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
 **In This Topic:**  
  
|Section|Description|  
|-------------|-----------------|  
|[Always On Availability Groups Is Not Enabled](#IsHadrEnabled)|If an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is not enabled for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], the instance does not support availability group creation and cannot host any availability replicas.|  
|[Accounts](#Accounts)|Discusses requirements for correctly configuring the accounts under which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running.|  
|[Endpoints](#Endpoints)|Discusses how to diagnose issues with the database mirroring endpoint of a server instance.|  
|[Network access](#NetworkAccess)|Documents the requirement that each server instance that is hosting an availability replica must be able to access the port of each of the other server instances over TCP.|  
|[Endpoint Access (SQL Server Error 1418)](#Msg1418)|Contains information about this [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] error message.|  
|[Join Database Fails (SQL Server Error 35250)](#JoinDbFails)|Discusses the possible causes and resolution of a failure to join secondary databases to an availability group because the connection to the primary replica is not active.|  
|[Read-Only Routing is Not Working Correctly](#ROR)||  
|[Related Tasks](#RelatedTasks)|Contains a list of task-oriented topics in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] Books Online that are particularly relevant to troubleshooting an availability group configuration.|  
|[Related Content](#RelatedContent)|Contains a list of relevant resources that are external to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.|  
  
##  <a name="IsHadrEnabled"></a> Always On Availability Groups Is Not Enabled  
 The [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature must be enabled on each of the instances of [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)]. For more information, see [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).  
  
##  <a name="Accounts"></a> Accounts  
 The accounts under which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running must be correctly configured.  
  
1.  Do the accounts have the correct permissions?  
  
    1.  If the partners run under the same domain account, the correct user logins exist automatically in both **master** databases. This simplifies the security configuration and is recommended.  
  
    2.  If two server instances run under different accounts, the each account must be created in **master** on the remote server instance, and that login must be granted CONNECT permissions to connect to the database mirroring endpoint of that server instance. For more information, see[Set Up Login Accounts for Database Mirroring or Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/database-mirroring/set-up-login-accounts-database-mirroring-always-on-availability.md).  You can use the following query on each instance to check if the logins have CONNECT permissions:

    ```sql
    SELECT 
      perm.class_desc,
      prin.name,
      perm.permission_name,
      perm.state_desc,
      prin.type_desc as PrincipalType,
      prin.is_disabled
    FROM sys.server_permissions perm
      LEFT JOIN sys.server_principals prin ON perm.grantee_principal_id = prin.principal_id
      LEFT JOIN sys.tcp_endpoints tep ON perm.major_id = tep.endpoint_id
    WHERE 
      perm.class_desc = 'ENDPOINT'
      AND perm.permission_name = 'CONNECT'
      AND tep.type = 4    
    ```

  
2.  If [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running under a built-in account, such as Local System, Local Service, or Network Service, or a nondomain account, you must use certificates for endpoint authentication. If your service accounts are using domain accounts in the same domain, you can choose to grant CONNECT access for each service account on all the replica locations or you can use certificates. For more information, see[Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md).  
  
##  <a name="Endpoints"></a> Endpoints  
 Endpoints must be correctly configured.  
  
1.  Make sure that each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that is going to host an availability replica (each *replica location*) has a database mirroring endpoint. To determine whether a database mirroring endpoint exists on a given server instance, use the [sys.database_mirroring_endpoints](../../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md) catalog view:

    ```sql
    SELECT name, state_desc FROM sys.database_mirroring_endpoints  
    ```

    For more information on creating endpoints, see either [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md) or [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md).  
  
2.  Check that the port numbers are correct.  
  
     To identify the port currently associated with database mirroring endpoint of a server instance, use the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement:  
  
    ```sql
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
  
    ```sql  
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
    ```  

6. Ensure correct server name is used in the endpoint URL

    For server name in an endpoint URL, it is recommended to use fully qualified domain name (FQDN), although you can use any name that uniquely identifies the machine. The server address can be a Netbios name (if the systems are in the same domain), a fully qualified domain name (FQDN), or an IP address (preferably, a static IP address). Using the fully qualified domain name is the recommended option. 

    If you have already defined an Endpoint URL, you can query it by using:

    ```sql
    select endpoint_url from sys.availability_replicas
    ```

    Next, compare the endpoint_url output to the server name (Netbios or FQDN).
    To query the Netbios and FQDN, run the following in a Command Prompt on the replica locally:

    ```dos
    hostname & echo %COMPUTERNAME%.%USERDNSDOMAIN%
    ```

    For query the server name of a remote computer, run this from a Command Prompt. Then compare the endpoint_url

    ```dos
    ping -a servername_from_endpoint_url
    ```


    For more information, see [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md).  
  
##  <a name="NetworkAccess"></a> Network Access  
 Each server instance that is hosting an availability replica must be able to access the port of each of the other server instance over TCP. This is especially important if the server instances are in different domains that do not trust each other (untrusted domains).  Check if you can connect to the endpoints by following these steps:

- Use Telnet to validate connectivity. Here are examples of commands you can use:

   ```DOS
   telnet ServerName Port
   telnet IP_Address Port

- If the Endpoint is listening and connection is successful, then you will see a blank screen.  If not, you will receive a connection error from Telnet
- If Telnet connection to the IP address works but to the ServerName it does not, there is likely a DNS or name resolution issue
- If connection works by ServerName and not by IP address, then there could be more than one endpoint defined on that server (another SQL instance perhaps) that is listening on that port. Though the status of the endpoint on the instance in question shows "STARTED" another instance may actually have the port binding and prevent the correct instance from listening and establishing TCP connections.
- If Telnet fails to connect, look for Firewall and/or Anti-virus software that may be blocking the endpoint port in question. Check the firewall setting to see if it allows the endpoint port communication between the server instances that host primary replica and the secondary replica (port 5022 by default).
Run the following PowerShell script to examine for disabled inbound traffic rules
- If Telnet fails to connect, look for Firewall and/or antivirus software that may be blocking the endpoint port in question. If you are running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Azure VM, additionally you would need to [ensure Network Security Group (NSG) allows the traffic to endpoint port](https://docs.microsoft.com/azure/virtual-machines/windows/nsg-quickstart-portal#create-an-inbound-security-rule). Check the firewall (and NSG, for Azure VM) setting to see if it allows the endpoint port communication between the server instances that host primary replica and the secondary replica (port 5022 by default)

   ```powershell
   Get-NetFirewallRule -Action Block -Enabled True -Direction Inbound |Format-Table

- Capture a NETSTAT -a output and verify the status is a LISTENING or ESTABLISHED on the IP:Port for the endpoint specified

   ```dos
   netstat -a

  
##  <a name="Msg1418"></a> Endpoint Access (SQL Server Error 1418)  
 This [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] message indicates that the server network address specified in the endpoint URL cannot be reached or does not exist, and it suggests that you verify the network address name and reissue the command.  
  
##  <a name="JoinDbFails"></a> Join Database Fails (SQL Server Error 35250)  
 This section discusses the possible causes and resolution of a failure to join secondary databases to the availability group because the connection to the primary replica is not active. This is the full error message:

`Msg 35250 The connection to the primary replica is not active.  The command cannot be processed.`

 **Resolution:**  
 
Summary of steps is outlined below. For detailed step-by-step instructions, please refer to Engine error [MSSQLSERVER_35250](../../../relational-databases/errors-events/mssqlserver-35250-database-engine-error.md)

1. Ensure the endpoint is created and started. 
2. Check if you can connect to the endpoint via Telnet and ensure no firewall rules are blocking connectivity
3. Check for errors in the system. You can query the **sys.dm_hadr_availability_replica_states** for the last_connect_error_number that may help you diagnose the join issue.
4. Ensure the endpoint is defined so it correctly matches the IP/port that AG is using.
5. Check whether the network service account has CONNECT permission to the endpoint.
6. Check for possible name resolution issues
7. Ensure your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is running a recent build (preferably the [latest build](https://docs.microsoft.com/troubleshoot/sql/general/determine-version-edition-update-level#latest-updates-available-for-currently-supported-versions-of-sql-server) to protect from running into fixed issues.

## <a name="ROR"></a> Read-Only Routing is Not Working Correctly  

1. Ensure that you have set up read-only routing by following [Configure read-only routing](https://docs.microsoft.com/sql/database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server) document.

2. Ensure Client Driver Support

    The client application must use a client providers that support `ApplicationIntent` parameter. See [Driver and client connectivity support for availability groups](https://docs.microsoft.com/sql/database-engine/availability-groups/windows/always-on-client-connectivity-sql-server)

   > [!NOTE]  
   > If you are connecting to a DNN Listener, the provider must also support `MultiSubnetFailover` parameter

3. Ensure connection string properties are set correctly

    For read-only routing to work properly, your client application must use these properties in the connection string:
  
   - A database name that belongs to AG: `Initial Catalog=<db_in_avail_group>`
   - An availability group listener name
      - If you are using DNN, you must specify DNN listener name and DNN port number `<DNN name, DNN port>`
   - ApplicationIntent set to ReadOnly `ApplicationIntent=ReadOnly`
   - MultiSubnetFailover set to true is required for Distributed network name (DNN)

    ### Example for a virtual network name (VNN) listener:

   `Server=tcp:VNN_AgListener,1433;Database=AgDb1;ApplicationIntent=ReadOnly;MultiSubnetFailover=True`

   ### Example for a distributed network name (DNN) listener:

   `Server=tcp:DNN_AgListener,DNN_Port;Database=AgDb1;ApplicationIntent=ReadOnly;MultiSubnetFailover=True`

   > [!NOTE]  
   > If you are using command line programs like SQLCMD, ensure that you specify the correct switches for server name. For instance, in SQLCMD the `-S` switch specifies the server name, whereas lower-case `-s` parameter specifies column separator.
   > </br>Example: `sqlcmd -S Listerne1,port -E -d AgDb1 -K ReadOnly -M`

4. Ensure that the availability group listener is online. To ensure that the availability group listener is online run the following query on the primary replica: 

    ```sql
   SELECT * FROM sys.dm_tcp_listener_states;
    ```

   If you find the listener is offline you can attempt to bring it online using a command like this:

   ```sql
   ALTER AVAILABILITY GROUP myAG RESTART LISTENER 'myAG_Listener';
   ```

5. Ensure READ_ONLY_ROUTING_LIST is correctly populated. On Primary replica, ensure that the READ_ONLY_ROUTING_LIST contains only server instances that are hosting a readable secondary replica.

   ```sql
   SELECT replica_id, replica_server_name, secondary_role_allow_connections_desc, read_only_routing_url FROM sys.availability_replicas;   
   ```

   To view a read-only routing list:

   ```sql
   SELECT * FROM sys.availability_read_only_routing_lists;
   ```

   To change a read-only routing list you can use a query like this:

   ```sql
   ALTER AVAILABILITY GROUP [AG1]   
   MODIFY REPLICA ON  
   N'COMPUTER02' WITH   
   (PRIMARY_ROLE (READ_ONLY_ROUTING_LIST=('COMPUTER01','COMPUTER02')));  
   ```
  
   For more information see [Configure read-only routing for an availability group - SQL Server Always On](configure-read-only-routing-for-an-availability-group-sql-server.md)

6. Check that READ_ONLY_ROUTING_URL port is open. Ensure that the Windows firewall is not blocking the READ_ONLY_ROUTING_URL port. Configure a Windows Firewall for database engine access on every replica in the read_only_routing_list

   >[!NOTE]
   > If you are running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Azure VM, additionally you must ensure the network security group (NSG) allows the traffic to endpoint port. Check the firewall (and NSG, for Azure VM) setting to see if it allows the endpoint port communication between the server instances that host primary replica and the secondary replica (port 5022 by default)

7. Ensure that the READ_ONLY_ROUTING_URL (TCP://system-address:port) contains the correct fully-qualified domain name (FQDN) and port number. See:  
   - [Calculating read_only_routing_url for Always On](https://docs.microsoft.com/archive/blogs/mattn/calculating-read_only_routing_url-for-alwayson)
   - [sys.availability_replicas (Transact-SQL)](https://docs.microsoft.com/sql/relational-databases/system-catalog-views/sys-availability-replicas-transact-sql)
   - [ALTER AVAILABILITY GROUP (Transact-SQL)](https://docs.microsoft.com/sql/t-sql/statements/alter-availability-group-transact-sql)

8. Ensure proper SQL Server Networking configuration in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.

   Verify on every replica in the read_only_routing_list that:
   - [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] remote connectivity is enabled
   - TCP/IP is enabled
   - The IP addresses are configured correctly

    See: [Configure a Server to Listen on a Specific TCP Port (SQL Server Configuration Manager)](https://docs.microsoft.com/sql/database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port) and [View or Change Server Properties (SQL Server)](https://docs.microsoft.com/sql/database-engine/configure-windows/view-or-change-server-properties-sql-server)

    > [!NOTE]
    > You can quickly verify all of these are properly configured if you can connect from a client machine using target secondary replica's [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance name using "TCP:instance" syntax.

  
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
  
-   [Get-ClusterLog Failover Cluster Cmdlet](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ee461045(v=technet.10))  
  
-   [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)  
  
## See Also  
 [Transport Security for Database Mirroring and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md)   
 [Client Network Configuration](../../../database-engine/configure-windows/client-network-configuration.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)  
  
