---
title: "SQL Minimum Grammar | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "minimum SQL syntax supported [ODBC]"
  - "ODBC drivers [ODBC], minimum SQL syntax supported"
ms.assetid: 4f36d785-104f-4fec-93be-f201203bc7c7
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL Minimum Grammar
This section describes the minimum SQL syntax that an ODBC driver must support. The syntax described in this section is a subset of the Entry level syntax of SQL-92.  
  
 An application can use any of the syntax in this section and be assured that any ODBC-compliant driver will support that syntax. To determine whether additional features of SQL-92 not in this section are supported, the application should call **SQLGetInfo** with the SQL_SQL_CONFORMANCE information type. Even if the driver does not conform to any SQL-92 conformance level, an application can still use the syntax described in this section. If a driver conforms to an SQL-92 level, on the other hand, it supports all syntax included in that level. This includes the syntax in this section because the minimum grammar described here is a pure subset of the lowest SQL-92 conformance level. Once the application knows the SQL-92 level supported, it can determine whether a higher-level feature is supported (if any) by calling **SQLGetInfo** with the individual information type corresponding to that feature.  
  
 Drivers that work only with read-only data sources might not support those parts of the grammar included in this section that deal with changing data. An application can determine if a data source is read-only by calling **SQLGetInfo** with the SQL_DATA_SOURCE_READ_ONLY information type.  
  
## Statement  
 *create-table-statement* ::=  
  
 CREATE TABLE *base-table-name*  
  
 (*column-identifier data-type* [*,column-identifier data-type*]...)  
  
> [!IMPORTANT]  
>  As a *data-type* in a *create-table-statement*, applications must use a data type from the TYPE_NAME column of the result set returned by **SQLGetTypeInfo**.  
  
 *delete-statement-searched* ::=  
  
 DELETE FROM *table-name* [WHERE *search-condition*]  
  
 *drop-table-statement* ::=  
  
 DROP TABLE *base-table-name*  
  
 *insert-statement* ::=  
  
 INSERT INTO *table-name* [( *column-identifier* [, *column-identifier*]...)]      VALUES (*insert-value*[, *insert-value*]... )  
  
 *select-statement* ::=  
  
 SELECT [ALL &#124; DISTINCT] *select-list*  
  
 FROM *table-reference-list*  
  
 [WHERE *search-condition*]  
  
 [*order-by-clause*]  
  
 *statement* ::= *create-table-statement*  
  
 &#124; *delete-statement-searched*  
  
 &#124; *drop-table-statement*  
  
 &#124; *insert-statement*  
  
 &#124; *select-statement*  
  
 &#124; *update-statement-searched*  
  
 *update-statement-searched*  
  
 UPDATE *table-name*  
  
 SET *column-identifier* = {*expression* &#124; NULL }  
  
 [, *column-identifier* = {*expression* &#124; NULL}]...  
  
 [WHERE *search-condition*]  
  
 This section contains the following topics.  
  
-   [Elements Used in SQL Statements](../../../odbc/reference/appendixes/elements-used-in-sql-statements.md)  
  
-   [Data Type Support](../../../odbc/reference/appendixes/data-type-support.md)  
  
-   [Parameter Data Types](../../../odbc/reference/appendixes/parameter-data-types.md)  
  
-   [Parameter Markers](../../../odbc/reference/appendixes/parameter-markers.md)
