---
title: "SQLFreeStmt Mapping | Microsoft Docs"
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
  - "SQLFreeStmt function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLFreeStmt"
ms.assetid: 267d95f2-4f0c-47ab-9411-5afe105215a2
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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