---
description: "SQLFreeStmt Mapping"
title: "SQLFreeStmt Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLFreeStmt function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLFreeStmt"
ms.assetid: 267d95f2-4f0c-47ab-9411-5afe105215a2
author: David-Engel
ms.author: v-davidengel
---
# SQLFreeStmt Mapping
When an application calls **SQLFreeStmt** with an *Option* argument of SQL_DROP through an ODBC *3.x* driver, the call to  
  
```  
SQLFreeStmt(hstmt, SQL_DROP)   
```  
  
 is mapped to  
  
```  
SQLFreeHandle(SQL_HANDLE_STMT,Handle)  
```  
  
 with the *Handle* argument set to the value in *hstmt*.
