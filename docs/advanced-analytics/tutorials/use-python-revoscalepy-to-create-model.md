---
title: Use Python with revoscalepy to create a model | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Use Python with revoscalepy to create a model
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this lesson, you learn how to run Python code from a remote development client, to create a linear regression model in SQL Server. 

## Prerequisites

+ This lesson uses different data than the previous lessons. You do not need to complete the previous lessons first. However, if you have completed the previous lessons and have a server already configured to run Python, use that server and database as a compute context.
+ To run Python code using SQL Server as a compute context requires SQL Server 2017 or later. Moreover, you must explicitly install and then enable the feature, **Machine Learning Services**, choosing the Python language option.

    If you installed a pre-release version of SQL Server 2017, you should update to at least the RTM version. Later service releases have continued to expand and improve Python functionality. Some features of this tutorial might not work in early pre-release versions.

+ This example uses a predefined Python environment, named `PYTEST_SQL_SERVER`. The environment has been configured to contain **revoscalepy** and other required libraries. 

    If you do not have an environment configured to run Python, you must do so separately. A discussion of how to create or modify Python environments is out of scope for this tutorial. For more information about how to set up a Python client that contains the correct libraries, see [Install Python client](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter) and [Link Python to tools](https://docs.microsoft.com/machine-learning-server/python/quickstart-python-tools).

## Remote compute contexts and revoscalepy

This sample demonstrates the process of creating a Python model in a remote _compute context_, which lets you work from a client, but choose a remote environment, such as SQL Server, Spark, or Machine Learning Server, where the operations are actually performed. Using compute contexts makes it easier to write code once and deploy it to any supported environment.

To execute Python code in SQL Server requires the **revoscalepy** package. This is a special Python package provided by Microsoft, similar to the **RevoScaleR** package for the R language. The **revoscalepy** package supports the creation of compute contexts, and provides the infrastructure for passing data and models between a local workstation and a remote server. The **revoscalepy** function that supports in-database code execution is [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rxinsqlserver).

In this lesson, you use data in SQL Server to train a linear model based on [rx_lin_mod](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-lin-mod), a function in **revoscalepy** that supports regression over very large datasets. 

This lesson also demonstrates the basics of how to set up and then use a **SQL Server compute context** in Python. For a discussion of how compute contexts work with other platforms, and which compute contexts are supported, see [Compute context for script execution in Machine Learning Server](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-compute-context)

## Prepare the database and sample data

1. This lesson uses the database **sqlpy**. If you have not completed any of the previous lessons, you can create the database by running the following code:

    ```sql
    CREATE DATABASE sqlpy;
    GO
    USE sqlpy;
    GO
    ```

    > [!IMPORTANT]
    > If you want to use a different database, be sure to edit the sample code and change the database name in the connection string.

2. This sample uses the Airline dataset, which is available in both R and Python. After you have created a database for your Python samples, populate a table with the data as described here: [Sample data in RevoScaleR](https://docs.microsoft.com/machine-learning-server/r/sample-built-in-data).

3. Change the name of this environment to use an environment available on your client.

## Run the sample code

After you have prepared the database and have the data for training stored in a table, open a Python development environment and run the code sample.

The code performs the following steps:

1. Imports the required libraries and functions.
2. Creates a connection to SQL Server. Creates **data source** objects for working with the data.
3. Modifies the data using **transformations** so that it can be used by the logistic regression algorithm.
4. Calls `rx_lin_mod` and defines the formula used to fit the model.
5. Generates a set of predictions based on the original data.
6. Creates a summary based on the predicted values.

All operations are performed using an instance of SQL Server as the compute context.

> [!NOTE]
> For a demonstration of this sample running from the command line, see this video: [SQL Server 2017 Advanced Analytics with Python](https://www.youtube.com/watch?v=FcoY795jTcc)

### Sample code

```python
from revoscalepy import RxComputeContext, RxInSqlServer, RxSqlServerData
from revoscalepy import rx_lin_mod, rx_predict, rx_summary
from revoscalepy import RxOptions, rx_import

import os

def test_linmod_sql():
    sql_server = os.getenv('PYTEST_SQL_SERVER', '.')
    
    sql_connection_string = 'Driver=SQL Server;Server=' + sqlServer + ';Database=sqlpy;Trusted_Connection=True;'
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

### Defining a data source vs. defining a compute context

A data source is different from a compute context. The _data source_ defines the data used in your code. The _compute context_ defines where the code will be executed. However, they use some of the same information:

+ Python variables, such as `sql_query` and `sql_connection_string`, define the source of the data. 

    Pass these variables to the [RxSqlServerData](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rxsqlserverdata) constructor to implement the **data source object** named `data_source`.

+ You create a **compute context object** by using the [RxInSqlServer](https://docs.microsoft.com/r-server/python-reference/revoscalepy/rxinsqlserverdata) constructor. The resulting **compute context object** is named `sql_cc`.

    This example re-uses the same connection string that you used in the data source, on the assumption that the data is on the same SQL Server instance that you will be using as the compute context. 
    
    However, the data source and the compute context could be on different servers.
 
### Changing compute contexts

After you define a compute context, you must set the **active compute context**. 

By default, most operations are run locally, which means that if you don't specify a different compute context, the data will be fetched from the data source, and the code will run in your current Python environment.

There are two ways to set the active compute context:
+ As an argument of a method or function
+ By calling `rx_set_computecontext`

#### Set compute context as an argument of a method or function

In this example, you set the compute context by using an argument of the individual **rx** function.
    
`linmod = rx_lin_mod_ex("ArrDelay ~ DayOfWeek", data = data, compute_context = sql_compute_context)`

This compute context is reused in the call to [rxsummary](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-summary):.

`summary = rx_summary("ArrDelay ~ DayOfWeek", data = data_source, compute_context = sql_compute_context)`

#### Set a compute context explicitly using rx_set_compute_context

The function [rx_set_compute_context](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-set-compute-context) lets you toggle between compute contexts that have already been defined.

After you have set the active compute context, it remains active until you change it.

### Using parallel processing and streaming

When you define the compute context, you can also set parameters that control how the data is handled by the compute context. These parameters differ depending on the data source type.

For SQL Server compute contexts, you can set the batch size, or provide hints about the degree of parallelism to use in running tasks.

+ The sample was run on a computer with four processors, so the `num_tasks` parameter is set to 4 to allow maximum use of resources. 
+ If you set this value to 0, SQL Server uses the default, which is to run as many tasks in parallel as possible, under the current MAXDOP settings for the server. However, the exact number of tasks that might be allocated depends on many other factors, such as server settings, and other jobs that are running.

## Related samples

These additional Python samples and tutorials demonstrate end-to-end scenarios using more complex data sources, as well as the use of remote compute contexts.

+ [In-Database Python for SQL developers](sqldev-in-database-python-for-sql-developers.md)
+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)
+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)
