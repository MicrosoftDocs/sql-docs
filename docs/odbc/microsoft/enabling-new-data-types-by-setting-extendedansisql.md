---
title: "Enabling New Data Types by Setting ExtendedAnsiSQL"
description: "Enabling New Data Types by Setting ExtendedAnsiSQL"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "extendedANSISQL [ODBC], enabling new data types"
---
# Enabling New Data Types by Setting ExtendedAnsiSQL
Two new data types are available in Jet 4.0 databases when the ExtendedAnsiSQL flag is turned on: SQL_DECIMAL and SQL_NUMERIC. The default precision and scale are 18 and 0, respectively. Data accessed via ODBC that is typed as SQL_DECIMAL or SQL_NUMERIC will be mapped to Microsoft Jet Decimal instead of Currency.  
  
 When the ExtendedAnsiSQL flag is turned off, you cannot create tables with decimal or numeric types, and these types will not appear in SQLGetTypeInfo(). However, if the table contains the new data types, they can be used with the correct data types.
