---
title: "Use Python with revoscalepy to Create a Model| Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 2
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Use Python with revoscalepy to Create a Model

This example demonstrates how you can create a logistic regression model in SQL Server, using an algorithm from the **revoscalepy** package.

The **revoscalepy** package for Python contains objects, transformations, and algorithms similar to those provided for the **RevoScaleR** package for the R language. With this library, you can create a compute context, move data between compute contexts, transform data, and train predictive models using popular algorithms such as logistic and linear regression, decision trees, and more.

For more information, see [What is revoscalepy?](../python/what-is-revoscalepy.md)

## Prerequisites

> [!IMPORTANT]
> To run Python code in SQL Server, you must have installed SQL Server 2017 CTP 2.0 or later, and you must install and enable the feature, **Machine Learning Services** with Python. Other versions of SQL Server do not support Python integration.

## Run the Sample Code

This code performs the following steps:

1. Imports the required libraries and functions
2. Creates a connection to SQL Server, and creates data source objects for working with the data
3. Modifies the data so that it can be used by the rxLinMod algorithm
4. Calls rxLinMod and defines the formula used to fit the model
5. Generates a set of predictions based on the original data set
6. Creates a summary based on the predicted values

All operations are performed using an instance of SQL Server as the compute context.

In general, the process of calling Python in a remote compute context is similar to the way you use R in a remote compute context. You execute the sample as a Python script from the command line, or by using a Python development environment that includes the Python integration components provided in this release. In your code, you create an use a compute context object to indicate where you want specific computations to be performed.

For a demonstration of this sample running from the command line, see this video: [SQL Server 2017 Advanced Analytics with Python](https://www.youtube.com/watch?v=FcoY795jTcc)

### Sample Code

```python

from revoscalepy.computecontext.RxComputeContext import RxComputeContext
from revoscalepy.computecontext.RxInSqlServer import RxInSqlServer
from revoscalepy.computecontext.RxInSqlServer import RxSqlServerData
from revoscalepy.functions.RxLinMod import rx_lin_mod_ex
from revoscalepy.functions.RxPredict import rx_predict_ex
from revoscalepy.functions.RxSummary import rx_summary
from revoscalepy.utils.RxOptions import RxOptions
from revoscalepy.etl.RxImport import rx_import_datasource

import os

def test_linmod_sql():
    sqlServer = os.getenv('RTEST_SQL_SERVER', '.')
    
    connectionString = 'Driver=SQL Server;Server=' + sqlServer + ';Database=RevoTestDb;Trusted_Connection=True;'
    print("connectionString={0!s}".format(connectionString))
    
    dataSource = RxSqlServerData(
        sqlQuery = "select top 10 * from airlinedemosmall", 
        connectionString = connectionString,
        colInfo = { 
            "ArrDelay" : { "type" : "integer" }, 
            "DayOfWeek" : { 
                "type" : "factor", 
                "levels" : ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]
            }
        })

    computeContext = RxInSqlServer(
        connectionString = connectionString,
        numTasks = 1,
        autoCleanup = False
        )

    #
    # Import data source to avoid factor levels
    #        
    data = rx_import_datasource(dataSource)
    print(data)

    #
    # run linmod
    #
    linmod = rx_lin_mod_ex("ArrDelay ~ DayOfWeek", data = data, compute_context = computeContext)
    assert (linmod is not None)
    assert (linmod._results is not None)
    print(linmod)

    #
    # Predict results
    # 
    data = rx_import_datasource(dataSource)
    del data["ArrDelay"]
    predict = rx_predict_ex(linmod, data = data)
    assert (predict is not None)
    print(predict._results)

    #
    # Create a summary
    #
    summary = rx_summary("ArrDelay ~ DayOfWeek", data = dataSource, compute_context = computeContext)
    assert (summary is not None)
    print(summary)

test_linmod_sql()
```

## Review the code

Let's review the code and highlight some key steps.

### Defining a data source and compute context

This is an important part of using **revoscalepy** and its related R package **RevoScaleR**. A data source is different from a compute context. The _data source_ defines the data used in your code. The _compute context_ defines where the code will be executed.

> [!NOTE]
> Support for some data source types supported in RevoScaleR might be limited in the pre-release version. For more information about functions included in the latest release, see [What is revoscalepy](../python/what-is-revoscalepy.md).

The overall process for creating and using a data source and compute context is as follows:

1. Create Python variables, such as _sqlQuery_ and _connectionString_, that define the source and the data you want to use. Pass these variables to the **RxSqlServerData** constructor to  implement the **data source object** named _dataSource_.
2. Create a compute context object by using the **RxInSqlServer** constructor. In this example, you pass the same connection string you defined earlier, on the assumption that the data is on the same SQL Server instance that you will be using as the compute context. However, the data source and the compute context could be on different servers. The resulting **compute context object** is named _computeContext_.
3. Choose the active compute context. By default, operations are run locally, which means that if you don't specify a different compute context, the data will be fetched from the data source, and the model-fitting will run in your current Python environment.

    In RevoScaleR, you can also use the function `rxSetComputeContext` to toggle between compute contexts. The function is not implemented yet in the preview version of **revoscalepy**, but you can specify the compute context as an argument to **rx_lin_mod_ex**.
    
    `linmod = rx_lin_mod_ex("ArrDelay ~ DayOfWeek", data = data, compute_context = computeContext)`

    The same applies in the call to **rxsummary**, where the compute context is reused.

    `summary = rx_summary("ArrDelay ~ DayOfWeek", data = dataSource, compute_context = computeContext)`


### Support for parallel processing

The definition of the compute context contains this line, which indicates the maximum number of tasks that will be used in the SQL Server compute context.

`numTasks = 1`

If you set this value to 0, SQL Server uses the default, which is to run as many tasks in parallel as possible, under the current MAXDOP settings for the server. However, even in servers with many processors, the exact number of tasks that might be allocated depends on many other factors, such as server settings, and other jobs that are running:

For more information, see [RxInSqlServer](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxinsqlserver).

> [!NOTE]
> Support for parallel operations will be implemented in a later release.

### Saving the prediction results

Note that this code only returns the predicted values to the console; it doesn't save the data to a table or even a local file.

    # Predict results
    # 
    data = rx_import_datasource(dataSource)
    del data["ArrDelay"]
    predict = rx_predict_ex(linmod, data = data)
    assert (predict is not None)
    print(predict._results)

If you want to save this data somewhere, you have many options:

+ You could wrap the Python code in a stored procedure, and use an INSERT statement to handle the values returned by the stored procedure.
+ You can use the function **rx_data_step_ex** to write the data to a SQL Server table.
+ You could use an ODBC call from your Python code to write the data to a table, or another Python function to convert the data to CSV and write to an external file.

## See Also

See these Python samples and tutorials for advanced tips and end-to-end demos.

+ [In-Database Python for SQL Developers](sqldev-in-database-python-for-sql-developers.md)
+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)
+ [Deploy and Consume Python Models](../python/publish-consume-python-code.md)
