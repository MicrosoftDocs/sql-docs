---
title: "Network Protocols and Network Libraries"
description: A server can be configured to monitor multiple network protocols. You can change the configuration using the SQL Server Configuration Manager.
author: adbadram
ms.author: adbadram
ms.date: 09/07/2025
ms.service: sql
ms.subservice: install
ms.topic: how-to
helpviewer_keywords:
  - "protocols [SQL Server]"
  - "configuration options [SQL Server], protocols"
  - "network libraries [SQL Server]"
  - "protocols [SQL Server], about network protocols"
  - "pipes [SQL Server]"
  - "network protocols [SQL Server]"
  - "default SQL Server configurations"
  - "library [SQL Server]"
  - "network protocols [SQL Server], about network protocols"
  - "configuration options [SQL Server], libraries"
---
# Network protocols and network libraries

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

A server can listen on, or monitor, multiple network protocols at one time. However, each protocol must be configured. If a particular protocol isn't configured, the server can't listen on that protocol. After installation, you can change the protocol configurations using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

## Default SQL Server network configuration

A default instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is configured for TCP/IP port 1433, and named pipe \\\\.\pipe\sql\query. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] named instances are configured for TCP dynamic ports, with a port number assigned by the operating system.

If you can't use dynamic port addresses (for example, when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connections must pass through a firewall server configured to pass through specific port addresses). Select an unassigned port number. Port number assignments are managed by the Internet Assigned Numbers Authority and are listed at [https://www.iana.org](https://go.microsoft.com/fwlink/?LinkId=48844).

To enhance security, network connectivity isn't fully enabled when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed. To enable, disable, and configure network protocols after Setup is complete, use the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Network Configuration area of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

## Server message block protocol

Servers in the perimeter network should have all unnecessary protocols disabled, including Server Message Block (SMB). Web servers and Domain Name System (DNS) servers don't require SMB. This protocol should be disabled to counter the threat of user enumeration.

### Effects of disabling SMB

Disabling SMB blocks the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Windows Cluster service from accessing the remote file share. Don't disable SMB if you do or plan to do one of the following:

- Use Windows Cluster Node and File Share Majority Quorum mode
- Specify an SMB file share as the data directory during [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation
- Create a database file on an SMB file share

### Disable SMB

1. On the **Start** menu, point to **Settings**, and then select **Network and Dial-up Connections**.

   Right-click the Internet-facing connection, and then select **Properties**.

1. Select the **Client for Microsoft Networks** check box, and then select **Uninstall**.

1. Follow the uninstall steps.

1. Select **File and Printer Sharing for Microsoft Networks**, and then select **Uninstall**.

1. Follow the uninstall steps.

### Disable SMB on servers accessible from the internet

- In the Local Area Connection properties, use the **Transmission Control Protocol/Internet Protocol (TCP/IP) properties** dialog box to remove **File and Printer Sharing for Microsoft Networks** and **Client for Microsoft Networks**.

## Endpoints

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] introduces a new concept for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connections; the connection is represented on the server end by a [!INCLUDE [tsql](../../includes/tsql-md.md)] *endpoint*. Permissions can be granted, revoked, and denied for [!INCLUDE [tsql](../../includes/tsql-md.md)] endpoints. By default, all users have permissions to access an endpoint unless the permissions are denied or revoked by a member of the sysadmin group or by the endpoint owner. The `GRANT`, `REVOKE`, and `DENY ENDPOINT` syntax uses an endpoint ID that the administrator must get from the endpoint's catalog view.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup creates [!INCLUDE [tsql](../../includes/tsql-md.md)] endpoints for all supported network protocols, and for the dedicated administrator connection.

[!INCLUDE [tsql](../../includes/tsql-md.md)] endpoints created by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup are as follows:

- [!INCLUDE [tsql](../../includes/tsql-md.md)] local machine
- [!INCLUDE [tsql](../../includes/tsql-md.md)] named pipes
- [!INCLUDE [tsql](../../includes/tsql-md.md)] default TCP

For more information about endpoints, see [Configure the Database Engine to listen on multiple TCP ports](../../database-engine/configure-windows/configure-the-database-engine-to-listen-on-multiple-tcp-ports.md) and [Endpoints Catalog Views](../../relational-databases/system-catalog-views/endpoints-catalog-views-transact-sql.md).

For more information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] network configurations, see [Server network configuration](../../database-engine/configure-windows/server-network-configuration.md).

## Related content

- [Surface area configuration](../../relational-databases/security/surface-area-configuration.md)
- [Security considerations for a SQL Server installation](security-considerations-for-a-sql-server-installation.md)
- [Plan a SQL Server installation](planning-a-sql-server-installation.md)
