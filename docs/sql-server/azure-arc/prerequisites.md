---
title: Azure Arc-enabled SQL Server prerequisites
description: Descibes prerequisites required by of Azure Arc-enabled SQL Server.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 01/25/2023
ms.service: sql
ms.topic: conceptual
ms.custom: references_regions
---

# Prerequisites

## Supported SQL versions and operating systems

Azure Arc-enabled SQL Server supports SQL Server 2012 or higher running on one of the following versions of the Windows or Linux operating system:

- Windows Server 2012 R2 and higher
- Ubuntu 20.04 (x64)
- Red Hat Enterprise Linux (RHEL) 8 (x64) 
- SUSE Linux Enterprise Server (SLES) 15 (x64)

> [!NOTE]
> Azure Arc-enabled SQL Server does not support the following configurations currently:
>
> - SQL Server running in containers.
> - SQL Server Failover Cluster Instances (FCI).
> - SQL Server roles other than the Database Engine, such as Analysis Services (SSAS), Reporting Services (SSRS), or Integration Services (SSIS).
> - SQL Server editions: Business Intelligence.
> - SQL Server 2008, SQL Server 2008 R2, and older.
> - Installing the Arc agent and SQL Server extension cannot be done as part of sysprep image creation.

## Required permissions

To connect the SQL Server instances and the hosting machine to Azure Arc, you must have a user account or Azure service principal with [Contributor](/azure/role-based-access-control/built-in-roles#contributor) role for the resource group in which the SQL Server will be managed.

Deploying the Connected Machine agent on a SQL Server machine requires that you have administrator permissions to install and configure the agent. On Linux this is done by using the root account, and on Windows, with an account that is a member of the Local Administrators group

## Azure subscription and service limits

Before configuring your SQL server instances and machines with Azure Arc, review the Azure Resource Manager [subscription limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#subscription-limits) and [resource group limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#resource-group-limits) to plan for the number of machines to be connected.

## Supported Azure regions

Arc-enabled SQL Server is available in the following regions:

- East US
- East US 2
- West US 2
- Central US
- South Central US
- UK South
- France Central
- West Europe
- North Europe
- Japan East
- Korea Central
- East Asia
- Southeast Asia
- Australia East

## Next steps

- [Azure Arc-enabled SQL Server](overview.md)
- [Connect your SQL Server to Azure Arc](connect.md)
- [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)
- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
