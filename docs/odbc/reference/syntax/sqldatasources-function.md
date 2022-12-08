---
description: "SQLDataSources Function"
title: "SQLDataSources Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLDataSources"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLDataSources"
helpviewer_keywords: 
  - "SQLDataSources function [ODBC]"
ms.assetid: 3f63b1b4-e70e-44cd-96c6-6878d50d0117
author: David-Engel
ms.author: v-davidengel
---
# SQLDataSources Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLDataSources** returns information about a data source. This function is implemented only by the Driver Manager.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLDataSources(  
     SQLHENV          EnvironmentHandle,  
     SQLUSMALLINT     Direction,  
     SQLCHAR *        ServerName,  
     SQLSMALLINT      BufferLength1,  
     SQLSMALLINT *    NameLength1Ptr,  
     SQLCHAR *        Description,  
     SQLSMALLINT      BufferLength2,  
     SQLSMALLINT *    NameLength2Ptr);  
```  
  
## Arguments  
 *EnvironmentHandle*  
 [Input] Environment handle.  
  
 *Direction*  
 [Input] Determines which data source the Driver Manager returns information about. Can be:  
  
 SQL_FETCH_NEXT (to fetch the next data source name in the list), SQL_FETCH_FIRST (to fetch from the beginning of the list), SQL_FETCH_FIRST_USER (to fetch the first user DSN), or SQL_FETCH_FIRST_SYSTEM (to fetch the first system DSN).  
  
 When *Direction* is set to SQL_FETCH_FIRST, subsequent calls to **SQLDataSources** with *Direction* set to SQL_FETCH_NEXT return both user and system DSNs. When *Direction* is set to SQL_FETCH_FIRST_USER, all subsequent calls to **SQLDataSources** with *Direction* set to SQL_FETCH_NEXT return only user DSNs. When *Direction* is set to SQL_FETCH_FIRST_SYSTEM, all subsequent calls to **SQLDataSources** with *Direction* set to SQL_FETCH_NEXT return only system DSNs.  
  
 *ServerName*  
 [Output] Pointer to a buffer in which to return the data source name.  
  
 If *ServerName* is NULL, *NameLength1Ptr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *ServerName*.  
  
 *BufferLength1*  
 [Input] Length of the **ServerName* buffer, in characters; this does not need to be longer than SQL_MAX_DSN_LENGTH plus the null-termination character.  
  
 *NameLength1Ptr*  
 [Output] Pointer to a buffer in which to return the total number of characters (excluding the null-termination character) available to return in \**ServerName*. If the number of characters available to return is greater than or equal to *BufferLength1*, the data source name in \**ServerName* is truncated to *BufferLength1* minus the length of a null-termination character.  
  
 *Description*  
 [Output] Pointer to a buffer in which to return the description of the driver associated with the data source. For example, dBASE or SQL Server.  
  
 If *Description* is NULL, *NameLength2Ptr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *Description*.  
  
 *BufferLength2*  
 [Input] Length in characters of the **Description* buffer.  
  
 *NameLength2Ptr*  
 [Output] Pointer to a buffer in which to return the total number of characters (excluding the null-termination character) available to return in \**Description*. If the number of characters available to return is greater than or equal to *BufferLength2*, the driver description in \**Description* is truncated to *BufferLength2* minus the length of a null-termination character.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLDataSources** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_ENV and a *Handle* of *EnvironmentHandle*. The following table lists the SQLSTATE values typically returned by **SQLDataSources** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|(DM) Driver Manager-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|(DM) The buffer \**ServerName* was not large enough to return the complete data source name. Therefore, the name was truncated. The length of the entire data source name is returned in \**NameLength1Ptr*. (Function returns SQL_SUCCESS_WITH_INFO.)<br /><br /> (DM) The buffer \**Description* was not large enough to return the complete driver description. Therefore, the description was truncated. The length of the untruncated data source description is returned in **NameLength2Ptr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|HY000|General error|(DM) An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|(DM) The Driver Manager was unable to allocate memory that is required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value specified for argument *BufferLength1* was less than 0.<br /><br /> (DM) The value specified for argument *BufferLength2* was less than 0.|  
|HY103|Invalid retrieval code|(DM) The value specified for the argument *Direction* was not equal to SQL_FETCH_FIRST, SQL_FETCH_FIRST_USER, SQL_FETCH_FIRST_SYSTEM, or SQL_FETCH_NEXT.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
  
## Comments  
 Because **SQLDataSources** is implemented in the Driver Manager, it is supported for all drivers regardless of a particular driver's standards compliance.  
  
 An application can call **SQLDataSources** multiple times to retrieve all data source names. The Driver Manager retrieves this information from the system information. When there are no more data source names, the Driver Manager returns SQL_NO_DATA. If **SQLDataSources** is called with SQL_FETCH_NEXT immediately after it returns SQL_NO_DATA, it will return the first data source name. For information about how an application uses the information returned by **SQLDataSources**, see [Choosing a Data Source or Driver](../../../odbc/reference/develop-app/choosing-a-data-source-or-driver.md).  
  
 If SQL_FETCH_NEXT is passed to **SQLDataSources** the very first time it is called, it will return the first data source name.  
  
 The driver determines how data source names are mapped to actual data sources.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Discovering and listing values required to connect to a data source|[SQLBrowseConnect Function](../../../odbc/reference/syntax/sqlbrowseconnect-function.md)|  
|Connecting to a data source|[SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md)|  
|Connecting to a data source using a connection string or dialog box|[SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md)|  
|Returning driver descriptions and attributes|[SQLDrivers Function](../../../odbc/reference/syntax/sqldrivers-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
