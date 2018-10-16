---
title: "Prepared Execution ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "prepared execution [ODBC]"
  - "SQL statements [ODBC], prepared execution"
  - "SQL statements [ODBC], executing"
ms.assetid: f08c8a98-31ee-48b2-9dbf-6f31c2166dbb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Prepared Execution ODBC
Prepared execution is an efficient way to execute a statement more than once. The statement is first compiled, or *prepared,* into an access plan. The access plan is then executed one or more times at a later time. For more information about access plans, see [Processing an SQL Statement](../../../odbc/reference/processing-a-sql-statement.md).  
  
 Prepared execution is commonly used by vertical and custom applications to repeatedly execute the same, parameterized SQL statement. For example, the following code prepares a statement to update the prices of different parts. It then executes the statement multiple times with different parameter values each time.  
  
```  
SQLREAL       Price;  
SQLUINTEGER   PartID;  
SQLINTEGER    PartIDInd = 0, PriceInd = 0;  
  
// Prepare a statement to update salaries in the Employees table.  
SQLPrepare(hstmt, "UPDATE Parts SET Price = ? WHERE PartID = ?", SQL_NTS);  
  
// Bind Price to the parameter for the Price column and PartID to  
// the parameter for the PartID column.  
SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_FLOAT, SQL_REAL, 7, 0,  
                  &Price, 0, &PriceInd);  
SQLBindParameter(hstmt, 2, SQL_PARAM_INPUT, SQL_C_ULONG, SQL_INTEGER, 10, 0,  
                  &PartID, 0, &PartIDInd);  
  
// Repeatedly execute the statement.  
while (GetPrice(&PartID, &Price)) {  
   SQLExecute(hstmt);  
}  
```  
  
 Prepared execution is faster than direct execution for statements executed more than once, primarily because the statement is compiled only once; statements executed directly are compiled each time they are executed. Prepared execution also can provide a reduction in network traffic because the driver can send an access plan identifier to the data source each time the statement is executed, rather than an entire SQL statement, if the data source supports access plan identifiers.  
  
 The application can retrieve the metadata for the result set after the statement is prepared and before it is executed. However, returning metadata for prepared, unexecuted statements is expensive for some drivers and should be avoided by interoperable applications if possible. For more information, see [Result Set Metadata](../../../odbc/reference/develop-app/result-set-metadata.md).  
  
 Prepared execution should not be used for statements executed a single time. For such statements, it is slightly slower than direct execution because it requires an additional ODBC function call.  
  
> [!IMPORTANT]  
>  Committing or rolling back a transaction, either by explicitly calling **SQLEndTran** or by working in auto-commit mode, causes some data sources to delete the access plans for all statements on a connection. For more information, see the SQL_CURSOR_COMMIT_BEHAVIOR and SQL_CURSOR_ROLLBACK_BEHAVIOR options in the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description.  
  
 To prepare and execute a statement, the application:  
  
1.  Calls **SQLPrepare** and passes it a string containing the SQL statement.  
  
2.  Sets the values of any parameters. Parameters can actually be set before or after preparing the statement. For more information, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md), later in this section.  
  
3.  Calls **SQLExecute** and does any additional processing that is necessary, such as fetching data.  
  
4.  Repeats steps 2 and 3 as necessary.  
  
5.  When **SQLPrepare** is called, the driver:  
  
    -   Modifies the SQL statement to use the data source's SQL grammar without parsing the statement. This includes replacing the escape sequences discussed in [Escape Sequences in ODBC](../../../odbc/reference/develop-app/escape-sequences-in-odbc.md). The application can retrieve the modified form of an SQL statement by calling **SQLNativeSql**. Escape sequences are not replaced if the SQL_ATTR_NOSCAN statement attribute is set.  
  
    -   Sends the statement to the data source for preparation.  
  
    -   Stores the returned access plan identifier for later execution (if the preparation succeeded) or returns any errors (if the preparation failed). Errors include syntactic errors such as SQLSTATE 42000 (Syntax error or access violation) and semantic errors such as SQLSTATE 42S02 (Base table or view not found).  
  
        > [!NOTE]  
        >  Some drivers do not return errors at this point but instead return them when the statement is executed or when catalog functions are called. Thus, **SQLPrepare** might appear to have succeeded when in fact it has failed.  
  
6.  When **SQLExecute** is called, the driver:  
  
    -   Retrieves the current parameter values and converts them as necessary. For more information, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md), later in this section.  
  
    -   Sends the access plan identifier and converted parameter values to the data source.  
  
    -   Returns any errors. These are generally run-time errors such as SQLSTATE 24000 (Invalid cursor state). However, some drivers return syntactic and semantic errors at this point.  
  
 If the data source does not support statement preparation, the driver must emulate it to the extent possible. For example, the driver might do nothing when **SQLPrepare** is called and then perform direct execution of the statement when **SQLExecute** is called.  
  
 If the data source supports syntax checking without execution, the driver might submit the statement for checking when **SQLPrepare** is called and submit the statement for execution when **SQLExecute** is called.  
  
 If the driver cannot emulate statement preparation, it stores the statement when **SQLPrepare** is called and submits it for execution when **SQLExecute** is called.  
  
 Because emulated statement preparation is not perfect, **SQLExecute** can return any errors normally returned by **SQLPrepare**.
