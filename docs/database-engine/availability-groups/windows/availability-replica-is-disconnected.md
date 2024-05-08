---
title: "Availability replica is disconnected in an availability group"
description: "Identify possible causes for why a replica is disconnected within an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/08/2024
ms.service: sql
ms.subservice: availability-groups
ms.topic: troubleshooting
f1_keywords:
  - "sql13.swb.agdashboard.arp2connected.issues.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], policies"
---

# Availability replica is disconnected within an Always On availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

## Introduction

- **Policy Name**: Availability Replica Connection State
- **Issue**: Availability replica is disconnected
- **Category**: **Critical**
- **Facet**: Availability replica

## Description

This policy checks the connection state between availability replicas. The policy is in an unhealthy state when the connection state of the availability replica is `DISCONNECTED`. The policy is otherwise in a healthy state.

## Possible causes

The secondary replica isn't connected to the primary replica. The connected state is `DISCONNECTED`. This issue can be caused by one of the following reasons:

- [The connection port might be in conflict with another application](#the-connection-port-might-be-in-conflict-with-another-application).
- [The encryption type or algorithm is mismatched](#the-encryption-type-or-algorithm-is-mismatched).
- [The connection endpoint was deleted or isn't started](#the-connection-endpoint-was-deleted-or-isnt-started).
- [There are network/connectivity issues or Ports are blocked at the firewall](#network-or-connectivity-issues-or-ports-are-blocked-at-the-firewall).
- [Service/startup account isn't a domain user and isn't able to connect to the DC and to the remote node and port (for example, 5022)](#account-isnt-a-domain-user-and-cant-connect-to-the-dc-and-to-the-remote-node).

## Possible solutions

Check the database mirroring endpoint configuration for the instances of the primary and secondary replica and update the mismatched configuration. Also, check if the port is conflicting, and if so, change the port number.

The following are possible solutions for this issue:

#### The connection port might be in conflict with another application

Run the following commands to diagnose port issues:

```powershell
$server_name = "server_instance"  #replace with your SQL Server instance
sqlcmd -S $server_name -E -Q "SELECT type_desc, port FROM sys.tcp_endpoints WHERE type_desc = 'DATABASE_MIRRORING'; "
```

The previous command returns the port number that you have to use in the following command.

```powershell
$port = "5022"
Get-NetTCPConnection -LocalPort $port
Get-Process -Id (Get-NetTCPConnection -LocalPort $port).OwningProcess | Select-Object Name, ProductVersion, Path, Id
```

#### The encryption type or algorithm is mismatched

Run this command on both servers and compare the encryption and make sure both are the same.

```powershell
$server_name = "server_instance"  #replace with your SQL Server instance
sqlcmd -S $server_name -E -Q "SELECT name, state_desc, encryption_algorithm_desc, protocol_desc, type_desc FROM sys.database_mirroring_endpoints"
```

#### The connection endpoint was deleted or isn't started

Run the following command if the mirroring endpoint exits and is started.

```powershell
$server_name = "server_instance"  #replace with your SQL Server instance
sqlcmd -S $server_name -E -Q "SELECT name, state_desc, encryption_algorithm_desc, protocol_desc, type_desc FROM sys.database_mirroring_endpoints"
```

Run the following command if you suspect that endpoint isn't responding to connections, or isn't running.

```powershell
$server_name = "server_instance"  #use your SQL Server instance here
$server_name = "hadr_endpoint"    #replace with your endpoint name
sqlcmd -S $server_name -E -Q "ALTER ENDPOINT hadr_endpoint STATE = stopped"
sqlcmd -S $server_name -E -Q "ALTER ENDPOINT hadr_endpoint STATE = started"
```

> [!WARNING]  
> Running the command with `STATE = stopped` will stop your endpoint and temporarily interrupt Always On traffic flow.

#### Network or connectivity issues, or ports are blocked at the firewall

Use the following commands to test connectivity in both directions from `Node1` to `Node2` and `Node2` to `Node1`:

```powershell
$computer = "remote_node"      # replace with node name in your environment
$port = "5022"                 # replace with the port from your database_mirroring_endpoints
Test-NetConnection -ComputerName $computer -Port $port
```

#### Account isn't a domain user and can't connect to the DC, and to the remote node

To test whether the service account can connect to the remote node, follow these steps. The steps assume that you aren't logged in with the service account.

1. Select **Start** > **Windows PowerShell** > right-click the icon.

1. Select **More** > **Run as Different User** > **Use a different account**.

1. Type the service account name and password.

1. After Windows PowerShell opens, type the following command to verify that you signed in with the service account:

   ```powershell
   whoami
   ```

1. Then you can test the connection to the remote node, as in the following example.

   ```powershell
   $computer = "remote_node" # replace with node name in your environment
   $port = "5022"            # replace with the port from your database_mirroring_endpoints
   Test-NetConnection -ComputerName $computer -Port $port
   ```

## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Use the Always On Availability Group dashboard (SQL Server Management Studio)](use-the-always-on-dashboard-sql-server-management-studio.md)
