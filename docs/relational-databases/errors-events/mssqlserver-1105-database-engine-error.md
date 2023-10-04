---
title: "MSSQLSERVER_1105"
description: "MSSQLSERVER_1105"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "1105 (Database Engine error)"
---
# MSSQLSERVER_1105
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1105|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NO_MORE_SPACE_IN_FG|  
|Message Text|Could not allocate space for object '%.*ls'%.\*ls in database '%.\*ls' because the '%.\*ls' filegroup is full. Create disk space by deleting unneeded files, dropping objects in the filegroup, adding additional files to the filegroup, or setting autogrowth on for existing files in the filegroup.|  
  
## Explanation  
No disk space is available in a filegroup.  
  
## User Action  
The following actions may make space available in the filegroup:  
  
-   Turn on autogrow.  
  
-   Add more files to the file group.  
  
-   Free disk space by dropping index or tables that are no longer needed.  
  
-   For more information, see [Troubleshooting Insufficient Data Disk Space](/previous-versions/sql/sql-server-2008-r2/ms366198(v=sql.105)).
  
> [!NOTE]  
> When an index is located on several files, **ALTER INDEX REORGANIZE** can return error 1105 when one of the files is full. The reorganization process is blocked when the process tries to move rows to the full file. To work around this limitation perform an **ALTER INDEX REBUILD** instead of **ALTER INDEX REORGANIZE** or increase the file growth limit of any files that are full.  
  
