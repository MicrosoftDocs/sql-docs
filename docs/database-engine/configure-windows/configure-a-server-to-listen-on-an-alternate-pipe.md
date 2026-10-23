---
title: "Configure a Server to Listen on an Alternate Pipe"
description: Find out how to configure the named pipe that the SQL Server Database Engine listens on. Learn how to connect a client application to a specific named pipe.
author: adbadram
ms.author: adbadram
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "Named Pipes [SQL Server], configuring"
  - "listening [SQL Server], pipes"
  - "pipes [SQL Server], alternate"
  - "alternate pipes [SQL Server]"
---
# Configure a server to listen on an alternate pipe

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure a server to listen on an alternate pipe in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Configuration Manager. By default, the default instance of [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] listens on named pipe `\\.\pipe\sql\query`. Named instances of [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and [!INCLUDE [ssEW](../../includes/ssew-md.md)] listen on other pipes.

There are three ways to connect to a specific named pipe with a client application:

- Run the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service on the server.

- Create an alias on the client, specifying the named pipe.

- Program the client to connect using a custom connection string.

<a id="SSMSProcedure"></a>

## Use SQL Server Configuration Manager

1. In SQL Server Configuration Manager, in the console pane, expand **SQL Server Network Configuration**, and then select expand **Protocols for** *\<instance name>*.

1. In the details pane, right-click **Named Pipes**, and then select **Properties**.

1. On the **Protocol** tab, in the **Pipe Name** box, type the pipe you want the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to listen on, and then select **OK**.

1. In the console pane, select **SQL Server Services**.

1. In the details pane, right-click **SQL Server (**\<instance name>**)** and then select **Restart**, to stop and restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is listening on an alternate pipe, there are three ways to connect to a specific named pipe with a client application:

- Run the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service on the server.

- Create an alias on the client, specifying the named pipe.

- Program the client to connect using a custom connection string.

## Related content

- [Create or delete a server alias for use by a client](create-or-delete-a-server-alias-for-use-by-a-client.md)
- [Server network configuration](server-network-configuration.md)
