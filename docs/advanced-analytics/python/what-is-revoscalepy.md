---
title: "Introducing revoscalepy | Microsoft Docs"
ms.custom: ""
ms.date: "08/20/2017"
ms.prod: "sql-server-2017"
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

MicrosoftML packages are also proided for both R and Python. For more information, see [Using MicrosoftML in SQL Server](../using-the-microsoftml-package.md)

> [!WARNING]
> 
> Python support is a new feature in SQL Server 2017 and is currently supported for preview purposes only.

## Versions and supported platforms

The **revoscalepy** module is available only when you install one of the following Microsoft products:

+ Machine Learning Services, in SQL Server 2017 CTP 2.0 or later
+ Microsoft Machine Learning Server 9.1.0. Requires installation using setup for SQL Server 2017 CTP 2.0 or later

## Supported functions and data types

This section provides an overview of the Python data types and new Python functions supported in the **revoscalepy** module, beginning with SQL Server 2017 CTP 2.0 release. 

For the latest list of functions in the Python libraries released to date, see these links:

+ [revoscalepy for Python](https://docs.microsoft.com/r-server/python-reference/revoscalepy/revoscalepy-package)

+ [microsoftml for Python](https://docs.microsoft.com/r-server/python-reference/microsoftml/microsoftml-package)

### Data types, data sources, and compute contexts

SQL Server and Python use different data types in some cases. For a list of mappings between SQL and Python data types, see [Python libraries and data Types](python-libraries-and-data-types.md).

Data sources supported for machine learning with Python in SQL Server includes ODBC data sources, SQL Server database, and local files, including XDF files.

You create the data source object by using functions listed in the following table. After defining the data source, you load or transform the data by using an appropriate [ETL function](#bkmk_etl).

> [!IMPORTANT]
> Many function names have changed since the initial release of Python in CTP 2.0.

**SQL Server data**

+ Use [RxSqlServerData](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rxsqlserverdata) to define a data source from a query or table
+ Use [RxInSqlServer](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rxinsqlserver) to create a SQL Server compute context
+ Use [RxOdbcData](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rxodbcdata) to create a data source from an ODBC connection

**revoscalepy** also supports the [XDF data source](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rxxdfdata), used for moving data between memory and other data sources.

> [!TIP]
> If you are new to the idea of data sources or compute contexts, we recommend that you start by reading about distributed computing works for machine learning in [RevoScaleR](https://msdn.microsoft.com/microsoft-r/scaler-user-guide-introduction).


### <a name="bkmk_algorithms"></a>Machine learning and summary functions

The following machine learning algorithms and summary functions from RevoScaleR are included in SQL Server 2017, beginning with CTP 2.0.

| Function| Description|Notes|
| ------ | ------ |------ |
|`rx_btrees` | Fit stochastic gradient boosted decision trees|`rx_btrees_ex` in CTP 2.0|
|`rx_dforest` | Fit classification and regression decision forests|`rx_dforest_ex` in CTP 2.0|
|`rx_dtree` | Fit classification and regression trees |`rx_dtree_ex` in CTP 2.0|
|`rx_lin_mod` | Create a linear model|`rx_lin_mod_ex` in CTP 2.0|
|`rx_logit` | Create a logistic regression model|`rx_logit_ex` in CTP 2.0|
|`rx_predict` | Generate predictions from a trained model|`rx_predict_ex` in CTP 2.0|
|`rx_summary` | Generate a summary of the model||

New machine learning algorithms are also provided by the Python version of [MicrosoftML](https://docs.microsoft.com/en-us/r-server/python-reference/microsoftml/microsoftml-package):

| Function| Description|
| ------ | ------ |
|`rx_fast_forest` |Create a decision forest model|
|`rx_fast_linear` | Linear regression with stochastic dual coordinate ascent|
|`rx_fast_trees` | Create a boosted tree model |
|`rx_logistic_regression` | Create a logistic regression model|
|`rx_neural_network` | Create a customizable neural network model |
|`rx_oneclass_svm` | Creates a SVM model for imabalnced datsets, for use in anomaly detection|

> [!TIP]
> Many of these algorithms are already provided as modules in Azure Machine Learning.

MicrosoftML for Python also includes a variety of transformations and helper functions, such as:

+ `rx_predict` generates predictions from a trained model and can be used for realtime scoring
+ image featurization functions
+ functions for text processing and sentiment extraction

For details, see [Introduction to MicrosoftML](https://docs.microsoft.com/r-server/r/concept-what-is-the-microsoftml-package)


> [!NOTE]
> The Python community uses coding conventions that might be different than what you're used to, including all lowercase letters and underscores rather than camel casing for parameter names. Also, maybe you've noticed that the **revoscalepy** library is always lowercase. That's right! Another Python convention.
> 
> Check out the tips on the Python reference documentation for Microsoft R: [Python function help][Python function help](https://docs.microsoft.com/r-server/python-reference/introducing-python-package-reference)

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

If you use Python Tools for Visual Studio, you can use IntelliSense to get syntax and argument help. For more information, see [Python support in Visual Studio](http://docs.microsoft.com/visualstudio/python/installation), and download the extension that matches your version of Visual Studio. You can use Python with Visual Studio 2015 and 2017, or earlier versions.

## See Also

[Python tutorials](../tutorials/sql-server-python-tutorials.md)