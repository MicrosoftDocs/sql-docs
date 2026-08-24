---
title: "ALTER EXTERNAL LIBRARY (Transact-SQL)"
description: ALTER EXTERNAL LIBRARY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 01/27/2026
ms.service: sql
ms.subservice: machine-learning
ms.topic: reference
f1_keywords:
  - "ALTER EXTERNAL LIBRARY"
  - "ALTER_EXTERNAL_LIBRARY_TSQL"
helpviewer_keywords:
  - "ALTER EXTERNAL LIBRARY"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-ver15 || =azuresqldb-mi-current"
---
# ALTER EXTERNAL LIBRARY (Transact-SQL)

[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

Modifies the content of an existing external package library.

::: moniker range=">=sql-server-2017 || =sql-server-linux-ver15"
> [!NOTE]  
> In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], R language and Windows platform are supported. R, Python, and external languages on the Windows and Linux platforms are supported in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.
::: moniker-end

::: moniker range=">=sql-server-ver15 || =sql-server-linux-ver15"

## Syntax for SQL Server 2019

```syntaxsql
ALTER EXTERNAL LIBRARY library_name
[ AUTHORIZATION owner_name ]
SET <file_spec>
WITH ( LANGUAGE = <language> )
[ ; ]

<file_spec> ::=
{
    (CONTENT = { <client_library_specifier> | <library_bits> | NONE}
    [, PLATFORM = <platform> )
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
    | <external_language>
}
```

::: moniker-end

::: moniker range="=sql-server-2017"

## Syntax for SQL Server 2017

```syntaxsql
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

::: moniker range="=azuresqldb-mi-current"

## Syntax for Azure SQL Managed Instance

```syntaxsql
CREATE EXTERNAL LIBRARY library_name
[ AUTHORIZATION owner_name ]
FROM <file_spec> [ ,...2 ]
WITH ( LANGUAGE = <language> )
[ ; ]

<file_spec> ::=
{
    (CONTENT = <library_bits>)
}

<library_bits> :: =
{
      varbinary_literal
    | varbinary_expression
}

<language> :: =
{
      'R'
    | 'Python'
}
```

::: moniker-end

### Arguments

#### LIBRARY_NAME

Specifies the name of an existing package library. Libraries are scoped to the user. Library names must be unique within the context of a specific user or owner.

The library name can't be arbitrarily assigned. That is, you must use the name that the calling runtime expects when it loads the package.

#### OWNER_NAME

Specifies the name of the user or role that owns the external library.

::: moniker range=">=sql-server-2017 || =sql-server-linux-ver15"

#### FILE_SPEC

Specifies the content of the package for a specific platform. Only one file artifact per platform is supported.

The file can be specified in the form of a local path or network path. If the data source option is specified, the file name can be a relative path with respect to the container referenced in the `EXTERNAL DATA SOURCE`.

Optionally, an OS platform for the file can be specified. Only one file artifact or content is permitted for each OS platform for a specific language or runtime.
::: moniker-end

#### LIBRARY_BITS

Specifies the content of the package as a hex literal, similar to assemblies.

This option is useful if you have the required permission to alter a library, but file access on the server is restricted and you can't save the contents to a path the server can access.

Instead, you can pass the package contents as a variable in binary format.

::: moniker range="=sql-server-2017"

#### PLATFORM = WINDOWS

Specifies the platform for the content of the library. This value is required when modifying an existing library to add a different platform.
In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], Windows is the only supported platform.
::: moniker-end

::: moniker range=">=sql-server-ver15 || =sql-server-linux-ver15"

#### PLATFORM

Specifies the platform for the content of the library. This value is required when modifying an existing library to add a different platform.
In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], Windows and Linux are the supported platforms.
::: moniker-end

::: moniker range="=sql-server-2017"

#### LANGUAGE = 'R'

Specifies the language of the package. R is supported in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].
::: moniker-end

::: moniker range="=azuresqldb-mi-current"

#### LANGUAGE

Specifies the language of the package. The value can be `R` or `Python` in Azure SQL Managed Instance.
::: moniker-end

::: moniker range=">=sql-server-ver15 || =sql-server-linux-ver15"

#### LANGUAGE

Specifies the language of the package. The value can be `R`, `Python`, or the name of an external language (see [CREATE EXTERNAL LANGUAGE](create-external-language-transact-sql.md)).
::: moniker-end

## Remarks

::: moniker range="=sql-server-2017"
For the R language, packages must be prepared in the form of zipped archive files with the `.zip` extension for Windows. In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], only the Windows platform is supported.  
::: moniker-end

::: moniker range=">=sql-server-ver15 || =sql-server-linux-ver15"
For the R language, when using a file, packages must be prepared in the form of zipped archive files with the `.zip` extension.

For the Python language, the package in a `.whl` or `.zip` file must be prepared in the form of a zipped archive file. If the package already is a `.zip` file, it must be included in a new `.zip` file. Uploading a package as `.whl` or `.zip` file directly isn't currently supported.
::: moniker-end

The `ALTER EXTERNAL LIBRARY` statement only uploads the library bits to the database. The modified library is installed when a user runs code in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) that calls the library.

Several packages, referred to as *system packages*, are preinstalled in a SQL instance. You can't add, update, or remove system packages.

## Permissions

By default, the **dbo** user or any member of the role **db_owner** has permission to run `ALTER EXTERNAL LIBRARY`. Additionally, the user who created the external library can alter that external library.

## Examples

The following examples change an external library called `customPackage`.

::: moniker range=">=sql-server-2017 || =sql-server-linux-ver15"

### Replace the contents of a library using a file

The following example modifies an external library called `customPackage`, using a zipped file containing the updated bits.

```sql
ALTER EXTERNAL LIBRARY customPackage
    SET (CONTENT = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\customPackage.zip')
    WITH (LANGUAGE = 'R');
```

To install the updated library, execute the stored procedure `sp_execute_external_script`.

```sql
EXECUTE sp_execute_external_script
    @language = N'R',
    @script = N'library(customPackage)';
```

::: moniker-end

::: moniker range=">=sql-server-ver15 || =sql-server-linux-ver15"
For the Python language, the example also works by replacing `'R'` with `'Python'`.
::: moniker-end

### Alter an existing library using a byte stream

The following example alters the existing library by passing the new bits as a hexadecimal literal.

```sql
ALTER EXTERNAL LIBRARY customLibrary
SET (CONTENT = 0xABC123...) WITH (LANGUAGE = 'R');
```

::: moniker range=">=sql-server-ver15 || =sql-server-linux-ver15 || =azuresqldb-mi-current"
For the Python language, the example also works by replacing `'R'` with `'Python'`.
::: moniker-end

> [!NOTE]  
> This code sample only demonstrates the syntax; the binary value in `CONTENT =` is truncated for readability and doesn't create a working library. The actual contents of the binary variable are longer.

## Related content

- [CREATE EXTERNAL LIBRARY (Transact-SQL)](create-external-library-transact-sql.md)
- [DROP EXTERNAL LIBRARY (Transact-SQL)](drop-external-library-transact-sql.md)
- [sys.external_library_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-library-files-transact-sql.md)
- [sys.external_libraries (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-libraries-transact-sql.md)
