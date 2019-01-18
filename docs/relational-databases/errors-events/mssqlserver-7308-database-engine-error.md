---
title: "MSSQLSERVER_7308 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "7308 (Database Engine error)"
  - "single-threaded apartment mode"
ms.assetid: cca9eab9-afb9-463d-abfd-0802257e2c99
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_7308
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|7308|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|RMT_STA_DISABLED|  
|Message Text|OLE DB provider '%ls' cannot be used for distributed queries because the provider is configured to run in single-threaded apartment mode.|  
  
## Explanation  
You have used an OLE DB provider that is configured to run in single-threaded apartment (STA) mode. OLE DB providers that run in single-threaded apartment (STA) mode cannot be used for distributed queries.  
  
## User Action  
To resolve this error, configure the provider to run in multithreaded apartment (MTA) mode. If the provider does not support MTA, and the provider cannot be upgraded to a version that supports MTA, consider configuring the provider to run out-of-process. The provider vendor should be able to tell you if the provider supports MTA or running out-of-process.  
  
