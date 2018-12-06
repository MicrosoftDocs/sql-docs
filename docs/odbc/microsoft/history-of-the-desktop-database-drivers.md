---
title: "History of the Desktop Database Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Jet-based ODBC drivers [ODBC], history"
  - "ODBC desktop database drivers [ODBC], history"
  - "desktop database drivers [ODBC], history"
ms.assetid: b4a2aff8-bde7-4bd5-8580-bc50f27311c8
author: MightyPen
ms.author: genemi
manager: craigg
---
# History of the Desktop Database Drivers
The following table shows the Desktop Database Drivers version history.  
  
|Version|Release Date|Description|  
|-------------|------------------|-----------------|  
|1.0|August 1993|Used the SIMBA query processor produced by PageAhead Software. SIMBA received ODBC calls and SQL statements, processed them into Microsoft Jet installable ISAM calls, and then called the Microsoft Jet ISAM dispatch layer to load and call the appropriate installable ISAM driver.|  
|2.0|December 1994|Used with ODBC 2.0, which significantly expanded ODBC functionality. The major change in version 2.0 was that the Microsoft Jet database engine replaced the SIMBA query processor. With the Microsoft Jet database engine, the Desktop Database Drivers integrated much more tightly with the Microsoft Jet installable ISAM drivers and Microsoft Access technology. Significant enhancements were:<br /><br /> -   Native support for scrollable cursors.<br />-   Native support for outer joins, updatable and heterogeneous joins, and transactions.<br />-   32-bit versions of the drivers for Microsoft Windows NT.|  
|3.0|October 1995|Provided support for Windows 95 and Windows NT Workstation or NT Server 3.51. Only 32-bit drivers were included in this release; the 16-bit drivers for Windows version 3.1 were removed.|  
|3.5|October 1996|These drivers were double-byte character set (DBCS)-enabled, were better suited for use with Internet applications than previous versions, and accommodated the use of File data source names (DSNs). The Microsoft Access driver was released in an RISC version for use on Alpha platforms for Windows 95/98 and Windows NT 3.51 and later operating systems.|  
|4.0|Late 1998|Provides support for Microsoft Jet Engine Unicode format along with compatibility for ANSI format of earlier versions.|  
  
> [!NOTE]  
>  The version3.5 drivers were designed to work with ODBC2.*x*. Although they also work with ODBC 3.0, they do not support all ODBC 3.0 features. For more information about how these drivers work with ODBC 3.0, see [Backward Compatibility and Standards Compliance](../../odbc/reference/develop-app/backward-compatibility-and-standards-compliance.md).
