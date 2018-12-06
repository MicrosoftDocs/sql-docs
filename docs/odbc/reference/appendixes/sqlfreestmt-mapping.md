---
title: "SQLFreeStmt Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLFreeStmt function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLFreeStmt"
ms.assetid: 267d95f2-4f0c-47ab-9411-5afe105215a2
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLFreeStmt Mapping
When an application calls **SQLFreeStmt** with an *Option* argument of SQL_DROP through an ODBC 3*.x* driver, the call to  
  
```  
SQLFreeStmt(hstmt, SQL_DROP)   
```  
  
 is mapped to  
  
```  
SQLFreeHandle(SQL_HANDLE_STMT,Handle)  
```  
  
 with the *Handle* argument set to the value in *hstmt*.
