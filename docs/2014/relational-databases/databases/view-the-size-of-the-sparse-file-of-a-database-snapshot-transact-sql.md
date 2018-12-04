---
title: "View the Size of the Sparse File of a Database Snapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshots [SQL Server database snapshots], sparse files"
  - "space [SQL Server], sparse files"
  - "sparse files [SQL Server]"
  - "size [SQL Server], sparse files"
  - "maximum sparse file size"
  - "database snapshots [SQL Server], sparse files"
  - "space [SQL Server], database snapshots"
ms.assetid: 1867c5f8-d57c-46d3-933d-3642ab0a8e24
author: stevestein
ms.author: sstein
manager: craigg
---
# View the Size of the Sparse File of a Database Snapshot (Transact-SQL)
  This topic describes how to use [!INCLUDE[tsql](../../includes/tsql-md.md)] to verify that a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database file is a sparse file and to find out its actual and maximum sizes. Sparse files, which are a feature of the NTFS file system, are used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database snapshots.  
  
> [!NOTE]  
>  During database snapshot creation, sparse files are created by using the file names in the CREATE DATABASE statement. These file names are stored in **sys.master_files** in the **physical_name** column. In **sys.database_files** (whether in the source database or in a snapshot), the **physical_name** column always contains the names of the source database files.  
  
## Verify that a Database File is a Sparse File  
  
1.  On the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
     Select the **is_sparse** column from either **sys.database_files** in the database snapshot or from **sys.master_files**. The value indicates whether the file is a sparse file, as follows:  
  
     1 = File is a sparse file.  
  
     0 = File is not a sparse file.  
  
## Find Out the Actual Size of a Sparse File  
  
> [!NOTE]  
>  Sparse files grow in 64-kilobyte (KB) increments; thus, the size of a sparse file on disk is always a multiple of 64 KB.  
  
 To view the number of bytes that each sparse file of a snapshot is currently using on disk, query the **size_on_disk_bytes** column of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][sys.dm_io_virtual_file_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-io-virtual-file-stats-transact-sql) dynamic management view.  
  
 To view the disk space used by a sparse file, right-click the file in Microsoft Windows, click **Properties**, and look at the **Size on disk** value.  
  
## Find Out the Maximum Size of a Sparse File  
 The maximum size to which a sparse can grow is the size of the corresponding source database file at the time of the snapshot creation. To learn this size, you can use one of the following alternatives:  
  
-   Using Windows Command Prompt:  
  
    1.  Use Windows **dir** commands.  
  
    2.  Select the sparse file, open the file **Properties** dialog box in Windows, and look at the **Size** value.  
  
-   On the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
     Select the **size** column from either **sys.database_files** in the database snapshot or from **sys.master_files**. The value of **size** column reflects the maximum space, in SQL pages, that the snapshot can ever use; this value is equivalent to the Windows **Size** field, except that it is represented in terms of the number of SQL pages in the file; the size in bytes is:  
  
     ( *number_of_pages* * 8192)  
  
## See Also  
 [Database Snapshots &#40;SQL Server&#41;](database-snapshots-sql-server.md)   
 [sys.fn_virtualfilestats &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-virtualfilestats-transact-sql)   
 [sys.database_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql)   
 [sys.master_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-master-files-transact-sql)  
  
  
