---
description: "Types of Applications"
title: "Types of Applications | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading applications [ODBC], application types"
  - "backward compatibility [ODBC], application and driver compatibility"
  - "compatibility [ODBC], application and driver compatibility"
  - "application upgrades [ODBC], application types"
  - "application compatibility issues [ODBC]"
ms.assetid: d346a64e-a32c-4153-a40f-5b53c2f57ef2
author: David-Engel
ms.author: v-davidengel
---
# Types of Applications
ODBC applications can be classified as follows:  
  
-   **Pure ODBC 2.**  
     **_x_ Application** A 32-bit application that:  
  
    -   Calls only ODBC 2.*x* functions (including the ODBC 1.0 function **SQLSetParam**). These include ODBC 1.*x* applications that have been ported to 32-bit.  
  
    -   Expects ODBC 2.*x* behavior for features that have had behavioral changes. (See [Behavioral Changes](../../../odbc/reference/develop-app/behavioral-changes.md) for more information.)  
  
    -   Has not been recompiled with ODBC 3.5 headers.  
  
-   **Pure ODBC 2.**  
     **_x_ Recompiled Application** A pure ODBC 2.*x* application that has been recompiled using the ODBC 3.5 header files, by setting ODBCVER=0x0250.  
  
-   **Pure ODBC 2.**  
     **_x_ Unicode Application** A pure ODBC 2.*x* recompiled application that is Unicode-compliant and uses the SQL_WCHAR data type.  
  
-   **Pure Open Group and ISO**-**compliant ODBC Application** A 32-bit application that:  
  
    -   Calls functions defined in the Open Group or ISO CLI standards. (These functions may include deprecated 3.0 functions.)  
  
    -   Does not use the Unicode data types.  
  
    -   Expects ODBC 3.0 behavior for features that have had behavioral changes.  
  
-   **Pure ODBC 3.0 Application** A 32-bit application that:  
  
    -   Is compiled with 3.0 headers.  
  
    -   Calls any ODBC 3.0 function, possibly including those that are deprecated.  
  
    -   Expects ODBC 3.0 behavior for features that have had behavioral changes.  
  
-   **Pure ODBC 3.5 Application** A 32 or 64-bit application that:  
  
    -   May use Unicode data types.  
  
    -   Calls any ODBC 3.5 function, possibly including those that are deprecated.  
  
    -   Expects ODBC 3.5 behavior for features that have had behavioral changes.  
  
-   **Pure ODBC 3.8 (or later) Application** A 32-bit or 64-bit application that:  
  
    -   May use Unicode data types.  
  
    -   Calls any ODBC 3.8 function, possibly including those that are deprecated.  
  
    -   Expects ODBC 3.8 behavior for features that have had behavioral changes.  
  
-   **Replaced Application** A 32 or 64-bit application that:  
  
    -   Implements new behavior for duplicated functionality.  
  
    -   Uses any new features in a later version of ODBC only within conditional code.  
  
    -   Has limited conditional code to handle behavioral changes or has registered itself to be an earlier version of ODBC application.
