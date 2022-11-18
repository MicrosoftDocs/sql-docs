---
title: "Binding Parameters | Microsoft Docs"
description: Find out how to bind each parameter marker in an SQL statement to a variable in the application before the statement can run.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, parameters"
  - "parameters [SQL Server Native Client], ODBC"
  - "statements [ODBC], parameters"
  - "binding parameters [SQL Server Native Client]"
  - "parameter markers [ODBC]"
  - "unbound parameter markers"
  - "SQLBindParameter function"
  - "ODBC applications, parameters"
  - "bound parameter markers [SQL Server Native Client]"
ms.assetid: d6c69739-8f89-475f-a60a-b2f6c06576e2
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Statement Parameters - Binding Parameters
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Each parameter marker in an SQL statement must be associated, or bound, to a variable in the application before the statement can be executed. This is done by calling the [SQLBindParameter](../../relational-databases/native-client-odbc-api/sqlbindparameter.md) function. **SQLBindParameter** describes the program variable (address, C data type, and so on) to the driver. It also identifies the parameter marker by indicating its ordinal value and then describes the characteristics of the SQL object it represents (SQL data type, precision, and so on).  
  
 Parameter markers can be bound or rebound at any time before a statement is executed. A parameter binding remains in effect until one of the following occurs:  
  
-   A call to [SQLFreeStmt](../../relational-databases/native-client-odbc-api/sqlfreestmt.md) with the *Option* parameter set to SQL_RESET_PARAMS frees all parameters bound to the statement handle.  
  
-   A call to **SQLBindParameter** with *ParameterNumber* set to the ordinal of a bound parameter marker automatically releases the previous binding.  
  
 An application can also bind parameters to arrays of program variables to process an SQL statement in batches. There are two types of array binding:  
  
-   Column-wise binding is done when each individual parameter is bound to its own array of variables.  
  
     Column-wise binding is specified by calling [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) with *Attribute* set to SQL_ATTR_PARAM_BIND_TYPE and *ValuePtr* set to SQL_PARAM_BIND_BY_COLUMN.  
  
-   Row-wise binding is done when all of the parameters in the SQL statement are bound as a unit to an array of structures that contain the individual variables for the parameters.  
  
     Row-wise binding is specified by calling **SQLSetStmtAttr** with *Attribute* set to SQL_ATTR_PARAM_BIND_TYPE and *ValuePtr* set to the size of the structure holding the program variables.  
  
 When the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver sends character or binary string parameters to the server, it pads the values to the length specified in **SQLBindParameter** *ColumnSize* parameter. If an ODBC 2.x application specifies 0 for *ColumnSize*, the driver pads the parameter value to the precision of the data type. The precision is 8000 when connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] servers, 255 when connected to earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. *ColumnSize* is in bytes for variant columns.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports defining names for stored procedure parameters. ODBC 3.5 also introduced support for named parameters used when calling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedures. This support can be used to:  
  
-   Call a stored procedure and provide values for a subset of the parameters defined for the stored procedure.  
  
-   Specify the parameters in a different order in the application than the order specified when the stored procedure was created.  
  
 Named parameters are only supported when using the [!INCLUDE[tsql](../../includes/tsql-md.md)] **EXECUTE** statement or the ODBC CALL escape sequence to execute a stored procedure.  
  
 If **SQL_DESC_NAME** is set for a stored procedure parameter, all stored procedure parameters in the query should also set **SQL_DESC_NAME**.  If literals are used in stored procedure calls, where parameters have **SQL_DESC_NAME** set, the literals should use the format *'name*=*value*', where *name* is the stored procedure parameter name (for example, @p1). For more information, see [Binding Parameters by Name (Named Parameters)](../../odbc/reference/develop-app/binding-parameters-by-name-named-parameters.md).  
  
## See Also  
 [Using Statement Parameters](../../relational-databases/native-client-odbc-queries/using-statement-parameters.md)  
  
