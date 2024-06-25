---
title: "Obtaining Descriptor Handles"
description: "Obtaining Descriptor Handles"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "descriptors [ODBC], retrieving or setting field values"
---
# Obtaining Descriptor Handles
An application obtains the handle of any explicitly allocated descriptor as an output argument of the call to **SQLAllocHandle**. The handle of an implicitly allocated descriptor is obtained by calling **SQLGetStmtAttr**.
