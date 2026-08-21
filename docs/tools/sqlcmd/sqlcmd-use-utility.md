---
title: Use sqlcmd
description: Learn how to use the sqlcmd for ad hoc interactive execution of Transact-SQL statements and scripts, and automate Transact-SQL scripting tasks.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mahyon
ms.date: 01/28/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Transact-SQL statements, executing"
  - "command prompt utilities [SQL Server], sqlcmd"
  - "command line utilities [SQL Server], sqlcmd"
  - "statements [SQL Server], executing"
  - "sqlcmd utility, about sqlcmd utility"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Use sqlcmd

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

**sqlcmd** is a command-line utility for ad hoc, interactive execution of Transact-SQL (T-SQL) statements and scripts. It also automates T-SQL scripting tasks. To use **sqlcmd** interactively or to build script files for **sqlcmd**, you should understand T-SQL. You can use **sqlcmd** in various ways. For example:

- Enter T-SQL statements from a command line interface (CLI). The console returns the results.

  > [!NOTE]  
  > In Windows, you can open a Command Prompt window using `cmd` in the Windows search box, and selecting **Command Prompt**. In macOS and Linux, you can use the built-in terminal emulator.

  Type `sqlcmd` in the console, followed by a list of options that you want, and then **Enter**. For a complete list of the options that **sqlcmd** supports, see [sqlcmd utility](sqlcmd-utility.md).

- Submit a **sqlcmd** job either by specifying a single T-SQL statement to execute, or by pointing the utility to a text file that contains T-SQL statements to execute. The output is directed to a text file, but can also be displayed in the console.

- [SQLCMD mode](/ssms/scripting/sqlcmd-scripts-query-editor) in SQL Server Management Studio (SSMS).
- SQL Server Management Objects (SMO).
- SQL Server Agent CmdExec jobs.

## Common sqlcmd options

- Server option (`-S`) identifies the instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to which **sqlcmd** connects.

- Authentication options (`-E`, `-U`, and `-P`) specify the credentials that **sqlcmd** uses to connect to the instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

  > [!NOTE]  
  > The option `-E` is the default and doesn't need to be specified.

- Input options (`-Q`, `-q`, and `-i`) identify the location of the input to **sqlcmd**.

- The output option (`-o`) specifies the file in which **sqlcmd** writes its output.

## Connect to the sqlcmd utility

- Connect to a default instance by using Windows Authentication to interactively run T-SQL statements:

  ```console
  sqlcmd -S <ComputerName>
  ```

  > [!NOTE]  
  > In the previous example, `-E` isn't specified because it's the default. **sqlcmd** connects to the default instance by using Windows Authentication.

- Connect to a named instance by using Windows Authentication to interactively run T-SQL statements:

  ```console
  sqlcmd -S <ComputerName>\<InstanceName>
  ```

  or

  ```console
  sqlcmd -S .\<InstanceName>
  ```

- Connect to a named instance by using Windows Authentication and specifying input and output files:

  ```console
  sqlcmd -S <ComputerName>\<InstanceName> -i <MyScript.sql> -o <MyOutput.rpt>
  ```

- Connect to the default instance on the local computer with Windows Authentication, execute a query, and keep **sqlcmd** running after the query finishes:

  ```console
  sqlcmd -q "SELECT * FROM AdventureWorks2025.Person.Person"
  ```

- Connect to the default instance on the local computer with Windows Authentication, execute a query, direct the output to a file, and exit **sqlcmd** after the query finishes:

  ```console
  sqlcmd -Q "SELECT * FROM AdventureWorks2025.Person.Person" -o MyOutput.txt
  ```

- Connect to a named instance using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Authentication to interactively run T-SQL statements, with **sqlcmd** prompting for a password:

  ```console
  sqlcmd -U MyLogin -S <ComputerName>\<InstanceName>
  ```

  > [!TIP]  
  > To see a list of the options that the **sqlcmd** utility supports, run: `sqlcmd -?`.

## Run Transact-SQL statements interactively by using sqlcmd

Use the **sqlcmd** utility interactively to execute T-SQL statements in the console. To interactively execute T-SQL statements by using **sqlcmd**, run the utility without using the `-Q`, `-q`, `-Z`, or `-i` options to specify any input files or queries. For example:

```console
sqlcmd -S <ComputerName>\<InstanceName>
```

When you run the command without input files or queries, **sqlcmd** connects to the specified instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. It then displays a new line with a `1>` followed by a blinking underscore that is named the **sqlcmd** prompt. The `1` signifies that this is the first line of a T-SQL statement, and the **sqlcmd** prompt is the point at which the T-SQL statement starts when you type it in.

At the **sqlcmd** prompt, you can type both T-SQL statements and **sqlcmd** commands, such as `GO` and `EXIT`. Each T-SQL statement goes into a buffer called the statement cache. These statements are sent to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] after you type the `GO` command and press **Enter**. To exit **sqlcmd**, type `EXIT` or `QUIT` at the start of a new line.

To clear the statement cache, type `:RESET`. Typing **Ctrl**+**C** causes **sqlcmd** to exit. **Ctrl**+**C** can also be used to stop the execution of the statement cache after a `GO` command.

Enter the `:ED` command at the **sqlcmd** prompt to edit T-SQL statements. The editor opens and, after editing the T-SQL statement and closing the editor, the revised T-SQL statement appears in the command window. Enter `GO` to run the revised T-SQL statement.

## Quoted strings

You can use characters that are enclosed in quotation marks without any extra preprocessing, except that you can insert quotation marks into a string by entering two consecutive quotation marks. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] treats this character sequence as one quotation mark. (However, the translation occurs in the server.) Scripting variables aren't expanded when they appear within a string.

For example:

```console
sqlcmd
PRINT "Length: 5"" 7'";
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Length: 5" 7'
```

## Strings that span multiple lines

**sqlcmd** supports strings that span multiple lines. For example, the following `SELECT` statement spans multiple lines but executes as a single string after you type `GO` and then press **Enter**.

```sql
SELECT <First line>
FROM <Second line>
WHERE <Third line>;
GO
```

## Interactive sqlcmd example

This example shows what you see when you run **sqlcmd** interactively.

When you open a console window in Windows, you might see output similar to the following example:

```output
C:\Temp\>
```

This line means the folder `C:\Temp\` is the current folder. If you specify a file name, the operating system looks for the file in that folder.

Type **sqlcmd** to connect to the default instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on the local computer. The contents of the console window are as follows:

```console
C:\Temp>sqlcmd
1>
```

This output means you connected to the instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. **sqlcmd** is now ready to accept T-SQL statements and **sqlcmd** commands. The flashing underscore after the `1>` is the **sqlcmd** prompt. It marks the location at which the statements and commands you type are displayed. Now, type `USE AdventureWorks2025` and press **Enter**. Then, type `GO` and press **Enter**. The contents of the console are as follows:

```console
sqlcmd
USE AdventureWorks2025;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Changed database context to 'AdventureWorks2025'.
1>
```

When you press **Enter**, it signals **sqlcmd** to start a new line. Pressing **Enter** after you type `GO` signals **sqlcmd** to send the `USE AdventureWorks2025` statement to the instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. **sqlcmd** then returns a message to indicate that the `USE` statement completed successfully. It displays a new `1>` prompt as a signal to enter a new statement or command.

The following example shows what the console contains if you type a `SELECT` statement, a `GO` to execute the `SELECT`, and an `EXIT` to exit **sqlcmd**:

```sql
USE AdventureWorks2025;
GO
SELECT TOP (3) BusinessEntityID, FirstName, LastName
FROM Person.Person;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
BusinessEntityID  FirstName  LastName
----------------- ---------- ------------
1                 Syed       Abbas
2                 Catherine  Abel
3                 Kim        Abercrombie
```

After **sqlcmd** generates output, it resets the **sqlcmd** prompt and displays `1>`. Type `EXIT` at the `1>` prompt to exit the session. You can now close the console window by typing another `EXIT` command, followed by **Enter**.

## Create and query a SQL Server container

You can use **sqlcmd** (Go) to create a new instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] in a container. **sqlcmd** (Go) exposes a `create` statement to specify a container image and [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backup. You can quickly create a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance for development, debugging, and analysis purposes.

> [!IMPORTANT]  
> You need a container runtime installed, such as [Docker](https://www.docker.com/) or [Podman](https://podman.io/).

The following command shows how to see all available options to create a new [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container:

```console
sqlcmd create mssql --help
```

The following command creates a new [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance using the latest version of [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], then restores the Wide World Importers sample database:

```console
sqlcmd create mssql --accept-eula --tag 2025-latest --using https://github.com/Microsoft/sql-server-samples/releases/download/wide-world-importers-v1.0/WideWorldImporters-Full.bak
```

After you create the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance, use **sqlcmd** (Go) to manage and query it.

The following command confirms the version of the instance that you created:

```console
sqlcmd query "SELECT @@version"
```

The following command starts an interactive session with the instance that you created:

```console
sqlcmd query
```

The following command lists connection strings to use to connect to the instance that you created:

```console
sqlcmd config connection-strings
```

Use the following command to remove the container when it's no longer needed:

```console
sqlcmd delete
```

## Run Transact-SQL script files using sqlcmd

Use **sqlcmd** to run database script files. Script files are text files that contain a mix of T-SQL statements, **sqlcmd** commands, and scripting variables. For more information about how to script variables, see [Use sqlcmd with scripting variables](sqlcmd-use-scripting-variables.md). **sqlcmd** works with the statements, commands, and scripting variables in a script file in a manner similar to how it works with statements and commands that you enter interactively. The main difference is that **sqlcmd** reads through the input file without pause instead of waiting for a user to enter the statements, commands, and scripting variables.

You can create database script files in different ways:

- Interactively build and debug a set of T-SQL statements in SQL Server Management Studio, and then save the contents of the Query window as a script file.

- Create a text file that contains T-SQL statements by using a text editor, such as Notepad.

## Examples

### A. Run a script by using sqlcmd

Start Notepad, and type the following T-SQL statements:

```sql
USE AdventureWorks2025;
GO
SELECT TOP (3) BusinessEntityID, FirstName, LastName
FROM Person.Person;
GO
```

Create a folder named `MyFolder` and then save the script as the file `MyScript.sql` in the folder `C:\MyFolder`. Enter the following command in the console to run the script and put the output in `MyOutput.txt` in `MyFolder`:

```console
sqlcmd -i C:\MyFolder\MyScript.sql -o C:\MyFolder\MyOutput.txt
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
Changed database context to 'AdventureWorks2025'.
BusinessEntityID  FirstName  LastName
----------------- ---------- ------------
1                 Syed       Abbas
2                 Catherine  Abel
3                 Kim        Abercrombie
(3 rows affected)
```

### B. Use sqlcmd with a dedicated administrative connection

The following example uses **sqlcmd** to connect to a server that has a blocking problem, by using the dedicated administrator connection (DAC).

```console
C:\Temp\>sqlcmd -S ServerName -A
1> SELECT session_id, blocking_session_id FROM sys.dm_exec_requests WHERE blocking_session_id <> 0;
2> GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
session_id   blocking_session_id
-----------  --------------------`
62           64
(1 rows affected)
```

Use **sqlcmd** to end the blocking process.

```console
1> KILL 64;
2> GO
```

### C. Use sqlcmd to execute a stored procedure

The following example shows how to execute a stored procedure by using **sqlcmd**. Create the following stored procedure.

```sql
USE AdventureWorks2025;
GO

IF OBJECT_ID('dbo.ContactEmailAddress', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ContactEmailAddress;
GO

CREATE PROCEDURE dbo.ContactEmailAddress (
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50)
)
AS
SET NOCOUNT ON;

SELECT EmailAddress
FROM Person.Person
WHERE FirstName = @FirstName
    AND LastName = @LastName;

SET NOCOUNT OFF;
GO
```

At the **sqlcmd** prompt, enter the following command:

```console
C:\Temp\sqlcmd
1> :Setvar FirstName Gustavo
1> :Setvar LastName Achong
1> EXEC dbo.ContactEmailAddress $(FirstName),$(LastName)
2> GO
EmailAddress
-----------------------------
gustavo0@adventure-works.com
```

### D. Use sqlcmd for database maintenance

The following example shows how to use **sqlcmd** for a database maintenance task. Create `C:\Temp\BackupTemplate.sql` with the following code.

```sql
USE master;
BACKUP DATABASE [$(db)] TO DISK = '$(bakfile)';
```

At the **sqlcmd** prompt, enter the following code:

```console
C:\Temp\>sqlcmd
1> :connect <server>
Sqlcmd: Successfully connected to server <server>.
1> :setvar db msdb
1> :setvar bakfile C:\Temp\msdb.bak
1> :r C:\Temp\BackupTemplate.sql
2> GO
Changed database context to 'master'.
Processed 688 pages for database 'msdb', file 'MSDBData' on file 2.
Processed 5 pages for database 'msdb', file 'MSDBLog' on file 2.
BACKUP DATABASE successfully processed 693 pages in 0.725 seconds (7.830 MB/sec)
```

### E. Use sqlcmd to run code on multiple instances

The following code in a file shows a script that connects to two instances. Notice `GO` before the connection to the second instance.

```console
:CONNECT <server>\,<instance1>
EXEC dbo.SomeProcedure
GO
:CONNECT <server>\,<instance2>
EXEC dbo.SomeProcedure
GO
```

### F. Return XML output

The following example shows how XML output returns as an unformatted, continuous stream.

```console
C:\Temp\>sqlcmd -d AdventureWorks2025
1> :XML ON
1> SELECT TOP 3 FirstName + ' ' + LastName + ', '
2> FROM Person.Person
3> GO
Syed Abbas, Catherine Abel, Kim Abercrombie,
```

### G. Use sqlcmd in a Windows script file

You can run a **sqlcmd** command, such as `sqlcmd -i C:\Temp\InputFile.txt -o C:\Temp\OutputFile.txt`, in a `.bat` file along with VBScript. Don't use interactive options in this scenario. **sqlcmd** must be installed on the computer that runs the `.bat` file.

First, create the following four files in `C:\Temp`:

- C:\Temp\badscript.sql

  ```sql
  SELECT batch_1_this_is_an_error
  GO
  SELECT 'batch #2'
  GO
  ```

- C:\Temp\goodscript.sql

  ```sql
  SELECT 'batch #1';
  GO
  SELECT 'batch #2';
  GO
  ```

- C:\Temp\returnvalue.sql

  ```sql
  :exit(select 100)
  ```

- C:\Temp\windowsscript.bat

  ```console
  @echo off

  echo Running badscript.sql
  sqlcmd -i badscript.sql -b -o out.log
  if not errorlevel 1 goto next1
  echo == An error occurred

  :next1

  echo Running goodscript.sql
  sqlcmd -i goodscript.sql -b -o out.log
  if not errorlevel 1 goto next2
  echo == An error occurred

  :next2
  echo Running returnvalue.sql
  sqlcmd -i returnvalue.sql -o out.log
  echo SQLCMD returned %errorlevel% to the command shell

  :exit
  ```

Then, run `C:\Temp\windowsscript.bat` in the console:

```console
C:\Temp\>windowsscript.bat
Running badscript.sql
== An error occurred
Running goodscript.sql
Running returnvalue.sql

SQLCMD returned 100 to the command shell
```

### H. Use sqlcmd to set encryption on Azure SQL Database

You can run **sqlcmd** on a connection to [!INCLUDE [ssSDS](../../includes/sssds-md.md)] data to specify encryption and certificate trust. Two **sqlcmd** options are available:

- The `-N` switch is the client request for an encrypted connection. This option is equivalent to the ADO.NET option `ENCRYPT = true`.

- The `-C` switch configures the client to implicitly trust the server certificate and not validate it. This option is equivalent to the ADO.NET option `TRUSTSERVERCERTIFICATE = true`.

The [!INCLUDE [ssSDS](../../includes/sssds-md.md)] service doesn't support all the `SET` options available on a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. The following options throw an error when the corresponding `SET` option is set to `ON` or `OFF`:

- `SET ANSI_DEFAULTS`
- `SET ANSI_NULLS`
- `SET REMOTE_PROC_TRANSACTIONS`
- `SET ANSI_NULL_DEFAULT`

The following `SET` options are deprecated. They don't throw exceptions but can't be used:

- `SET CONCAT_NULL_YIELDS_NULL`
- `SET ANSI_PADDING`
- `SET QUERY_GOVERNOR_COST_LIMIT`

#### Syntax

The following examples refer to cases where SQL Server Native Client Provider settings include:

- `ForceProtocolEncryption = False`
- `Trust Server Certificate = No`

Connect using Windows credentials and encrypt communication:

```console
sqlcmd -E -N
```

Connect using Windows credentials and trust server certificate:

```console
sqlcmd -E -C
```

Connect using Windows credentials, encrypt communication, and trust server certificate:

```console
sqlcmd -E -N -C
```

The following examples refer to cases where SQL Server Native Client Provider settings include:

- `ForceProtocolEncryption = True`
- `TrustServerCertificate = Yes`

Connect using Windows credentials, encrypt communication, and trust server certificate:

```console
sqlcmd -E
```

Connect using Windows credentials, encrypt communication, and trust server certificate:

```console
sqlcmd -E -N
```

Connect using Windows credentials, encrypt communication, and trust server certificate:

```console
sqlcmd -E -C
```

Connect using Windows credentials, encrypt communication, and trust server certificate:

```console
sqlcmd -E -N -C
```

If the provider specifies `ForceProtocolEncryption = True`, then encryption is enabled even if `Encrypt=No` in the connection string.

## Related content

- [sqlcmd utility](sqlcmd-utility.md)
- [Use sqlcmd with scripting variables](sqlcmd-use-scripting-variables.md)
- [Edit SQLCMD scripts with Query Editor](/ssms/scripting/sqlcmd-scripts-query-editor)
- [Manage Job Steps](/ssms/agent/manage-job-steps)
- [Create a CmdExec Job Step](/ssms/agent/create-a-cmdexec-job-step)
