---
title: "Best practices for Data Migration Assistant (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "10/04/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "dma"
ms.service: ""
ms.component:
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "sql-dma"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Best Practices"
ms.assetid: ""
caps.latest.revision: ""
author: "HJToland3"
ms.author: "jtoland"
manager: "craigg"
ms.workload: "Inactive"
---


# Best practices for running Data Migration Assistant
This article provides the following best practice information for installation, assessments, and migration.

## Installation

Do not install and run Data Migration Assistant directly on the SQL Server host machine.

## Assessment

- Run assessments on production databases during non-peak times.

- Perform the **Compatibility issues** and **New feature recommendations** assessments separately to reduce the assessment duration.

## Migration

- Migrate a server during non-peak times.

- When migrating a database, provide a single share location accessible by source server and target server, and avoid a copy operation if possible. A copy operation may introduce delay based on the size of the backup file. The copy operation also increases the chances of a migration failing because of an extra step. When a single location is provided, Data Migration Assistant bypasses the copy operation.
 
    Also be sure that to provide the correct permissions to the shared folder to avoid migration failures. The correct permissions are specified in the tool. If a SQL Server instance runs under Network Service credentials, give the correct permissions on the shared folder to the machine account for the SQL Server instance.

- Enable encrypt connection when connecting to the source and target servers. Using SSL encryption increases the security of data transmitted across the networks between Data Migration Assistant and the SQL Server instance. This is beneficial especially when migrating SQL logins. If SSL encryption is not used and the network is compromised by an attacker, the SQL logins being migrated could get intercepted and/or modified on-the-fly by the attacker.

    However, if all access involves a secure intranet configuration, encryption might not be required. Enabling encryption slows performance as a result of the extra overhead required to encrypt and decrypt packets. For more information, please refer to [Encrypting Connections to SQL Server](https://go.microsoft.com/fwlink/?linkid=832513).
