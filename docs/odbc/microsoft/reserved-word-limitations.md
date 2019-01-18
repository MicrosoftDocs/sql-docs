---
title: "Reserved Word Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "05/01/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC desktop database drivers [ODBC]"
  - "desktop database drivers [ODBC]"
ms.assetid: ed42f083-c9e8-4ee4-9d64-d879bf955c78
author: MightyPen
ms.author: genemi
manager: craigg
---
# Reserved Keyword Limitations

Avoid using any ODBC reserved keywords as identifiers in your SQL tables or related objects. If an odd case arises where you must use a reserved keyword as an identifier, you must surround the identifier with a pair of *backticks* (`). Another name for *backtick* is *back quote*.

The reserved keyword limitation also applies to any shorthand form of the reserved keywords.

A list of the ODBC reserved keywords is available at:

- [ODBC Reserved Keywords](https://docs.microsoft.com/sql/odbc/reference/appendixes/reserved-keywords).

- In the *ODBC Programmer's Reference Guide*, see [Appendix C: SQL Grammar](https://docs.microsoft.com/sql/odbc/reference/appendixes/appendix-c-sql-grammar).

