---
description: "MSSQLSERVER_824"
title: "MSSQLSERVER_824 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "824 (Database Engine error)"
ms.assetid: 2aa22246-2712-4fdb-9744-36e7e6f3175e
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_824
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|824|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|B_HARDSSERR|  
|Message Text|SQL Server detected a logical consistency-based I/O error: %ls. It occurred during a %S_MSG of page %S_PGID in database ID %d at offset %#016I64x in file '%ls'.  Additional messages in the SQL Server error log or system event log may provide more detail.|  
  
## Symptom  


You might encounter the following error message in the SQL Server error log or the Windows Application event log if a logical consistency check fails after reading or writing a database page:
 
``` 
2009-11-02 15:46:42.90 spid51      Error: 824, Severity: 24, State: 2.
2009-11-02 15:46:42.90 spid51      SQL Server detected a logical consistency-based I/O error: incorrect pageid (expected 1:43686; actual 0:0). It occurred during a read of page (1:43686) in database ID 23 at offset 0x0000001554c000 in file 'H:\MSSQL.SQL2008\MSSQL\DATA\my_db.mdf'.  Additional messages in the SQL Server error log or system event log may provide more detail. This is a severe error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.
```
 
If an application encounters this message while querying or modifying data, the error message is returned to the application and the database connection is terminated. 
  
## Cause
This error indicates that Windows reports that the page is successfully read from disk, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has discovered something wrong with the page. This error is similar to error 823 except that Windows did not detect the error and usually indicates a problem in the I/O subsystem, such as a failing disk drive, disk firmware problems, faulty device driver, and so on. For more information about I/O errors, see [Microsoft SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10)).  

SQL Server uses Windows APIs, e.g.,  ReadFile, WriteFile, ReadFileScatter, WriteFileGather] to perform the I/O operations. After performing these I/O operations, SQL Server checks for any error conditions associated with these API calls. If these API calls fail with an Operating System error, then SQL Server reports the Error 823. There can be situations where the Windows API call actually succeeds but the data transferred by the I/O operation might have encountered a logical consistency problem. These logical consistency problems are reported through Error 824.
 
The 824 error contains the following information:

- The database file against which the I/O operation is performed
- The offset with the file where the I/O operation was attempted
- The database to which this file belongs
- The page number that was involved in the I/O operation
- Was the operation a read or write operation
- Details about the logical consistency check that failed [The type of check, actual value and expected value used for this check]
 
These logical consistency checks are additional integrity checks performed by SQL Server to ensure certain key aspects of the data that was involved in the data transfer was maintained throughout the I/O Operation. The checks include checksum, Torn Page, Short Transfer, Bad Page ID, Stale Read, Page Audit Failure. The nature of the checks performed vary depending on different configuration options at the database and server level. 
 
The 824 error message usually indicates that there is a problem with underlying storage system or the hardware or a driver that is in the path of the I/O request. You can encounter this error when there are inconsistencies in the file system or if the database file is damaged.

## Resolution  

If you encounter error 824, you can try the following resolutions: 

- Review the [suspect_pages](../backup-restore/manage-the-suspect-pages-table-sql-server.md) table in msdb to check if other pages [in the same database or different databases] are encountering this problem.
- Check the consistency of the databases that are located in the same volume [as the one reported in the 824 message] using DBCC CHECKDB command. If you find inconsistencies from the DBCC CHECKDB command, use the guidance from Knowledge Base article [How to troubleshoot database consistency errors reported by DBCC CHECKDB](https://support.microsoft.com/help/2015748/how-to-troubleshoot-database-consistency-errors-reported-by-dbcc-check).
- If the database that encounters these 824 errors does not have the PAGE_VERIFY CHECKSUM database option turned on, do so immediately. 824 errors can occur for other reasons than a checksum failure but CHECKSUM provides the best option to verify consistency of the page after it has been written to disk.
- Review the Windows Event logs for any errors or messages reported from the Operating System or a Storage Device or a Device Driver. If they are related to this error in some manner, please address those errors first. For example, apart from the 824 message, you may also notice an  event like "The driver detected a controller error on \Device\Harddisk4\DR4" reported by the Disk source in the Event Log. In that case, you have to evaluate if this file is present on this device and then first correct those disk errors.
- Use the [SQLIOSim](https://support.microsoft.com/help/231619/how-to-use-the-sqliosim-utility-to-simulate-sql-server-activity-on-a-d) utility to find out if these 824 errors can be reproduced outside of regular SQL Server I/O requests. SQLIOSim ships with SQL Server 2008 so there is no need for a separate download on this version or later.
- Work with your hardware vendor or device manufacturer to ensure:
   - The hardware devices and configuration conforms to the [I/O requirements of SQL Server](https://support.microsoft.com/help/967576/microsoft-sql-server-database-engine-input-output-requirements).
   - The device drivers and other supporting software components of all devices in the I/O path are updated.
- If the hardware vendor or device manufacturer provided you with any diagnostic utilities,  use them to evaluate the health of the I/O system.
- Evaluate if there are Filter Drivers that exist in the path of these I/O requests that encounter problems.
   - Check if there are any updates to these filter drivers
   - Can these filter drivers be removed or disabled to observe if the problem that results in the 824 error goes away
- If the problem is not hardware-related and a known clean backup is available, restore the database from the backup.  

## See Also  
[Manage the suspect_pages Table &#40;SQL Server&#41;](~/relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
