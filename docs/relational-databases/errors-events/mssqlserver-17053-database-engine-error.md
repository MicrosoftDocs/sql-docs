---
title: "MSSQLSERVER_17053"
description: "MSSQLSERVER_17053"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: jopilov
ms.date: 01/17/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17053 (Database Engine error)"
---
# MSSQLSERVER_17053

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 17053 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | OS_ERROR |
| Message Text | %ls: Operating system error %ls encountered. |

## Explanation

Generic operating system error occurred. The error message wraps a more specific operating system (OS) error, which can be used to help diagnose that failure. Examples would include reads or writes to data or log files that fail, registry read/write operations, or other unexpected [Win32 API](/windows/win32/api/) call failures.  
You'll frequently see this error together with other error messages in the SQL Server error log. The operating system errors are shown with their numeric value, followed by the text message of the error.

## User Action

Here are examples of how you may see 17053 error together with other errors. Each example provides ideas on how to approach the specific scenario.

### Example with OS error 665

In this case, the underlying OS error 665 indicates a file system limitation has been encountered during file write or read.

```output
Error: 17053, Severity: 16, State: 1.
K:\DATA\MyDB.MDF_MSSQL_DBCC11: Operating system error 665(The requested operation could not be completed due to a file system limitation) encountered.

The operating system returned error 665(The requested operation could not be completed due to a file system limitation) to SQL Server during a write at offset 0x00031397ce2000 in file 'K:\DATA\MyDB.MDF_MSSQL_DBCC11'.
```

**Resolution:**

If you run into this scenario, follow the steps in this article to resolve:
[OS errors 665 and 1450 are reported for SQL Server files](/troubleshoot/sql/database-engine/database-file-operations/1450-and-665-errors-running-dbcc-checkdb)

### Example with SQL Server error 9001 and underlying OS errors 1117 and 21

In this case, the underlying OS error 1117 indicates the disk device has an error or is physically damaged.

```output
Error: 17053, Severity: 16, State: 1.
SQLServerLogMgr::LogWriter: Operating system error 1117(The request could not be performed because of an I/O device error.) encountered.

Write error during log flush.

Error: 9001, Severity: 21, State: 5.
The log for database 'SQLContoso' is not available. Check the operating system error log for related error messages. Resolve any errors and restart the database.
```

In this case the underlying OS error is 21, which indicates the disk device is offline and not available for the OS and SQL Server to use.

```output
Error: 17053, Severity: 16, State: 1.
SQLServerLogMgr::LogWriter: Operating system error 21(The device is not ready.) encountered.
Write error during log flush.

Error: 9001, Severity: 21, State: 4.
The log for database 'ContosoDB' is not available. Check the operating system error log for related error messages. Resolve any errors and restart the database.
```

**Resolution:**

If you encounter a similar scenario, address the underlying OS error. In this case work with your system administrator and hardware vendor to ensure that the disk device is online, functioning properly and there are no errors and damage reported. In cases like this one, you may have to check the physical integrity of the databases once the disk device is restored by running DBCC CHECKDB. If database damage is reported, restore a last known good database backup.

### Example with SQL Server error 9001 and underlying OS errors 170

In this case, the underlying OS error 170 indicates the files on disk are being used or locked by some other program, most commonly a file system filter driver.

```output
Error: 17053, Severity: 16, State: 1.
SQLServerLogMgr::LogWriter: Operating system error 170(The requested resource is in use.) encountered.

Write error during log flush.

Error: 9001, Severity: 21, State: 5.
The log for database 'SQLContoso' is not available. Check the operating system error log for related error messages. Resolve any errors and restart the database.
```

**Resolution:**

If you encounter a similar scenario, address the underlying OS error. In this case work with your system administrator to ensure that the database and log files aren't locked by other programs. Most commonly anti-virus or host protection software, defragmentation software, or backup software that holds locks on the files for a long time may cause this OS error. Make sure to exclude database files from being scanned or used; see [How to choose antivirus software to run on computers that are running SQL Server](https://support.microsoft.com/en-us/topic/how-to-choose-antivirus-software-to-run-on-computers-that-are-running-sql-server-feda079b-3e24-186b-945a-3051f6f3a95b).

In WSFC (clustered) environments, if drives aren't properly configured on the back end they may lock database files when they aren't supposed to.
For more information on storage for WSFC, see [Failover Clustering hardware solution](../../sql-server/failover-clusters/install/before-installing-failover-clustering.md)
and [Failover cluster instances with SQL Server on Azure Virtual Machines - Storage](/azure/azure-sql/virtual-machines/windows/failover-cluster-instance-overview#storage)

### Example with SQL Server error 9002 and underlying OS error 112

In this case, the underlying OS error 112 indicates the disk volume is out of space.

```output
Error: 17053, Severity: 16, State: 1.
L:\SQLLOG\Contoso.LDF: Operating system error 112(There is not enough space on the disk.) encountered.

Error: 9002, Severity: 17, State: 5.
The transaction log for database 'ContosoDb' is full due to 'DATABASE_MIRRORING'.

Error: 5149, Severity: 16, State: 3.
MODIFY FILE encountered operating system error 112(There is not enough space on the disk.) while attempting to expand the physical file 'L:\SQLLOG\Contoso.LDF'.
```

**Resolution:**

If you encounter a similar scenario, address the underlying OS 112 error. In this case, work with your system administrator to free up disk space on the device and then attempt to address the full transaction log. For detailed steps on troubleshooting error 9002, see [Troubleshoot a Full Transaction Log (SQL Server Error 9002)](../logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md).

### Example with error 17053 and 5173

In this case, the underlying OS error 112 is reported at startup because a database file (frequently `tempdb`) is configured with an *initial size* that's larger than the free space on its drive. SQL Server can't create or grow the file, and the instance fails to start. Because SQL Server can't start, you can't fix the file size through a normal connection. You first need to bring SQL Server up with minimal configuration.

```output
2026-07-19 17:18:46.70 spid21s     Error: 17053, Severity: 16, State: 1.
2026-07-19 17:18:46.70 spid21s     D:\SQLTemp\Log\tempdb.ldf: Operating system error 112(There is not enough space on the disk.) encountered.

2026-07-19 17:18:46.72 spid21s     Error: 5173, Severity: 16, State: 1.
2026-07-19 17:18:46.72 spid21s     One or more files do not match the primary file of the database. If you are attempting to attach a database, retry the operation with the correct files. If this is an existing database, the file may be corrupted and should be restored from a backup.

2026-07-19 17:18:46.72 spid21s     Error: 1802, Severity: 16, State: 4.
2026-07-19 17:18:46.72 spid21s     CREATE DATABASE failed. Some file names listed could not be created. Check related errors.
```

**Resolution:**

If SQL Server fails to start and repeatedly crashes because a database file's configured initial size exceeds the disk's free space:

1. Start the SQL Server service with [minimal configuration](../../database-engine/configure-windows/start-sql-server-with-minimal-configuration.md) (`-f` startup parameter). This starts SQL Server with a minimal `tempdb` and lets you connect through the dedicated administrator connection (DAC).

1. Inspect the configured initial size of the user database files:

   ```sql
   SELECT
       DB_NAME(database_id) AS DatabaseName,
       name AS LogicalFileName,
       type_desc AS FileType,       -- ROWS = Data file, LOG = Log file
       size * 8 / 1024 AS SizeMB    -- Initial size in MB
   FROM sys.master_files
   WHERE database_id > 4            -- Exclude system databases
   ORDER BY DatabaseName, FileType;
   ```

1. For any file whose initial size is larger than the available disk space, reduce it to a value that fits:

   ```sql
   ALTER DATABASE YourDatabaseName
   MODIFY FILE
   (
       NAME = YourDataFileLogicalName,
       SIZE = 512MB                 -- new initial size
   );
   ```

1. Close the DAC connection and restart the SQL Server service normally from SQL Server Configuration Manager or the Services console.

For `tempdb`, the same principle applies: reduce the configured initial size of the `tempdb` data or log files so they fit the drive, then restart SQL Server. For more information on sizing `tempdb`, see [Optimize tempdb performance in SQL Server](../databases/tempdb-database.md#optimize-tempdb-performance-in-sql-server).