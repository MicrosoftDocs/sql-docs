---
description: "Jet 4.0 Uses SQL-92 Reserved Words List when ExtendedAnsiSQL_Set"
title: "Jet 4.0 Uses SQL-92 Reserved Words List when ExtendedAnsiSQL_Set | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "extendedANSISQL [ODBC], reserved words"
ms.assetid: 7645187e-7777-4c07-9686-0a80d5c5834d
author: David-Engel
ms.author: v-davidengel
---
# Jet 4.0 Uses SQL-92 Reserved Words List when ExtendedAnsiSQL_Set
When the ExtendedAnsiSQL flag is turned on, Jet 4.0 uses the SQL-92 reserved words list. Trying to use a SQL-92 reserved word as an unquoted object name will result in a syntax error. When the ExtendedAnsiSQL flag is turned off, the new reserved words can be used as object names as before.
