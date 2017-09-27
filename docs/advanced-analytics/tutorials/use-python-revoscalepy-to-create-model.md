---
title: "Use Python with revoscalepy to Create a Model| Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "09/19/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 4
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Use Python with revoscalepy to create a model

This example demonstrates how you can create a linear regression model in SQL Server, using an algorithm from the **revoscalepy** package.

The **revoscalepy** package for Python contains objects, transformations, and algorithms similar to those provided for the **RevoScaleR** package for the R language. With this library, you can create a compute context, move data between compute contexts, transform data, and train predictive models using popular algorithms such as logistic and linear regression, decision trees, and more.

For more information, see [What is revoscalepy?](../python/what-is-revoscalepy.md) and the [Python function reference](https://docs.microsoft.com/r-server/python-reference/introducing-python-package-reference)

## Prerequisites

> [!IMPORTANT]
> To run Python code in SQL Server, you must have installed SQL Server 2017 CTP 2.0 or later, and you must install and enable the feature, **Machine Learning Services** with Python. Other versions of SQL Server do not support Python integration.

## Run the sample code

This code performs the following steps:

1. Imports the required libraries and functions
2. Creates a connection to SQL Server, and creates data source objects for working with the data
3. Modifies the data so that it can be used by the logistic regression algorithm
4. Calls `rx_lin_mod` and defines the formula used to fit the model
5. Generates a set of predictions based on the original data set
6. Creates a summary based on the predicted values

All operations are performed using an instance of SQL Server as the compute context.

In general, the process of calling Python in a remote compute context is similar to the way you use R in a remote compute context. 
You execute the sample as a Python script from the command line, or by using a Python development environment that includes the Python integration components provided in this release. 
In your code, you create and use a compute context object to indicate where you want specific computations to be performed.

> [!NOTE]
> Be sure to change the database and environment names as appropriate.
> 
> For a demonstration of this sample running from the command line, see this video: [SQL Server 2017 Advanced Analytics with Python](https://www.youtube.com/watch?v=FcoY795jTcc)


### Sample code

```python
from revoscalepy import RxComputeContext, RxInSqlServer, RxSqlServerData
from revoscalepy import rx_lin_mod, rx_predict, rx_summary
from revoscalepy import RxOptions, rx_import

from pandas import Categorical
import os

def test_linmod_sql():
    sql_server = os.getenv('PYTEST_SQL_SERVER', '.')
    
    sql_connection_string = 'Driver=SQL Server;Server=' + sqlServer + ';Database=PyTestDb;Trusted_Connection=True;'
    print("connectionString={0!s}".format(sql_connection_string))

    data_source = RxSqlServerData(
        sql_query = "select top 10 * from airlinedemosmall",
        connection_string = sql_connection_string,

        column_info = {
            "ArrDelay" : { "type" : "integer" },
            "DayOfWeek" : {
                "type" : "factor",
                "levels" : [ "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" ]
            }
        })

    sql_compute_context = RxInSqlServer(
        connection_string = sql_connection_string,
        num_tasks = 4,
        auto_cleanup = False
        )

    #
    # Run linmod locally
    #
    linmod_local = rx_lin_mod("ArrDelay ~ DayOfWeek", data = data_source)
    #
    # Run linmod remotely
    #
    linmod = rx_lin_mod("ArrDelay ~ DayOfWeek", data = data_source, compute_context = sql_compute_context)

    # Predict results
    # 
    predict = rx_predict(linmod, data = rx_import(input_data = data_source))
    summary = rx_summary("ArrDelay ~ DayOfWeek", data = data_source, compute_context = sql_compute_context)
```

## Discussion

Let's review the code and highlight some key steps.

### Defining a data source and compute context

A data source is different from a compute context. The _data source_ defines the data used in your code. The _compute context_ defines where the code will be executed.

1. Create Python variables, such as `sql_query` and `sql_connection_string`, that define the source and the data you want to use. Pass these variables to the [RxSqlServerData](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rxsqlserverdata) constructor to implement the **data source object** named `data_source`.
2. Create a compute context object by using the [RxInSqlServer](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rxinsqlserverdata) constructor. In this example, you pass the same connection string you defined earlier, on the assumption that the data is on the same SQL Server instance that you will be using as the compute context. However, the data source and the compute context could be on different servers. The resulting **compute context object** is named `sql_cc`.
3. Choose the active compute context. By default, operations are run locally, which means that if you don't specify a different compute context, the data will be fetched from the data source, and the model-fitting will run in your current Python environment.

### Changing compute contexts

In this example, you set the compute context by using an argument of the individual **rx** function.
    
`linmod = rx_lin_mod_ex("ArrDelay ~ DayOfWeek", data = data, compute_context = sql_compute_context)`

The same applies in the call to **rxsummary**, where the compute context is reused.

`summary = rx_summary("ArrDelay ~ DayOfWeek", data = data_source, compute_context = sql_compute_context)`

You can also use the function [rx_set_computecontext](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rx-set-compute-context) to toggle between compute contexts that have already been defined.

### Setting the degree of parallelism

When you define the compute context, you can also set parameters that control how the data is handled by the compute context. These parameters differ depending on the data source type.

For SQL Server compute contexts, you can set the batch size, or provide hints about the degree of parallelism to use in running tasks.

The sample was run on a computer with four processors, so we set the *num_tasks* parameter to 4. If you set this value to 0, SQL Server uses the default, which is to run as many tasks in parallel as possible, under the current MAXDOP settings for the server.However, even in servers with many processors, the exact number of tasks that might be allocated depends on many other factors, such as server settings, and other jobs that are running.

## Related samples

See these Python samples and tutorials for advanced tips and end-to-end demos.

+ [In-Database Python for SQL developers](sqldev-in-database-python-for-sql-developers.md)
+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)
+ [Deploy and consume Python Models](../python/publish-consume-python-code.md)
