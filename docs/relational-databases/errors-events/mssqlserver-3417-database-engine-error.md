---
title: "MSSQLSERVER_3417"
description: "MSSQLSERVER_3417"
author: MashaMSFT
ms.author: mathoma
ms.date: "12/07/2022"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3417 (Database Engine error)"
---
# MSSQLSERVER_3417
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3417|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REC_BADMASTER|  
|Message Text|Cannot recover the master database. SQL Server is unable to run. Restore master from a full backup, repair it, or rebuild it. For more information about how to rebuild the master database, see SQL Server Books Online.|  
  
## Explanation  
SQL Server can't start the `master` database. If the `master` or `tempdb` databases can't be brought online, SQL Server can't run. This error is typically preceded by other errors. Review error logs for those errors to find the root cause.  
This error may be encountered during an upgrade of SQL Server to a Cumulative Update or Service Pack. The upgrade scripts may fail due to an underlying issue and may cause the `master` database to not recover. Again the underlying cause is to be found in the latest SQL Server error log prior to this message.

## User Action  
- [Restore backup of the master database](../backup-restore/restore-the-master-database-transact-sql.md). Or rebuild the [system databases](../databases/rebuild-system-databases.md#rebuild-system-databases-for-an-instance-of-sql-server) followed by attaching or restoring the remaining databases (`msdb`, user databases, `SSISDB`, etc.)
- If you see this error during an upgrade failure, refer to [Troubleshoot common SQL Server Cumulative Update installation issues](/troubleshoot/sql/install/sqlserver-patching-issues) and error 912 - [MSSQLSERVER_912](mssqlserver-912-database-engine-error.md).
