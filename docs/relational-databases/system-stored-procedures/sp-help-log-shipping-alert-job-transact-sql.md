---
title: "sp_help_log_shipping_alert_job (Transact-SQL)"
description: "sp_help_log_shipping_alert_job (Transact-SQL)"
author: MashaMSFT
ms.author: mathoma
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_alert_job_TSQL"
  - "sp_help_log_shipping_alert_job"
helpviewer_keywords:
  - "sp_help_log_shipping_alert_job"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_alert_job (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This stored procedure returns the job ID of the alert job from the log shipping monitor.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_log_shipping_alert_job  
  
```  
  
## Arguments  
 None  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 This stored procedure returns the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID of the log shipping alert job. If no log shipping alert job exists, it returns an empty result set.  
  
## Remarks  
 **sp_help_log_shipping_alert_job** must be run from the **master** database on the monitor server.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
