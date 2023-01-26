---
title: Azure Arc-enabled SQL Server network requirements
description: Describes network requirements for of Azure Arc-enabled SQL Server.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 01/25/2023
ms.service: sql
ms.topic: conceptual
ms.custom: references_regions
---

# Networking configuration and resource providers

Review [networking configuration, transport layer security, and resource providers](/azure/azure-arc/servers/agent-overview#prerequisites) required for Connected Machine agent.

The resource provider `Microsoft.AzureArcData` is required to connect the SQL Server instances to Azure Arc. To register the resource provider, follow the instructions in the [Prerequisites](connect.md#prerequisites) section.

If you connected an instance of SQL Server to Azure Arc prior to December 2020, you need to follow the [prerequisite steps](connect.md#prerequisites) to migrate the existing Arc-enabled SQL Server resources to the new namespace.

## Next steps

- [Azure Arc-enabled SQL Server](overview.md)
- [Connect your SQL Server to Azure Arc](connect.md)
- [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)
- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
