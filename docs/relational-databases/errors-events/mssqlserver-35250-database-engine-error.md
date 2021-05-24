---
description: "MSSQLSERVER_35250"
title: "MSSQLSERVER_35250 | Microsoft Docs"
ms.custom: ""
ms.date: "05/06/2021"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "35250 (Database Engine error)"
ms.assetid: 
author: pijocoder
ms.author: mathoma
---
# MSSQLSERVER_35250
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|35250|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HADR_PRIMARYNOTACTIVE|  
|Message Text|The connection to the primary replica is not active.  The command cannot be processed.|  
  
## Explanation  
This message occurs when attempting to join secondary databases to an Always On availability group. Inability to connect to the endpoint can typically cause this error.
  
## User Action  

> [!NOTE]  
> All the following steps must be run on both the Primary replica and the problematic Secondary replica(s).

1. Ensure the endpoint is created and started. Run the following query to discover the endpoint

    ```sql
    SELECT
      tep.name as EndPointName,
      sp.name As CreatedBy,
      tep.type_desc,
      tep.state_desc,
      tep.port
    FROM
      sys.tcp_endpoints tep
    INNER JOIN sys.server_principals sp ON tep.principal_id = sp.principal_id
    WHERE tep.type = 4
    ```

    > [!WARNING]
    > Use caution when executing the next command as it can cause a momentary downtime for the replica. 

    You can use these commands to restart the endpoint you discovered

    ```sql
    ALTER ENDPOINT hadr_endpoint STATE = STOPPED
    ALTER ENDPOINT hadr_endpoint STATE = STARTED
    ```

2. Check if you can connect to the endpoint
    - Use Telnet to validate connectivity. Here are examples of commands you can use:

    ```DOS
    telnet ServerName Port
    telnet IP_Address Port
    ```

    - If the Endpoint is listening and connection is successful, then you will see a blank screen.  If not, you will receive a connection error from Telnet
    - If Telnet connection to the IP address works but to the ServerName it does not, there is likely a DNS or name resolution issue
    - If connection works by ServerName and not by IP address, then there could be more than one endpoint defined on that server (another SQL instance perhaps) that is listening on that port. Though the status of the endpoint on the instance in question shows "STARTED" another instance may actually have the port binding and prevent the correct instance from listening and establishing TCP connections.
    - If Telnet fails to connect, look for Firewall and/or Anti-virus software that may be blocking the endpoint port in question. Check the firewall setting to see if it allows the endpoint port communication between the server instances that host primary replica and the secondary replica (port 5022 by default).
    Run the following PowerShell script to examine for disabled inbound traffic rules
    - If Telnet fails to connect, look for Firewall and/or antivirus software that may be blocking the endpoint port in question. If you are running SQL Server on Azure VM, additionally you would need to [ensure Network Security Group (NSG) allows the traffic to endpoint port](https://docs.microsoft.com/azure/virtual-machines/windows/nsg-quickstart-portal#create-an-inbound-security-rule). Check the firewall (and NSG, for Azure VM) setting to see if it allows the endpoint port communication between the server instances that host primary replica and the secondary replica (port 5022 by default)

    ```powershell
    Get-NetFirewallRule -Action Block -Enabled True -Direction Inbound |Format-Table
    ```

    - Capture a NETSTAT -a output and verify the status is a LISTENING or ESTABLISHED on the IP:Port for the endpoint specified

    ```dos
    netstat -a
    ```  

3. Check for errors in the system. You can query the **sys.dm_hadr_availability_replica_states** for the last_connect_error_number that may help you diagnose the join issue. Depending on which replica was having difficulty communicating, you can query both the primary and secondary:

    ```sql
    select
      r.replica_server_name,
      r.endpoint_url,
      rs.connected_state_desc,
      rs.last_connect_error_description,
      rs.last_connect_error_number,
      rs.last_connect_error_timestamp
    from
      sys.dm_hadr_availability_replica_states rs
      join sys.availability_replicas r on rs.replica_id = r.replica_id
    where
      rs.is_local = 1
    ```

    For example, if the secondary was unable to communicate with the DNS server or if a replica's endpoint_url was configured incorrectly when creating the availability group, you may get the following results in the last_connect_error_description:

    `DNS Lookup failed with error '11001(No such host is known)' `

4. Ensure the endpoint is configured for the correct IP/port that AG is defined for.
    - Run the following query on the Primary and then each Secondary replica that is failing to connect. This will help you find the endpoint URL and port

     ```sql 
     select endpoint_url from sys.availability_replicas
     ```

    - Run the following query to find the endpoints and ports

     ```sql
     SELECT
       tep.name as EndPointName,
       sp.name As CreatedBy,
       tep.type_desc,
       tep.state_desc,
       tep.port
     FROM
       sys.tcp_endpoints tep
       INNER JOIN sys.server_principals sp ON tep.principal_id = sp.principal_id
     WHERE
       tep.type = 4
     ```

    - Compare endpoint_url and port from each query and ensure the port from the endpoint_url matches the port defined for the endpoint on each respective replica

   > [!NOTE]  
   > If you are using specific IP addresses for the endpoint to listen on, versus the default of “listen all”, then you may have to define URLs that use the specific IP address rather than the FQDN.

5. Check whether the network service account has CONNECT permission to the endpoint. Run the following queries to list the accounts that have connect permission to the endpoint on the server(s) in question, and to show the permission assigned to each relevant endpoint.

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
      AND tep.type = 4;

    SELECT 
      ep.name, 
      sp.state,
      CONVERT(nvarchar(38), suser_name(sp.grantor_principal_id)) AS grantor,
      sp.TYPE AS permission,
      CONVERT(nvarchar(46),suser_name(sp.grantee_principal_id)) AS grantee
    FROM sys.server_permissions SP 
      INNER JOIN sys.endpoints ep  ON sp.major_id = ep.endpoint_id
    AND EP.type = 4
    ORDER BY Permission,grantor, grantee;   
    ```

6. Check for possible name resolution issues
    - Validate DNS resolution by using NSLookup on the IP address and the name:

     ```DOS
     nslookup IP_Address
     nslookup ServerName
     ```

    - Does the name resolve to the correct IP address? Does the IP address resolve to the correct name?
    - Check for local HOSTS file entries on each node that may be pointing to an incorrect server. From Command Prompt print the HOSTS file using this:

     ```DOS
     type C:\WINDOWS\system32\drivers\etc\hosts
     ```

    - Check if there are [Server Aliases for Use by a Client](../../database-engine/configure-windows/create-or-delete-a-server-alias-for-use-by-a-client.md) defined on the replicas
  
7. Ensure your SQL Server is running a recent build (preferably the [latest build](https://docs.microsoft.com/troubleshoot/sql/general/determine-version-edition-update-level#latest-updates-available-for-currently-supported-versions-of-sql-server) to protect from running into issues like [KB3213703](https://support.microsoft.com/topic/kb3213703-fix-an-always-on-secondary-replica-goes-into-a-disconnecting-state-10131118-b63a-f49f-b140-907f77774dc2).

  
For more information, refer to [Create Availability Group Fails With Error 35250 'Failed to join the database'](https://techcommunity.microsoft.com/t5/sql-server-support/create-availability-group-fails-with-error-35250-failed-to-join/ba-p/317987)  
