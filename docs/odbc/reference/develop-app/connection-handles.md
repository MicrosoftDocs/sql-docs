---
title: "Connection Handles | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connection handles [ODBC]"
  - "handles [ODBC], connection"
ms.assetid: 12222653-f04d-46d6-bdee-61348f5d550f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connection Handles
A *connection* consists of a driver and a data source. A connection handle identifies each connection. The connection handle defines not only which driver to use but which data source to use with that driver. Within a segment of code that implements ODBC (the Driver Manager or a driver), the connection handle identifies a structure that contains connection information, such as the following:  
  
-   The state of the connection  
  
-   The current connection-level diagnostics  
  
-   The handles of statements and descriptors currently allocated on the connection  
  
-   The current settings of each connection attribute  
  
 ODBC does not prevent multiple simultaneous connections, if the driver supports them. Therefore, in a particular ODBC environment, multiple connection handles might point to a variety of drivers and data sources, to the same driver and a variety of data sources, or even to multiple connections to the same driver and data source. Some drivers limit the number of active connections they support; the SQL_MAX_DRIVER_CONNECTIONS option in **SQLGetInfo** specifies how many active connections a particular driver supports.  
  
 Connection handles are primarily used when connecting to the data source (**SQLConnect**, **SQLDriverConnect**, or **SQLBrowseConnect**), disconnecting from the data source (**SQLDisconnect**), getting information about the driver and data source (**SQLGetInfo**), retrieving diagnostics (**SQLGetDiagField** and **SQLGetDiagRec**), and performing transactions (**SQLEndTran**). They are also used when setting and getting connection attributes (**SQLSetConnectAttr** and **SQLGetConnectAttr**) and when getting the native format of an SQL statement (**SQLNativeSql**).  
  
 Connection handles are allocated with **SQLAllocHandle** and freed with **SQLFreeHandle**.
