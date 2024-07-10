---
title: "SQLTransact (Access Driver)"
description: "SQLTransact (Access Driver)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "Access driver [ODBC], SQLTransact"
  - "SQLTransact function [ODBC], Access Driver"
---
# SQLTransact (Access Driver)
> [!NOTE]  
>  This topic provides Access Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 When the Microsoft Access driver is used, SQL_COMMIT and SQL_ROLLBACK are supported for the *fType* argument in a call to **SQLTransact**.  
  
 If a failure occurs during the commit process, the affected database can be repaired using the Repair Database option in the Microsoft Access driver setup, or through the use of the REPAIR_DB keyword in the **SQLConfigDataSource** function.
