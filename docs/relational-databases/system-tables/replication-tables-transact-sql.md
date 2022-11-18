---
title: "Replication Tables (Transact-SQL)"
description: Replication system tables support a replication topology. Replication adds system tables to a database configured as a Publisher or a Subscriber.
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
helpviewer_keywords:
  - "system tables [SQL Server], replication"
  - "replication [SQL Server], system tables"
dev_langs:
  - "TSQL"
ms.assetid: 5696ee73-5d7c-4f26-b7ee-6831c9c3edf7
---
# Replication Tables (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  A replication topology is supported by replication system tables. When a user database is configured as a Publisher or a Subscriber, replication adds system tables to the database. These tables are removed when a user database is removed from a replication topology. For general rules regarding using system tables, see [System Tables &#40;Transact-SQL&#41;](system-tables-transact-sql.md).  
  
## Replication Tables  
 The following is a list of the system tables used by replication, grouped by database.  
  
### Replication Tables in the master Database  

:::row:::
    :::column:::
        [MSreplication_options &#40;Transact-SQL&#41;](msreplication-options-transact-sql.md)
    :::column-end:::
    :::column:::

    :::column-end:::
:::row-end:::

### Replication Tables in the msdb Database  

:::row:::
    :::column:::
        [MSagentparameterlist](msagentparameterlist-transact-sql.md)

        [MSagent_parameters](msagent-parameters-transact-sql.md)

        [MSagent_profiles](msagent-profiles-transact-sql.md)

        [MSdistpublishers](msdistpublishers-transact-sql.md)

        [MSdistributiondbs](msdistributiondbs-transact-sql.md)

        [MSdistributor](msdistributor-transact-sql.md)
    :::column-end:::
    :::column:::
        [MSdbms](msdbms-transact-sql.md)

        [MSdbms_datatype](msdbms-datatype-transact-sql.md)

        [MSdbms_datatype_mapping](msdbms-datatype-mapping-transact-sql.md)

        [MSdbms_map](msdbms-map-transact-sql.md)

        [MSreplmonthresholdmetrics](msreplmonthresholdmetrics-transact-sql.md)

        [sysreplicationalerts](sysreplicationalerts-transact-sql.md)
    :::column-end:::
:::row-end:::

### Replication Tables in the Distribution Database  

:::row:::
    :::column:::
        [MSarticles](msarticles-transact-sql.md)

        [MScached_peer_lsns](mscached-peer-lsns-transact-sql.md)

        [MSdistribution_agents](msdistribution-agents-transact-sql.md)

        [MSdistribution_history](msdistribution-history-transact-sql.md)

        [MSlogreader_agents](mslogreader-agents-transact-sql.md)

        [MSlogreader_history](mslogreader-history-transact-sql.md)

        [MSmerge_agents](msmerge-agents-transact-sql.md)

        [MSmerge_articlehistory](msmerge-articlehistory-transact-sql.md)

        [MSmerge_history](msmerge-history-transact-sql.md)

        [MSmerge_identity_range_allocations](msmerge-identity-range-allocations-transact-sql.md)

        [MSmerge_sessions](msmerge-sessions-transact-sql.md)

        [MSmerge_subscriptions](msmerge-subscriptions-transact-sql.md)

        [MSpublication_access](mspublication-access-transact-sql.md)

        [MSpublications](mspublications-transact-sql.md)

        [MSpublicationthresholds](mspublicationthresholds-transact-sql.md)

        [MSpublisher_databases](mspublisher-databases-transact-sql.md)

        [MSqreader_agents](msqreader-agents-transact-sql.md)

        [MSqreader_history](msqreader-history-transact-sql.md)
    :::column-end:::
    :::column:::
        [MSrepl_backup_lsns](msrepl-backup-lsns-transact-sql.md)

        [MSrepl_commands](msrepl-commands-transact-sql.md)

        [MSrepl_errors](msrepl-errors-transact-sql.md)

        [MSrepl_identity_range](msrepl-identity-range-transact-sql.md)

        [MSrepl_originators](msrepl-originators-transact-sql.md)

        [MSrepl_transactions](msrepl-transactions-transact-sql.md)

        [MSrepl_version](msrepl-version-transact-sql.md)

        [MSreplication_monitordata](msreplication-monitordata-transact-sql.md)

        [MSsnapshot_agents](mssnapshot-agents-transact-sql.md)

        [MSsnapshot_history](mssnapshot-history-transact-sql.md)

        [MSsubscriber_info](mssubscriber-info-transact-sql.md)

        [MSsubscriber_schedule](mssubscriber-schedule-transact-sql.md)

        [MSsubscriptions](mssubscriptions-transact-sql.md)

        [MSsubscription_properties](mssubscription-properties-transact-sql.md)

        [MSsync_states](mssync-states-transact-sql.md)

        [MStracer_history](mstracer-history-transact-sql.md)

        [MStracer_tokens](mstracer-tokens-transact-sql.md)
    :::column-end:::
:::row-end:::

 These tables in the distribution database are used for replicating data from non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. For more information, see [Non-SQL Server Publishers](../../relational-databases/replication/non-sql/non-sql-server-publishers.md).  

:::row:::
    :::column:::
        [IHarticles](iharticles-transact-sql.md)

        [IHcolumns](ihcolumns-transact-sql.md)

        [IHconstrainttypes](ihconstrainttypes-transact-sql.md)

        [IHindextypes](ihindextypes-transact-sql.md)

        [IHindextypes](ihindextypes-transact-sql.md)

        [IHpublications](ihpublications-transact-sql.md)

        [IHpublishercolumnconstraints](ihpublishercolumnconstraints-transact-sql.md)
    :::column-end:::
    :::column:::
        [IHpublishercolumnindexes](ihpublishercolumnindexes-transact-sql.md)

        [IHpublishercolumns](ihpublishercolumns-transact-sql.md)

        [IHpublisherconstraints](ihpublisherconstraints-transact-sql.md)

        [IHpublisherindexes](ihpublisherindexes-transact-sql.md)

        [IHpublishers](ihpublishers-transact-sql.md)

        [IHpublishertables](ihpublishertables-transact-sql.md)

        [IHsubscriptions](ihsubscriptions-transact-sql.md)
    :::column-end:::
:::row-end:::

### Replication Tables in the Publication Database  

:::row:::
    :::column:::
        [conflict_\<schema>_\<table>](conflict-schema-table-transact-sql.md)

        [MSdynamicsnapshotjobs](msdynamicsnapshotjobs-transact-sql.md)

        [MSdynamicsnapshotviews](msdynamicsnapshotviews-transact-sql.md)

        [MSmerge_altsyncpartners](msmerge-altsyncpartners-transact-sql.md)

        [MSmerge_conflicts_info](msmerge-conflicts-info-transact-sql.md)

        [MSmerge_contents](msmerge-contents-transact-sql.md)

        [MSmerge_current_partition_mappings](msmerge-current-partition-mappings.md)

        [MSmerge_dynamic_snapshots](msmerge-dynamic-snapshots-transact-sql.md)

        [MSmerge_errorlineage](msmerge-errorlineage-transact-sql.md)

        [MSmerge_generation_partition_mappings](msmerge-generation-partition-mappings-transact-sql.md)

        [MSmerge_genhistory](msmerge-genhistory-transact-sql.md)

        [MSmerge_identity_range](msmerge-identity-range-transact-sql.md)

        [MSmerge_metadataaction_request](msmerge-metadataaction-request-transact-sql.md)

        [MSmerge_partition_groups](msmerge-partition-groups-transact-sql.md)

        [MSmerge_past_partition_mappings](msmerge-past-partition-mappings-transact-sql.md)

        [MSmerge_replinfo](msmerge-replinfo-transact-sql.md)

        [MSmerge_settingshistory](msmerge-settingshistory-transact-sql.md)

        [MSmerge_tombstone](msmerge-tombstone-transact-sql.md)

        [MSpeer_conflictdetectionconfigrequest](mspeer-conflictdetectionconfigrequest-transact-sql.md)

        [MSpeer_conflictdetectionconfigresponse](mspeer-conflictdetectionconfigresponse-transact-sql.md)

        [MSpeer_lsns](mspeer-lsns-transact-sql.md)

        [MSpeer_originatorid_history](mspeer-originatorid-history-transact-sql.md)
    :::column-end:::
    :::column:::
        [MSpeer_request](mspeer-request-transact-sql.md)

        [MSpeer_response](mspeer-response-transact-sql.md)

        [MSpeer_topologyrequest](mspeer-topologyrequest-transact-sql.md)

        [MSpeer_topologyresponse](mspeer-topologyresponse-transact-sql.md)

        [MSpub_identity_range](mspub-identity-range-transact-sql.md)

        [MSrepl_identity_range](msrepl-identity-range-transact-sql.md)

        [sysarticlecolumns](sysarticlecolumns-transact-sql.md)

        [sysarticles](sysarticles-transact-sql.md)

        [sysarticleupdates](sysarticleupdates-transact-sql.md)

        [sysmergearticlecolumns](sysmergearticlecolumns-transact-sql.md)

        [sysmergearticles](sysmergearticles-transact-sql.md)

        [sysmergepartitioninfo](sysmergepartitioninfo-transact-sql.md)

        [sysmergepublications](sysmergepublications-transact-sql.md)

        [sysmergeschemaarticles](sysmergeschemaarticles-transact-sql.md)

        [sysmergeschemachange](sysmergeschemachange-transact-sql.md)

        [sysmergesubscriptions](sysmergesubscriptions-transact-sql.md)

        [sysmergesubsetfilters](sysmergesubsetfilters-transact-sql.md)

        [syspublications](syspublications-transact-sql.md)

        [sysschemaarticles](sysschemaarticles-transact-sql.md)

        [syssubscriptions](syssubscriptions-transact-sql.md)

        [systranschemas](../../relational-databases/system-views/systranschemas-transact-sql.md)
    :::column-end:::
:::row-end:::

### Replication Tables in the Subscription Database  

:::row:::
    :::column:::
        [MSdynamicsnapshotjobs](msdynamicsnapshotjobs-transact-sql.md)

        [MSdynamicsnapshotviews](msdynamicsnapshotviews-transact-sql.md)

        [MSmerge_altsyncpartners](msmerge-altsyncpartners-transact-sql.md)

        [MSmerge_conflicts_info](msmerge-conflicts-info-transact-sql.md)

        [MSmerge_contents](msmerge-contents-transact-sql.md)

        [MSmerge_current_partition_mappings](msmerge-current-partition-mappings.md)

        [MSmerge_dynamic_snapshots](msmerge-dynamic-snapshots-transact-sql.md)

        [MSmerge_errorlineage](msmerge-errorlineage-transact-sql.md)

        [MSmerge_generation_partition_mappings](msmerge-generation-partition-mappings-transact-sql.md)

        [MSmerge_genhistory](msmerge-genhistory-transact-sql.md)

        [MSmerge_identity_range](msmerge-identity-range-transact-sql.md)

        [MSmerge_metadataaction_request](msmerge-metadataaction-request-transact-sql.md)

        [MSmerge_partition_groups](msmerge-partition-groups-transact-sql.md)

        [MSmerge_past_partition_mappings](msmerge-past-partition-mappings-transact-sql.md)

        [MSmerge_replinfo](msmerge-replinfo-transact-sql.md)

        [MSmerge_settingshistory](msmerge-settingshistory-transact-sql.md)
    :::column-end:::
    :::column:::
        [MSmerge_tombstone](msmerge-tombstone-transact-sql.md)

        [MSpeer_lsns](mspeer-lsns-transact-sql.md)

        [MSredirected_publishers](msredirected-publishers.md)

        [MSrepl_identity_range](msrepl-identity-range-transact-sql.md)

        [MSrepl_queuedtraninfo](msrepl-queuedtraninfo-transact-sql.md)

        [MSreplication_objects](msreplication-objects-transact-sql.md)

        [MSreplication_subscriptions](msreplication-subscriptions-transact-sql.md)

        [MSsnapshotdeliveryprogress](mssnapshotdeliveryprogress-transact-sql.md)

        [MSsubscription_properties](mssubscription-properties-transact-sql.md)

        [sysmergearticles](sysmergearticles-transact-sql.md)

        [sysmergepartitioninfo](sysmergepartitioninfo-transact-sql.md)

        [sysmergepublications](sysmergepublications-transact-sql.md)

        [sysmergeschemaarticles](sysmergeschemaarticles-transact-sql.md)

        [sysmergeschemachange](sysmergeschemachange-transact-sql.md)

        [sysmergesubscriptions](sysmergesubscriptions-transact-sql.md)

        [sysmergesubsetfilters](sysmergesubsetfilters-transact-sql.md)

        [systranschemas](../../relational-databases/system-views/systranschemas-transact-sql.md)
    :::column-end:::
:::row-end:::

## See Also  
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
