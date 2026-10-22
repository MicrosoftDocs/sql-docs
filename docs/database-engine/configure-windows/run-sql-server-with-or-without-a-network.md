---
title: "Run SQL Server with or Without a Network"
description: Learn how to run SQL Server on a network and without one. For local use, see how to use a local pipe. For network use, see how to check for required services.
author: adbadram
ms.author: adbadram
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
helpviewer_keywords:
  - "verifying Server service has been started"
  - "net start [SQL Server]"
  - "command prompt [SQL Server], connections"
  - "SQL Server services, networks"
  - "status information [SQL Server], Server service"
  - "running SQL Server"
  - "networking [SQL Server], SQL Server with or without"
  - "services [SQL Server], networks"
  - "starting Server service"
  - "SQL Server, running"
---
# Run SQL Server with or without a network

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can run on a network, or it can function without a network.

## Run SQL Server on a network

For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to communicate over a network, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service must be running. By default, Windows automatically starts the built-in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. To find out whether the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service has started, at the command prompt, type the following command:

```console
net start
```

If the services associated with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] have been started, the following services appear in the `net start` output:

- Analysis Services (MSSQLSERVER)
- SQL Server (MSSQLSERVER)
- SQL Server Agent (MSSQLSERVER)

## Run SQL Server without a network

When running an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] without a network, you don't need to start the built-in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. Because [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], SQL Server Configuration Manager, and the `net start` and `net stop` commands are functional even without a network, the procedures for starting and stopping an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are identical for a network or stand-alone operation.

When connecting to an instance of a stand-alone [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a local client such as **sqlcmd**, you bypass the network and connect directly to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using a local pipe. The difference between a local pipe and a network pipe is whether you're using a network. Both local and network pipes establish a connection with an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using the standard pipe (`\\.\pipe\sql\query`), unless otherwise directed.

When you connect to an instance of a local [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] without specifying a server name, you're using a local pipe. When you connect to an instance of a local [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and specify a server name explicitly, you're using either a network pipe or another network interprocess communication (IPC) mechanism, such as Internetwork Packet Exchange/Sequenced Packet Exchange (IPX/SPX) (assuming you configured [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to use multiple networks). Because a stand-alone [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support network pipes, you must omit the unnecessary **/**_<Server_name>_ argument when connecting to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a client. For example, to connect to a stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from **osql**, type:

```console
osql /Usa /P <saPassword>
```
