---
description: "SQLTransact (Access Driver)"
title: "SQLTransact (Access Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Access driver [ODBC], SQLTransact"
  - "SQLTransact function [ODBC], Access Driver"
ms.assetid: 892b79c7-9e20-4d1f-bc60-d4b25694ca25
author: David-Engel
ms.author: v-davidengel
---
# SQLTransact (Access Driver)
> [!NOTE]  
>  This topic provides Access Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 When the Microsoft Access driver is used, SQL_COMMIT and SQL_ROLLBACK are supported for the *fType* argument in a call to **SQLTransact**.  
  
 If a failure occurs during the commit process, the affected database can be repaired using the Repair Database option in the Microsoft Access driver setup, or through the use of the REPAIR_DB keyword in the **SQLConfigDataSource** function.
