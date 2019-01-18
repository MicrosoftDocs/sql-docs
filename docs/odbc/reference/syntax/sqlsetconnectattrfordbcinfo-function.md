---
title: "SQLSetConnectAttrForDbcInfo Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetConnectAttrForDbcInfo function [ODBC]"
ms.assetid: a28fadb9-b998-472a-b252-709507e92005
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetConnectAttrForDbcInfo Function
**Conformance**  
 Version Introduced: ODBC 3.81 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLSetConnectAttrForDbcInfo** is the same as **SQLSetConnectAttr**, but it sets the attribute on the connection information token instead of on the connection handle.  
  
## Syntax  
  
```  
SQLRETURN  SQLSetConnectAttrForDbcInfo(  
                SQLHDBC_INFO_TOKEN    hDbcInfoToken,  
                SQLINTEGER            Attribute,  
                SQLPOINTER            ValuePtr,  
                SQLINTEGER            StringLength );  
```  
  
## Arguments  
 *hDbcInfoToken*  
 [Input] Token handle.  
  
 *Attribute*  
 [Input] Attribute to set. The list of valid attributes is driver specific and the same as for [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md).  
  
 *ValuePtr*  
 [Input] Pointer to the value to be associated with *Attribute*. Depending on the value of *Attribute*, *ValuePtr* will be a 32-bit unsigned integer value or will point to a null-terminated character string. Note that if the *Attribute* argument is a driver-specific value, the value in *ValuePtr* may be a signed integer.  
  
 *StringLength*  
 [Input] If *Attribute* is an ODBC-defined attribute and *ValuePtr* points to a character string or a binary buffer, this argument should be the length of **ValuePtr*. For character string data, this argument should contain the number of bytes in the string.  
  
 If *Attribute* is an ODBC-defined attribute and *ValuePtr* is an integer, *StringLength* is ignored.  
  
 If *Attribute* is a driver-defined attribute, the application indicates the nature of the attribute to the Driver Manager by setting the *StringLength* argument. *StringLength* can have the following values:  
  
-   If *ValuePtr* is a pointer to a character string, then *StringLength* is the length of the string or SQL_NTS.  
  
-   If *ValuePtr* is a pointer to a binary buffer, then the application places the result of the SQL_LEN_BINARY_ATTR(*length*) macro in *StringLength*. This places a negative value in *StringLength*.  
  
-   If *ValuePtr* is a pointer to a value other than a character string or a binary string, then *StringLength* should have the value SQL_IS_POINTER.  
  
-   If *ValuePtr* contains a fixed-length value, then *StringLength* is either SQL_IS_INTEGER or SQL_IS_UINTEGER, as appropriate.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 Same as [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md), except that the Driver Manager will use a **HandleType** of SQL_HANDLE_DBC_INFO_TOKEN and a **Handle** of *hDbcInfoToken*.  
  
## Remarks  
 **SQLSetConnectAttrForDbcInfo** is the same as **SQLSetConnectAttr**, but it sets the attribute on the connection information token, instead of on the connection handle. For example, if **SQLSetConnectAttr** does not recognize an attribute, **SQLSetConnectAttrForDbcInfo** should also return SQL_ERROR for that attribute.  
  
 Whenever driver returns SQL_ERROR or SQL_INVALID_HANDLE, the driver should ignore this attribute to compute the pool ID. Also, the Driver Manager will obtain the diagnostic information from *hDbcInfoToken*, and return SQL_SUCCESS_WITH_INFO to the application in [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) and [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md). Therefore, an application can retrieve details about why some attributes cannot be set.  
  
 Applications should not call this function directly. An ODBC driver that supports driver-aware connection pooling must implement this function.  
  
 Include sqlspi.h for ODBC driver development.  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md)   
 [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md)
