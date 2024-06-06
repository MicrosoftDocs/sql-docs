---
title: Connect to the SQL Server Database Engine
description: Learn how to connect to the Database Engine used by SQL Server and Azure SQL services
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/07/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Connect to the Database Engine

This article provides a high level overview for connecting to the [!INCLUDE [ssdenoversion-md](../includes/ssdenoversion-md.md)], used by the following products and services:

- [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]
- [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)]
- [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)]
- [!INCLUDE [ssazurepdw_md](../includes/ssazurepdw_md.md)]
- [!INCLUDE [ssazuresynapse-md](../includes/ssazuresynapse-md.md)]
- [!INCLUDE [ssazurede-md](../includes/ssazurede-md.md)]

## Prerequisites

You connect to the [!INCLUDE [ssde-md](../includes/ssde-md.md)] using a *client tool* or *client library*. Client tools run in a graphical user interface (GUI), or a command-line interface (CLI).

The following table describes some of the more common client tools.

| Client tool | Type | Operating system |
| --- | --- | --- |
| **[SQL Server Management Studio](../ssms/sql-server-management-studio-ssms.md)** (SSMS) | GUI | Windows |
| **[Azure Data Studio](/azure-data-studio/what-is-azure-data-studio)** (ADS) | GUI | Windows, macOS, Linux |
| **[bcp](../tools/bcp-utility.md)** | CLI | Windows, macOS, Linux |
| **[sqlcmd](../tools/sqlcmd/sqlcmd-utility.md)** | CLI | Windows, macOS, Linux |

> [!NOTE]  
> Client tools include at least one client library. For more information about connecting with a client library, see [Connection modules for Microsoft SQL Database](../connect/sql-connection-libraries.md).

## Connection options

When you connect to the [!INCLUDE [ssde-md](../includes/ssde-md.md)], you must provide an *instance* name (that is, the server or instance where the [!INCLUDE [ssde-md](../includes/ssde-md.md)] is installed), a network *protocol*, and a connection *port*, in the following format:

```text
[<protocol>:]<instance>[,<port>]
```

The protocol and port are optional because they have default values. Depending on the client tool and client library, they might be skipped.

> [!NOTE]  
> If you use a custom TCP port for connecting to the [!INCLUDE [ssde-md](../includes/ssde-md.md)], you must separate it with a comma (`,`), because the colon (`:`) is used to specify the protocol.

| Setting | Values | Default | Details |
| --- | --- | --- | --- |
| **Protocol** | `tcp` (TCP/IP), `np` (named pipes), or `lpc` (shared memory). | `np` is the default when connecting to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].<br /><br />`tcp` is the default when connecting to Azure SQL services. | **Protocol** is optional, and is frequently excluded when connecting to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on the same computer as the client tool.<br /><br />For more information, see [Network protocol considerations](#network-protocol-considerations) in the next section. |
| **Instance** | The name of the server or instance. For example, `MyServer` or `MyServer\MyInstance`. | `localhost` | If the [!INCLUDE [ssde-md](../includes/ssde-md.md)] is located on the same computer as the client tool, you may be able to connect using `localhost`, `127.0.0.1`, or even `.` (a single period).<br/><br/>If you're connecting to a named instance, you must specify the server name and the instance name, separated by a slash. For example, `MyServer\MyInstance`. A named instance on the local machine may be specified by `.\MyInstance`. [!INCLUDE [ssexpress-md](../includes/ssexpress-md.md)] uses `MyServer\SQLEXPRESS`. |
| **Port** | Any TCP port. | `1433` | The default TCP port for connecting to the default instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is `1433`. However, your infrastructure team may configure custom ports.<br /><br />[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Windows, including [!INCLUDE [ssexpress-md](../includes/ssexpress-md.md)] edition, can be configured as a named instance and may also have a custom port.<br /><br />For connecting to Azure SQL services, see the [Connect to Azure SQL](#connect-to-azure-sql) section.<br /><br />For more information about custom ports with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see [SQL Server Configuration Manager](../relational-databases/sql-server-configuration-manager.md). |

## Network protocol considerations

For [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Windows, when you connect to an instance on the same machine as the client tool, and depending on which edition is installed, the default protocol can be configured with multiple protocols, including named pipes (`np`), TCP/IP (`tcp`), and shared memory (`lpc`). Use the shared memory protocol for troubleshooting when you suspect the other protocols are configured incorrectly.

If you connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] over a TCP/IP network, make sure that TCP/IP is enabled on the server as well. TCP/IP may be disabled by default on installations of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. For more information, see [Default SQL Server Network Protocol Configuration](../database-engine/configure-windows/default-sql-server-network-protocol-configuration.md#default-configuration).

Connections to Azure SQL services, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, and [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] in containers, all use TCP/IP.

For both [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)], see [Connect and query articles](/azure/azure-sql/database/connect-query-content-reference-guide).

## Connect to Azure SQL

This section provides information on connecting to Azure SQL services.

### [Azure SQL Database](#tab/sqldb)

To quickly connect to and query an [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)] from the Azure portal, use the [Azure portal Query editor for Azure SQL Database](/azure/azure-sql/database/query-editor?view=azuresql-db&preserve-view=true).

For external connections, be aware of the secure-by-default [Azure SQL Database database-level firewall](/azure/azure-sql/database/firewall-configure?view=azuresql-db&preserve-view=true).

Examples for application connections are available:

- [Use .NET and the Microsoft.Data.SqlClient library](/azure/azure-sql/database/azure-sql-dotnet-quickstart?view=azuresql-db&preserve-view=true)
- [Use .NET and EF Core](/azure/azure-sql/database/azure-sql-dotnet-entity-framework-core-quickstart?view=azuresql-db&preserve-view=true)
- [Use Python with pyodbc](/azure/azure-sql/database/azure-sql-python-quickstart?view=azuresql-db&preserve-view=true)
- [Use Node.js with mssql](/azure/azure-sql/database/azure-sql-javascript-mssql-quickstart?view=azuresql-db&preserve-view=true)

### [Azure SQL Managed Instance](#tab/sqlmi)

Connect to an [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)] in the same ways you connect to a SQL Server instance, see [Connect your application to Azure SQL Managed Instance](/azure/azure-sql/managed-instance/connect-application-instance?view=azuresql-mi&preserve-view=true).

You can also [configure a point-to-site connection to Azure SQL Managed Instance from on-premises](/azure/azure-sql/managed-instance/point-to-site-p2s-configure?view=azuresql-mi&preserve-view=true) or [connect to Azure SQL Managed Instance from an Azure VM](/azure/azure-sql/managed-instance/connect-vm-instance-configure?view=azuresql-mi&preserve-view=true).

[!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)] can enforce a minimum [Transport Layer Security (TLS)](/troubleshoot/sql/database-engine/connect/tls-1-2-support-microsoft-sql-server) version for application connections. For more information, see [Configure minimal TLS version in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/minimal-tls-version-configure?view=azuresql-mi&preserve-view=true).

### [SQL Server on Azure VM](#tab/sqlvm)

Connect to the **Public IP address** of the VM. For an example, see [Connect to SQL Server on a Windows virtual machine in the Azure portal](/azure/azure-sql/virtual-machines/windows/sql-vm-create-portal-quickstart#connect-to-sql-server).

---

## Connect to SQL Server

This section provides information on connecting to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

### Connect to SQL Server on the same machine as the client

You can connect to the local machine using named pipes (`np`), shared memory (`lpc`), or TCP/IP (`tcp`). Shared memory is the fastest, because it doesn't use the network interface.

> [!NOTE]  
> If you use an IP address for your instance name and don't specify `tcp`, the protocol defaults to `np` (named pipes) if it's a configured protocol.

A named instance has a dynamically assigned TCP port. If you want to connect to a named instance, the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser service must be running on the server.

#### Connect to a default SQL Server instance on the same machine

1. If you're connecting to a server configured with default settings, use one of the following options:

   - `localhost`
   - `127.0.0.1`
   - `.` (a single period)

1. If you're connecting to a custom TCP port, such as `51433`, use one of the following options:

   - `tcp:localhost,51433`
   - `127.0.0.1,1433`

#### Connect to a SQL Server named instance on the same machine

In this example, the named instance is called `MyInstance`. Make sure the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser service is running, and use one of the following options:

- `localhost\MyInstance`
- `127.0.0.1\MyInstance`
- `.\MyInstance`

### Connect to SQL Server on the network

You can connect using a server name or an IP address. In this example, the server name `MyServer` resolves to `192.10.1.128`.

#### Connect to a default SQL Server instance on the network, using named pipes

To connect to a server on the local network with named pipes, use one of the following options:

- `MyServer`
- `np:MyServer`

> [!NOTE]  
> On a local area network, connecting with TCP/IP might be faster than with named pipes.

#### Connect to a default SQL Server instance on the network, using TCP/IP

1. If you're connecting to a server configured with default TCP port `1433`, use one of the following options:

   - `tcp:MyServer`
   - `tcp:192.10.1.128`

1. If you're connecting to a server configured with a custom TCP port, such as `51433`, use one of the following options:

   - `MyServer,51433`
   - `tcp:MyServer,51433`
   - `192.10.1.128,51433`
   - `tcp:192.10.1.128,51433`

#### Connect to a SQL Server named instance on the network, using TCP/IP

In this example, the named instance is called `MyInstance`. Make sure the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Browser service is running on the server, and use one of the following options:

- `tcp:MyServer\MyInstance`
- `tcp:192.10.1.128\MyInstance`

## Get help

- [Create a valid connection string using the shared memory protocol](../tools/configuration-manager/creating-a-valid-connection-string-using-shared-memory-protocol.md)
- [Create a valid connection string using TCP/IP](../tools/configuration-manager/creating-a-valid-connection-string-using-tcp-ip.md)
- [Troubleshoot connectivity issues in SQL Server](/troubleshoot/sql/database-engine/connect/resolve-connectivity-errors-overview)
- [Trace the network authentication process to the Database Engine](../relational-databases/database-engine-connection-open-network-trace.md)

## Related content

- [What is SQL Server Management Studio (SSMS)?](../ssms/sql-server-management-studio-ssms.md)
- [What is Azure Data Studio?](../azure-data-studio/what-is-azure-data-studio.md)
- [Configure Database Engine Instances (SQL Server)](../database-engine/configure-windows/configure-database-engine-instances-sql-server.md)
- [sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md)
