---
title: "ODBC and the Standard CLI | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC [ODBC], CLI"
  - "CLI [ODBC]"
  - "CLI [ODBC], about CLI"
  - "call-level interface [ODBC]"
  - "call-level interface [ODBC], about call-level interface"
ms.assetid: 79b9c268-16ac-4b80-b451-f9dcd8c02ca4
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC and the Standard CLI
ODBC aligns with the following specifications and standards that deal with the Call-Level Interface (CLI). (The ODBC features are a superset of each of these standards.)  
  
-   The Open Group CAE Specification "Data Management: SQL Call-Level Interface (CLI)"  
  
-   ISO/IEC 9075-3:1995 (E) Call-Level Interface (SQL/CLI)  
  
 As a result of this alignment, the following are true:  
  
-   An application written to the Open Group and ISO CLI specifications will work with an ODBC 3.*x* driver or a standards-compliant driver when it is compiled with the ODBC 3.*x* header files and linked with ODBC 3.*x* libraries, and when it gains access to the driver through the ODBC 3.*x* Driver Manager.  
  
-   A driver written to the Open Group and ISO CLI specifications will work with an ODBC 3*.x* application or a standards-compliant application when it is compiled with the ODBC 3*.x* header files and linked with ODBC 3*.x* libraries, and when the application gains access to the driver through the ODBC 3*.x* Driver Manager. (For more information, see [Standards-Compliant Applications and Drivers](../../odbc/reference/develop-app/standards-compliant-applications-and-drivers.md).  
  
 The Core interface conformance level encompasses all the features in the ISO CLI and all the nonoptional features in the Open Group CLI. Optional features of the Open Group CLI appear in higher interface conformance levels. Because all ODBC 3.*x* drivers are required to support the features in the Core interface conformance level, the following are true:  
  
-   An ODBC 3.*x* driver will support all the features used by a standards-compliant application.  
  
-   An ODBC 3.*x* application using only the features in ISO CLI and the nonoptional features of the Open Group CLI will work with any standards-compliant driver.  
  
 In addition to the call-level interface specifications contained in the ISO/IEC and Open Group CLI standards, ODBC implements the following features. (Some of these features existed in versions of ODBC prior to ODBC 3.*x*.)  
  
-   Multirow fetches by a single function call  
  
-   Binding to an array of parameters  
  
-   Bookmark support including fetching by bookmark, variable-length bookmarks, and bulk update and delete by bookmark operations on discontiguous rows  
  
-   Row-wise binding  
  
-   Binding offsets  
  
-   Support for batches of SQL statements, either in a stored procedure or as a sequence of SQL statements executed through **SQLExecute** or **SQLExecDirect**  
  
-   Exact or approximate cursor row counts  
  
-   Positioned update and delete operations and batched updates and deletes by function call (**SQLSetPos**)  
  
-   Catalog functions that extract information from the information schema without the need for supporting information schema views  
  
-   Escape sequences for outer joins, scalar functions, datetime literals, interval literals, and stored procedures  
  
-   Code-page translation libraries  
  
-   Reporting of a driver's ANSI-conformance level and SQL support  
  
-   On-demand automatic population of implementation parameter descriptor  
  
-   Enhanced diagnostics and row and parameter status arrays  
  
-   Datetime, interval, numeric/decimal, and 64-bit integer application buffer types  
  
-   Asynchronous execution  
  
-   Stored procedure support, including escape sequences, output parameter binding mechanisms, and catalog functions  
  
-   Connection enhancements including support for connection attributes and attribute browsing
