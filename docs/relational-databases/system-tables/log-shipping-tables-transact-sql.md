---
title: "Log Shipping Tables (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "log shipping [SQL Server], system tables"
  - "system tables [SQL Server], log shipping"
ms.assetid: f8910aae-2013-4645-880c-134577cbcbe0
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Log Shipping Tables (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The following topics describe the system tables that store the information used by log-shipping operations.  
  
## In This Section  
 [log_shipping_monitor_alert](../../relational-databases/system-tables/log-shipping-monitor-alert-transact-sql.md)  
 Stores alert job IDs for log shipping.  
  
 [log_shipping_monitor_error_detail](../../relational-databases/system-tables/log-shipping-monitor-error-detail-transact-sql.md)  
 Stores error detail for log shipping jobs.  
  
 [log_shipping_monitor_history_detail](../../relational-databases/system-tables/log-shipping-monitor-history-detail-transact-sql.md)  
 Stores history details for log shipping jobs.  
  
 [log_shipping_monitor_primary](../../relational-databases/system-tables/log-shipping-monitor-primary-transact-sql.md)  
 Stores one monitor record per primary database in each log shipping configuration.  
  
 [log_shipping_monitor_secondary](../../relational-databases/system-tables/log-shipping-monitor-secondary-transact-sql.md)  
 Stores one monitor record per secondary database in a log shipping configuration.  
  
 [log_shipping_primary_databases](../../relational-databases/system-tables/log-shipping-primary-databases-transact-sql.md)  
 Stores one record for the primary database in a log shipping configuration.  
  
 [log_shipping_primary_secondaries](../../relational-databases/system-tables/log-shipping-primary-secondaries-transact-sql.md)  
 Maps each primary database to its secondary databases.  
  
 [log_shipping_secondary](../../relational-databases/system-tables/log-shipping-secondary-transact-sql.md)  
 Stores one record per secondary ID.  
  
 [log_shipping_secondary_databases](../../relational-databases/system-tables/log-shipping-secondary-databases-transact-sql.md)  
 Stores one record per secondary database in a log shipping configuration.  
  
  
