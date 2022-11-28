---
title: "MSSQLSERVER_17132 | Microsoft Docs"
description: The SQL Server computer was unable to process the client login packet. See an explanation of the error and possible resolutions.
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17132 (Database Engine error)"
ms.assetid: d1d198bd-6730-4394-bd5f-28f320c01a38
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_17132
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|17132|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|INIT_NODESSPACE|  
|Message Text|Server startup failed due to insufficient memory for descriptor. Reduce non-essential memory load or increase system memory.|  
  
## Explanation  
Failed to allocate enough memory to store server internal descriptor.  
  
## User Action  
Add more memory to the machine. Generic memory troubleshooting steps may be useful.  
  
