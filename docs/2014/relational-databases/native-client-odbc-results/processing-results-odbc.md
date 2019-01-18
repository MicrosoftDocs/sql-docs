---
title: "Processing Results (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "result sets [ODBC], about result sets"
  - "SQLRowCount function"
  - "SQL Server Native Client ODBC driver, result sets"
  - "ODBC applications, result sets"
  - "COMPUTE clause"
  - "result sets [ODBC]"
  - "COMPUTE BY clause"
ms.assetid: 61a8db19-6571-47dd-84e8-fcc97cb60b45
author: MightyPen
ms.author: genemi
manager: craigg
---
# Processing Results (ODBC)
  After an application submits a SQL statement, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns any resulting data as one or more result sets. A result set is a set of rows and columns that match the criteria of the query. SELECT statements, catalog functions, and some stored procedures produce a result set made available to an application in tabular form. If the executed SQL statement is a stored procedure, a batch containing multiple commands, or a SELECT statement containing keywords, there will be multiple result sets to process.  
  
 ODBC catalog functions also can retrieve data. For example, [SQLColumns](../native-client-odbc-api/sqlcolumns.md) retrieves data about columns in the data source. These result sets can contain zero or more rows.  
  
 Other SQL statements, such as GRANT or REVOKE, do not return result sets. For these statements, the return code from **SQLExecute** or **SQLExecDirect** is usually the only indication the statement was successful.  
  
 Each INSERT, UPDATE, and DELETE statement returns a result set containing only the number of rows affected by the modification. This count is made available when application calls [SQLRowCount](../native-client-odbc-api/sqlrowcount.md). ODBC 3.*x* applications must either call **SQLRowCount** to retrieve the result set or [SQLMoreResults](../native-client-odbc-api/sqlmoreresults.md) to cancel it. When an application executes a batch or stored procedure containing multiple INSERT, UPDATE, or DELETE statements, the result set from each modification statement must be processed using **SQLRowCount** or cancelled using **SQLMoreResults**. These counts can be cancelled by including a SET NOCOUNT ON statement in the batch or stored procedure.  
  
 Transact-SQL includes the SET NOCOUNT statement. When the NOCOUNT option is set on, SQL Server does not return the counts of the rows affected by a statement and **SQLRowCount** returns 0. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver version introduces a driver-specific [SQLGetStmtAttr](../native-client-odbc-api/sqlgetstmtattr.md) option, SQL_SOPT_SS_NOCOUNT_STATUS, to report on whether the NOCOUNT option is on or off. Anytime **SQLRowCount** returns 0, the application should test SQL_SOPT_SS_NOCOUNT_STATUS. If SQL_NC_ON is returned, the value of 0 from **SQLRowCount** only indicates that SQL Server has not returned a row count. If SQL_NC_OFF is returned, it means that NOCOUNT is off and the value of 0 from **SQLRowCount** indicates that the statement did not affect any rows. Applications should not display the value of **SQLRowCount** when SQL_SOPT_SS_NOCOUNT_STATUS is SQL_NC_OFF. Large batches or stored procedures may contain multiple SET NOCOUNT statements so programmers cannot assume SQL_SOPT_SS_NOCOUNT_STATUS remains constant. The option should be tested each time **SQLRowCount** returns 0.  
  
 Several other Transact-SQL statements return their data in messages rather than result sets. When the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver receives these messages, it returns SQL_SUCCESS_WITH_INFO to let the application know that informational messages are available. The application can then call **SQLGetDiagRec** to retrieve these messages. The [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that work this way are:  
  
-   DBCC  
  
-   SET SHOWPLAN (available with earlier versions of SQL Server)  
  
-   SET STATISTICS  
  
-   PRINT  
  
-   RAISERROR  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver returns SQL_ERROR on a RAISERROR with a severity of 11 or higher. If the severity of the RAISERROR is 19 or higher, the connection is also dropped.  
  
 To process the result sets from an SQL statement, the application:  
  
-   Determines the characteristics of the result set.  
  
-   Binds the columns to program variables.  
  
-   Retrieves a single value, an entire row of values, or multiple rows of values.  
  
-   Tests to see if there are more result sets, and if so, loops back to determining the characteristics of the new result set.  
  
 The process of retrieving rows from the data source and returning them to the application is called fetching.  
  
## In This Section  
  
-   [Determining the Characteristics of a Result Set &#40;ODBC&#41;](determining-the-characteristics-of-a-result-set-odbc.md)  
  
-   [Assigning Storage](assigning-storage.md)  
  
-   [Fetching Result Data](fetching-result-data.md)  
  
-   [Mapping Data Types &#40;ODBC&#41;](mapping-data-types-odbc.md)  
  
-   [Data Type Usage](data-type-usage.md)  
  
-   [Autotranslation of Character Data](autotranslation-of-character-data.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../native-client/odbc/sql-server-native-client-odbc.md)   
 [Processing Results How-to Topics &#40;ODBC&#41;](../../database-engine/dev-guide/processing-results-how-to-topics-odbc.md)  
  
  
