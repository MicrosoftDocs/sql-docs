---
title: "Use Python with revoscalepy to Create a Model| Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "04/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Use Python with revoscalepy to Create a Model

This code sample demonstrates how you can create a logistic regression model using one of the algorithms in the **revoscalepy** package for Python, using Microsoft Machine Learning Services.

The **revoscalepy** package for Python contains objects, transformation, and algorithms similar to those provided for the R language's **RevoScaleR** package. With this library, you can create a compute context, move data between compute contexts, transform data, and train predictive models using popular algorithms such as logistic and linear regression, decision trees, and more.

For more information, see [What is revoscalepy?](../python/what-is-revoscalepy.md)

## Prerequisites

> [!IMPORTANT]
> To run Python code in SQL Server, you must have installed SQL Server 2017 CTP 2.0, with the feature, **Machine Learning Services** with Python. Other versions of SQL Server do not support Python integration.

To run this code, execute the sample as a Python script from the command line, or by using a Python development environment that includes the Python integration components provide in this release.

## Sample Code

This sample contains the following steps:

1. Import the libraries and functions you need
2. Create the connection to SQL Server and create data source objects for working with the data
3. In your Python code, modify the data so that it can be used by the rxLinMod algorithm
4. Call rxLinMod and define the formula to train the model
5. Generate a set of predictions based on the original data set
6. Create a summary based on the predicted values

All operations are performed using an instance of SQL Server as the compute context.

In general, the process of calling Python in a remote compute context is very much like that used for using R in a remote compute context.

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
    # import data source to avoid factor levels
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
    # predict results
    # 
    data = rx_import_datasource(dataSource)
    del data["ArrDelay"]
    predict = rx_predict_ex(linmod, data = data)
    assert (predict is not None)
    print(predict._results)

    #
    # do a summary
    #
    summary = rx_summary("ArrDelay ~ DayOfWeek", data = dataSource, compute_context = computeContext)
    assert (summary is not None)
    print(summary)

test_linmod_sql()

```

## See Also

[Deploy and Consume Python Models](../python/publish-consume-python-code.md)
