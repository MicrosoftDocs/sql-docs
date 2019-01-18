---
title: "Machine Data Sources | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "machine data sources [ODBC]"
  - "data sources [ODBC], machine"
ms.assetid: 371bb5b5-1258-4657-acb5-d2b688b2ab4c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Machine Data Sources
*Machine data sources* are stored on the system with a user-defined name. Associated with the data source name is all of the information the Driver Manager and driver need to connect to the data source. For an Xbase data source, this might be the name of the Xbase driver, the full path of the directory containing the Xbase files, and some options that tell the driver how to use those files, such as single-user mode or read-only. For an Oracle data source, this might be the name of the Oracle driver, the server where the Oracle DBMS resides, the SQL*Net connection string that identifies the SQL\*Net driver to use, and the system ID of the database on the server.
