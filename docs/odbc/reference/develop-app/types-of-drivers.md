---
title: "Types of Drivers | Microsoft Docs"
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
ms.assetid: 864c53c1-b68a-48b6-b6bc-5ecb520bb9dc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Types of Drivers
ODBC drivers can be classified as follows:  
  
-   **32-bit ODBC 2.**  
     **_x_ Driver** A 32-bit driver that:  
  
    -   Exports only ODBC 2*.x* functions.  
  
    -   Exhibits ODBC 2.*x* behavior for behavioral changes.  
  
-   **ISO and Open Group-Compliant Driver** A 32-bit driver that:  
  
    -   Exports all functions that are documented in the Open Group or ISO CLI documents. This will include some of functions that are deprecated in ODBC.  
  
    -   Exhibits ODBC 3.0 behavior for behavioral changes.  
  
    -   Does not necessarily go through the ODBC 3.0 Driver Manager.  
  
-   **ODBC 3.0 Driver** A 32-bit driver that:  
  
    -   Exports only functions that are in ODBC 3.0 minus deprecated functions.  
  
    -   Is capable of exhibiting ODBC 2.*x* behavior or ODBC 3.0 behavior with respect to behavioral changes, based on the SQL_ATTR_APP_ODBC_VERSION environment attribute.  
  
-   **ODBC 3.5 (or later) ANSI Driver** A 32-bit driver that:  
  
    -   Exports only functions that are in ODBC 3.5 minus deprecated functions.  
  
    -   Is capable of exhibiting ODBC 2.*x* behavior or ODBC 3.0 behavior, or ODBC 3.5 behavior with respect to behavioral changes, based on the SQL_ATTR_APP_ODBC_VERSION environment attribute.  
  
-   **ODBC 3.5 (or later) Unicode Driver** A 32-bit driver that:  
  
    -   Supports all the features of an ODBC 3.5 ANSI driver.  
  
    -   Exports Unicode versions of all ODBC string APIs.  
  
    -   Can store and process Unicode data on the data source.  
  
> [!NOTE]  
>  16-bit ODBC drivers will not work directly with the ODBC 3.*x* Driver Manager. However, it is possible for 16-bit drivers to work with the 2.0 ODBC Driver Manager, which subsequently thunks up to the 3.*x* Driver Manager.
