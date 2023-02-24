---
description: "MSSQLSERVER_3937"
title: "MSSQLSERVER_3937 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3937 (Database Engine error)"
ms.assetid: 312d5bbe-c8de-42db-af4b-4ccb448ce6ef
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_3937
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|MSSQLSERVER|  
|Event ID|3937|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|XACT_FILESTREAM_ROLLBACK_ERROR|  
|Message Text|An error occurred while trying to notify the FILESTREAM filter driver that a transaction was rolled back. Error code: 0x%0x.|  
  
## Explanation  
An error was returned by the RsFx driver when issuing a rollback notification for a transaction. This is probably caused by a resource shortage. This can cause a small memory leak in the RsFx filter driver, but this will be freed when the sqlservr.exe process that created the transaction exits.  
  
## User Action  
