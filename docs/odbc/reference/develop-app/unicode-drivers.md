---
title: "Unicode Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Unicode [ODBC], drivers"
  - "Unicode [ODBC], functions"
  - "functions [ODBC], Unicode functions"
ms.assetid: 3b4742d5-74fb-4aff-aa21-d83a0064d73d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Unicode Drivers
Whether a driver should be a Unicode driver or an ANSI driver depends entirely on the nature of the data source. If the data source supports Unicode data, the driver should be a Unicode driver. If the data source only supports ANSI data, the driver should remain an ANSI driver.  
  
 A Unicode driver must export **SQLConnectW** to be recognized as a Unicode driver by the Driver Manager.  
  
 A Unicode driver must accept Unicode functions (with a suffix of *W*) and store Unicode data. It can also accept ANSI functions, but is not required to. (The Driver Manager does not pass an ANSI function call with the *A* suffix to the driver, but converts it to an ANSI function call without the suffix and then passes it to the driver.)  
  
 A Unicode driver must be able to return result sets in either Unicode or ANSI, depending on the application's binding. If an application binds to SQL_C_CHAR, the Unicode driver must convert SQL_WCHAR data to SQL_CHAR. The driver manager will map SQL_C_WCHAR to SQL_C_CHAR for ANSI drivers but does no mapping for Unicode drivers.  
  
> [!NOTE]  
>  When determining the driver type, the Driver Manager will call **SQLSetConnectAttr** and set the SQL_ATTR_ANSI_APP attribute at connection time. If the application is using ANSI APIs, SQL_ATTR_ANSI_APP will be set to SQL_AA_TRUE, and if it is using Unicode, it will be set to a value of SQL_AA_FALSE. This attribute is used so that the driver can exhibit different behavior based on the application type. The attribute cannot be set by the application directly, and it is not supported by **SQLGetConnectAttr**. If a driver exhibits the same behavior for both ANSI and Unicode applications, it should return SQL_ERROR for this attribute. If the driver returns SQL_SUCCESS, the Driver Manager will separate ANSI and Unicode connections when Connection Pooling is used.
