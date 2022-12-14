---
title: "Quickstart: R functions"
titleSuffix: SQL machine learning
description: In this quickstart, you'll learn how to use R mathematical and utility functions with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning
ms.date: 05/21/2020
ms.topic: quickstart
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
  - seo-lt-2019
  - intro-quickstart
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---

# Quickstart: R functions with SQL machine learning
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In this quickstart, you'll learn how to use R mathematical and utility functions with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) or on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md). Statistical functions are often complicated to implement in T-SQL, but can be done in R with only a few lines of code.
::: moniker-end
::: moniker range="=sql-server-2017"
In this quickstart, you'll learn how to use R mathematical and utility functions with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md). Statistical functions are often complicated to implement in T-SQL, but can be done in R with only a few lines of code.
::: moniker-end
::: moniker range="=sql-server-2016"
In this quickstart, you'll learn how to use R mathematical and utility functions with [SQL Server R Services](../r/sql-server-r-services.md). Statistical functions are often complicated to implement in T-SQL, but can be done in R with only a few lines of code.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In this quickstart, you'll learn how to use data structures and data types when using R in [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview). You'll learn about moving data between R and SQL Managed Instance, and the common issues that might occur.
::: moniker-end

## Prerequisites

You need the following prerequisites to run this quickstart.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
- SQL Server Machine Learning Services. To install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning.md?toc=%2Fsql%2Fmachine-learning%2Ftoc.json). You can also [enable Machine Learning Services on SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017"
- SQL Server Machine Learning Services. To install Machine Learning Services, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md). 
::: moniker-end
::: moniker range="=sql-server-2016"
- SQL Server 2016 R Services. To install R Services, see the [Windows installation guide](../install/sql-r-services-windows-install.md).
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
- Azure SQL Managed Instance Machine Learning Services. For information, see the [Azure SQL Managed Instance Machine Learning Services overview](/azure/azure-sql/managed-instance/machine-learning-services-overview).
::: moniker-end

- A tool for running SQL queries that contain R scripts. This quickstart uses [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md).

## Create a stored procedure to generate random numbers

For simplicity, let's use the R `stats` package, that's installed and loaded by default. The package contains hundreds of functions for common statistical tasks, among them the `rnorm` function, which generates a specified number of random numbers using the normal distribution, given a standard deviation and mean.

For example, the following R code returns 100 numbers on a mean of 50, given a standard deviation of 3.

```R
as.data.frame(rnorm(100, mean = 50, sd = 3));
```

To call this line of R from T-SQL, add the R function in the R script parameter of `sp_execute_external_script`, like this:

```sql
EXECUTE sp_execute_external_script
      @language = N'R'
    , @script = N'
         OutputDataSet <- as.data.frame(rnorm(100, mean = 50, sd =3));'
    , @input_data_1 = N'   ;'
      WITH RESULT SETS (([Density] float NOT NULL));
```

What if you'd like to make it easier to generate a different set of random numbers?

That's easy when combined with T-SQL. You define a stored procedure that gets the arguments from the user, then pass those arguments into the R script as variables.

```sql
CREATE PROCEDURE MyRNorm (
    @param1 INT
    , @param2 INT
    , @param3 INT
    )
AS
EXECUTE sp_execute_external_script @language = N'R'
    , @script = N'
	     OutputDataSet <- as.data.frame(rnorm(mynumbers, mymean, mysd));'
    , @input_data_1 = N'   ;'
    , @params = N' @mynumbers int, @mymean int, @mysd int'
    , @mynumbers = @param1
    , @mymean = @param2
    , @mysd = @param3
WITH RESULT SETS(([Density] FLOAT NOT NULL));
```

- The first line defines each of the SQL input parameters that are required when the stored procedure is executed.

- The line beginning with `@params` defines all variables used by the R code, and the corresponding SQL data types.

- The lines that immediately follow map the SQL parameter names to the corresponding R variable names.

Now that you've wrapped the R function in a stored procedure, you can easily call the function and pass in different values, like this:

```sql
EXECUTE MyRNorm @param1 = 100,@param2 = 50, @param3 = 3
```

## Use R utility functions for troubleshooting

The **utils** package, installed by default,  provides a variety of utility functions for investigating the current R environment. These functions can be useful if you're finding discrepancies in the way your R code performs in SQL Server and in outside environments.

For example, you might use the system timing functions in R, such as `system.time` and `proc.time`, to capture the time used by R processes and analyze performance issues. For an example, see the tutorial [Create Data Features](../tutorials/walkthrough-create-data-features.md) where R timing functions are embedded in the solution.

```sql
EXECUTE sp_execute_external_script
      @language = N'R'
    , @script = N'
        library(utils);
        start.time <- proc.time();
        
        # Run R processes
        
        elapsed_time <- proc.time() - start.time;'
```

For other useful functions, see [Use R code profiling functions to improve performance](../r/using-r-code-profiling-functions.md).

## Next steps

To create a machine learning model using R with SQL machine learning, follow this quickstart:

> [!div class="nextstepaction"]
> [Create and score a predictive model in R with SQL machine learning](quickstart-r-train-score-model.md)
