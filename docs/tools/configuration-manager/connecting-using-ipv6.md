---
title: "Connecting Using IPv6"
description: Learn about support for IPv4 and IPv6 in SQL Server and SQL Server Native Client, and see how to configure the Database Engine for the address you want to use.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: how-to
ms.collection:
  - data-tools
helpviewer_keywords:
  - "Internet Protocol"
  - "IPv4"
  - "IPv6"
monikerRange: ">=sql-server-2017"
---
# Connect using IPv6

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client fully support both Internet Protocol version 4 (IPv4) and Internet Protocol version 6 (IPv6). When Windows is configured with IPv6 [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], components automatically recognize the existence of IPv6. No special [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] configuration is necessary.

## Supported functionality

Support includes, but isn't limited to, the following scenarios.

### IPv4 and IPv6 listener

The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and the other server components can listen on both IPv4 and IPv6 addresses at the same time. When both IPv4 and IPv6 are present, you can use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to configure the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to listen only on IPv4 addresses or only on IPv6 addresses.

### SQL Server Browser Service

When the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service running on a machine that supports both IPv4 and IPv6 is queried on an IPv4 address, it responds with an IPv4 address and the first IPv4 TCP port in its list. When queried on an IPv6 address, it responds with an IPv6 address and the first IPv6 TCP port in its list. To avoid inconsistency, configure the IPv4 and IPv6 listeners to listen to the same port.

### Client tools

Tools such as [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager accept both IPv4 and IPv6 formats for IP addresses. In most cases, the connection string doesn't need to be modified if the `<computer_name>\<instance_name>` is specified using server hostname or fully qualified domain name (FQDN).

If the server computer has both IPv4 and IPv6, its hostname or FQDN will be resolved into multiple IP addresses, including at least one IPv4 address and multiple IPv6 addresses. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client attempts to establish connections using these IP addresses in the order received from TCP/IP and uses the first connection that succeeds.

Because [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client can't predict the order, the resolution should be regarded as random order. IPv4 addresses are attempted first if both IPv4 and IPv6 addresses are present. This logic is transparent to the users of ODBC, OLE DB, or ADO.NET.

> [!NOTE]  
> If the [!INCLUDE [ssDE](../../includes/ssde-md.md)] isn't listening on IPv4, the attempted IPv4 connection must wait for the timeout period before the IPv6 address is attempted. To avoid this, connect directly to the IPv6 IP address or configure an alias on the client with the IPv6 address.

## Related content

- [SQL Server Configuration Manager](sql-server-configuration-manager.md)
