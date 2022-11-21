---
title: "remote admin connections Server Configuration Option"
description: "Learn how applications on remote computers can use the DAC. See how to use the remote admin connections option with sp_configure to turn on this capability."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/12/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "administrator connections [SQL Server]"
  - "DAC"
  - "connections [SQL Server], dedicated administrator"
  - "remote admin connections option"
  - "dedicated administrator connections [SQL Server]"
---
# remote admin connections server configuration option

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a dedicated administrator connection (DAC). You can use the DAC to execute diagnostic functions or [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, or to troubleshoot problems on the server, even when the server is locked or running in an abnormal state and not responding to a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] connection.

By default, the DAC is only available from a client application directly on the server. To enable client applications on remote computers to use the DAC, use the **remote admin connections** option of `sp_configure`.

By default, the DAC only listens on the loop-back IP address (127.0.0.1), port 1434. If TCP port 1434 isn't available, a TCP port is dynamically assigned when the [!INCLUDE[ssDE](../../includes/ssde-md.md)] starts up. When more than one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a computer, check the error log for the TCP port number.

The following table lists the possible values for the remote admin connections option.

|Value|Description|  
|-----------|-----------------|
|0|Only local connections are allowed by using the DAC.|
|1|Remote connections are allowed by using the DAC.|

## Example

The following example enables the DAC from a remote computer:

```sql
EXEC sp_configure 'remote admin connections', 1;
GO
RECONFIGURE;
GO
```

## See also

- [Diagnostic Connection for Database Administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md)
- [Connect to SQL Server when system administrators are locked out](connect-to-sql-server-when-system-administrators-are-locked-out.md)
