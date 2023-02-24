---
description: "Creating and Opening Tables (Text File Driver)"
title: "Creating and Opening Tables (Text File Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "text file driver [ODBC], creating and opening tables"
ms.assetid: e6a07dda-a665-4f5b-a8d6-9ff479700513
author: David-Engel
ms.author: v-davidengel
---
# Creating and Opening Tables (Text File Driver)
When the Text driver is used, a new table is created using the format specified in Odbcinst.ini. If not specified, tables are created in CSVDELIMITED format. By default, INTEGER columns default to 11 characters and FLOAT columns default to 22 characters. DATE columns use the YYYY-MM-DD format. CHAR and LONGCHAR columns are the width specified in the CREATE statement.
