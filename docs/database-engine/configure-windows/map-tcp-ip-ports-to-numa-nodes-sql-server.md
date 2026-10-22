---
title: "Map TCP IP Ports to NUMA Nodes (SQL Server)"
description: "Learn how to use SQL Server Configuration Manager to map TCP/IP ports to non-uniform memory access (NUMA) nodes. See how to create a node identification bitmap."
author: adbadram
ms.author: adbadram
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "NUMA"
  - "memory [SQL Server], NUMA"
  - "affinity NUMA"
  - "ports [SQL Server], working with NUMA"
  - "CPU [SQL Server], NUMA support"
  - "NUMA, How to map a port to a NUMA node"
  - "NUMA affinity"
  - "TCP/IP [SQL Server], NUMA support"
  - "non-uniform memory access"
---
# Map TCP IP Ports to NUMA Nodes (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to map TCP/IP ports to non-uniform memory access (NUMA) nodes by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. On startup, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] writes the node information to the error log.

To determine the node number of the node you want to use, either read the node information from the error log, or from the `sys.dm_os_schedulers` view. To set a TCP/IP address and port to single or multiple nodes, append a node identification bitmap (an affinity mask) in brackets after the port number. Nodes can be specified in either decimal or hexadecimal format. To create the bitmap, first number the nodes from right to left starting with zero, as in 76543210. Create a binary representation of the node list, providing 1 for nodes you want to use, and 0 for nodes you don't want to use. For example, to use NUMA nodes 0, 2, and 5, specify `00100101`.

```text
NUMA node number                            76543210
Mask for 0, 2, and 5 counting from right    00100101
```

Convert the binary representation (`00100101`), into decimal `[37]`, or hexadecimal `[0x25]`. To listen on all nodes, provide no node identifier.

If a port is mapped to more than one NUMA node, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] assigns connections to nodes in a round-robin fashion without attempting to balance load across the nodes.

> [!NOTE]  
> To enable [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to listen on multiple TCP ports for each IP address, see [Configure the Database Engine to listen on multiple TCP ports](configure-the-database-engine-to-listen-on-multiple-tcp-ports.md).

<a id="SSMSProcedure"></a>

## Use SQL Server Configuration Manager

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Network Configuration**, and then select **Protocols for** *\<instance name>*.

1. In the details pane, double-click **TCP/IP**.

1. On the **IP Addresses** tab, in the section corresponding to the IP address to configure, in the **TCP Port** box, add the NUMA node identifier in brackets after the port number. For example, for TCP port 1500 and nodes 0, 2, and 5, use `1500[37]`, or `1500[0x25]`.

## Related content

- [Soft-NUMA (SQL Server)](soft-numa-sql-server.md)
