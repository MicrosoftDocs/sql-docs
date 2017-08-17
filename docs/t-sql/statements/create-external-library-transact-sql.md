---
title: "CREATE EXTERNAL LIBRARY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/17/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE EXTERNAL LIBRARY"
  - "CREATE_EXTERNAL_LIBRARY_TSQL"
  - "EXTERNAL LIBRARY"
  - "EXTERNAL_LIBRARY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CREATE EXTERNAL LIBRARY"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# CREATE EXTERNAL LIBRARY (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]  

Uploads R packages to a database from the specified byte stream or file path.

This statement serves as a generic mechanism for the database administrator to upload artifacts needed for any new external language runtimes (R, Python, Java, etc.) and OS platforms supported by [!INCLUDE[ssnoversion](../../includes/ssnoversion.md)]. Currently only the R language and Windows platform are supported.

## Syntax

```
CREATE EXTERNAL LIBRARY library_name  
    [ AUTHORIZATION owner_name ]  
FROM <file_spec> [,…2]  
WITH ( LANGUAGE = 'R' )  
[ ; ]  

<file_spec> ::=  
{  
(CONTENT = { <client_library_specifier> | <library_bits> }  
[, PLATFORM = WINDOWS ])  
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

Libraries are added to the database scoped to the user. That is, library names are considered unique within the context of a specific user or owner.

For example, two users **RUser1** and **RUser2** can both individually and separately upload the R library `ggplot2`. The zip file for this R package contains the Windows version binaries, and the .gz file contains the Linux version binaries. Both users could upload the Windows version, and then both users could also add the Linux platform version to their libraries by calling `ALTER EXTERNAL LIBRARY`.

However, neither user could subsequently upload a Python library called "ggplot2". In short, library names must be unique per user. The library name need not be aligned with the source package name, and is purely for SQL management purposes.

**owner_name**

Specifies the name of the user or role that owns the external library. If not specified, ownership is given to the current user.

The libraries owned by database owner are considered global to the database and runtime. In other words, database owners can create libraries that contain a common set of libraries or packages that are shared by many users. When an external library is created by a user other than the `dbo` user, the external library is private to that user only.   

When the user **RUser1** executes an R script, the value of `libPath` can contain multiple paths. The first path is always the path to the shared library created by the database owner. The second part of `libPath` specifies the path containing packages uploaded individually by **RUser1**.

**file_spec**

Specifies the content of the package for a specific platform. Only one file artifact per platform is supported. 

The file can be specified in the form of a local path, or network path.

Optionally, an OS platform for the file can be specified. Only one file artifact or content is permitted for each OS platform for a specific language or runtime.

**PLATFORM = WINDOWS**

Specifies the platform for the content of the library. The value defaults to the host platform on which SQL Server is running. Therefore, the user doesn’t have to specify the value. It is required in case where multiple platforms are supported, or the user needs to specify a different platform. Windows is the only supported platform.

### Return values

An informational message is returned if the statement was successful.  
- "Library installation successful"  

## Remarks

For the R language, packages must be prepared in the form of zipped archive files with the .ZIP extension for Windows. Currently, only the Windows platform is supported.  

The `CREATE EXTERNAL LIBRARY` statement only uploads the library bits to the database. The library is not actually installed until a user runs an external script afterwards, by executing [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).  

## Permissions  
Requires the `CREATE EXTERNAL LIBRARY` permission.  

## Examples

### A. Add an external library to a database  
The following example adds an external library called customPackage to a database.   
```sql
CREATE EXTERNAL LIBRARY customPackage 
FROM 
    (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\customPackage.zip')
WITH (LANGUAGE = 'R');
```

### B. Add ggplot2 to a database

The database administrator needs to install R packages on a specific database, and make them available to all users of the database. To install the packages, the DBA must log in to [!INCLUDE[ssnoversion](../../includes/ssnoversion.md)] using the database owner role and run the following statement to generate the package library:

```sql
CREATE EXTERNAL LIBRARY ggplot2 
FROM 
    (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\ggplot2.zip') 
WITH (LANGUAGE = 'R'); 
```

For this to work, the folder where the packages are saved must be accessible to the server. 

### Change an existing package library

The `ALTER EXTERNAL LIBRARY` DDL statement can be used to add new library content or modify existing library content.   

### Delete a package library

To delete a package library from the database, run the statement:

```sql
DROP EXTERNAL LIBRARY ggplot2 <user_name>;
```

> [!NOTE]
> Unlike other `DROP` statements in [!INCLUDE[ssnoversion](../../includes/ssnoversion.md)], this statement supports an optional parameter that specifies the user authority. This option allows users with ownership roles to delete libraries uploaded by regular users. 

## See also  
[ALTER EXTERNAL LIBRARY (Transact-SQL)](alter-external-library-transact-sql.md)  
[DROP EXTERNAL LIBRARY (Transact-SQL)](drop-external-library-transact-sql.md)  
[sys.external_library_files](../../relational-databases/system-catalog-views/sys-external-library-files-transact-sql.md)  
[sys.external_libraries](../../relational-databases/system-catalog-views/sys-external-libraries-transact-sql.md)  
