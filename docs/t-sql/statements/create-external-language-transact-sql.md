---
title: CREATE EXTERNAL LANGUAGE (Transact-SQL) - SQL Server
description: CREATE EXTERNAL LANGUAGE (Transact-SQL) - SQL Server
author: VanMSFT
ms.author: vanto
ms.date: 06/22/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: language-extensions
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---

# CREATE EXTERNAL LANGUAGE (Transact-SQL)
[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

Registers external language extensions in the database from the specified file path or byte stream. This statement is a generic mechanism for the database administrator to register new external language extensions on any OS platform that SQL Server supports. For more information, see [Language Extensions](../../language-extensions/language-extensions-overview.md).

> [!NOTE]
> **R** and **Python** are reserved names. You can't create an external language with those specific names. For more information on how to use **R** and **Python**, see [SQL Server Machine Learning Services](../../machine-learning/index.yml).

## Syntax

```syntaxsql
CREATE EXTERNAL LANGUAGE language_name  
[ AUTHORIZATION owner_name ]  
FROM <file_spec> [ ,...2 ]  
[ ; ]  

<file_spec> ::=  
{
    ( CONTENT = { <external_lang_specifier> | <content_bits> },
    FILE_NAME = <external_lang_file_name>
    [ , PLATFORM = <platform> ]
    [ , PARAMETERS = <external_lang_parameters> ]
    [ , ENVIRONMENT_VARIABLES = <external_lang_env_variables> ] )
}

<external_lang_specifier> :: =  
{
    '[file_path\]os_file_name'  
}

<content_bits> :: =  
{
    varbinary_literal
    | varbinary_expression
}

<external_lang_file_name> :: =  
'extension_file_name'


<platform> :: =
{
    WINDOWS
  | LINUX
}

<external_lang_parameters> :: =  
'extension_specific_parameters'
```

### Arguments

**language_name**

Languages are database-scoped objects. Language names must be unique within the database.

**owner_name**

Specifies the name of the user or role that owns the external language. If you don't specify a value, the current user becomes the owner. Depending on permissions, other users might need explicit permission to execute scripts using a specific language.

**file_spec**

Specifies the content of the language extension. Only one `<file_spec>` is allowed for a specific language, per platform.

**external_lang_specifier**

The full file path to the .zip or tar.gz file that contains the extension's code. This content can be either a path to a .zip file (on Windows) or a tar.gz file (on Linux).

**content_bits**

Specifies the content of the language as a hex literal, similar to assemblies.

Use this option when the server file system is restricted and you can't copy the library files to a location that the server can access. You must have the required permissions to create or alter the language.

**external_lang_file_name**

Name of the extension .dll or .so file. The name identifies the correct file when several .dll or .so files exist in the `<external_lang_specifier>` .zip or tar.gz.

**external_lang_parameters**

Specifies a set of parameters to pass to the external language runtime. The external runtime receives parameter values after the external process starts. Environment variables, in contrast, become accessible to the language extension before the external process starts.

**external_lang_env_variables**

Specifies a set of environment variables to make available to the external language runtime before the external process starts. For example, set the home directory of the runtime itself, such as `JRE_HOME`.

**platform**

This parameter is needed for hybrid OS scenarios. In a hybrid architecture, you must register the language once per platform. If you don't specify a platform, SQL Server assumes the current OS.

## Permissions

Requires the `CREATE EXTERNAL LANGUAGE` permission. By default, any member of the **db_owner** fixed database role has permissions to create an external language. For all other users, you must explicitly grant permission by using a [GRANT](./grant-database-permissions-transact-sql.md) statement, specifying `CREATE EXTERNAL LANGUAGE` as the privilege.

To modify a library, you need the separate permission, `ALTER ANY EXTERNAL LANGUAGE`.

### EXECUTE EXTERNAL SCRIPT permission

Use `EXECUTE EXTERNAL SCRIPT` permissions to grant external script execution on specific languages. `EXECUTE EXTERNAL SCRIPT` differs from the `EXECUTE ANY EXTERNAL SCRIPT` database permission, which doesn't allow granting execution permission on a specific language.

Grant non-**dbo** users permission to execute a specific language:

```sql
GRANT EXECUTE EXTERNAL SCRIPT ON EXTERNAL LANGUAGE ::language_name
TO database_principal_name;
```

### Reference permissions to external libraries

Similar to assemblies, external languages require reference permissions so that there's a link between external libraries and external languages. For example, before you drop an external language, you must drop all external libraries that reference it. View the external language as a higher-level object than external libraries in the hierarchy.

## Examples

### A. Create an external language in a database

The following example adds an external language called Java to a database on SQL Server on Windows.

```sql
CREATE EXTERNAL LANGUAGE Java 
FROM (CONTENT = N'<path-to-zip>', FILE_NAME = 'javaextension.dll');
GO
```

### B. Create an external language for both Windows and Linux

You can specify up to two `<file_spec>`, one for Windows and one for Linux.

```sql
CREATE EXTERNAL LANGUAGE Java
FROM
(CONTENT = N'<path-to-zip>', FILE_NAME = 'javaextension.dll', PLATFORM = WINDOWS),
(CONTENT = N'<path-to-tar.gz>', FILE_NAME = 'javaextension.so', PLATFORM = LINUX);
GO
```
### C. Grant permissions to execute external script

The following example grants the **mylogin** principal access to execute scripts using the **Java** external language.

```sql
GRANT EXECUTE EXTERNAL SCRIPT ON EXTERNAL LANGUAGE ::Java 
TO mylogin;
```


## Related content

- [ALTER EXTERNAL LANGUAGE (Transact-SQL)](alter-external-language-transact-sql.md)
- [DROP EXTERNAL LANGUAGE (Transact-SQL)](drop-external-language-transact-sql.md)
- [sys.external_languages (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-languages-transact-sql.md)
- [sys.external_language_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-language-files-transact-sql.md)
