---
title: "SQLSpecialColumns (Desktop Database Drivers) | Microsoft Docs"
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
  - "SQLSpecialColumns function [ODBC], Desktop Database Drivers"
ms.assetid: 3de66fdf-053b-4354-979d-e76a5a5e975f
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLSpecialColumns (Desktop Database Drivers)
A unique index will be returned (if one exists) for the SQL_BEST_ROWID flag in *fColType*. No result set will be returned for the SQL_ROWVER flag.  
  
 All row IDs have a scope of SQL_SCOPE_CURROW.  
  
 Pattern matching is not supported for either the *szTableQualifier* or *szTableName* argument.