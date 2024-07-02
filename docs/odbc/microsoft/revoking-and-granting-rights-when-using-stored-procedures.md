---
title: "Revoking and Granting Rights When Using Stored Procedures"
description: "Revoking and Granting Rights When Using Stored Procedures"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "stored procedures [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], stored procedures"
---
# Revoking and Granting Rights When Using Stored Procedures
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 The Microsoft ODBC Driver for Oracle returns the following error message when user rights are granted and then revoked on a table accessed by a stored procedure:  
  
 SQL_ERROR=-1  
  
 szErrorMsg="[Microsoft][ODBC driver for Oracle]Wrong number of parameters"  
  
 szErrorMsg="[Microsoft][ODBC driver for Oracle]Syntax error or access violation"  
  
 The call to the Oracle OCI function Odessp() fails in this scenario but is necessary in order to implement default parameters. After the underlying table permissions are modified, the stored procedure must be recompiled before running it again.
