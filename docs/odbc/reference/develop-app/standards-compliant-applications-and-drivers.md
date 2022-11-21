---
description: "Standards-Compliant Applications and Drivers"
title: "Standards-Compliant Applications and Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "standards-compliant applications and drivers [ODBC]"
  - "ODBC drivers [ODBC], standards-compliant"
  - "application features are standards-compliant [ODBC]"
ms.assetid: a1145c4c-3094-4f3f-8cc2-e6bb1a930ab1
author: David-Engel
ms.author: v-davidengel
---
# Standards-Compliant Applications and Drivers
A standards-compliant application or driver is one that conforms to the Open Group CAE Specification "Data Management: SQL Call-Level Interface (CLI)," and the ISO/IEC 9075-3:1995 (E) Call-Level Interface (SQL/CLI).  
  
 ODBC *3.x* guarantees the following features:  
  
-   An application written to the Open Group and ISO CLI specifications will work with an ODBC *3.x* driver or a standards-compliant driver when it is compiled with the ODBC *3.x* header files and linked with ODBC *3.x* libraries, and when it gains access to the driver through the ODBC *3.x* Driver Manager.  
  
-   A driver written to the Open Group and ISO CLI specifications will work with an ODBC *3.x* application or a standards-compliant application when it is compiled with the ODBC *3.x* header files and linked with ODBC *3.x* libraries, and when the application gains access to the driver through the ODBC *3.x* Driver Manager.  
  
 Standards-compliant applications and drivers are compiled with the ODBC_STD compile flag.  
  
 Standards-compliant applications exhibit the following behavior:  
  
-   If a standards-compliant application calls **SQLAllocEnv** (which can occur because **SQLAllocEnv** is a valid function in the Open Group and ISO CLI), the call is mapped to **SQLAllocHandleStd** at compile time. As a result, at run time, the application calls **SQLAllocHandleStd**. During the course of processing this call, the Driver Manager sets the SQL_ATTR_ODBC_VERSION environment attribute to SQL_OV_ODBC3. A call to **SQLAllocHandleStd** is equivalent to a call to **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV and a call to **SQLSetEnvAttr** to set SQL_ATTR_ODBC_VERSION to SQL_OV_ODBC3.  
  
-   If a standards-compliant application calls **SQLBindParam** (which can occur because **SQLBindParam** is a valid function in the Open Group and ISO CLI), the ODBC *3.x* Driver Manager maps the call to the equivalent call in **SQLBindParameter**. (See [SQLBindParam Mapping](../../../odbc/reference/appendixes/sqlbindparam-mapping.md) in Appendix G: Driver Guidelines for Backward Compatibility.)  
  
-   To align with the ISO CLI, the ODBC *3.x* header files contain aliases for information types used in calls to **SQLGetInfo**. A standards-compliant application can use these aliases instead of the ODBC *3.x* information types. For more information, see the next topic, [Header Files](../../../odbc/reference/develop-app/header-files.md).  
  
-   A standards-compliant application must verify that all features it supports are supported in the driver it will work with. Setting the SQL_ATTR_CURSOR_SCROLLABLE statement attribute to SQL_SCROLLABLE and setting the SQL_ATTR_CURSOR_SENSITIVITY statement attribute to SQL_INSENSITIVE or SQL_SENSITIVE are capabilities that are available as optional features in the standards but are not included in the ODBC *3.x* Core level and therefore might not be supported by all ODBC *3.x* drivers. If a standards-compliant application uses these capabilities, it should verify that the driver that it will work with supports them.
