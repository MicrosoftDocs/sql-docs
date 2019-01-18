---
title: "sp_upgrade_log_shipping (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_upgrade_log_shipping"
  - "sp_upgrade_log_shipping_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_upgrade_log_shipping"
ms.assetid: ee01092f-9caf-4e88-888b-ec7b84223705
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_upgrade_log_shipping (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The sp_upgrade_log_shipping stored procedure is invoked automatically for upgrading metadata that is specific to log shipping.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_upgrade_log_shipping  
```  
  
## Arguments  
 None.  
  
## Return Code Values  
 0 (success) or 1 (otherwise)  
  
## Result Sets  
 None.  
  
## Remarks  
 This stored procedure is invoked automatically during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] upgrade for upgrading metadata for log shipping. You do not need to execute this procedure explicitly, unless a problem occurs with the metadata during upgrade.  
  
 sp_upgrade_log_shipping must be run from the master database on the primary, secondary, or monitor server.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
