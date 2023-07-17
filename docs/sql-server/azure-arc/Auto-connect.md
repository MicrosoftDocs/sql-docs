---
title: Connect your SQL Server to Azure Arc
description: Connect an instance of SQL Server to Azure Arc. Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, maghan
ms.date: 07/06/2023
ms.service: sql
ms.topic: conceptual
---
# Connect your SQL Server to Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article explains how to connect your SQL Server instance to Azure Arc. Before you proceed, complete the [Prerequisites](prerequisites.md#prerequisites).

## Onboard the server to Azure Arc

The experience of connecting SQL Servers to Azure is streamlined by automatically installing the Azure extension for SQL Server when the servers are connected to Azure Arc that have SQL Server installed. All the SQL Server instance resources are automatically created in Azure, providing a centralized management platform for all your SQL Servers.

To learn more, see [Auto deploymnet of Azure extension for SQL Server](connect-at-scale-autodeploy.md)

To connect your SQL Server to Azure, see [Connect hybrid machines to Azure using a deployment script](https://learn.microsoft.com/en-us/azure/azure-arc/servers/onboard-portal)

> [!NOTE]  
> If your server is already connected to Azure and to deploy Azure SQL Server extension for SQL Server proceed to [When the machine is already connected to an Arc-enabled Server](connect-already-enabled.md).

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
