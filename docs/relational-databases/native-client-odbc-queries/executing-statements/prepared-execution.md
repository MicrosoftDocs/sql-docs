---
title: "Prepared Execution | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "deferred statement preparation"
  - "prepared execution [ODBC]"
  - "SQLPrepare function"
  - "ODBC applications, statements"
  - "SQLExecute function"
  - "statements [ODBC], prepared execution"
ms.assetid: f3a9d32b-6cd7-4f0c-b38d-c8ccc4ee40c3
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Prepared Execution
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  The ODBC API defines prepared execution as a way to reduce the parsing and compiling overhead associated with repeatedly executing a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement. The application builds a character string containing an SQL statement and then executes it in two stages. It calls [SQLPrepare Function](https://go.microsoft.com/fwlink/?LinkId=59360) once to have the statement parsed and compiled into an execution plan by the [!INCLUDE[ssDE](../../../includes/ssde-md.md)]. It then calls **SQLExecute** for each execution of the prepared execution plan. This saves the parsing and compiling overhead on each execution. Prepared execution is commonly used by applications to repeatedly execute the same, parameterized SQL statement.  
  
 For most databases, prepared execution is faster than direct execution for statements executed more than three or four times primarily because the statement is compiled only once, while statements executed directly are compiled each time they are executed. Prepared execution can also provide a reduction in network traffic because the driver can send an execution plan identifier and the parameter values, rather than an entire SQL statement, to the data source each time the statement is executed.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] reduces the performance difference between direct and prepared execution through improved algorithms for detecting and reusing execution plans from **SQLExecDirect**. This makes some of the performance benefits of prepared execution available to statements executed directly. For more information, see [Direct Execution](../../../relational-databases/native-client-odbc-queries/executing-statements/direct-execution.md).  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] also provides native support for prepared execution. An execution plan is built on **SQLPrepare** and later executed when **SQLExecute** is called. Because [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is not required to build temporary stored procedures on **SQLPrepare**, there is no extra overhead on the system tables in **tempdb**.  
  
 For performance reasons, the statement preparation is deferred until **SQLExecute** is called or a metaproperty operation (such as [SQLDescribeCol](../../../relational-databases/native-client-odbc-api/sqldescribecol.md) or [SQLDescribeParam](../../../relational-databases/native-client-odbc-api/sqldescribeparam.md) in ODBC) is performed. This is the default behavior. Any errors in the statement being prepared are not known until the statement is executed or a metaproperty operation is performed. Setting the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver-specific statement attribute SQL_SOPT_SS_DEFER_PREPARE to SQL_DP_OFF can turn off this default behavior.  
  
 In case of deferred prepare, calling either **SQLDescribeCol** or **SQLDescribeParam** before calling **SQLExecute** generates an extra roundtrip to the server. On **SQLDescribeCol**, the driver removes the WHERE clause from the query and sends it to the server with SET FMTONLY ON to get the description of the columns in the first result set returned by the query. On **SQLDescribeParam**, the driver calls the server to get a description of the expressions or columns referenced by any parameter markers in the query. This method also has some restrictions, such as not being able to resolve parameters in subqueries.  
  
 Excess use of **SQLPrepare** with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver degrades performance, especially when connected to earlier versions of SQL Server. Prepared execution should not be used for statements executed a single time. Prepared execution is slower than direct execution for a single execution of a statement because it requires an extra network roundtrip from the client to the server. On earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] it also generates a temporary stored procedure.  
  
 Prepared statements cannot be used to create temporary objects on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 Some early ODBC applications used **SQLPrepare** any time [SQLBindParameter](../../../relational-databases/native-client-odbc-api/sqlbindparameter.md) was used. **SQLBindParameter** does not require the use of **SQLPrepare**, it can be used with **SQLExecDirect**. For example, use **SQLExecDirect** with **SQLBindParameter** to retrieve the return code or output parameters from a stored procedure that is only executed one time. Do not use **SQLPrepare** with **SQLBindParameter** unless the same statement will be executed multiple times.  
  
## See Also  
 [Executing Statements &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-queries/executing-statements/executing-statements-odbc.md)  
  
  
