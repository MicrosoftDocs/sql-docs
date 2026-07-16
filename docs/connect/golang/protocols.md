---
title: "go-mssqldb Protocols"
description: "TCP, named pipes, shared memory, and Dedicated Administrator Connection (DAC) protocols in the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---
# go-mssqldb protocols

The `go-mssqldb` driver supports multiple transport protocols for connecting to SQL Server. This article describes each protocol and how to configure it.

## Protocol parameter

Set the `protocol` connection parameter to specify the transport:

| Value | Protocol | Description |
| --- | --- | --- |
| `tcp` | TCP/IP | Default protocol. Works on all platforms. |
| `np` | Named pipes | Windows inter-process communication. Works on Windows and Linux (with SMB). |
| `lpc` | Shared memory | Local connections on Windows only. |
| `admin` | DAC (TCP) | Dedicated Administrator Connection. Connects to the DAC endpoint. |

If you don't specify a `protocol`, the driver uses TCP.

## TCP/IP

TCP is the default and most common protocol. Specify the host and port in the connection string:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025
```

Or explicitly:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&protocol=tcp
```

### Server prefix format

You can also specify the protocol as a prefix on the server name (ADO/ODBC formats):

```text
server=tcp:<server>,1433;user id=<user>;password=<password>;database=AdventureWorks2025
```

> [!NOTE]
> In the ADO prefix format, the port is separated by a comma, not a colon.

## Named pipes

Named pipes use the `np` protocol. Specify the pipe path with the `pipe` parameter:

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&protocol=np&pipe=\sql\query
```

Or use the server prefix format:

```text
server=np:<server>\myinstance;user id=<user>;password=<password>;database=AdventureWorks2025
```

The default pipe name is `\sql\query` for the default instance.

Current driver releases support named pipe connections on Windows for Arm64 in addition to Windows and Linux for x86-64 environments that expose the pipe over SMB.

### Named instance pipe names

For named instances, the pipe path is typically `\MSSQL$INSTANCENAME\sql\query`. If you know the pipe path, specify it directly. Otherwise, omit the `pipe` parameter and let SQL Server Browser resolve it.

## Shared memory

Shared memory (`lpc`) provides the fastest communication for local connections on Windows. It works only when the client and server are on the same machine:

```text
sqlserver://<user>:<password>@localhost?database=AdventureWorks2025&protocol=lpc
```

Or with the server prefix:

```text
server=lpc:.\myinstance;user id=<user>;password=<password>;database=AdventureWorks2025
```

> [!NOTE]
> Shared memory isn't available on Linux or macOS.

## Dedicated Administrator Connection (DAC)

The DAC provides a special diagnostic connection to SQL Server when the server is unresponsive. The DAC listens on a dedicated port (default: 1434 for default instances):

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&protocol=admin
```

Or with the server prefix:

```text
server=admin:<server>;user id=<user>;password=<password>;database=AdventureWorks2025
```

> [!WARNING]
> SQL Server allows only one DAC connection at a time. Use the DAC only for emergency diagnostics.

## SQL Server Browser

When the driver connects to a named instance without a specified port, it contacts the SQL Server Browser service (on UDP port 1434) to resolve the instance to a TCP port. Ensure the SQL Server Browser service is running on the target machine.

```text
sqlserver://<user>:<password>@<server>\myinstance?database=AdventureWorks2025
```

## Protocol selection guidance

| Scenario | Recommended protocol |
| --- | --- |
| Remote connections | `tcp` |
| Local connections on Windows | `lpc` (fastest) or `tcp` |
| Cross-platform applications | `tcp` |
| Emergency diagnostics | `admin` |
| Legacy applications requiring named pipes | `np` |

## Related content

- [Connection strings](connection-strings.md)
- [Connection options](connection-options.md)
- [Troubleshooting](troubleshooting.md)
