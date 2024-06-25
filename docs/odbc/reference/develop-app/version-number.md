---
title: "Version Number"
description: "Version Number"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "version number supported [ODBC]"
  - "interoperability [ODBC], version number supported"
---
# Version Number
There are several versions of ODBC, each with different features. An application determines which ODBC version the Driver Manager and a particular driver support by calling **SQLGetInfo** with the SQL_ODBC_VER and SQL_DRIVER_ODBC_VER options.
