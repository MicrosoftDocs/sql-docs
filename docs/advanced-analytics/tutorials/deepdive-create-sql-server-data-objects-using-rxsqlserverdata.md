---
title: "Create SQL Server data objects using RxSqlServerData (SQL and R deep dive)| Microsoft Docs"
ms.custom: ""
ms.date: "12/14/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2016"
  - "SQL Server 2017"
dev_langs: 
  - "R"
ms.assetid: bcf5f7ff-795b-4815-b163-bcddd496efce
caps.latest.revision: 18
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "On Demand"
---
# Create SQL Server data objects using RxSqlServerData (SQL and R deep dive)

This article is part of the Data Science Deep Dive tutorial, on how to use [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

By now you have created the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and have the necessary permissions to work with the data. In this step, you create some objects in R that let you work with the data.

## Create the SQL Server data objects

In this step, you use functions from the **RevoScaleR** package to create and populate two tables. One table is used for training the models, and the other table is used for scoring. Both tables contain simulated credit card fraud data.

To create tables on the remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, call the **RxSqlServerData** function.

> [!TIP]
> If you're using R Tools for Visual Studio, select **R Tools** from the toolbar and click **Windows** to see options for debugging and viewing R variables.

### Create the training data table

1. Save the database connection string in an R variable. Here are two examples of valid ODBC connection strings for SQL Server: one using a SQL login, and one for Windows integrated authentication.

    **SQL login**
    ```R
    sqlConnString <- "Driver=SQL Server;Server=instance_name; Database=DeepDive;Uid=user_name;Pwd=password"
    ```

    **Windows authentication**
    ```R
    sqlConnString <- "Driver=SQL Server;Server=instance_name;Database=DeepDive;Trusted_Connection=True"
    ```

    Be sure to modify the instance name, database name, user name, and password as appropriate.
  
2. Specify the name of the table you want to create, and save it in an R variable.
  
    ```R
    sqlFraudTable <- "ccFraudSmall"
    ```
  
    Because the instance and database name are already specified as part of the connection string, when you combine the two variables, the *fully qualified* name of the new table becomes _instance.database.schema.ccFraudSmall_.
  
3.  Before instantiating the data source object, add a line specifying an additional parameter, *rowsPerRead*.  The *rowsPerRead* parameter controls how many rows of data are read in each batch.
  
    ```R
    sqlRowsPerRead = 5000
    ```
  
    Although this parameter is optional, it is important for handling memory usage and efficient computations.  Most of the enhanced analytical functions in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] process data in chunks and store intermediate results, returning the final computations after all of the data has been read.
  
    If the value of this parameter is too large, data access might be slow because you don’t have enough memory to efficiently process such a large chunk of data.  On some systems, if the value of *rowsPerRead* is too small, performance might be slower. Therefore, we recommend that you experiment with this setting on your system when you are working with a large data set.
  
    For this walkthrough, use the default batch process size defined by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to control the number of rows in each chunk. Save that value in the variable `sqlRowsPerRead`.
  
4.  Finally, define a variable for the new data source object, and pass the arguments previously defined to the RxSqlServerData constructor. Note that this only creates the data source object and does not populate it.
  
    ```R
    sqlFraudDS <- RxSqlServerData(connectionString = sqlConnString,
       table = sqlFraudTable,
       rowsPerRead = sqlRowsPerRead)
    ```

#### To create the scoring data table

Using the same steps, create the table that holds the scoring data using the same process.

1. Create a new R variable, *sqlScoreTable*, to store the name of the table used for scoring.
  
    ```R
    sqlScoreTable <- "ccFraudScoreSmall"
    ```
  
2. Provide that variable as an argument to the RxSqlServerData function to define a second data source object, *sqlScoreDS*.
  
    ```R
    sqlScoreDS \<- RxSqlServerData(connectionString = sqlConnString,
       table = sqlScoreTable, rowsPerRead = sqlRowsPerRead)
    ```

Because you've already defined the connection string and other parameters as variables in the R workspace, it is easy to create new data sources for different tables, views, or queries.

> [!NOTE]
> The function uses different arguments for defining a data source based on an entire table than for a data source based on a query. This is because the SQL Server database engine must prepare the queries differently. Later in this tutorial, you learn how to create a data source object based on a SQL query.

## Load data into SQL tables using R

Now that you have created the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables, you can load data into them using the appropriate **Rx** function.

The **RevoScaleR** package contains functions that support many different data sources: For text data, use [RxTextData](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxtextdata) to generate the data source object. There are additional functions for creating data source objects from Hadoop data, ODBC data, and so forth.

> [!NOTE]
> For this section, you must have **Execute DDL** permissions on the database.

### Load data into the training table

1. Create an R variable, *ccFraudCsv*, and assign to the variable the file path for the CSV file containing the sample data.
  
    ```R
    ccFraudCsv <- file.path(rxGetOption("sampleDataDir"), "ccFraudSmall.csv")
    ```
  
    Notice the call to **rxGetOption**, which is the GET method associated with [rxOptions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxoptions) in **RevoScaleR**. Use this utility to set and list options related to local and remote compute contexts, such as the default shared directory, or the number of processors (cores) to use in computations.
    
    This particular call gets the samples from the correct library, regardless of where you are running your code. For example, try running the function on SQL Server, and on your development computer, and see how the paths differ.
  
2. Define a variable to store the new data, and use the **RxTextData** function to specify the text data source.
  
    ```R
    inTextData <- RxTextData(file = ccFraudCsv,      colClasses = c(
        "custID" = "integer", "gender" = "integer", "state" = "integer",
        "cardholder" = "integer", "balance" = "integer",
        "numTrans" = "integer",
        "numIntlTrans" = "integer", "creditLine" = "integer",
        "fraudRisk" = "integer"))
    ```
  
    The argument *colClasses* is important. You use it to indicate the data type to assign to each column of data loaded from the text file. In this example, all columns are handled as text, except for the named columns, which are handled as integers.
  
3. At this point, you might want to pause a moment, and view your database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  Refresh the list of tables in the database.
  
    You can see that, although the R data objects have been created in your local workspace, the tables have not been created in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Also, no data has been loaded from the text file into the R variable.
  
4. Now, call the function [rxDataStep](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdatastep) to insert the data into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.
  
    ```R
    rxDataStep(inData = inTextData, outFile = sqlFraudDS, overwrite = TRUE)
    ```
  
    Assuming no problems with your connection string, after a brief pause, you should see results like these:
  
      *Total Rows written: 10000, Total time: 0.466*

      *Rows Read: 10000, Total Rows Processed: 10000, Total Chunk Time: 0.577 seconds*
  
5. Using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], refresh the list of tables. To verify that each variable has the correct data types and was imported successfully, you can also right-click the table in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and select **Select Top 1000 Rows**.

### Load data into the scoring table

1. Repeat the steps to load the data set used for scoring into the database.
  
    Start by providing the path to the source file.
  
    ```R
    ccScoreCsv <- file.path(rxGetOption("sampleDataDir"), "ccFraudScoreSmall.csv")
    ```
  
2. Use the **RxTextData** function to get the data and save it in the variable, *inTextData*.
  
    ```R
    inTextData <- RxTextData(file = ccScoreCsv,      colClasses = c(
        "custID" = "integer", "gender" = "integer", "state" = "integer",
        "cardholder" = "integer", "balance" = "integer",
        "numTrans" = "integer",
        "numIntlTrans" = "integer", "creditLine" = "integer"))
    ```
  
3.  Call the **rxDataStep** function to overwrite the current table with the new schema and data.
  
    ```R
    rxDataStep(inData = inTextData, sqlScoreDS, overwrite = TRUE)
    ```
  
    - The *inData* argument defines the data source to use.
  
    - The *outFile* argument specifies the table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you want to save the data.
  
    - If the table already exists and you don't use the *overwrite* option, results are inserted without truncation.
  
Again, if the connection was successful, you should see a message indicating completion and the time required to write the data into the table:

*Total Rows written: 10000, Total time: 0.384*

*Rows Read: 10000, Total Rows Processed: 10000, Total Chunk Time: 0.456 seconds*

## More about rxDataStep

[rxDataStep](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdatastep) is a powerful function that can perform multiple transformations on an R data frame. You can also use rxDataStep to convert data into the representation required by the destination: in this case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Optionally, you can specify transformations on the data, by using R functions in the arguments to **rxDataStep**. Examples of these operations are provided later in this tutorial.

## Next step

[Query and modify the SQL Server data](../../advanced-analytics/tutorials/deepdive-query-and-modify-the-sql-server-data.md)

## Previous step

[Work with SQL Server data using R](../../advanced-analytics/tutorials/deepdive-work-with-sql-server-data-using-r.md)
