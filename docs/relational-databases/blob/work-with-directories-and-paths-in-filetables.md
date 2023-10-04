---
title: "Work with directories and paths in FileTables"
description: The FileTables feature uses a directory structure to store files. Learn how to work with its directories, paths, restrictions, and semantics.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/02/2023
ms.service: sql
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords:
  - "FileTables [SQL Server], directories"
---
# Work with directories and paths in FileTables

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Describes the directory structure in which the files are stored in FileTables.

## <a id="HowToDirectories"></a> How To: Work with Directories and Paths in FileTables

You can use the following three functions to work with FileTable directories in [!INCLUDE [tsql](../../includes/tsql-md.md)]:

| To get this result | Use this function |
| --- | --- |
| Get the root-level UNC path for a specific FileTable or for the current database. | [FileTableRootPath (Transact-SQL)](../../relational-databases/system-functions/filetablerootpath-transact-sql.md) |
| Get an absolute or relative UNC path for a file or directory in a FileTable. | [GetFileNamespacePath (Transact-SQL)](../../relational-databases/system-functions/getfilenamespacepath-transact-sql.md) |
| Get the path locator ID value for the specified file or directory in a FileTable, by providing the path. | [GetPathLocator (Transact-SQL)](../../relational-databases/system-functions/getpathlocator-transact-sql.md) |

## <a id="BestPracticeRelativePaths"></a> Use relative paths for portable code

To keep code and applications independent of the current computer and database, avoid writing code that relies on absolute file paths. Instead, get the complete path for a file at run time by using the [FileTableRootPath (Transact-SQL)](../../relational-databases/system-functions/filetablerootpath-transact-sql.md) and [GetFileNamespacePath (Transact-SQL)](../../relational-databases/system-functions/getfilenamespacepath-transact-sql.md)) functions together, as shown in the following example. By default, the `GetFileNamespacePath` function returns the relative path of the file under the root path for the database.

```sql
USE database_name;

DECLARE @root NVARCHAR(100);
DECLARE @fullpath NVARCHAR(1000);

SELECT @root = FileTableRootPath();

SELECT @fullpath = @root + file_stream.GetFileNamespacePath()
FROM filetable_name
WHERE name = N'document_name';

PRINT @fullpath;
GO
```

## <a id="restrictions"></a> Limitations

### <a id="nesting"></a> Nest level

> [!IMPORTANT]  
> You cannot store more than 15 levels of subdirectories in the FileTable directory. When you store 15 levels of subdirectories, then the lowest level cannot contain files, since these files would represent an additional level.

### <a id="fqnlength"></a> Length of full path name

> [!IMPORTANT]  
> The NTFS file system supports path names that are much longer than the 260-character limit of the Windows shell and most Windows APIs. Therefore it is possible to create files in the file hierarchy of a FileTable by using Transact-SQL that you cannot view or open with Windows Explorer or many other Windows applications, because the full path name exceeds 260 characters. However you can continue to access these files by using Transact-SQL.

## <a id="fullpath"></a> The full path to an item stored in a FileTable

The full path to a file or directory stored in a FileTable begins with the following elements:

1. The share enabled for FILESTREAM file I/O access at the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance level.

1. The `DIRECTORY_NAME` specified at the database level.

1. The `FILETABLE_DIRECTORY` specified at the FileTable level.

The resulting hierarchy looks like this:

`\\<machine>\<instance-level FILESTREAM share>\<database-level directory>\<FileTable directory>\`

This directory hierarchy forms the root of the FileTable's file namespace. Under this directory hierarchy, the FILESTREAM data for the FileTable is stored as files, and as subdirectories that can also contain files and subdirectories.

It is important to keep in mind that the directory hierarchy created under the instance-level FILESTREAM share is a virtual directory hierarchy. This hierarchy is stored in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database and isn't represented physically in the NTFS file system. All operations that access files and directories under the FILESTREAM share and in the FileTables that it contains are intercepted and handled by a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] component embedded in the file system.

## <a id="roots"></a> The semantics of the root directories at the instance, database, and FileTable levels

This directory hierarchy observes the following semantics:

- The instance-level FILESTREAM share is configured by an administrator and stored as a property of the server. You can rename this share by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. A renaming operation doesn't take effect until the server is restarted.

- The database-level `DIRECTORY_NAME` is null by default when you create a new database. An administrator can set or change this name by using the `ALTER DATABASE` statement. The name must be unique (in a case-insensitive comparison) in that instance.

- You typically provide the `FILETABLE_DIRECTORY` name as part of the `CREATE TABLE` statement when you create a FileTable. You can change this name by using the `ALTER TABLE` command.

- You can't rename these root directories through file I/O operations.

- You can't open these root directories with exclusive file handles.

## <a id="is_directory"></a> The is_directory column in the FileTable schema

The following table describes the interaction between the `is_directory` column and the `file_stream` column that contains the FILESTREAM data in a FileTable.

| is_directory value | file_stream value | Behavior |
| --- | --- | --- |
| `FALSE` | `NULL` | This is an invalid combination that is caught by a system-defined constraint. |
| `FALSE` | `<value>` | The item represents a file. |
| `TRUE` | `NULL` | The item represents a directory. |
| `TRUE` | `<value>` | This is an invalid combination that is caught by a system-defined constraint. |

## <a id="alwayson"></a> Use Virtual Network Names (VNNs) with Always On availability groups

When the database that contains FILESTREAM or FileTable data belongs to an availability group:

- The FILESTREAM and FileTable functions accept or return virtual network names (VNNs) instead of computer names. For more information about these functions, see [FILESTREAM and FileTable Functions (Transact-SQL)](../../relational-databases/system-functions/filestream-and-filetable-functions-transact-sql.md).

- All access to FILESTREAM or FileTable data through the file system APIs should use VNNs instead of computer names. For more information, see [FILESTREAM and FileTable with Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/filestream-and-filetable-with-always-on-availability-groups-sql-server.md).

## Related content

- [Enable the prerequisites for FileTable](enable-the-prerequisites-for-filetable.md)
- [Create, Alter, and Drop FileTables](create-alter-and-drop-filetables.md)
- [Access FileTables with Transact-SQL](access-filetables-with-transact-sql.md)
- [Access FileTables with File Input-Output APIs](access-filetables-with-file-input-output-apis.md)
