---
title: "Data Truncation Detection Enabled Using ExtendedAnsiSQL"
description: "Data Truncation Detection Enabled Using ExtendedAnsiSQL"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "truncating data [ODBC]"
  - "extendedANSISQL [ODBC], data truncation detection"
---
# Data Truncation Detection Enabled Using ExtendedAnsiSQL
When the ExtendedAnsiSQL flag is turned on and the application is inserting data into a char or binary column and data is truncated, the truncation will be detected. When the ExtendedAnsiSQL flag is turned off, the data is truncated without warning, as it was in previous versions of the ODBC Desktop Database Drivers.
