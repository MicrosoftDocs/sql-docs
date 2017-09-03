---
title: "Migrating SQL Server logins (Data Migration Assistant) | Microsoft Docs"
ms.custom: 
ms.date: "08/31/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-dma"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, login migration"
ms.assetid: ""
caps.latest.revision: ""
author: “HJToland3”
ms.author: “jtoland”
manager: "craigg"
---

# Migrating SQL Server logins using Data Migration Assistant

This article provides an overview of migrating SQL Server logins using Data Migration Assistant. 

## Key concepts
The following are the key concepts.

- You can migrate the logins based on a Windows principal (such as a domain user or a Windows domain group). You can also migrate logins created based on SQL authentication, also called SQL Server logins.

- Data Migration Assistant currently doesn’t support the logins associated with a stand-alone security certificate (logins mapped to certificate), a stand-alone asymmetric key (logins mapped to asymmetric key) and logins mapped to credentials.

- Data Migration Assistant doesn’t move the **sa** login and server principles with names enclosed by double hash marks (\#\#), which are for internal use only.

- By default, Data Migration Assistatn selects all the qualified logins to migrate. Optionally, you can select specific logins to migrate. When Data Migration Assistant migrates all qualified logins, the login-user mapping remains intact in the databases that are migrated. 

  If you plan to migrate specific logins, make sure to select the logins that are mapped to one or more users in the databases selected for migration.

- As part of login migration, Data Migration Assistant also moves user-defined server roles and adds server-level permissions to the user-defined server roles. The owner of the role will be set to **sa** principal.

- As part of login migration, Data Migration Assistant assigns the permissions to securables on the target SQL Server as they exist on the source SQL Server. 

  If the login already exists on the target SQL Server, Data Migration Assistant migrates only the permissions assigned to securables and will not re-create the whole login.

- Data Migration Assistant makes the best effort to map the login to database users if the login already exists on the target server.

- It is recommended that you review the migration results to understand the overall status of the login migration and any recommended post-migration actions.

## Resources

[Data Migration Assistant (DMA)](../dma/dma-overview.md)

[Data Migration Assistant: Configuration settings](../dma/dma-configurationsettings.md)
