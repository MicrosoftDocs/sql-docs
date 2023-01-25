---
title: "Quickstart: Python functions"
titleSuffix: SQL machine learning
description: In this quickstart, you'll learn how to use Python mathematical and utility functions with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning
ms.date: 09/28/2020
ms.topic: quickstart
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
  - seo-lt-2019
  - intro-quickstart
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---

# Quickstart: Python functions with SQL machine learning
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

In this quickstart, you'll learn how to use Python mathematical and utility functions with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md), [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview), or [SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md). Statistical functions are often complicated to implement in T-SQL, but can be done in Python with only a few lines of code.

## Prerequisites

You need the following prerequisites to run this quickstart.

- A SQL database on one of these platforms:
  - [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md). To install, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning.md?toc=%2Fsql%2Fmachine-learning%2Ftoc.json).
  - SQL Server Big Data Clusters. See how to [enable Machine Learning Services on SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
  - Azure SQL Managed Instance Machine Learning Services. For information, see the [Azure SQL Managed Instance Machine Learning Services overview](/azure/azure-sql/managed-instance/machine-learning-services-overview).

- A tool for running SQL queries that contain Python scripts. This quickstart uses [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md).

## Create a stored procedure to generate random numbers

For simplicity, let's use the Python `numpy` package, that's installed and loaded by default. The package contains hundreds of functions for common statistical tasks, among them the `random.normal` function, which generates a specified number of random numbers using the normal distribution, given a standard deviation and mean.

For example, the following Python code returns 100 numbers on a mean of 50, given a standard deviation of 3.

```Python
numpy.random.normal(size=100, loc=50, scale=3)
```

To call this line of Python from T-SQL, add the Python function in the Python script parameter of `sp_execute_external_script`. The output expects a data frame, so use `pandas` to convert it.

```sql
EXECUTE sp_execute_external_script @language = N'Python'
    , @script = N'
import numpy
import pandas
OutputDataSet = pandas.DataFrame(numpy.random.normal(size=100, loc=50, scale=3));
'
    , @input_data_1 = N'   ;'
WITH RESULT SETS(([Density] FLOAT NOT NULL));
```

What if you'd like to make it easier to generate a different set of random numbers? You define a stored procedure that gets the arguments from the user, then pass those arguments into the Python script as variables.

```sql
CREATE PROCEDURE MyPyNorm (
      @param1 INT
    , @param2 INT
    , @param3 INT
    )
AS
EXECUTE sp_execute_external_script @language = N'Python'
    , @script = N'
import numpy
import pandas
OutputDataSet = pandas.DataFrame(numpy.random.normal(size=mynumbers, loc=mymean, scale=mysd));
'
    , @input_data_1 = N'   ;'
    , @params = N' @mynumbers int, @mymean int, @mysd int'
    , @mynumbers = @param1
    , @mymean = @param2
    , @mysd = @param3
WITH RESULT SETS(([Density] FLOAT NOT NULL));
```

- The first line defines each of the SQL input parameters that are required when the stored procedure is executed.

- The line beginning with `@params` defines all variables used by the Python code, and the corresponding SQL data types.

- The lines that immediately follow map the SQL parameter names to the corresponding Python variable names.

Now that you've wrapped the Python function in a stored procedure, you can easily call the function and pass in different values, like this:

```sql
EXECUTE MyPyNorm @param1 = 100,@param2 = 50, @param3 = 3
```

## Use Python utility functions for troubleshooting

Python packages provide a variety of utility functions for investigating the current Python environment. These functions can be useful if you're finding discrepancies in the way your Python code performs in SQL Server and in outside environments.

For example, you might use system timing functions in the `time` package to measure the amount of time used by Python processes and analyze performance issues.

```sql
EXECUTE sp_execute_external_script
      @language = N'Python'
    , @script = N'
import time
start_time = time.time()

# Run Python processes

elapsed_time = time.time() - start_time
'
    , @input_data_1 = N' ;';
```

## Next steps

To create a machine learning model using Python with SQL machine learning, follow this quickstart:

> [!div class="nextstepaction"]
> [Quickstart: Create and score a predictive model in Python](quickstart-python-train-score-model.md)
