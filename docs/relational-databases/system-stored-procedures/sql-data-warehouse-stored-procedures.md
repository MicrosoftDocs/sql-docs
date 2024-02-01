---
title: "Azure Synapse Analytics stored procedures"
description: "Azure Synapse Analytics stored procedures"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || =fabric"
---
# Azure Synapse Analytics Stored Procedures

[!INCLUDE [asa-fabricse-fabricdw](../../includes/applies-to-version/asa-fabricse-fabricdw.md)]

[!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [fabric](../../includes/fabric.md)] provide built-in procedures that you can use to perform operations related to database roles.

<a id="AggregateFunctions"></a>

- [sp_datatype_info_90 (Azure Synapse Analytics)](sp-datatype-info-90-sql-data-warehouse.md)

[!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] includes the following system procedures:

- [sp_pdw_add_network_credentials (Azure Synapse Analytics)](sp-pdw-add-network-credentials-sql-data-warehouse.md)
- [sp_pdw_database_encryption (Azure Synapse Analytics)](sp-pdw-database-encryption-sql-data-warehouse.md)
- [sp_pdw_database_encryption_regenerate_system_keys (Azure Synapse Analytics)](sp-pdw-database-encryption-regenerate-system-keys-sql-data-warehouse.md)
- [sp_pdw_log_user_data_masking (Azure Synapse Analytics)](sp-pdw-log-user-data-masking-sql-data-warehouse.md)
- [sp_pdw_remove_network_credentials (Azure Synapse Analytics)](sp-pdw-remove-network-credentials-sql-data-warehouse.md)
- [sp_special_columns_100 (Azure Synapse Analytics)](sp-special-columns-100-sql-data-warehouse.md)

> [!NOTE]  
> Some additional system stored procedures are used only within an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or through client APIs and aren't intended for general customer use. These procedures are listed at [System stored procedures (Transact-SQL)](./system-stored-procedures-transact-sql.md). These procedures are subject to change and compatibility isn't guaranteed. All procedures on the list aren't available in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

## Related content

- [System functions by category for Transact-SQL](../system-functions/system-functions-category-transact-sql.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
