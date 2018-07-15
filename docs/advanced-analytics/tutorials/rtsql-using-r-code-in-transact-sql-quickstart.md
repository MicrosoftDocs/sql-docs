---
title: Quickstart for a "Hello World" basic R code execution in T-SQL (SQL Server Machine Learning) | Microsoft Docs
description: In this quickstart for R script in SQL Server, learn the basics of the sp_execute_external_script system stored procedure with a hello-world exercise.
ms.prod: sql
ms.technology: machine-learning

ms.date: 07/15/2018  
ms.topic: quickstart
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Quickstart: "Hello world" R script in SQL Server 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server include R langugage feature support for in-database analytics on resident SQL Server data. You can use open-source R functions, third-party packages, and built-in Microsoft R packages for predictive analystics at scale.

In this quickstart, you learn the basic mechanics of calling an R script from T-SQL. 

R script execution is through stored procedures. You can either use [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) and pass R script in as an input parameter, or wrap R script in a [custom stored procedure](sqldev-in-database-r-for-sql-developers.md). 

This quickstart is a "hello world" introduction to the **sp_execute_external_script** system stored procedure.

## Prerequisites

This exercise requires access to an instance of SQL Server with one of the following already installed:

+ [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md), with the R language installed
+ [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)

  Your SQL Server instance can be in an Azure virtual machine or on-premises. Just be aware that the external scripting feature is disabled by default, so you might need to [enable external scripting](../install/sql-machine-learning-services-windows-install.md#bkmk_enableFeature) and verify that **SQL Server Launchpad service** is running before you start.

+ A tool for running SQL queries. You can use any application that can connect to a SQL Server database and run T-SQL code. SQL professionals can use SQL Server Management Studio (SSMS) or Visual Studio.

For this tutorial, to show how easy it is to run R inside SQL Server, we've used the new **mssql extension for Visual Studio Code**. VS Code is a free development environment that can run on Linux, macOS, or Windows. The **mssql** extension is a lightweight extension for running T-SQL queries. To install it, see this article: [Use the mssql extension for Visual Studio Code](https://docs.microsoft.com/sql/linux/sql-server-linux-develop-use-vscode).

## Connect to a database and run a Hello World test script

1. In Visual Studio Code, create a new text file and name it BasicRSQL.sql.

2. While this file is open, press CTRL+SHIFT+P (COMMAND + P on a macOS), type **sql** to list the SQL commands, and select **CONNECT**. Visual Studio Code prompts you to create a profile to use when connecting to a specific database. This is optional, but makes it easier to switch between databases and logins.
    + Choose a server or instance where R in SQL Server has been installed.
    + Use an account that has permissions to create a new database, run SELECT statements, and view table definitions.

2. If the connection is successful, you should be able to see the server and database name in the status bar, together with your current credentials. If the connection failed, check whether the computer name and server name are correct.

3. Paste in this statement and run it.

    ```sql
    EXEC sp_execute_external_script
      @language =N'R',
      @script=N'OutputDataSet<-InputDataSet',
      @input_data_1 =N'SELECT 1 AS hello'
      WITH RESULT SETS (([Hello World] int));
    GO
    ```

Inputs to this stored procedure include:

+ *@language* parameter defines the language extension to call, in this case, R.
+ *@script* parameter, you define the commands to pass to the R runtime. Your entire R script must be enclosed in this argument, as Unicode text. You could also add the text to a variable of type **nvarchar** and then call the variable.
+ *@input_data_1* is data returned by the query is passed to the R runtime, which returns the data to SQL Server as a data frame.
+ WITH RESULT SETS clause defines the schema of the returned data table for SQL Server, adding "Hello World" as the column name, **int** for the data type.

> [!TIP]
> In Visual Studio Code, you can highlight the code you want to run and press CTRL+SHIFT+E. If this is too hard to remember, you can change it! See [Customize the shortcut key bindings](https://github.com/Microsoft/vscode-mssql/wiki/customize-shortcuts).
> 
> ![rsql-basictut_hello1code](media/rsql-basictut-hello1code.PNG)
> 

**Results**

![rsql_basictut_hello1](media/rsql-basictut-hello1.PNG)

If you get any errors from this query, rule out any installation issues. Post-install configuration is required to enable use of external code libraries. See [Install SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) or [Install SQL Server 2016 R Services](../install/sql-r-services-windows-install.md).

Likewise, make sure that the Launchpad service is running. Depending on your environment, you might need to enable the R worker accounts to connect to SQL Server, install additional network libraries, enable remote code execution, or restart the instance after everything is configured. See
[R Services Installation and Upgrade FAQ](../r/upgrade-and-installation-faq-sql-server-r-services.md)

+ To get Visual Studio Code, see [Download and install Visual Studio Code](https://code.visualstudio.com/Download).

## Next steps

Now that you have confirmed your instance is ready to work with R, take a closer look at structuring inputs and outputs.

> [!div class="nextstepaction"]
> [Quickstart: Handle inputs and outputs](rtsql-working-with-inputs-and-outputs.md)
