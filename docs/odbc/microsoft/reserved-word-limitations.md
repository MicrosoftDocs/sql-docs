---
title: "Reserved keyword limitations"
description: "Reserved keyword limitations"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC desktop database drivers [ODBC]"
  - "desktop database drivers [ODBC]"
---
# Reserved keyword limitations

Avoid using any ODBC reserved keywords as identifiers in your SQL tables or related objects. If an odd case arises where you must use a reserved keyword as an identifier, you must surround the identifier with a pair of *backticks* (`). Another name for *backtick* is *back quote*.

The reserved keyword limitation also applies to any shorthand form of the reserved keywords.

A list of the ODBC reserved keywords is available at:

- [Reserved Keywords](../reference/appendixes/reserved-keywords.md)
- In the *ODBC Programmer's Reference Guide*, see [Appendix C: SQL Grammar](../reference/appendixes/appendix-c-sql-grammar.md)
