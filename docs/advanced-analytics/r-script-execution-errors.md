---
title: R scripting errors and troubleshooting - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/31/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# R scripting errors in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article documents several scriptin gerrors when running R code in SQL Server. The list is not comprehensive. There are many packages, and errors can vary between versions of the same package. We recommend posting script errors on the [Machine Learning Server forum](https://social.msdn.microsoft.com/Forums/en-US/home?category=MicrosoftR), which supports the machine learning components used in R Services (In-Database), Microsoft R Client, and Microsoft R Server.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services


## Valid script fails in T-SQL or in stored procedures

Before wrapping your R code in a stored procedure, it is a good idea to run your R code in an external IDE, or in one of the R tools such as RTerm or RGui. By using these methods, you can test and debug the code by using the detailed error messages that are returned by R.

However, sometimes code that works perfectly in an external IDE or utility might fail to run in a stored procedure or in a SQL Server compute context. If this happens, there are a variety of issues to look for before you can assume that the package doesn't work in SQL Server.

1. Check to see whether Launchpad is running.

2. Review messages to see whether either the input data or output data contains columns with incompatible or unsupported data types. For example, queries on a SQL database often return GUIDs or RowGUIDs, both of which are unsupported. For more information, see [R libraries and data types](r/r-libraries-and-data-types.md).

3. Review the help pages for individual R functions to determine whether all parameters are supported for the SQL Server compute context. For ScaleR help, use the inline R help commands, or see [Package Reference](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler).

If the R runtime is functioning but your script returns errors, we recommend that you try debugging the script in a dedicated R development environment, such as  R Tools for Visual Studio.

We also recommend that you review and slightly rewrite the script to correct any problems with data types that might arise when you move data between R and the database engine. For more information, see [R libraries and data types](r/r-libraries-and-data-types.md).

Additionally, you can use the sqlrutils package to bundle your R script in a format that is more easily consumed as a stored procedure. For more information, see:
* [sqlrutils package](r/ref-r-sqlrutils.md)
* [Create a stored procedure by using sqlrutils](r/how-to-create-a-stored-procedure-using-sqlrutils.md)

## Script returns inconsistent results

R scripts can return different values in a SQL Server context, for several reasons:

- Implicit type conversion is automatically performed on some data types, when the data is passed between SQL Server and R. For more information, see [R libraries and data types](r/r-libraries-and-data-types.md).

- Determine whether bitness is a factor. For example, there are often differences in the results of math operations for 32-bit and 64-bit floating point libraries.

- Determine whether NaNs were produced in any operation. This can invalidate results.

- Small differences can be amplified when you take a reciprocal of a number near zero.

- Accumulated rounding errors can cause such things as values that are less than zero instead of zero.

## Implied authentication for remote execution via ODBC

If you connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] computer to run R commands by using the **RevoScaleR** functions, you might get an error when you use ODBC calls that write data to the server. This error happens only when you're using Windows authentication.

The reason is that the worker accounts that are created for R Services do not have permission to connect to the server. Therefore, ODBC calls cannot be executed on your behalf. The problem does not occur with SQL logins because, with SQL logins, the credentials are passed explicitly from the R client to the SQL Server instance and then to ODBC. However, using SQL logins is also less secure than using Windows authentication.

To enable your Windows credentials to be passed securely from a script that's initiated remotely, SQL Server must emulate your credentials. This process is termed _implied authentication_. To make this work, the worker accounts that run R or Python scripts on the SQL Server computer must have the correct permissions.

1. Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] as an administrator on the instance where you want to run R code.

2. Run the following script. Be sure to edit the user group name, if you changed the default, and the computer and instance names.

    ```sql
    USE [master]
    GO
    
    CREATE LOGIN [computername\\SQLRUserGroup] FROM WINDOWS WITH
    DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[language]
    GO
    ```

## Avoid clearing the workspace while you're running R in a SQL compute context

Although clearing the workspace is common when you work in the R console, it can have unintended consequences in a SQL compute context.

`revoScriptConnection` is an object in the R workspace that contains information about an R session that's called from SQL Server. However, if your R code includes a command to clear the workspace (such as `rm(list=ls())`), all information about the session and other objects in the R workspace is cleared as well.

As a workaround, avoid indiscriminate clearing of variables and other objects while you're running R in SQL Server. You can delete specific variables by using the **remove** function:

```R
remove('name1', 'name2', ...)
```

If there are multiple variables to delete, we suggest that you save the names of temporary variables to a list and then perform periodic garbage collections on the list.



## Next steps

[Machine Learning Services troubleshooting and known issues](machine-learning-troubleshooting-faq.md)

[Data collection for troubleshooting machine learning](data-collection-ml-troubleshooting-process.md)

[Upgrade and installation FAQ](r/upgrade-and-installation-faq-sql-server-r-services.md)

[Troubleshoot database engine connections](../database-engine/configure-windows/troubleshoot-connecting-to-the-sql-server-database-engine.md)
