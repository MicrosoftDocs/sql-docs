---
title: "Best practices for Data Migration Assistant"
description: Learn best practices for migrating SQL Server databases with Data Migration Assistant, including information about installation, assessment, and migration.
ms.custom: "seo-lt-2019"
ms.date: "03/12/2019"
ms.prod: sql
ms.prod_service: "dma"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Best Practices"
ms.assetid: ""
author: rajeshsetlem
ms.author: rajpo
---


# Best practices for running Data Migration Assistant
This article provides some best practice information for installation, assessment, and migration.

## Installation
Don't install and run the Data Migration Assistant directly on the SQL Server host machine.

## Assessment
- Run assessments on production databases during non-peak times.
- Perform the **Compatibility issues** and **New feature recommendations** assessments separately to reduce the assessment duration.

## Migration
- Migrate a server during non-peak times.

- When migrating a database, provide a single share location accessible by the source server and the target server, and avoid a copy operation if possible. A copy operation may introduce delay based on the size of the backup file. The copy operation also increases the chances that a migration will fail because of an extra step. When a single location is provided, Data Migration Assistant bypasses the copy operation.
 
    In addition, be sure that to provide the correct permissions to the shared folder to avoid migration failures. The correct permissions are specified in the tool. If a SQL Server instance runs under Network Service credentials, give the correct permissions on the shared folder to the machine account for the SQL Server instance.

- Enable encrypt connection when connecting to the source and target servers. Using TLS encryption increases the security of data transmitted across the networks between Data Migration Assistant and the SQL Server instance, which is beneficial especially when migrating SQL logins. If TLS encryption isn't used and the network is compromised by an attacker, the SQL logins being migrated could get intercepted and/or modified on-the-fly by the attacker.

    However, if all access involves a secure intranet configuration, encryption might not be required. Enabling encryption slows performance because the extra overhead that is required to encrypt and decrypt packets. For more information, please refer to [Encrypting Connections to SQL Server](https://go.microsoft.com/fwlink/?linkid=832513).
    
- Check for untrusted constraints on both the source database and the target database before migrating data. After the migration, analyze the target database again to see if any constraints became untrusted as part of the data movement. Fix untrusted constraints as needed. Leaving the constraints untrusted can result in poor execution plans, and it can affect performance.
