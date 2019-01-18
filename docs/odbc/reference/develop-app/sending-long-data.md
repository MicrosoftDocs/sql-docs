---
title: "Sending Long Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "long data [ODBC]"
  - "sending long data [ODBC]"
ms.assetid: ea989084-a8e6-4737-892e-9ec99dd49caf
author: MightyPen
ms.author: genemi
manager: craigg
---
# Sending Long Data
DBMSs define *long data* as any character or binary data over a certain size, such as 254 characters. It might not be possible to store an entire item of long data in memory, such as when the item represents a long text document or a bitmap. Because such data cannot be stored in a single buffer, the data source sends it to the driver in parts with **SQLPutData** when the statement is executed. Parameters for which data is sent at execution time are known as *data-at-execution parameters*.  
  
> [!NOTE]  
>  An application can actually send any type of data at execution time with **SQLPutData**, although only character and binary data can be sent in parts. However, if the data is small enough to fit in a single buffer, there is generally no reason to use **SQLPutData**. It is much easier to bind the buffer and let the driver retrieve the data from the buffer.  
  
 To send data at execution time, the application performs the following actions:  
  
1.  Passes a 32-bit value that identifies the parameter in the *ParameterValuePtr* argument in **SQLBindParameter** rather than passing the address of a buffer. This value is not analyzed by the driver. It will be returned to the application later, so it should mean something to the application. For example, it might be the number of the parameter or the handle of a file containing data.  
  
2.  Passes the address of a length/indicator buffer in the *StrLen_or_IndPtr* argument of **SQLBindParameter**.  
  
3.  Stores SQL_DATA_AT_EXEC or the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro in the length/indicator buffer. Both of these values indicate to the driver that the data for the parameter will be sent with **SQLPutData**. SQL_LEN_DATA_AT_EXEC(*length*) is used when sending long data to a data source that needs to know how many bytes of long data will be sent so that it can preallocate space. To determine if a data source requires this value, the application calls **SQLGetInfo** with the SQL_NEED_LONG_DATA_LEN option. All drivers must support this macro; if the data source does not require the byte length, the driver can ignore it.  
  
4.  Calls **SQLExecute** or **SQLExecDirect**. The driver discovers that a length/indicator buffer contains the value SQL_DATA_AT_EXEC or the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro and returns SQL_NEED_DATA as the return value of the function.  
  
5.  Calls **SQLParamData** in response to the SQL_NEED_DATA return value. If long data needs to be sent, **SQLParamData** returns SQL_NEED_DATA. In the buffer pointed to by the *ValuePtrPtr* argument, the driver returns the value that identifies the data-at-execution parameter. If there is more than one data-at-execution parameter, the application must use this value to determine which parameter to send data for; the driver is not required to request data for data-at-execution parameters in any particular order.  
  
6.  Calls **SQLPutData** to send the parameter data to the driver. If the parameter data does not fit into a single buffer, as is often the case with long data, the application calls **SQLPutData** repeatedly to send the data in parts; it is up to the driver and data source to reassemble the data. If the application passes null-terminated string data, the driver or data source must remove the null-termination character as part of the reassembly process.  
  
7.  Calls **SQLParamData** again to indicate that it has sent all of the data for the parameter. If there are any data-at-execution parameters for which data has not been sent, the driver returns SQL_NEED_DATA and the value that identifies the next parameter; the application returns to step 6. If data has been sent for all data-at-execution parameters, the statement is executed. **SQLParamData** returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO and can return any return value or diagnostic that **SQLExecute** or **SQLExecDirect** can return.  
  
 After **SQLExecute** or **SQLExecDirect** returns SQL_NEED_DATA and before data has been completely sent for the last data-at-execution parameter, the statement is in a Need Data state. While a statement is in a Need Data state, the application can call only **SQLPutData**, **SQLParamData**, **SQLCancel**, **SQLGetDiagField**, or **SQLGetDiagRec**; all other functions return SQLSTATE HY010 (Function sequence error). Calling **SQLCancel** cancels execution of the statement and returns it to its previous state. For more information, see [Appendix B: ODBC State Transition Tables](../../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md).  
  
 For an example of sending data at execution time, see the [SQLPutData](../../../odbc/reference/syntax/sqlputdata-function.md) function description.
