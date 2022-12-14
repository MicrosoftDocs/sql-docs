---
title: sqlrutils helper functions
description: sqlrutils is an R package from Microsoft that provides a mechanism for R users to put their R scripts into a T-SQL stored procedure, register that stored procedure with a database, and run the stored procedure from an R development environment. The package is included in SQL Server Machine Learning Services and SQL Server 2016 R Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 09/30/2021
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# sqlrutils (R package in SQL Server Machine Learning Services)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

**sqlrutils** is an R package from Microsoft that provides a mechanism for R users to put their R scripts into a T-SQL stored procedure, register that stored procedure with a database, and run the stored procedure from an R development environment. The package is included in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and [SQL Server 2016 R Services](sql-server-r-services.md).

By converting your R code to run within a single stored procedure, you can make more effective use of SQL Server R Services, which requires that R script be embedded as a parameter to [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). The **sqlrutils** package helps you build this embedded R script and set related parameters appropriately.

The **sqlrutils** package performs these tasks:

- Saves the generated T-SQL script as a string inside an R data structure
- Optionally, generate a .sql file for the T-SQL script, which you can edit or run to create a stored procedure
- Registers the newly created stored procedure with the SQL Server instance from your R development environment

You can also execute the stored procedure from an R environment, by passing well-formed parameters and processing the results. Or, you can use the stored procedure from SQL Server to support common database integration scenarios such as ETL, model training, and high-volume scoring.

> [!NOTE]
> If you intend to run the stored procedure from an R environment by calling the *executeStoredProcedure* function, you must use an ODBC 3.8 provider, such as ODBC Driver 13 for SQL Server.  
  
## Full reference documentation

The **sqlrutils** package is distributed in multiple Microsoft products, but usage is the same whether you get the package in SQL Server or another product. Should any product-specific behaviors exist, discrepancies will be noted in the function help page.

## Functions list

The following section provides an overview of the functions that you can call from the **sqlrutils** package to develop a stored procedure containing embedded R code. For details of the parameters for each method or function, see the R help for the package: `help(package="sqlrutils")`

|Function | Description |
|------|-------------|
|[executeStoredProcedure](reference/sqlrutils/executestoredprocedure.md)| Execute a SQL stored procedure.|
|[getInputParameters](reference/sqlrutils/getinputparameters.md)| Get a list of input parameters to the stored procedure.| 
|[InputData](reference/sqlrutils/inputdata.md)| Defines the source of data in SQL Server that will be used in the R data frame. You specify the name of the data.frame in which to store the input data, and a query to get the data, or a default value. Only simple SELECT queries are supported. | 
|[InputParameter](reference/sqlrutils/inputparameter.md)| Defines a single input parameter that will be embedded in the T-SQL script. You must provide the name of the parameter and its R data type.| 
|[OutputData](reference/sqlrutils/outputdata.md)| Generates an intermediate data object that is needed if your R function returns a list that contains a data.frame. The *OutputData* object is used to store the name of a single data.frame obtained from the list.| 
|[OutputParameter](reference/sqlrutils/outputparameter.md) | Generates an intermediate data object that is needed if your R function returns a list. The *OutputParameter* object stores the name and data type of a single member of the list, assuming that member is **not** a data frame. |
|[registerStoredProcedure](reference/sqlrutils/registerstoredprocedure.md) | Register the stored procedure with a database.|
|[setInputDataQuery](reference/sqlrutils/setinputdataquery.md)| Assign a query to an input data parameter of the stored procedure.| 
|[setInputParameterValue](reference/sqlrutils/setinputparametervalue.md)| Assign a value to an input parameter of the stored procedure.| 
|[StoredProcedure](reference/sqlrutils/storedprocedure.md)| A stored procedure object.|


## How to use sqlrutils

The **sqlrutils** package functions must run on a computer having SQL Server Machine Learning with R. If you are working on a client workstation, set a remote compute context to shift execution to SQL Server. The workflow for using this package includes the following steps:

+ Define stored procedure parameters (inputs, outputs, or both) 
+ Generate and register the stored procedure    
+ Execute the stored procedure  

In an R session, load **sqlrutils** from the command line by typing `library(sqlrutils)`.

> [!Note]
> You can load this package on computer that does not have SQL Server (for example, on an R Client instance) if you change the compute context to SQL Server and execute the code in that compute context.


### Define stored procedure parameters and inputs

`StoredProcedure` is the main constructor used to build the stored procedure. This constructor generates a *SQL Server Stored Procedure* object, and optionally creates a text file containing a query that can be used to generate the stored procedure using a T-SQL command. 

Optionally, the *StoredProcedure* function can also register the stored procedure with the specified instance and database.

+ Use the `func` argument to specify a valid R function. All the variables that the function uses must be defined either inside the function or be provided as input parameters. These parameters can include a maximum of one data frame.

+ The R function must return either a data frame, a named list, or a NULL. If the function returns a list, the list can contain a maximum of one data.frame.

+ Use the argument `spName` to specify the name of the stored procedure you want to create.

+ You can pass in optional input and output parameters, using the objects created by these helper functions: `setInputData`, `setInputParameter`, and `setOutputParameter`.

+  Optionally, use `filePath` to provide the path and name of a .sql file to create. You can run this file on the SQL Server instance to generate the stored procedure using T-SQL.

+ To define the server and database where the stored procedure will be saved, use the arguments `dbName` and  `connectionString`.

+ To get a list of the *InputData* and *InputParameter* objects that were used to create a specific *StoredProcedure* object, call `getInputParameters`. 

+ To register the stored procedure with the specified database, use `registerStoredProcedure`.

The stored procedure object typically does not have any data or values associated with it, unless a default value was specified. Data is not retrieved until the stored procedure is executed. 

### Specify inputs and execute

+ Use `setInputDataQuery` to assign a query to an *InputParameter* object. For example, if you have created a stored procedure object in R, you can use `setInputDataQuery` to pass arguments to the *StoredProcedure* function in order to execute the stored procedure with the desired inputs.

+ Use `setInputValue` to assign specific values to a parameter stored as an *InputParameter* object. You then pass the parameter object and its value assignment to the *StoredProcedure* function to execute the stored procedure with the set values.

+ Use `executeStoredProcedure` to execute a stored procedure defined as an *StoredProcedure* object. Call this function only when executing a stored procedure from R code. Do not use it when running the stored procedure from SQL Server using T-SQL.

> [!NOTE]
> The *executeStoredProcedure* function requires an ODBC 3.8 provider, such as ODBC Driver 13 for SQL Server.  

## See also

[How to create a stored procedure using sqlrutils](reference/sqlrutils/how-to-create-stored-procedure-from-r.md)
