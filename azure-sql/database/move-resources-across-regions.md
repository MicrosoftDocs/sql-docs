---
title: Move resources to new region
titleSuffix: Azure SQL Database
description: Learn how to move your database or elastic pool to another region for Azure SQL Database. 
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 04/30/2024
ms.service: sql-database
ms.subservice: data-movement
ms.topic: how-to
ms.custom: azure-sql-split
monikerRange: "= azuresql || = azuresql-db"
zone_pivot_groups: azure-sql-deployment-option-single-elastic
---

# Move resources to new region - Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](move-resources-across-regions.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/move-resources-across-regions.md?view=azuresql&preserve-view=true)
> * [SQL Server on Azure VMs](../virtual-machines/windows/move-sql-vm-different-region.md?view=azuresql&preserve-view=true)

This article teaches you a generic workflow for how to move your database or elastic pool to a new region.

> [!NOTE]
> - To move databases and elastic pools to a different Azure region, you can also use the recommended [Azure Resource Mover](/azure/resource-mover/tutorial-move-region-sql). 
> - This article applies to migrations within the Azure public cloud or within the same sovereign cloud. 

## Overview

There are various scenarios in which you'd want to move your existing database or pool from one region to another. For example, you're expanding your business to a new region and want to optimize it for the new customer base. Or you need to move the operations to a different region for compliance reasons. Or Azure released a new region that provides a better proximity and improves the customer experience.  

The general workflow to move resources to a different region consists of the following steps: 

1. Verify the prerequisites for the move.
1. Prepare to move the resources in scope.
1. Monitor the preparation process.
1. Test the move process.
1. Initiate the actual move.

::: zone pivot="azure-sql-single-db"

## Verify prerequisites to move database

1. Create a target server for each source server.
1. Configure the firewall with the right exceptions by using [PowerShell](scripts/create-and-configure-database-powershell.md).  
1. Configure both servers with the correct logins. If you're not the subscription administrator or SQL server administrator, work with the administrator to assign the permissions that you need. For more information, see [How to manage Azure SQL Database security after disaster recovery](active-geo-replication-security-configure.md).
1. If your databases are encrypted with transparent data encryption (TDE) and bring your own encryption key (BYOK or Customer-Managed Key) in Azure Key Vault, ensure that the correct encryption material is provisioned in the target regions. 
    - The simplest way to do this is to add the encryption key from the existing key vault (that is being used as TDE Protector on source server) to the target server and then set the key as the TDE Protector on the target server since a server in one region can now be connected to a key vault in any other region. 
    - As a best practice to ensure the target server has access to older encryption keys (required for restoring database backups), run the [Get-AzSqlServerKeyVaultKey](/powershell/module/az.sql/get-azsqlserverkeyvaultkey) cmdlet on the source server to return the list of available keys and add those keys to the target server.
    - For more information and best practices on configuring customer-managed TDE on the target server, see [Azure SQL transparent data encryption with customer-managed keys in Azure Key Vault](transparent-data-encryption-byok-overview.md).
    - To move the key vault to the new region, see [Move an Azure key vault across regions](/azure/key-vault/general/move-region). 
1. If database-level audit is enabled, disable it and enable server-level auditing instead. After failover, database-level auditing requires cross-region traffic, which isn't desired or possible after the move.
1. For server-level audits, ensure that:
   - The storage container, Log Analytics, or event hub with the existing audit logs is moved to the target region.
   - Auditing is configured on the target server. For more information, see [Get started with SQL Database auditing](./auditing-overview.md).
1. If your server has a long-term retention policy (LTR), the existing LTR backups remain associated with the current server. Because the target server is different, you are able to access the older LTR backups in the source region by using the source server, even if the server is deleted.

  > [!NOTE]
  > Migrating databases with existing LTR backups between sovereign and public regions is unsupported since this requires moving LTR backups to the target server, which is not currently possible. 

## Prepare resources

1. Create a [failover group](failover-group-configure-sql-db.md#create-failover-group) between the server of the source and the server of the target.  
1. Add the databases you want to move to the failover group. Replication of all added databases is initiated automatically. For more information, see [Using failover groups with SQL Database](failover-group-sql-db.md).

## Monitor the preparation process

You can periodically call [Get-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/get-azsqldatabasefailovergroup) to monitor replication of your databases from the source to the target server. The output object of `Get-AzSqlDatabaseFailoverGroup` includes a property for the **ReplicationState**:

- **ReplicationState = 2** (CATCH_UP) indicates the database is synchronized and can be safely failed over.
- **ReplicationState = 0** (SEEDING) indicates that the database isn't yet seeded, and an attempt to fail over will fail.

## Test synchronization

After **ReplicationState** is `2`, connect to each database or subset of databases using the secondary endpoint `<fog-name>.secondary.database.windows.net` and perform any query against the databases to ensure connectivity, proper security configuration, and data replication.

## Initiate the move

1. Connect to the target server using the secondary endpoint `<fog-name>.secondary.database.windows.net`.
1. Use [Switch-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/switch-azsqldatabasefailovergroup) to switch the secondary server to be the primary with full synchronization. This operation succeeds or it rolls back.
1. Verify that the command has completed successfully by using `nslook up <fog-name>.secondary.database.windows.net` to ascertain that the DNS CNAME entry points to the target region IP address. If the switch command fails, the CNAME won't be updated.

## Remove the source databases

Once the move completes, remove the resources in the source region to avoid unnecessary charges.

1. Delete the failover group using [Remove-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/remove-azsqldatabasefailovergroup).
1. Delete each source database using [Remove-AzSqlDatabase](/powershell/module/az.sql/remove-azsqldatabase) for each of the databases on the source server. This automatically terminates geo-replication links.
1. Delete the source server using [Remove-AzSqlServer](/powershell/module/az.sql/remove-azsqlserver).
1. Remove the key vault, audit storage containers, event hub, Microsoft Entra tenant, and other dependent resources to stop being billed for them.

::: zone-end

::: zone pivot="azure-sql-elastic-pool"


## Verify prerequisites to move pool

1. Create a target server for each source server.
1. Configure the firewall with the right exceptions using [PowerShell](scripts/create-and-configure-database-powershell.md).
1. Configure the servers with the correct logins. If you're not the subscription administrator or server administrator, work with the administrator to assign the permissions that you need. For more information, see [How to manage Azure SQL Database security after disaster recovery](active-geo-replication-security-configure.md).
1. If your databases are encrypted with transparent data encryption and use your own encryption key in Azure Key Vault, ensure that the correct encryption material is provisioned in the target region.
1. Create a target elastic pool for each source elastic pool, making sure the pool is created in the same service tier, with the same name and the same size.
1. If a database-level audit is enabled, disable it and enable server-level auditing instead. After failover, database-level auditing will require cross-region traffic, which isn't desired, or possible after the move.
1. For server-level audits, ensure that:
    - The storage container, Log Analytics, or event hub with the existing audit logs is moved to the target region.
    - Audit configuration is configured at the target server. For more information, see [SQL Database auditing](./auditing-overview.md).
1. If your server has a long-term retention policy (LTR), the existing LTR backups remain associated with the current server. Because the target server is different, you are able to access the older LTR backups in the source region using the source server, even if the server is deleted.

  > [!NOTE]
  > Migrating databases with existing LTR backups between sovereign and public regions is unsupported since this requires moving LTR backups to the target server, which is not currently possible. 

## Prepare to move

1. Create a separate [failover group](failover-group-configure-sql-db.md#create-failover-group) between each elastic pool on the source server and its counterpart elastic pool on the target server.
1. Add all the databases in the pool to the failover group. Replication of the added databases is initiated automatically. For more information, see [Using failover groups with SQL Database](failover-group-sql-db.md).

      > [!NOTE]
      > While it is possible to create a failover group that includes multiple elastic pools, we strongly recommend that you create a separate failover group for each pool. If you have a large number of databases across multiple elastic pools that you need to move, you can run the preparation steps in parallel and then initiate the move step in parallel. This process scales better and takes less time compared to having multiple elastic pools in the same failover group.

## Monitor the preparation process

You can periodically call [Get-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/get-azsqldatabasefailovergroup) to monitor replication of your databases from the source to the target. The output object of `Get-AzSqlDatabaseFailoverGroup` includes a property for the **ReplicationState**:

- **ReplicationState = 2** (CATCH_UP) indicates the database is synchronized and can be safely failed over.
- **ReplicationState = 0** (SEEDING) indicates that the database isn't yet seeded, and an attempt to fail over will fail.

## Test synchronization

Once **ReplicationState** is `2`, connect to each database or subset of databases using the secondary endpoint `<fog-name>.secondary.database.windows.net` and perform any query against the databases to ensure connectivity, proper security configuration, and data replication.

## Initiate the move

1. Connect to the target server using the secondary endpoint `<fog-name>.secondary.database.windows.net`.
1. Use [Switch-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/switch-azsqldatabasefailovergroup) to switch the secondary server to be the primary with full synchronization. This operation either succeeds, or it rolls back.
1. Verify that the command has completed successfully by using `nslook up <fog-name>.secondary.database.windows.net` to ascertain that the DNS CNAME entry points to the target region IP address. If the switch command fails, the CNAME isn't updated.

## Remove the source elastic pools

Once the move completes, remove the resources in the source region to avoid unnecessary charges.

1. Delete the failover group using [Remove-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/remove-azsqldatabasefailovergroup).
1. Delete each source elastic pool on the source server using [Remove-AzSqlElasticPool](/powershell/module/az.sql/remove-azsqlelasticpool).
1. Delete the source server using [Remove-AzSqlServer](/powershell/module/az.sql/remove-azsqlserver).
1. Remove the key vault, audit storage containers, event hub, Microsoft Entra tenant, and other dependent resources to stop being billed for them.

::: zone-end

## Next steps

[Manage](manage-data-after-migrating-to-database.md) your database after it has been migrated.
