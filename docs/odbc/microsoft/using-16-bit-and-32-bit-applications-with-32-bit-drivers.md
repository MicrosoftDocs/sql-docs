---
description: "Using 16-Bit and 32-Bit Applications with 32-Bit Drivers"
title: "Using 16-Bit and 32-Bit Applications with 32-Bit Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC drivers [ODBC], 16-bit applications"
  - "ODBC drivers [ODBC], 32-bit applications"
  - "32-bit applications with 32-bit drivers [ODBC]"
  - "16-bit applications with 32-bit drivers [ODBC]"
ms.assetid: fc65c988-b31f-4cc9-851f-30d2119604fd
author: David-Engel
ms.author: v-davidengel
---
# Using 16-Bit and 32-Bit Applications with 32-Bit Drivers
> [!IMPORTANT]  
>  16-bit application support will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Develop 32-bit or 64-bit applications instead.  
  
 With the ODBC data access component, you can use 16-bit and 32-bit applications with 32-bit drivers. The Microsoft® Windows® 95/98 and Microsoft Windows NT®/Windows 2000 operating systems support the following combinations of applications and drivers:  
  
-   16-bit applications with 32-bit drivers  
  
-   32-bit applications with 32-bit drivers  
  
 Using a 32-bit application with a 16-bit driver is not supported.  
  
> [!NOTE]  
>  Beginning with the release of ODBC version 3.0, Windows NT 4.0 has been supported.  
  
 ODBC includes the ODBC components necessary to support the above configurations by "thunking" dynamic-link libraries (DLLs) to convert 16-bit addresses to 32-bit addresses and vice versa. The Setup program determines which operating system you are using and installs ODBC components required by that system. You can also choose to install the ODBC components used by all systems.  
  
 In most cases, porting an application or driver from 16-bit to 32-bit involves five types of changes:  
  
-   Changes to message-handling code  
  
-   Changes because integers and handles are 32 bits  
  
-   Changes in calls to Windows application programming interfaces (APIs)  
  
-   Changes to make the driver thread-safe  
  
-   Changes to ODBC components  
  
 From an application or driver programming standpoint, the major difference between 16-bit and 32-bit ODBC components is that they have different file names. From a system standpoint, the architecture of each application or driver connection is different and the tools used to manage data sources are different.  
  
 This section contains the following topics.  
  
-   [Using 16-Bit Applications with 32-Bit Drivers](../../odbc/microsoft/using-16-bit-applications-with-32-bit-drivers.md)  
  
-   [Using 32-Bit Applications with 32-Bit Drivers](../../odbc/microsoft/using-32-bit-applications-with-32-bit-drivers.md)
