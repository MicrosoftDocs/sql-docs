---
title: "Parameters | Microsoft Docs"
description: Learn how to use parameters to exchange data between stored procedures and functions and the application or tool that called the stored procedure or function.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "stored procedures [SQL Server], parameters"
  - "user-defined functions [SQL Server], parameters"
ms.assetid: c1f9bd93-3271-4098-a23b-7bd7a19ab65b
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Parameters
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
Parameters are used to exchange data between stored procedures and functions and the application or tool that called the stored procedure or function: 

*  Input parameters allow the caller to pass a data value to the stored procedure or function.
*  Output parameters allow the stored procedure to pass a data value or a cursor variable back to the caller. User-defined functions cannot specify output parameters.
*  Every stored procedure returns an integer return code to the caller. If the stored procedure does not explicitly set a value for the return code, the return code is 0.

The following stored procedure shows the use of an input parameter, an output parameter, and a return code:
```
-- Create a procedure that takes one input parameter and returns one output parameter and a return code.
CREATE PROCEDURE SampleProcedure @EmployeeIDParm INT,
         @MaxTotal INT OUTPUT
AS
-- Declare and initialize a variable to hold @@ERROR.
DECLARE @ErrorSave INT
SET @ErrorSave = 0

-- Do a SELECT using the input parameter.
SELECT FirstName, LastName, JobTitle
FROM HumanResources.vEmployee
WHERE EmployeeID = @EmployeeIDParm

-- Save any nonzero @@ERROR value.
IF (@@ERROR <> 0)
   SET @ErrorSave = @@ERROR

-- Set a value in the output parameter.
SELECT @MaxTotal = MAX(TotalDue)
FROM Sales.SalesOrderHeader;

IF (@@ERROR <> 0)
   SET @ErrorSave = @@ERROR

-- Returns 0 if neither SELECT statement had an error; otherwise, returns the last error.
RETURN @ErrorSave
GO
```

When a stored procedure or function is executed, input parameters can either have their value set to a constant or use the value of a variable. Output parameters and return codes must return their values into a variable. Parameters and return codes can exchange data values with either Transact-SQL variables or application variables.

If a stored procedure is called from a batch or script, the parameters and return code values can use Transact-SQL variables defined in the same batch. The following example is a batch that executes the procedure created earlier. The input parameter is specified as a constant and the output parameter and return code place their values in Transact-SQL variables:
```
-- Declare the variables for the return code and output parameter.
DECLARE @ReturnCode INT
DECLARE @MaxTotalVariable INT

-- Execute the stored procedure and specify which variables
-- are to receive the output parameter and return code values.
EXEC @ReturnCode = SampleProcedure @EmployeeIDParm = 19,
   @MaxTotal = @MaxTotalVariable OUTPUT

-- Show the values returned.
PRINT ' '
PRINT 'Return code = ' + CAST(@ReturnCode AS CHAR(10))
PRINT 'Maximum Quantity = ' + CAST(@MaxTotalVariable AS CHAR(10))
GO
```

An application can use parameter markers bound to program variables to exchange data between application variables, parameters, and return codes.

## See Also
[CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)   
 [DECLARE @local_variable (Transact-SQL)](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [CREATE FUNCTION (Transact-SQL)](../../t-sql/statements/create-function-transact-sql.md)   
 [Parameters and Execution Plan Reuse section](../../relational-databases/query-processing-architecture-guide.md)   
 [Variables (Transact-SQL)](../../t-sql/language-elements/variables-transact-sql.md)
