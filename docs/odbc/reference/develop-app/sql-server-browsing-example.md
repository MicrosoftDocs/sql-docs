---
title: "SQL Server Browsing Example | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLBrowseConnect function [ODBC], example"
  - "connecting to data source [ODBC], SqlBrowseConnect"
  - "connecting to driver [ODBC], SQLBrowseConnect"
ms.assetid: 6e0d5fd1-ec93-4348-a77a-08f5ba738bc6
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL Server Browsing Example
The following example shows how **SQLBrowseConnect** might be used to browse the connections available with a driver for SQL Server. First, the application requests a connection handle:  
  
```  
SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);  
```  
  
 Next, the application calls **SQLBrowseConnect** and specifies the SQL Server driver, using the driver description returned by **SQLDrivers**:  
  
```  
SQLBrowseConnect(hdbc, "DRIVER={SQL Server};", SQL_NTS, BrowseResult,  
                  sizeof(BrowseResult), &BrowseResultLen);  
```  
  
 Because this is the first call to **SQLBrowseConnect**, the Driver Manager loads the SQL Server driver and calls the driver's **SQLBrowseConnect** function with the same arguments it received from the application.  
  
> [!NOTE]  
>  If you are connecting to a data source provider that supports Windows authentication, you should specify `Trusted_Connection=yes` instead of user ID and password information in the connection string.  
  
 The driver determines that this is the first call to **SQLBrowseConnect** and returns the second level of connection attributes: server, user name, password, application name, and workstation ID. For the server attribute, it returns a list of valid server names. The return code from **SQLBrowseConnect** is SQL_NEED_DATA. Here is the browse result string:  
  
```  
"SERVER:Server={red,blue,green,yellow};UID:Login ID=?;PWD:Password=?;  
   *APP:AppName=?;*WSID:WorkStation ID=?;"  
```  
  
 Each keyword in the browse result string is followed by a colon and one or more words before the equal sign. These words are the user-friendly name that an application can use to build a dialog box. The **APP** and **WSID** keywords are prefixed by an asterisk, which means they are optional. The **SERVER**, **UID**, and **PWD** keywords are not prefixed by an asterisk; values must be supplied for them in the next browse request string. The value for the **SERVER** keyword may be one of the servers returned by **SQLBrowseConnect** or a user-supplied name.  
  
 The application calls **SQLBrowseConnect** again, specifying the green server and omitting the **APP** and **WSID** keywords and the user-friendly names after each keyword:  
  
```  
SQLBrowseConnect(hdbc, "SERVER=green;UID=Smith;PWD=Sesame;", SQL_NTS,  
                  BrowseResult, sizeof(BrowseResult), &BrowseResultLen);  
```  
  
 The driver attempts to connect to the green server. If there are any nonfatal errors, such as a missing keyword-value pair, **SQLBrowseConnect** returns SQL_NEED_DATA and remains in the same state as it was prior to the error. The application can call **SQLGetDiagField** or **SQLGetDiagRec** to determine the error. If the connection is successful, the driver returns SQL_NEED_DATA and returns the browse result string:  
  
```  
"*DATABASE:Database={master,model,pubs,tempdb};  
   *LANGUAGE:Language={us_english,Franais};"  
```  
  
 Because the attributes in this string are optional, the application can omit them. However, the application must call **SQLBrowseConnect** again. If the application chooses to omit the database name and language, it specifies an empty browse request string. In this example, the application chooses the pubs database and calls **SQLBrowseConnect** a final time, omitting the **LANGUAGE** keyword and the asterisk before the **DATABASE** keyword:  
  
```  
SQLBrowseConnect(hdbc, "DATABASE=pubs;", SQL_NTS, BrowseResult,  
                  sizeof(BrowseResult), &BrowseResultLen);  
```  
  
 Because the **DATABASE** attribute is the final connection attribute required by the driver, the browsing process is complete, the application is connected to the data source, and **SQLBrowseConnect** returns SQL_SUCCESS. **SQLBrowseConnect** also returns the complete connection string as the browse result string:  
  
```  
"DSN=MySQLServer;SERVER=green;UID=Smith;PWD=Sesame;DATABASE=pubs;"  
```  
  
 The final connection string returned by the driver does not contain the user-friendly names after each keyword, nor does it contain optional keywords not specified by the application. The application can use this string with **SQLDriverConnect** to reconnect to the data source on the current connection handle (after disconnecting) or to connect to the data source on a different connection handle. For example:  
  
```  
SQLDriverConnect(hdbc, hwnd, BrowseResult, SQL_NTS, ConnStrOut,  
                  sizeof(ConnStrOut), &ConnStrOutLen, SQL_DRIVER_NOPROMPT);  
```
