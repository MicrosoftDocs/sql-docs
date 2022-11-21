---
description: "SQLCopyDesc Function"
title: "SQLCopyDesc Function | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLCopyDesc"
apilocation: 
  - "sqlsrv32.dll"
  - "odbc32.dll"
  - "Msodbcsql11.dll"
  - "Sqlncli10.dll"
  - "Sqlncli11.dll"
  - "Sqlncli11e.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLCopyDesc"
helpviewer_keywords: 
  - "SQLCopyDesc function [ODBC]"
ms.assetid: d5450895-3824-44c4-8aa4-d4f9752a9602
author: David-Engel
ms.author: v-davidengel
---
# SQLCopyDesc Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLCopyDesc** copies descriptor information from one descriptor handle to another.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLCopyDesc(  
     SQLHDESC     SourceDescHandle,  
     SQLHDESC     TargetDescHandle);  
```  
  
## Arguments  
 *SourceDescHandle*  
 [Input] Source descriptor handle.  
  
 *TargetDescHandle*  
 [Input] Target descriptor handle. The *TargetDescHandle* argument can be a handle to an application descriptor or an IPD. *TargetDescHandle* cannot be set to a handle to an IRD, or **SQLCopyDesc** will return SQLSTATE HY016 (Cannot modify an implementation row descriptor).  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLCopyDesc** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DESC and a *Handle* of *TargetDescHandle*. If an invalid *SourceDescHandle* was passed in the call, SQL_INVALID_HANDLE will be returned but no SQLSTATE will be returned. The following table lists the SQLSTATE values commonly returned by **SQLCopyDesc** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
 When an error is returned, the call to **SQLCopyDesc** is immediately aborted, and the contents of the fields in the *TargetDescHandle* descriptor are undefined.  
  
 Because **SQLCopyDesc** may be implemented by calling **SQLGetDescField** and **SQLSetDescField**, **SQLCopyDesc** may return SQLSTATEs returned by **SQLGetDescField** or **SQLSetDescField**.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate the memory required to support execution or completion of the function.|  
|HY007|Associated statement is not prepared|*SourceDescHandle* was associated with an IRD, and the associated statement handle was not in the prepared or executed state.|  
|HY010|Function sequence error|(DM) The descriptor handle in *SourceDescHandle* or *TargetDescHandle* was associated with a *StatementHandle* for which an asynchronously executing function (not this one) was called and was still executing when this function was called.<br /><br /> (DM) The descriptor handle in *SourceDescHandle* or *TargetDescHandle* was associated with a *StatementHandle* for which **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) An asynchronously executing function was called for the connection handle that is associated with the *SourceDescHandle* or *TargetDescHandle*. This asynchronous function was still executing when the **SQLCopyDesc** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for one of the statement handles associated with the *SourceDescHandle* or *TargetDescHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY016|Cannot modify an implementation row descriptor|*TargetDescHandle* was associated with an IRD.|  
|HY021|Inconsistent descriptor information|The descriptor information checked during a consistency check was not consistent. For more information, see "Consistency Checks" in **SQLSetDescField**.|  
|HY092|Invalid attribute/option identifier|The call to **SQLCopyDesc** prompted a call to **SQLSetDescField**, but *\*ValuePtr* was not valid for the *FieldIdentifier* argument on *TargetDescHandle*.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *SourceDescHandle* or *TargetDescHandle* does not support the function.|  
  
## Comments  
 A call to **SQLCopyDesc** copies the fields of the source descriptor handle to the target descriptor handle. Fields can be copied only to an application descriptor or an IPD, but not to an IRD. Fields can be copied from either an application or an implementation descriptor.  
  
 Fields can be copied from an IRD only if the statement handle is in the prepared or executed state; otherwise, the function returns SQLSTATE HY007 (Associated statement is not prepared).  
  
 Fields can be copied from an IPD whether or not a statement has been prepared. If an SQL statement with dynamic parameters has been prepared and automatic population of the IPD is supported and enabled, then the IPD is populated by the driver. When **SQLCopyDesc** is called with the IPD as the *SourceDescHandle*, the populated fields are copied. If the IPD is not populated by the driver, the contents of the fields originally in the IPD are copied.  
  
 All fields of the descriptor, except SQL_DESC_ALLOC_TYPE (which specifies whether the descriptor handle was automatically or explicitly allocated), are copied, whether or not the field is defined for the destination descriptor. Copied fields overwrite the existing fields.  
  
 The driver copies all descriptor fields if the *SourceDescHandle* and *TargetDescHandle* arguments are associated with the same driver, even if the drivers are on two different connections or environments. If the *SourceDescHandle* and *TargetDescHandle* arguments are associated with different drivers, the Driver Manager copies ODBC-defined fields, but does not copy driver-defined fields or fields that are not defined by ODBC for the type of descriptor.  
  
 The call to **SQLCopyDesc** is aborted immediately if an error occurs.  
  
 When the SQL_DESC_DATA_PTR field is copied, a consistency check is performed on the target descriptor. If the consistency check fails, SQLSTATE HY021 (Inconsistent descriptor information) is returned and the call to **SQLCopyDesc** is immediately aborted. For more information on consistency checks, see "Consistency Checks" in [SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md).  
  
 Descriptor handles can be copied across connections even if the connections are under different environments. If the Driver Manager detects that the source and the destination descriptor handles do not belong to the same connection and the two connections belong to separate drivers, it implements **SQLCopyDesc** by performing a field-by-field copy using **SQLGetDescField** and **SQLSetDescField**.  
  
 When **SQLCopyDesc** is called with a *SourceDescHandle* on one driver and a *TargetDescHandle* on another driver, the error queue of the *SourceDescHandle* is cleared. This occurs because **SQLCopyDesc** in this case is implemented by calls to **SQLGetDescField** and **SQLSetDescField**.  
  
> [!NOTE]  
>  An application might be able to associate an explicitly allocated descriptor handle with a *StatementHandle*, rather than calling **SQLCopyDesc** to copy fields from one descriptor to another. An explicitly allocated descriptor can be associated with another *StatementHandle* on the same *ConnectionHandle* by setting the SQL_ATTR_APP_ROW_DESC or SQL_ATTR_APP_PARAM_DESC statement attribute to the handle of the explicitly allocated descriptor. When this is done, **SQLCopyDesc** does not have to be called to copy descriptor field values from one descriptor to another. A descriptor handle cannot be associated with a *StatementHandle* on another *ConnectionHandle*, however; to use the same descriptor field values on *StatementHandles* on different *ConnectionHandles*, **SQLCopyDesc** has to be called.  
  
 For a description of the fields in a descriptor header or record, see [SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md). For more information on descriptors, see [Descriptors](../../../odbc/reference/develop-app/descriptors.md).  
  
## Copying Rows Between Tables  
 An application may copy data from one table to another without copying the data at the application level. To do this, the application binds the same data buffers and descriptor information to a statement that fetches the data and the statement that inserts the data into a copy. This can be accomplished either by sharing an application descriptor (binding an explicitly allocated descriptor as both the ARD to one statement and the APD in another) or by using **SQLCopyDesc** to copy the bindings between the ARD and the APD of the two statements. If the statements are on different connections, **SQLCopyDesc** must be used. In addition, **SQLCopyDesc** has to be called to copy the bindings between the IRD and the IPD of the two statements. When copying across statements on the same connection, the SQL_ACTIVE_STATEMENTS information type returned by the driver for a call to **SQLGetInfo** must be greater than 1 for this operation to succeed. (This is not the case when copying across connections.)  
  
### Code Example  
 In the following example, descriptor operations are used to copy the fields of the PartsSource table into the PartsCopy table. The contents of the PartsSource table are fetched into rowset buffers in *hstmt0*. These values are used as parameters of an INSERT statement on *hstmt1* to populate the columns of the PartsCopy table. To do so, the fields of the IRD of *hstmt0* are copied to the fields of the IPD of *hstmt1*, and the fields of the ARD of *hstmt0* are copied to the fields of the APD of *hstmt1*. Use **SQLSetDescField** to set the IPD's SQL_DESC_PARAMETER_TYPE attribute to SQL_PARAM_INPUT when you copy IRD fields from a statement with output parameters to IPD fields that need to be input parameters.  
  
```cpp  
#define ROWS 100  
#define DESC_LEN 50  
#define SQL_SUCCEEDED(rc) (rc == SQL_SUCCESS || rc == SQL_SUCCESS_WITH_INFO)  
  
// Template for a row  
typedef struct {  
   SQLINTEGER   sPartID;  
   SQLINTEGER   cbPartID;  
   SQLUCHAR     szDescription[DESC_LENGTH];  
   SQLINTEGER   cbDescription;  
   REAL         sPrice;  
   SQLINTEGER   cbPrice;  
} PartsSource;  
  
PartsSource    rget[ROWS];          // rowset buffer  
SQLUSMALLINT   sts_ptr[ROWS];       // status pointer  
SQLHSTMT       hstmt0, hstmt1;  
SQLHDESC       hArd0, hIrd0, hApd1, hIpd1;  
  
// ARD and IRD of hstmt0  
SQLGetStmtAttr(hstmt0, SQL_ATTR_APP_ROW_DESC, &hArd0, 0, NULL);  
SQLGetStmtAttr(hstmt0, SQL_ATTR_IMP_ROW_DESC, &hIrd0, 0, NULL);  
  
// APD and IPD of hstmt1  
SQLGetStmtAttr(hstmt1, SQL_ATTR_APP_PARAM_DESC, &hApd1, 0, NULL);  
SQLGetStmtAttr(hstmt1, SQL_ATTR_IMP_PARAM_DESC, &hIpd1, 0, NULL);  
  
// Use row-wise binding on hstmt0 to fetch rows  
SQLSetStmtAttr(hstmt0, SQL_ATTR_ROW_BIND_TYPE, (SQLPOINTER) sizeof(PartsSource), 0);  
  
// Set rowset size for hstmt0  
SQLSetStmtAttr(hstmt0, SQL_ATTR_ROW_ARRAY_SIZE, (SQLPOINTER) ROWS, 0);  
  
// Execute a select statement  
SQLExecDirect(hstmt0, "SELECT PARTID, DESCRIPTION, PRICE FROM PARTS ORDER BY 3, 1, 2"",  
               SQL_NTS);  
  
// Bind  
SQLBindCol(hstmt0, 1, SQL_C_SLONG, rget[0].sPartID, 0,   
   &rget[0].cbPartID);  
SQLBindCol(hstmt0, 2, SQL_C_CHAR, &rget[0].szDescription, DESC_LEN,   
   &rget[0].cbDescription);  
SQLBindCol(hstmt0, 3, SQL_C_FLOAT, rget[0].sPrice,   
   0, &rget[0].cbPrice);  
  
// Perform parameter bindings on hstmt1.   
SQLCopyDesc(hArd0, hApd1);  
SQLCopyDesc(hIrd0, hIpd1);  
  
// Set the array status pointer of IRD  
SQLSetStmtAttr(hstmt0, SQL_ATTR_ROW_STATUS_PTR, sts_ptr, SQL_IS_POINTER);  
  
// Set the ARRAY_STATUS_PTR field of APD to be the same  
// as that in IRD.  
SQLSetStmtAttr(hstmt1, SQL_ATTR_PARAM_OPERATION_PTR, sts_ptr, SQL_IS_POINTER);  
  
// Set the hIpd1 records as input parameters  
rc = SQLSetDescField(hIpd1, 1, SQL_DESC_PARAMETER_TYPE, (SQLPOINTER)SQL_PARAM_INPUT, SQL_IS_INTEGER);  
rc = SQLSetDescField(hIpd1, 2, SQL_DESC_PARAMETER_TYPE, (SQLPOINTER)SQL_PARAM_INPUT, SQL_IS_INTEGER);  
rc = SQLSetDescField(hIpd1, 3, SQL_DESC_PARAMETER_TYPE, (SQLPOINTER)SQL_PARAM_INPUT, SQL_IS_INTEGER);  
  
// Prepare an insert statement on hstmt1. PartsCopy is a copy of  
// PartsSource  
SQLPrepare(hstmt1, "INSERT INTO PARTS_COPY VALUES (?, ?, ?)", SQL_NTS);  
  
// In a loop, fetch a rowset, and copy the fetched rowset to PARTS_COPY  
  
rc = SQLFetchScroll(hstmt0, SQL_FETCH_NEXT, 0);  
while (SQL_SUCCEEDED(rc)) {  
  
   // After the call to SQLFetchScroll, the status array has row   
   // statuses. This array is used as input status in the APD  
   // and hence determines which elements of the rowset buffer  
   // are inserted.  
   SQLExecute(hstmt1);  
  
   rc = SQLFetchScroll(hstmt0, SQL_FETCH_NEXT, 0);  
} // while  
```  
  
### Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Getting multiple descriptor fields|[SQLGetDescRec Function](../../../odbc/reference/syntax/sqlgetdescrec-function.md)|  
|Setting a single descriptor field|[SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md)|  
|Setting multiple descriptor fields|[SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
