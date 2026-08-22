---
title: "SQL Server Browser Service (Database Engine and SSAS)"
description: Learn about SQL Server Browser. This service listens for requests for SQL Server resources and provides information about installed SQL Server instances.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
f1_keywords:
  - "sql13.swb.browseservers.local.f1"
  - "sql13.swb.browseservers.network.f1"
helpviewer_keywords:
  - "services [SQL Server], security"
  - "SQL Browser service (See SQL Server Browser Service)"
  - "Browser Service"
  - "SQL Server Browser service"
---
# SQL Server Browser service (Database Engine and SSAS)

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

The SQL Server Browser (`sqlbrowser`) runs as a service, to help client computers find instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a server running Windows. SQL Server Browser is installed with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service doesn't need to be configured, but must be running under some connection scenarios.

The SQL Server Browser can help with:

- Browsing a list of available servers. For each instance of the Database Engine and [SQL Server Analysis Services](/analysis-services/ssas-overview) (SSAS), the SQL Server Browser service provides the instance name and the version number.

- Connecting to the correct server instance.
- Connecting to [dedicated administrator connection](diagnostic-connection-for-database-administrators.md) (DAC) endpoints.

## Configure SQL Server Browser service

SQL Server Browser can be configured during setup or using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md). By default, the SQL Server Browser service starts automatically:

- When upgrading an installation.
- When installing on a cluster.
- When installing a named instance of the Database Engine, including all instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Express.
- When installing a named instance of SSAS.

## How SQL Server Browser works

The following sections describe how the SQL Server Browser service works.

- [TCP/IP port or named pipe assignment](#tcpip-port-or-named-pipe-assignment)
- [Named instances and dynamic ports](#named-instances-and-dynamic-ports)
- [SQL Server startup process and port discovery](#sql-server-startup-process-and-port-discovery)

### TCP/IP port or named pipe assignment

When an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] starts and the TCP/IP protocol is enabled for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the instance is assigned a TCP/IP port. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] listens on a specific named pipe if the named pipes protocol is enabled. This port or named pipe is used by that specific instance to exchange data with client applications. TCP/IP port `1433` and pipe `\sql\query` are assigned to the default instance during installation. The server administrator can change the port or named pipe using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

### Named instances and dynamic ports

Because only one instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can use a port or pipe, different port numbers and pipe names are assigned for named instances, including [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Express edition. When enabled, named instances and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Express are configured to use dynamic ports by default. That is, an available port is assigned when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] starts.

If you want, a specific port can be assigned to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Clients can specify a specific port when connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. However, if the port is dynamically assigned, the port number can change anytime the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance is restarted, so the correct port number is unknown to the client.

### SQL Server startup process and port discovery

Upon startup, the SQL Server Browser starts and claims User Datagram Protocol (UDP) port `1434`. SQL Server Browser reads the registry, identifies all instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the computer, and notes the ports and named pipes they use. When a server has two or more network cards, SQL Server Browser returns the first enabled port it encounters for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. SQL Server Browser supports IPv4 and IPv6.

When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] clients request [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resources, the client network library sends a UDP message to the server using port `1434`. SQL Server Browser responds with the TCP/IP port or named pipe of the requested instance. The network library on the client application then completes the connection by sending a request to the server using the port or named pipe of the desired instance.

For more information on starting and stopping the SQL Server Browser service, see [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md).

## Use SQL Server Browser

If the SQL Server Browser service isn't running, you can still connect to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] if you provide the correct port number or named pipe. For instance, you can connect to the default instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with TCP/IP if it's running on port `1433`.

However, if the SQL Server Browser service isn't running, the following connections don't work:

- Any component that tries to connect to a named instance without fully specifying all the parameters (such as the TCP/IP port or named pipe).

- Any component that generates or passes `<server>\<instance>` information that other components could later use to reconnect.

- Connecting to a named instance without providing the port number or pipe.

- [Diagnostic connection for database administrators](diagnostic-connection-for-database-administrators.md) to a named instance or the default instance if not using TCP/IP port `1433`.

- The Online Analytical Processing (OLAP) redirector service.

- Enumerating servers in [SQL Server Management Studio](/ssms/menu-help/about-sql-server-management-studio).

Suppose you're using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in a client-server scenario (for example, when your application is accessing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] across a network). If you stop or disable the SQL Server Browser service, you must assign a specific port number to each instance and configure your client application code to use that port number. This approach has the following problems:

- You must update and maintain client application code to ensure it's connecting to the proper port.

- The port you choose for each instance could be used by another service or application on the server, causing the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to be unavailable.

## Clusters and SQL Server Browser

SQL Server Browser isn't a clustered resource and doesn't support failover from one cluster node to the other. Therefore, if there's a cluster, SQL Server Browser should be installed and turned on for each cluster node. On clusters, the SQL Server Browser listens on `IP_ANY`.

> [!NOTE]  
> When the SQL Server Browser listens on `IP_ANY` and you enable listening on specific IP addresses, you must configure the same TCP/IP port on each IP address, because SQL Server Browser returns the first IP address and port pair it encounters.

## Install, uninstall, and run from the command line

By default, the SQL Server Browser program is installed at `<drive>:\Program Files (x86)\Microsoft SQL Server\<nn>\Shared\sqlbrowser.exe`.

The SQL Server Browser service is uninstalled when the last instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is removed.

SQL Server Browser can be started from the command prompt for troubleshooting by using the `-c` switch:

```console
<drive>\<path>\sqlbrowser.exe -c
```

## Security

The SQL Server Browser Service is crucial in facilitating network communication with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances.

Security measures for SQL Server Browser Service include:

- Configuring firewalls to allow its traffic.

- Restricting access to trusted IP addresses.

- Regularly applying updates to patch vulnerabilities.

- Additionally, you must implement strong authentication and authorization policies to prevent unauthorized access and maintain the integrity of your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] environment.

### Account privileges

SQL Server Browser listens on a UDP port and accepts unauthenticated requests using SQL Server Resolution Protocol (SSRP). SQL Server Browser should be run in the security context of a low-privileged user to minimize exposure to a malicious attack. The sign in account can be changed by using the SQL Server Configuration Manager.

The minimum user rights for SQL Server Browser are:

- Deny access to this computer from the network.
- Deny sign in locally.
- Deny sign in as a batch job.
- Deny Log On Through Terminal Services.
- Sign in as a service.
- Read and write the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] registry keys related to network communication (ports and pipes).

### Default account

Setup configures SQL Server Browser to use the account selected for services during setup. Other possible accounts include:

- Any **domain\local** account.
- The **Local Service** account.
- The **Local System** account (not recommended as it has unnecessary privileges).

### Hide SQL Server

Hidden instances are instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that the SQL Server Browser service doesn't advertise. Set the `HideInstance` flag to prevent SQL Server Browser from responding with information about this server instance. Clients can still connect to a hidden instance over the network if they specify the protocol endpoint explicitly, such as `tcp:server,5000`. To restrict network access, disable the relevant protocols in SQL Server Configuration Manager.

### Use a firewall

To communicate with the SQL Server Browser service on a server behind a firewall, open UDP port `1434` and the TCP/IP port used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (for example, `1433`). For information about working with a firewall, see [Configure the Windows Firewall to allow SQL Server access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).

## Related content

- [Network protocols and network libraries](../../sql-server/install/network-protocols-and-network-libraries.md)
- [Connect using IPv6](../../tools/configuration-manager/connecting-using-ipv6.md)
- [Enable or disable a server network protocol](enable-or-disable-a-server-network-protocol.md)
