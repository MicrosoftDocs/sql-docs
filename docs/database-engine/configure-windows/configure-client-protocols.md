---
title: "Configure Client Protocols"
description: Learn various ways of configuring the protocols that client applications use in SQL Server. Supported protocols include TCP/IP, named pipes, and shared memory.
author: adbadram
ms.author: adbadram
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: install-set-up-deploy
helpviewer_keywords:
  - "default protocols"
  - "network protocols [SQL Server], client configuration"
  - "TCP/IP [SQL Server], client protocols"
  - "disabling client protocols"
  - "ordering protocols [SQL Server]"
  - "protocols [SQL Server], order for client computers"
  - "configure client protocols"
  - "client protocols [SQL Server]"
  - "protocols [SQL Server], client configuration"
  - "default protocols, client"
---
# Configure client protocols

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure client protocols used by client applications in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports client communication with the TCP/IP network protocol and the named pipes protocol. The shared memory protocol is also available if the client is connecting to an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] on the same computer. There are three common methods of selecting the protocol.

- Configure all client applications to use the same network protocol by setting the protocol order in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

- Configure a single client application to use a different network protocol by creating an alias. For more information, see [Create or delete a server alias for use by a client](create-or-delete-a-server-alias-for-use-by-a-client.md).

- Some client applications, such as sqlcmd.exe, can specify the protocol as part of the connection string. For more information, see [Connect to SQL Server with sqlcmd](../../tools/sqlcmd/sqlcmd-connect-database-engine.md).

<a id="SSMSProcedure"></a>

## Use SQL Server Configuration Manager

<a id="EnableDisable"></a>

### Enable or disable a client protocol

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Native Client Configuration**, right-click **Client Protocols**, and then select **Properties**.

1. Select a protocol in the **Disabled Protocols** box, and then select **Enable**, to enable a protocol.

1. Select a protocol in the **Enabled Protocols** box, and then select **Disable**, to disable a protocol.

<a id="ChangeDefault"></a>

### Change the default protocol or the protocol order for client computers

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Native Client Configuration**, right-click **Client Protocols**, and then select **Properties**.

1. In the **Enabled Protocols** box, select **Move Up** or **Move Down**, to change the order in which protocols are tried, when attempting to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The top protocol in the **Enabled Protocols** box is the default protocol.

   [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager creates registry entries for the server alias configurations and default client network library. However, the application doesn't install either the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] client network libraries or the network protocols. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] client network libraries are installed during [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup; the network protocols are installed as part of Microsoft Windows Setup (or through **Networks** in **Control Panel**). A particular network protocol might not be available as part of Windows Setup. For more information about installing these network protocols, see the vendor documentation.

<a id="Configure"></a>

### Configure a client to use TCP/IP

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Native Client Configuration**, right-click **Client Protocols**, and then select **Properties**.

1. In the **Enabled Protocols** box, select the up and down arrows to change the order in which protocols are tried, when attempting to connect to SQL Server. The top protocol in the **Enabled Protocols** box is the default protocol.

The shared memory protocol is enabled separately by checking the **Enabled Shared Memory Protocol** box.

## Related content

- [Server configuration: remote login timeout](configure-the-remote-login-timeout-server-configuration-option.md)
