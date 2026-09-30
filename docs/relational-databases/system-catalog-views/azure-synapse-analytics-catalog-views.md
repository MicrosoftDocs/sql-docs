---
title: Catalog Views
titleSuffix: Azure Synapse Analytics
description: Azure Synapse Analytics catalog views.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/14/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
dev_langs:
  - TSQL
monikerRange: "=azure-sqldw-latest"
---
# Azure Synapse Analytics catalog views

[!INCLUDE [asa-md](../../includes/applies-to-version/asa.md)]

[!INCLUDE [synapse-fabric-migration](../../includes/synapse-fabric-migration.md)]

This article lists the [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] catalog views.

| Catalog view | Description |
| --- | --- |
| [sys.pdw_column_distribution_properties](sys-pdw-column-distribution-properties-transact-sql.md) | Returns distribution information for columns. |
| [sys.pdw_distributions](sys-pdw-distributions-transact-sql.md) | Holds information about the distributions on the appliance, with one row per appliance distribution. |
| [sys.pdw_index_mappings](sys-pdw-index-mappings-transact-sql.md) | Maps the logical indexes to the physical name used on Compute nodes. |
| [sys.pdw_loader_backup_run_details](sys-pdw-loader-backup-run-details-transact-sql.md) | Contains detailed information about ongoing and completed backup, restore, and load operations, beyond the information in `sys.pdw_loader_backup_runs`. |
| [sys.pdw_loader_backup_runs](sys-pdw-loader-backup-runs-transact-sql.md) | Contains information about ongoing and completed backup, restore, and load operations. |
| [sys.pdw_materialized_view_column_distribution_properties](sys-pdw-materialized-view-column-distribution-properties-transact-sql.md) | Displays distribution information for columns in a materialized view. |
| [sys.pdw_materialized_view_distribution_properties](sys-pdw-materialized-view-distribution-properties-transact-sql.md) | Displays distribution information for materialized views. |
| [sys.pdw_materialized_view_mappings](sys-pdw-materialized-view-mappings-transact-sql.md) (Preview) | Ties the materialized view to internal object names by `object_id`. |
| [sys.pdw_nodes_column_store_dictionaries](sys-pdw-nodes-column-store-dictionaries-transact-sql.md) | Contains a row for each dictionary used in columnstore indexes. |
| [sys.pdw_nodes_column_store_row_groups](sys-pdw-nodes-column-store-row-groups-transact-sql.md) | Provides clustered columnstore index information on a per-segment basis to help make system management decisions. |
| [sys.pdw_nodes_column_store_segments](sys-pdw-nodes-column-store-segments-transact-sql.md) | Contains a row for each column in a columnstore index. |
| [sys.pdw_nodes_columns](sys-pdw-nodes-columns-transact-sql.md) | Shows columns for user-defined tables and user-defined views. |
| [sys.pdw_nodes_indexes](sys-pdw-nodes-indexes-transact-sql.md) | Returns indexes for Azure Synapse Analytics. |
| [sys.pdw_nodes_partitions](sys-pdw-nodes-partitions-transact-sql.md) | Contains a row for each partition of all the tables and most types of indexes. |
| [sys.pdw_nodes_pdw_physical_databases](sys-pdw-nodes-pdw-physical-databases-transact-sql.md) | Contains a row for each physical database on a compute node. |
| [sys.pdw_nodes_tables](sys-pdw-nodes-tables-transact-sql.md) | Contains a row for each table object that a principal either owns or on which the principal has been granted some permission. |
| [sys.pdw_permanent_table_mappings](sys-pdw-permanent-table-mappings-transact-sql.md) | Ties permanent user tables to internal object names by `object_id`. |
| [sys.pdw_replicated_table_cache_state](sys-pdw-replicated-table-cache-state-transact-sql.md) | Returns the state of the cache associated with a replicated table by `object_id`. |
| [sys.pdw_table_distribution_properties](sys-pdw-table-distribution-properties-transact-sql.md) | Holds distribution information for tables. |
| [sys.pdw_table_mappings](sys-pdw-table-mappings-transact-sql.md) | Ties user tables to internal object names by `object_id`. |
| [sys.workload_management_workload_classifier_details](sys-workload-management-workload-classifier-details-transact-sql.md) | Returns details for each classifier. |
| [sys.workload_management_workload_classifiers](sys-workload-management-workload-classifiers-transact-sql.md) | Returns details for workload classifiers. |
| [sys.workload_management_workload_groups](sys-workload-management-workload-groups-transact-sql.md) | Returns details for workload groups. |

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
