---
title: Database mirroring in SQL Server
description: Learn about database mirroring and considering things like failover events in your ADO.NET application during development.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "09/30/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Database mirroring in SQL Server

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Database mirroring in SQL Server allows you to keep a copy, or mirror, of a SQL Server database on a standby server. Mirroring ensures two separate copies of the data always exist, providing high availability and complete data redundancy. The Microsoft SqlClient Provider for SQL Server provides implicit support for database mirroring. The developer doesn't need to do anything once the client has been configured for a SQL Server database. Also, the <xref:Microsoft.Data.SqlClient.SqlConnection> object supports an explicit connection mode that allows supplying the name of a failover partner server in the <xref:Microsoft.Data.SqlClient.SqlConnection.ConnectionString%2A>.

The following simplified sequence of events occurs for a <xref:Microsoft.Data.SqlClient.SqlConnection> object that targets a database configured for mirroring:

1. The client application successfully connects to the principal database, and the server sends back the name of the partner server, which the client caches.
2. If the server containing the principal database fails or connectivity is interrupted, connection and transaction state is lost. The client application attempts to re-establish a connection to the principal database and fails.
3. The client application then transparently attempts to establish a connection to the mirror database on the partner server. If it succeeds, the connection is redirected to the mirror database, which then becomes the new principal database.

## Specifying the failover partner in the connection string

If you supply the name of a failover partner server in the connection string and the principal database is unavailable when the client application connects, the client will transparently attempt a connection with the failover partner.

```csharp
";Failover Partner=PartnerServerName"
```

If you omit the name of the failover partner server and the principal database is unavailable when the client application first connects, then a <xref:Microsoft.Data.SqlClient.SqlException> occurs.

When a <xref:Microsoft.Data.SqlClient.SqlConnection> is successfully opened, the server returns the failover partner name, which supersedes any values that are supplied in the connection string.

> [!NOTE]
> You must explicitly specify the initial catalog or database name in the connection string for database mirroring scenarios. If the client receives failover information on a connection that doesn't have an explicitly specified initial catalog or database, the failover information is not cached and the application does not attempt to fail over if the principal server fails. If a connection string has a value for the failover partner, but no value for the initial catalog or database, an `InvalidArgumentException` is raised.

## Retrieving the current server name

When a failover happens, you can retrieve the name of the server to which the current connection is connected by using the <xref:Microsoft.Data.SqlClient.SqlConnection.DataSource%2A> property of a <xref:Microsoft.Data.SqlClient.SqlConnection> object. The following code fragment retrieves the name of the active server, assuming that the connection variable references an open <xref:Microsoft.Data.SqlClient.SqlConnection>.

When a failover event occurs and the connection switches to the mirror server, the **DataSource** property updates to reflect the mirror name.

```csharp
string activeServer = connection.DataSource;
```

## SqlClient mirroring behavior

The client always tries to connect to the principal server. If it fails, it tries the failover partner. If the mirror database has already been switched to the principal role on the partner server, the connection succeeds and the new principal-mirror mapping is sent to the client and cached for the lifetime of the calling <xref:System.AppDomain>. It isn't stored in persistent storage and isn't available for future connections in a different **AppDomain** or process. However, it's available for later connections within the same **AppDomain**. Another **AppDomain** or process running on the same or a different computer always has its pool of connections, and those connections aren't reset. In that case, if the primary database goes down, each process or **AppDomain** fails once, and the pool is automatically cleared.

> [!NOTE]
> Mirroring support on the server is configured on a per-database basis. If data manipulation operations are executed against other databases not included in the principal/mirror set, either by using multipart names or by changing the current database, the changes to these other databases do not propagate in the event of failure. No error is generated when data is modified in a database that is not mirrored. The developer must evaluate the possible impact of such operations.

## Next steps

### Database mirroring resources

For conceptual documentation and information on configuring, deploying, and administering mirroring, see the following resources in SQL Server documentation.

|Resource|Description|
|--------------|-----------------|
|[Database Mirroring](../../../database-engine/database-mirroring/database-mirroring-sql-server.md)|Describes how to set up and configure mirroring in SQL Server.|
