---
description: "MSSQLSERVER_4064"
title: "MSSQLSERVER_4064 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "4064 (Database Engine error)"
ms.assetid: 32112b90-0a2f-4834-a027-756811732be7
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_4064
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|4064|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DB_UFAIL_FATAL|  
|Message Text|Cannot open user default database. Login failed.|  
  
## Explanation  
The SQL Server login was unable to connect because of a problem with its default database. Either the database itself is invalid or the login lacks CONNECT permission on the database.  
  
## User Action  
Use ALTER LOGIN to change the login's default database. Grant CONNECT permission to the login.  
  
