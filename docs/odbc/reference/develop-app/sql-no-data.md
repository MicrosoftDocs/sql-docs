---
title: "SQL_NO_DATA | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL_NO_DATA [ODBC]"
  - "application upgrades [ODBC], SQL_NO_DATA"
  - "backward compatibility [ODBC], SQL_NO_DATA"
  - "compatibility [ODBC], SQL_NO_DATA"
  - "upgrading applications [ODBC], SQL_NO_DATA"
ms.assetid: 07a4144a-a548-4578-b2be-715c3cf73bf8
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL_NO_DATA
When an ODBC 3.*x* application calls **SQLExecDirect**, **SQLExecute**, or **SQLParamData** in an ODBC 2.*x* driver to execute a searched update or delete statement that does not affect any rows at the data source, the driver should return SQL_SUCCESS, not SQL_NO_DATA. When an ODBC 2.*x* or ODBC 3.*x* application working with an ODBC 3.*x* driver calls **SQLExecDirect**, **SQLExecute**, or **SQLParamData** with the same result, the ODBC 3.*x* driver should return SQL_NO_DATA.
