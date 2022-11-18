---
description: "MSSQLSERVER_1101"
title: "MSSQLSERVER_1101 | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1101 (Database Engine error)"
ms.assetid: d63b67d5-59f5-4f77-904e-5ba67f2dd850
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_1101
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1101|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NOALLOCPG|  
|Message Text|Could not allocate a new page for database '%.*ls' because of insufficient disk space in filegroup '%.\*ls'. Create the necessary space by dropping objects in the filegroup, adding additional files to the filegroup, or setting autogrowth on for existing files in the filegroup.|  
  
## Explanation  
No disk space available in a filegroup.  
  
## User Action  
The following actions may make space available in the filegroup.  
  
-   Turn on AUTOGROW.  
  
-   Add more files to the file group.  
  
-   Free up disk space by dropping unnecessary indexes or tables in the filegroup.  
  
This T-SQL script can help you diagnose which files are full and offer a solution command to resolve the issue. Note that it does not diagnose disk space issues.

```sql
set nocount on
declare @prcnt_full int = 95
SELECT db_name(database_id) DbName,
       name LogName,
       physical_name,
       type_desc ,
	   convert(bigint, SIZE)/128 File_Size_MB ,
       convert(bigint,(case when max_size = -1 then 17179869176 else max_size end))/128 File_MaxSize_MB ,
	   (size/(case when max_size = -1 then 17179869176 else max_size end)) *100 as percent_full_of_max_size
FROM sys.master_files
WHERE file_id != 2
  AND (size/(case when max_size = -1 then 17179869176 else max_size end)) *100 > @prcnt_full


if @@ROWCOUNT > 0
BEGIN
    DECLARE @db_name_max_size sysname, @log_name_max_size sysname, @configured_max_log_boundary bigint 
    
    DECLARE reached_max_size CURSOR FOR
		SELECT db_name(database_id) DbName,
			   name LogName,
			   convert(bigint, SIZE)/128 File_Size_MB
		FROM sys.master_files
		WHERE file_id != 2
		  AND (size/(case when max_size = -1 then 17179869176 else max_size end)) *100 > @prcnt_full


    OPEN reached_max_size

    FETCH NEXT FROM reached_max_size into @db_name_max_size , @log_name_max_size, @configured_max_log_boundary 

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT 'The database "' + @db_name_max_size+'" contains a data file "' + @log_name_max_size + '" whose max limit is set to ' + convert(varchar(24), @configured_max_log_boundary) + ' MB and this limit is close to be reached!' as Finding
        SELECT 'Consider using one of the below ALTER DATABASE commands to either change the log file size or add a new file' as Recommendation
        SELECT 'ALTER DATABASE ' + @db_name_max_size + ' MODIFY FILE ( NAME = N''' + @log_name_max_size + ''', MAXSIZE = UNLIMITED)' as SetUnlimitedSize
        SELECT 'ALTER DATABASE ' + @db_name_max_size + ' MODIFY FILE ( NAME = N''' + @log_name_max_size + ''', MAXSIZE = something_larger_than_' + CONVERT(varchar(24), @configured_max_log_boundary) +'MB )' as IncreaseFileSize
        SELECT 'ALTER DATABASE ' + @db_name_max_size + ' ADD FILE ( NAME = N''' + @log_name_max_size + '_new'', FILENAME = N''SOME_FOLDER_LOCATION\' + @log_name_max_size + '_NEW.NDF'', SIZE = 81920KB , FILEGROWTH = 65536KB )' as AddNewFile

        FETCH NEXT FROM reached_max_size into @db_name_max_size , @log_name_max_size, @configured_max_log_boundary 

    END

    CLOSE reached_max_size
    DEALLOCATE reached_max_size
END
ELSE
    SELECT 'Found no files that have reached max log file size' as Findings

```
