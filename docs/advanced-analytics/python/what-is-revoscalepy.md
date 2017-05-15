---
title: "Introducing revoscalepy | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Introducing revoscalepy

**revoscalepy** is a new library provided by Microsoft to support distributed computing, remote compute contexts, and high-performance algorithms for Python.

It is based on the **RevoScaleR** package for R, which was provided in Microsoft R Server and SQL Server R Services, and aims to provide the same functionality:

+ Support multiple compute contexts, remote or local
+ Provide functions equivalent to those in RevoScaleR for data transformation and visualization
+ Provide Python versions of RevoScaleR machine learning algorithms for distributed or parallel processing
+ Improved performance and use of the Intel math libraries

> [!WARNING]
> 
> Python support is a new feature in SQL Server 2017 and is supported for preview only.
> 
> The **revoscalepy** module contains only a subset of the functionality provided in the corresponding **RevoScaleR** package for R.

## Versions and Supported Platforms

The **revoscalepy** module is available only when you install one of the following Microsoft products:

+ Machine Learning Services, in SQL Server 2017 CTP 2.0
+ Microsoft Machine Learning Server 9.1.0, using SQL Server 2017 CTP 2.0 setup

## Supported Functions and Data Types

This sections lists the Python data types and new Python functions supported in the **revoscalepy** module for the SQL Server 2017 CTP 2.0 release.

### Data types

For a list of mappings between SQL and Python data types, see [Python Libraries and Data Types](python-libraries-and-data-types.md).

### Data sources and compute contexts

You can get data from any ODBC database, SQL Server, or  XDF file, using the data soure functions listed in the following table.

Remote compute contexts supported for this release are local, or in SQL Server 2017.

### Functions

The following functions are included in SQL Server 2017 CTP 2.0.

| Function name | Category|
| ------ | ------ |
|`rx_btrees_ex` | analytic|
|`rx_dforest_ex` | analytic |
|`rx_dtree_ex` | analytic|
|`rx_lin_mod_ex` | analytic|
|`rx_logit_ex` | analytic |
|`rx_predict_ex` | analytic|
|`rx_summary` | analytic|
|`RxInSqlServer` | compute context|
|`RxLocalSeq`|compute context|
|`RxFileData` | data source|
|`RxOdbcData` | data source|
|`RxSqlServerData` | data source|
|`RxXdfData` | data source|
|`rx_data_step_ex` | ETL |
|`rx_import_datasource` | ETL|

**Need more function help?**

If you are new to the idea of remote compute contexts, we recommend that you start by reading about [RevoScaleR](https://msdn.microsoft.com/microsoft-r/scaler-user-guide-introduction) and how distributed computing works for machine learning.

+ View RevoScaleR help

  Locate the corresponding function in R help or in the MSDN library for [RevoScaleR](https://msdn.microsoft.com/microsoft-r/scaler/scaler).

  > [!NOTE]
  > All functions are pre-release versions. Some functions have not been fully tested, and some functions do not have the same level of functionality as the corresponding function in RevoScaleR.
  
+ Use Python help features

  You can get help on any Python function by importing the module, and then calling `help()`. For example, running `help(revoscalepy)` from your Python IDE returns a list of all included functions with their signatures.

+ IntelliSense in Visual Studio

  If you use Python Tools for Visual Studio, you can use Intellisense to get syntax and argument help.

  For more information, see [Installing Python Support in Visual Studio](http://docs.microsoft.com/visualstudio/python/installation), and download the extension that matches your version of Visual Studio. You can use Python with Visual Studio 2015 and 2017, or earlier versions. 


## Examples

You can run code that includes **revoscalepy** functions in two scenarios:

+ Execute a Python script from the command line, or from a Python development environment, and call a remote compute context.
+ Embed Python code in the stored procedure, [sp_execute_external_script].(https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql), and call the stored procedure from any client that supports T-SQL.

> [!NOTE]
> To run Python code in SQL Server, you must have installed SQL Server 2017 together with the feature **Machine Learning Services**, and enabled the Python language. Other versions of SQL Server do not support Python integration.
>
> Open source distributions of Python do not support SQL Server compute contexts. However, you can install Microsoft Machine Learning Server to publish and consume Python applications from Windows without installing SQL Server. For more information, see [Create a Standalone R Server](../r/create-a-standalone-r-server.md)

### Using remote compute contexts

This example demonstrates how to run Python using an instance of SQL Server as the compute context.

[Create a Model using revoscalepy](../tutorials/use-python-revoscalepy-to-create-model.md)

### Using T-SQL

This example demonstrates how to run Python using Python script embedded in a stored procedure.

[Run Python using T-SQL](../tutorials/run-python-using-t-sql.md)


