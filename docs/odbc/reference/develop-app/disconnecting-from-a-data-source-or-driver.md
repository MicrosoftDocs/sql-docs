---
description: "Disconnecting from a Data Source or Driver"
title: "Disconnecting from a Data Source or Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "disconnecting from driver [ODBC]"
  - "data sources [ODBC], disconnecting"
  - "disconnecting from data source [ODBC]"
  - "connecting to data source [ODBC], disconnecting"
  - "connecting to driver [ODBC], disconnecting"
  - "ODBC drivers [ODBC], disconnecting"
ms.assetid: 83dbf0bf-b400-41fb-8537-9b016050dc3c
author: David-Engel
ms.author: v-davidengel
---
# Disconnecting from a Data Source or Driver
When an application has finished using a data source, it calls **SQLDisconnect**. **SQLDisconnect** frees any statements that are allocated on the connection and disconnects the driver from the data source. It returns an error if a transaction is in process.  
  
 After disconnecting, the application can call **SQLFreeHandle** to free the connection. After freeing the connection, it is an application programming error to use the connection's handle in a call to an ODBC function; doing so has undefined but probably fatal consequences. When **SQLFreeHandle** is called, the driver releases the structure used to store information about the connection.  
  
 The application also can reuse the connection, either to connect to a different data source or reconnect to the same data source. The decision to remain connected, as opposed to disconnecting and reconnecting later, requires that the application writer consider the relative costs of each option; both connecting to a data source and remaining connected can be relatively costly depending on the connection medium. In making a correct tradeoff, the application must also make assumptions about the likelihood and timing of further operations on the same data source.
