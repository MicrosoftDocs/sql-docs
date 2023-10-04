---
title: sqlcmd utility - Edit SQLCMD scripts with Query Editor
description: You use SQLCMD scripts when you have to process Windows System commands and Transact-SQL statements in the same script. Learn how to write and edit SQLCMD scripts using the Database Engine Query Editor.
author: dlevy-msft
ms.author: dlevy
ms.reviewer: maghan, randolphwest
ms.date: 08/15/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "scripts [SQL Server], SQLCMD scripts"
  - "SQLCMD scripts"
  - "modifying scripts"
  - "Query Editor [Database Engine], SQLCMD scripts"
  - "scripts [SQL Server], SQL Server Management Studio"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Edit SQLCMD Scripts with Query Editor

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Using the Database Engine Query Editor in SQL Server Management Studio, you can write and edit queries as SQLCMD scripts. You use SQLCMD scripts when processing Windows System commands and Transact-SQL statements in the same script.

## SQLCMD Mode

To use the Database Engine Query Editor to write or edit SQLCMD scripts, you must enable the SQLCMD scripting mode. By default, SQLCMD mode isn't allowed in the Query Editor. You can enable scripting mode by selecting the **SQLCMD Mode** icon in the toolbar or by selecting **SQLCMD Mode** from the **Query** menu.

> [!NOTE]  
> Enabling SQLCMD mode turns off IntelliSense and the Transact-SQL debugger in the Database Engine Query Editor.

SQLCMD scripts in the Query Editor can use the same features that all Transact-SQL scripts use. These features include the following:

- Color coding
- Executing scripts
- Source control
- Parsing scripts
- Showplan

## Enable SQLCMD scripting in Query Editor

To turn on SQLCMD scripting for an active Database Engine Query Editor window, use the following procedure.

#### Switch a database engine query editor window to SQLCMD mode

1. In Object Explorer, right-click the server and then select **New Query**, to open a new Database Engine Query Editor window.

1. On the **Query** menu, select **SQLCMD Mode**.

     The Query Editor executes SQLCMD statements in the context of the Query Editor.

1. On the **SQL Editor** toolbar, in the **Available Databases** list, select [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)].

1. In the Query Editor window, type the following Transact-SQL statements and the `!!DIR` SQLCMD statement:

   ```sql
   SELECT DISTINCT Type FROM Sales.SpecialOffer;
   GO
   !!DIR
   GO
   SELECT ProductCategoryID, Name FROM Production.ProductCategory;
   GO
   ```

1. Press F5 to execute the mixed Transact-SQL and MS-DOS statements section.

   Notice the two SQL result panes from the first and third statements.

1. In the **Results** pane, select the **Messages** tab to see the messages from all three statements:

   - (6 row(s) affected)
   - \<The directory information>
   - (4 row(s) affected)

> [!IMPORTANT]  
> When executed from the command line, the **sqlcmd** utility permits full interaction with the operating system. When you use the Query Editor in **SQLCMD Mode**, you must be careful not to execute interactive statements. The Query Editor cannot respond to operating system prompts.

For more information about how to run SQLCMD, see [**sqlcmd** utility](sqlcmd-utility.md), or take the SQLCMD tutorial.

## Enable SQLCMD scripting by default

To turn on SQLCMD scripting by default, on the **Tools** menu, select **Options**, expand **Query Execution**, and **SQL Server**, select the **General** page and then check the **By default open new queries in SQLCMD Mode** box.

## Write and edit SQLCMD scripts

After enabling scripting mode, you may write SQLCMD commands and Transact-SQL statements. The following rules apply:

- SQLCMD commands must be the first statement on a line.

- Only one SQLCMD command is permitted on each line.

- SQLCMD commands can be preceded by comments or white space.

- SQLCMD commands within comment characters aren't executed.

- Single-line comment characters are two hyphens (`--)` and must appear at the beginning of a line.

- Operating system commands must be preceded by two exclamation points (`!!`). The double-exclamation points command causes the statement that follows the exclamation points to be executed using the `cmd.exe` command processor. The text after `!!` is passed in as a parameter to `cmd.exe`, so the final command line will execute as: `"%SystemRoot%\system32\cmd.exe /c <text after !!>"`.

- To make a clear distinction between SQLCMD commands and Transact-SQL, all SQLCMD commands need to be prefixed with a colon (`:`).

- The `GO` command may be used without preface or preceded by `!!:`

- The Database Engine Query Editor supports environment variables and variables that are defined as part of a SQLCMD script, but doesn't support built-in SQLCMD or **osql** variables. SQLCMD processing by SQL Server Management Studio is case sensitive for variables. For example, PRINT '$(COMPUTERNAME)' produces the correct result, but PRINT '$(ComputerName)' returns an error.

> [!CAUTION]  
> SQL Server Management Studio uses [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)]SqlClient for execution in regular and SQLCMD mode. When run from the command line, SQLCMD uses the OLE DB provider. Because different default options may apply, it is possible to get different behavior while executing the same query in SQL Server Management Studio SQLCMD Mode and in the SQLCMD utility.

## Supported SQLCMD syntax

The Database Engine Query Editor supports the following SQLCMD script keywords:

- `[!!:]GO[count]`
- `!! <command>`
- `:exit(statement)`
- `:Quit`
- `:r <filename>`
- `:setvar <var> <value>`
- `:connect server[\instance] [-l login_timeout] [-U user [-P password]]`
- `:on error [ignore|exit]`
- `:error <filename>|stderr|stdout`
- `:out <filename>|stderr|stdout`

> [!NOTE]  
> Send output to the messages tab for both `:error` and `:out`, `stderr` and `stdout`.

SQLCMD commands not listed above aren't supported in Query Editor. When a script containing SQLCMD keywords aren't supported is executed, the Query Editor sends an "Ignoring command *\<ignored command*>" message to the destination for each unsupported keyword. The script executes successfully, but the unsupported commands are ignored.

> [!CAUTION]  
> Because you are not starting SQLCMD from the command line, there are some limitations when running Query Editor in SQLCMD Mode. You cannot pass in command-line parameters such as variables, and, because the Query Editor cannot respond to operating system prompts, you must be careful not to execute interactive statements.

## Color coding in SQLCMD scripts

With SQLCMD scripting enabled, scripts are color coded. The color coding for Transact-SQL keywords remain the same. SQLCMD commands are presented with a shaded background.

## Example

The following example uses a SQLCMD statement to create an output file called testoutput.txt, executes two Transact-SQL SELECT statements along with one operating system command (to print the current directory). The resultant file contains the message output from the `DIR` statement and the results output from the Transact-SQL statements.

```sql
:out C:\testoutput.txt
SELECT @@VERSION As 'Server Version';
!!DIR
!!:GO
SELECT @@SERVERNAME AS 'Server Name';
GO
```

## Next steps

- [sqlcmd utility](sqlcmd-utility.md)
