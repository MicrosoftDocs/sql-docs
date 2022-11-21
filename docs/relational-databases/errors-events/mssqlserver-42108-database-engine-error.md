---
description: "MSSQLSERVER_42108"
title: "MSSQLSERVER_42108 | Microsoft Docs"
ms.custom: ""
ms.date: "08/05/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "42108 (Database Engine error)"
ms.assetid: 5e5acb07-16ca-4329-8210-cd2bab0c904f
author: azaricstefan
ms.author: stefanazaric
---
# MSSQLSERVER_42108

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details
  
| Attribute | Value |
| :-------- | :---- |
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|
|Event ID|42108|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|GATEWAY_SQL_POOL_CONNECT_ACTIVATION_BLOCKED|
|Message Text|Can not connect to the SQL pool since it is paused. Please resume the SQL pool and try again.|
  
## Explanation  

Synapse SQL pool is in status called Paused and needs to be resumed before retrying connection to the pool. This error requires action from the user.
  
## User Action  

Resume a SQL pool and retry connecting to the Synapse SQL pool.
