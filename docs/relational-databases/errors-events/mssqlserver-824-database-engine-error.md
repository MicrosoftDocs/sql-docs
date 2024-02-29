---
title: "MSSQLSERVER_824"
description: "MSSQLSERVER_824: SQL Server detected a logical consistency-based I/O error."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: jopilov, randolphwest
ms.date: 01/22/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "conceptual"
helpviewer_keywords:
  - "824 (Database Engine error)"
---
# MSSQLSERVER_824

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product name | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] |
| Event ID | 824 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | B_HARDSSERR |
| Message Text | SQL Server detected a logical consistency-based I/O error: %ls. It occurred during a %S_MSG of page %S_PGID in database ID %d at offset %#016I64x in file '%ls'. Additional messages in the SQL Server error log or operating system error log may provide more detail. This is a severe error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see https://go.microsoft.com/fwlink/?linkid=2252374. |

## Symptom

You might encounter the following error message in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log or the Windows Application event log if a logical consistency check fails after reading or writing a database page:

```output
2022-11-02 15:46:42.90 spid51      Error: 824, Severity: 24, State: 2.
2022-11-02 15:46:42.90 spid51      SQL Server detected a logical consistency-based I/O error: incorrect pageid (expected 1:43686; actual 0:0). It occurred during a read of page (1:43686) in database ID 23 at offset 0x0000001554c000 in file 'H:\MSSQL16.MSSQLSERVER\MSSQL\DATA\my_db.mdf'. Additional messages in the SQL Server error log or operating system error log may provide more detail. This is a severe error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see https://go.microsoft.com/fwlink/?linkid=2252374.
```

If a SELECT or DML query runs into this message, the error message is returned to the application, and the database connection is terminated.

## Cause

This error indicates that Windows reports that the page is successfully read from disk, but [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] discovered something wrong with the page. This error is similar to [Error 823](mssqlserver-823-database-engine-error.md), except that Windows didn't detect the error. Error 824 usually indicates a problem in the I/O subsystem such as failing disk drives, firmware problems, faulty device drivers, and so on. For more information about I/O errors, see [Microsoft SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10)).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses the following Windows APIs to perform the I/O operations: `ReadFile`, `WriteFile`, `ReadFileScatter`, and `WriteFileGather`. After completing these I/O operations, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] checks for any error conditions associated with these API calls. If these API calls fail with an Operating System error, then [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reports Error 823. There can be situations where the Windows API call actually succeeds, but the data transferred by the I/O operation might have encountered a logical consistency problem. These logical consistency problems are reported through Error 824.

The 824 error contains the following information:

- The database file against which the I/O operation is performed
- The offset with the file where the I/O operation was attempted
- The database to which this file belongs
- The page number that was involved in the I/O operation
- Was the operation a read or write operation
- Details about the logical consistency check that failed (the type of check, actual value, and expected value used for this check)

These logical consistency checks are integrity checks performed by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to ensure key elements of the data that was involved in the I/O transfer remained intact throughout the I/O operation. The checks include Checksum, Torn Page, Short transfer, Bad Page ID, Stale Read, and Page Audit Failure. The nature of the checks performed vary depending on different configuration options at the database and server level.

The 824 error message usually indicates that there's a problem with underlying storage system or the hardware or a driver that is in the path of the I/O request. You can encounter this error when there are inconsistencies in the file system or if the database file is damaged.

## Resolution

If you encounter error 824, you can try the following resolutions:

- Review the [suspect_pages](../backup-restore/manage-the-suspect-pages-table-sql-server.md) table in `msdb` to check if other pages (in the same database or different databases) are encountering this problem.

  ```sql
  SELECT * FROM msdb..suspect_pages
  WHERE (event_type = 1 OR event_type = 2 OR event_type = 3);
  ```

- Check the consistency of the databases that are located in the same volume (as the one reported in the 824 message) using DBCC CHECKDB command. If you find inconsistencies from the `DBCC CHECKDB` command, use the guidance from Knowledge Base article [How to troubleshoot database consistency errors reported by DBCC CHECKDB](/troubleshoot/sql/admin/troubleshoot-dbcc-checkdb-errors).

  ```sql
  DBCC CHECKDB;
  ```

- If the database that encounters these 824 errors doesn't have the `PAGE_VERIFY CHECKSUM` database option turned on, turn on the option immediately. 824 errors can occur for other reasons than a checksum failure but CHECKSUM provides the best option to verify consistency of the page after it has been written to disk. Use this script to identify databases where CHECKSUM option isn't enabled:

  ```sql
  SELECT * FROM sys.databases
  WHERE page_verify_option_desc != 'CHECKSUM';
  ```

- Review the Windows Event logs for any errors or messages reported from the Operating System or a Storage Device or a Device Driver. If they're related to this error in some manner, you should address those errors first. For example, apart from the 824 message, you might also notice an  event like "The driver detected a controller error on \Device\Harddisk4\DR4" reported by the Disk source in the Event Log. In that case, you have to evaluate if this file is present on this device and then first correct those disk errors.
- Use the [SQLIOSim](/troubleshoot/sql/tools/sqliosim-utility-simulate-activity-disk-subsystem) utility to find out if these 824 errors can be reproduced outside of regular [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] I/O requests. SQLIOSim ships with [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, so there's no need for a separate download.
- Work with your hardware vendor or device manufacturer to ensure:
  - The hardware devices and configuration conform to the [I/O requirements of SQL Server](/troubleshoot/sql/admin/database-engine-input-output-requirements).
  - The device drivers and other supporting software components of all devices in the I/O path are updated.
- If the hardware vendor or device manufacturer provided you with any diagnostic utilities, use them to evaluate the health of the I/O system.
- Evaluate if there are Filter Drivers that exist on the I/O path of these requests. You can run the following commands to list all filter drivers on the system:

  ```console
  fltmc filters
  fltmc instances
  ```

  - Exclude database and log files from being scanned by such filter drivers. For more information, see [Directories and file name extensions to exclude from virus scanning](https://support.microsoft.com/topic/how-to-choose-antivirus-software-to-run-on-computers-that-are-running-sql-server-feda079b-3e24-186b-945a-3051f6f3a95b#:~:text=Directories%20and%20file%20name%20extensions%20to%20exclude%20from%20virus%20scanning)
  - Check if there are any updates to these filter drivers
  - Can these filter drivers be removed or disabled to observe if the problem that results in the 824 error goes away?

- If you're running a virtual machine, ensure all virtualization drivers are updated or check with the virtualization vendor for more information.
- If the problem isn't hardware-related and a known clean backup is available, restore the database from the backup.

## Related content

- [Manage the suspect_pages Table (SQL Server)](../backup-restore/manage-the-suspect-pages-table-sql-server.md)
