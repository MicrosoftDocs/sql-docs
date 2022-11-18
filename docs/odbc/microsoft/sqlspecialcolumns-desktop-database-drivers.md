---
description: "SQLSpecialColumns (Desktop Database Drivers)"
title: "SQLSpecialColumns (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSpecialColumns function [ODBC], Desktop Database Drivers"
ms.assetid: 3de66fdf-053b-4354-979d-e76a5a5e975f
author: David-Engel
ms.author: v-davidengel
---
# SQLSpecialColumns (Desktop Database Drivers)
A unique index will be returned (if one exists) for the SQL_BEST_ROWID flag in *fColType*. No result set will be returned for the SQL_ROWVER flag.  
  
 All row IDs have a scope of SQL_SCOPE_CURROW.  
  
 Pattern matching is not supported for either the *szTableQualifier* or *szTableName* argument.
