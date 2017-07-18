---
title: "ALTER EXTERNAL LIBRARY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER EXTERNAL LIBRARY"
  - "ALTER_EXTERNAL_LIBRARY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER EXTERNAL LIBRARY"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# ALTER EXTERNAL LIBRARY (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]  

Add new content to an existing package library, or modifies existing library content.

New content can be added for a library only when adding content for a new platform. For example, if an existing package library named `ggplot2` contains packages for the WINDOWS platform, then the `ALTER EXTERNAL LIBRARY ADD` statement could be used to add content for the LINUX platform.

## Syntax

```
ALTER EXTERNAL LIBRARY library_name
[ AUTHORIZATION owner_name ]
{ SET | ADD } <file_spec>
WITH ( <library_option> [,…n] )
[ ; ]

<library_option> :: =  
  LANGUAGE = 'R' | 'Python'
| DATA_SOURCE = external_data_source_name  

<file_spec> ::=
{
(CONTENT = { <client_library_specifier> | <library_bits> | NONE}
[, PLATFORM = WINDOWS | LINUX])
}

<client_library_specifier> :: =
  '[\\computer_name\]share_name\[path\]manifest_file_name'
| '[local_path\]manifest_file_name'
| '<relative_path_in_external_data_source>'

<library_bits> :: =
{ varbinary_literal | varbinary_expression }
```

### Arguments

**library_name**

Specifies the name of an existing package library.

Libraries are scoped to the user. That is, library names are considered unique within the context of a specific user or owner.

**owner_name**

Specifies the name of the user or role that owns the external library.

Database owners can modify user libraries.

**file_spec**

Specifies the content of the package for a specific platform. Only one file artifact per platform will be supported.

The file can be specified in the form of a local path, network path, or path to a blob storage account, or as a binary blob. Use of Azure BLOB store URL will be supported only for SQL Database. If the data source option is specified, the file name can be a relative path with respect to the container referenced in the `EXTERNAL DATA SOURCE`.

Optionally, an OS platform for the file can be specified. Only one file artifact or content is permitted for each OS platform for a specific language or runtime.

**DATA_SOURCE = external_data_source_name**

Specifies the name of the external data source that contains the location of the library file. This location should reference an Azure blob storage path. To create an external data source, use [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](create-external-data-source-transact-sql.md).

**PLATFORM = WINDOWS | LINUX**

Specifies the platform for the content of the library. This value is required when modifying an existing library to add a different platform.

For example, assume two users **RUser1** and **RUser2** have individually uploaded the R library with the name `ggplot2`, using the zip file containing the Windows version binaries.

Both users could then call `ALTER EXTERNAL LIBRARY`, and upload the .gz file containing the Linux version binaries, to add the Linux platform version to their libraries.

### Return values

An informational message is returned if the statement was successful.  
- "Library installation successful."  

If the statement failed, these messages might be returned:  
- "File does not exist in the given path when creating external library."  
- "The DB user does not have permission to the folder containing the package."  
- "Extensibility not enabled."  

## Remarks

For the R language, packages must be prepared in the form of zipped archive files with the .ZIP extension for Windows, and .tar files with the .gz extension for Linux.

Instructions will be provided at a later date for package installation on Linux, or for Python libraries.

## Examples

The `ALTER EXTERNAL LIBRARY` DDL statement can be used to add new library content or modify existing library content.

New content can be added for a library only for a new platform. For example, if there is an existing package for `ggplot2` for the WINDOWS platform, the `ALTER EXTERNAL LIBRARY ADD` statement can be used only to add content for the LINUX platform.

## See also  
[CREATE EXTERNAL DATA SOURCE (Transact-SQL)](create-external-data-source-transact-sql.md)
[DROP EXTERNAL DATA SOURCE (Transact-SQL)](drop-external-data-source-transact-sql.md)  
[sys.external_library_files](../../relational-databases/system-catalog-views/sys-external-library-files.md)  
[sys.external_libraries](../../relational-databases/system-catalog-views/sys-external-libraries-transact-sql.md)  
