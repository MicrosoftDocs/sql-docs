---
title: "Long Data and SQLSetPos and SQLBulkOperations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "long data [ODBC]"
  - "SQLSetPos function [ODBC], long data and SQLBulkOperations"
  - "data updates [ODBC], long data"
  - "updating data [ODBC], long data"
  - "SQLBulkOperations function [ODBC], long data"
ms.assetid: e2fdf842-5e4c-46ca-bb21-4625c3324f28
author: MightyPen
ms.author: genemi
manager: craigg
---
# Long Data and SQLSetPos and SQLBulkOperations
As is the case with parameters in SQL statements, long data can be sent when updating rows with **SQLBulkOperations** or **SQLSetPos** or when inserting rows with **SQLBulkOperations**. The data is sent in parts, with multiple calls to **SQLPutData**. Columns for which data is sent at execution time are known as *data-at-execution columns*.  
  
> [!NOTE]  
>  An application actually can send any type of data at execution time with **SQLPutData**, although only character and binary data can be sent in parts. However, if the data is small enough to fit in a single buffer, there is generally no reason to use **SQLPutData**. It is much easier to bind the buffer and let the driver retrieve the data from the buffer.  
  
 Because long data columns typically are not bound, the application must bind the column before calling **SQLBulkOperations** or **SQLSetPos** and unbind it after calling **SQLBulkOperations** or **SQLSetPos**. The column must be bound because **SQLBulkOperations** or **SQLSetPos** operates only on bound columns and must be unbound so that **SQLGetData** can be used to retrieve data from the column.  
  
 To send data at execution time, the application does the following:  
  
1.  Places a 32-bit value in the rowset buffer instead of a data value. This value will be returned to the application later, so the application should set it to a meaningful value, such as the number of the column or the handle of a file containing data.  
  
2.  Sets the value in the length/indicator buffer to the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro. This value indicates to the driver that the data for the parameter will be sent with **SQLPutData**. The *length* value is used when sending long data to a data source that needs to know how many bytes of long data will be sent so that it can preallocate space. To determine whether a data source requires this value, the application calls **SQLGetInfo** with the SQL_NEED_LONG_DATA_LEN option. All drivers must support this macro; if the data source does not require the byte length, the driver can ignore it.  
  
3.  Calls **SQLBulkOperations** or **SQLSetPos**. The driver discovers that a length/indicator buffer contains the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro and returns SQL_NEED_DATA as the return value of the function.  
  
4.  Calls **SQLParamData** in response to the SQL_NEED_DATA return value. If long data needs to be sent, **SQLParamData** returns SQL_NEED_DATA. In the buffer pointed to by the *ValuePtrPtr* argument, the driver returns the unique value that the application placed in the rowset buffer. If there is more than one data-at-execution column, the application uses this value to determine which column to send data for; the driver is not required to request data for data-at-execution columns in any particular order.  
  
5.  Calls **SQLPutData** to send the column data to the driver. If the column data does not fit in a single buffer, as is often the case with long data, the application calls **SQLPutData** repeatedly to send the data in parts; it is up to the driver and data source to reassemble the data. If the application passes null-terminated string data, the driver or data source must remove the null-termination character as part of the reassembly process.  
  
6.  Calls **SQLParamData** again to indicate that it has sent all of the data for the column. If there are any data-at-execution columns for which data has not been sent, the driver returns SQL_NEED_DATA and the unique value for the next data-at-execution column; the application returns to step 5. If data has been sent for all data-at-execution columns, the data for the row is sent to the data source. **SQLParamData** then returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO and can return any SQLSTATE that **SQLBulkOperations** or **SQLSetPos** can return.  
  
 After **SQLBulkOperations** or **SQLSetPos** returns SQL_NEED_DATA and before data has been completely sent for the last data-at-execution column, the statement is in a Need Data state. In this state, the application can call only **SQLPutData**, **SQLParamData**, **SQLCancel**, **SQLGetDiagField**, or **SQLGetDiagRec**; all other functions return SQLSTATE HY010 (Function sequence error). Calling **SQLCancel** cancels execution of the statement and returns it to its previous state. For more information, see [Appendix B: ODBC State Transition Tables](../../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md).
