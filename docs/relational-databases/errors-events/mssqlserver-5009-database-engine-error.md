---
description: "MSSQLSERVER_5009"
title: MSSQLSERVER_5009
ms.custom: ""
ms.date: 08/20/2020
ms.prod: sql
ms.reviewer: ramakoni, pijocoder, suresh-kandoth, Masha
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "5009 (Database Engine error)"
ms.assetid: 
author:
ms.author: ramakoni, sureshka
---
# MSSQLSERVER_5009

 [!INCLUDE [SQL Server](../../includes/ssnoversion-md.md)]
 [!INCLUDE [SQL Server 2019](../../includes/sssqlv15-md.md)]
 [!INCLUDE [SQL Server 2017](../../includes/sssql17-md.md)]
 [!INCLUDE [SQL Server 2016](../../includes/sssql15-md.md)]
 [!INCLUDE [SQL Server 2014](../../includes/sssql14-md.md)]
 [!INCLUDE [SQL Server 2012](../../includes/sssql11-md.md)]
 [!INCLUDE [SQL Server 2008](../../includes/sskatmai-md.md)]
 [!INCLUDE [Azure SQL DB](../../includes/sssdsfull-md.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|5009|
|Event Source|MSSQLSERVER|
|Component|SQL SQLEngine|
|Symbolic Name|ALT_BADDISKS|
|Message Text|One or more files listed in the statement could not be found or could not be initialized|
||

## Explanation

This error indicates that you specified a file name or fileID in ALTER DATABASE or DBCC SHRINK* command that could not be resolved.

Consider the following scenario:

- You have a Microsoft SQL Server database that uses a full or bulk-logged recovery model.
- You add a new data file that is named *db_file1* to the database.
- You set the file type for the `db_file1` file as data.
- You realize that you specified the file type incorrectly.
- You remove the `db_file1` file, and then you back up the transaction log for this database.
- You add a new log file that is named *db_file1* to the same database.
- You try to remove the log file that is named *db_file1* by using the ALTER DATABASE statement or by using SQL Server Management Studio.

In this scenario, you receive an error message that resembles the following:

> Msg 5009, Level 16, State 9, Line 1
One or more files listed in the statement could not be found or could not be initialized.

## Possible causes

This issue occurs if the logical name of the file that you try to remove is not unique in the system catalog tables. For example, this issue occurs if the file existed in the database earlier, and then the file was removed.

When you try to remove a file that has the same logical name, SQL Server tries to remove the dropped logical file. This results in the error message.

## User action

To work around this issue, follow these steps.

> [!NOTE]
> These steps cause the file ID values to be reused.

1. Use the ALTER DATABASE statement to create a new logical file that has a different name and the same data type. For example, name the logical file as `different_remove_file_name` instead of `db_file1`, as in the following example:

    ```sql
    ALTER DATABASE [DBNAME] ADD FILE ( NAME = N'different_remove_file_name',
    FILENAME = N'D:\MSSQL.1\MSSQL\DATA\db_file1.ndf', SIZE = 1MB, MAXSIZE = 1MB)
    ```

    > [!NOTE]
    > You can use any file name or any file path.

1. Use the ALTER DATABASE statement to remove the logical file that you created in step 1, as in the following example:

    ```sql
    ALTER DATABASE [DBNAME] REMOVE FILE [different_remove_file_name]
    ```

1. Create a transaction log backup of the database.
1. Try to remove the logical file that is named *db_file1* again.
