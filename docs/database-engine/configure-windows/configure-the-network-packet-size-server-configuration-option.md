---
title: "Configure the network packet size server configuration option"
description: Learn how to use the network packet size option to set the packet size that SQL Server uses when transferring requests and results between clients and servers.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 11/21/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "default packet size"
  - "size [SQL Server], packets"
  - "packets [SQL Server], size"
  - "network packet size option"
---
# Configure the network packet size server configuration option

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the **network packet size** server configuration option in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **network packet size** option sets the packet size (in bytes) that's used across the whole network. Packets are the fixed-size chunks of data that transfer requests and results between clients and servers. The default packet size is 4,096 bytes.

> [!NOTE]  
> Don't change the packet size unless you are certain that it will improve performance. For most applications, the default packet size is best.

The setting takes effect immediately without restarting the server.

## <a id="Restrictions"></a> Limitations and restrictions

- The maximum network packet size for encrypted connections is 16,383 bytes.

> [!NOTE]  
> If MARS is enabled, the SMUX provider will add a 16-byte header to the packet before TLS encryption, reducing the maximum network packet size to 16368 bytes.

## Recommendations

- This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] professional.

- If an application does bulk copy operations or sends or receives large amounts of text or image data, a packet size larger than the default might improve efficiency because it results in fewer network read-and-write operations. If an application sends and receives small amounts of information, the packet size can be set to 512 bytes, which is sufficient for most data transfers.

- On systems that are using different network protocols, set network packet size to the size for the most common protocol used. The network packet size option improves network performance when network protocols support larger packets. Client applications can override this value.

- You can also call OLE DB, Open Database Connectivity (ODBC), and DB-Library functions request a change the packet size. If the server can't support the requested packet size, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will send a warning message to the client. In some circumstances, changing the packet size might lead to a communication link failure, such as the following:

  `Native Error: 233, no process is on the other end of the pipe.`

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Advanced** node.

1. Under **Network**, select a value for the **Network Packet Size** box.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `network packet size` option to `6500` bytes.

```sql
USE AdventureWorks2019;
GO
EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
EXEC sp_configure 'network packet size', 6500;
GO
RECONFIGURE;
GO
```

For more information, see [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md).

### Configure network packet size on the client side

The following table provides examples of some data connection technologies that you can use to connect to SQL Server and how to control the network packet size when using these in client applications. For a complete list of various data connection technologies that you can use to connect to SQL server visit [Homepage for SQL client programming](../../connect/homepage-sql-connection-programming.md):

| Client library | Option | Default |
| --- | --- | --- |
| [ODBC](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | `SQL_ATTR_PACKET_SIZE` | Use server side |
| [JDBC](../../connect/jdbc/setting-the-connection-properties.md) | `setPacketSize(int packetSize)` | 8000 |
| [ADO.NET - Microsoft.Data.SqlClient](/dotnet/api/microsoft.data.sqlclient.sqlconnection.packetsize?view=sqlclient-dotnet-standard-5.0&preserve-view=true) | `PacketSize` | 8000 |
| [ADO.NET - System.Data.SqlClient](/dotnet/api/system.data.sqlclient.sqlconnection.packetsize?view=dotnet-plat-ext-7.0&preserve-view=true) | `PacketSize` | 8000 |
| [OLEDB](../../connect/oledb/ole-db-data-source-objects/initialization-and-authorization-properties.md) | `SSPROP_INIT_PACKETSIZE` | 0 (use server side) |

You can monitor the **Audit Login** event or the **ExistingConnection** event in SQL Profiler to determine the network packet size of a client connection.

> [!NOTE]  
> If the application's connection string contains a value for the network packet size, then that value is used for communication. If the connection string doesn't contain a value, the drivers use defaults for the network packet size. For example, as described in the preceding table, SqlClient applications use a default packet size of 8000, whereas ODBC applications use the packet size that's configured on the server.

> [!IMPORTANT]  
> The [SQL Server Native Client](../../relational-databases/native-client/sql-server-native-client.md) (often abbreviated SNAC) has been removed from [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] 19 (SSMS). Both the SQL Server Native Client OLE DB provider (SQLNCLI or SQLNCLI11) and the legacy Microsoft OLE DB Provider for SQL Server (SQLOLEDB) are not recommended for new development. Switch to the new [Microsoft OLE DB Driver (MSOLEDBSQL) for SQL Server](../../connect/oledb/oledb-driver-for-sql-server.md) or the latest [Microsoft ODBC Driver for SQL Server](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md) going forward.

## See also

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
