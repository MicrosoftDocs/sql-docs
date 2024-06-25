---
title: "Implicitly Allocated Descriptors"
description: "Implicitly Allocated Descriptors"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "descriptors [ODBC], allocating and freeing"
  - "implicitly allocated descriptors [ODBC]"
  - "allocating and freeing descriptors [ODBC]"
---
# Implicitly Allocated Descriptors
When a statement handle is allocated, the application implicitly allocates one set of four descriptors. The application can obtain the handles of these implicitly allocated descriptors as attributes of the statement handle. When the application frees the statement handle, the driver frees all implicitly allocated descriptors on that handle.
