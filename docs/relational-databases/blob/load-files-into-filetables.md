---
title: "Load files into FileTables"
description: Discover how to load and migrate files into FileTables in SQL Server when the files are stored in various ways. Read about bulk loading operations.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/08/2024
ms.service: sql
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords:
  - "FileTables [SQL Server], migrating files"
  - "FileTables [SQL Server], bulk loading"
  - "FileTables [SQL Server], loading files"
---
# Load files into FileTables

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Describes how to load or migrate files into FileTables.

## <a id="BasicsLoadNew"></a> Load or migrate files into a FileTable

The method that you choose for loading or migrating files into a FileTable depends on where the files are currently stored.

| Current location of files | Options for migration |
| --- | --- |
| Files are currently stored in the file system.<br /><br />[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has no knowledge of the files. | Since a FileTable appears as a folder in the Windows file system, you can easily load files into a new FileTable by using any of the available methods for moving or copying files. These methods include Windows Explorer, command-line options including **xcopy** and **robocopy**, and custom scripts or applications.<br /><br />You can't convert an existing folder to a FileTable. |
| Files are currently stored in the file system.<br /><br />[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] contains a table of metadata that contains pointers to the files. | The first step is to move or copy the files by using one of the preceding methods mentioned.<br /><br />The second step is to update the existing table of metadata to point to the new location of the files.<br /><br />For more information, see [Example: Migrate files from the file system into a FileTable](#HowToMigrateFiles) in this article. |

### <a id="HowToLoadNew"></a> How to: Load files into a FileTable

You can use the following methods to load files into a FileTable:

- Drag and drop files from the source folders to the new FileTable folder in Windows Explorer.

- Use command-line options such as `move`, `copy`, `xcopy`, or `robocopy` from the command prompt or in a batch file or script.

- Write a custom application to move or copy the files in [!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)] or [!INCLUDE [visual-basic-md](../../includes/visual-basic-md.md)] .NET. Call methods from the `System.IO` namespace.

### <a id="HowToMigrateFiles"></a> Example: Migrate files from the file system into a FileTable

In this scenario, your files are stored in the file system, and you have a table of metadata in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that contains pointers to the files. You want to move the files into a FileTable, and then replace the original UNC path for each file in the metadata with the FileTable UNC path. The [GetPathLocator](../system-functions/getpathlocator-transact-sql.md) function helps you to achieve this goal.

For this example, assume that there's an existing database table, `PhotoMetadata`, which contains data about photographs. This table has a column `UNCPath` of type **varchar(512)** which contains the actual UNC path to a `.jpg` file.

To migrate the image files from the file system into a FileTable, you have to do the following things:

1. Create a new FileTable to hold the files. This example uses the table name, `dbo.PhotoTable`, but doesn't show the code to create the table.

1. Use **xcopy** or a similar tool to copy the `.jpg` files, with their directory structure, into the root directory of the FileTable.

1. Fix the metadata in the `PhotoMetadata` table, by using code similar to the following example:

```sql
--  Add a path locator column to the PhotoMetadata table.
ALTER TABLE PhotoMetadata ADD pathlocator HIERARCHYID;

-- Get the root path of the Photo directory on the File Server.
DECLARE @UNCPathRoot VARCHAR(100) = '\\RemoteShare\Photographs';

-- Get the root path of the FileTable.
DECLARE @FileTableRoot VARCHAR(1000);

SELECT @FileTableRoot = FileTableRootPath('dbo.PhotoTable');

-- Update the PhotoMetadata table.
-- Replace the File Server UNC path with the FileTable path.
UPDATE PhotoMetadata
SET UNCPath = REPLACE(UNCPath, @UNCPathRoot, @FileTableRoot);

-- Update the pathlocator column to contain the pathlocator IDs from the FileTable.
UPDATE PhotoMetadata
SET pathlocator = GetPathLocator(UNCPath);
```

## <a id="BasicsBulkLoad"></a> Bulk load files into a FileTable

A FileTable behaves like a normal table for bulk operations. A FileTable has system-defined constraints that ensure that the integrity of the file and directory namespace is maintained. These constraints have to be verified on the data bulk loaded into the FileTable. Since some bulk insert operations allow table constraints to be ignored, the following requirements are enforced.

- Bulk loading operations that enforce constraints can be run against a FileTable as against any other table. This category includes the following operations:

  - **bcp** with `CHECK_CONSTRAINTS` clause.
  - `BULK INSERT` with `CHECK_CONSTRAINTS` clause.
  - `INSERT INTO ... SELECT * FROM OPENROWSET(BULK ...)` without `IGNORE_CONSTRAINTS` clause.

- Bulk loading operations that don't enforce constraints fail unless the FileTable system-defined constraints are disabled. This category includes the following operations:

  - **bcp** without `CHECK_CONSTRAINTS` clause.
  - `BULK INSERT` without `CHECK_CONSTRAINTS` clause.
  - `INSERT INTO ... SELECT * FROM OPENROWSET(BULK ...)` with `IGNORE_CONSTRAINTS` clause.

### <a id="HowToBulkLoad"></a> How to: Bulk load files into a FileTable

You can use various methods to bulk load files into a FileTable:

#### [bcp](#tab/bcp)

Call with the `CHECK_CONSTRAINTS` clause.

Disable the FileTable namespace and call without the `CHECK_CONSTRAINTS` clause. Then re-enable the FileTable namespace.

#### [BULK INSERT](#tab/bulk-insert)

Call with the `CHECK_CONSTRAINTS` clause.

Disable the FileTable namespace and call without the `CHECK_CONSTRAINTS` clause. Then re-enable the FileTable namespace.

#### [INSERT INTO ... SELECT * FROM OPENROWSET(BULK ...)](#tab/openrowset)

Call with the `IGNORE_CONSTRAINTS` clause.

Disable the FileTable namespace and call without the `IGNORE_CONSTRAINTS` clause. Then re-enable the FileTable namespace.

---

For information about disabling the FileTable constraints, see [Manage FileTables](manage-filetables.md).

### <a id="disabling"></a> How To: Disable FileTable constraints for bulk loading

To bulk load files into a FileTable without the overhead of enforcing the system-defined constraints, you can temporarily disable the constraints. For more information, see [Manage FileTables](manage-filetables.md).

## Related content

- [Access FileTables with Transact-SQL](access-filetables-with-transact-sql.md)
- [Access FileTables with File Input-Output APIs](access-filetables-with-file-input-output-apis.md)
