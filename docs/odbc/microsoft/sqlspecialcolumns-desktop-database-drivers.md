---
title: "SQLSpecialColumns (Desktop Database Drivers)"
description: "SQLSpecialColumns (Desktop Database Drivers)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLSpecialColumns function [ODBC], Desktop Database Drivers"
---
# SQLSpecialColumns (Desktop Database Drivers)
A unique index will be returned (if one exists) for the SQL_BEST_ROWID flag in *fColType*. No result set will be returned for the SQL_ROWVER flag.  
  
 All row IDs have a scope of SQL_SCOPE_CURROW.  
  
 Pattern matching is not supported for either the *szTableQualifier* or *szTableName* argument.
