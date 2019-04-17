---
title: "Block Cursors, Scrollable Cursors, and Backward Compatibility | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "scrollable cursors [ODBC]"
  - "cursors [ODBC], backward compatibility"
  - "compatibility [ODBC], cursors"
  - "backward compatibility [ODBC], cursors"
  - "block cursors [ODBC]"
ms.assetid: d9d271f6-d2d9-49b9-a365-4909ca06caae
author: MightyPen
ms.author: genemi
manager: craigg
---
# Block Cursors, Scrollable Cursors, and Backward Compatibility
The existence of both **SQLFetchScroll** and **SQLExtendedFetch** represents the first clear split in ODBC between the Application Programming Interface (API), which is the set of functions the application calls, and the Service Provider Interface (SPI), which is the set of functions the driver implements. This split is necessary so that ODBC 3.*x*, which uses **SQLFetchScroll**,bealigned with the standards and also be compatible with ODBC 2.*x*, which uses **SQLExtendedFetch**.  
  
 The ODBC 3*.x* API, which is the set of functions the application calls, includes **SQLFetchScroll** and related statement attributes. The ODBC 3*.x* SPI, which is the set of functions the driver implements, includes **SQLFetchScroll**, **SQLExtendedFetch**, and related statement attributes. Because ODBC does not formally enforce this split between the API and the SPI, it is possible for ODBC 3*.x* applications to call **SQLExtendedFetch** and related statement attributes. However, there is no reason for ODBC 3*.x* application to do this. For more information about APIs and SPIs, see the introduction to [ODBC Architecture](../../../odbc/reference/odbc-architecture.md).  
  
 For information about what functions and statement attributes an ODBC 3.*x* application should use with block and scrollable cursors, see [Block Cursors, Scrollable Cursors, and Backward Compatibility for ODBC 3.x Applications](../../../odbc/reference/develop-app/block-cursors-scrollable-backward-compatibility-odbc-3-x-applications.md).  
  
 This section contains the following topics.  
  
-   [What the Driver Manager Does](../../../odbc/reference/appendixes/what-the-driver-manager-does.md)  
  
-   [What the Driver Does](../../../odbc/reference/appendixes/what-the-driver-does.md)
