---
title: Configure tempdb settings
titleSuffix: Azure SQL Managed Instance
description: Learn how to enroll new and existing instances in the November 2022 feature wave.
author: MashaMSFT
ms.author: mathoma
ms.date: 04/15/2023
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: how-to
---
# Configure tempdb settings for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to configure your `tempdb` settings for Azure SQL Managed Instance. 

Azure SQL Managed Instance allows you to configure the following:
- Number of `tempdb` files
- The growth increment of `tempdb` files
- Maximum `tempdb` size 

Tempdb settings persist after your instance is restarted, updated, or if there is a failover. 

## Overview 

SQL Server instance comes with four system databases by default, one of which is `tempdb`. The structure of `tempdb` is the same as any other user database structure; the difference is that it is used for non-durable storage and therefore the transactions are minimally logged. Even though it is rarely called explicitly, `tempdb` is a database that has so many functions within SQL Server, that it is probably the busiest database on most SQL Server instances.

`tempdb` cannot be dropped, detached, taken offline, renamed, or restored. Attempting any of these operations will return an error. `tempdb` is regenerated upon every start of the server instance and any objects that may have been created in `tempdb` during a previous session will not persist upon a service restart, an instance update management operation or a failover.

The workload in `tempdb` is quite different than workloads in other user databases; objects and data are frequently being created and destroyed and there is extremely high concurrency. Moreover, there is only one `tempdb` on each server and even if you have multiple databases and applications connecting to that server, they all will be connecting to the same `tempdb`. When `tempdb` is heavily used, a service may experience contention when it tries to allocate pages. Depending on the degree of contention, this may cause queries and requests that involve `tempdb` to be unresponsive. Therefore, `tempdb` is critical to the performance of the service.

## Number of `tempdb` files

You can use both SQL Server Management Studio (SSMS) and Transact-SQL (T-SQL) to change the number of files for `tempdb`. 
 
### [SQL Server Management Studio (SSMS)](#tab/ssms)

### [Transact-SQL (T-SQL)](#tab/tsql)

---


## Growth increment 

You can use both SQL Server Management Studio (SSMS) and Transact-SQL (T-SQL) to change the growth increment for your  `tempdb` files. 

### [SQL Server Management Studio (SSMS)](#tab/ssms)

### [Transact-SQL (T-SQL)](#tab/tsql)

---

## Maximum size 

You can use both SQL Server Management Studio (SSMS) and Transact-SQL (T-SQL) to change the maximum size for your `tempdb` files. 

### [SQL Server Management Studio (SSMS)](#tab/ssms)

### [Transact-SQL (T-SQL)](#tab/tsql)

---

## tempdb limits 

The following table defines limits for various `tempdb` configuration settings: 


|Configuration setting  |Limits  |
|---------|---------|
|Logical names of `tempdb` files     | Maximum 16 characters      |
|Number of `tempdb` files     |  Maximum is 128      |
|Default number of `tempdb` files     |  13 (1 log file + 12 data files)        |
|Initial size of `tempdb` data files    | 16 MB        |
|Default growth increment of `tempdb` data files     |   256 MB      |
|Initial size of `tempdb` log files     |    16 MB     |
|Default growth increment of `tempdb` log files     |   64 MB      |
|Initial max `tempdb`size    |   -1 (unlimited)       |
|Max size of `tempdb` | Up to the storage size | 




## Next steps

To learn about specific changes related to the feature wave, see these articles:

- [Simplified connectivity architecture](connectivity-architecture-overview.md)
- [Instance stop/start](instance-stop-start-how-to.md)
- [Zone redundancy for Business Critical tier](../database/high-availability-sla.md)
- [Managed DTC](distributed-transaction-coordinator-dtc.md)
- [Frequently asked questions](frequently-asked-questions-faq.yml#november-2022-feature-wave)
