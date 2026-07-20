---
title: Osql Utility
description: In SQL Server, the osql utility lets you enter Transact-SQL statements, system procedures, and script files. Osql uses ODBC to communicate with the server.
author: rwestMSFT
ms.author: randolphwest
ms.manager: jroth
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
ms.custom:
  - sfi-ropc-blocked
helpviewer_keywords:
  - "statements [SQL Server], command prompt"
  - "QUIT command"
  - "Transact-SQL statements, command prompt"
  - "EXIT command"
  - "operating systems [SQL Server], commands"
  - "osql utility [SQL Server]"
  - "stored procedures [SQL Server], command prompt"
  - "scripts [SQL Server], command prompt"
  - "RESET command"
  - "GO command"
  - "command prompt utilities [SQL Server], osql"
  - "CTRL+C command"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017"
---

# osql Utility

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The **osql** utility allows you to enter [!INCLUDE [tsql](../includes/tsql-md.md)] statements, system procedures, and script files. This utility uses ODBC to communicate with the server.

> [!IMPORTANT]  
> This feature will be removed in a future version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use the feature. Use **sqlcmd** instead. For more information, see [sqlcmd Utility](sqlcmd/sqlcmd-utility.md).

## Syntax

```console
osql
[ -? ] |
[ -L ] |
[
  {
     { -Ulogin_id [ -Ppassword ] } | -E }
     [ -Sserver_name [ \instance_name ] ] [ -Hwksta_name ] [ -ddb_name ]
     [ -ltime_out ] [ -ttime_out ] [ -hheaders ]
     [ -scol_separator ] [ -wcolumn_width ] [ -apacket_size ]
     [ -e ] [ -I ] [ -D data_source_name ]
     [ -ccmd_end ] [ -q "query" ] [ -Q"query" ]
     [ -n ] [ -merror_level ] [ -r { 0 | 1 } ]
     [ -iinput_file ] [ -ooutput_file ] [ -p ]
     [ -b ] [ -u ] [ -R ] [ -O ]
]
```

## Arguments

#### -?

Displays the syntax summary of **osql** switches.

#### -L

Lists the locally configured servers and the names of the servers broadcasting on the network.

> [!NOTE]  
> **osql** might not receive a timely response from all servers in a Windows network. The list of servers returned might vary for each invocation of this option.

#### -U *login_id*

The user login ID. Login IDs are case-sensitive.

#### -P *password*

A user-specified password. If the `-P` option isn't used, **osql** prompts for a password. If the `-P` option is used at the end of the command prompt without any password, **osql** uses the default password (`NULL`).

> [!IMPORTANT]  
> Don't use a blank password. Use a strong password. For more information, see [Strong passwords](../relational-databases/security/strong-passwords.md).

Passwords are case-sensitive.

The `OSQLPASSWORD` environment variable allows you to set a default password for the current session. Therefore, you don't have to hard code a password into batch files.

If you don't specify a password with the `-P` option, **osql** first checks for the `OSQLPASSWORD` variable. If no value is set, **osql** uses the default password, `NULL`. The following example sets the `OSQLPASSWORD` variable at a command prompt and then accesses the **osql** utility:

```console
SET OSQLPASSWORD=abracadabra
osql
```

> [!IMPORTANT]  
> To mask your password, don't specify the `-P` option along with the `-U` option. Instead, after specifying **osql** along with the `-U` option and other switches (don't specify `-P`), press **Enter**, and **osql** prompts you for a password. This method ensures that your password is masked when it's entered.

#### -E

Uses a trusted connection instead of requesting a password.

#### -S *server_name*[\\*instance_name*]

Specifies the instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to connect to. Specify *server_name* to connect to the default instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on that server. Specify `<server_name>\<instance_name>` to connect to a named instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on that server. If no server is specified, **osql** connects to the default instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on the local computer. This option is required when executing **osql** from a remote computer on the network.

#### -H *wksta_name*

A workstation name. The workstation name is stored in `sysprocesses.hostname` and is displayed by `sp_who`. If this option isn't specified, the current computer name is assumed.

#### -d *db_name*

Issues a USE *db_name* statement when **osql** is started.

#### -l *time_out*

Specifies the number of seconds before an **osql** login times out. The default timeout for connecting to **osql** is eight seconds.

#### -t *time_out*

Specifies the number of seconds before a command times out. If a timeout value isn't specified, commands don't time out.

#### -h *headers*

Specifies the number of rows to print between column headings. The default is to print headings one time for each set of query results. Use `-1` to specify that no headers should be printed. If `-1` is used, there must be no space between the parameter and the setting (`-h-1`, not `-h -1`).

#### -s *col_separator*

Specifies the column-separator character, which is a blank space by default. To use characters that have special meaning to the operating system (for example, | ; & < >), enclose the character in double quotation marks (").

#### -w *column_width*

Allows the user to set the screen width for output. The default is 80 characters. When an output line reaches its maximum screen width, it's broken into multiple lines.

#### -a *packet_size*

Allows you to request a different-sized packet. The valid values for *packet_size* are 512 through 65535. The default value **osql** is the server default. Increased packet size can enhance performance on larger script execution where the number of Transact-SQL statements between `GO` commands is substantial. [!INCLUDE [msCoName](../includes/msconame-md.md)] testing indicates that 8192 is typically the fastest setting for bulk copy operations. A larger packet size can be requested, but **osql** defaults to the server default if the request can't be granted.

#### -e

Echoes input.

#### -I

Sets the `QUOTED_IDENTIFIER` connection option on.

#### -D *data_source_name*

Connects to an ODBC data source that is defined using the ODBC driver for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. The **osql** connection uses the options specified in the data source.

> [!NOTE]  
> This option doesn't work with data sources defined for other drivers.

#### -c *cmd_end*

Specifies the command terminator. By default, commands are terminated and sent to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] by entering `GO` on a line by itself. When you reset the command terminator, don't use [!INCLUDE [tsql](../includes/tsql-md.md)] reserved words or characters that have special meaning to the operating system, whether preceded by a backslash or not.

#### -q "*query*"

Executes a query when **osql** starts, but doesn't exit **osql** when the query completes. (The query statement shouldn't include `GO`). If you issue a query from a batch file, use `%<variable>`, or environment `%<variable>%`. For example:

```sql
SET table=sys.objects
osql -E -q "select name, object_id from %table%"
```

Use double quotation marks around the query and single quotation marks around anything embedded in the query.

#### -Q" *query* "

Executes a query and immediately exits **osql**. Use double quotation marks around the query and single quotation marks around anything embedded in the query.

#### -n

Removes numbering and the prompt symbol (>) from input lines.

#### -m *error_level*

Customizes the display of error messages. The message number, state, and error level are displayed for errors of the specified severity level or higher. Nothing is displayed for errors of levels lower than the specified level. Use `-1` to specify that all headers are returned with messages, even informational messages. If using `-1`, there must be no space between the parameter and the setting (`-m-1`, not `-m -1`).

#### -r { 0| 1}

Redirects message output to the screen (`stderr`). If you don't specify a parameter, or if you specify `0`, only error messages with a severity level 11 or higher are redirected. If you specify `1`, all message output (including "print") is redirected.

#### -i *input_file*

Identifies the file that contains a batch of SQL statements or stored procedures. The less than (`<`) comparison operator can be used in place of `-i`.

#### -o *output_file*

Identifies the file that receives output from **osql**. The greater than (`>`) comparison operator can be used in place of `-o`.

If *input_file* isn't Unicode and `-u` isn't specified, *output_file* is stored in OEM format. If *input_file* is Unicode or `-u` is specified, *output_file* is stored in Unicode format.

#### -p

Prints performance statistics.

#### -b

Specifies that **osql** exits and returns a DOS `ERRORLEVEL` value when an error occurs. The value returned to the DOS `ERRORLEVEL` variable is 1 when the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] error message has a severity of 11 or greater; otherwise, the value returned is 0. MS-DOS batch files can test the value of DOS `ERRORLEVEL` and handle the error appropriately.

#### -u

Specifies that *output_file* is stored in Unicode format, regardless of the format of the *input_file*.

#### -R

Specifies that the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] ODBC driver should use client settings when converting currency, date, and time data to character data.

#### -O

Specifies that certain **osql** features be deactivated to match the behavior of earlier versions of **isql**. These features are deactivated:

- EOF batch processing
- Automatic console width scaling
- Wide messages

It also sets the default DOS `ERRORLEVEL` value to `-1`.

> [!NOTE]  
> The `-n`, `-O`, and `-D` options aren't supported by **osql**.

## Remarks

The **osql** utility is started directly from the operating system with the case-sensitive options listed here. After **osql** starts, it accepts SQL statements and sends them to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] interactively. The results are formatted and displayed on the screen (`stdout`). Use `QUIT` or `EXIT` to exit from **osql**.

If you don't specify a user name when you start **osql**, [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] checks for the environment variables and uses those, for example, `osqluser=(<user>)` or `osqlserver=(<server>)`. If no environment variables are set, the workstation user name is used. If you don't specify a server, the name of the workstation is used.

If neither the `-U` or `-P` options are used, [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] attempts to connect using Windows Authentication Mode. Authentication is based on the Windows account of the user running **osql**.

The **osql** utility uses the ODBC API. The utility uses the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] ODBC driver default settings for the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] ISO connection options. For more information, see [Considerations When You Use the SET Statements](../t-sql/statements/set-statements-transact-sql.md#considerations-when-you-use-the-set-statements).

> [!NOTE]  
> The **osql** utility doesn't support CLR user-defined data types. To process these data types, you must use the **sqlcmd** utility. For more information, see [sqlcmd Utility](sqlcmd/sqlcmd-utility.md).

## OSQL Commands

In addition to [!INCLUDE [tsql](../includes/tsql-md.md)] statements within **osql**, these commands are also available.

| Command | Description |
| --- | --- |
| `GO` | Executes all statements entered after the last `GO`. |
| `RESET` | Clears any statements you have entered. |
| `QUIT` or `EXIT()` | Exits from **osql**. |
| **Ctrl**+**C** | Ends a query without exiting from **osql**. |

> [!NOTE]  
> The `!!` and `ED` commands aren't supported by **osql**.

The command terminators `GO` (by default), `RESET`, `EXIT`, `QUIT`, and **Ctrl**+**C**, are recognized only if they appear at the beginning of a line, immediately following the **osql** prompt.

`GO` signals both the end of a batch and the execution of any cached [!INCLUDE [tsql](../includes/tsql-md.md)] statements. When you press **Enter** at the end of each input line, **osql** caches the statements on that line. When you press **Enter** after typing `GO`, all of the currently cached statements are sent as a batch to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)].

The current **osql** utility works as if there's an implied `GO` at the end of any script executed, therefore all statements in the script execute.

End a command by typing a line beginning with a command terminator. You can follow the command terminator with an integer to specify how many times the command should be run. For example, to execute this command 100 times, type:

```sql
SELECT x = 1
GO 100
```

The results are printed once at the end of execution. **osql** doesn't accept more than 1,000 characters per line. Large statements should be spread across multiple lines.

You can use the command recall facilities in Windows, to recall and modify **osql** statements. The existing query buffer can be cleared by typing `RESET`.

When **osql** runs stored procedures, it prints a blank line between each set of results in a batch. In addition, the "0 rows affected" message doesn't appear when it doesn't apply to the statement executed.

## Use osql interactively

To use **osql** interactively, type the **osql** command (and any of the options) at a command prompt.

You can read in a file containing a query (such as Stores.qry) for execution by **osql** by typing a command similar to this:

```console
osql -E -i stores.qry
```

You can read in a file containing a query (such as Titles.qry) and direct the results to another file by typing a command similar to this:

```console
osql -E -i titles.qry -o titles.res
```

> [!IMPORTANT]  
> When possible, use the **-E** option (trusted connection).

When you use **osql** interactively, you can read an operating system file into the command buffer with `:r<file_name>`. This sends the SQL script in *file_name* directly to the server as a single batch.

> [!NOTE]  
> **osql** treats the batch separator `GO` as a syntax error, if it appears in a SQL script file.

## Insert comments

You can include comments in a Transact-SQL statement submitted to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] by **osql**. Two types of commenting styles are allowed: `--` and `/*...*/`.

## Use exit to return results in osql

You can use the result of a `SELECT` statement as the return value from **osql**. If it's numeric, the last column of the last result row is converted to a 4-byte integer (long). MS-DOS passes the low byte to the parent process or operating system error level. Windows passes the entire 4-byte integer. The syntax is:

```console
EXIT ( <query> )
```

For example:

```sql
EXIT(SELECT @@ROWCOUNT)
```

You can also include the `EXIT` parameter as part of a batch file. For example:

```sql
osql -E -Q "EXIT(SELECT COUNT(*) FROM '%1')"
```

The **osql** utility passes everything between the parentheses `()` to the server exactly as entered. If a stored system procedure selects a set and returns a value, only the selection is returned. The `EXIT()` statement with nothing between the parentheses executes everything preceding it in the batch and then exits with no return value.

There are four `EXIT` formats:

| Format | Details |
| --- | --- |
| `EXIT` | Doesn't execute the batch; quits immediately and returns no value. |
| `EXIT()` | Executes the batch, and then quits and returns no value. |
| `EXIT(<query>)` | Executes the batch, including the query, and then quits after returning the results of the query. |
| `RAISERROR` with a state of `127` | If `RAISERROR` is used within an **osql** script and a state of 127 is raised, **osql** quits and returns the message ID back to the client. |

The following example demonstrates how to affect **osql** behavior with `RAISERROR`:

```sql
RAISERROR (50001, 10, 127);
```

This error causes the **osql** script to end, and returns message ID 50001 to the client.

The return values `-1` to `-99` are reserved. **osql** defines the following values:

- `-100`: Error encountered before selecting return value.
- `-101`: No rows found when selecting return value.
- `-102`: Conversion error occurred when selecting return value.

## Display money and smallmoney data types

**osql** displays the **money** and **smallmoney** data types with two decimal places although [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] stores the value internally with four decimal places. Consider the example:

```sql
SELECT CAST (CAST (10.3496 AS MONEY) AS DECIMAL (6, 4));
GO
```

This statement produces a result of `10.3496`, which indicates that the value is stored with all decimal places intact.

## Related content

- [Comment (MDX)](../mdx/comment-mdx.md)
- [Comment - MDX Operator Reference](../mdx/comment-mdx-operator-reference.md)
- [CAST and CONVERT (Transact-SQL)](../t-sql/functions/cast-and-convert-transact-sql.md)
- [RAISERROR (Transact-SQL)](../t-sql/language-elements/raiserror-transact-sql.md)
