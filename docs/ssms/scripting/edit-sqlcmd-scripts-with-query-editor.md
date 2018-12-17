---
title: "Edit SQLCMD Scripts with Query Editor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "scripts [SQL Server], SQLCMD scripts"
  - "SQLCMD scripts"
  - "modifying scripts"
  - "Query Editor [Database Engine], SQLCMD scripts"
  - "scripts [SQL Server], SQL Server Management Studio"
ms.assetid: f77b866d-c330-47c9-9e74-0b8d8dff4b31
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Edit SQLCMD Scripts with Query Editor
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  By using the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] you can write and edit queries as SQLCMD scripts. You use SQLCMD scripts when you have to process Windows System commands and [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in the same script.  
  
## SQLCMD Mode  
 To use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor to write or edit SQLCMD scripts, you must enable the SQLCMD scripting mode. By default, SQLCMD mode is not enabled in the Query Editor. You can enable scripting mode by clicking the **SQLCMD Mode** icon in the toolbar or by selecting **SQLCMD Mode** from the **Query** menu.  
  
> [!NOTE]  
>  Enabling SQLCMD mode turns off IntelliSense and the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor.  
  
 SQLCMD scripts in the Query Editor can use the same features that all [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts use. These features include the following:  
  
-   Color Coding  
  
-   Executing Scripts  
  
-   Source Control  
  
-   Parsing Scripts  
  
-   Showplan  
  
## Enable SQLCMD Scripting in Query Editor  
 To turn SQLCMD scripting on for an active [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window, use the following procedure.  
  
#### To switch a Database Engine Query Editor window to SQLCMD mode  
  
1.  In Object Explorer, right-click the server, and then click **New Query**, to open a new [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window.  
  
2.  On the **Query** menu, click **SQLCMD Mode**.  
  
     The Query Editor executes **sqlcmd** statements in the context of the Query Editor.  
  
3.  On the **SQL Editor** toolbar, in the **Available Databases** list, select [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)].  
  
4.  In the Query Editor window, type the following two [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and the `!!DIR` **sqlcmd** statement:  
  
    ```  
    SELECT DISTINCT Type FROM Sales.SpecialOffer;  
    GO  
    !!DIR  
    GO  
    SELECT ProductCategoryID, Name FROM Production.ProductCategory;  
    GO  
    ```  
  
5.  Press F5 to execute the whole section of mixed [!INCLUDE[tsql](../../includes/tsql-md.md)] and MS-DOS statements.  
  
     Notice the two SQL result panes from the first and third statements.  
  
6.  In the **Results** pane, click the **Messages** tab to see the messages from all three statements:  
  
    -   (6 row(s) affected)  
  
    -   \<The directory information>  
  
    -   (4 row(s) affected)  
  
> [!IMPORTANT]  
>  When executed from the command line, the **sqlcmd** utility permits full interaction with the operating system. When you use the Query Editor in **SQLCMD Mode**, you must be careful not to execute interactive statements. The Query Editor cannot respond to operating system prompts.  
  
 For more information about how to run SQLCMD, see [sqlcmd Utility](../../tools/sqlcmd-utility.md), or take the SQLCMD tutorial.  
  
## Enable SQLCMD Scripting by Default  
 To turn SQLCMD scripting on by default, on the **Tools** menu select **Options**, expand **Query Execution**, and **SQL Server**, click the **General** page, and then check the **By default open new queries in SQLCMD Mode** box.  
  
## Writing and Editing SQLCMD Scripts  
 After enabling scripting mode you may write SQLCMD commands and [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. The following rules apply:  
  
-   SQLCMD commands must be the first statement on a line.  
  
-   Only one SQLCMD command is permitted on each line.  
  
-   SQLCMD commands can be preceded by comments or white space.  
  
-   SQLCMD commands within comment characters are not executed.  
  
-   Single line comment characters are two hyphens (`--)` and must appear at the beginning of a line.  
  
-   Operating system commands must be preceded by two exclamation points (`!!`). The double-exclamation points command causes the statement that follows the exclamation points to be executed using the `cmd.exe` command processor. The text after `!!` is passed in as a parameter to `cmd.exe`, so the final command line will execute as: `"%SystemRoot%\system32\cmd.exe /c <text after !!>"`.  
  
-   To make a clear distinction between SQLCMD commands and [!INCLUDE[tsql](../../includes/tsql-md.md)], all SQLCMD commands, need to be prefixed with a colon (`:`).  
  
-   The `GO` command may be used without preface, or preceded by `!!:`  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor supports environment variables and variables that are defined as part of a SQLCMD script, but does not support built-in SQLCMD or **osql** variables. SQLCMD processing by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is case sensitive for variables. For example, PRINT '$(COMPUTERNAME)' produces the correct result, but PRINT '$(ComputerName)' returns an error.  
  
> [!CAUTION]
>  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]SqlClient for execution in regular and SQLCMD mode. When run from the command line, SQLCMD uses the OLE DB provider. Because different default options may apply, it is possible to get different behavior while executing the same query in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] SQLCMD Mode, and in the SQLCMD utility.  
  
## Supported SQLCMD Syntax  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor supports the following SQLCMD script keywords:  
  
 `[!!:]GO[count]`  
  
 `!! <command>`  
  
 `:exit(statement)`  
  
 `:Quit`  
  
 `:r <filename>`  
  
 `:setvar <var> <value>`  
  
 `:connect server[\instance] [-l login_timeout] [-U user [-P password]]`  
  
 `:on error [ignore|exit]`  
  
 `:error <filename>|stderr|stdout`  
  
 `:out <filename>|stderr|stdout`  
  
> [!NOTE]  
>  For both `:error` and `:out`, `stderr` and `stdout` send output to the messages tab.  
  
 SQLCMD commands not listed above are not supported in Query Editor. When a script containing SQLCMD keywords that are not supported is executed, the Query Editor will send an "Ignoring command *\<ignored command*>" message to the destination for each unsupported keyword. The script will execute successfully, but the unsupported commands will be ignored.  
  
> [!CAUTION]  
>  Because you are not starting SQLCMD from the command line, there are some limitations when running Query Editor in SQLCMD Mode. You cannot pass in command-line parameters such as variables, and, because the Query Editor does not have the ability to respond to operating system prompts, you must be careful not to execute interactive statements.  
  
## Color Coding in SQLCMD Scripts  
 With SQLCMD scripting enabled, scripts will be color coded. The color coding for [!INCLUDE[tsql](../../includes/tsql-md.md)] keywords will remain the same. SQLCMD commands are presented with a shaded background.  
  
## Example  
 The following example uses a **sqlcmd** statement to create an output file called testoutput.txt, executes two [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statements along with one operating system command (to print out the current directory). The resultant file contains the message output from the `DIR` statement, followed by the results output from the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
```  
:out C:\testoutput.txt  
SELECT @@VERSION As 'Server Version'  
!!DIR  
!!:GO  
SELECT @@SERVERNAME AS 'Server Name'  
GO  
```  
  
## See Also  
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)  
  
  
