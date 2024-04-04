---
title: "xp_cmdshell (Transact-SQL)"
description: "Spawns a Windows command shell and passes in a string for execution. Any output is returned as rows of text."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_cmdshell"
  - "xp_cmdshell_TSQL"
helpviewer_keywords:
  - "xp_cmdshell"
dev_langs:
  - "TSQL"
---
# xp_cmdshell (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Spawns a Windows command shell and passes in a string for execution. Any output is returned as rows of text.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_cmdshell { 'command_string' } [ , NO_OUTPUT ]
```

## Arguments

#### '*command_string*'

The string that contains a command to be passed to the operating system. *command_string* is **varchar(8000)** or **nvarchar(4000)**, with no default. *command_string* can't contain more than one set of double quotation marks. A single pair of quotation marks is required if any spaces are present in the file paths or program names referenced in *command_string*. If you have trouble with embedded spaces, consider using FAT 8.3 file names as a workaround.

#### NO_ OUTPUT

An optional parameter, specifying that no output should be returned to the client.

## Return code values

`0` (success) or `1` (failure).

## Result set

Executing the following `xp_cmdshell` statement returns a directory listing of the current directory.

```sql
EXEC xp_cmdshell 'dir *.exe';
GO
```

The rows are returned in an **nvarchar(255)** column. If the `NO_OUTPUT` option is used, only the following output is returned:

```output
The command(s) completed successfully.
```

## Remarks

The Windows process spawned by `xp_cmdshell` has the same security rights as the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account.

> [!CAUTION]  
> `xp_cmdshell` is a powerful feature and disabled by default. `xp_cmdshell` can be enabled and disabled by using Policy-Based Management or by executing `sp_configure`. For more information, see [Surface area configuration](../security/surface-area-configuration.md) and [xp_cmdshell (server configuration option)](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md). Using `xp_cmdshell` can trigger security audit tools.

`xp_cmdshell` operates synchronously. Control isn't returned to the caller until the command-shell command is completed. If `xp_cmdshell` is executed within a batch and returns an error, the batch will fail.

## xp_cmdshell proxy account

When it's called by a user that isn't a member of the **sysadmin** fixed server role, `xp_cmdshell` connects to Windows by using the account name and password stored in the credential named **##xp_cmdshell_proxy_account##**. If this proxy credential doesn't exist, `xp_cmdshell` fails.

The proxy account credential can be created by executing `sp_xp_cmdshell_proxy_account`. As arguments, this stored procedure takes a Windows user name and password. For example, the following command creates a proxy credential for Windows domain user `SHIPPING\KobeR` that has the Windows password `sdfh%dkc93vcMt0`.

```sql
EXEC sp_xp_cmdshell_proxy_account 'SHIPPING\KobeR', 'sdfh%dkc93vcMt0';
```

For more information, see [sp_xp_cmdshell_proxy_account (Transact-SQL)](sp-xp-cmdshell-proxy-account-transact-sql.md).

## Permissions

Because malicious users sometimes attempt to elevate their privileges by using `xp_cmdshell`, `xp_cmdshell` is disabled by default. Use `sp_configure` or **Policy Based Management** to enable it. For more information, see [xp_cmdshell Server Configuration Option](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md).

When first enabled, `xp_cmdshell` requires CONTROL SERVER permission to execute and the Windows process created by `xp_cmdshell` has the same security context as the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account often has more permissions than are necessary for the work performed by the process created by `xp_cmdshell`. To enhance security, access to `xp_cmdshell` should be restricted to highly privileged users.

To allow non-administrators to use `xp_cmdshell`, and allow [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to create child processes with the security token of a less-privileged account, follow these steps:

1. Create and customize a Windows local user account or a domain account with the least privileges that your processes require.

1. Use the `sp_xp_cmdshell_proxy_account` system procedure to configure `xp_cmdshell` to use that least-privileged account.

   > [!NOTE]  
   > You can also configure this proxy account using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by right-clicking **Properties** on your server name in Object Explorer, and looking on the **Security** tab for the **Server proxy account** section.

1. In [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)], using the `master` database, execute the following Transact-SQL statement to give specific non-**sysadmin** users the ability to execute `xp_cmdshell`. The specified user must exist in the `master` database.

   ```sql
    GRANT exec ON xp_cmdshell TO N'<some_user>';
   ```

Now non-administrators can launch operating system processes with `xp_cmdshell` and those processes run with the permissions of the proxy account that you configured. Users with CONTROL SERVER permission (members of the **sysadmin** fixed server role) continue to receive the permissions of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account for child processes that are launched by `xp_cmdshell`.

To determine the Windows account being used by `xp_cmdshell` when launching operating system processes, execute the following statement:

```sql
EXEC xp_cmdshell 'whoami.exe';
```

To determine the security context for another login, execute the following Transact-SQL code:

```sql
EXEC AS LOGIN = '<other_login>';
GO
xp_cmdshell 'whoami.exe';
REVERT;
```

## Examples

### A. Return a list of executable files

The following example shows the `xp_cmdshell` extended stored procedure executing a directory command.

```sql
EXEC master..xp_cmdshell 'dir *.exe'
```

### B. Return no output

The following example uses `xp_cmdshell` to execute a command string without returning the output to the client.

```sql
USE master;

EXEC xp_cmdshell 'copy c:\SQLbcks\AdvWorks.bck
    \\server2\backups\SQLbcks', NO_OUTPUT;
GO
```

### C. Use return status

In the following example, the `xp_cmdshell` extended stored procedure also suggests return status. The return code value is stored in the variable `@result`.

```sql
DECLARE @result INT;

EXEC @result = xp_cmdshell 'dir *.exe';

IF (@result = 0)
    PRINT 'Success'
ELSE
    PRINT 'Failure';
```

### D. Write variable contents to a file

The following example writes the contents of the `@var` variable to a file named `var_out.txt` in the current server directory.

```sql
DECLARE @cmd SYSNAME,
    @var SYSNAME;

SET @var = 'Hello world';
SET @cmd = 'echo ' + @var + ' > var_out.txt';

EXEC master..xp_cmdshell @cmd;
```

### E. Capture the result of a command to a file

The following example writes the contents of the current directory to a file named `dir_out.txt` in the current server directory.

```sql
DECLARE @cmd SYSNAME,
    @var SYSNAME;

SET @var = 'dir /p';
SET @cmd = @var + ' > dir_out.txt';

EXEC master..xp_cmdshell @cmd;
```

## Related content

- [General extended stored procedures (Transact-SQL)](general-extended-stored-procedures-transact-sql.md)
- [xp_cmdshell (server configuration option)](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md)
- [Surface area configuration](../security/surface-area-configuration.md)
- [sp_xp_cmdshell_proxy_account (Transact-SQL)](sp-xp-cmdshell-proxy-account-transact-sql.md)
