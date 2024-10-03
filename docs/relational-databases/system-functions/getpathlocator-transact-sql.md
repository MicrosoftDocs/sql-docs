---
title: "GetPathLocator (Transact-SQL)"
description: GetPathLocator returns the path locator ID value for the specified file or directory in a FileTable.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "GetPathLocator"
  - "GetPathLocator_TSQL"
helpviewer_keywords:
  - "GetPathLocator function"
dev_langs:
  - "TSQL"
---
# GetPathLocator (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the path locator ID value for the specified file or directory in a FileTable.

## Syntax

```syntaxsql
GetPathLocator ( filenamespace_path )
```

## Arguments

#### *filenamespace_path*

A namespace path in the FileTable. The namespace path is of type **nvarchar(max)**.

When the database belongs to an Always On availability group, then the `GetPathLocator` function accepts the virtual network name (VNN) or the computer name.

## Return types

**hierarchyid**

## Remarks

For more information, see [Work with directories and paths in FileTables](../blob/work-with-directories-and-paths-in-filetables.md).

## Examples

You can use the `GetPathLocator` function when you're migrating files from a file server to a FileTable. In this scenario, you move the files into the FileTable, and then replace the original UNC path for each file with the FileTable UNC path. For a complete example, see [Load Files into FileTables](../blob/load-files-into-filetables.md).

## Related content

- [Work with directories and paths in FileTables](../blob/work-with-directories-and-paths-in-filetables.md)
