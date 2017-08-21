---
title: "Using R code in Transact-SQL (R in SQL quickstart) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
ms.assetid: 4e6fe30d-a105-4d5b-bc05-5e5204753847
caps.latest.revision: 36
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Using R code in Transact-SQL (R in SQL quickstart)

This tutorial walks you through the basic mechanics of calling an R script from a T-SQL stored procedure.

**What you'll learn**

+ How to embed R in a T-SQL function
+ Some tips for working with R and SQL data types and data objects
+ How to create a simple model, and save it to SQL Server
+ How to create predictions and an R plot using the model

**Estimated time**

30 minutes, not including setup

## Prerequisites

You must have access to an instance of SQL Server with one of the following already installed:

+ SQL Server 2017 Machine Learning Services, with the R language installed
+ SQL Server 2016 R Services

Your SQL Server instance can be in an Azure virtual machine or on-premises. Just be aware that the external scripting feature is disabled by default, so you might need to perform some additional steps to get it working.

To run SQL queries that include R script, you can use any other application that can connect to a database and run ad hoc T-SQL code. If you are a SQL pro,  you can use SQL Server Management Studio (SSMS) or Visual Studio.

For this tutorial, to show how easy it is to run R inside SQL Server, we'll use the new **mssql extension for Visual Studio Code**. VS Code is a free development environment that can run on Linux, macOS, or Windows. The **mssql*** extension is a lightweight extension for running SLq queries. To install it, see this article: [Use the mssql extension for Visual Studio Code](https://docs.microsoft.com/sql/linux/sql-server-linux-develop-use-vscode).

## Connect to a database and run a Hello World test script

1. In Visual Studio Code, create a new text file and name it BasicRSQL.sql.
2. While this file is open, press CTRL+SHIFT+P (COMMAND + P on a macOS), type **sql** to list the SQL commands, and select **CONNECT**. Visual Studio Code will prompt you to create a profile to use when connecting to a specific database. This is optional, but will make it easier to switch between databases and logins.
    + Choose a server or instance where R in SQL Server has been installed.
    + Use an account that has permissions to create a new database, run SELECT statements, and view table definitions.
2. If the connection is successful, you should be able to see the server and database name in the status bar, together with your current credentials. If the connection failed, check whether the computer name and server name are correct.
3. Paste in this statement and run it.

    ```sql
    EXEC sp_execute_external_script
      @language =N'R',
      @script=N'OutputDataSet<-InputDataSet',
      @input_data_1 =N'SELECT 1 AS hello'
      WITH RESULT SETS (([hello] int not null));
    GO
    ```

    In Visual Studio Code, you can highlight the code you want to run and press CTRL+SHIFT+E. If this is too hard to remember, you can change it! See [Customize the shortcut key bindings](https://github.com/Microsoft/vscode-mssql/wiki/customize-shortcuts).

    ![rsql-basictut_hello1code](media/rsql-basictut-hello1code.PNG)

**Results**

![rsql_basictut_hello1](media/rsql-basictut-hello1.PNG)

## Troubleshooting

+ If you get any errors from this query, installation might be incomplete. After adding the feature using the SQL Server setup wizard, you must take some additional steps to enable use of external code libraries.  See [Set up SQL Server R Services](../r/set-up-sql-server-r-services-in-database.md).

+ Make sure that the Launchpad service is running. Depending on your environment, you might need to enable the R worker accounts to connect to SQL Server, install additional network libraries, enable remote code execution, or restart the instance after everything is configured. See
[R Services Installation and Upgrade FAQ](../r/upgrade-and-installation-faq-sql-server-r-services.md)

+ To get Visual Studio Code, see [Download and install Visual Studio Code](https://code.visualstudio.com/Download).

## Next lesson

Now that your instance is ready to work with R, let's get started.

Lesson 1: [Working with inputs and outputs](rtsql-working-with-inputs-and-outputs.md)

Lesson 2: [R and SQL data types and data objects](rtsql-r-and-sql-data-types-and-data-objects.md)

Lesson 3: [Using R functions with SQL Server data](rtsql-using-r-functions-with-sql-server-data.md)

Lesson 4:  [Create a predictive model](rtsql-create-a-predictive-model-r.md)

Lesson 5:  [Predict and plot from model](rtsql-predict-and-plot-from-model.md)
