---
description: "Behavioral Changes"
title: "Behavioral Changes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "backward compatibility [ODBC], behavioral changes"
  - "behavioral changes [ODBC]"
  - "compatibility [ODBC], behavioral changes"
ms.assetid: a17ae701-6ab6-4eaf-9e46-d3b9cd0a3a67
author: David-Engel
ms.author: v-davidengel
---
# Behavioral Changes
Behavioral changes are those changes for which the *syntax* of the interface remains the same, but the *semantics* have changed. For these changes, functionality used in ODBC 2.*x* behaves differently than the same functionality in ODBC 3.*x*.  
  
 Whether an application exhibits ODBC 2.*x* behavior or ODBC 3.*x* behavior is determined by the SQL_ATTR_ODBC_VERSION environment attribute. This 32-bit value is set to SQL_OV_ODBC2 to exhibit ODBC 2.*x* behavior, and SQL_OV_ODBC3 to exhibit ODBC 3.*x* behavior.  
  
 The SQL_ATTR_ODBC_VERSION environment attribute is set by a call to **SQLSetEnvAttr**. After an application calls **SQLAllocHandle** to allocate an environment handle, it must call**SQLSetEnvAttr** immediately to set the behavior it exhibits. (As a result, there is a new environment state to describe the environment handle in an allocated, but versionless, state.) For more information, see [Appendix B: ODBC State Transition Tables](../../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md).  
  
 An application states what behavior it exhibits with the SQL_ATTR_ODBC_VERSION environment attribute, but the attribute has no effect on the application's connection with an ODBC 2.*x* or ODBC 3.*x* driver. An ODBC 3.*x* application can connect to either an ODBC 2.*x* or 3.*x* driver, no matter what the setting of the environment attribute.  
  
 ODBC 3.*x* applications should never call **SQLAllocEnv**. As a result, if the Driver Manager receives a call to **SQLAllocEnv**, it recognizes the application as an ODBC 2.*x* application.  
  
 The SQL_ATTR_ODBC_VERSION attribute affects three different aspects of an ODBC 3.*x* driver's behavior:  
  
-   SQLSTATEs  
  
-   Data types for date, time, and timestamp  
  
-   The *CatalogName* argument in **SQLTables** accepts search patterns in ODBC 3.*x*, but not in ODBC 2.*x*  
  
 The setting of the SQL_ATTR_ODBC_VERSION environment attribute does not affect **SQLSetParam** or **SQLBindParam**. **SQLColAttribute** is also not affected by this bit. Although **SQLColAttribute** returns attributes that are affected by the version of ODBC (date type, precision, scale and length), the intended behavior is determined by the value of the *FieldIdentifier* argument. When *FieldIdentifier* is equal to SQL_DESC_TYPE, **SQLColAttribute** returns the ODBC 3.*x* codes for date, time, and timestamp; when *FieldIdentifier* is equal to SQL_COLUMN_TYPE, **SQLColAttribute** returns the ODBC 2.*x* codes for date, time, and timestamp.  
  
 This section contains the following topics.  
  
-   [SQLSTATE Mappings](../../../odbc/reference/develop-app/sqlstate-mappings.md)  
  
-   [Datetime Data Type Changes](../../../odbc/reference/develop-app/datetime-data-type-changes.md)
