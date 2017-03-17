---
title: "View and Summarize Data using R (Data Science End-to-End Walkthrough) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "2016-11-23"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
ms.assetid: 358e1431-8f47-4d32-a02f-f90e519eef49
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Lesson 2-1 - View and Summarize Data using R
Now you'll work with the same data using R code. You'll also learn how to use the functions in the **RevoScaleR** package included with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] to generate  data summaries in the context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] server, and send the results back to your R environment.  

An R script is provided with this walkthrough that includes all the code needed to create the data object, generate summaries, and build models. The R script file, **RSQL_RWalkthrough.R**, can be found in the location where you installed the script files.  
+ If you are experienced with R, you can run the script all at once.
+ For people learning to use RevoScaleR, this tutorial goes through the script line by line
+ To run individual lines from the script, you can highlight a line or lines in the file and press Ctrl + ENTER.    
  
> [!TIP]  
> Save your R workspace in case you want to complete the rest of the walkthrough later.  That way the data objects and other variables will be ready for re-use.   

## Defining SQL Server Data Sources and Compute Contexts
To get data from  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use in your R code, you need to:  
  
- Create a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance  
- Define a query that has the data you need, or specify a table or view    
- =Define one or more compute contexts to use when running R code    
-   Optionally, define functions that can be applied to the data source  
 

### Load the RevoScaleR library

+  If the **RevoScaleR** package is  not already loaded, run:
    ```R
    library("RevoScaleR")`.  
    ```  
    If you get an error, make sure that your R development environment is using the library that includes the RevoScaleR  package. Use a command such as `.libPaths())` to view the current path.  

    If this is your first time using the **RevoScaleR** package, get the online help in the R environment by typing `help("RevoScaleR")` or `help("RxSqlServerData")`.  

### Create connection strings

1. The R script that you downlaoded uses SQL logins. For this tutorial, we've provided examples of both SQL logins and Windows integrated authentication. We recommend that you use Windows authentication where possible, to avoid saving paswords in your R code.

    Regardless of which credentials you use, the account that you use must have permissions to read data and to create new tables in the specified database. For information about how to add users to the SQL database and give them the correct permissions, see [Post-Installation Server Configuration &#40;SQL Server R Services&#41;](http://msdn.microsoft.com/library/b32d43de-7e9c-4ae4-a110-d8d56b514172). 

    ```R  
    # SQL authentication  
    connStr <- "Driver=SQL Server;Server=Your_Server_Name.somedomain.com;Database=Your_Database_Name;Uid=Your_User_Name;Pwd=Your_Password"
  

    # Windows authentication  
    connStrWin <- "Driver=SQL Server;Server=SQL_instance_name;Database=database_name;Trusted_Connection=Yes" 
    
    # Use one of the connection strings
    connStr \<- connStrWin 
    ```
    > [!NOTE] 
    > Be sure to edit the server name, database name, user name and password as needed. 
      
  
### Define and set a compute context  

Next, you'll define a *compute context* that enable the R code to run on the SQL Server computer. Typically, when you are using R, all operations run in memory on your computer. However, by indicating that R operations should be run on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, you can run some tasks in parallel, and tae advantage of server resources.  

By default, the compute context is local, so you'll need to explicitly set the compute context, depending on the operation.  


1.  Begin by defining some variables used to create the compute context.  
  
    ```R    
    sqlShareDir <- paste("C:\\AllShare\\",Sys.getenv("USERNAME"),sep="")  
    sqlWait <- TRUE  
    sqlConsoleOutput <- FALSE    
    ```    
    -   R uses a temporary directory when serializing R objects back and forth between your workstation and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer. You can specify the local directory that is used as *sqlShareDir*, or accept the default.  
  
    -   Use *sqlWait* to indicate whether you want R to wait for results or not.  For a discussion of waiting vs. non-waiting jobs, see [ScaleR Distributed Computing](https://msdn.microsoft.com/microsoft-r/scaler-distributed-computing).
  
    -   Use the argument *sqlConsoleOutput* to indicate that you dont want to see output from the R console.  
  
2.  Create the compute context object with the variables and connection strings already defined, and save it in the R variable *sqlcc*.  
  
    ```  
    cc <- RxInSqlServer(connectionString = connStr, shareDir = sqlShareDir,   
                        wait = sqlWait, consoleOutput = sqlConsoleOutput)  
    ```  

4. Set the compute context.
    ```R
    rxSetComputeContext(cc)  
    ```  
   + `rxSetComputeContext` returns the previously active compute context invisibly so that you can use it
   + `rxGetComputeContext` returns the active compute context  
  
    Note that setting a compute context only affects operations that use functions in the **RevoScaleR** package; the compute context does not affect the way that open source R operations are performed.  
  
### Create an RxSqlServer data object  

Having defined the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection to work with, you can use that data connection object as the basis to define different data sources. A *data source* specifies some set of data that you want to use for a task, such as training, exploration, scoring, or generating features.  
    
You define sets of SQL Server data by using the *RxSqlServer* function, and passing a connection string and the definition of the data to get.  
  
1. Save the SQL statement as a string variable. This query defines the data you'll use to train the model.    
    ```R
    sampleDataQuery <- "SELECT TOP 1000 tipped, fare_amount, 
           passenger_count,trip_time_in_secs,trip_distance,   
           pickup_datetime, dropoff_datetime, pickup_longitude, 
           pickup_latitude, dropoff_longitude,    
           dropoff_latitude 
           FROM nyctaxi_sample"  
    ```

2. Pass the query definition as an argument to the SQL Server data source. The *colClasses* argument specifies the schema of the data to return, by mapping the SQL 

    ```R  
    inDataSource <- RxSqlServerData(sqlQuery = sampleDataQuery, 
         connectionString = connStr,
         colClasses = c(pickup_longitude = "numeric", pickup_latitude = "numeric",
         dropoff_longitude = "numeric", dropoff_latitude = "numeric"),  
         rowsPerRead=500)  
    ```
    
    + The argument  *colClasses* specifies the column types to use when moving the data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and R.  This is important because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses different data types than R, and more data types. For more information, see [Working with R Data Types](../../advanced-analytics/r-services/working-with-r-data-types.md).  
  
    + The argument *rowsPerRead* is important for handling memory usage and efficient computations.  Most of the enhanced analytical functions in[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] process data in chunks and accumulate intermediate results, returning the final computations after all of the data has been read.  By adding the `rowsPerRead` parameter, you can control how many rows of data are read into each chunk for processing.  If the value of this parameter is too large, data access might be slow because you don’t have enough memory to efficiently process such a large chunk of data.  On some systems, setting `rowsPerRead` to too small a value can also provide slower performance.  

> [!TIP] 
> After the *inDataSource* object has been created, you can reuse this object as many times as you need, to get basic information about the data and the variables used, to manipulate and transform the data, or to use it for training a model.  
> 
> However, the *inDataSource* object itself doesn't contain any data from the SQL query yet. The data is not pulled into the local environment until you run a function such as *rxImport* or *rxSummary*.          
  
## Using the SQL Server Data in R  
You can now apply R functions to the data source, to explore, summarize, and chart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data. In this section, you 'll try out several of the functions provided in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] that support remote compute contexts.  
  
-   **rxGetVarInfo**: Use this function with any data frame or set of data in a remote data object (also with some lists and matrices) to get information such as the maximum and minimum values, the data type, and the number of levels in factor columns.  
  
    Consider running this function after any kind of  data input, feature transformation, or feature engineering. By doing so you can ensure that all the features you want to use in your model are of the expected data type and avoid errors.  
 
  
-   **rxSummary**: Use this function to get more detailed statistics about individual variables. You can also transform values, compute summaries using factor levels, and save the summaries for re-use.  
  
  
### Inspect variables in the data source  
    
+ Call the function *rxGetVarInfo*, using the data source  *inDataSource* as an argument, to get a list of the variables in the data source and their data types.  
  
    ```R  
    rxGetVarInfo(data = inDataSource)  
    ```  
  
    **Results:**  
    *Var 1: tipped, Type: integer*  
    *Var 2: fare_amount, Type: numeric*  
    *Var 3: passenger_count, Type: integer*  
    *Var 4: trip_time_in_secs, Type: numeric, Storage: int64*  
    *Var 5: trip_distance, Type: numeric*  
    *Var 6: pickup_datetime, Type: character*  
    *Var 7: dropoff_datetime, Type: character*  
    *Var 8: pickup_longitude, Type: numeric*  
    *Var 9: pickup_latitude, Type: numeric*  
    *Var 10: dropoff_longitude, Type: numeric*  
  
### Create a summary using R

+ Call the RevoScaleR function *rxSummary* to summarize the fare amount, based on the number of passengers. 
    ```R  
    start.time <- proc.time()  
    rxSummary(~fare_amount:F(passenger_count), data = inDataSource)  
    used.time <- proc.time() - start.time  
    print(paste("It takes CPU Time=", round(used.time[1]+used.time[2],2)," seconds,
      Elapsed Time=", round(used.time[3],2), 
      " seconds to summarize the inDataSource.", sep=""))
    ```  
 
    + The first argument to *rxSummary* specifies the formula or term to summarize by. Here, the `F()` function is used to  convert the values in passenger_count into factors before summarizing.  
    + If you do not specify the statistics to output, by default *rxSummary* outputs Mean, StDev, Min, Max, and the number of valid and missing observations.  
    + This example also includes some code to track the time the function starts and completes, so that you can compare performance.  
  
    **Results**  
    *rxSummary(formula = ~fare_amount:F(passenger_count), data = inDataSource)*  
    *Summary Statistics Results for: ~fare_amount:F(passenger_count)*  
    *Data: inDataSource (RxSqlServerData Data Source)*  
    *Number of valid observations: 1000*   
    *Name                          Mean    StdDev   Min Max ValidObs MissingObs*  
    *fare_amount:F_passenger_count 12.4875 9.682605 2.5 64  1000     0*   
    *Statistics by category (6 categories):  
    *Category                             F_passenger_count Means    StdDev    Min*   
    *fare_amount for F(passenger_count)=1 1                 12.00901  9.219458  2.5*  
    *fare_amount for F(passenger_count)=2 2                 11.61893  8.858739  3.0*  
    *fare_amount for F(passenger_count)=3 3                 14.40196 10.673340  3.5*  
    *fare_amount for F(passenger_count)=4 4                 13.69048  8.647942  4.5*  
    *fare_amount for F(passenger_count)=5 5                 19.30909 14.122969  3.5*  
    *fare_amount for F(passenger_count)=6 6                 12.00000        NA 12.0*  
    *Max ValidObs*  
    *55  666*   
    *52  206*   
    *52   51*   
    *39   21*   
    *64   55*   
    *12    1*   
    *"It takes CPU Time=0.5 seconds, Elapsed Time=4.59 seconds to summarize the inDataSource."*  
  

  
## Next Step  
[Create Graphs and Plots Using R &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-2-2-create-graphs-and-plots-using-r.md)  
  
## Previous Lesson  
[Lesson 1: Prepare the Data &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-1-prepare-the-data-data-science-end-to-end-walkthrough.md)  
  
  
  
