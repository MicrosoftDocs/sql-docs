---
title: Create RxSqlServerData objects
description: "Learn how to use RevoScaleR functions with SQL Server. This tutorial is a continuation of database creation: adding tables and loading data."
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 11/26/2018  
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Create SQL Server data objects using RxSqlServerData (SQL Server and RevoScaleR tutorial)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This is tutorial 2 of the [RevoScaleR tutorial series](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

This tutorial is a continuation of database creation: adding tables and loading data. If a DBA created the database and login in [tutorial two](deepdive-work-with-sql-server-data-using-r.md), you can add tables using an R IDE like RStudio or a built-in tool like **Rgui**.

From R, connect to SQL Server and use **RevoScaleR** functions to perform the following tasks:

> [!div class="checklist"]
> * Create tables for training data and predictions
> * Load tables with data from a local .csv file

Sample data is simulated credit card fraud data (the ccFraud dataset), partitioned into training and scoring datasets. The data file is included in **RevoScaleR**.

Use an R IDE or **Rgui** to complete these taks. Be sure to use the R executables found at this location: C:\Program Files\Microsoft\R Client\R_SERVER\bin\x64 (either Rgui.exe if you are using that tool, or an R IDE pointing to C:\Program Files\Microsoft\R Client\R_SERVER). Having an [R client workstation](../r/set-up-data-science-client.md) with these executables is considered a prerequisite of this tutorial.

## Create the training data table

1. Store the database connection string in an R variable. Below are two examples of valid ODBC connection strings for SQL Server: one using a SQL login, and one for Windows integrated authentication. 

   Be sure to modify the server name, user name, and password as appropriate.

    **SQL login**
    ```R
    sqlConnString <- "Driver=SQL Server;Server=<server-name>; Database=RevoDeepDive;Uid=<user_name>;Pwd=<password>"
    ```

    **Windows authentication**
    ```R
    sqlConnString <- "Driver=SQL Server;Server=<server-name>;Database=RevoDeepDive;Trusted_Connection=True"
    ```

2. Specify the name of the table you want to create, and save it in an R variable.
  
    ```R
    sqlFraudTable <- "ccFraudSmall"
    ```
  
    Because the server instance and database name are already specified as part of the connection string, when you combine the two variables, the *fully qualified* name of the new table becomes *instance.database.schema.ccFraudSmall*.
  
3.  Optionally, specify *rowsPerRead* to control how many rows of data are read in each batch.
  
    ```R
    sqlRowsPerRead = 5000
    ```
  
    Although this parameter is optional, setting it can result in more efficient computations. Most of the enhanced analytical functions in **RevoScaleR** and **MicrosoftML** process data in chunks. The *rowsPerRead* parameter determines the number of rows in each chunk.
  
    You might need to experiment with this setting to find the right balance. If the value is too large, data access might be slow if there is not enough memory to process data in chunks of that size. Conversely, on some systems, if the value of *rowsPerRead* is too small, performance can also slow down.
  
    As an initial value, use the default batch process size defined by the database engine instance to control the number of rows in each chunk (5,000 rows). Save that value in the variable *sqlRowsPerRead*.
  
4.  Define a variable for the new data source object, and pass the arguments previously defined to the **RxSqlServerData** constructor. Note that this only creates the data source object and does not populate it. Loading data is a separate step.
  
    ```R
    sqlFraudDS <- RxSqlServerData(connectionString = sqlConnString,
       table = sqlFraudTable,
       rowsPerRead = sqlRowsPerRead)
    ```

## Create the scoring data table

Using the same steps, create the table that holds the scoring data using the same process.

1. Create a new R variable, *sqlScoreTable*, to store the name of the table used for scoring.
  
    ```R
    sqlScoreTable <- "ccFraudScoreSmall"
    ```
  
2. Provide that variable as an argument to the **RxSqlServerData** function to define a second data source object, *sqlScoreDS*.
  
    ```R
    sqlScoreDS <- RxSqlServerData(connectionString = sqlConnString,
       table = sqlScoreTable, rowsPerRead = sqlRowsPerRead)
    ```

Because you've already defined the connection string and other parameters as variables in the R workspace, you can reuse it for new data sources representing different tables, views, or queries.

> [!NOTE]
> The function uses different arguments for defining a data source based on an entire table than for a data source based on a query. This is because the SQL Server database engine must prepare the queries differently. Later in this tutorial, you learn how to create a data source object based on a SQL query.

## Load data into SQL tables using R

Now that you have created the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables, you can load data into them using the appropriate **Rx** function.

The **RevoScaleR** package contains functions specific to data source types. For text data, use [RxTextData](/machine-learning-server/r-reference/revoscaler/rxtextdata) to generate the data source object. There are additional functions for creating data source objects from Hadoop data, ODBC data, and so forth.

> [!NOTE]
> For this section, you must have **Execute DDL** permissions on the database.

### Load data into the training table

1. Create an R variable, *ccFraudCsv*, and assign to the variable the file path for the CSV file containing the sample data. This dataset is provided in **RevoScaleR**. The "sampleDataDir" is a keyword on the **rxGetOption** function.
  
    ```R
    ccFraudCsv <- file.path(rxGetOption("sampleDataDir"), "ccFraudSmall.csv")
    ```
  
    Notice the call to **rxGetOption**, which is the GET method associated with [rxOptions](/machine-learning-server/r-reference/revoscaler/rxoptions) in **RevoScaleR**. Use this utility to set and list options related to local and remote compute contexts, such as the default shared directory, or the number of processors (cores) to use in computations.
    
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
  
3. At this point, you might want to pause a moment, and view your database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Refresh the list of tables in the database.
  
    You can see that, although the R data objects have been created in your local workspace, the tables have not been created in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Also, no data has been loaded from the text file into the R variable.
  
4. Insert the data by calling the function [rxDataStep](/machine-learning-server/r-reference/revoscaler/rxdatastep) function.
  
    ```R
    rxDataStep(inData = inTextData, outFile = sqlFraudDS, overwrite = TRUE)
    ```
    
    Assuming no problems with your connection string, after a brief pause, you should see results like these:
  
    *Total Rows written: 10000, Total time: 0.466*
    *Rows Read: 10000, Total Rows Processed: 10000, Total Chunk Time: 0.577 seconds*
  
5. Refresh the list of tables. To verify that each variable has the correct data types and was imported successfully, you can also right-click the table in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and select **Select Top 1000 Rows**.

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

[rxDataStep](/machine-learning-server/r-reference/revoscaler/rxdatastep) is a powerful function that can perform multiple transformations on an R data frame. You can also use rxDataStep to convert data into the representation required by the destination: in this case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Optionally, you can specify transformations on the data, by using R functions in the arguments to **rxDataStep**. Examples of these operations are provided later in this tutorial.

## Next steps

> [!div class="nextstepaction"]
> [Query and modify the SQL Server data](../../machine-learning/tutorials/deepdive-query-and-modify-the-sql-server-data.md)