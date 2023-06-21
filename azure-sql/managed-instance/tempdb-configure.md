---
title: Configure tempdb settings
titleSuffix: Azure SQL Managed Instance
description: Learn how to configure `tempdb` settings for Azure SQL Managed Instance, such as the number of `tempdb` files, the growth increment, and the maximum size of `tempdb`. 
author: MashaMSFT
ms.author: mathoma
ms.date: 06/25/2023
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

`tempdb` settings persist after your instance is restarted, updated, or if there is a failover. 

## Overview 

`tempdb` is one of the default system databases that comes with Azure SQL Managed Instance. The structure of `tempdb` is the same as any other user database structure; the difference is that since `tempdb` used for non-durable storage, transactions are minimally logged. 

`tempdb` cannot be dropped, detached, taken offline, renamed, or restored. Attempting any of these operations returns an error. `tempdb` is regenerated upon every start of the server instance and any objects that may have been created in `tempdb` during a previous session don't persist when the service restarts, after an instance update management operation or a failover.

The workload in `tempdb` differs from workloads in other user databases; objects and data are frequently created and destroyed and there is extremely high concurrency. There is only one `tempdb` for each managed instance. Even if you have multiple databases and applications connecting to the instance, they all use the same `tempdb` database. Services may experience contention when they try to allocate pages in a heavily-used `tempdb`. Depending on the degree of contention, queries and requests that involve `tempdb` could become unresponsive. This is why `tempdb` is critical to the performance of the service.

## Number of `tempdb` files

Increasing the number of `tempdb` data files creates one or more GAM and SGAM pages for each data file, which helps improve `tempdb` concurrency and reduces PFC page contention. However, increasing the number of `tempdb` data files could have other performance implications, so test thoroughly before implementing in production. 

By default, Azure SQL Managed Instance creates 12 `tempdb` data files, and 1 `tempdb` log file, but it's possible to modify this configuration. 

Modifying the number of `tempdb` files has the following limitations:
- The logical name of the new file is case insensitive, with a maximum of 16 characters and no spaces. 
- The maximum number of `tempdb` files is 128. 

> [!NOTE]
> You do not have to restart the server after adding new files; however, the emptier files will be filled with higher priority and the round-robin algorithm for allocating pages will be shortly lost, until the system is rebalanced.

You can use both SQL Server Management Studio (SSMS) and Transact-SQL (T-SQL) to change the number of files for `tempdb` in Azure SQL Managed Instance. 
 
### [SQL Server Management Studio (SSMS)](#tab/ssms)

You can use SQL Server Management Studio (SSMS) to modify the number of `tempdb` files. To do so, follow these steps: 

1. Connect to your managed instance in SSMS. 
1. Expand **Databases** in **Object Explorer**, and then expand **System databases**. 
1. Right-click `tempdb`, and choose **Properties**. 
1. Select **Files** under **Select a page** to view the existing number of `tempdb` files.
1. To add a file, choose **Add** and then provide information about the new data file in the row. 

   :::image type="content" source="media/tempdb-configure/add-new-tempdb-file.png.png" alt-text="Screenshot of Database Properties in SSMS, with new database file name highlighted. ":::

1. To remove a `tempdb` file, choose the file you want to remove from the list of database files, and then select **Remove**. 

### [Transact-SQL (T-SQL)](#tab/tsql)

You can use Transact-SQL (T-SQL) to check the number of existing `tempdb` files, as well as add or remove files. 

To count the number of all existing `tempdb` files, use the following command: 

```sql
USE tempdb
SELECT COUNT(*) TempDBFiles FROM sys.database_files
```

To count only the number of `tempdb` data files, use the following command: 

```sql
USE tempdb
SELECT COUNT(*) TempDBFiles FROM sys.database_files where type = 0
```

To add a new `tempdb` file, use the following command: 

```sql
ALTER DATABASE tempdb ADD FILE (NAME = 'file_name')
```

To remove an existing `tempdb` file, use the following command: 

```sql
ALTER DATABASE tempdb ADD FILE (NAME = 'file_name')
```

---


## Growth increment 

`tempdb` file growth can have a performance impact to queries using `tempdb`. As such, `tempdb` data file growth increments that are too small may cause extent fragmentation, while increments that are too large may result in slow growth, or growth failure if there isn't sufficient space for the growth to happen. The optimal value for `tempdb` file growth increments depends on the your workload. 

The default growth increments for SQL Managed Instance are 254 MB for `tempdb` data files, and 64 MB for `tempdb` log files, but you can configure growth increments to adapt to your workload and tune your performance. 

Consider the following: 
- The file growth parameter supports the following units for `int_growth_increment`: KB, MB, GB, TB, and %. 
- Growth increments should be the same for all `tempdb` data files as otherwise, the round robin algorithm that allocates pages could be impacted. 

You can use both SQL Server Management Studio (SSMS) and Transact-SQL (T-SQL) to change the growth increment for your  `tempdb` files. 

### [SQL Server Management Studio (SSMS)](#tab/ssms)

You can use SQL Server Management Studio (SSMS) to modify the the growth increment of `tempdb` files. To do so, follow these steps: 

1. Connect to your managed instance in SSMS. 
1. Expand **Databases** in **Object Explorer**, and then expand **System databases**. 
1. Right-click `tempdb`, and choose **Properties**. 
1. Select **Files** under **Select a page** to view the existing number of `tempdb` files.
1. Choose the ellipses (...) next to a data file to open the **Change Autogrowth properties** dialog window. 
1. Modify your autogrowth settings by specifying the file growth values.
1. Select **OK** to save your settings. 


### [Transact-SQL (T-SQL)](#tab/tsql)

Use the following T-SQL command to modify the growth increments for your `tempdb` data files: 

```sql
ALTER DATABASE tempdb ADD FILE (NAME = 'file_name', FILEGROWTH = int_growth_increment [KB|MB|GB|TB|%])
```

---

## Maximum size 

`tempdb` **size** is the sum size of all `tempdb` files. `tempdb` file size is an allocated (zeroed) space for that `tempdb` file. The initial file size for all `tempdb` files is 16 MB, which is the size of all `tempdb` files when the instance restarts, or fails over. Once a `tempdb` data file's used space reaches the file size, all `tempdb` data files automatically grow by their configured growth increments. 

`tempdb` **used space** is the sum of the used space of all `tempdb` files. `tempdb` file used space is equal to the part of that `tempdb` file size which is occupied with non-zero information. The sum of `tempdb` **used space** and `tempdb` **free space** is equal to the `tempdb` size. 

You can use T-SQL to determine the current used and free space for your `tempdb` files. 

To get used space, free space, and size of your `tempdb` data files, run this command: 

```sql
USE tempdb
SELECT SUM((allocated_extent_page_count)*1.0/128) AS TempDB_used_data_space_inMB, 
	SUM((unallocated_extent_page_count)*1.0/128) AS TempDB_free_data_space_inMB, 
	SUM(total_page_count*1.0/128) AS TempDB_data_size_inMB 
FROM sys.dm_db_file_space_usage
```

To get the used space, free space, and size of your `tempdb` log files, run this command: 

```sql
USE tempdb
SELECT used_log_space_in_bytes*1.0/1024/1024 AS TempDB_used_log_space_inMB,
     (total_log_size_in_bytes- used_log_space_in_bytes)*1.0/1024/1024 AS TempDB_free_log_space_inMB,
     total_log_size_in_bytes*1.0/1024/1024 AS TempDB_log_size_inMB
FROM sys.dm_db_log_space_usage
```

`tempdb` max size is the limit after which your `tempdb` can't grow further. 

`tempdb` max size in SQL Managed Instance has the following limitations: 
- In the General Purpose service tier, the maximum size for `tempdb` is limited to 24 GB/vCore (96-1920 GB), and the log file is 120 GB. 
- In the Business Critical service tier, `tempdb` competes with other databases for resources, so the reserved storage is shared between `tempdb` and other databases. The max size of the `tempdb` log file is 2 TB. 

`tempdb` files grow until they reach either the maximum limit allowed by the service tier, or by the manually configured max `tempdb` file size. 

You can use both SQL Server Management Studio (SSMS) and Transact-SQL (T-SQL) to change the maximum size for your `tempdb` files. 


### [SQL Server Management Studio (SSMS)](#tab/ssms)

To determine your current `tempdb` max size in SSMS, follow these steps: 

1. Connect to your managed instance in SSMS. 
1. Expand **Databases** in **Object Explorer**, and then expand **System databases**. 
1. Right-click `tempdb`, and choose **Properties**.
1. On the **General page**, check the **Size** value under **Database** to determine your max tempdb size. A value of `-1` indicates tempdb max size is unlimited. 



To change your current `tempdb` max size in SSMS, follow these steps: 

1. Connect to your managed instance in SSMS. 
1. Expand **Databases** in **Object Explorer**, and then expand **System databases**. 
1. Right-click `tempdb`, and choose **Properties**. 
1. Select **Files** under **Select a page** to view the existing number of `tempdb` files.
1. Choose the ellipses (...) next to a data file to open the **Change Autogrowth properties** dialog window. 
1. Modify your `tempdb` max size settings by changing the values under **Maximum file size**. 
1. Select **OK** to save your settings. 


### [Transact-SQL (T-SQL)](#tab/tsql)


To determine `tempdb` max size, run this command: 

```sql
USE tempdb
SELECT name, max_size FROM sys.database_files
```

:::image type="content" source="media/tempdb-configure/tempdb-max-size.png" alt-text="Query result window in SSMS showing the max size of tempdb files. ":::

To get the total `tempdb` size in MB, run this command: 

```sql
USE tempdb
SELECT (SUM(size)*1.0/128) AS TempDB_size_InMB FROM sys.database_files
```



:::image type="content" source="media/tempdb-configure/tempdb-size-in-megabytes.png.png" alt-text="Screenshot of query results in SSMS showing tempdb size in megabytes.":::


To set the max size for a new `tempdb` data file, run the following command: 

```sql
ALTER DATABASE tempdb ADD FILE (NAME = 'file_name', MAXSIZE = int_maxsize[KB|MB|GB|TB])
```

To change the max size of an existing `tempdb` file, run the following command: 

```sql
ALTER DATABASE tempdb MODIFY FILE (NAME = file_name, MAXSIZE = int_maxsize[KB|MB|GB|TB])
```

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
