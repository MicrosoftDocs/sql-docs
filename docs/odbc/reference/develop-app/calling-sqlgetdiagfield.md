---
title: "Calling SQLGetDiagField | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "application upgrades [ODBC], SQLGetDiagField"
  - "backward compatibility [ODBC], SqlGetDiagField"
  - "upgrading applications [ODBC], SQLGetDiagField"
  - "SQLGetDiagField function [ODBC], calling"
  - "compatibility [ODBC], SQLGetDiagField"
ms.assetid: 3c4fb606-b81c-4f11-9820-f0a54e3bc401
author: MightyPen
ms.author: genemi
manager: craigg
---
# Calling SQLGetDiagField
When an ODBC 3.*x* application calls **SQLGetDiagField** in an ODBC 2*.x* driver, the driver will return SQL_SUCCESS and the appropriate information in *\*DiagInfoPtr* if the *DiagIdentifier* argument is SQL_DIAG_CLASS_ORIGIN, SQL_DIAG_CLASS_SUBCLASS_ORIGIN, SQL_DIAG_CONNECTION_NAME, SQL_DIAG_MESSAGE_TEXT, SQL_DIAG_NATIVE, SQL_DIAG_NUMBER, SQL_DIAG_RETURNCODE, SQL_DIAG_SERVER_NAME, or SQL_DIAG_SQLSTATE. All other diagnostic fields will return SQL_ERROR.
