---
title: Write advanced R functions
titleSuffix: SQL Server Machine Learning Services
description: Learn how to write an R function for advanced statistical computation in SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/17/2019  
ms.topic: quickstart
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Write advanced R functions in SQL Server Machine Learning Services

This article describes how to embed R mathematical and utility functions in a SQL stored procedure using [SQL Server Machine Learning Services](../what-is-sql-server-machine-learning.md). Advanced statistical functions that are complicated to implement in T-SQL can be done in R with only a single line of code.

## Prerequisites

* This article requires access to an instance of SQL Server with [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) with the R language installed.

* You also need a tool for running SQL queries. You can run the R scripts using any database management or query tool, as long as it can connect to a SQL Server instance, and run a T-SQL query or stored procedure. This quickstart uses [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms).

## Create a stored procedure to generate random numbers

For simplicity, let's use the R `stats` package that's installed and loaded by default with SQL Server Machine Learning Services. The package contains hundreds of functions for common statistical tasks, among them the `rnorm` function. This function generates a specified number of random numbers using the normal distribution, given a standard deviation and means.

For example, the following R code returns 100 numbers on a mean of 50, given a standard deviation of 3.

```R
as.data.frame(rnorm(100, mean = 50, sd = 3));
```

To call this line of R from T-SQL, run `sp_execute_external_script` and add the R function in the R script parameter, like this:

```sql
EXECUTE sp_execute_external_script @language = N'R'
    , @script = N'
OutputDataSet <- as.data.frame(rnorm(100, mean = 50, sd =3));
'
    , @input_data_1 = N'   ;'
WITH RESULT SETS(([Density] FLOAT NOT NULL));
```

What if you'd like to make it easier to generate a different set of random numbers?

That's easy when combined with SQL. You define a stored procedure that gets the arguments from the user, then pass those arguments into the R script as variables.

```sql
CREATE PROCEDURE MyRNorm (
    @param1 INT
    , @param2 INT
    , @param3 INT
    )
AS
EXECUTE sp_execute_external_script @language = N'R'
    , @script = N'
OutputDataSet <- as.data.frame(rnorm(mynumbers, mymean, mysd));
'
    , @input_data_1 = N'   ;'
    , @params = N' @mynumbers int, @mymean int, @mysd int'
    , @mynumbers = @param1
    , @mymean = @param2
    , @mysd = @param3
WITH RESULT SETS(([Density] FLOAT NOT NULL));
```

* The first line defines each of the SQL input parameters that are required when the stored procedure is executed.

* The line beginning with `@params` defines all variables used by the R code, and the corresponding SQL data types.

* The lines that immediately follow map the SQL parameter names to the corresponding R variable names.

Now that you've wrapped the R function in a stored procedure, you can easily call the function and pass in different values, like this:

```sql
EXECUTE MyRNorm @param1 = 100
    , @param2 = 50
    , @param3 = 3
```

## Use R utility functions for troubleshooting

The `utils` package, installed by default, provides a variety of utility functions for investigating the current R environment. These functions can be useful if you're finding discrepancies in the way your R code performs in SQL and in outside environments. For example, you might use the R `memory.limit()` function to get memory for the current R environment.

Because the `utils` package is installed but not loaded by default, you must use the `library()` function to load it first.

```sql
EXECUTE sp_execute_external_script @language = N'R'
    , @script = N'
library(utils);
mymemory <- memory.limit();
OutputDataSet <- as.data.frame(mymemory);
'
    , @input_data_1 = N' ;'
WITH RESULT SETS(([Col1] INT NOT NULL));
```

> [!TIP]
> Many users like to use the system timing functions in R, such as `system.time` and `proc.time`, to capture the time used by R processes and analyze performance issues.
