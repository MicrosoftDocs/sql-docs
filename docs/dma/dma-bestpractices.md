---
title: "Best Practices for Data Migration Assistant | Microsoft Docs"
ms.custom: 
ms.date: "08/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-dma"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Best Practices"
ms.assetid: ""
caps.latest.revision: ""
author: "sabotta"
ms.author: "carlasab"
manager: "craigg"
---


# Best practices for Running Data Migration Assistant:


**General**

Do not install and run Data Migration Assistance (DMA) directly on the SQL Server host machine.

**Assessment**

- Run assessments on production databases during non-peak times.

- Perform the “Compatibility issues” and “New feature recommendations” assessments separately to reduce the assessment duration.

**Migration**

Migrate a server during non-peak times.

When migrating a database, provide a single share location accessible by source server and target server, and avoid copy operation if possible. Based on the size of the backup file, copy operation will introduce delay. It also increases the chances of failing migration due to an extra step. If a single location is provided, then DMA will bypass copy operation. However, please make sure that the correct permissions are given to the shared folder to avoid migration failures. The correct permissions are specified in the tool. If SQL Server instance runs under Network Service credentials, then give the machine account of the instance the correct permissions on the shared folder.

Enable "encrypt connection" when connecting to Source and Target server. Using SSL encryption increases the security of data transmitted across the networks between DMA and the SQL Server instance. This is beneficial especially when migrating SQL Logins. If SSL encryption is not used and the network is compromised by an attacker, then the SQL Logins being migrated could get intercepted and/or modified, on-the-fly by the attacker. However, if all access involves a secure intranet configuration, encryption might not be required. Enabling encryption does slow performance due to extra overhead to encrypt and decrypt packets. For more information please refer to [Encrypting Connections to SQL Server](https://go.microsoft.com/fwlink/?linkid=832513).
