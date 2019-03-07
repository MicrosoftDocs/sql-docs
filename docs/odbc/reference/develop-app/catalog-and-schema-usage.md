---
title: "Catalog and Schema Usage | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], interoperability"
  - "interoperability of SQL statements [ODBC], catalog names"
  - "catalog names [ODBC]"
  - "interoperability of SQL statements [ODBC], schema names"
  - "schema names in SQL statements [ODBC]"
ms.assetid: 84f7ef61-1ef1-46f3-9678-b087aa8e8e34
author: MightyPen
ms.author: genemi
manager: craigg
---
# Catalog and Schema Usage
Data sources do not necessarily support catalog and schema names as object name identifiers in all SQL statements. Data sources might support catalog and schema names in one or more of the following classes of SQL statements: Data Manipulation Language (DML) statements, procedure calls, table definition statements, index definition statements, and privilege definition statements. To determine the classes of SQL statements in which catalog and schema names can be used, an application calls **SQLGetInfo** with the SQL_CATALOG_USAGE and SQL_SCHEMA_USAGE options.
