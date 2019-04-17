---
title: "Constructing Interoperable SQL Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], interoperability"
  - "interoperability of SQL statements [ODBC], constructing statements"
ms.assetid: dee6f7e2-bcc4-4c74-8c7c-12aeda8a90eb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Constructing Interoperable SQL Statements
As mentioned in the previous sections, interoperable applications should use the ODBC SQL grammar. Beyond using this grammar, however, a number of additional problems are faced by interoperable applications. For example, what does an application do if it wants to use a feature, such as outer joins, that is not supported by all data sources?  
  
 At this point, the application writer must make some decisions about which language features are required and which are optional. In most cases, if a particular driver does not support a feature required by the application, the application simply refuses to run with that driver. However, if the feature is optional, the application can work around the feature. For example, it might disable those parts of the interface that allow the user to use the feature.  
  
 To determine which features are supported, applications start by calling **SQLGetInfo** with the SQL_SQL_CONFORMANCE option. The SQL conformance level gives the application a broad view of which SQL is supported. To refine this view, the application calls **SQLGetInfo** with any of a number of other options. For a complete list of these options, see the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description. Finally, **SQLGetTypeInfo** returns information about the data types supported by the data source. The following sections list a number of possible factors that applications should watch for when constructing interoperable SQL statements.  
  
 This section contains the following topics.  
  
-   [Catalog and Schema Usage](../../../odbc/reference/develop-app/catalog-and-schema-usage.md)  
  
-   [Catalog Position](../../../odbc/reference/develop-app/catalog-position.md)  
  
-   [Quoted Identifiers](../../../odbc/reference/develop-app/quoted-identifiers.md)  
  
-   [Identifier Case](../../../odbc/reference/develop-app/identifier-case.md)  
  
-   [Escape Sequences](../../../odbc/reference/develop-app/escape-sequences.md)  
  
-   [Literal Prefixes and Suffixes](../../../odbc/reference/develop-app/literal-prefixes-and-suffixes.md)  
  
-   [Parameter Markers in Procedure Calls](../../../odbc/reference/develop-app/parameter-markers-in-procedure-calls.md)  
  
-   [DDL Statements](../../../odbc/reference/develop-app/ddl-statements.md)
