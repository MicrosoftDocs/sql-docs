---
description: "Connecting with SQLBrowseConnect"
title: "Connecting with SQLBrowseConnect | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connecting to driver [ODBC], SQLBrowseConnect"
  - "SQLBrowseConnect function [ODBC], connecting"
  - "connecting to data source [ODBC], SQLBrowseConnect"
ms.assetid: 6c2e9f76-b766-48df-b109-246bb05ae45d
author: David-Engel
ms.author: v-davidengel
---
# Connecting with SQLBrowseConnect
**SQLBrowseConnect**, like **SQLDriverConnect**, uses a connection string. However, by using **SQLBrowseConnect**, an application can construct a complete connection string at run time. This allows the application to do two things:  
  
-   Build its own dialog boxes to prompt for this information, thereby retaining control over its "look and feel."  
  
-   Browse the system for data sources that can be used by a particular driver, possibly in several steps. For example, the user might first browse the network for servers and, after choosing a server, browse the server for databases accessible by the driver.  
  
 The application calls **SQLBrowseConnect** and passes a connection string, known as the *browse request connection string,* that specifies a driver or data source. The driver returns a connection string, known as the *browse result connection string,* that contains keywords, possible values (if the keyword accepts a discrete set of values), and user-friendly names. The application builds a dialog box with the user-friendly names and prompts the user for values. It then builds a new browse request connection string from these values and returns this to the driver with another call to **SQLBrowseConnect**.  
  
 Because connection strings are passed back and forth, the driver can provide several levels of browsing by returning a new connection string when the application returns the old one. For example, the first time an application calls **SQLBrowseConnect**, the driver might return keywords to prompt the user for a server name. When the application returns the server name, the driver might return keywords to prompt the user for a database. The browsing process would be complete after the application returned the database name.  
  
 Each time **SQLBrowseConnect** returns a new browse result connection string, it returns SQL_NEED_DATA as its return code. This tells the application that the connection process is not complete. Until **SQLBrowseConnect** returns SQL_SUCCESS, the connection is in a Need Data state and cannot be used for other purposes, such as to set a connection attribute. The application can terminate the connection browsing process by calling **SQLDisconnect**.  
  
 This section contains the following topic.  
  
-   [SQL Server Browsing Example](../../../odbc/reference/develop-app/sql-server-browsing-example.md)
