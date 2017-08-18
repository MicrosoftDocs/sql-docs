---
title: "ALTER EXTERNAL LIBRARY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/18/2017"
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

Modifies existing library content.  

## Syntax

```
ALTER EXTERNAL LIBRARY library_name
[ AUTHORIZATION owner_name ]
SET <file_spec>
WITH ( LANGUAGE = 'R' )
[ ; ]

<file_spec> ::=
{
(CONTENT = { <client_library_specifier> | <library_bits> | NONE}
[, PLATFORM = WINDOWS )
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

Specifies the name of an existing package library. Libraries are scoped to the user. That is, library names are considered unique within the context of a specific user or owner.

**owner_name**

Specifies the name of the user or role that owns the external library. Database owners can modify user libraries.

**file_spec**

Specifies the content of the package for a specific platform. Only one file artifact per platform will be supported.

The file can be specified in the form of a local path or network path. If the data source option is specified, the file name can be a relative path with respect to the container referenced in the `EXTERNAL DATA SOURCE`.

Optionally, an OS platform for the file can be specified. Only one file artifact or content is permitted for each OS platform for a specific language or runtime.

**DATA_SOURCE = external_data_source_name**

Specifies the name of the external data source that contains the location of the library file. This location should reference an Azure blob storage path. To create an external data source, use [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](create-external-data-source-transact-sql.md).

**PLATFORM = WINDOWS**

Specifies the platform for the content of the library. This value is required when modifying an existing library to add a different platform. Windows is the only supported platform.

## Remarks

For the R language, packages must be prepared in the form of zipped archive files with the .ZIP extension for Windows. Currently, only the Windows platform is supported.  
The `ALTER EXTERNAL LIBRARY` statement only uploads the library bits to the database. The modified library is not actually installed until a user runs an external script afterwards, by executing [sp_execute_external_script (Transact-SQL)](../../(relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

## Permissions
Requires the `ALTER ANY EXTERNAL LIBRARY` permission.

## Examples

### A. Add an external library to a database

The following example modifies an external library called customPackage.

```sql  
ALTER EXTERNAL LIBRARY customPackage 
SET 
  (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\customPackage.zip')
WITH (LANGUAGE = 'R');
```  
Then execute the `sp_execute_external_script` procedure, to install the library.

```sql   
EXEC sp_execute_external_script 
@language =N'R', 
@script=N'
# load customPackage
library(customPackage)

# call customPackageFunc
OutputDataSet <- customPackageFunc()
'
with result sets (([result] int));    
```


## See also  
[CREATE EXTERNAL LIBRARY (Transact-SQL)](create-external-library-transact-sql.md)
[DROP EXTERNAL LIBRARY (Transact-SQL)](drop-external-library-transact-sql.md)  
[sys.external_library_files](../../relational-databases/system-catalog-views/sys-external-library-files-transact-sql.md)  
[sys.external_libraries](../../relational-databases/system-catalog-views/sys-external-libraries-transact-sql.md)  
