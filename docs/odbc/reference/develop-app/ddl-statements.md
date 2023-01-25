---
description: "DDL Statements"
title: "DDL Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], interoperability"
  - "interoperability of SQL statements [ODBC], DDL statements"
  - "DDL statements [ODBC]"
ms.assetid: 96ac9859-5976-4b06-ae1f-2fec3231e266
author: David-Engel
ms.author: v-davidengel
---
# DDL Statements
Data Definition Language (DDL) statements vary tremendously among DBMSs. ODBC SQL defines statements for the most common data definition operations: creating and dropping tables, indexes, and views; altering tables; and granting and revoking privileges. All other DDL statements are data source-specific. Therefore, interoperable applications cannot perform some data definition operations. In general, this is not a problem, because such operations tend to be highly DBMS-specific and are best left to the proprietary database administration software shipped with most DBMSs or the setup program shipped with the driver.  
  
 Another problem in data definition is that data type names vary tremendously among DBMSs. Rather than defining standard data type names and forcing drivers to convert them to DBMS-specific names, **SQLGetTypeInfo** provides a way for applications to discover DBMS-specific data type names. Interoperable applications should use these names in SQL statements to create and alter tables; the names listed in [Appendix C: SQL Grammar](../../../odbc/reference/appendixes/appendix-c-sql-grammar.md), and [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md), are examples only.
