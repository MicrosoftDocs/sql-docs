---
title: revoscalepy Python package - SQL Server Machine Learning Services
description: Introduction to the revoscalepy module in SQL Server 2017 Machine Learning Services with Python.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/12/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# revoscalepy (Python module in SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

**revoscalepy** is a Python35-compatible module from Microsoft that supports distributed computing, remote compute contexts, and high-performance data science algorithms. It is based on the **RevoScaleR** package for R (distributed with Microsoft R Server and SQL Server R Services), offering similar functionality:

+ Local and remote compute contexts on systems having the same version of **revoscalepy**
+ Data transformation and visualization functions
+ Data science functions, scalable through distributed or parallel processing
+ Improved performance, including use of the Intel math libraries

Data sources and compute contexts that you create in **revoscalepy** can also be used in machine learning algorithms. For an introduction to these algorithms, see [microsoftml Python module in SQL Server](ref-py-microsoftml.md).

## Full reference documentation

The **revoscalepy** library is distributed in multiple Microsoft products, but usage is the same whether you get the library in SQL Server or another product. Because the functions are the same, [documentation for individual revoscalepy functions](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) is published to just one location under the [Python reference](https://docs.microsoft.com/machine-learning-server/python-reference/introducing-python-package-reference) for Microsoft Machine Learning Server. Should any product-specific behaviors exist, discrepancies will be noted in the function help page.

## Versions and platforms

The **revoscalepy** module is based on Python 3.5 and available only when you install one of the following Microsoft products or downloads:

+ [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)
+ [Microsoft Machine Learning Server 9.2.0 or later](https://docs.microsoft.com/machine-learning-server/)
+ [Python client libraries for a data science client](setup-python-client-tools-sql.md)

> [!NOTE]
> Full product release versions are Windows-only, starting with SQL Server 2017. Linux support for **revoscalepy** is new in [SQL Server 2019 Preview](../../linux/sql-server-linux-setup-machine-learning.md).

## Functions by category

This section lists the functions by category to give you an idea of how each one is used. You can also use the [table of contents](https://docs.microsoft.com/machine-learning-server/python-reference/introducing-python-package-reference) to find functions in alphabetical order.

## 1-Data source and compute

**revoscalepy** includes functions for creating data sources and setting the location, or *compute context*, of where computations are performed. Functions relevant to SQL Server scenarios are listed in the table below.

SQL Server and Python use different data types in some cases. For a list of mappings between SQL and Python data types, see [Python-to-SQL data types](python-libraries-and-data-types.md).

| Function| Description|
| ------- | ---------- |
| [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rxinsqlserver) |  Create a SQL Server compute context object to push computations to a remote instance. Several **revoscalepy** functions take compute context as an argument. For a context-switch example, see [Create a model using revoscalepy](../tutorials/use-python-revoscalepy-to-create-model.md).|
| [RxSqlServerData](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rxsqlserverdata) | Create a data object based on a SQL Server query or table. |
| [RxOdbcData](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rxodbcdata)| Create a data source based on an ODBC connection. |
| [RxXdfData](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rxxdfdata) | Create a data source based on a local XDF file. XDF files are often used to offload in-memory data to disk. An XDF file can be useful when working with more data than can be transferred from the database in one batch, or more data than can fit in memory. For example, if you regularly move large amounts of data from a database to a local workstation, rather than query the database repeatedly for each R operation, you can use the XDF file as a kind of cache to save the data locally and then work with it in your R workspace. |

> [!TIP]
> If you are new to the idea of data sources or compute contexts, we recommend that you start with [distributed computing](https://docs.microsoft.com/machine-learning-server/r/how-to-revoscaler-distributed-computing) in the Microsoft Machine Learning Server documentation.

## 2-Data manipulation (ETL)

| Function | Description |
|----------|-------------|
|[rx_import](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-import) | Import data into an .xdf file or data frame.|
|[rx_data_step](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-data-step) | Transform data from an input data set to an output data set.|

<a name="bkmk_algorithms"></a>

## 3-Training and summarization

| Function| Description|
| ------- | ---------- |
|[rx_btrees](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-btrees) | Fit stochastic gradient boosted decision trees|
|[rx_dforest](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-dforest) | Fit classification and regression decision forests|
|[rx_dtree](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-dtree) | Fit classification and regression trees |
|[rx_lin_mod](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-lin-mod) | Create a linear regression model|
|[rx_logit](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-logit) | Create a logistic regression model|
|[rx_summary](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-summary) | Produce univariate summaries of objects in revoscalepy.|

You should also review the functions in [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) for additional approaches.

<a name="ml-scoring"></a>

## 4-Scoring functions

| Function| Description|
| ------- | ---------- |
| [rx_predict](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-predict) | Generate predictions from a trained model|) | Generates predictions from a trained model and can be used for realtime scoring. |
|[rx_predict_default](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-predict-default) | Compute predicted values and residuals using rx_lin_mod and rx_logit objects. |
|[rx_predict_rx_dforest](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-predict-rx-dforest) | Calculate predicted or fitted values for a data set from an rx_dforest or rx_btrees object. |
|[rx_predict_rx_dtree](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-predict-rx-dtree) | Calculate predicted or fitted values for a data set from an rx_dtree object. |

## How to work with revoscalepy

Functions in **revoscalepy** are callable in Python code encapsulated in stored procedures. Most developers build **revoscalepy** solutions locally, and then migrate finished Python code to stored procedures as a deployment exercise.

When running locally, you typically run a Python script from the command line, or from a Python development environment, and specify a SQL Server compute context using one of the **revoscalepy** functions. You can use the remote compute context for the entire code, or for individual functions. For example, you might want to offload model training to the server to use the latest data and avoid data movement.

When you are ready to encapsulate Python script inside a stored procedure, [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql), we recommend rewriting the code as a single function that has clearly defined inputs and outputs. 

Inputs and outputs must be **pandas** data frames. When this is done, you can call the stored procedure from any client that supports T-SQL, easily pass SQL queries as inputs, and save the results to SQL tables. For an example, see [Learn in-database Python analytics for SQL developers](../tutorials/sqldev-in-database-python-for-sql-developers.md).

### Using revoscalepy with microsoftml

The Python functions for [microsoftml](ref-py-microsoftml.md) are integrated with the compute contexts and data sources that are provided in revoscalepy. When calling functions from microsoftml, for example when defining and training a model, use the revoscalepy functions to execute the Python code either locally or in a SQl Server remote compute context.

The following example shows the syntax for importing modules in your Python code. You  can then reference the individual functions you need.

```python
from microsoftml.modules.logistic_regression.rx_logistic_regression import rx_logistic_regression
from revoscalepy.functions.RxSummary import rx_summary
from revoscalepy.etl.RxImport import rx_import_datasource
```

## See also

+ [Python tutorials](../tutorials/sql-server-python-tutorials.md)
+ [Tutorial: Embed Python code in T-SQL](../tutorials/run-python-using-t-sql.md)
+ [Python Reference (Microsoft Machine Learning Server)](https://docs.microsoft.com/machine-learning-server/python-reference/introducing-python-package-reference)
