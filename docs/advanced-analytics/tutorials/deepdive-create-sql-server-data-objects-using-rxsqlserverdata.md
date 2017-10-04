---
title: "Create SQL Server Data Objects using RxSqlServerData | Microsoft Docs"
ms.custom: ""
ms.date: "05/18/2017"
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
ms.assetid: bcf5f7ff-795b-4815-b163-bcddd496efce
caps.latest.revision: 18
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Create SQL Server Data Objects using RxSqlServerData

Now that you have created the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and have the necessary permissions to work with the data, you'll create objects in R that let you work with the data — both on the server and on your workstation.

## Create the SQL Server Data Objects

In this step, you’ll use R to create and populate two tables. Both tables contain simulated credit card fraud data. One table is used for training the models, and the other table is used for scoring.

To create tables on the remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, you'll use the **RxSqlServerData** function provided in the **RevoScaleR** package.

> [!TIP]
> If you're using R Tools for Visual Studio, select **R Tools** from the toolbar and click **Windows** to see options for debugging and viewing R variables.

### Create the training data table

1. Provide your database connection string in an R variable. Here we've provided two examples of valid ODBC connection strings for SQL Server: one using a SQL login, and one for Windows integrated authentication (recommended).

    **Using a SQL login**
    ```R
    sqlConnString <- "Driver=SQL Server;Server=instance_name; Database=DeepDive;Uid=user_name;Pwd=password"
    ```

    **Using Windows authentication**
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
  
    Although this parameter is optional, it is important for handling memory usage and efficient computations.  Most of the enhanced analytical functions in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] process data in chunks and accumulate intermediate results, returning the final computations after all of the data has been read.
  
    If the value of this parameter is too large, data access might be slow because you don’t have enough memory to efficiently process such a large chunk of data.  On some systems, if the value of *rowsPerRead* is too small, performance might be slower.
  
    For this walkthrough, you'll use the batch process size defined by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to control the number of rows in each chunk, and save that value in the variable *sqlRowsPerRead*.  We recommend that you experiment with this setting on your system when you are working with a large data set.
  
4.  Finally, define a variable for the new data source object, and pass the arguments previously defined to the RxSqlServerData constructor. Note that this only creates the data source object and does not populate it.
  
    ```R
    sqlFraudDS <- RxSqlServerData(connectionString = sqlConnString,
       table = sqlFraudTable,
       rowsPerRead = sqlRowsPerRead)
    ```

#### To create the scoring data table

You'll create the table that holds the scoring data using the same process.

1. Create a new R variable, *sqlScoreTable*, to store the name of the table used for scoring.
  
    ```R
    sqlScoreTable <- "ccFraudScoreSmall"
    ```
  
2. Provide that variable as an argument to the RxSqlServerData function to define a second data source object, *sqlScoreDS*.
  
    ```R
    sqlScoreDS \<- RxSqlServerData(connectionString = sqlConnString,
       table = sqlScoreTable, rowsPerRead = sqlRowsPerRead)
    ```

Because you've already defined the connection string and other parameters as variables in the R workspace, it is easy to create new data sources for different tables, views, or queries; just specify a different table name.

Later in this tutorial you'll learn how to create a data source object based on a SQL query.

## Load Data into SQL Tables Using R

Now that you have created the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables, you can load data into them using the appropriate **Rx** function.

The **RevoScaleR** package contains functions that support many different data sources: For text data, you'll use RxTextData to generate the data source object. There are additional functions for creating data source objects from Hadoop data, ODBC data, and so forth.

> [!NOTE]
> For this section, you must have Execute DDL permissions on the database.

### Load data into the training table

1. Create an R variable, *ccFraudCsv*, and assign to the variable the file path for the CSV file containing the sample data.
  
    ```R
    ccFraudCsv <- file.path(rxGetOption("sampleDataDir"), "ccFraudSmall.csv")
    ```
  
    Notice the utility function, **rxGetOption**. This function is provided in the **RevoScaleR** package to help you set and manage options related to local and remote compute contexts, such as the default shared directory, the number of processors (cores) to use in computations, etc.  This call is useful because it gets the samples from the correct library regardless of where you are running your code. For example, try running the function on SQL Server, and on your development computer, and see how the paths differ.
  
2. Define a variable to store the new data, and use the RxTextData function to specify the text data source.
  
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
  
    You'll see that although the R data objects have been created in your local workspace, the tables have not been created in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database yet. Also, no data has been loaded from the text file into the R variable.
  
4. Now, call the function **rxDataStep** to insert the data into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.
  
    ```R
    rxDataStep(inData = inTextData, outFile = sqlFraudDS, overwrite = TRUE)
    ```
  
    Assuming no problems with your connection string, after a brief pause, you should see results like these:
  
      *Total Rows written: 10000, Total time: 0.466*

      *Rows Read: 10000, Total Rows Processed: 10000, Total Chunk Time: 0.577 seconds*
  
5. Using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], refresh the list of tables. To verify that each variable has the correct data types and was imported successfully, you can also right-click the table in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and select **Select Top 1000 Rows**.

### Load data into the scoring table

1. You'll follow the same steps to load the data set used for scoring into the database.
  
    Start by providing the path to the source file.
  
    ```R
    ccScoreCsv <- file.path(rxGetOption("sampleDataDir"), "ccFraudScoreSmall.csv")
    ```
  
2. Use the RxTextData function to get the data and save it in the variable, *inTextData*.
  
    ```R
    inTextData <- RxTextData(file = ccScoreCsv,      colClasses = c(
        "custID" = "integer", "gender" = "integer", "state" = "integer",
        "cardholder" = "integer", "balance" = "integer",
        "numTrans" = "integer",
        "numIntlTrans" = "integer", "creditLine" = "integer"))
    ```
  
3.  Call the rxDataStep function to overwrite the current table with the new schema and data.
  
    ```R
    rxDataStep(inData = inTextData, sqlScoreDS, overwrite = TRUE)
    ```
  
    - The *inData* argument defines the data source to use.
  
    - The *outFile* argument specifies the table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you want to save the data.
  
    - If the table already exists and you don't use the *overwrite* option, results will be inserted without truncation.
  
Again, if the connection was successful, you should see a message indicating completion and the time required to write the data into the table:

*Total Rows written: 10000, Total time: 0.384*

*Rows Read: 10000, Total Rows Processed: 10000, Total Chunk Time: 0.456 seconds*

## More about rxDataStep

The **rxDataStep** is a powerful function that can perform multiple transformations on an R data frame, to convert the data into the representation required by the destination. In this case, the destination is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

You can also specify transformations on the data, by using R functions in the arguments to rxDataStep. You'll see examples of these operations later.

## Next Step

[Query and Modify the SQL Server Data](../../advanced-analytics/tutorials/deepdive-query-and-modify-the-sql-server-data.md)

## Previous Step

[Work with SQL Server Data using R](../../advanced-analytics/tutorials/deepdive-work-with-sql-server-data-using-r.md)

