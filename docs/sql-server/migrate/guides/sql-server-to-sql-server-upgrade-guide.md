---
title: Upgrade to the Latest Version of SQL Server
description: Step-by-step guidance for modernizing your data assets
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ajithkr
ms.date: 04/27/2026
ms.service: sql
ms.subservice: migration-guide
ms.topic: how-to
ms.custom:
  - sfi-image-nochange
---

# Upgrade SQL Server to the latest version

In this guide, you learn how to upgrade your user databases from previous versions of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] by using the SQL Server migration component in SQL Server Management Studio (SSMS).

For other migration guides, see [Azure Database Migration](/data-migration/).

## Prerequisites

Before beginning your migration project, address the associated prerequisites.
Learn about the supported versions and considerations for [upgrading SQL Server](../../../database-engine/install-windows/upgrade-sql-server.md).

To prepare for the migration, use the [SQL Server migration component in SSMS](/ssms/migrate-sql-server-component).

## Premigration

Once you confirm that the source environment is supported and you address any prerequisites, you can start the premigration stage. The process involves conducting an inventory of the databases that you need to migrate. The next step is to assess the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instances. To assess your source database, use the [SQL migration component in SQL Server Management Studio](/ssms/migrate-sql-server-component#assess-your-environment) before upgrading your [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance. After all database assessments are complete, select **Save report** to export the results to a JSON file for analyzing the data at your own convenience. Next, review the reports for potential migration issues or blockers, and resolve any items that were uncovered.

## Migration overview

When you have the necessary prerequisites in place and complete the tasks associated with the premigration stage, you're ready to complete the schema and data migration. A successful migration and upgrade means you addressed all the issues discovered during the premigration stage.

Review the preparation steps for migration of your databases and logins using [SQL Server migration component in SQL Server Management Studio](/ssms/migrate-sql-server-component#prepare-for-migration).

Preserve backup logs, maintenance plans, and other automated tasks, including jobs, by creating a backup of the system [database msdb](../../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).

View [linked servers](../../../relational-databases/linked-servers/linked-servers-database-engine.md) by using SSMS. In the Object Explorer, right-click server objects to expand the list.

You might need to consider the following factors, depending on the complexity of your data and environment.

| Article | Description |
| --- | --- |
| [Troubleshoot orphaned users (SQL Server)](../../failover-clusters/troubleshoot-orphaned-users-sql-server.md) | Logins that exist on the source instance but not the target can result in orphaned database users after migration. Remap them before or after the upgrade. |
| [Migrating Triggers](../../../relational-databases/in-memory-oltp/migrating-triggers.md) | If your databases use In-Memory OLTP (memory-optimized tables), review how triggers are handled during migration. |
| [Generate and Publish Scripts Wizard](/ssms/scripting/generate-and-publish-scripts-wizard) | Use this tool to script database objects when you need to transfer schemas between instances that can't use backup and restore directly. |
| [Mirrored Backup Media Sets (SQL Server)](../../../relational-databases/backup-restore/mirrored-backup-media-sets-sql-server.md) | Relevant if your backup strategy uses mirrored media sets. Verify that your backup and restore workflow is compatible with the target version. |
| [Backup overview (SQL Server)](../../../relational-databases/backup-restore/backup-overview-sql-server.md) | Take full backups of all databases before upgrading. Backup compatibility is forward-only: backups from a newer version can't be restored to an older version. |
| [Editions and supported features of SQL Server](../../editions-and-components-of-sql-server-latest.md) | Verify that features you depend on are available in your target edition. Some features are edition-specific. |

### Migrate databases and logins

Next, start migrating the databases and logins using the [SQL Server migration component in SQL Server Management Studio](/ssms/migrate-sql-server-component#migrate-your-database).

## Post migration

After you successfully complete the **Migration** stage, go through the post-migration tasks to ensure that everything functions as smoothly and efficiently as possible. The post-migration process is crucial for reconciling any data accuracy issues, verifying completeness, and addressing performance issues with the workload.

For more information about these issues, specific steps to mitigate them, and after the migration
see the [Post-migration validation and optimization guide](../../../relational-databases/post-migration-validation-and-optimization-guide.md).

### Verify applications

After the data migrates to the target environment, all the applications that formerly consumed the source need to start consuming the target. You might need to change the applications to accomplish this goal. Test against the databases to verify that the applications work as expected after the migration.

## Related content

- [Compare SQL data migration tools](../compare-sql-migration-tools.md)
- [Services and tools for data migration](/azure/dms/dms-tools-matrix)
- [Azure Database Migration Guide](/data-migration/)
