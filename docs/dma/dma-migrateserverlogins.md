---
title: "Migrate SQL Server logins with Data Migration Assistant"
description: Migrate SQL Server logins with Data Migration Assistant, including SQL Server upgrades to later versions of the on-premises product or to SQL Server on Azure VMs.
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
helpviewer_keywords:
  - "Data Migration Assistant, login migration"
---

# Migrate SQL Server logins with Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

This article provides an overview of migrating SQL Server logins using Data Migration Assistant.

This article applies to scenarios involving SQL Server upgrades to later versions of the on-premises product or to SQL Server on Azure Virtual Machines.

## Which logins are migrated

- You can migrate the logins based on a Windows principal (such as a domain user or a Windows domain group). You can also migrate logins created based on SQL authentication, also called SQL Server logins.

- Data Migration Assistant currently doesn't support the logins associated with a stand-alone security certificate (logins mapped to certificate), a stand-alone asymmetric key (logins mapped to asymmetric key), and logins mapped to credentials.

- Data Migration Assistant doesn't move the **sa** login and server principles with names enclosed by double hash marks (\#\#), which are for internal use only.

- By default, Data Migration Assistant selects all the qualified logins to migrate. Optionally, you can select specific logins to migrate. When Data Migration Assistant migrates all qualified logins, the login-user mapping remains intact in the databases that are migrated.

  If you plan to migrate specific logins, make sure to select the logins that are mapped to one or more users in the databases selected for migration.

- As part of login migration, Data Migration Assistant also moves user-defined server roles and adds server-level permissions to the user-defined server roles. The owner of the role will be set to **sa** principal.

## During and after migration

- As part of login migration, Data Migration Assistant assigns the permissions to securables on the target SQL Server as they exist on the source SQL Server.

  If the login already exists on the target SQL Server, Data Migration Assistant migrates only the permissions assigned to securables and won't re-create the whole login.

- Data Migration Assistant makes the best effort to map the login to database users if the login already exists on the target server.

- It's recommended that you review the migration results to understand the overall status of the login migration and any recommended post-migration actions.

## Related content

- [Data Migration Assistant (DMA)](dma-overview.md)
- [Data Migration Assistant: Configuration settings](dma-configurationsettings.md)
