---
description: "Multiple Results"
title: "Multiple Results | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLMoreResults function [ODBC], multiple results"
  - "row counts [ODBC]"
  - "multiple results [ODBC]"
  - "result sets [ODBC], multiple results"
  - "SQLGetInfo function [ODBC], multiple results"
ms.assetid: a3c32e4b-8fe7-4a33-ae39-ae664001f315
author: David-Engel
ms.author: v-davidengel
---
# Multiple Results
A *result* is something returned by the data source after a statement is executed. ODBC has two types of results: result sets and row counts. *Row counts* are the number of rows affected by an update, delete, or insert statement. Batches, described in [Batches of SQL Statements](../../../odbc/reference/develop-app/batches-of-sql-statements.md), can generate multiple results.  
  
 The following table lists the **SQLGetInfo** options an application uses to determine whether a data source returns multiple results for each different type of batch. In particular, a data source can return a single row count for the entire batch of statements or individual row counts for each statement in the batch. In the case of a result set-generating statement executed with an array of parameters, the data source can return a single result set for all sets of parameters or individual result sets for each set of parameters.  
  
|Batch type|Row counts|Result sets|  
|----------------|----------------|-----------------|  
|Explicit batch|SQL_BATCH_ROW_COUNT[a]|--[b]|  
|Procedure|SQL_BATCH_ROW_COUNT[a]|--[b]|  
|Arrays of parameters|SQL_PARAM_ARRAYS_ROW_COUNTS|SQL_PARAM_ARRAYS_SELECTS|  
  
 [a]   Row count-generating statements in a batch may be supported, yet the return of the row counts not supported. The SQL_BATCH_SUPPORT option in **SQLGetInfo** indicates whether row count-generating statements are allowed in batches; the SQL_BATCH_ROW_COUNTS option indicates whether these row counts are returned to the application.  
  
 [b]   Explicit batches and procedures always return multiple result sets when they include multiple result set-generating statements.  
  
> [!NOTE]  
>  The SQL_MULT_RESULT_SETS option introduced in ODBC 1.0 provides only general information about whether multiple result sets can be returned. In particular, it is set to "Y" if the SQL_BS_SELECT_EXPLICIT or SQL_BS_SELECT_PROC bits are returned for SQL_BATCH_SUPPORT or if SQL_PAS_BATCH is returned for SQL_PARAM_ARRAYS_SELECT.  
  
 To process multiple results, an application calls **SQLMoreResults**. This function discards the current result and makes the next result available. It returns SQL_NO_DATA when no more results are available. For example, suppose the following statements are executed as a batch:  
  
```  
SELECT * FROM Parts WHERE Price > 100.00;  
UPDATE Parts SET Price = 0.9 * Price WHERE Price > 100.00  
```  
  
 After these statements are executed, the application fetches rows from the result set created by the **SELECT** statement. When it is done fetching rows, it calls **SQLMoreResults** to make available the number of parts that were repriced. If necessary, **SQLMoreResults** discards unfetched rows and closes the cursor. The application then calls **SQLRowCount** to determine how many parts were repriced by the **UPDATE** statement.  
  
 It is driver-specific whether the entire batch statement is executed before any results are available. In some implementations, this is the case; in others, calling **SQLMoreResults** triggers the execution of the next statement in the batch.  
  
 If one of the statements in a batch fails, **SQLMoreResults** will return either SQL_ERROR or SQL_SUCCESS_WITH_INFO. If the batch was aborted when the statement failed or the failed statement was the last statement in the batch, **SQLMoreResults** will return SQL_ERROR. If the batch was not aborted when the statement failed and the failed statement was not the last statement in the batch, **SQLMoreResults** will return SQL_SUCCESS_WITH_INFO. SQL_SUCCESS_WITH_INFO indicates that at least one result set or count was generated and that the batch was not aborted.
