---
title: Move resources to new region
description: Learn how to move your managed instance to another region.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 04/30/2024
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
ms.custom: azure-sql-split
monikerRange: "= azuresql || = azuresql-mi"
---

# Move resources to new region - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/move-resources-across-regions.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](move-resources-across-regions.md?view=azuresql&preserve-view=true)
> * [SQL Server on Azure VMs](../virtual-machines/windows/move-sql-vm-different-region.md?view=azuresql&preserve-view=true)

This article teaches you a generic workflow for how to move Azure SQL Managed Instance to a new region.

> [!NOTE]
> This article applies to migrations within the Azure public cloud or within the same sovereign cloud.

## Overview

There are various scenarios in which you'd want to move your existing managed instance from one region to another. For example, you're expanding your business to a new region and want to optimize it for the new customer base. Or you need to move the operations to a different region for compliance reasons. Or Azure released a new region that provides a better proximity and improves the customer experience.  

The general workflow to move resources to a different region consists of the following steps: 

1. Verify the prerequisites for the move.
1. Prepare to move the resources in scope.
1. Monitor the preparation process.
1. Test the move process.
1. Initiate the actual move.
1. Remove the resources from the source region.


## Verify prerequisites

1. For each source managed instance, create an instance of the same size in the target region.  
1. Configure the network for a managed instance. For more information, see [network configuration](how-to-content-reference-guide.md#network-configuration).
1. Configure the target `master` database with the correct logins. If you're not the subscription or SQL Managed Instance administrator, work with the administrator to assign the permissions that you need.
1. If your databases are encrypted with transparent data encryption (TDE) and bring your own encryption key (BYOK or Customer-Managed Key) in Azure Key Vault, ensure that the correct encryption material is provisioned in the target regions. 
    - The simplest way to do this is to add the encryption key from the existing key vault (that is being used as TDE Protector on source instance) to the target instance and then set the key as the TDE Protector on the target instance since an instance in one region can now connect to a key vault in any other region.
    - As a best practice to ensure the target instance has access to older encryption keys (required for restoring database backups), run the [Get-AzSqlServerKeyVaultKey](/powershell/module/az.sql/get-azsqlserverkeyvaultkey) cmdlet on the source instance or [Get-AzSqlInstanceKeyVaultKey](/powershell/module/az.sql/get-azsqlinstancekeyvaultkey) cmdlet on the source managed instance to return the list of available keys and add those keys to the target instance.
    - For more information and best practices on configuring customer-managed TDE on the target instance, see [Azure SQL transparent data encryption with customer-managed keys in Azure Key Vault](../database/transparent-data-encryption-byok-overview.md).
1. If audit is enabled for the managed instance, ensure that:
    - The storage container or event hub with the existing logs is moved to the target region.
    - Audit is configured on the target instance. For more information, see [Auditing with SQL Managed Instance](auditing-configure.md).
1. If your instance has a long-term retention policy (LTR), the existing LTR backups will remain associated with the current instance. Because the target instance is different, you'll be able to access the older LTR backups in the source region using the source instance, even if the instance is deleted.

  > [!NOTE]
  > Migrating instances with existing LTR backups between sovereign and public regions is unsupported since this requires moving LTR backups to the target instance, which is not currently possible. 

## Prepare resources

Create a failover group between each source managed instance and the corresponding target instance of SQL Managed Instance.

Replication of all databases on each instance is initiated automatically. For more information, see [Failover groups](failover-group-sql-mi.md).

## Monitor the preparation process

You can periodically call [Get-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/get-azsqldatabaseinstancefailovergroup) to monitor replication of your databases from the source to the target instance. The output object of `Get-AzSqlDatabaseInstanceFailoverGroup` includes a property for the **ReplicationState**:

- **ReplicationState = CATCH_UP** indicates the database is synchronized and can be safely failed over.
- **ReplicationState = SEEDING** indicates that the database isn't yet seeded, and an attempt to fail over will fail.

## Test synchronization

Once **ReplicationState** is `CATCH_UP`, connect to the geo-secondary using the secondary endpoint `<fog-name>.secondary.<zone_id>.database.windows.net` and perform any query against the databases to ensure connectivity, proper security configuration, and data replication.

## Initiate the move

1. Connect to the target managed instance by using the secondary endpoint `<fog-name>.secondary.<zone_id>.database.windows.net`.
1. Use [Switch-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/switch-azsqldatabaseinstancefailovergroup) to switch the secondary managed instance to be the primary with full synchronization. This operation succeeds, or rolls back.
1. Verify that the command has completed successfully by using `nslook up <fog-name>.secondary.<zone_id>.database.windows.net` to ascertain that the DNS CNAME entry points to the target region IP address. If the switch command fails, the CNAME isn't updated.

## Remove the source managed instances

Once the move finishes, remove the resources in the source region to avoid unnecessary charges.

1. Delete the failover group using [Remove-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/remove-azsqldatabaseinstancefailovergroup). This drops the failover group configuration and terminates geo-replication links between the two instances.
1. Delete the source managed instance using [Remove-AzSqlInstance](/powershell/module/az.sql/remove-azsqlinstance).
1. Remove any additional resources in the resource group, such as the virtual network and security group.

## Related content

- [Performance guidance](performance-guidance.md)
- [High availability](high-availability-sla-local-zone-redundancy.md)