---
description: "SQLDrivers Function"
title: "SQLDrivers Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLDrivers"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLDrivers"
helpviewer_keywords: 
  - "SQLDrivers function [ODBC]"
ms.assetid: 6b5b7514-e9cb-4cfd-8b7a-ab51dfab9efa
author: David-Engel
ms.author: v-davidengel
---
# SQLDrivers Function
**Conformance**  
 Version Introduced: ODBC 2.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLDrivers** lists driver descriptions and driver attribute keywords. This function is implemented only by the Driver Manager.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLDrivers(  
     SQLHENV         EnvironmentHandle,  
     SQLUSMALLINT    Direction,  
     SQLCHAR *       DriverDescription,  
     SQLSMALLINT     BufferLength1,  
     SQLSMALLINT *   DescriptionLengthPtr,  
     SQLCHAR *       DriverAttributes,  
     SQLSMALLINT     BufferLength2,  
     SQLSMALLINT *   AttributesLengthPtr);  
```  
  
## Arguments  
 *EnvironmentHandle*  
 [Input] Environment handle.  
  
 *Direction*  
 [Input] Determines whether the Driver Manager fetches the next driver description in the list (SQL_FETCH_NEXT) or whether the search starts from the beginning of the list (SQL_FETCH_FIRST).  
  
 *DriverDescription*  
 [Output] Pointer to a buffer in which to return the driver description.  
  
 If *DriverDescription* is NULL, *DescriptionLengthPtr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *DriverDescription*.  
  
 *BufferLength1*  
 [Input] Length of the **DriverDescription* buffer, in characters.  
  
 *DescriptionLengthPtr*  
 [Output] Pointer to a buffer in which to return the total number of characters (excluding the null-termination character) available to return in \**DriverDescription*. If the number of characters available to return is greater than or equal to *BufferLength1*, the driver description in \**DriverDescription* is truncated to *BufferLength1* minus the length of a null-termination character.  
  
 *DriverAttributes*  
 [Output] Pointer to a buffer in which to return the list of driver attribute value pairs (see "Comments").  
  
 If *DriverAttributes* is NULL, *AttributesLengthPtr* will still return the total number of bytes (excluding the null-termination character for character data) available to return in the buffer pointed to by *DriverAttributes*.  
  
 *BufferLength2*  
 [Input] Length of the \**DriverAttributes* buffer, in characters. If the *\*DriverDescription* value is a Unicode string (when calling **SQLDriversW**), the *BufferLength* argument must be an even number.  
  
 *AttributesLengthPtr*  
 [Output] Pointer to a buffer in which to return the total number of bytes (excluding the null-termination byte) available to return in \**DriverAttributes*. If the number of bytes available to return is greater than or equal to *BufferLength2*, the list of attribute value pairs in \**DriverAttributes* is truncated to *BufferLength2* minus the length of the null-termination character.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLDrivers** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_ENV and a *Handle* of *EnvironmentHandle*. The following table lists the SQLSTATE values typically returned by **SQLDrivers** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|(DM) Driver Manager-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|(DM) The buffer \**DriverDescription* was not large enough to return the complete driver description. Therefore, the description was truncated. The length of the complete driver description is returned in \**DescriptionLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)<br /><br /> (DM) The buffer \**DriverAttributes* was not large enough to return the complete list of attribute value pairs. Therefore, the list was truncated. The length of the untruncated list of attribute value pairs is returned in **AttributesLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|(DM) The Driver Manager was unable to allocate memory that is required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value specified for argument *BufferLength1* was less than 0.<br /><br /> (DM) The value specified for argument *BufferLength2* was less than 0 or equal to 1.|  
|HY103|Invalid retrieval code|(DM) The value specified for the argument *Direction* was not equal to SQL_FETCH_FIRST or SQL_FETCH_NEXT.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
  
## Comments  
 **SQLDrivers** returns the driver description in the \**DriverDescription* buffer. It returns additional information about the driver in the \**DriverAttributes* buffer as a list of keyword-value pairs. All keywords listed in the system information for drivers will be returned for all drivers, except for **CreateDSN**, which is used to prompt creation of data sources and therefore is optional. Each pair is terminated with a null byte, and the complete list is terminated with a null byte (that is, two null bytes mark the end of the list). For example, a file-based driver using C syntax might return the following list of attributes ("\0" represents a null character):  
  
```  
FileUsage=1\0FileExtns=*.dbf\0\0  
```  
  
 If \**DriverAttributes* is not large enough to hold the entire list, the list is truncated, **SQLDrivers** returns SQLSTATE 01004 (Data truncated), and the length of the list (excluding the final null-termination byte) is returned in **AttributesLengthPtr*.  
  
 Driver attribute keywords are added from the system information when the driver is installed. For more information, see [Installing ODBC Components](../../../odbc/reference/install/installing-odbc-components.md).  
  
 An application can call **SQLDrivers** multiple times to retrieve all driver descriptions. The Driver Manager retrieves this information from the system information. When there are no more driver descriptions, **SQLDrivers** returns SQL_NO_DATA. If **SQLDrivers** is called with SQL_FETCH_NEXT immediately after it returns SQL_NO_DATA, it returns the first driver description. For information about how an application uses the information returned by **SQLDrivers**, see [Choosing a Data Source or Driver](../../../odbc/reference/develop-app/choosing-a-data-source-or-driver.md).  
  
 If SQL_FETCH_NEXT is passed to **SQLDrivers** the very first time it is called, **SQLDrivers** returns the first data source name.  
  
 Because **SQLDrivers** is implemented in the Driver Manager, it is supported for all drivers regardless of a particular driver's standards compliance.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Discovering and listing values required to connect to a data source|[SQLBrowseConnect Function](../../../odbc/reference/syntax/sqlbrowseconnect-function.md)|  
|Connecting to a data source|[SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md)|  
|Returning data source names|[SQLDataSources Function](../../../odbc/reference/syntax/sqldatasources-function.md)|  
|Connecting to a data source using a connection string or dialog box|[SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
