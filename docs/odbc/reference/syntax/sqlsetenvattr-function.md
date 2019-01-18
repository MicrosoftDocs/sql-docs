---
title: "SQLSetEnvAttr Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLSetEnvAttr"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLSetEnvAttr"
helpviewer_keywords: 
  - "SQLSetEnvAttr function [ODBC]"
ms.assetid: 0343241c-4b15-4d4b-aa2b-2e8ab5215cd2
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetEnvAttr Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLSetEnvAttr** sets attributes that govern aspects of environments.  
  
## Syntax  
  
```  
  
SQLRETURN SQLSetEnvAttr(  
     SQLHENV      EnvironmentHandle,  
     SQLINTEGER   Attribute,  
     SQLPOINTER   ValuePtr,  
     SQLINTEGER   StringLength);  
```  
  
## Arguments  
 *EnvironmentHandle*  
 [Input] Environment handle.  
  
 *Attribute*  
 [Input] Attribute to set, listed in "Comments."  
  
 *ValuePtr*  
 [Input] Pointer to the value to be associated with *Attribute*. Depending on the value of *Attribute*, *ValuePtr* will be a 32-bit integer value or point to a null-terminated character string.  
  
 *StringLength*  
 [Input] If *ValuePtr* points to a character string or a binary buffer, this argument should be the length of **ValuePtr*. For character string data, this argument should contain the number of bytes in the string.  
  
 If *ValuePtr* is an integer, *StringLength* is ignored.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLSetEnvAttr** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_ENV and a *Handle* of *EnvironmentHandle*. The following table lists the SQLSTATE values typically returned by **SQLSetEnvAttr** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise. If a driver does not support an environment attribute, the error can be returned only during connect time.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S02|Option value changed|The driver did not support the value specified in *ValuePtr* and substituted a similar value. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY009|Invalid use of null pointer|The Attribute argument identified an environment attribute that required a string value, and the *ValuePtr* argument was a null pointer.|  
|HY010|Function sequence error|(DM) A connection handle has been allocated on *EnvironmentHandle*.<br /><br /> (DM) **SQL_ATTR_ODBC_VERSION** has not been set with **SQLSetEnvAttr** and *Attribute* is not equal to **SQL_ATTR_ODBC_VERSION**. You do not need to set **SQL_ATTR_ODBC_VERSION** explicitly if you are using **SQLAllocHandleStd**.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY024|Invalid attribute value|Given the specified *Attribute* value, an invalid value was specified in *ValuePtr*.|  
|HY090|Invalid string or buffer length|The *StringLength* argument was less than 0 but was not SQL_NTS.|  
|HY092|Invalid attribute/option identifier|(DM) The value specified for the argument *Attribute* was not valid for the version of ODBC supported by the driver.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The value specified for the argument *Attribute* was a valid ODBC environment attribute for the version of ODBC supported by the driver, but was not supported by the driver.<br /><br /> (DM) The *Attribute* argument was SQL_ATTR_OUTPUT_NTS, and *ValuePtr* was SQL_FALSE.|  
  
## Comments  
 An application can call **SQLSetEnvAttr** only if no connection handle is allocated on the environment. All environment attributes successfully set by the application for the environment persist until **SQLFreeHandle** is called on the environment. More than one environment handle can be allocated simultaneously in ODBC 3*.x*.  
  
 The format of information set through *ValuePtr* depends on the specified *Attribute*. **SQLSetEnvAttr** will accept attribute information in one of two different formats: a null-terminated character string or a 32-bit integer value. The format of each is noted in the attribute's description.  
  
 There are no driver-specific environment attributes.  
  
 Connection attributes cannot be set by a call to **SQLSetEnvAttr**. Trying to do this will return SQLSTATE HY092 (Invalid attribute/option identifier).  
  
|*Attribute*|*ValuePtr* contents|  
|-----------------|-------------------------|  
|SQL_ATTR_CONNECTION_POOLING (ODBC 3.8)|A 32-bit SQLUINTEGER value that enables or disables connection pooling at the environment level. The following values are used:<br /><br /> SQL_CP_OFF = Connection pooling is turned off. This is the default.<br /><br /> SQL_CP_ONE_PER_DRIVER = A single connection pool is supported for each driver. Every connection in a pool is associated with one driver.<br /><br /> SQL_CP_ONE_PER_HENV = A single connection pool is supported for each environment. Every connection in a pool is associated with one environment.<br /><br /> SQL_CP_DRIVER_AWARE = Use the connection-pool awareness feature of the driver, if it is available. If the driver does not support connection-pool awareness, SQL_CP_DRIVER_AWARE is ignored and SQL_CP_ONE_PER_HENV is used. For more information, see [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md). In an environment where some drivers support and some drivers do not support connection-pool awareness, SQL_CP_DRIVER_AWARE can enable the connection-pool awareness feature on those supporting drivers, but it is equivalent to setting to SQL_CP_ONE_PER_HENV on those drivers that do not support connection-pool awareness feature.<br /><br /> Connection pooling is enabled by calling **SQLSetEnvAttr** to set the SQL_ATTR_CONNECTION_POOLING attribute to SQL_CP_ONE_PER_DRIVER or SQL_CP_ONE_PER_HENV. This call must be made before the application allocates the shared environment for which connection pooling is to be enabled. The environment handle in the call to **SQLSetEnvAttr** is set to null, which makes SQL_ATTR_CONNECTION_POOLING a process-level attribute. After connection pooling is enabled, the application then allocates an implicit shared environment by calling **SQLAllocHandle** with the *InputHandle* argument set to SQL_HANDLE_ENV.<br /><br /> After connection pooling has been enabled and a shared environment has been selected for an application, SQL_ATTR_CONNECTION_POOLING cannot be reset for that environment, because **SQLSetEnvAttr** is called with a null environment handle when setting this attribute. If this attribute is set while connection pooling is already enabled on a shared environment, the attribute affects only shared environments that are allocated subsequently.<br /><br /> It is also possible to enable connection pooling on an environment. Note the following about environment connection pooling:<br /><br /> -   Enabling connection pooling on a NULL handle is a process-level attribute. Subsequently allocated environments will be a shared environment, and will inherit the process-level connection pooling setting.<br />-   After an environment is allocated, an application can still change its connection pool setting.<br />-   If environment connection pooling is enabled and the connection's driver uses driver pooling, environment pooling takes preference.<br /><br /> SQL_ATTR_CONNECTION_POOLING is implemented inside the Driver Manager. A driver does not need to implement SQL_ATTR_CONNECTION_POOLING. ODBC 2.0 and 3.0 applications can set this environment attribute.<br /><br /> For more information, see [ODBC Connection Pooling](../../../odbc/reference/develop-app/driver-manager-connection-pooling.md).|  
|SQL_ATTR_CP_MATCH (ODBC 3.0)|A 32-bit SQLUINTEGER value that determines how a connection is chosen from a connection pool. When **SQLConnect** or **SQLDriverConnect** is called, the Driver Manager determines which connection is reused from the pool. The Driver Manager tries to match the connection options in the call and the connection attributes set by the application to the keywords and connection attributes of the connections in the pool. The value of this attribute determines the level of precision of the matching criteria.<br /><br /> The following values are used to set the value of this attribute:<br /><br /> SQL_CP_STRICT_MATCH = Only connections that exactly match the connection options in the call and the connection attributes set by the application are reused. This is the default.<br /><br /> SQL_CP_RELAXED_MATCH = Connections with matching connection string keywords can be used. Keywords must match, but not all connection attributes must match.<br /><br /> For more information about how the Driver Manager performs the match in connecting to a pooled connection, see [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md). For more information about connection pooling, see [ODBC Connection Pooling](../../../odbc/reference/develop-app/driver-manager-connection-pooling.md).|  
|SQL_ATTR_ODBC_VERSION (ODBC 3.0)|A 32-bit integer that determines whether certain functionality exhibits ODBC 2*.x* behavior or ODBC 3*.x* behavior. The following values are used to set the value of this attribute:<br /><br /> SQL_OV_ODBC3_80 = The Driver Manager and driver exhibit the following ODBC 3.8 behavior:<br /><br /> -   The driver returns and expects ODBC 3.*x* codes for date, time, and timestamp.<br />-   The driver returns ODBC 3.*x* SQLSTATE codes when **SQLError**, **SQLGetDiagField**, or **SQLGetDiagRec** is called.<br />-   The *CatalogName* argument in a call to **SQLTables** accepts a search pattern.<br />-   The Driver Manager supports C data type extensibility. For more information about C data type extensibility, see [C Data Types in ODBC](../../../odbc/reference/develop-app/c-data-types-in-odbc.md).<br /><br /> For more information, see [What's New in ODBC 3.8](../../../odbc/reference/what-s-new-in-odbc-3-8.md).<br /><br /> SQL_OV_ODBC3 = The Driver Manager and driver exhibit the following ODBC 3*.x* behavior:<br /><br /> -   The driver returns and expects ODBC 3*.x* codes for date, time, and timestamp.<br />-   The driver returns ODBC 3*.x* SQLSTATE codes when **SQLError**, **SQLGetDiagField**, or **SQLGetDiagRec** is called.<br />-   The *CatalogName* argument in a call to **SQLTables** accepts a search pattern.<br />-   The Driver Manager does not support C data type extensibility.<br /><br /> SQL_OV_ODBC2 = The Driver Manager and driver exhibit the following ODBC 2*.x* behavior. This is especially useful for an ODBC 2*.x* application working with an ODBC 3*.x* driver.<br /><br /> -   The driver returns and expects ODBC 2*.x* codes for date, time, and timestamp.<br />-   The driver returns ODBC 2*.x* SQLSTATE codes when **SQLError**, **SQLGetDiagField**, or **SQLGetDiagRec** is called.<br />-   The *CatalogName* argument in a call to **SQLTables** does not accept a search pattern.<br />-   The Driver Manager does not support C data type extensibility.<br /><br /> An application must set this environment attribute before it calls any function that has an SQLHENV argument, or the call will return SQLSTATE HY010 (Function sequence error). It is driver-specific whether additional behavior exists for these environmental flags.<br /><br /> -   For more information, see [Declaring the Application's ODBC Version](../../../odbc/reference/develop-app/declaring-the-application-s-odbc-version.md) and [Behavioral Changes](../../../odbc/reference/develop-app/behavioral-changes.md).|  
|SQL_ATTR_OUTPUT_NTS (ODBC 3.0)|A 32-bit integer that determines how the driver returns string data. If SQL_TRUE, the driver returns string data null-terminated. If SQL_FALSE, the driver does not return string data null-terminated.<br /><br /> This attribute defaults to SQL_TRUE. A call to **SQLSetEnvAttr** to set it to SQL_TRUE returns SQL_SUCCESS. A call to **SQLSetEnvAttr** to set it to SQL_FALSE returns SQL_ERROR and SQLSTATE HYC00 (Optional feature not implemented).|  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Allocating a handle|[SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)|  
|Returning the setting of an environment attribute|[SQLGetEnvAttr Function](../../../odbc/reference/syntax/sqlgetenvattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [What's New in ODBC 3.8](../../../odbc/reference/what-s-new-in-odbc-3-8.md)
