---
title: "Driver Architecture | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC architecture [ODBC], drivers"
  - "drivers [ODBC], architecture"
ms.assetid: c5003413-0cc1-4f41-b877-a64e2f5ab118
author: MightyPen
ms.author: genemi
manager: craigg
---
# Driver Architecture
Driver architecture falls into two categories, depending on which software processes SQL statements:  
  
-   **File-Based Drivers** The driver accesses the physical data directly. In this case, the driver acts as both driver and data source; that is, it processes ODBC calls and SQL statements. For example, dBASE drivers are file-based drivers because dBASE does not provide a stand-alone database engine the driver can use. It is important to note that developers of file-based drivers must write their own database engines.  
  
-   **DBMS-Based Drivers** The driver accesses the physical data through a separate database engine. In this case the driver processes only ODBC calls; it passes SQL statements to the database engine for processing. For example, Oracle drivers are DBMS-based drivers because Oracle has a stand-alone database engine the driver uses. Where the database engine resides is immaterial. It can reside on the same machine as the driver or a different machine on the network; it might even be accessed through a gateway.  
  
 Driver architecture is generally interesting only to driver writers; that is, driver architecture generally makes no difference to the application. However, the architecture can affect whether an application can use DBMS-specific SQL. For example, Microsoft Access provides a stand-alone database engine. If a Microsoft Access driver is DBMS-based - it accesses the data through this engine - the application can pass Microsoft Access-SQL statements to the engine for processing.  
  
 However, if the driver is file-based - that is, it contains a proprietary engine that accesses the Microsoft® Access .mdb file directly - any attempts to pass Microsoft Access-specific SQL statements to the engine are likely to result in syntax errors. The reason is that the proprietary engine is likely to implement only ODBC SQL.  
  
 This section contains the following topics.  
  
-   [File-Based Drivers](../../odbc/reference/file-based-drivers.md)  
  
-   [DBMS-Based Drivers](../../odbc/reference/dbms-based-drivers.md)  
  
-   [Network Example](../../odbc/reference/network-example.md)  
  
-   [Other Driver Architectures](../../odbc/reference/other-driver-architectures.md)
