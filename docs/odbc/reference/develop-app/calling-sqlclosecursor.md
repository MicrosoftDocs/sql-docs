---
title: "Calling SQLCloseCursor | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "application upgrades [ODBC], SQLCloseCursor"
  - "backward compatibility [ODBC], SqlCloseCursor"
  - "SQLCloseCursor function [ODBC], calling"
  - "upgrading applications [ODBC], SQLCloseCursor"
  - "compatibility [ODBC], SQLCloseCursor"
ms.assetid: ef448c39-a9ad-4f07-8ef3-65bd4cef672a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Calling SQLCloseCursor
Because **SQLCloseCursor** is almost the same as **SQLFreeStmt** with SQL_CLOSE, the Driver Manager does not map this function. Replacement functions are mapped so that existing ODBC 2*.x* applications can easily move to ODBC 3.*x* by using the new functions. Such a move makes it easier for such applications to begin using new ODBC 3.*x* functionality inside of conditional code in a modular fashion. **SQLCloseCursor** does not represent any new functionality. An application does not gain any advantage by moving to **SQLCloseCursor** from **SQLFreeStmt** with SQL_CLOSE.
