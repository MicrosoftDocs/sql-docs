---
title: Release notes - Azure Synapse Pathway
description: Azure Synapse Pathway release notes
author: twounder
ms.author: mausher
ms.topic: overview 
ms.date: 05/10/2021
ms.prod: sql
ms.technology: tools-other
monikerRange: "=azure-sqldw-latest"
ms.custom: template-overview 
---
# Azure Synapse Pathway - release notes

[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

This article lists the additions in each release of Azure Synapse Pathway.

## Azure Synapse Pathway (preview) v0.2

The v0.2 release of Azure Synapse Pathway provides support for:
- ALTER/CREATE/DROP View objects for SQL Server

In addition, this release of Synapse Pathway includes:
- Support for parsing of additional object types in IBM Netezza enabling tool execution without errors.
- Improved support for parsing ALTER TABLE statements in Microsoft SQL Server.
- Improved error/warning handling framework to reduce non-supported statements halting execution.


## Azure Synapse Pathway (preview) v0.1

The v0.1 release of Azure Synapse Pathway provides support for:
- ALTER/CREATE/DROP Database, Schema and Table objects for IBM Netezza
- ALTER/CREATE/DROP Database, Schema and Table objects for Microsoft SQL Server
- ALTER/CREATE/DROP Database, Schema and Table objects for Snowflake
- Local logging and output of compliant T-SQL

The initial release of Azure Synapse Pathway includes a GUI and console application to transpile SQL code to T-SQL compliant with Azure Synapse SQL.

> [!NOTE] 
> Like any other Azure Service, all preview services are eligible for support, just without SLA's in place.

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)