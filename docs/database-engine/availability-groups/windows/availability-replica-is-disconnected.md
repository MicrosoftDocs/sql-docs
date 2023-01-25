---
title: "Availability replica is disconnected in an availability group"
description: "Identify possible causes for why a replica is disconnected within an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.date: "07/28/2022"
ms.service: sql
ms.subservice: availability-groups
ms.topic: troubleshooting
ms.custom: seodec18
f1_keywords:
  - "sql13.swb.agdashboard.arp2connected.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---

# Availability replica is disconnected within an Always On availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
    
## Introduction  
  
- **Policy Name**: Availability Replica Connection State
- **Issue**: Availability replica is disconnected.
- **Category**: **Critical**
- **Facet**: Availability replica  
  
## Description  

This policy checks the connection state between availability replicas. The policy is in an unhealthy state when the connection state of the availability replica is DISCONNECTED. The policy is otherwise in a healthy state.  
  
## Possible Causes

 The secondary replica isn't connected to the primary replica. The connected state is DISCONNECTED. This issue can be caused by the following:  
  
- The connection port might be in conflict with another application.  
  
- The encryption type or algorithm is mismatched.  
  
- The connection endpoint has been deleted or hasn't been started.  
  
- There are network/connectivity issues or Ports are blocked at the firewall.  

- Service/startup account isn't a domain user and  isn't able to connect to the DC and to the remote node and port (for example, 5022) 
 
## Possible Solutions  

Check the database mirroring endpoint configuration for the instances of the primary and secondary replica and update the mismatched configuration. Also, check if the port is conflicting, and if so, change the port number. 

The following are possible solutions for this issue:  
 
- The connection port might be in conflict with another application.

   Run the following commands to diagnose port issue:
   
   ```PowerShell  
   $server_name = "server_instance"  #replace with your SQL Server instance
   Sqlcmd -S $server_name -E -Q "SELECT type_desc, port FROM sys.tcp_endpoints WHERE type_desc = 'DATABASE_MIRRORING'; "
   ```
   The above command will return the port number that you have to use in below command.
   
   ```PowerShell
   $port = "5022"
   Get-NetTCPConnection -LocalPort $port
   Get-Process -Id (Get-NetTCPConnection -LocalPort $port).OwningProcess |Select-Object Name, ProductVersion, Path, Id
   ```

- The encryption type or algorithm is mismatched.

   Run this on both servers and compare the encryption and make sure both are same:

   ```PowerShell
   $server_name = "server_instance"  #replace with your SQL Server instance
   sqlcmd -S $server_name -E -Q "SELECT name, state_desc, encryption_algorithm_desc, protocol_desc, type_desc  FROM sys.database_mirroring_endpoints"
   ```

- The connection endpoint has been deleted or hasn't been started.
	
   Run the following command if the mirroring endpoint exits and is started.
	
   ```PowerShell
   $server_name = "server_instance" #replace with your SQL Server instance
   Sqlcmd -S $server_name -E -Q "SELECT name, state_desc, encryption_algorithm_desc, protocol_desc, type_desc  FROM sys.database_mirroring_endpoints"
   ```

   Run the below command if you suspect that endpoint is not responding to connections or is not running. 
  
  
   ```PowerShell
   $server_name = "server_instance" #use your SQL Server instance here
   $server_name = "hadr_endpoint" #replace with your endpoint name
   Sqlcmd -S $server_name -E -Q "ALTER ENDPOINT hadr_endpoint STATE = stopped"
   Sqlcmd -S $server_name -E -Q "ALTER ENDPOINT hadr_endpoint STATE = started"
  ```
  >[!WARNING]
  >Running the command with `STATE = stopped` will stop your endpoint and temporarily interrupt Always On traffic flow.
  
  
   
- There are network /connectivity issues or Ports are blocked at the firewall

   Use the following commands to test connectivity in both directions from Node1 to Node2 and Node2 to Node1:

   ```PowerShell
   $computer = $env:computername
   $port = "5022"                 # replace with the port from your database_mirroring_endpoints.
   Test-NetConnection -ComputerName $computer -Port $port 
   ```

- Service/startup account is not a domain user and  is not able to connect to the DC and to the remote node and port (for example, 5022) 

   To test whether the service account can connect to the remote node, follow these steps. The steps assume that you are not logged in with the service account:

   1. Select **Start** > **Windows PowerShell** > right-click the icon.
   1. Select **More** > **Run as Different User** > **Use a different account**.
   1. Type the service account name and password.
   1. After Windows PowerShell opens, type the following command to verify you've logged in with the service account:
   
      ```PowerShell	
      whoami
      ```
	
   1. Then you can test the connection to the remote node. For example:

      ```PowerShell
      $computer = "remote_node" # Replace with Naode name as per your environment.
      $port = "5022"            # Replace with the port from your database_mirroring_endpoints.        
      Test-NetConnection -ComputerName $computer -Port 5022
      ```
  
## See also  
- [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
- [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
