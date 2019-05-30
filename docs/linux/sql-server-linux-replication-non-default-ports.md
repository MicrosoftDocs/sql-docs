---
title: Configure snapshot folder shares SQL Server Replication on Linux | Microsoft Docs
description: This article describes how to configure snapshot folder shares SQL Server replication on Linux.
author: MikeRayMSFT 
ms.author: mikeray
manager: craigg
ms.date: 09/24/2018
ms.topic: article
ms.prod: sql
ms.technology: linux
ms.custom: "sql-linux"
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Configure replication with non-default ports

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

You can configure replication with SQL Server on Linux instances listening on any port configured with the network.tcpport mssql-conf setting. The port needs to be appended to the server name during configuration if the following conditions are true:

1. Replication set-up involves an instance of SQL Server on Linux
2. Any instance (Windows or Linux) is listening on a non-default port. 

The server name of an instance can be found by running @@servername on the instance.

## Examples

'Server1' listens on port 1500 on Linux. To configure 'Server1' for distribution, run `sp_adddistributor` with `@distributor`. For example: 

```sql
exec sp_adddistributor @distributor = 'Server1,1500'
```

'Server1' listens on port 1500 on Linux. To configure a publisher for the distributor, run `sp_adddistpublisher` with `@publisher`. For example:

```sql
exec sp_adddistpublisher @publisher = 'Server1,1500' ,  ,  
```

'Server2' listens on port 6549 on Linux. To configure 'Server2' as a subscriber, run `sp_addsubscription` with `@subscriber`. For example:

```sql
exec sp_addsubscription @subscriber = 'Server2,6549' ,  ,  
```

'Server3' listens on port 6549 on Windows with server name of Server3 and instance name of MSSQL2017. To configure 'Server3' as a subscriber, run the `sp_addsubscription` with `@subscriber`. For example:

```sql
exec sp_addsubscription @subscriber = 'Server3/MSSQL2017,6549',  ,  
```

## Next steps

[Concepts: SQL Server replication on Linux](sql-server-linux-replication.md)

[Replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

