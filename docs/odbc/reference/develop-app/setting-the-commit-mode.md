---
title: "Setting the Commit Mode | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "transactions [ODBC], commit modes"
  - "committing transactions [ODBC]"
  - "commit modes [ODBC]"
ms.assetid: b60d0d74-0655-4013-8d5a-bc1866eaa166
author: MightyPen
ms.author: genemi
manager: craigg
---
# Setting the Commit Mode
Applications specify the transaction mode with the SQL_ATTR_AUTOCOMMIT connection attribute. By default, ODBC transactions are in auto-commit mode (unless **SQLSetConnectAttr** and **SQLSetConnectOption** are not supported, which is unlikely). Switching from manual-commit mode to auto-commit mode automatically commits any open transaction on the connection.
