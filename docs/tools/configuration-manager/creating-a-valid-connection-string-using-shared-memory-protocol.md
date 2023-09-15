---
title: Create a valid connection string using the shared memory protocol
description: Find out when connections to SQL Server use the shared memory protocol and how to create a valid connection string for this protocol.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/01/2023
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords:
  - "connection strings [Database Engine], shared memory"
  - "aliases [SQL Server], shared memory"
monikerRange: ">=sql-server-2016"
---
# Create a valid connection string using the shared memory protocol

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Connections to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a client running on the same computer use the shared memory and named pipes protocols. Shared memory has no configurable properties. Shared memory is always tried first, and can't be moved from the top position of the **Enabled Protocols** list in the **Client Protocols Properties** list. The Shared Memory protocol can be disabled, which is useful when troubleshooting one of the other protocols.

You can't create an alias using the shared memory protocol, but if shared memory is enabled, then connecting to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] by name, creates a shared memory connection. A shared memory connection string uses the format `lpc:<servername>[\instancename]`.

## Connect to the local server

When connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on the same computer as the client, you can use `(local)` as the server name. This value isn't encouraged as it leads to ambiguity, however it can be useful when the client is known to be running on the intended computer. For instance, when creating an application for mobile disconnected users, where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs on laptop computers and stores project data, a client connecting to `(local)` would always connect to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on the laptop. The word **localhost** or a period (**.**) can be used in place of `(local)`.

## Verify your connection protocol

The following query returns the protocol used for the current connection.

```sql
SELECT net_transport
FROM sys.dm_exec_connections
WHERE session_id = @@SPID;
```

## Examples

The following names connect to the local computer with the shared memory protocol if it's enabled:

- `<servername>`
- `<servername>\<instancename>`
- `(local)`
- `localhost`

You can't create an alias for a shared memory connection.

> [!NOTE]  
> Depending on the configuration of the server, specifying an IP address in the **Server** box will result in a named pipes or TCP/IP connection.

## See also

- [Creating a Valid Connection String Using TCP IP](../../tools/configuration-manager/creating-a-valid-connection-string-using-tcp-ip.md)
- [Creating a Valid Connection String Using Named Pipes](/previous-versions/sql/sql-server-2016/ms189307(v=sql.130))
- [Choosing a Network Protocol](/previous-versions/sql/sql-server-2016/ms187892(v=sql.130))
