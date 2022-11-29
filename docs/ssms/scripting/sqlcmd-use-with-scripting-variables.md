---
title: "Use sqlcmd with Scripting Variables"
description: Learn how to use scripting variables to make a script that that can be used in multiple scenarios.
ms.custom: seo-lt-2019
ms.date: "10/14/2021"
ms.service: sql
ms.subservice: ssms
ms.reviewer: ""
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "scripts [SQL Server], sqlcmd utility"
  - "variables [SQL Server], scripts"
  - "scripting variables [SQL Server]"
  - "sqlcmd utility, scripts"
  - "setvar command"
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sqlcmd - Use with Scripting Variables
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  Variables that are used in scripts are called scripting variables. Scripting variables enable one script to be used in multiple scenarios. For example, if you want to run one script against multiple servers, instead of modifying the script for each server, you can use a scripting variable for the server name. By changing the server name supplied to the scripting variable, the same script can be executed on different servers.  
  
 Scripting variables can be defined explicitly by using the **setvar** command, or implicitly by using the **sqlcmd -v** option.  
  
 This topic also includes examples defining environmental variables at the Cmd.exe command prompt by using **SET**.  
  
## Setting Scripting Variables by Using the setvar Command  
 The **setvar** command is used to define scripting variables. Variables that are defined by using the **setvar** command are stored internally. Scripting variables should not be confused with environment variables that are defined at the command prompt by using **SET**. If a script references a variable that is not an environment variable or is not defined by using **setvar**, an error message is returned and the execution of the script will stop. For more information, see the **-b** option in [sqlcmd Utility](../../tools/sqlcmd-utility.md).  
  
## Variable Precedence (Low to High)  
 If more than one type of variable has the same name, the variable with the highest precedence is used.  
  
1.  System level environmental variables  
  
2.  User level environmental variables  
  
3.  Command shell (**SET X=Y**) set at command prompt before starting **sqlcmd**  
  
4.  **sqlcmd -v** X=Y  
  
5.  **:Setvar** X Y  
  
> [!NOTE]  
>  To view the environmental variables, in **Control Panel**, open **System**, and then click the **Advanced** tab.  
  
## Implicitly Setting Scripting Variables  
 When you start **sqlcmd** with an option that has a related **sqlcmd** variable, the **sqlcmd** variable is set implicitly to the value that is specified by using the option. In the following example, `sqlcmd` is started with the `-l` option. This implicitly sets the SQLLOGINTIMEOUT variable.  
  
```
c:\> sqlcmd -l 60
```
 
You can also use the **-v** option to set a scripting variable that exists in a script. In the following script (the file name is `testscript.sql`), `ColumnName` is a scripting variable.  
 
```
USE AdventureWorks2012;

SELECT x.$(ColumnName)
FROM Person.Person x
WHERE x.BusinessEntityID < 5;
```

You can then specify the name of the column that you want returned by using the `-v` option:  
 
```
sqlcmd -v ColumnName ="FirstName" -i c:\testscript.sql
```

To return a different column by using the same script, change the value of the `ColumnName` scripting variable.  
  
```
sqlcmd -v ColumnName ="LastName" -i c:\testscript.sql
```

## Guidelines for Scripting Variable Names and Values  
 Consider the following guidelines when you name scripting variables:  
  
-   Variable names must not contain white space characters or quotation marks.  
  
-   Variable names must not have the same form as a variable expression, such as *$(var)*.  
  
-   Scripting variables are case-insensitive  
  
    > [!NOTE]  
    >  If no value is assigned to a **sqlcmd** environment variable, the variable is removed. Using **:setvar VarName** without a value clears the variable.  
  
 Consider the following guidelines when you specify values for scripting variables:  
  
-   Variable values that are defined by using **setvar** or the **-v** option must be enclosed by quotation marks if the string value contains spaces.  
  
-   If quotation marks are part of the variable value, they must be escaped. For example: :`setvar MyVar "spac""e"`.  
  
## Guidelines for Cmd.exe SET Variable Values and Names  
 Variables that are defined by using SET are part of the Cmd.exe environment and can be referenced by **sqlcmd**. Consider the following guidelines:  
  
-   Variable names must not contain white space characters or quotation marks.  
  
-   Variable values may contain spaces or quotation marks.  
  
## sqlcmd Scripting Variables  
 Variables that are defined by **sqlcmd** are known as scripting variables. The following table lists **sqlcmd** scripting variables.  
  
|        Variable         | Related option | R/W |         Default         |
| ----------------------- | -------------- | --- | ----------------------- |
| SQLCMDUSER\*             | -U             | R   | ""                      |
| SQLCMDPASSWORD\*         | -P             | --  | ""                      |
| SQLCMDSERVER\*           | -S             | R   | "DefaultLocalInstance"  |
| SQLCMDWORKSTATION       | -H             | R   | "ComputerName"          |
| SQLCMDDBNAME            | -d             | R   | ""                      |
| SQLCMDLOGINTIMEOUT      | -l             | R/W | "8" (seconds)           |
| SQLCMDSTATTIMEOUT       | -t             | R/W | "0" = wait indefinitely |
| SQLCMDHEADERS           | -h             | R/W | "0"                     |
| SQLCMDCOLSEP            | -s             | R/W | " "                     |
| SQLCMDCOLWIDTH          | -w             | R/W | "0"                     |
| SQLCMDPACKETSIZE        | -a             | R   | "4096"                  |
| SQLCMDERRORLEVEL        | -m             | R/W | "0"                     |
| SQLCMDMAXVARTYPEWIDTH   | -y             | R/W | "256"                   |
| SQLCMDMAXFIXEDTYPEWIDTH | -Y             | R/W | "0" = unlimited         |
| SQLCMDEDITOR            |                | R/W | "edit.com"              |
| SQLCMDINI               |                | R   | ""                      |

SQLCMDUSER, SQLCMDPASSWORD and SQLCMDSERVER are set when **:Connect** is used.  

R indicates the value can only be set one time during program initialization.  
  
R/W indicates that the value can be reset by using the **setvar** command and subsequent commands will use the new value.  
  
## Examples  
  
### A. Using the setvar command in a script  
 Many **sqlcmd** options can be controlled in a script by using the **setvar** command. In the following example, the script `test.sql` is created in which the `SQLCMDLOGINTIMEOUT` variable is set to `60` seconds and another scripting variable, `server`, is set to `testserver`. The following code is in `test.sql`.  

```
:setvar SQLCMDLOGINTIMEOUT 60
:setvar server "testserver"
:connect $(server) -l $(SQLCMDLOGINTIMEOUT)

USE AdventureWorks2012;

SELECT FirstName, LastName
FROM Person.Person;
```

The script is then called by using sqlcmd:

```
sqlcmd -i c:\test.sql
```
  
### B. Using the setvar command interactively  
 The following example shows how to set a scripting variable interactively by using the `setvar` command.  

```
sqlcmd
:setvar  MYDATABASE AdventureWorks2012
USE $(MYDATABASE);
GO
```

 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
Changed database context to 'AdventureWorks2012'
1>
```
  
### C. Using command prompt environment variables within sqlcmd  
 In the following example, four environment variables `are` set and then called from `sqlcmd`.  

```
C:\>SET tablename=Person.Person
C:\>SET col1=FirstName
C:\>SET col2=LastName
C:\>SET title=Ms.
C:\>sqlcmd -d AdventureWorks2012
1> SELECT TOP 5 $(col1) + ' ' + $(col2) AS Name
2> FROM $(tablename)
3> WHERE Title ='$(title)'
4> GO
```
  
### D. Using user-level environment variables within sqlcmd  
 In the following example the user-level environmental variable `%Temp%` is set at the command prompt and passed to the `sqlcmd` input file. To obtain the user-level environment variable, in **Control Panel**, double-click **System**. Click the **Advance** tab, and then click **Environment Variables**.  
  
 The following code is in the input file `c:\testscript.txt`:

```
:OUT $(MyTempDirectory)
USE AdventureWorks2012;

SELECT FirstName
FROM AdventureWorks2012.Person.Person
WHERE BusinessEntityID` `< 5;
```

This following code is entered at the command prompt:

```
C:\ >SET MyTempDirectory=%Temp%\output.txt
C:\ >sqlcmd -i C:\testscript.txt
```

 The following result is sent to the output file C:\Documents and Settings\\<user\>\Local Settings\Temp\output.txt.  

```
Changed database context to 'AdventureWorks2012'.
FirstName
--------------------------------------------------
Gustavo
Catherine
Kim
Humberto

(4 rows affected)
```

### E. Using a startup script  
 A **sqlcmd** startup script is executed when **sqlcmd** is started. The following example sets the environment variable `SQLCMDINI`. This is the contents of `init.sql.`  

```
SET NOCOUNT ON
GO

DECLARE @nt_username nvarchar(128)
SET @nt_username = (SELECT rtrim(convert(nvarchar(128), nt_username))
FROM sys.dm_exec_sessions WHERE spid = @@SPID)
SELECT  @nt_username + ' is connected to ' +
rtrim(CONVERT(nvarchar(20), SERVERPROPERTY('servername'))) +
' (' +`  
rtrim(CONVERT(nvarchar(20), SERVERPROPERTY('productversion'))) +
')'
:setvar SQLCMDMAXFIXEDTYPEWIDTH 100
SET NOCOUNT OFF
GO

:setvar SQLCMDMAXFIXEDTYPEWIDTH
```

 This calls the `init.sql` file when `sqlcmd` is started.  
  
```
c:\> SET sqlcmdini=c:\init.sql
>1 Sqlcmd
```

 This is the output.  

```
>1 < user > is connected to < server > (9.00.2047.00)
```

  
> [!NOTE]  
>  The **-X** option disables the startup script feature.  
  
### F. Variable expansion  
 The following example shows working with data in the form of a **sqlcmd** variable.  

```
USE AdventureWorks2012;
CREATE TABLE AdventureWorks2012.dbo.VariableTest
(
Col1 nvarchar(50)
);
GO
```

 Insert one row into `Col1` of `dbo.VariableTest` that contains the value `$(tablename)`.  

```
INSERT INTO AdventureWorks2012.dbo.VariableTest(Col1)
VALUES('$(tablename)');
GO
```
  
 At the `sqlcmd` prompt, when no variable is set equal to `$(tablename)`, the following statements return the row and also return the message "'tablename' scripting variable not defined." By default, the **sqlcmd** flag `-b` is not set. If `-b` is set, **sqlcmd** will terminate after the "variable not defined" error.
  
```
C:\> sqlcmd
>1 SELECT Col1 FROM dbo.VariableTest WHERE Col1 = '$(tablename)';
>2 GO
>3 SELECT Col1 FROM dbo.VariableTest WHERE Col1 = N'$(tablename)';
>4 GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

```
>1 Col1
>2 ------------------
>3 $(tablename)
>4
>5 (1 rows affected)
```

 Given the variable `MyVar` is set to `$(tablename)`.  

```
>6 :setvar MyVar $(tablename)
```

 These statements return the row and also return the message "'tablename' scripting variable not defined."  

```
>6 SELECT Col1 FROM dbo.VariableTest WHERE Col1 = '$(tablename)';
>7 GO

>1 SELECT Col1 FROM dbo.VariableTest WHERE Col1 = N'$(tablename)';
>2 GO
```

 These statements return the row.  

```
>1 SELECT Col1 FROM dbo.VariableTest WHERE Col1 = '$(MyVar)';
>2 GO
```

```
>1 SELECT Col1 FROM dbo.VariableTest WHERE Col1 = N'$(MyVar)';
>2 GO
```
  
## See Also  
 [Use the sqlcmd Utility](./sqlcmd-use-the-utility.md)   
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)   
 [Command Prompt Utility Reference &#40;Database Engine&#41;](../../tools/command-prompt-utility-reference-database-engine.md)  
  
