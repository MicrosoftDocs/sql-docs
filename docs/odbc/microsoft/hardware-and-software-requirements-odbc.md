---
title: "Hardware and Software Requirements (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "hardware requirements [ODBC], desktop database drivers"
  - "software requirements [ODBC], desktop database drivers"
  - "system requirements [ODBC], desktop database drivers"
  - "requirements [ODBC], desktop database drivers"
ms.assetid: 6df2e9cd-de10-4629-97bd-32f2782616c7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Hardware and Software Requirements (ODBC)
This topic lists requirements for using the ODBC Desktop Database Drivers.  
  
## Hardware Requirements  
 To use the ODBC Desktop Database Drivers, you must have:  
  
-   An IBM-compatible personal computer.  
  
-   A hard disk with 6 MB of free disk space.  
  
-   At least 16 MB of random-access memory (RAM).  
  
## Software Requirements  
 To access data with an ODBC driver, you must have:  
  
-   The ODBC driver.  
  
-   The 32-bit ODBC Driver Manager, version 3.51 or later (Odbc32.dll).  
  
-   Microsoft Windows 95 or later, or Windows NT 4.0 or Windows 2000.  
  
-   A stack size of at least 20 KB for an application using a Microsoft ODBC driver.  
  
 When using Microsoft Windows NT 4.0 or Windows 2000, the 32-bit driver is thread-safe, but only through the use of a global semaphore that controls access to the driver. Concurrent use of the driver is very limited under Windows NT. All access to the Jet ISAM layer will be single-threaded for all applications using the Microsoft Jet engine.  
  
 When running multiple 16-bit applications on Windows on Windows (WOW) on Microsoft Windows NT 4.0, the applications must be run in separate memory spaces. (The same memory space cannot be used because ODBC does not support multiple environments in the same process.) To run an application in a separate memory space, select the application's icon in the Program Manager, open the **File** menu and click **Properties**, and then click **Run In Separate Memory Space**.  
  
 The use of these drivers by 16-bit applications on Windows 95 is not supported.  
  
## Driver-Specific Hardware and Software Requirements  
  
-   The MicrosoftAccess and dBASEdrivers may require changes in the Autoexec.bat or Config.sys files.
