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

In general, the process of calling Python in a remote compute context is similar to the way you use R in a remote compute context. You execute the sample as a Python script from the command line, or by using a Python development environment that includes the Python integration components provided in this release.

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

Now let's review the code and highlight some key steps:

### Defining a data source and compute context

This is an important part of using **revoscalepy** and its related R package **RevoScaleR**. A data source is different from a compute context. The _data source_ defines the data used in your code. The _compute context_ defines where the code will be executed.

Whereas RevoScaleR provides multiple functions for defining different types of data sources and compute contexts, currently **revoscalepy** supports just SQL Server. More data source and compute context types will be added in later releases.

The overall process for creating and using a data source and compute context is as follows:

1. Create Python variables, such as _sqlQuery_ and _connectionString_, that define the source and the data you want to use. Pass these variables to the **RxSqlServerData** constructor to  implement the **data source object** named _dataSource_.
2. Create a compute context object by using the **RxInSqlServer** constructor. In this example, you pass the same connection string you defined earlier, on the assumption that the data is on the same SQL Server instance that you will be using as the compute context. However, the data source and the compute context could be completely different.

    The resulting **compute context object** is named _computeContext_.
3. Choose the active compute context. By default, operations are run locally, which means that if you don't specify a different compute context, the data will be fetched from the data source, but the model-fitting will run in your current Python environment.

    In RevoScaleR, you would use the function `rxSetcomputeContext` to toggle between compute contexts, but in **revoscalepy**, you specify the compute context (if not local) as an argument to **rx_lin_mod_ex**.
    
    `linmod = rx_lin_mod_ex("ArrDelay ~ DayOfWeek", data = data, compute_context = computeContext)`

    The same applies in the call to **rxsummary**, where the compute context is reused.

    `summary = rx_summary("ArrDelay ~ DayOfWeek", data = dataSource, compute_context = computeContext)`

### Using local data

If you are familiar with the **rxImport** and **rxDataStep** functions in RevoscaleR, you might be wondering how the `rx_import_datasource` compares.

### Saving the prediction results

Note that this code only returns the predicted values to the console; it doesn't save the data to a table or even a local file.

    # Predict results
    # 
    data = rx_import_datasource(dataSource)
    del data["ArrDelay"]
    predict = rx_predict_ex(linmod, data = data)
    assert (predict is not None)
    print(predict._results)

If you want to save this data somewhere, you have several options:

+ You could run the sample from a stored procedure, and use an INSERT statement to handle the values returned by the stored procedure.
+ You can use the function rxDataStep to write the data to an existing SQL Server table.
+ You could use an ODBC call from your Python code to write the data to a table, or save to an external file.

## See Also

See these Python samples and tutorials for advanced tips and end-to-end demos.

+ [In-Database Python for SQL Developers](sqldev-in-database-python-for-sql-developers.md)
+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)
+ [Deploy and Consume Python Models](../python/publish-consume-python-code.md)
