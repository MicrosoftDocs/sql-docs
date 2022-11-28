---
title: "Configure transaction set job (Oracle Publisher)"
description: Learn how to configure the transaction set job for an Oracle Publisher publishing to a SQL Server Subscriber.
ms.custom: seo-lt-2019
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_publisherproperty"
  - "Oracle publishing [SQL Server replication], configuring"
ms.assetid: beea1a5c-0053-4971-a68f-0da53063fcbb
author: "MashaMSFT"
ms.author: "mathoma"
---
# Configure the Transaction Set Job for an Oracle Publisher
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  The **Xactset** job is an Oracle database job created by replication that runs at an Oracle Publisher to create transaction sets when the Log Reader Agent is not connected to the Publisher. You can enable and configure this job from the Distributor programmatically using replication stored procedures. For more information, see [Performance Tuning for Oracle Publishers](../../../relational-databases/replication/non-sql/performance-tuning-for-oracle-publishers.md).  
  
### To enable the transaction set job  
  
1.  At the Oracle Publisher, set the **job_queue_processes** initialization parameter to a sufficient value to allow the Xactset job run. For more information about this parameter, see the database documentation for the Oracle Publisher.  
  
2.  At the Distributor, execute [sp_publisherproperty &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-publisherproperty-transact-sql.md). Specify the name of the Oracle Publisher for **\@publisher**, a value of **xactsetbatching** for **\@propertyname**, and a value of **enabled** for **\@propertyvalue**.  
  
3.  At the Distributor, execute [sp_publisherproperty &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-publisherproperty-transact-sql.md). Specify the name of the Oracle Publisher for **\@publisher**, a value of **xactsetjobinterval** for **\@propertyname**, and the job interval, in minutes, for **\@propertyvalue**.  
  
4.  At the Distributor, execute [sp_publisherproperty &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-publisherproperty-transact-sql.md). Specify the name of the Oracle Publisher for **\@publisher**, a value of **xactsetjob** for **\@propertyname**, and a value of **enabled** for **\@propertyvalue**.  
  
### To configure the transaction set job  
  
1.  (Optional) At the Distributor, execute [sp_publisherproperty &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-publisherproperty-transact-sql.md). Specify the name of the Oracle Publisher for **\@publisher**. This returns properties of the **Xactset** job at the Publisher.  
  
2.  At the Distributor, execute [sp_publisherproperty &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-publisherproperty-transact-sql.md). Specify the name of the Oracle Publisher for **\@publisher**, the name of the Xactset job property being set for **\@propertyname**, and new setting for **\@propertyvalue**.  
  
3.  (Optional) Repeat step 2 for each Xactset job property being set. When changing the **xactsetjobinterval** property, you must restart the job on the Oracle Publisher for the new interval to take effect.  
  
### To view properties of the transaction set job  
  
1.  At the Distributor, execute [sp_helpxactsetjob](../../../relational-databases/system-stored-procedures/sp-helpxactsetjob-transact-sql.md). Specify the name of the Oracle Publisher for **\@publisher**.  
  
### To disable the transaction set job  
  
1.  At the Distributor, execute [sp_publisherproperty &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-publisherproperty-transact-sql.md). Specify the name of the Oracle Publisher for **\@publisher**, a value of **xactsetjob** for **\@propertyname**, and a value of **disabled** for **\@propertyvalue**.  
  
## Example  
 The following example enables the `Xactset` job and sets an interval of three minutes between runs.  
  
 [!code-sql[HowTo#sp_enable_xactsetjob](../../../relational-databases/replication/codesnippet/tsql/configure-the-transactio_1.sql)]  
  
## See Also  
 [Performance Tuning for Oracle Publishers](../../../relational-databases/replication/non-sql/performance-tuning-for-oracle-publishers.md)   
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)  
  
  
