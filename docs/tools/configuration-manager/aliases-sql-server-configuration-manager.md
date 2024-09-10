---
title: "Aliases (SQL Server Configuration Manager)"
description: Learn how to create an alias in SQL Server Configuration Manager so that you can use an alternate name when connecting to an instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.date: "09/09/2024"
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---
# Aliases (SQL Server Configuration Manager)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

An alias is an alternate name that can be used to make a connection. The alias encapsulates the required elements of a connection string, and exposes them with a name chosen by the user. To create an alias for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients on this computer, right-click **Aliases** in the console pane, and then select **New Alias**. To configure an existing alias for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients on this computer, select **Aliases** in the console pane, right-click the desired existing alias in the details pane, and then select **Properties**.  
  
> [!NOTE]
> Aliases for SQL Server are a client side configuration. Each client computer that uses the alias must have an identical alias configuration, and SQL Server Configuration Manager is not the only tool that can be used to create or manage aliases.
>
> SQL Server 2022 and later versions do not support creating aliases using SQL Server Configuration Manager. To create an alias for SQL Server 2022 and later versions, use the SQL Server Client Network Utility tool.

## When to Use an Alias

By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connects to a local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the **Shared Memory** protocol, and to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on another computer using either **TCP/IP** or **Named Pipes**. Create an alias when you're using TCP/IP or named pipes, and you want to provide a customized connection string, or when you want to use a name other than the server name for the connection.  
  
### Examples  
  
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] isn't listening on the default TCP/IP port of 1433 so you want to provide a connection string with a different port number.  
  
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] isn't listening on the default named pipe so you want to provide a connection string with a different pipe name.  
  
- An application expects to connect to a database on the server named `ACCT`, but that database has been consolidated as an instance named `ACCT` on a server named `CENTRAL`. The application can't easily be changed. Create an alias named `ACCT`, with a connection string pointing to `CENTRAL\ACCT`.  

### Alias properties  
  
#### Alias Name

The name (alias) that you want to use to refer to this connection.  
  
#### Pipe Name or Port Number

Additional elements of the connection string. The name of this box varies with the selected protocol.  
  
#### Protocol

The protocol used for the connection.  
  
#### Server

The name of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance being connected to.

## Shared memory connections

Connections to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a client running on the same computer use the shared memory and named pipes protocols. Shared memory has no configurable properties. Shared memory is always tried first, and can't be moved from the top position of the **Enabled Protocols** list in the **Client Protocols Properties** list. The Shared Memory protocol can be disabled, which is useful when troubleshooting one of the other protocols.

You can't create an alias using the shared memory protocol, but if shared memory is enabled, then connecting to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] by name, creates a shared memory connection. A shared memory connection string uses the format `lpc:<servername>[\instancename]`.

### Connect to the local server using shared memory

When connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on the same computer as the client, you can use `(local)` as the server name. This value isn't encouraged as it leads to ambiguity, however it can be useful when the client is known to be running on the intended computer. For instance, when creating an application for mobile disconnected users, where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs on laptop computers and stores project data, a client connecting to `(local)` would always connect to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on the laptop. The word **localhost** or a period (**.**) can be used in place of `(local)`.

### Examples of shared memory connections

The following names connect to the local computer with the shared memory protocol if it's enabled:

- `<servername>`
- `<servername>\<instancename>`
- `(local)`
- `localhost`

You can't create an alias for a shared memory connection.

> [!NOTE]  
> Depending on the configuration of the server, specifying an IP address in the **Server** box will result in a named pipes or TCP/IP connection.

## TCP/IP connections

To connect to the SQL Server using an alias with TCP/IP, you must:

- Specify an **Alias Name**.

- For the **Server**, enter either a server name to which you can connect using the **ping** utility, or an IP address to which you can connect using the **ping** utility. For a named instance, append the instance name.

- Specify **TCP/IP** for the **Protocol**.

- Optionally, enter a port number for the **Port No**. The default is `1433`, which is the port number of the default instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] on a server. To connect to a named instance or a default instance that isn't listening on port 1433, you must provide the port number, or start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service. For information on configuring the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service, see [SQL Server Browser Service](../../tools/configuration-manager/sql-server-browser-service.md).

At the time of connection, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client component reads the server, protocol, and port values from the registry for the specified alias name, and creates a connection string in the format `tcp:<servername>[\<instancename>],<port>` or `tcp:<IPAddress>[\<instancename>],<port>`.

> [!NOTE]  
> The Windows Firewall closes port 1433 by default. Because [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] communicates over port 1433, you must reopen the port if [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is configured to listen for incoming client connections using TCP/IP. For information on configuring a firewall, see "How to: Configure a Firewall for SQL Server Access" in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online or review your firewall documentation.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client fully support both Internet Protocol version 4 (IPv4) and Internet Protocol version 6 (IPv6). [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager accepts both IPv4 and IPv6 formats for IP addresses.

### Examples of TCP/IP alias settings

#### Connecting by server name

**Alias Name:** `<serveralias>`</br>
**Port No:** `<blank>`</br>
**Protocol:** `TCP/IP`</br>
**Server:** `<servername>`

#### Connecting by server name to a named instance

**Alias Name:** `<serveralias>`</br>
**Port No:** `<blank>`</br>
**Protocol:** `TCP/IP`</br>
**Server:** `<servername>\<instancename>`

#### Connecting by server name to a specified port

**Alias Name:** `<serveralias>`</br>
**Port No:** `<port number>`</br>
**Protocol:** `TCP/IP`</br>
**Server:** `<servername>`

#### Connecting by IP address

**Alias Name:** `<serveralias>`</br>
**Port No:** `<blank>`</br>
**Protocol:** `TCP/IP`</br>
**Server:** `<IPAddress>`

> [!NOTE]  
> For information on specifying the network protocol as a **sqlcmd** parameter, see [sqlcmd - Connect to the database engine](../sqlcmd/sqlcmd-connect-database-engine.md).

## Named pipes connections

Unless changed by the user, when the default instance of Microsoft SQL Server listens on the named pipes protocol, it uses `\\.\pipe\sql\query` as the pipe name. The period indicates that the computer is the local computer. The `pipe` indicates that the connection is a named pipe, and `sql\query` is the name of the pipe. To connect to the default pipe, the alias must have `\\<computer_name>\pipe\sql\query` as the pipe name. If SQL Server has been configured to listen on a different pipe, the pipe name must use that pipe. For instance, if SQL Server is using `\\.\pipe\unit\app` as the pipe, the alias must use `\\<computer_name>\pipe\unit\app` as the pipe name.

To connect to the SQL Server using an alias with named pipes, you must:

- Specify an **Alias Name**.

- Select **Named Pipes** as the **Protocol**

- Enter the **Pipe Name**. Alternatively, you can leave **Pipe Name** blank and SQL Server Configuration Manager will complete the appropriate pipe name after you specify the **Protocol** and **Server**.

- Specify a **Server**. For a named instance you can provide a server name and instance name.

At the time of connection, the SQL Server Native Client component reads the server, protocol, and pipe name values from the registry for the specified alias name, and creates a pipe name in the format `np:\\<computer_name>\pipe\<pipename>` or `np:\\<IPAddress>\pipe\<pipename>`. For a named instance, the default pipe name is `\\<computer_name>\pipe\MSSQL$<instance_name>\sql\query`.

> [!NOTE]
> The Microsoft Windows Firewall closes port 445 by default. Because Microsoft SQL Server communicates over port 445, you must reopen the port if SQL Server is configured to listen for incoming client connections using named pipes.

### Examples of Named Pipes alias settings

#### Connecting by server name to the default pipe

**Alias Name:** `<serveralias>`</br>
**Pipe Name:** `<blank>`</br>
**Protocol:** `Named Pipes`</br>
**Server:** `<servername>`

#### Connecting by IP Address to the default pipe

**Alias Name:** `<serveralias>`</br>
**Pipe Name:** `<blank>`</br>
**Protocol:** `Named Pipes`</br>
**Server:** `<IPAddress>`

#### Connecting by server name to a nondefault pipe

**Alias Name:** `<serveralias>`</br>
**Pipe Name:** `\\<servername>\pipe\unit\app`</br>
**Protocol:** `Named Pipes`</br>
**Server:** `<servername>`

#### Connecting by server name to a named instance

**Alias Name:** `<serveralias>`</br>
**Pipe Name:** `\\<servername>\pipe\MSSQL$<instancename>\SQL\query`</br>
**Protocol:** `Named Pipes`</br>
**Server:** `<servername>`

## Verify your connection protocol

The following query returns the protocol used for the current connection.

```sql
SELECT net_transport
FROM sys.dm_exec_connections
WHERE session_id = @@SPID;
```

## Related content

[Network Protocols and Network Libraries](../../sql-server/install/network-protocols-and-network-libraries.md)