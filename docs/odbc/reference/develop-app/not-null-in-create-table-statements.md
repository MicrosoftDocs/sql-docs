---
title: "NOT NULL in CREATE TABLE Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "not null [ODBC]"
  - "interoperability [ODBC], not null"
ms.assetid: 3fb69943-f0c9-4ed2-aa42-20440e37e49d
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# NOT NULL in CREATE TABLE Statements
Some databases, and especially desktop databases, do not support the **NOT NULL** column constraint in **CREATE TABLE** statements. For more information, see the SQL_NON_NULLABLE_COLUMNS option in the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description.