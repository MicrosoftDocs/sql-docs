---
title: "Connecting Using File Data Sources | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connecting to driver [ODBC], file data sources"
  - "SQLDriverConnect function [ODBC], connecting using file data sources"
  - "connecting to data source [ODBC], SQLDriverConnect"
  - "connecting to driver [ODBC], SQLDriverConnect"
  - "connecting to data source [ODBC], file data sources"
  - "file data sources [ODBC]"
ms.assetid: 3003f8c2-8be6-41cc-8d9c-612e9bd0f3ae
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connecting Using File Data Sources
The connection information for a file data source is stored in a .dsn file. As a result, the connection string can be used repeatedly by a single user or shared among several users if they have the appropriate driver installed. The file contains a driver name (or another data source name in the case of an unshareable file data source) and optionally, a connection string that can be used by **SQLDriverConnect**. The Driver Manager builds the connection string for the call to **SQLDriverConnect** from the keywords in the .dsn file.  
  
 A file data source allows an application to specify connection options without having to build a connection string for use with **SQLDriverConnect**. The file data source usually is created by specifying the **SAVEFILE** keyword, which causes the Driver Manager to save the output connection string created by a call to **SQLDriverConnect** to the .dsn file. That connection string can be used repeatedly by calling **SQLDriverConnect** with the **FILEDSN** keyword. This streamlines the connection process and provides a persistent source of the connection string.  
  
 File data sources also can be created by calling **SQLCreateDataSource** in the installer DLL. Information can be written into the .dsn file by calling **SQLWriteFileDSN**, and read from the .dsn file by calling **SQLReadFileDSN**; both of these functions are also in the installer DLL. For information about the installer DLL, see [Configuring Data Sources](../../../odbc/reference/install/configuring-data-sources.md).  
  
 The keywords used for connection information are in the [ODBC] section of a .dsn file. The minimum information that a shareable .dsn file would have in the [ODBC] section is the DRIVER keyword:  
  
```  
DRIVER = SQL Server  
```  
  
 The shareable .dsn file usually contains a connection string, as follows:  
  
```  
DRIVER = SQL Server  
UID = Larry  
DATABASE = MyDB  
```  
  
 When the file data source is unshareable, the .dsn file contains only a **DSN** keyword. When the Driver Manager is sent the information in an unshareable file data source, it connects as necessary to the data source indicated by the **DSN** keyword. An unshareable .dsn file would contain the following keyword:  
  
```  
DSN = MyDataSource  
```  
  
 The connection string used for a file data source is the union of the keywords specified in the .dsn file and the keywords specified in the connection string in the call to **SQLDriverConnect**. If any of the keywords in the .dsn file conflict with keywords in the connection string, the Driver Manager decides which keyword value should be used. For more information, see [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md).  
  
## See Also  
 [https://support.microsoft.com/kb/165866](https://support.microsoft.com/kb/165866)
