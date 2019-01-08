---
title: Quickstart showing R functions- SQL Server Machine Learning
description: In this quickstart, learn how to write an R function for advanced statistical computation.
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/04/2019  
ms.topic: quickstart
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Quickstart: Using R functions
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

If you completed the previous quickstarts, you're familiar with basic operations and ready for something more complex, such as statistical functions. Advanced statistical functions that are complicated to implement in T-SQL can be done in R with only a single line of code.

In this quickstart, you'll embed R mathematical and utility functions in a SQL Server stored procedure.

## Prerequisites

A previous quickstart, [Verify R exists in SQL Server](quickstart-r-verify.md), provides information and links for setting up the R environment required for this quickstart.

## Create a stored procedure to generate random numbers

For simplicity, let's use the R `stats` package, which is installed and loaded by default when you install R feature support in SQL Server. The package contains hundreds of functions for common statistical tasks, among them the `rnorm` function, which generates a specified number of random numbers using the normal distribution, given a standard deviation and mean.

For example, this R code returns 100 numbers on a mean of 50, given a standard deviation of 3.

```R
as.data.frame(rnorm(100, mean = 50, sd = 3));
```

To call this line of R from T-SQL, run sp_execute_external_script and add the R function in the R script parameter, like this:

```sql
EXEC sp_execute_external_script
      @language = N'R'
    , @script = N'
         OutputDataSet <- as.data.frame(rnorm(100, mean = 50, sd =3));'
    , @input_data_1 = N'   ;'
      WITH RESULT SETS (([Density] float NOT NULL));
```

What if you'd like to make it easier to generate a different set of random numbers?

That's easy when combined with SQL Server: define a stored procedure that gets the arguments from the user. Then, pass those arguments into the R script as variables.

```sql
CREATE PROCEDURE MyRNorm (@param1 int, @param2 int, @param3 int)
AS
    EXEC sp_execute_external_script
      @language = N'R'
    , @script = N'
	     OutputDataSet <- as.data.frame(rnorm(mynumbers, mymean, mysd));'
    , @input_data_1 = N'   ;'
	, @params = N' @mynumbers int, @mymean int, @mysd int'
	, @mynumbers = @param1
	, @mymean = @param2
	, @mysd = @param3
    WITH RESULT SETS (([Density] float NOT NULL));
```

+ The first line defines each of the SQL input parameters that are required when the stored procedure is executed.

+ The line beginning with `@params` defines all variables used by the R code, and the corresponding SQL data types.

+ The lines that immediately follow map the SQL parameter names to the corresponding R variable names.

Now that you've wrapped the R function in a stored procedure, you can easily call the function and pass in different values, like this:

```sql
EXEC MyRNorm @param1 = 100,@param2 = 50, @param3 = 3
```

## Use R utility functions for troubleshooting

By default, an installation of R includes the `utils` package, which provides a variety of utility functions for investigating the current R environment. This can be useful if you are finding discrepancies in the way your R code performs in SQL Server and in outside environments.

For example, you might use the R `memory.limit()` function to get memory for the current R environment. Because the `utils` package is installed but not loaded by default, you must use the `library()` function to load it first.

```sql
EXECUTE sp_execute_external_script
      @language = N'R'
    , @script = N'
        library(utils);
        mymemory <- memory.limit();
        OutputDataSet <- as.data.frame(mymemory);'
    , @input_data_1 = N' ;'
WITH RESULT SETS (([Col1] int not null));
```

Many users like to use the system timing functions in R, such as `system.time` and `proc.time`,  to capture the time used by R processes and analyze performance issues.

For an example, see this tutorial: [Create Data Features](../tutorials/walkthrough-create-data-features.md). In this walkthrough, R timing functions are embedded in the solution to compare the performance of two methods for creating features from data: R functions vs. T-SQL functions.

## Next steps

Next, you'll build a predictive model using R in SQL Server.

> [!div class="nextstepaction"]
> [Quickstart: Create a predictive model](quickstart-r-create-predictive-model.md)
