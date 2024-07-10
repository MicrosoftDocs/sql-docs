---
title: "Loading by Ordinal"
description: "Loading by Ordinal"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "backward compatibility [ODBC], loading by ordinal"
  - "compatibility [ODBC], loading by ordinal"
  - "loading by ordinal [ODBC]"
---
# Loading by Ordinal
In ODBC *2.x*, loading by ordinal could be performed to improve the performance of the connection process. An ODBC *2.x* driver exports a dummy function with the ordinal 199; when the Driver Manager detects it, it resolves the addresses of the ODBC functions by ordinal, not by name. This functionality is still supported for ODBC *2.x* drivers but is not supported for ODBC *3.x* drivers.
