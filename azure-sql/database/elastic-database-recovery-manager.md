---
title: Recovery Manager to Fix Shard Map Problems
description: Learn how to use the RecoveryManager class to solve problems with shard maps.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: scoriani
ms.date: 06/13/2025
ms.service: azure-sql-database
ms.subservice: scale-out
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db"
ms.custom:
  - sqldbrb=1
  - sfi-ropc-nochange
---
# Using the RecoveryManager class to fix shard map problems

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

[!INCLUDE [elastic-query-shard-map-manager-mode-end-of-support](includes/elastic-query-shard-map-manager-mode-end-of-support.md)]

The [RecoveryManager](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.recovery.recoverymanager) class provides ADO.NET applications the ability to easily detect and correct any inconsistencies between the global shard map (GSM) and the local shard map (LSM) in a sharded database environment.

The GSM and LSM track the mapping of each database in a sharded environment. Occasionally, a break occurs between the GSM and the LSM. In that case, use the `RecoveryManager` class to detect and repair the break.

The `RecoveryManager` class is part of the [Building scalable cloud databases](elastic-database-client-library.md).

:::image type="content" source="media/elastic-database-recovery-manager/recovery-manager.png" alt-text="Diagram of a Shard map." lightbox="media/elastic-database-recovery-manager/recovery-manager.png":::

For term definitions, see [Elastic Database tools glossary](elastic-scale-glossary.md). To understand how the `ShardMapManager` is used to manage data in a sharded solution, see [Scale out databases with the shard map manager](elastic-scale-shard-map-management.md).

## Why use the recovery manager

In a sharded database environment, there's one tenant per database, and many databases per server. There can also be many servers in the environment. Each database is mapped in the shard map, so calls can be routed to the correct server and database. Databases are tracked according to a **sharding key**, and each shard is assigned a **range of key values**. For example, a sharding key might represent the customer names from "D" to "F." The mapping of all shards (also known as databases) and their mapping ranges are contained in the **global shard map (GSM)**. Each database also contains a map of the ranges contained on the shard that is known as the **local shard map (LSM)**. When an app connects to a shard, the mapping is cached with the app for quick retrieval. The LSM is used to validate cached data.

The GSM and LSM can become out of sync for the following reasons:

1. The deletion of a shard whose range is believed to no longer be in use, or renaming of a shard. Deleting a shard results in an **orphaned shard mapping**. Similarly, a renamed database can cause an orphaned shard mapping. Depending on the intent of the change, the shard might need to be removed or the shard location needs to be updated. To recover a deleted database, see [Restore a database from a backup in Azure SQL Database](recovery-using-backups.md).
1. A geo-failover event occurs. To continue, one must update the server name, and database name of shard map manager in the application and then update the shard-mapping details for all shards in a shard map. If there's a geo-failover, such recovery logic should be automated within the failover workflow. Automating recovery actions enables a frictionless manageability for geo-enabled databases and avoids manual human actions. To learn about options to recover a database if there's a data center outage, see [Business continuity in Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-db&preserve-view=true) and [Disaster recovery guidance - Azure SQL Database](disaster-recovery-guidance.md).
1. Either a shard or the `ShardMapManager` database is restored to an earlier point-in time. To learn about point in time recovery using backups, see [Restore a database from a backup in Azure SQL Database](recovery-using-backups.md).

For more information about Azure SQL Database Elastic Database tools, geo-replication and Restore, see:

- [Business continuity in Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- [Get started with Elastic Database Tools](elastic-scale-get-started.md)  
- [Scale out databases with the shard map manager](elastic-scale-shard-map-management.md)

<a id="retrieving-recoverymanager-from-a-shardmapmanager"></a>

## Retrieve RecoveryManager from a ShardMapManager

The first step is to create a `RecoveryManager` instance. The [GetRecoveryManager method](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.shardmapmanager.getrecoverymanager) returns the recovery manager for the current [ShardMapManager](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.shardmapmanager) instance. To address any inconsistencies in the shard map, you must first retrieve the `RecoveryManager` for the particular shard map.

   ```java
    ShardMapManager smm = ShardMapManagerFactory.GetSqlShardMapManager(smmConnectionString,  
             ShardMapManagerLoadPolicy.Lazy);
             RecoveryManager rm = smm.GetRecoveryManager();
   ```

In this example, the `RecoveryManager` is initialized from the `ShardMapManager`. The `ShardMapManager` containing a `ShardMap` is also already initialized.

Since this application code manipulates the shard map itself, the credentials used in the factory method (in the preceding example, `smmConnectionString`) should be credentials that have read-write permissions on the GSM database referenced by the connection string. These credentials are typically different from credentials used to open connections for data-dependent routing. For more information, see [Credentials used to access the Elastic Database client library](elastic-scale-manage-credentials.md).

<a id="removing-a-shard-from-the-shardmap-after-a-shard-is-deleted"></a>

## Remove a shard from the ShardMap after a shard is deleted

The [DetachShard method](/previous-versions/azure/dn842083(v=azure.100)) detaches the given shard from the shard map and deletes mappings associated with the shard.  

- The `location` parameter is the shard location, specifically server name and database name, of the shard being detached.
- The `shardMapName` parameter is the shard map name. This is only required when multiple shard maps are managed by the same shard map manager. Optional.

> [!IMPORTANT]
> Use this technique only if you're certain that the range for the updated mapping is empty. These methods don't check data for the range being moved, so it's best to include checks in your code.

This example removes shards from the shard map.

   ```java
   rm.DetachShard(s.Location, customerMap);
   ```

The shard map reflects the shard location in the GSM before the deletion of the shard. Because the shard was deleted, it's assumed this was intentional, and the sharding key range is no longer in use. If not, you can execute point-in time restore. To recover the shard from an earlier point-in-time. (In that case, review the following section to detect shard inconsistencies.) To recover, see [Restore a database from a backup in Azure SQL Database](recovery-using-backups.md).

Since it's assumed that the database deletion was intentional, the final administrative cleanup action is to delete the entry to the shard in the shard map manager. This prevents the application from inadvertently writing information to a range that isn't expected.

<a id="to-detect-mapping-differences"></a>

## Detect mapping differences

The [DetectMappingDifferences method](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.recovery.recoverymanager.detectmappingdifferences) selects and returns one of the shard maps (either local or global) as the source of truth and reconciles mappings on both shard maps (GSM and LSM).

   ```java
   rm.DetectMappingDifferences(location, shardMapName);
   ```

- The `location` specifies the server name and database name.
- The `shardMapName` parameter is the shard map name. This is only required if multiple shard maps are managed by the same shard map manager. Optional.

<a id="to-resolve-mapping-differences"></a>

## Resolve mapping differences

The [ResolveMappingDifferences method](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.recovery.recoverymanager.resolvemappingdifferences) selects one of the shard maps (either local or global) as the source of truth and reconciles mappings on both shard maps (GSM and LSM).

   ```java
   ResolveMappingDifferences (RecoveryToken, MappingDifferenceResolution.KeepShardMapping);
   ```

- The `RecoveryToken` parameter enumerates the differences in the mappings between the GSM and the LSM for the specific shard.
- The [MappingDifferenceResolution enumeration](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.recovery.mappingdifferenceresolution) is used to indicate the method for resolving the difference between the shard mappings.
- `MappingDifferenceResolution.KeepShardMapping` is recommended that when the LSM contains the accurate mapping and therefore the mapping in the shard should be used. This is typically the case if there's a failover: the shard now resides on a new server. Since the shard must first be removed from the GSM (using the `RecoveryManager.DetachShard` method), a mapping no longer exists on the GSM. Therefore, the LSM must be used to re-establish the shard mapping.

## Attach a shard to the ShardMap after a shard is restored

The [AttachShard method](/dotnet/api/microsoft.azure.sqldatabase.elasticscale.shardmanagement.recovery.recoverymanager.attachshard) attaches the given shard to the shard map. It then detects any shard map inconsistencies and updates the mappings to match the shard at the point of the shard restoration. It's assumed that the database is also renamed to reflect the original database name (before the shard was restored), since the point-in time restoration defaults to a new database appended with the timestamp.

   ```java
   rm.AttachShard(location, shardMapName)
   ```

- The `location` parameter is the server name and database name, of the shard being attached.
- The `shardMapName` parameter is the shard map name. This is only required when multiple shard maps are managed by the same shard map manager. Optional.

This example adds a shard to the shard map that has been recently restored from an earlier point-in time. Since the shard (namely the mapping for the shard in the LSM) has been restored, it's potentially inconsistent with the shard entry in the GSM. Outside of this example code, the shard was restored and renamed to the original name of the database. Since it was restored, it's assumed that the mapping in the LSM is the trusted mapping.

   ```csharp
   rm.AttachShard(s.Location, customerMap);
   var gs = rm.DetectMappingDifferences(s.Location);
      foreach (RecoveryToken g in gs)
       {
       rm.ResolveMappingDifferences(g, MappingDifferenceResolution.KeepShardMapping);
       }
   ```

<a id="updating-shard-locations-after-a-geo-failover-restore-of-the-shards"></a>

## Update shard locations after a geo-failover (restore) of the shards

If there's a geo-failover, the secondary database is made write accessible and becomes the new primary database. The name of the server, and potentially the database (depending on your configuration), might be different from the original primary. Therefore the mapping entries for the shard in the GSM and LSM must be fixed. Similarly, if the database is restored to a different name or location, or to an earlier point in time, this might cause inconsistencies in the shard maps. The Shard Map Manager handles the distribution of open connections to the correct database. Distribution is based on the data in the shard map and the value of the sharding key that is the target of the applications request. After a geo-failover, this information must be updated with the accurate server name, database name, and shard mapping of the recovered database.

## Best practices

Geo-failover and recovery are operations typically managed by a cloud administrator of the application intentionally utilizing Azure SQL Database business continuity features. Business continuity planning requires processes, procedures, and measures to ensure that business operations can continue without interruption. The methods available as part of the `RecoveryManager` class should be used within this work flow to ensure the GSM and LSM are kept up-to-date based on the recovery action taken. There are five basic steps to properly ensuring the GSM and LSM reflect the accurate information after a failover event. The application code to execute these steps can be integrated into existing tools and workflow.

1. Retrieve the `RecoveryManager` from the ShardMapManager.
1. Detach the old shard from the shard map.
1. Attach the new shard to the shard map, including the new shard location.
1. Detect inconsistencies in the mapping between the GSM and LSM.
1. Resolve differences between the GSM and the LSM, trusting the LSM.

This example performs the following steps:

1. Removes shards from the Shard Map that reflect shard locations before the failover event.
1. Attaches shards to the Shard Map reflecting the new shard locations (the parameter `Configuration.SecondaryServer` is the new server name but the same database name).
1. Retrieves the recovery tokens by detecting mapping differences between the GSM and the LSM for each shard.
1. Resolves the inconsistencies by trusting the mapping from the LSM of each shard.

   ```csharp
   var shards = smm.GetShards();
   foreach (shard s in shards)
   {
     if (s.Location.Server == Configuration.PrimaryServer)
         {
          ShardLocation slNew = new ShardLocation(Configuration.SecondaryServer, s.Location.Database);
          rm.DetachShard(s.Location);
          rm.AttachShard(slNew);
          var gs = rm.DetectMappingDifferences(slNew);
          foreach (RecoveryToken g in gs)
            {
               rm.ResolveMappingDifferences(g, MappingDifferenceResolution.KeepShardMapping);
            }
        }
    }
   ```

[!INCLUDE [elastic-scale-include](../includes/elastic-scale-include.md)]