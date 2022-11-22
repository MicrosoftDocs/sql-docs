---
description: "Data Truncation Detection Enabled Using ExtendedAnsiSQL"
title: "Data Truncation Detection Enabled Using ExtendedAnsiSQL | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "truncating data [ODBC]"
  - "extendedANSISQL [ODBC], data truncation detection"
ms.assetid: cec2359b-917d-4e1d-9625-5cd678b62f10
author: David-Engel
ms.author: v-davidengel
---
# Data Truncation Detection Enabled Using ExtendedAnsiSQL
When the ExtendedAnsiSQL flag is turned on and the application is inserting data into a char or binary column and data is truncated, the truncation will be detected. When the ExtendedAnsiSQL flag is turned off, the data is truncated without warning, as it was in previous versions of the ODBC Desktop Database Drivers.
