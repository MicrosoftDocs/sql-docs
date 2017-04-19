---
title: "Using R Code Profiling Functions | Microsoft Docs"
ms.custom: ""
ms.date: "11/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 132db249-b9f1-48f5-a63e-c9806cacc4af
caps.latest.revision: 6
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Using R Code Profiling Functions
In addition to using SQL Server resources and tools to monitor R script execution, you can use performance tools provided by other R packages to get more information about internal function calls. This topic provides a list of some basic resources to get you started. For expert guidance, we recommend the chapter on [Performance](http://adv-r.had.co.nz/Performance.html) in the book ""Advanced R"", by Hadley Wickham.

## Using RPROF

*rprof* is a function included in the base package **utils**, which is loaded by default. One advantage of *rprof* is that it performs sampling, thus lessening the performance load from monitoring.

To use R profiling in your code, you call this function and specify its parameters, including the name of the location of the log file that will be written. See the help for *rprof* for details.

In general, the *rprof* function works by writing out the call stack to a file, at specified intervals. You can then use the *summaryRprof* function to process the output file. 

Profiling can be turned on and off in your code. To turn profiling on, suspend profiling, and then restart it, you would use a sequence of calls to *rprof*:

1. Specify profiling output file.

    ```R
    varOutputFile <- "C:/TEMP/run001.log")
    Rprof(varOutputFile)
    ```
2. Turn off profiling
    ```R
    Rprof(NULL)
    ```
    
3. Restart profiling
    ```R
    Rprof(append=TRUE)
    ```


> [!NOTE]
> Using this function requires that Windows Perl be installed on the computer where code is run. Therefore, we recommend that you profile code during development in an R environment, and then deploy the debugged code to SQL Server.  


## R System Functions

The R language includes many base package functions for returning the contents of system variables. 

For example, as part of your R code, you might use `Sys.timezone` to get the current time zone, or `Sys.Time` to get the system time from R. 

To get information about individual R system functions, type the function name as the argument to the R `help()` function from an R command prompt.

```R
help("Sys.time")
```

## Debugging and Profiling in R

The documentation for Microsoft R Open, which is installed by default, includes a manual on developing extensions for the R language that discusses profiling and debugging in detail.

The chapter is also available online: [https://cran.r-project.org/doc/manuals/r-release/R-exts.html#Debugging](https://cran.r-project.org/doc/manuals/r-release/R-exts.html#Debugging)

### Location of R help files

C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\doc\manual



