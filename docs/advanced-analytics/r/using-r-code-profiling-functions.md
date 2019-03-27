---
title: Use R code profiling functions - SQL Server Machine Learning Services
description: Improve performance and get faster results on R computations on SQL Server by using R profiling functions to return information about internal function calls.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/12/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Use R code profiling functions to improve performance
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In addition to using SQL Server resources and tools to monitor R script execution, you can use performance tools provided by other R packages to get more information about internal function calls. 

> [!TIP]
> This article provides basic resources to get you started. For expert guidance, we recommend the *Performance* section in ["Advanced R" by Hadley Wickham](http://adv-r.had.co.nz).

## Using RPROF

[*rprof*](https://www.rdocumentation.org/packages/utils/versions/3.5.1/topics/Rprof) is a function included in the base package [**utils**](https://www.rdocumentation.org/packages/utils/versions/3.5.1), which is loaded by default. 

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

The documentation for Microsoft R Open, which is installed by default, includes a manual on developing extensions for the R language that discusses [profiling and debugging](https://cran.r-project.org/doc/manuals/r-release/R-exts.html#Debugging) in detail. You can find the same documentation on your computer at C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\doc\manual.

## See also

+ [utils R package](https://www.rdocumentation.org/packages/utils/versions/3.5.1)
+ ["Advanced R" by Hadley Wickham](http://adv-r.had.co.nz)
