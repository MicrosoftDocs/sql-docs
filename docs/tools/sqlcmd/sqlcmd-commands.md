---
title: Commands in the sqlcmd Utility
description: Control sqlcmd with extra commands for editing, variables, output, and execution control.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mahyon
ms.date: 04/22/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
ms.custom:
  - linux-related-content
  - ignite-2025
helpviewer_keywords:
  - "statements [SQL Server], command prompt"
  - "go-sqlcmd"
  - "QUIT command"
  - "Transact-SQL statements, command prompt"
  - "EXIT command"
  - "sqlcmd commands"
  - "ED command"
  - "sqlcmd utility"
  - "command prompt utilities [SQL Server], sqlcmd"
  - "!! command"
  - "stored procedures [SQL Server], command prompt"
  - "system stored procedures [SQL Server], command prompt"
  - "sqlcmd utility, about sqlcmd utility"
  - "scripts [SQL Server], command prompt"
  - "RESET command"
  - "GO command"
zone_pivot_groups: cs1-command-shell
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb"
---
# Commands in the sqlcmd utility

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

The [sqlcmd utility](sqlcmd-utility.md) accepts Transact-SQL statements, system procedures, and script files.

> [!NOTE]  
> To find out which variant and version of **sqlcmd** is installed on your system, see [Check installed version of sqlcmd utility](sqlcmd-installed-version.md). For information on how to get **sqlcmd**, see [Download and install the sqlcmd utility](sqlcmd-download-install.md).

In addition to Transact-SQL statements within **sqlcmd**, use the following commands:

- `GO [ <count> ]`
- `:List`
- `[:]RESET`
- `:Error`
- `[:]ED` <sup>1</sup>
- `:Out`
- `[:]!!`
- `:Perftrace`
- `[:]QUIT`
- `:Connect`
- `[:]EXIT`
- `:On Error`
- `:r`
- `:Help`
- `:ServerList` <sup>1</sup>
- `:XML [ ON | OFF ]` <sup>1</sup>
- `:Setvar`
- `:Listvar`

<sup>1</sup> Not supported on Linux or macOS.

Keep the following points in mind when you use **sqlcmd** commands:

- All **sqlcmd** commands, except `GO`, must start with a colon (`:`).

  > [!IMPORTANT]  
  > To maintain backward compatibility with existing **osql** scripts, some commands work without the colon (indicated by `[:]`).

- **sqlcmd** recognizes commands only if they appear at the start of a line.

- All **sqlcmd** commands are case insensitive.

- Each command must be on a separate line. You can't follow a command with a Transact-SQL statement or another command.

- Commands run immediately. They aren't put in the execution buffer as Transact-SQL statements are.

## Editing commands

#### [:]ED

Starts the text editor. Use this editor to edit the current Transact-SQL batch, or the last run batch. To edit the last run batch, type the `ED` command immediately after the last batch finishes execution.

The `SQLCMDEDITOR` environment variable defines the text editor. The default editor is `Edit`. To change the editor, set the `SQLCMDEDITOR` environment variable. For example, to set the editor to Microsoft Notepad, type the following command:

```console
SET SQLCMDEDITOR=notepad
```

#### [:]RESET

Clears the statement cache.

#### :List

Prints the content of the statement cache.

## Variables

#### :Setvar \<var> [ "*value*" ]

Defines **sqlcmd** scripting variables. Scripting variables have the following format: `$(VARNAME)`.

Variable names are case insensitive.

Scripting variables can be set in the following ways:

- Implicitly using a command-line option. For example, the `-l` option sets the `SQLCMDLOGINTIMEOUT` **sqlcmd** variable.
- Explicitly by using the `:Setvar` command.
- Defining an environment variable before you run **sqlcmd**.

> [!NOTE]  
> The `-X` option prevents environment variables from being passed on to **sqlcmd**.

If a variable defined by using `:Setvar` and an environment variable have the same name, the variable defined by using `:Setvar` takes precedence.

Variable names must not contain blank space characters.

Variable names can't have the same form as a variable expression, such as `$(var)`.

If the string value of the scripting variable contains blank spaces, enclose the value in quotation marks. If a value for a scripting variable isn't specified, the scripting variable is dropped.

#### :Listvar

Displays a list of the scripting variables that are currently set.

> [!NOTE]  
> Only scripting variables that are set by **sqlcmd**, and variables that are set using the `:Setvar` command, are displayed.

## Output commands

#### :Error \<*filename*> | STDERR | STDOUT

Redirect all error output to the file specified by *filename*, to `stderr`, or to `stdout`. The `:Error` command can appear multiple times in a script. By default, error output goes to `stderr`.

- *filename*

  Creates and opens a file that receives the output. An existing file is truncated to zero bytes. If the file isn't available because of permissions or other reasons, the output isn't switched, and the last specified or default destination receives the error output.

- STDERR

  Switches error output to the `stderr` stream. If output is redirected, the target to which the stream is redirected receives the error output.

- STDOUT

  Switches error output to the `stdout` stream. If output is redirected, the target to which the stream is redirected receives the error output.

#### :Out \<*filename*> | STDERR | STDOUT

Creates and redirects all query results to the file specified by *filename*, to `stderr`, or to `stdout`. By default, output goes to `stdout`. If the file already exists, it's truncated to zero bytes. The `:Out` command can appear multiple times in a script.

#### :Perftrace \<*filename*> | STDERR | STDOUT

Creates and redirects all performance trace information to the file specified by *filename*, to `stderr`, or to `stdout`. By default, performance trace output goes to `stdout`. An existing file is truncated to zero bytes. The `:Perftrace` command can appear multiple times in a script.

## Execution control commands

#### :On Error [ exit | ignore ]

Sets the action to perform when an error occurs during script or batch execution.

When you use the `exit` option, **sqlcmd** exits with the appropriate error value.

When you use the `ignore` option, **sqlcmd** ignores the error and continues executing the batch or script. By default, **sqlcmd** prints an error message.

#### [:]QUIT

Causes **sqlcmd** to exit.

#### [:]EXIT [ ( *statement* ) ]

Use the result of a `SELECT` statement as the return value from **sqlcmd**. If numeric, the first column of the last result row is converted to a 4-byte integer (long). MS-DOS, Linux, and macOS pass the low byte to the parent process or operating system error level. Windows 2000 and later versions pass the whole 4-byte integer. The syntax is `:EXIT(query)`.

For example:

```text
:EXIT(SELECT @@ROWCOUNT)
```

You can also include the `:EXIT` parameter as part of a batch file. For example, at the command prompt, type:

```console
sqlcmd -Q ":EXIT(SELECT COUNT(*) FROM '%1')"
```

The **sqlcmd** utility sends everything between the parentheses (`()`) to the server. If a system stored procedure selects a set and returns a value, only the selection is returned. The `:EXIT()` statement with nothing between the parentheses runs everything before it in the batch, and then exits without a return value.

When you specify an incorrect query, **sqlcmd** exits without a return value.

Here's a list of `EXIT` formats:

- `:EXIT`

  Doesn't run the batch, and then quits immediately and returns no value.

- `:EXIT( )`

  Runs the batch, and then quits and returns no value.

- `:EXIT(query)`

  Runs the batch that includes the query, and then quits after it returns the results of the query.

If you use `RAISERROR` within a **sqlcmd** script, and raise a state of 127, **sqlcmd** quits, and returns the message ID back to the client. For example:

```text
RAISERROR(50001, 10, 127)
```

This error causes the **sqlcmd** script to end and return the message ID 50001 to the client.

The return values `-1` to `-99` are reserved by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], and **sqlcmd** defines extra return values:

| Return value | Description |
| --- | --- |
| `-100` | Error encountered before selecting return value. |
| `-101` | No rows found when selecting return value. |
| `-102` | Conversion error occurred when selecting return value. |

#### GO [*count*]

`GO` signals both the end of a batch and the execution of any cached Transact-SQL statements. The batch is run multiple times as separate batches. You can't declare a variable more than once in a single batch.

## Miscellaneous commands

#### :r \<*filename*>

Parses extra Transact-SQL statements and **sqlcmd** commands from the file specified by *filename* into the statement cache. **sqlcmd** reads *filename* relative to the startup directory.

If the file contains Transact-SQL statements that aren't followed by `GO`, you must enter `GO` on the line that follows `:r`.

**sqlcmd** reads and runs the file after it encounters a batch terminator. You can issue multiple `:r` commands. The file can include any **sqlcmd** command, including the batch terminator `GO`.

> [!NOTE]  
> The line count that displays in interactive mode increases by one for every `:r` command encountered. The `:r` command appears in the output of the list command.

#### :ServerList

Lists the locally configured servers and the names of the servers broadcasting on the network.

#### :Connect *server_name*[\\*instance_name*] [-l *timeout*] [-U *user_name* [-P *password*]] [-N[s|m|o]] [-F hostname_in_certificate]

Connects to an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Also closes the current connection.

> [!IMPORTANT]
> The `:Connect` command doesn't act as an implicit batch separator. Any Transact-SQL statements buffered in the current batch aren't run until a [GO](#go-count) command is issued. If you use multiple `:Connect` commands without intervening `GO` statements, all buffered statements run against the last connected server, and not against each server individually.

- Encryption options (`-N[s|m|o]`):

  Use this option to request an encrypted connection. If you don't include `-N`, `-Nm` (for `mandatory`) is the default. This option is a breaking change from [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, where `-No` (for `optional`) is the default.

  | Value | Description |
  | --- | --- |
  | `-Ns` | Strict |
  | `-Nm` (default) | Mandatory |
  | `-No` | Optional |

- Hostname in certificate (`-F hostname_in_certificate`)

  Specifies a different, expected Common Name (CN) or Subject Alternate Name (SAN) in the server certificate to use during server certificate validation. Without this option, certificate validation ensures that the CN or SAN in the certificate matches the server name to which you're connecting. This parameter can be populated when the server name doesn't match the CN or SAN, for example, when using DNS aliases.

- Timeout options:

  | Value | Behavior |
  | --- | --- |
  | `0` | Wait forever |
  | `n>0` | Wait for *n* seconds |

  The `SQLCMDSERVER` scripting variable reflects the current active connection.

  If *timeout* isn't specified, the value of the `SQLCMDLOGINTIMEOUT` variable is the default.

If you specify only *user_name* (either as an option or as an environment variable), **sqlcmd** prompts you to enter a password. Users aren't prompted if the `SQLCMDUSER` or `SQLCMDPASSWORD` environment variables are set. If you don't provide options or environment variables, Windows Authentication mode is used to sign in. For example, to connect to an instance, `instance1`, of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], `myserver`, by using integrated security you would use the following command:

```text
:connect myserver\instance1
```

To connect to the default instance of `myserver` using scripting variables, use the following settings:

```text
:setvar myusername test
:setvar myservername myserver
:connect $(myservername) $(myusername)
```

#### [:]!! *command*

Runs operating system commands. To run an operating system command, start a line with two exclamation marks (`!!`) followed by the operating system command. For example:

```text
:!! dir
```

> [!NOTE]  
> The command runs on the computer where **sqlcmd** runs.

#### :XML [ ON | OFF ]

For more information, see [XML Output Format](#OutputXML) and [JSON Output Format](#OutputJSON) in this article.

#### :Help

Lists **sqlcmd** commands, along with a short description of each command.

## sqlcmd file names

Specify **sqlcmd** input files by using the `-i` option or the `:r` command. Specify output files by using the `-o` option or the `:Error`, `:Out`, and `:Perftrace` commands. When you work with these files, use the following guidelines:

- Use separate *filename* values for `:Error`, `:Out`, and `:Perftrace`. If you use the same *filename*, the commands might intermingle inputs.

- If you call an input file located on a remote server from **sqlcmd** on a local computer, and the file contains a drive file path such as `:Out c:\OutputFile.txt`, **sqlcmd** creates the output file on the local computer and not on the remote server.

- Valid file paths include: `C:\<filename>`, `\\<Server>\<Share$>\<filename>`, and `"C:\Some Folder\<file name>"`. If there's a space in the path, use quotation marks.

- Each new **sqlcmd** session overwrites existing files that have the same names.

## Informational messages

**sqlcmd** prints any informational message that the server sends. In the following example, after **sqlcmd** runs the Transact-SQL statements, it prints an informational message.

Start **sqlcmd**. At the **sqlcmd** command prompt, type the query:

```sql
USE AdventureWorks2025;
GO
```

When you press <kbd>Enter</kbd>, **sqlcmd** prints the following informational message:

```output
Changed database context to 'AdventureWorks2025'.
```

## Output format from Transact-SQL queries

**sqlcmd** first prints a column header that contains the column names specified in the select list. The column names are separated by using the `SQLCMDCOLSEP` character. By default, this column separator is a space. If the column name is shorter than the column width, **sqlcmd** pads the output with spaces up to the next column.

**sqlcmd** prints a separator line that is a series of dash characters. The following output shows an example.

Start **sqlcmd**. At the **sqlcmd** command prompt, type the query:

```sql
USE AdventureWorks2025;

SELECT TOP (2) BusinessEntityID,
               FirstName,
               LastName
FROM Person.Person;
GO
```

When you press <kbd>Enter</kbd>, **sqlcmd** returns the following result set.

```output
BusinessEntityID FirstName    LastName
---------------- ------------ ----------
285              Syed         Abbas
293              Catherine    Abel
(2 row(s) affected)
```

Although the `BusinessEntityID` column is only four characters wide, it expands to accommodate the longer column name. By default, **sqlcmd** terminates output at 80 characters. You can change this width by using the `-w` option or by setting the `SQLCMDCOLWIDTH` scripting variable.

<a id="OutputXML"></a>

## XML output format

XML output that is the result of a `FOR XML` clause is output, unformatted, in a continuous stream.

When you expect XML output, use the following command: `:XML ON`.

> [!NOTE]  
> **sqlcmd** returns error messages in the usual format. The error messages are also output in the XML text stream in XML format. By using `:XML ON`, **sqlcmd** doesn't display informational messages.

To set the XML mode to off, use the following command: `:XML OFF`.

The `GO` command shouldn't appear before the `:XML OFF` command is issued, because the `:XML OFF` command switches **sqlcmd** back to row-oriented output.

XML (streamed) data and rowset data can't be mixed. If the `:XML ON` command wasn't issued before a Transact-SQL statement that outputs XML streams is run, the output is garbled. Once the `:XML ON` command is issued, you can't run Transact-SQL statements that output regular row sets.

> [!NOTE]  
> The `:XML` command doesn't support the `SET STATISTICS XML` statement.

<a id="OutputJSON"></a>

## JSON output format

When you expect JSON output, use the following command: `:XML ON`. Otherwise, the output includes both the column name and the JSON text. This output isn't valid JSON.

To set the XML mode to off, use the following command: `:XML OFF`.

For more info, see [XML Output Format](#OutputXML) in this article.

<a id="use-azure-ad-authentication"></a>

## Use Microsoft Entra authentication

Examples that use Microsoft Entra authentication:

::: zone pivot="cs1-bash"

```bash
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net  -G  -l 30
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net -G -U bob@contoso.com -P MyAzureADPassword -l 30
```

::: zone-end

::: zone pivot="cs1-powershell"

```powershell
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net  -G  -l 30
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net -G -U bob@contoso.com -P MyAzureADPassword -l 30
```

::: zone-end

::: zone pivot="cs1-cmd"

```cmd
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net  -G  -l 30
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net -G -U bob@contoso.com -P MyAzureADPassword -l 30
```

::: zone-end

## Related content

- [sqlcmd utility](sqlcmd-utility.md)
- [Check installed version of sqlcmd utility](sqlcmd-installed-version.md)
- [Start the sqlcmd utility](sqlcmd-start-utility.md)
- [Execute T-SQL from a script file with sqlcmd](sqlcmd-run-transact-sql-script-files.md)
- [Use sqlcmd](sqlcmd-use-utility.md)
