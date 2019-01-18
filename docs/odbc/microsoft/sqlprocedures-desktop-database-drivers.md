---
title: "SQLProcedures (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLProcedures function [ODBC], Desktop Database Drivers"
ms.assetid: c996ad6f-e790-40f4-a962-843422496149
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLProcedures (Desktop Database Drivers)
**SQLProcedures** will only return rows for those procedures that have at least one argument. Procedures that have no arguments are treated as views.  
  
|Column|Comments|  
|------------|--------------|  
|PROCEDURE_QUALIFIER|The path to the database file.|  
|PROCEDURE_OWNER|NULL|  
|PROCEDURE_NAME|Undelimited procedure name|  
|PROCEDURE_TYPE|SQL_PT_PROCEDURE|
