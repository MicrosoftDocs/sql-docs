---
title: "CREATE EXTERNAL LIBRARY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 03/27/2018
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
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
author: dphansen
ms.author: davidph
manager: cgronlund
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE EXTERNAL LIBRARY (Transact-SQL)  

[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]  

Uploads R, Python, or Java package files to a database from the specified byte stream or file path. This statement serves as a generic mechanism for the database administrator to upload artifacts needed for any new external language runtimes and OS platforms supported by [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. 

> [!NOTE]
> In SQL Server 2017, R language and Windows platform are supported. R, Python, and Java on the Windows and Linux platforms are supported in SQL Server 2019 CTP 2.4.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
## Syntax for SQL Server 2019

```text
CREATE EXTERNAL LIBRARY library_name  
[ AUTHORIZATION owner_name ]  
FROM <file_spec> [ ,...2 ]  
WITH ( LANGUAGE = <language> )  
[ ; ]  

<file_spec> ::=  
{  
    (CONTENT = { <client_library_specifier> | <library_bits> }  
    [, PLATFORM = <platform> ])  
}  

<client_library_specifier> :: = 
{
      '[\\computer_name\]share_name\[path\]manifest_file_name'  
    | '[local_path\]manifest_file_name'  
    | '<relative_path_in_external_data_source>'  
} 

<library_bits> :: =  
{ 
      varbinary_literal 
    | varbinary_expression 
}

<platform> :: = 
{
      WINDOWS
    | LINUX
}

<language> :: = 
{
      'R'
    | 'Python'
    | 'Java'
}

```
::: moniker-end
::: moniker range=">=sql-server-2017 <=sql-server-2017||=sqlallproducts-allversions"
## Syntax for SQL Server 2017

```text
CREATE EXTERNAL LIBRARY library_name  
[ AUTHORIZATION owner_name ]  
FROM <file_spec> [ ,...2 ]  
WITH ( LANGUAGE = 'R' )  
[ ; ]  

<file_spec> ::=  
{  
    (CONTENT = { <client_library_specifier> | <library_bits> }  
    [, PLATFORM = WINDOWS ])  
}  

<client_library_specifier> :: = 
{
      '[\\computer_name\]share_name\[path\]manifest_file_name'  
    | '[local_path\]manifest_file_name'  
    | '<relative_path_in_external_data_source>'  
} 

<library_bits> :: =  
{ 
      varbinary_literal 
    | varbinary_expression 
}
```
::: moniker-end

### Arguments

**library_name**

Libraries are added to the database scoped to the user. Library names must be unique within the context of a specific user or owner. For example, two users **RUser1** and **RUser2** can both individually and separately upload the R library `ggplot2`. However, if **RUser1** wanted to upload a newer version of `ggplot2`, the second instance must be named differently or must replace the existing library. 

Library names cannot be arbitrarily assigned; the library name should be the same as the name required to load the library in the external script.

**owner_name**

Specifies the name of the user or role that owns the external library. If not specified, ownership is given to the current user.

The libraries owned by database owner are considered global to the database and runtime. In other words, database owners can create libraries that contain a common set of libraries or packages that are shared by many users. When an external library is created by a user other than the `dbo` user, the external library is private to that user only.

When the user **RUser1** executes an external script, the value of `libPath` can contain multiple paths. The first path is always the path to the shared library created by the database owner. The second part of `libPath` specifies the path containing packages uploaded individually by **RUser1**.

**file_spec**

Specifies the content of the package for a specific platform. Only one file artifact per platform is supported.

The file can be specified in the form of a local path, or network path.

Optionally, an OS platform for the file can be specified. Only one file artifact or content is permitted for each OS platform for a specific language or runtime.

**library_bits**

Specifies the content of the package as a hex literal, similar to assemblies. 

This option is useful if you need to create a library or alter an existing library (and have the required permissions to do so), but the file system on the server is restricted and you cannot copy the library files to a location that the server can access.

::: moniker range=">=sql-server-2017 <=sql-server-2017||=sqlallproducts-allversions"
**PLATFORM = WINDOWS**

Specifies the platform for the content of the library. The value defaults to the host platform on which SQL Server is running. Therefore, the user doesn't have to specify the value. It is required in case where multiple platforms are supported, or the user needs to specify a different platform. 

In SQL Server 2017, Windows is the only supported platform.
::: moniker-end
::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
**PLATFORM**

Specifies the platform for the content of the library. The value defaults to the host platform on which SQL Server is running. Therefore, the user doesn't have to specify the value. It is required in case where multiple platforms are supported, or the user needs to specify a different platform.

In SQL Server 2019, Windows and Linux are the supported platforms.

**language**

Specifies the language of the package. The value can be `R`, `Python`, or `Java`.
::: moniker-end

## Remarks

::: moniker range=">=sql-server-2017 <=sql-server-2017||=sqlallproducts-allversions"
For the R language, when using a file, packages must be prepared in the form of zipped archive files with the .ZIP extension for Windows. In SQL Server 2017, only the Windows platform is supported. 
::: moniker-end
::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
For the R language, when using a file, packages must be prepared in the form of zipped archive files with the .ZIP extension.  

For the Python language, the package in a .whl or .zip file must be prepared in the form of a zipped archive file. If the package already is a .zip file, it must be included in a new .zip file. Uploading a package as .whl or .zip file directly is currently not supported.
::: moniker-end

The `CREATE EXTERNAL LIBRARY` statement uploads the library bits to the database. The library is installed when a user runs an external script using [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) and calls the package or library.

Libraries uploaded to the instance can be either public or private. If the library is created by a member of `dbo`, the library is public and can be shared with all users. Otherwise, the library is private to that user only.

## Permissions

Requires the `CREATE EXTERNAL LIBRARY` permission. By default, any user who has **dbo** who is a member of the **db_owner** role has permissions to create an external library. For all other users, you must explicitly give them permission using a [GRANT](https://docs.microsoft.com/sql/t-sql/statements/grant-database-permissions-transact-sql) statement, specifying CREATE EXTERNAL LIBRARY as the privilege.

To modify a library requires the separate permission, `ALTER ANY EXTERNAL LIBRARY`.

To create an external library by using a file path, the user must be a Windows authenticated login or a member of the sysadmin fixed server role.

## Examples

### A. Add an external library to a database  

The following example adds an external library called `customPackage` to a database.

```sql
CREATE EXTERNAL LIBRARY customPackage
FROM (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\customPackage.zip') WITH (LANGUAGE = 'R');
```

After the library has been successfully uploaded to the instance, a user executes the `sp_execute_external_script` procedure, to install the library.

```sql
EXEC sp_execute_external_script 
@language =N'R', 
@script=N'library(customPackage)'
```

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
For the Python language in SQL Server 2019, the example also works by replacing `'R'` with `'Python'`.
::: moniker-end

### B. Installing packages with dependencies

If the package you want to install has any dependencies, it is critical that you analyze both first-level and second-level dependencies, and ensure that all required packages are available _before_ you try to install the target package.

For example, assume you want to install a new package, `packageA`:

+ `packageA` has a dependency on `packageB`
+ `packageB` has a dependency on `packageC`

To succeed in installing `packageA`, you must create libraries for `packageB` and `packageC` at the same time that you add `packageA` to SQL Server. Be sure to check the required package versions as well.

In practice, package dependencies for popular packages are usually much more complicated than this simple example. For example, **ggplot2** might require over 30 packages, and those packages might require additional packages that are not available on the server. Any missing package or wrong package version can cause installation to fail.

Because it can be difficult to determine all dependencies just from looking at the  package manifest, we recommend that you use a package such as [miniCRAN](https://cran.r-project.org/web/packages/miniCRAN/index.html) to identify all packages that might be required to complete installation successfully.

+ Upload the target package and its dependencies. All files must be in a folder that is accessible to the server.

    ```sql
    CREATE EXTERNAL LIBRARY packageA 
    FROM (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\packageA.zip') 
    WITH (LANGUAGE = 'R'); 
    GO

    CREATE EXTERNAL LIBRARY packageB FROM (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\packageB.zip') 
    WITH (LANGUAGE = 'R');
    GO

    CREATE EXTERNAL LIBRARY packageC FROM (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\packageC.zip') 
    WITH (LANGUAGE = 'R');
    GO
    ```

+ Install the required packages first.

    If a required package has already been uploaded to the instance, you need not add it again. Just be sure to check whether the existing package is the correct version. 
    
    The required packages `packageC` and `packageB` are installed, in the correct order, when `sp_execute_external_script` is first run to install package `packageA`.

    However, if any required package is not available, installation of the target package `packageA` fails.

    ```sql
    EXEC sp_execute_external_script 
    @language =N'R', 
    @script=N'
    # load the desired package packageA
    library(packageA)
    '
    ```

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
For the Python language in SQL Server 2019, the example also works by replacing `'R'` with `'Python'`.
::: moniker-end

### C. Create a library from a byte stream

If you do not have the ability to save the package files in a location on the server, you can  pass the package contents in a variable. The following example creates a library by passing the bits as a hexadecimal literal.

```SQL
CREATE EXTERNAL LIBRARY customLibrary FROM (CONTENT = 0xabc123) WITH (LANGUAGE = 'R');
```

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
For the Python language in SQL Server 2019, the example also works by replacing **'R'** with **'Python'**.
::: moniker-end

> [!NOTE]
> This code sample only demonstrates the syntax; the binary value in `CONTENT =` has been truncated for readability and does not create a working library. The actual contents of the binary variable would be much longer.

### D. Change an existing package library

The `ALTER EXTERNAL LIBRARY` DDL statement can be used to add new library content or modify existing library content. To modify an existing library requires the `ALTER ANY EXTERNAL LIBRARY` permission.

For more information, see [ALTER EXTERNAL LIBRARY](alter-external-library-transact-sql.md).

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
### E. Add a Java .jar file to a database  

The following example adds an external jar file called `customJar` to a database.

```sql
CREATE EXTERNAL LIBRARY customJar
FROM (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\customJar.jar') 
WITH (LANGUAGE = 'Java');
```

After the library has been successfully uploaded to the instance, a user executes the `sp_execute_external_script` procedure, to install the library.

```sql
EXEC sp_execute_external_script
    @language = N'Java'
    , @script = N'customJar.MyCLass.myMethod'
    , @input_data_1 = N'SELECT * FROM dbo.MyTable'
WITH RESULT SETS ((column1 int))
```

### F. Add an external package for both Windows and Linux

You can specify up to two `<file_spec>`, one for Windows and one for Linux.

```sql
CREATE EXTERNAL LIBRARY lazyeval 
FROM (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\packageA.zip', PLATFORM = WINDOWS),
(CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\packageA.tar.gz', PLATFORM = LINUX)
WITH (LANGUAGE = 'R')
```

When you use `sp_execute_external_script` to install the package, depending on the platform the SQL Server instance is running on, the library content for that platform will be used.

```sql
EXECUTE sp_execute_external_script 
    @LANGUAGE = N'R',
    @SCRIPT = N'
library(packageA)
```
::: moniker-end

## See also

[ALTER EXTERNAL LIBRARY (Transact-SQL)](alter-external-library-transact-sql.md)  
[DROP EXTERNAL LIBRARY (Transact-SQL)](drop-external-library-transact-sql.md)  
[sys.external_library_files](../../relational-databases/system-catalog-views/sys-external-library-files-transact-sql.md)  
[sys.external_libraries](../../relational-databases/system-catalog-views/sys-external-libraries-transact-sql.md)  
