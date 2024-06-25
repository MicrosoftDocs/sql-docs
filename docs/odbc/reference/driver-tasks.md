---
title: "Driver Tasks"
description: "Driver Tasks"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "ODBC architecture [ODBC], drivers"
  - "drivers [ODBC], tasks"
---
# Driver Tasks
Specific tasks performed by drivers include:  
  
-   Connecting to and disconnecting from the data source.  
  
-   Checking for function errors not checked by the Driver Manager.  
  
-   Initiating transactions; this is transparent to the application.  
  
-   Submitting SQL statements to the data source for execution. The driver must modify ODBC SQL to DBMS-specific SQL; this is often limited to replacing escape clauses defined by ODBC with DBMS-specific SQL.  
  
-   Sending data to and retrieving data from the data source, including converting data types as specified by the application.  
  
-   Mapping DBMS-specific errors to ODBC SQLSTATEs.
