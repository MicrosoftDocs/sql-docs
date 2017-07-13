---
title: "Use Python with revoscalepy to Create a Model| Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "07/03/2017"
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

## Example

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

### Code

```python

from revoscalepy import RxComputeContext, RxInSqlServer, RxSqlServerData
from revoscalepy import rx_lin_mod, rx_predict, rx_summary
from revoscalepy import RxOptions, rx_import

from pandas import Categorical
import os

def test_linmod_sql(num_tasks):
    sql_server = os.getenv('RTEST_SQL_SERVER', '.')
    connection_string  = 'Driver=SQL Server;Server=' + sql_server + ';Database=RevoTestDb;Trusted_Connection=True;'
    print("connection_string={0!s}".format(connection_string))

    data_source = RxSqlServerData(
        sql_query = "select top 1000 * from airlinedemosmall",
        connection_string = connection_string,
        column_info = {
            "ArrDelay" : { "type" : "integer" },
            "DayOfWeek" : {
                "type" : "factor",
                "levels" : [ "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" ]
            }
        })

    compute_context = RxInSqlServer(
        connection_string = connection_string,
        num_tasks = num_tasks,
        auto_cleanup = False
        )

    #
    # run linmod locally
    #
    linmod_local = rx_lin_mod("ArrDelay ~ DayOfWeek", data = data_source)
    assert (linmod_local is not None)
    assert (linmod_local._results is not None)
    print(linmod_local)

    #
    # run limod remotely
    #
    linmod = rx_lin_mod("ArrDelay ~ DayOfWeek", data = data_source, compute_context = compute_context)
    assert (linmod is not None)
    assert (linmod._results is not None)
    print(linmod)

    #
    # verify local and remote models are same
    #
    assert linmod_local.coefficients.equals(linmod.coefficients)

    #run prediction
    predict = rx_predict(linmod, data = rx_import(input_data = data_source))
    assert (predict is not None)
    print(predict)

    #
    #do a summary
    #
    summary = rx_summary("ArrDelay ~ DayOfWeek", data = data_source, compute_context = compute_context)
    assert (summary is not None)
    print(summary)

print ("Executing non-MPI LinMod test...")
test_linmod_sql(num_tasks = 1)

print ("Executing MPI LinMod test...")
test_linmod_sql(num_tasks = 4)
```

### Comments

Let's review the code and highlight some key steps.

+ Compute context vs. data source

    First, note the lines that create the _data source_ and the _compute context_.  These are important concepts in both **revoscalepy** and its R package counterpart, **RevoScaleR**.

    The _data source_ defines the data used in your code. In this case, you;re using SQL Server data, so you use **RxSqlServerData**, and provide the connection string, the data you want to use, and instructions for handling the columns.
    
    > [!NOTE]
    > Support for some data source types supported in RevoScaleR might be limited in the pre-release version. For more information about functions included in the latest release, see [What is revoscalepy](../python/what-is-revoscalepy.md).

    The _compute context_ defines where the code will be executed. You create a compute context object by using the **RxInSqlServer** constructor. In this example, the same connection string used earlier is passed, on the assumption that the data is on the same SQL Server instance that you will be using as the compute context. However, the data source and the compute context could be on different servers.

+ Changing compute context

    After defining a compute context, you can use the function `rxSetComputeContext` to toggle between compute contexts. Or you can specify the compute context as an argument to the analytical function, as in this example:

    ```python
    `linmod = rx_lin_mod("ArrDelay ~ DayOfWeek", data = data_source, compute_context = compute_context)`
    ```

    By default, operations are run locally, which means that if you don't specify a different compute context, the data will be fetched from the data source, and the model-fitting will run in your current Python environment.

+ Support for parallel processing

    Notice the _numtask_ variable. In revoscalepy (and in RevoScaleR) you can set the maximum number of tasks that will be run concurrently in the SQL Server compute context. This variable is specified on the compute context, along with other arguments such as the trace  level, whether to wait for console messages, etc.

    If you set this value to 0, SQL Server uses the default, which is to run as many tasks in parallel as possible, under the current MAXDOP settings for the server. However, even in servers with many processors, the exact number of tasks that might be allocated depends on many other factors, such as server settings, and other jobs that are running. For more information, see [RxInSqlServer](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxinsqlserver).

## Related samples

See these Python samples and tutorials for advanced tips and end-to-end demos.

+ [In-Database Python for SQL Developers](sqldev-in-database-python-for-sql-developers.md)
+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)
+ [Deploy and Consume Python Models](../python/publish-consume-python-code.md)
