---
author: danimir
ms.author: danil
ms.date: 11/18/2026
ms.service: azure-sql-managed-instance
ms.topic: include
ai-usage: ai-assisted
---

As part of pre-migration, discover your existing data sources, assess migration feasibility, and identify any blocking issues that might prevent your migration. If you already completed these steps, skip this section.

Use the following tools to discover SQL sources in your environment:

- [SQL Server enabled by Azure Arc](/sql/sql-server/azure-arc/migration-assessment): SQL Server enabled by Azure Arc automatically produces an assessment for migration to Azure, simplifying the discovery process and readiness assessment for migration.
- [Azure Migrate](/azure/migrate/migrate-services-overview) to assess migration suitability of on-premises servers, perform performance-based sizing, and provide cost estimations for running them in Azure.
- [Microsoft Assessment and Planning Toolkit (the "MAP Toolkit")](https://www.microsoft.com/download/details.aspx?id=7826) to assess your current IT infrastructure. The toolkit provides a powerful inventory, assessment, and reporting tool to simplify the migration planning process.

After you discover data sources, assess any on-premises SQL Server instances that you can migrate to Azure SQL Managed Instance to identify migration blockers or compatibility issues.

Use the [migration readiness assessment](/sql/sql-server/azure-arc/migration-assessment) to assess your source SQL Server instance.

For detailed guidance, review [pre-migration](/data-migration/sql-server/managed-instance/guide).
