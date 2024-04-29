---
title: Restore to a point-in-time
description: Describes how to configure automated backups and restore to a point in time
author: AbdullahMSFT
ms.author: amamun 
ms.reviewer: mikeray, randolphwest
ms.date: 10/25/2023
ms.topic: conceptual
ms.custom: ignite-2023
---

# Restore to a point-in-time 

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article demonstrates how to restore a database to a point-in-time as a new database on the same instance of SQL Server enabled by Azure Arc.

The new database is restored from backup to a point-in-time in the past that is within the retention period.  

[!INCLUDE [azure-arc-sql-preview](includes/azure-arc-sql-preview.md)]

## Prerequisite

Before you can restore a database to a point-in-time with the instructions in this article, you have to enable automatic backups. For instructions, see [Manage automated backups - [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]](backup-local.md).

Automated backups are disabled by default.

## Steps

### [Azure portal](#tab/azure)

To restore to a point-in-time from Azure portal:

1. Browse to the Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]
1. Select **Backups**
1. Among the list of databases on the right pane, select **Restore** for the database you want to restore.

   Azure portal guides you through the instructions to create a database with the selected database as the source database.

1. Provide details such as the **point-in-time to restore** to, and the **name** for the new database.
1. Proceed through the wizard to submit the restore deployment

### [Azure CLI](#tab/az)

To restore to a point-in-time with `az` CLI, update the following example for your environment and run it through your CLI:

```azurecli
az sql db-arc restore --dest-name <name for new database> --resource-group <resource-group> --name <name of source database> --server <Name of Arc-enabled SQL Server> --time <point-in-time to restore to>
```

Example:

```azurecli
az sql db-arc restore --dest-name "new_db" --resouce-group "my-rg" --name "mysourcedb" --server "ArcSQL1" --time "2020-08-16T12:12:12Z"
```
---

## Limitations

- The process described in this article requires the backup be taken by an automated backup from an instance of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]. For instructions, see [Manage automated backups - [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]](backup-local.md).

## Related tasks

- [View SQL Server databases - Azure Arc](view-databases.md)
- [Recovery Models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md)
