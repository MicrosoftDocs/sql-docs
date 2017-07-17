---
title: "Introducing revoscalepy | Microsoft Docs"
ms.custom: ""
ms.date: "06/22/2017"
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

+ Supports multiple compute contexts, remote or local
+ Provides functions equivalent to those in RevoScaleR for data transformation and visualization
+ Provides Python versions of RevoScaleR machine learning algorithms for distributed or parallel processing
+ Improved performance, including use of the Intel math libraries

CTP 2.0 also includes the MicrosoftML packages for R and Python. for more information, see [Using MicrosoftML in SQL Server](../using-the-microsoftml-package.md)

> [!WARNING]
> 
> Python support is a new feature in SQL Server 2017 and is currently supported for preview purposes only.
> 
> The **revoscalepy** module contains only a subset of the functionality provided in the corresponding **RevoScaleR** package for R.

## Versions and supported platforms

The **revoscalepy** module is available only when you install one of the following Microsoft products:

+ Machine Learning Services, in SQL Server 2017 CTP 2.0 or later
+ Microsoft Machine Learning Server 9.1.0. Requires installation using setup for SQL Server 2017 CTP 2.0 or later

## Supported functions and data types

This section lists the Python data types and new Python functions supported in the **revoscalepy** module.

> [!IMPORTANT] 
> This list describes functionality available in the SQL Server 2017 CTP 2.0 release. Some additional functions might be included in subsequent updates, but have not been extensively tested.

### Data types

For a list of mappings between SQL and Python data types, see [Python Libraries and Data Types](python-libraries-and-data-types.md).

### <a name="bkmk_cc"></a>Data sources and compute contexts

You can work with data from an ODBC database, SQL Server, or XDF file by using the data source functions listed in the following table. After defining the data source, you load or transform the data by using an appropriate [ETL function](#bkmk_etl).

|Function|Description|Availability|
|----|----|----|
|RxLocalSeq|Define local compute context|CTP 2.0|
|RxInSqlServer|Create SQL Server compute context|CTP 2.0|
|RxFileData|Constructor for creating external files (RevoScaleR)|CTP 2.0|
|RxXdfData| Create XDF files (RevoScaleR)|CTP 2.0|
|RxOdbcData| Create ODBC connections (RevoScaleR)|CTP 2.0|


If you are new to the idea of remote compute contexts, we recommend that you start by reading about distributed computing works for machine learning in [RevoScaleR](https://msdn.microsoft.com/microsoft-r/scaler-user-guide-introduction).

### <a name="bkmk_algorithms"></a>Machine learning

The following machine learning algorithms are included in SQL Server 2017 CTP 2.0.

| Function| Description|Availability|
| ------ | ------ |------ |
|`rx_btrees_ex` | create and train model (RevoScaleR)|CTP 2.0|
|`rx_dforest_ex` | create and train model (RevoScaleR)|CTP 2.0|
|`rx_dtree_ex` | create and train model (RevoScaleR)|CTP 2.0|
|`rx_lin_mod_ex` | create and train model (RevoScaleR)|CTP 2.0|
|`rx_logit_ex` | create and train model (RevoScaleR)|CTP 2.0|
|`rx_predict_ex` | generate predictions from trained model (RevoScaleR)|CTP 2.0|
|`rx_summary` | print summary of model  (RevoScaleR)|CTP 2.0|


### <a name="bkmk_etl"></a>Data movement and transformation

The following machine learning algorithms are included in SQL Server 2017 CTP 2.0.

| Function| Description|Availability|
| ------ | ------ |------ |
|`rx_data_step_ex` | ETL (RevoScaleR) | CTP 2.0|
|`rx_import_datasource` | ETL (RevoScaleR)|CTP 2.0|

## Examples

You can run code that includes **revoscalepy** functions either locally or in a remote compute context. You can also run Python inside SQL Server by embedding Python code in a stored procedure.

When running locally, you typically run a Python script from the command line, or from a Python development environment, and specify a SQL Server compute context using one of the **revoscalepy** functions. You can use the remote compute context for the entire code, or for individual functions. For example, you might want to offload model training to the server to use the latest data and avoid data movement.

If you want to put a complete Python script inside the stored procedure, [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql), we recommend that you rewrite the code as a single function that has clearly defined inputs and outputs. Inputs and outputs must be **pandas** data frames. When this is done, you can call the stored procedure from any client that supports T-SQL, easily pass SQL queries as inputs, and save the results to SQL tables. For an example, see [In-Database Python Analytics for L Developers](../tutorials/sqldev-in-database-python-for-sql-developers.md).

### Using remote compute contexts

+ [Create a Model using revoscalepy](../tutorials/use-python-revoscalepy-to-create-model.md)

  This example demonstrates how to run Python using an instance of SQL Server as the compute context.

### Using a stored procedure

+ [Run Python using T-SQL](../tutorials/run-python-using-t-sql.md)

  This example demonstrates the basic mechanism of calling Python script that is embedded in a stored procedure.

### Using revoscalepy with MicrosoftML

The Python functions for MicrosoftML are integrated with the compute contexts and data sources that are provided in revoscalepy. Therefore, you could use an MicrosoftML algorithm to define and train a model in Python, and use the revoscalepy functions to execute the Python code either locally or in a SQl Server compute context.

Just import the modules in your Python code, and then reference the individual functions you need.

```Python
from microsoftml.modules.logistic_regression.rx_logistic_regression import rx_logistic_regression
from revoscalepy.functions.RxSummary import rx_summary
from revoscalepy.etl.RxImport import rx_import_datasource
```

### Requirements

To run Python code in SQL Server, you must have installed SQL Server 2017 together with the feature **Machine Learning Services**, and enabled the Python language. Earlier versions of SQL Server do not support Python integration.

> [!NOTE]
> Open source distributions of Python do not support SQL Server compute contexts. However, if you need to publish and consume Python applications from Windows, you can install Microsoft Machine Learning Server without installing SQL Server. For more information, see [Create a Standalone R Server](../r/create-a-standalone-r-server.md)

## Get more help

Complete documentation for these APIs will be available when the product is released. In the meantime, we recommend that you reference the corresponding function in the RevoScaleR or MicrosoftML libraries.

+ [RevoScaleR](https://msdn.microsoft.com/microsoft-r/scaler/scaler).
+ [MicrosoftML](https://msdn.microsoft.com/microsoft-r/microsoftml/microsoftml)

You can get help on any Python function by importing the module, and then calling `help()`. For example, running `help(revoscalepy)` from your Python IDE returns a list of all functions in the revoscalepy module, with their signatures.

If you use Python Tools for Visual Studio, you can use Intellisense to get syntax and argument help. For more information, see [Installing Python Support in Visual Studio](http://docs.microsoft.com/visualstudio/python/installation), and download the extension that matches your version of Visual Studio. You can use Python with Visual Studio 2015 and 2017, or earlier versions.

## See Also

[Python Tutorials](../tutorials/sql-server-python-tutorials.md)