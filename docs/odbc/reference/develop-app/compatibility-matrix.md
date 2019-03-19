---
title: "Compatibility Matrix | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver compatibility issues [ODBC]"
  - "ODBC drivers [ODBC], backward compatibility"
  - "backward compatibility [ODBC], application and driver compatibility"
  - "compatibility [ODBC], application and driver compatibility"
  - "application compatibility issues [ODBC]"
  - "application upgrades [ODBC], compatibility matrix"
  - "upgrading applications [ODBC], compatibility matrix"
ms.assetid: 0690b463-15a1-48fa-9d0b-9cc9e5bf7fc6
author: MightyPen
ms.author: genemi
manager: craigg
---
# Compatibility Matrix
The following table describes the compatibility of the types of applications and drivers defined previously in this section.  
  
|Application type<br /><br /> and version|32-bit ODBC<br /><br /> 2.*x* driver|ODBC 3.*x*<br /><br /> driver|ODBC 3.8 driver|ISO and Open Group-compliant driver|  
|--------------------------------------|-----------------------------------|---------------------------|---------------------|-----------------------------------------|  
|16-bit application, any version|Compatible|Compatible|Compatible|Compatible|  
|Pure 2.*x* application|Compatible|Compatible|Compatible|Not compatible[3]|  
|Pure 2.*x* recompiled application|Compatible|Compatible[1]|Compatible[1]|Not compatible[3]|  
|Pure 2.*x* Unicode application|Compatible|Compatible[1]|Compatible[1]|Not Compatible[3]|  
|Pure Open Group and ISO-compliant application|Not compatible|Compatible[2]|Compatible[2]|Compatible[2]|  
|Pure 3.0 application|Not compatible|Compatible|Compatible|Not compatible[4]|  
|Pure 3.5 application|Not compatible|Compatible|Compatible|Not compatible[4]|  
|Pure 3.8 (or higher) application|Not compatible [5]|Not compatible [5]|Compatible|Not compatible [4]|  
|Replaced application|Compatible|Compatible|Compatible|Not compatible[3]|  
  
 [1]   The application must recompile using ODBC 3.5 (or higher) headers with the UNICODE option (if it is a Unicode application) and must set ODBCVER to 0x0250.  
  
 [2]   The application must compile using ODBC 3.5 (or higher)headers and link with the ODBC Driver Manager. It must also set the header flag ODBC_STD.  
  
 [3]   This configuration can potentially fail to work because there are features in ODBC 2.*x* that are not in the standards, such as bookmarks.  
  
 [4]   This configuration can potentially fail to work because there are features in ODBC 3*.x* that are not in the standards, such as bookmarks.  
  
 [5]   This configuration can potentially fail because there are features in ODBC 3.8 that are not in ODBC 2.x or 3.x drivers, such as driver-specific [C Data Types in ODBC](../../../odbc/reference/develop-app/c-data-types-in-odbc.md).  
  
## Driver Manager Compatibility  
 An ODBC 3.0 application that must operate with all Driver Manager versions should do the following on startup:  
  
-   Allocate an environment handle.  
  
-   Set the SQL_ATTR_ODBC_VERSION environment attribute to SQL_OV_ODBC3_80. If the Driver Manager returns SQL_ERROR, the Driver Manager is older than 3.8. Reset SQL_ATTR_ODBC_VERSION to SQL_OV_ODBC3 or SQL_OV_ODBC2, as appropriate, to correspond to the Driver Manager.  
  
-   Allocate a connection handle.  
  
-   Make a connection.  
  
-   Call SQLGetInfo for SQL_DRIVER_ODBC_VER to determine the driver version. If the driver is an ODBC 3.8 driver, you can use driver-specific C types. Otherwise, do not use driver-specific C data types.  
  
 Note that a recompiled ODBC 3.x application can use ODBC 3.8 features other than driver-specific C types without specifying SQL_OV_ODBC3_80 for SQL_ATTR_ODBC_VERSION. This is similar to a recompiled ODBC 2.x application using ODBC 3.x features.  
  
## Using SQLCancelHandle in an Application Compatible with all Driver Managers  
 Because [SQLCancelHandle Function](../../../odbc/reference/syntax/sqlcancelhandle-function.md) is not supported in Driver Managers that were released before Windows 7, an application cannot be loaded in older versions of Windows if it calls **SQLCancelHandle** directly. To work with all versions of Driver Managers and use **SQLCancelHandle** on new versions of Windows, an application should call **SQLCancelHandle** indirectly by using **LoadLibrary** and **GetProcAddress.**  
  
## See Also  
 [What's New in ODBC 3.8](../../../odbc/reference/what-s-new-in-odbc-3-8.md)
