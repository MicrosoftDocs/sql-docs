---
title: "Client Protocols Properties (Order Tab)"
description: Discover how to enable or disable client protocols. See how to rearrange the order in which protocols are used when clients try to connect to SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
helpviewer_keywords:
  - "client protocols [SQL Server]"
monikerRange: ">=sql-server-2017"
---
# Client Protocols Properties (Order tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Use the **Order** page on the **Client Protocols Properties** dialog box in SQL Server Configuration Manager to view and enable the client protocols.

Select a protocol, and then select **Enable** or **Disable** to move the selected protocol to the **Disabled Protocols** or **Enabled Protocols** list.

Protocols are tried in the order listed, attempting to connect using the top protocol first, and then the second listed protocol, etc. Move protocols up or down the **Enabled Protocols** list, by selecting the up arrow and down arrow buttons. When connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a client on that computer, the **Shared Memory** protocol is always tried first, if enabled.

> [!NOTE]  
> These settings aren't used by .NET SqlClient. The protocol order for .NET SqlClient is first TCP, and then named pipes, which can't be changed.

## Options

#### Disabled protocols

Lists the protocols that are installed but aren't currently used.

#### Enabled protocols

Lists the protocols that are available for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] clients on this computer.

#### > (greater than)

Enables the currently highlighted protocol in the **Disabled Protocols** box, moving it to the **Enabled Protocols** box.

#### \< (less than)

Disables the currently highlighted protocol in the **Enabled Protocols** box, moving it to the **Disabled Protocols** box.

#### &#8593; (up arrow)

Moves up the highlighted protocol in the list. This option allows you to increase the priority in which the network library attempts to use the selected protocol for connections.

#### &#8595; (down arrow)

Moves down the highlighted protocol in the list. This option allows you to decrease the priority in which the network library attempts to use the selected protocol for connections.

#### Enable Shared Memory protocol

Enables the shared memory protocol which is always tried first (if enabled), when connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a client on that computer.

If the protocol is specified through a prefix or as part of the connection string, only the specified protocol is attempted.

## Related content

- [Connect to the Database Engine](../../sql-server/connect-to-database-engine.md)
- [Enable or disable a server network protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
- [Network protocols and network libraries](../../sql-server/install/network-protocols-and-network-libraries.md)
- [Choosing a Network Protocol](/previous-versions/sql/sql-server-2016/ms187892(v=sql.130))
