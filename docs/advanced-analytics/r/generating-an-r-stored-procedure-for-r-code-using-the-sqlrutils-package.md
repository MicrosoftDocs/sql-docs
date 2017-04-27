---
title: "Generating an R Stored Procedure for R Code using the sqlrutils Package | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: d8739f16-ac26-4f69-870c-51c77cf286d3
caps.latest.revision: 8
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Generating an R Stored Procedure for R Code using the sqlrutils Package
The **sqlrutils** package provides a mechanism for R users to put their R scripts into a T-SQL stored procedure, register that stored procedure with a database, and run the stored procedure from an R development environment. 

By converting your R code to run within a single stored procedure, you can make more effective use of SQL Server R Services, which requires that R script be embedded as a parameter to [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). The **sqlrutils** package helps you build this embedded R script and set related parameters appropriately.

The **sqlrutils** package performs these tasks:

- Saves the generated T-SQL script as a string inside an R data structure
- Optionally, generates a .sql file for the T-SQL script, which you can edit or run to create a stored procedure
- Registers the newly created stored procedure with the SQL Server instance from your R development environment

You can also execute the stored procedure from an R environment, by passing well-formed parameters and processing the results. Or, you can use the stored procedure from SQL Server to support common database integration scenarios such as ETL, model training, and high-volume scoring.

  > [!NOTE]
  > If you intend to run the stored procedure from an R environment by calling the *executeStoredProcedure* function, you must use an ODBC 3.8 provider, such as ODBC Driver 13 for SQL Server.  
  
## Functions provided in sqlrutils

The following list provides an overview of the functions that you can call from the **sqlrutils** package to develop a stored procedure for use in SQL Server R Services. For details of the parameters for each method or function, see the R help for the package:

```R
help(package="sqlrutils") 
```

**Define stored procedure parameters and inputs**

- `InputData`. Defines the source of data in SQL Server that will be used in the R data frame. You specify the name of the data.frame in which to store the input data, and a query to get the data, or a default value. Only simple SELECT queries are supported.

- `InputParameter`. Defines a single input parameter that will be embedded in the T-SQL script. You must provide the name of the parameter and its R data type.

- `OutputData`. Generates an intermediate data object that is needed if your R function returns a list that contains a data.frame. 
   The *OutputData* object is used to store the name of a single data.frame obtained from the list. 

- `OutputParameter`. Generates an intermediate data object that is needed if your R function returns a list. The *OutputParameter* object stores the name and data type of a single member of the list, assuming that member is **not** a data frame. 


**Generate and register the stored procedure**


- `StoredProcedure` is the main constructor used to build the stored procedure.  This constructor generates a *SQL Server Stored Procedure* object, and optionally creates a text file containing a query that can be used to generate the stored procedure using a T-SQL command. Optionally, the *StoredProcedure* function can also register the stored procedure with the specified instance and database.

   + Use the `func` argument to specify a valid R function. All the variables that the function uses must be defined either inside the function or be provided as input parameters. These parameters can include a maximum of one data frame.
   + The R function must return either a data frame, a named list, or a NULL. If the function returns a list, the list can contain a maximum of one data.frame.
   + Use the argument `spName` to specify the name of the stored procedure you want to create.
   + You can pass in optional input and output parameters, using the objects created by these helper functions: `setInputData`, `setInputParameter`, and `setOutputParameter`.
   +  Optionally, use `filePath` to provide the path and name of a .sql file to create. You can run this file on the SQL Server instance to generate the stored procedure using T-SQL.
   + To define the server and database where the stored procedure will be saved, use the arguments `dbName` and  `connectionString`.
   + To get a list of the *InputData* and *InputParameter* objects that were used to create a specific *StoredProcedure* object, call `getInputParameters`. 
   + To register the stored procedure with the specified database, use `registerStoredProcedure`.

   The stored procedure object typically does not have any data or values associated with it, unless a default value was specified. Data is not retrieved until the stored procedure is executed. 


**Specify inputs and execute**

- Use `setInputDataQuery` to assign a query to an *InputParameter* object. For example, if you have created a stored procedure object in R, you can use `setInputDataQuery` to pass arguments to the *StoredProcedure* function in order to execute the stored procedure with the desired inputs.

- Use `setInputValue` to assign specific values to a parameter stored as an *InputParameter* object. You then pass the parameter object and its value assignment to the *StoredProcedure* function to execute the stored procedure with the set values.

- Use `executeStoredProcedure` to execute a stored procedure defined as an *StoredProcedure* object. Call this function only when executing a stored procedure from R code. Do not use it when running the stored procedure from SQL Server using T-SQL.

  > [!NOTE]
  > The *executeStoredProcedure* function requires an ODBC 3.8 provider, such as ODBC Driver 13 for SQL Server.  
  
  



## See Also
[How to Create a Stored Procedure using sqlrutils](../../advanced-analytics/r-services/how-to-create-a-stored-procedure-using-sqlrutils.md)

