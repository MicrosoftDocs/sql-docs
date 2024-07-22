---
title: "Catalog and Schema Usage"
description: "Catalog and Schema Usage"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "SQL statements [ODBC], interoperability"
  - "interoperability of SQL statements [ODBC], catalog names"
  - "catalog names [ODBC]"
  - "interoperability of SQL statements [ODBC], schema names"
  - "schema names in SQL statements [ODBC]"
---
# Catalog and Schema Usage
Data sources do not necessarily support catalog and schema names as object name identifiers in all SQL statements. Data sources might support catalog and schema names in one or more of the following classes of SQL statements: Data Manipulation Language (DML) statements, procedure calls, table definition statements, index definition statements, and privilege definition statements. To determine the classes of SQL statements in which catalog and schema names can be used, an application calls **SQLGetInfo** with the SQL_CATALOG_USAGE and SQL_SCHEMA_USAGE options.
