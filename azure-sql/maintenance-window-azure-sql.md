---
title: Maintenance window in Azure SQL
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn more about configurable maintenance windows in Azure SQL Database and Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, urosmil, scottkim
ms.date: 01/11/2024
ms.service: azure-sql
ms.subservice: service-overview
ms.topic: conceptual
ms.custom: azure-sql-split
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---

# Maintenance window in Azure SQL
[!INCLUDE [appliesto-sqldb-sqlmi](includes/appliesto-sqldb-sqlmi.md)]

The maintenance window feature allows you to configure maintenance schedule for [Azure SQL Database](database/sql-database-paas-overview.md?view=azuresql-db&preserve-view=true) and [Azure SQL Managed Instance](managed-instance/sql-managed-instance-paas-overview.md?view=azuresql-mi&preserve-view=true) resources making impactful maintenance events predictable and less disruptive for your workload.

The maintenance window is free of charge and can be configured on creation or for existing Azure SQL resources. It can be configured using the Azure portal, PowerShell, CLI, or Azure API.

## Overview

Azure periodically performs [planned maintenance](database/planned-maintenance.md) of SQL Database and SQL Managed Instance resources. During Azure SQL maintenance event, databases are fully available but can be subject to short reconfigurations within availability SLAs for [Azure SQL Database](https://azure.microsoft.com/support/legal/sla/azure-sql-database) and [Azure SQL Managed Instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance).

By choosing a maintenance window you prefer, you can minimize the impact of planned maintenance by scheduling it to occur outside of your peak business hours.

For more information for each platform, see:

**Azure SQL Database**

- [Maintenance window in Azure SQL Database](database/maintenance-window.md?view=azuresql-db&preserve-view=true)
- [Maintenance window FAQ in Azure SQL Database](database/maintenance-window-faq.yml?view=azuresql-db&preserve-view=true)
- [Configure maintenance window in Azure SQL Database](database/maintenance-window-configure.md?view=azuresql-db&preserve-view=true)
- [Advance notifications for planned maintenance events in Azure SQL Database](database/advance-notifications.md?view=azuresql-db&preserve-view=true)

**Azure SQL Managed Instance**

- [Maintenance window in Azure SQL Managed Instance](managed-instance/maintenance-window.md?view=azuresql-mi&preserve-view=true)
- [Maintenance window FAQ in Azure SQL Managed Instance](managed-instance/maintenance-window-faq.yml?view=azuresql-mi&preserve-view=true)
- [Configure maintenance window in Azure SQL Managed Instance](managed-instance/maintenance-window-configure.md?view=azuresql-mi&preserve-view=true)
- [Advance notifications for planned maintenance events in Azure SQL Managed Instance](managed-instance/advance-notifications.md?view=azuresql-mi&preserve-view=true)

## Related content

- [Plan for Azure maintenance events in Azure SQL Database and Azure SQL Managed Instance](database/planned-maintenance.md)