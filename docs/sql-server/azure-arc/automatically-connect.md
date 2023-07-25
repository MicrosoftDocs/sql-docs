---
title: Automatically connect your SQL Server to Azure Arc
description: Automatically connect an instance of SQL Server to Azure Arc. Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: rajpo
ms.reviewer: mikeray, maghan
ms.date: 07/18/2023
ms.service: sql
ms.topic: conceptual
---

# Automatically connect your SQL Server to Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article explains how to connect your SQL Server instance to Azure Arc. Before you proceed, complete the [Prerequisites](prerequisites.md#prerequisites).

## Onboard the server to Azure Arc

Azure Arc automatically installs the Azure extension for SQL Server when a server connected to Azure Arc has SQL Server installed. All the SQL Server instance resources are automatically created in Azure, providing a centralized management platform for all your SQL Servers.

To learn more, see [Auto deployment of Azure extension for SQL Server](connect-at-scale-autodeploy.md)

Connecting SQL Servers in your hybrid environment directly with Azure can be accomplished using different methods, depending on your requirements and the tools you prefer to use.

To connect your SQL Server to Azure, see [Azure Connected Machine agent deployment options](/azure/azure-arc/servers/deployment-options).

> [!NOTE]  
> If your server is already connected to Azure, but Azure extension for SQL Server is not deployed automatically using above methods, proceed to [Alternate deployment options for Azure Arc-enabled SQL Server](deployment-options.md) to connect SQL Server to Azure.

## Validate your Arc-enabled SQL Server resources

Go to **Azure Arc > SQL Server** and open the newly registered Arc-enabled SQL Server resource to validate.

   :::image type="content" source="media/join/validate-sql-server-azure-arc.png" alt-text="Screenshot of validating a connected SQL Server.":::

> [!NOTE]  
> After connecting the server to Arc, you need to wait 10 minutes for the Azure extension for SQL Server to deploy and the Arc-enabled SQL Server resource to be created.
>
If the Arc-enabled SQL Server resource does not appear in the same resource group as the Arc Server, follow the [Azure extension for SQL Server](troubleshoot-deployment.md) guide to troubleshoot the issue.

## Next steps

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure best practices assessment on an Azure Arc-enabled SQL Server instance](assess.md)
