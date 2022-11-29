---
description: "The sp_create_openrowset_statistics system stored procedure creates column statistics for a column in the OPENROWSET path of Azure Synapse SQL resources."
title: "sp_create_openrowset_statistics (Transact-SQL)"
ms.custom: ""
ms.date: "04/13/2022"
ms.service: synapse-analytics
ms.reviewer: ""
ms.topic: "reference"
f1_keywords: 
  - "sp_create_openrowset_statistics_TSQL"
  - "sp_create_openrowset_statistics"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_create_openrowset_statistics"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azure-sqldw-latest||=azuresqldb-mi-current"
---
# sp_create_openrowset_statistics (Transact-SQL)
[!INCLUDE [asdbmi-asa-svrless-poolonly](../../includes/applies-to-version/asdbmi-asa-svrless-poolonly.md)]

  Creates column statistics for a column in the OPENROWSET path of Azure Synapse serverless SQL pools. For more information, see [Statistics in Synapse SQL](/azure/synapse-analytics/sql/develop-tables-statistics). This procedure is also used by [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] for column statistics in external data sources via OPENROWSET.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
sys.sp_create_openrowset_statistics [ @stmt = ] N'statement_text'
```  

## Arguments  

#### [ @stmt = ] N'statement_text'
Specifies a Transact-SQL statement that will return column values to be used for statistics. You can use TABLESAMPLE within the `@stmt` to specify samples of data to be used. If TABLESAMPLE isn't specified, FULLSCAN will be used. For CSV data sources, only FULLSCAN is supported.

`<tablesample_clause> ::= TABLESAMPLE ( sample_number PERCENT )`
  
## Remarks  
 Use `sys.sp_create_openrowset_statistics` to create statistics on external data sources via OPENROWSET. Currently, you can create single-column statistics only. 

 Statistics metadata is not available for OPENROWSET columns.

 For statistics on external table columns, use `CREATE STATISTICS` instead. For more information, see [Create statistics for external table column](/azure/synapse-analytics/sql/develop-tables-statistics#examples-create-statistics-for-external-table-column).
  
## Permissions  
 Requires ADMINISTER BULK OPERATIONS or ADMINISTER DATABASE BULK OPERATIONS permissions.
  
## Example

For usage scenarios and examples, review [Create statistics for column in OPENROWSET path](/azure/synapse-analytics/sql/develop-tables-statistics#examples-create-statistics-for-column-in-openrowset-path).
  
## See also  

- [sys.sp_drop_openrowset_statistics (Transact-SQL)](sp-drop-openrowset-statistics.md)
 
## Next steps

- [Statistics in Synapse SQL](/azure/synapse-analytics/sql/develop-tables-statistics)