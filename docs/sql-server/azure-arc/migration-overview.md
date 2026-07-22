---
title: Migration Overview
titleSuffix: SQL Server migration in Azure Arc
description: Learn about the capabilities of SQL Server migration in Azure Arc.
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: mathoma
ms.date: 04/16/2026
ms.topic: how-to
ms.collection: ce-skilling-ai-copilot

---

# SQL Server migration in Azure Arc Overview

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article provides an overview of SQL Server migration in Azure Arc for [SQL Server enabled by Azure Arc](overview.md).

## Overview

After your SQL Server instance is enabled by Azure Arc, you can assess your SQL Server data estate to identify an optimal Azure SQL migration target. Then you can migrate your SQL Server databases to Azure directly from the Azure portal.

When your SQL Server instance is enabled by Azure Arc, you can:

- Evaluate and assess whether your SQL Server instance is ready to migrate to Azure.
- Identify potential migration issues, and learn how to mitigate them.
- Optimize for performance and cost with guidance around service tiers, configuration, and sizing.

Discovery of SQL Server instances and generation of readiness reports happen automatically every weekend, but you can start them manually at any time. The process takes only a few minutes to complete. No extra configuration or setup is required. First, choose an appropriate Azure SQL target and prepare your environment. Then, you can migrate your SQL Server databases to Azure directly from the Azure portal through a fully managed and automated process.

SQL Server migration in Azure Arc is available by default for all SQL Server instances enabled by Azure Arc, starting with [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)].

## Migration targets

You can migrate to [Azure SQL Managed Instance](migrate-to-azure-sql-managed-instance.md) or [SQL Server on Azure VMs](migrate-to-sql-server-on-azure-vms.md).

## Microsoft Copilot assisted migration

On the database migration pane in the Azure portal, Microsoft Copilot is built into the experience to assist you throughout the migration process. Interactively chatting with Microsoft Copilot searches through the Microsoft knowledgebase to help you along the way as you migrate to Azure.

Microsoft Copilot provides AI-powered assistance to help you make decisions or take actions at certain points with prompts such as:
- How are assessments made?
- Help me compare.
- Start the migration.
- Help me choose the right migration method.
- Monitor the migration.
- Complete the migration.

Select the **Copilot Start migration** icon on the **Database migration** pane to open the Copilot chat window:

:::image type="content" source="media/migrate-to-azure-sql-managed-instance/copilot-integration.png" alt-text="Screenshot that shows the Copilot Start migration icon on the Database migration pane in the Azure portal.":::

## Migration dashboard

The [migration dashboard](migration-inventory.md) is a convenient view that shows all instances of SQL Server enabled by Azure Arc and their migration readiness. SQL Server enabled by Azure Arc automatically produces an assessment for migration to Azure. This assessment plays a vital role in the success of your cloud migration and modernization journey. With this dashboard, you can track the migration journey at scale. The readiness is projected as properties into the Azure management plane, which allows the use of organizational, tagging, and querying capabilities native to Azure.

:::image type="content" source="media/migration-assessment/migration-dashboard.png" alt-text="Screenshot that shows the migration dashboard for SQL Server enabled by Azure Arc." lightbox="media/migration-assessment/migration-dashboard.png":::

The dashboard provides:

- An overview of discovered SQL Server instances and databases.
- An overview of the SQL Server instances with generated assessments.
- A migration-readiness summary for each Azure SQL offering.
- Rich filtering capabilities that you can use to tailor the view to your needs.

## Discover and assess

SQL Server enabled by Azure Arc automatically produces an [assessment for migration to Azure](migration-assessment.md). This assessment plays a vital role in the success of your cloud migration and modernization journey. Azure Arc simplifies the discovery process and readiness assessment for migration.

:::image type="content" source="media/migration-assessment/assessment-main.png" alt-text="Screenshot that shows how to get to the SQL Server migration assessment report for a SQL Server resource." lightbox="media/migration-assessment/assessment-main.png":::

Evaluate and measure the readiness of SQL Server for migration to Azure SQL. This process:

- Discovers and assesses the SQL Server instance and databases.
- Pinpoints SQL Server workloads that are ready for migration.
- Identifies potential compatibility issues with the target environment.
- Assesses migration risks.
- Provides recommendations to mitigate these risks.

The assessment:

- Provides cloud readiness, identifies risks, and offers mitigation strategies.
- Provides the specific service tier and Azure SQL configuration for each Azure SQL migration target type that best fits the workload needs.
- Provides retail prices for the suggested configuration for each Azure SQL migration target type.
- Suggests the best Azure SQL migration target type based on the chosen migration strategy.
- Is generated automatically.
- Runs continuously on a default schedule of once per week.
- Is free and available for all SQL Server editions.

You can obtain a migration assessment for SQL Servers located anywhere, such as:
- Your datacenter.
- Edge site locations, such as retail stores.
- Public cloud or hosting providers.

The assessment is available for any instance of SQL Server enabled by Azure Arc.

## Database migration

On the **Database migration** pane, you can migrate your SQL Server databases to Azure SQL Managed Instance or SQL Server on Azure VMs. The migration process is fully managed and automated from the Azure portal. Once you're ready to start, you can use the tiles to assess your SQL Server databases, choose a migration target, and start the migration process.

**Database migration** guides you through the migration with easy to follow tiles for each step of the process:

:::image type="content" source="media/migrate-to-azure-sql-managed-instance/migration-home-page.png" alt-text="Screenshot that shows the migration home page for a SQL Server instance in the Azure portal." lightbox="media/migrate-to-azure-sql-managed-instance/migration-home-page.png":::

The **Database Migration** pane also has a useful summary of the migration status for your instance, such as the number of total databases, the recommended target, the number of completed migrations, and the number of ongoing migrations:

:::image type="content" source="media/migrate-to-azure-sql-managed-instance/database-migration-summary.png" alt-text="Screenshot of the summary on the Database Migration pane in the Azure portal." lightbox="media/migrate-to-azure-sql-managed-instance/database-migration-summary.png":::

## Related content

- [Track migration journey by using migration dashboard - SQL Server enabled by Azure Arc](migration-inventory.md)
- [Assess migration readiness - SQL Server enabled by Azure Arc](migration-assessment.md)
- [Prepare environment for a Managed Instance link migration - SQL Server migration in Azure Arc](migration-sql-mi-prepare-link.md)
- [Prepare environment for LRS migration - SQL Server migration in Azure Arc](migration-sql-mi-prepare-log-replay-service.md)
- [SQL Server enabled by Azure Arc](overview.md)
- [Deployment options for SQL Server enabled by Azure Arc](deployment-options.md)
