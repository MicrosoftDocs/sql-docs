---
description: "SQLFreeEnv Mapping"
title: "SQLFreeEnv Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLFreeEnv function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLFreeEnv"
ms.assetid: c0f76455-d072-4bae-bee7-452277dfa479
author: David-Engel
ms.author: v-davidengel
---
# SQLFreeEnv Mapping
When an application calls **SQLFreeEnv** through an ODBC *3.x* driver, the call to  
  
```  
SQLFreeEnv(henv)   
```  
  
 is mapped to  
  
```  
SQLFreeHandle(SQL_HANDLE_ENV,Handle)  
```  
  
 with the *Handle* argument set to the value in *henv*.
