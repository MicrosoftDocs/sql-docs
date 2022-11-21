---
description: "Driver-Specific Connection Information"
title: "Driver-Specific Connection Information | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLConnect function [ODBC], driver-specific connection information"
  - "connecting to driver [ODBC], SQLConnect"
  - "SQLDriverConnect function [ODBC], driver specific connection information"
  - "connecting to data source [ODBC], SQLDriverConnect"
  - "connecting to driver [ODBC], SQLDriverConnect"
  - "connecting to data source [ODBC], SQLConnect"
  - "connecting to driver [ODBC], driver-specific information"
ms.assetid: 3748758a-f16a-4f3b-9c40-06f2e300704e
author: David-Engel
ms.author: v-davidengel
---
# Driver-Specific Connection Information
**SQLConnect** assumes that a data source name, user ID, and password are sufficient to connect to a data source and that all other connection information can be stored on the system. This is frequently not the case. For example, a driver might need one user ID and password to log on to a server and a different user ID and password to log on to a DBMS. Because **SQLConnect** accepts a single user ID and password, this means that the other user ID and password must be stored with the data source information on the system if **SQLConnect** is to be used. This is a potential breach of security and should be avoided unless the password is encrypted.  
  
 **SQLDriverConnect** allows the driver to define an arbitrary amount of connection information in the keyword-value pairs of the connection string. For example, suppose a driver requires a data source name, a user ID and password for the server, and a user ID and password for the DBMS. A custom program that always uses the XYZ Corp data source might prompt the user for IDs and passwords and build the following set of keyword-value pairs, or *connection string,* to pass to **SQLDriverConnect**:  
  
> [!NOTE]  
>  If you are connecting to a data source provider that supports Windows authentication, you should specify `Trusted_Connection=yes` instead of user ID and password information in the connection string.  
  
```  
DSN={MyDataSourceName};UID={MyUserID};PWD={MyServerPassword};UIDDBMS={MyDBMSUserID};PWDDBMS={MyDBMSUserPassword};  
```  
  
 The **DSN** (Data Source Name) keyword names the data source, the **UID** and **PWD** keywords specify the user ID and password for the server, and the **UIDDBMS** and **PWDDBMS** keywords specify the user ID and password for the DBMS. Notice that the final semicolon is optional. **SQLDriverConnect** parses this string; uses the XYZ Corp data source name to retrieve additional connection information from the system, such as the server address; and logs on to the server and DBMS using the specified user IDs and passwords.  
  
 Keyword-value pairs in **SQLDriverConnect** must follow certain syntax rules. The keywords and their values should not contain the **[]{}(),;?\*=!@** characters. The value of the **DSN** keyword cannot consist only of blanks and should not contain leading blanks. Because of the registry grammar, keywords and data source names cannot contain the backslash (\\) character. Spaces are not allowed around the equal sign in the keyword-value pair.  
  
 The **FILEDSN** keyword can be used in a call to **SQLDriverConnect** to specify the name of a file that contains data source information (see [Connecting Using File Data Sources](../../../odbc/reference/develop-app/connecting-using-file-data-sources.md), later in this section). The **SAVEFILE** keyword can be used to specify the name of a .dsn file in which the keyword-value pairs of a successful connection made by the call to **SQLDriverConnect** will be saved. For more information about file data sources, see the [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md) function description.
