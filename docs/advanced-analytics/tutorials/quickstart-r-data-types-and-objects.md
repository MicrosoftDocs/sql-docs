---
title: "Quickstart: R data types"
description: In this quickstart, learn how to work with data types and data objects in R and SQL Server with SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/04/2019  
ms.topic: quickstart
author: garyericson
ms.author: garye
ms.reviewer: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Quickstart: Handle data types and objects using R in SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In this quickstart, you'll learn about common issues that occur when moving data between R and SQL Server. The experience you gain through this exercise provides essential background when working with data in your own script.

Common issues to know up front include:

- Data types sometimes don't match
- Implicit conversions might take place
- Cast and convert operations are sometimes required
- R and SQL use different data objects

## Prerequisites

- This quickstart requires access to an instance of SQL Server with [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) with the R language installed.

  Your SQL Server instance can be in an Azure virtual machine or on-premises. Just be aware that the external scripting feature is disabled by default, so you might need to [enable external scripting](../install/sql-machine-learning-services-windows-install.md#bkmk_enableFeature) and verify that **SQL Server Launchpad service** is running before you start.

- You also need a tool for running SQL queries that contain R scripts. You can run these scripts using any database management or query tool, as long as it can connect to a SQL Server instance, and run a T-SQL query or stored procedure. This quickstart uses [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms).

## Always return a data frame

When your script returns results from R to SQL Server, it must return the data as a **data.frame**. Any other type of object that you generate in your script - whether that be a list, factor, vector, or binary data - must be converted to a data frame if you want to output it as part of the stored procedure results. Fortunately, there are multiple R functions to support changing other objects to a data frame. You can even serialize a binary model and return it in a data frame, which you'll do later in this quickstart.

First, let's experiment with some R basic R objects - vectors, matrices, and lists - and see how conversion to a data frame changes the output passed to SQL Server.

Compare these two "Hello World" scripts in R. The scripts look almost identical, but the first returns a single column of three values, whereas the second returns three columns with a single value each.

**Example 1**

```sql
EXECUTE sp_execute_external_script
       @language = N'R'
     , @script = N' mytextvariable <- c("hello", " ", "world");
       OutputDataSet <- as.data.frame(mytextvariable);'
     , @input_data_1 = N' ';
```

**Example 2**

```sql
EXECUTE sp_execute_external_script
        @language = N'R'
      , @script = N' OutputDataSet<- data.frame(c("hello"), " ", c("world"));'
      , @input_data_1 = N'  ';
```

## Identify schema and data types

Why are the results so different?

The answer can usually be found by using the R `str()` command. Add the function `str(object_name)` anywhere in your R script to have the data schema of the specified R object returned as an informational message. To view messages, see in the **Messages** pane of Visual Studio Code, or the **Messages** tab in SSMS.

To figure out why Example 1 and Example 2 have such different results, insert the line `str(OutputDataSet)` at the end of the _@script_ variable definition in each statement, like this:

**Example 1 with str function added**

```sql
EXECUTE sp_execute_external_script
        @language = N'R'
      , @script = N' mytextvariable <- c("hello", " ", "world");
      OutputDataSet <- as.data.frame(mytextvariable);
      str(OutputDataSet);'
      , @input_data_1 = N'  '
;
```

**Example 2 with str function added**

```sql
EXECUTE sp_execute_external_script
  @language = N'R', 
  @script = N' OutputDataSet <- data.frame(c("hello"), " ", c("world"));
    str(OutputDataSet);' , 
  @input_data_1 = N'  ';
```

Now, review the text in **Messages** to see why the output is different.

**Results - Example 1**

```sql
STDOUT message(s) from external script:
'data.frame':	3 obs. of  1 variable:
$ mytextvariable: Factor w/ 3 levels " ","hello","world": 2 1 3
```

**Results - Example 2**

```sql
STDOUT message(s) from external script:
'data.frame':	1 obs. of  3 variables:
$ c..hello..: Factor w/ 1 level "hello": 1
$ X...      : Factor w/ 1 level " ": 1
$ c..world..: Factor w/ 1 level "world": 1
```

As you can see, a slight change in R syntax had a big effect on the schema of the results. We won't go into why, but the differences in R data types are explained in details in the *Data Structures* section in ["Advanced R" by Hadley Wickham](http://adv-r.had.co.nz).

For now, just be aware that you need to check the expected results when coercing R objects into data frames.

> [!TIP]
> You can also use R identity functions, such as `is.matrix`, `is.vector`, to return information about the internal data structure.

## Implicit conversion of data objects

Each R data object has its own rules for how values are handled when combined with other data objects if the two data objects have the same number of dimensions, or if any data object contains heterogenous data types.

First, create a small table of test data.

```sql
CREATE TABLE RTestData (col1 INT NOT NULL)

INSERT INTO RTestData
VALUES (1);

INSERT INTO RTestData
VALUES (10);

INSERT INTO RTestData
VALUES (100);
GO
```

For example, assume you run the following statement to perform matrix multiplication using R. You multiply a single-column matrix with the three values by an array with four values, and expect a 4x3 matrix as a result.

```sql
EXECUTE sp_execute_external_script
    @language = N'R'
    , @script = N'
        x <- as.matrix(InputDataSet);
        y <- array(12:15);
    OutputDataSet <- as.data.frame(x %*% y);'
    , @input_data_1 = N' SELECT [Col1]  from RTestData;'
    WITH RESULT SETS (([Col1] int, [Col2] int, [Col3] int, Col4 int));
```

Under the covers, the column of three values is converted to a single-column matrix. Because a matrix is just a special case of an array in R, the array `y` is implicitly coerced to a single-column matrix to make the two arguments conform.

**Results**

|Col1|Col2|Col3|Col4|
|---|---|---|---|
|12|13|14|15|
|120|130|140|150|
|1200|1300|1400|1500|

However, note what happens when you change the size of the array `y`.

```sql
execute sp_execute_external_script
   @language = N'R'
   , @script = N'
        x <- as.matrix(InputDataSet);
        y <- array(12:14);
   OutputDataSet <- as.data.frame(y %*% x);'
   , @input_data_1 = N' SELECT [Col1]  from RTestData;'
   WITH RESULT SETS (([Col1] int ));
```

Now R returns a single value as the result.

**Results**

|Col1|
|---|
|1542|

Why? In this case, because the two arguments can be handled as vectors of the same length, R returns the inner product as a matrix.  This is the expected behavior according to the rules of linear algebra; however, it could cause problems if your downstream application expects the output schema to never change!

> [!TIP]
> 
> Getting errors? Make sure that you're running the stored procedure in the context of the database that contains the table, and not in **master** or another database.
>
> Also, we suggest that you avoid using temporary tables for these examples. Some R clients will terminate a connection between batches, deleting temporary tables.

## Merge or multiply columns of different length

R provides great flexibility for working with vectors of different sizes, and for combining these column-like structures into data frames. Lists of vectors can look like a table, but they don't follow all the rules that govern database tables.

For example, the following script defines a numeric array of length 6 and stores it in the R variable `df1`. The numeric array is then combined with the integers of the RTestData table, which contains three (3) values, to make a new data frame, `df2`.

```sql
EXECUTE sp_execute_external_script
    @language = N'R'
    , @script = N'
               df1 <- as.data.frame( array(1:6) );
               df2 <- as.data.frame( c( InputDataSet , df1 ));
               OutputDataSet <- df2'
    , @input_data_1 = N' SELECT [Col1]  from RTestData;'
    WITH RESULT SETS (( [Col2] int not null, [Col3] int not null ));
```

To fill out the data frame, R repeats the elements retrieved from RTestData as many times as needed to match the number of elements in the array `df1`.

**Results**

|*Col2*|*Col3*|
|----|----|
|1|1|
|10|2|
|100|3|
|1|4|
|10|5|
|100|6|

Remember that a data frame only looks like a table, and is actually a list of vectors.

## Cast or convert SQL Server data

R and SQL Server don't use the same data types, so when you run a query in SQL Server to get data and then pass that to the R runtime, some type of implicit conversion usually takes place. Another set of conversions takes place when you return data from R to SQL Server.

- SQL Server pushes the data from the query to the R process managed by the Launchpad service and converts it to an internal representation for greater efficiency.
- The R runtime loads the data into a data.frame variable and performs its own operations on the data.
- The database engine returns the data to SQL Server using a secured internal connection and presents the data in terms of SQL Server data types.
- You get the data by connecting to SQL Server using a client or network library that can issue SQL queries and handle tabular data sets. This client application can potentially affect the data in other ways.

To see how this works, run a query such as this one on the [AdventureWorksDW](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks) data warehouse. This view returns sales data used in creating forecasts.

```sql
USE AdventureWorksDW
GO

SELECT ReportingDate
         , CAST(ModelRegion as varchar(50)) as ProductSeries
         , Amount
           FROM [AdventureWorksDW].[dbo].[vTimeSeries]
           WHERE [ModelRegion] = 'M200 Europe'
           ORDER BY ReportingDate ASC
```

> [!NOTE]
>
> You can use any version of AdventureWorks, or create a different query using a database of your own. The point is to try to handle some data that contains text, datetime and numeric values.

Now, try pasting this query as the input to the stored procedure.

```sql
EXECUTE sp_execute_external_script
       @language = N'R'
      , @script = N' str(InputDataSet);
      OutputDataSet <- InputDataSet;'
      , @input_data_1 = N'
           SELECT ReportingDate
         , CAST(ModelRegion as varchar(50)) as ProductSeries
         , Amount
           FROM [AdventureWorksDW].[dbo].[vTimeSeries]
           WHERE [ModelRegion] = ''M200 Europe''
           ORDER BY ReportingDate ASC ;'
WITH RESULT SETS undefined;
```

If you get an error, you'll probably need to make some edits to the query text. For example, the string predicate in the WHERE clause must be enclosed by two sets of single quotation marks.

After you get the query working, review the results of the `str` function to see how R treats the input data.

**Results**

```sql
STDOUT message(s) from external script: 'data.frame':    37 obs. of  3 variables:
STDOUT message(s) from external script: $ ReportingDate: POSIXct, format: "2010-12-24 23:00:00" "2010-12-24 23:00:00"
STDOUT message(s) from external script: $ ProductSeries: Factor w/ 1 levels "M200 Europe",..: 1 1 1 1 1 1 1 1 1 1
STDOUT message(s) from external script: $ Amount       : num  3400 16925 20350 16950 16950
```

- The datetime column has been processed using the R data type, **POSIXct**.
- The text column "ProductSeries" has been identified as a **factor**, meaning a categorical variable. String values are handled as factors by default. If you pass a string to R, it is converted to an integer for internal use, and then mapped back to the string on output.

### Summary

From even these short examples, you can see the need to check the effects of data conversion when passing SQL queries as input. Because some SQL Server data types are not supported by R, consider these ways to avoid errors:

- Test your data in advance and verify columns or values in your schema that could be a problem when passed to R code.
- Specify columns in your input data source individually, rather than using `SELECT *`, and know how each column will be handled.
- Perform explicit casts as necessary when preparing your input data, to avoid surprises.
- Avoid passing columns of data (such as GUIDS or rowguids) that cause errors and aren't useful for modeling.

For more information on supported and unsupported data types, see [R libraries and data types](../r/r-libraries-and-data-types.md).

For information about the performance impact of run-time conversion of strings to numerical factors, see [SQL Server R Services performance tuning](../r/sql-server-r-services-performance-tuning.md).

## Next steps

To learn about writing advanced R functions in SQL Server, follow this quickstart:

> [!div class="nextstepaction"]
> [Write advanced R functions with SQL Server Machine Learning Services](quickstart-r-functions.md)

For more information on using R in SQL Server Machine Learning Services, see the following articles:

- [Create and score a predictive model in R with SQL Server Machine Learning Services](quickstart-r-train-score-model.md)
- [What is SQL Server Machine Learning Services (Python and R)?](../what-is-sql-server-machine-learning.md)
