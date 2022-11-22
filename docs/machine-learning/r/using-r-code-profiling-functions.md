---
title: Improve performance by using R code profiling function
description: Collect useful information to improve performance and get faster results on R computations on SQL Server by using R profiling functions. The *rprof* function collects and returns information about internal function calls.
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 10/30/2020 
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Use R code profiling functions to improve performance
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This article describes performance tools provided by R packages to get information about internal function calls. You can use this information to improve the performance of your code.

> [!TIP]
> This article provides basic resources to get you started. For expert guidance, we recommend the *Performance* section in ["Advanced R" by Hadley Wickham](http://adv-r.had.co.nz).

## Using RPROF

[*rprof*](https://www.rdocumentation.org/packages/utils/versions/3.5.1/topics/Rprof) is a function included in the base package [**utils**](https://www.rdocumentation.org/packages/utils/versions/3.5.1/topics/PkgUtils), which is loaded by default. 

In general, the *rprof* function works by writing out the call stack to a file, at specified intervals. You can then use the [*summaryRprof*](https://www.rdocumentation.org/packages/utils/versions/3.5.1/topics/summaryRprof) function to process the output file. One advantage of *rprof* is that it performs sampling, thus lessening the performance load from monitoring.

To use R profiling in your code, you call this function and specify its parameters, including the name of the location of the log file that will be written. Profiling can be turned on and off in your code. The following syntax illustrates basic usage: 

```R
# Specify profiling output file.
varOutputFile <- "C:/TEMP/run001.log")
Rprof(varOutputFile)

# Turn off profiling
Rprof(NULL)
    
# Restart profiling
Rprof(append=TRUE)
```

> [!NOTE]
> Using this function requires that Windows Perl be installed on the computer where code is run. Therefore, we recommend that you profile code during development in an R environment, and then deploy the debugged code to SQL Server.  


## R System Functions

The R language includes many base package functions for returning the contents of system variables. For example, as part of your R code, you might use `Sys.timezone` to get the current time zone, or `Sys.Time` to get the system time from R. 

To get information about individual R system functions, type the function name as the argument to the R `help()` function from an R command prompt.

```R
help("Sys.time")
```

## Debugging and Profiling in R

The documentation for Microsoft R Open, which is installed by default, includes a manual on developing extensions for the R language that discusses [profiling and debugging](https://cran.r-project.org/doc/manuals/r-release/R-exts.html#Debugging) in detail.

## Next steps

+ For more information about optimizing R scripts in SQL Server, see [Performance tuning and data optimization for R](r-and-data-optimization-r-services.md).
+ For more complete information about performance tuning on SQL Server, see [Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md).
+ For more information on the utils package, see [The R Utils Package](https://www.rdocumentation.org/packages/utils/versions/3.5.1/topics/PkgUtils).
+ For in-depth discussions of R programming, see ["Advanced R" by Hadley Wickham](http://adv-r.had.co.nz).
