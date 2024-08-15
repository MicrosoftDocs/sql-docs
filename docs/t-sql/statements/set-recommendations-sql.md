---
title: "SET RECOMMENDATIONS (Transact-SQL)"
description: "SET RECOMMENDATIONS enables or disables the Azure Synapse distribution advisor for the current session."
author: mariyaali
ms.author: mariyaali
ms.reviewer: xiaoyul, wiassaf
ms.date: 07/16/2023
ms.service: azure-synapse-analytics
ms.topic: reference
f1_keywords:
  - "SET RECOMMENDATIONS"
  - "RECOMMENDATIONS"
helpviewer_keywords:
  - "SET RECOMMENDATIONS"
  - "RECOMMENDATIONS"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---
# SET RECOMMENDATIONS (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa-dedicated-sqlpool-only.md)]

Enables or disables the Azure Synapse distribution advisor for the current session. For instructions and samples on the use of the distribution advisor, see [Distribution Advisor in Azure Synapse SQL](/azure/synapse-analytics/sql/distribution-advisor).

> [!NOTE]
> Distribution Advisor is currently in preview for Azure Synapse Analytics. Preview features are meant for testing only and should not be used on production instances or production data. As a preview feature, Distribution Advisor is subject to undergo changes in behavior or functionality. Please also keep a copy of your test data if the data is important.
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

```syntaxsql
SET RECOMMENDATIONS { ON | OFF };
```  

## Arguments

#### ON
Enables Distribution Advisor for the current client session. Subsequently run queries will be taken into consideration for distribution strategy recommendations.

#### OFF
Turns Distribution Advisor OFF for the current client session. Returns advice as a string.

## Remarks

Applies to [!INCLUDE [ssSDW](../../includes/ssazuresynapse_sqlpool_only.md)] only.

Run this command when connected to a user database.

## Permissions

Requires membership in the public role.

## Examples

Following example will return distribution recommendation on selected TPC-DS queries. TPC-DS is an industry standard benchmark for analytical decision support workloads.

First, begin the distribution advisor recommendation collection and run sample queries.

```sql
-- Step 1: Turn the distribution advisor ON for the current client session
SET RECOMMENDATIONS ON;
GO

-- <insert your queries here, up to 100>
SELECT ss_store_sk, COUNT(*) FROM store_sales, store WHERE ss_store_sk = s_store_sk GROUP BY ss_store_sk;

SELECT cs_item_sk, COUNT(*) FROM catalog_sales, item WHERE cs_item_sk = i_item_sk  AND i_manufact_id > 100 GROUP BY cs_item_sk;

SELECT * FROM dbo.reason;

-- Turn the distribution advisor OFF for the current client session.
SET RECOMMENDATIONS OFF;
GO
```

Collect recommendations from the dynamic management view `sys.dm_pdw_distrib_advisor_results` for the current session. For example:

```sql

-- Step 2: view advice generated for the above workload
DECLARE @sessionid nvarchar(100), @recommendation nvarchar(max);
SELECT @sessionid = SESSION_ID();
SELECT @recommendation = recommendation FROM sys.dm_pdw_distrib_advisor_results WHERE session_id = @sessionid;
SELECT @recommendation;
GO
```

## Next steps

- [Distribution Advisor in Azure Synapse SQL](/azure/synapse-analytics/sql/distribution-advisor)