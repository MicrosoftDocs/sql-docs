---
title: "Working with inputs and outputs (R in SQL quickstart) | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
  - "SQL"
ms.assetid: 75480e5c-f37f-45b9-a176-67e08e9a9daf
caps.latest.revision: 7
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Working with inputs and outputs (R in SQL quickstart)

When you want to run R code in SQL Server, you must wrap the R script in a system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). This stored procedure is used to start the R runtime in the context of SQL Server, which passes data to R, manages R user sessions securely, and returns any results to the client.

## <a name="bkmk_SSMSBasics"></a>Create some simple test data

Create a small table of test data by running the following T-SQL statement:

```sql
CREATE TABLE RTestData ([col1] int not null) ON [PRIMARY]
INSERT INTO RTestData   VALUES (1);
INSERT INTO RTestData   VALUES (10);
INSERT INTO RTestData   VALUES (100) ;
GO
```

When the table has been created, use the following statement to query the table:
  
```sql
SELECT * FROM RTestData
```

**Results**

|col1|
|------|
|*1*|
|*10*|
|*100*|

## Get the same data using R script

After the table has been created, run the following statement:

```sql
EXECUTE sp_execute_external_script
      @language = N'R'
    , @script = N' OutputDataSet <- InputDataSet;'
    , @input_data_1 = N' SELECT *  FROM RTestData;'
    WITH RESULT SETS (([NewColName] int NOT NULL));
```

It gets the data from the table, makes a round trip through the R runtime, and returns the values with the column name, *NewColName*.

**Results**

![rsql_basictut_getsamedataR](media/rsql-basictut-getsamedatar.PNG)


**Comments**

+ In Management Studio, tabular results are returned in the **Values** pane. Messages returned by the R runtime are provided in the **Messages** pane.
+ The *@language* parameter defines the language extension to call, in this case, R.
+ In the *@script* parameter, you define the commands to pass to the R runtime. Your entire R script must be enclosed in this argument, as Unicode text. You could also add the text to a variable of type **nvarchar** and then call the variable.
+ The data returned by the query is passed to the R runtime, which returns the data to SQL Server as a data frame.
+ The WITH RESULT SETS clause defines the schema of the returned data table for SQL Server.

## Change input or output variables

The preceding example used the default input and output variable names, _InputDataSet_ and _OutputDataSet_. To define the input data associated with  _InputDatSet_, you use the *@input_data_1*  variable.

In this example, the names of the output and input variables for the stored procedure have been changed to *SQL_Out* and *SQL_In*:

```sql
EXECUTE sp_execute_external_script
  @language = N'R'
  , @script = N' SQL_out <- SQL_in;'
  , @input_data_1 = N' SELECT 12 as Col;'
  , @input_data_1_name  = N'SQL_In'
  , @output_data_1_name =  N'SQL_Out'
  WITH RESULT SETS (([NewColName] int NOT NULL));
```

Did you get the error, "object SQL\_in not found"? That's because R is case-sensitive! In the example, the R script uses the variables *SQL_in* and *SQL_out*, but the parameters to the stored procedure use the variables *SQL_In* and *SQL_Out*.

That's because R is case-sensitive. In the example, the R script uses the variables *SQL_in* and *SQL_out*, but the parameters to the stored procedure use the variables *SQL_In* and *SQL_Out*.
Try correcting **only** the *SQL_In* variable and re-run the stored procedure.

Now you get a **different** error:

```Error
EXECUTE statement failed because its WITH RESULT SETS clause specified 1 result set(s), but the statement only sent 0 result set(s) at run time.
```

We're showing you this error because you can expect to see it often when testing new R code. It means that the R script ran successfully, but SQL Server received no data, or received wrong or unexpected data.

In this case, the output schema (the line beginning with **WITH**) specifies that one column of integer data should be returned, but since R put the data in a different variable, nothing came back to SQL Server; hence, the error. to fix the error, correct the second variable name.

**Remember these requirements!**

- Variable names must follow the rules for valid SQL identifiers.
- The order of the parameters is important. You must specify the required parameters *@input_data_1* and *@output_data_1* first, in order to use the optional parameters *@input_data_1_name* and *@output_data_1_name*.
- Only one input dataset can be passed as a parameter, and you can return only one dataset. However, you can call other datasets from inside your R code and you can return outputs of other types in addition to the dataset. You can also add the OUTPUT keyword to any parameter to have it returned with the results. There is a simple example later in this tutorial.
- The `WITH RESULT SETS` statement defines the schema for the data, for the benefit of SQL Server. You need to provide SQL compatible data types for each column you return from R. You can use the schema definition to provide new column names too; you need not use the column names from the R data.frame. In some cases, this clause is optional; try omitting it and see what happens.

## Generate results using R

You can also generate values using just the R script and leave the input query string in _@input_data_1_ blank. Or, use a valid SQL SELECT statement as a placeholder, and not use the SQL results in the R script.

```sql
EXECUTE sp_execute_external_script
    @language = N'R'
   , @script = N' mytextvariable <- c("hello", " ", "world");
       OutputDataSet <- as.data.frame(mytextvariable);'
   , @input_data_1 = N' SELECT 1 as Temp1'
   WITH RESULT SETS (([Col1] char(20) NOT NULL));
```

**Results**

*Col1*
*hello*
<code>   </code>
*world*

## Next lesson

You'll examine some of the problems that you might encounter when passing data between R and SQL Server, such as implicit conversions and differences in tabular data between R and SQL.

[R and SQL data types and data objects](../tutorials/rtsql-r-and-sql-data-types-and-data-objects.md)
