---
description: "MSSQLSERVER_3043"
title: "MSSQLSERVER_3043 | Microsoft Docs"
ms.custom: ""
ms.date: "10/05/2022"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3043 (Database Engine error)"
author: Pijocoder
ms.author: mathoma
---
# MSSQLSERVER_3043
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3043|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DMP_PAGE_ERROR_DURING_BACKUP|  
|Message Text|BACKUP '%ls' detected an error on page (%d:%d) in file '%ls'. |  
  
## Explanation

This error is raised during a backup operation if SQL Server detects that a page is damaged. Specifically you get this error if the page checksum validation fails during the backup operation. The validation failure is a result of a corrupt database page.  A record of the detected bad page would be added to the suspect_pages table in MSDB. 

A database page could be damaged due to a number of reasons including hardware failures and OS issues 


In this scenario SQL Server stops the backup operations and reports an error like the following: 

```output
Msg 3043, Level 16, State 1, Line 1 
BACKUP 'database_name' detected an error on page (file_id:page_number) in file 'database_file'. 
Msg 3013, Level 16, State 1, Line 1 
BACKUP DATABASE is terminating abnormally. 
```

When you use the CHECKSUM option during a backup operation, the following processes are enabled: 

- Validation of page checksum if the database has the PAGE_VERIFY option set to CHECKSUM and the database page was last written by using checksum protection. This checksum validation ensures that the data that is backed up is in a good state. 

- Generation of a backup checksum over the backup streams that are written to the backup file. During a restore operation, this makes sure that the backup media itself was not damaged during file copy or transfers. 

 
 
## User Action  

- Run DBCC CHECKDB on the impacted database to check its consistency state and address database inconsistencies. For more information see [Troubleshoot database consistency errors reported](/troubleshoot/sql/admin/troubleshoot-dbcc-checkdb-errors)

- Investigate your hardware to ensure no other database pages are impacted and that this issue does not occur in the future

- Restore the page from a good database backup. For more information, see [Restore Pages (SQL Server)](../backup-restore/restore-pages-sql-server.md)