---
title: Managing Credentials in the Elastic Database Client Library
description: How to set the right level of credentials, admin to read-only, for elastic database apps
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: bgavrilovic
ms.date: 06/13/2025
ms.service: azure-sql-database
ms.subservice: scale-out
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db "
ms.custom:
  - sqldbrb=1
  - sfi-ropc-nochange
---
# Credentials used to access the Elastic Database client library

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

[!INCLUDE [elastic-query-shard-map-manager-mode-end-of-support](includes/elastic-query-shard-map-manager-mode-end-of-support.md)]

The [Building scalable cloud databases](elastic-database-client-library.md) uses three different kinds of credentials to access the [shard map manager](elastic-scale-shard-map-management.md). Depending on the need, use the credential with  the lowest level of access possible.

- **Management credentials**: for creating or manipulating a shard map manager. (See the [glossary](elastic-scale-glossary.md).)
- **Access credentials**: to access an existing shard map manager to obtain information about shards.
- **Connection credentials**: to connect to shards.

For more information, see [Authorize database access to Azure SQL Database](logins-create-manage.md).

## About management credentials

Management credentials are used to create a `ShardMapManager` ([Java](/java/api/com.microsoft.azure.elasticdb.shard.mapmanager.shardmapmanager), [.NET](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.shardmapmanager)) object for applications that manipulate shard maps. (For example, see [Adding a shard using Elastic Database tools](elastic-scale-add-a-shard.md) and [data-dependent routing](elastic-scale-data-dependent-routing.md)). The user of the elastic scale client library creates the SQL users and SQL logins and makes sure each is granted the read/write permissions on the global shard map database and all shard databases as well. These credentials are used to maintain the global shard map and the local shard maps when changes to the shard map are performed. For instance, use the management credentials to create the shard map manager object (using `GetSqlShardMapManager` ([Java](/java/api/com.microsoft.azure.elasticdb.shard.mapmanager.shardmapmanagerfactory.getsqlshardmapmanager), [.NET](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.shardmapmanagerfactory.getsqlshardmapmanager)):

```java
// Obtain a shard map manager.
ShardMapManager shardMapManager = ShardMapManagerFactory.GetSqlShardMapManager(smmAdminConnectionString,ShardMapManagerLoadPolicy.Lazy);
```

The variable `smmAdminConnectionString` is a connection string that contains the management credentials. The user ID and password provide read/write access to both shard map database and individual shards. The management connection string also includes the server name and database name to identify the global shard map database. Here's a typical connection string for that purpose:

```java
"Server=<yourserver>.database.windows.net;Database=<yourdatabase>;User ID=<yourmgmtusername>;Password=<yourmgmtpassword>;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"
```

Don't use values in the form of "username@server"—instead just use the "username" value. This is because credentials must work against both the shard map manager database and individual shards, which can be on different servers.

## Access credentials

When creating a shard map manager in an application that doesn't administer shard maps, use credentials that have read-only permissions on the global shard map. The information retrieved from the global shard map under these credentials is used for [data-dependent routing](elastic-scale-data-dependent-routing.md) and to populate the shard map cache on the client. The credentials are provided through the same call pattern to `GetSqlShardMapManager`:

```java
// Obtain shard map manager.
ShardMapManager shardMapManager = ShardMapManagerFactory.GetSqlShardMapManager(smmReadOnlyConnectionString, ShardMapManagerLoadPolicy.Lazy);  
```

Note the use of the `smmReadOnlyConnectionString` to reflect the use of different credentials for this access on behalf of **non-admin** users: these credentials shouldn't provide write permissions on the global shard map.

## Connection credentials

Additional credentials are needed when using the `OpenConnectionForKey` ([Java](/java/api/com.microsoft.azure.elasticdb.shard.mapper.listshardmapper.openconnectionforkey), [.NET](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.shardmap.openconnectionforkey)) method to access a shard associated with a sharding key. These credentials need to provide permissions for read-only access to the local shard map tables residing on the shard. This is needed to perform connection validation for data-dependent routing on the shard. This code snippet allows data access in the context of data-dependent routing:

```csharp
using (SqlConnection conn = rangeMap.OpenConnectionForKey<int>(targetWarehouse, smmUserConnectionString, ConnectionOptions.Validate))
```

In this example, `smmUserConnectionString` holds the connection string for the user credentials. For Azure SQL Database, here's a typical connection string for user credentials:

```java
"User ID=<yourusername>; Password=<youruserpassword>; Trusted_Connection=False; Encrypt=True; Connection Timeout=30;"  
```

As with the admin credentials, don't use values in the form of `username@server`. Instead, just use `username`. Also note that the connection string doesn't contain a server name and database name. That is because the `OpenConnectionForKey` call automatically directs the connection to the correct shard based on the key. Hence, the database name and server name aren't provided.

[!INCLUDE [elastic-scale-include](../includes/elastic-scale-include.md)]

- [Authorize database access to SQL Database, SQL Managed Instance, and Azure Synapse Analytics](logins-create-manage.md)
- [An overview of Azure SQL Database and SQL Managed Instance security capabilities](security-overview.md)
