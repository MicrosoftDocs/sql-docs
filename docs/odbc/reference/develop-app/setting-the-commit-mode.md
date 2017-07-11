---
title: "Setting the Commit Mode | Microsoft Docs"
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
  - "transactions [ODBC], commit modes"
  - "committing transactions [ODBC]"
  - "commit modes [ODBC]"
ms.assetid: b60d0d74-0655-4013-8d5a-bc1866eaa166
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Setting the Commit Mode
Applications specify the transaction mode with the SQL_ATTR_AUTOCOMMIT connection attribute. By default, ODBC transactions are in auto-commit mode (unless **SQLSetConnectAttr** and **SQLSetConnectOption** are not supported, which is unlikely). Switching from manual-commit mode to auto-commit mode automatically commits any open transaction on the connection.