---
title: Azure Arc enabled SQL Server - Release notes
description: Latest release notes 
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray
ms.date: 10/29/2020
ms.topic: conceptual
ms.prod: sql
---

# Release notes - Azure Arc enabled SQL Server (Preview)

> [!NOTE]
> As a preview feature, the technology presented in this article is subject to [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

## September 2020

Azure Arc enabled SQL Server is released for public preview. Azure Arc enabled SQL Server extends Azure services to SQL Server instances hosted outside of Azure in the customer’s datacenter, on the edge or in a multi-cloud environment.

For details, see [Azure Arc enabled SQL Server Overview](overview.md)

### Known issues

The following issues apply to the September release:

* The **Register Azure Arc enabled SQL Server** blade does not support configuring custom tags. To add custom tags, open the **SQL Server - Azure Arc** resource after registration and change Tags in the **Overview** page.

* Connecting SQL Server instances to Azure Arc requires an account with a broad set of permissions. For details, see [Required permissions](overview.md#required-permissions).

## October 2020

The October update includes the following improvements:

* The register Azure Arc enabled SQL Server blade now includes the **Tags** tab. The tags are included in the registration script and are reflected in the **SQL Server - Azure Arc** resource(s). For details, see [Connect your SQL Server to Azure Arc](connect.md).

* The **Environment Health** entry now supports activation of **SQL Assessment** from the Portal by deploying a *CustomScriptExtension*. For details, see [Configure SQL Assessment](assess.md#run-on-demand-sql-assessment).

### Known issues

The following issues apply to the October release:

* Connecting SQL Server instances to Azure Arc requires an account with a broad set of permissions. For details, see [Required permissions](overview.md#required-permissions).

## Next steps

**Just want to try things out?**  Get started quickly with [Azure Arc enabled SQL Server Jumpstart](https://aka.ms/AzureArcSqlServerJumpstart).
