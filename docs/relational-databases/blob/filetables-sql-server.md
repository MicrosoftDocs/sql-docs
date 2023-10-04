---
title: "FileTables (SQL Server)"
description: Explore the benefits and functionality of FileTables, the SQL Server feature that uses a directory structure to store files. Learn how to work with FileTables.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/02/2023
ms.service: sql
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords:
  - "FileTables [SQL Server], overview"
  - "FileTables [SQL Server]"
  - "FileTable [SQL Server], see FileTables [SQL Server]"
  - "FileTable [SQL Server]"
---
# FileTables (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The FileTable feature brings support for the Windows file namespace and compatibility with Windows applications to the file data stored in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. FileTable lets an application integrate its storage and data management components, and provides integrated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services - including full-text search and semantic search - over unstructured data and metadata.

In other words, you can store files and documents in special tables in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] called FileTables, but access them from Windows applications as if they were stored in the file system, without making any changes to your client applications.

The FileTable feature builds on top of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] FILESTREAM technology. To learn more about FILESTREAM, see [FILESTREAM (SQL Server)](../../relational-databases/blob/filestream-sql-server.md).

## <a id="Goals"></a> Benefits of the FileTable feature

The goals of the FileTable feature include the following:

- Windows API compatibility for file data stored within a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. Windows API compatibility includes the following:

  - Non-transactional streaming access and in-place updates to FILESTREAM data.

  - A hierarchical namespace of directories and files.

  - Storage of file attributes, such as created date and modified date.

  - Support for Windows file and directory management APIs.

- Compatibility with other [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] features including management tools, services, and relational query capabilities over FILESTREAM and file attribute data.

Thus FileTables remove a significant barrier to the use of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] for the storage and management of unstructured data that is currently residing as files on file servers. Enterprises can move this data from file servers into FileTables to take advantage of integrated administration and services provided by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. At the same time, they can maintain Windows application compatibility for their existing Windows applications that see this data as files in the file system.

## <a id="Description"></a> What is a FileTable?

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides a special *table of files*, also referred to as a *FileTable*, for applications that require file and directory storage in the database, with Windows API compatibility and non-transactional access. A FileTable is a specialized user table with a predefined schema that stores FILESTREAM data, as well as file and directory hierarchy information and file attributes.

A FileTable provides the following functionality:

- A FileTable represents a hierarchy of directories and files. It stores data related to all the nodes in that hierarchy, for both directories and the files they contain. This hierarchy starts from a root directory that you specify when you create the FileTable.

- Every row in a FileTable represents a file or a directory.

- Every row contains the following items. For more information about the schema of a FileTable, see [FileTable Schema](../../relational-databases/blob/filetable-schema.md).

  - A `file_stream` column for stream data and a `stream_id` (GUID) identifier. (The `file_stream` column is NULL for a directory.)

  - Both `path_locator` and `parent_path_locator` columns for representing and maintaining the current item (file or directory) and directory hierarchy.

  - 10 file attributes such as created date and modified date that are useful with file I/O APIs.

  - A type column that supports full-text search and semantic search over files and documents.

- A FileTable enforces certain system-defined constraints and triggers to maintain file namespace semantics.

- When the database is configured for non-transactional access, the file and directory hierarchy represented in the FileTable is exposed under the FILESTREAM share configured for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. This provides file system access for Windows applications.

### Some additional characteristics of FileTables

- The file and directory data stored in a FileTable is exposed through a Windows share for non-transactional file access for Windows API based applications. For a Windows application, this looks like a normal share with its files and directories. Applications can use a rich set of Windows APIs to manage the files and directories under this share.

- The directory hierarchy surfaced through the share is a purely logical directory structure that is maintained within the FileTable.

- Calls to create or change a file or directory through the Windows share are intercepted by a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] component and reflected in the corresponding relational data in the FileTable.

- Windows API operations are non-transactional in nature, and aren't associated with user transactions. However, transactional access to FILESTREAM data stored in a FileTable is fully supported, as is the case for any FILESTREAM column in a regular table. If you need to modify files frequently from multiple connections and ensure proper file protection, use transactional FILESTREAM access via [OpenSqlFilestream()](../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md), rather than exclusive file locks at the Windows API level.

- FileTables can also be queried and updated through normal [!INCLUDE [tsql](../../includes/tsql-md.md)] access. They are also integrated with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] management tools, and features such as backup.

- You can't send an email request through Database Mail and attach a file located in a FILESTREAM directory (and therefore FileTable). The filesystem filter driver RsFx0420 inspects incoming I/O requests going in and out of the FILESTREAM folder. If the request isn't both from the SQLServer executable and FILESTREAM code, they are explicitly disallowed.

## <a id="additional"></a> Additional considerations for using FileTables

### <a id="DBA"></a> Administrative considerations

#### About FILESTREAM and FileTables

You configure FileTables separately from FILESTREAM. Therefore you can continue to use the FILESTREAM feature without enabling non-transactional access or creating FileTables.

There is no non-transactional access to FILESTREAM data except through FileTables. Therefore, when you enable non-transactional access, the behavior of existing FILESTREAM columns and applications isn't affected.

#### About FileTables and non-transactional access

You can enable or disable non-transactional access at the database level.

You can configure or fine-tune non-transactional access at the database level by turning it off, or by enabling read only or full read/write access.

### <a id="memory"></a> FileTables don't support memory-mapped files

FileTables don't support memory-mapped files. Notepad and Paint are two common examples of applications that use memory-mapped files. You can't use these applications on the same computer as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to open files that are stored in a FileTable. However you can use these applications from a remote computer to open files that are stored in a FileTable, because in these circumstances the memory-mapping feature isn't used.

## Related tasks

- [Enable the prerequisites for FileTable](enable-the-prerequisites-for-filetable.md)
- [Create, Alter, and Drop FileTables](create-alter-and-drop-filetables.md)
- [Load Files into FileTables](load-files-into-filetables.md)
- [Work with Directories and Paths in FileTables](work-with-directories-and-paths-in-filetables.md)
- [Access FileTables with Transact-SQL](access-filetables-with-transact-sql.md)
- [Access FileTables with File Input-Output APIs](access-filetables-with-file-input-output-apis.md)
- [Manage FileTables](manage-filetables.md)

## Related content

- [FileTable Schema](filetable-schema.md)
- [FileTable compatibility with other SQL Server features](filetable-compatibility-with-other-sql-server-features.md)
- [FileTable DDL, functions, stored procedures, and views](filetable-ddl-functions-stored-procedures-and-views.md)
- [FILESTREAM and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
- [FILESTREAM and FileTable Catalog Views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
- [FILESTREAM and FileTable system stored procedures (Transact-SQL)](../system-stored-procedures/filestream-and-filetable-system-stored-procedures.md)
