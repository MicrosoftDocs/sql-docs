---
title: "Upgrade Replication Scripts (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "scripts [SQL Server replication], upgrading"
  - "upgrading SQL Server, replicated databases"
  - "upgrading replication applications"
  - "replication [SQL Server], scripting"
  - "replication [SQL Server], upgrading"
  - "upgrading replicated databases"
ms.assetid: 0b8720bd-f339-4842-bc8f-b35a46f6d3ee
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Upgrade Replication Scripts (Replication Transact-SQL Programming)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[tsql](../../../includes/tsql-md.md)] script files can be used to programmatically configure a replication topology. For more information, see [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md).  
  
> [!IMPORTANT]  
>  Although you are not required to upgrade scripts that are executed by members of the **sysadmin** role, we recommend that you modify existing scripts as described in this topic. Specify an account that has minimum permissions for each replication agent as described in the "Permissions Required By Agents" section of the topic [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).  
  
 These security improvements, which enable more control over permissions by allowing you to explicitly specify the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows accounts under which replication agent jobs are executed, affect the following stored procedures in existing scripts:  
  
-   **sp_addpublication_snapshot**:  
  
     You should now supply the Windows credentials as **@job_login** and **@job_password** when executing [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md) to create the job under which the Snapshot Agent runs at the Distributor.  
  
-   **sp_addpushsubscription_agent**:  
  
     You should now execute [sp_addpushsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md) to explicitly add a job and supply the Windows credentials (**@job_login** and **@job_password**) under which the Distribution Agent job runs at the Distributor. In versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] before [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], this was done automatically when a push subscription was created.  
  
-   **sp_addmergepushsubscription_agent**:  
  
     You should now execute [sp_addmergepushsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepushsubscription-agent-transact-sql.md) to explicitly add a job and supply the Windows credentials (**@job_login** and **@job_password**) under which the Merge Agent job runs at the Distributor. In versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] before [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], this was done automatically when a push subscription was created.  
  
-   **sp_addpullsubscription_agent**:  
  
     You should now supply the Windows credentials as **@job_login** and **@job_password** when executing [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md) to create the job under which the Distribution Agent runs at the Subscriber.  
  
-   **sp_addmergepullsubscription_agent**:  
  
     You should now supply the Windows credentials as **@job_login** and **@job_password** when executing [sp_addmergepullsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md) to create the job under which the Merge Agent runs at the Subscriber.  
  
-   **sp_addlogreader_agent**:  
  
     You should now execute [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md) to manually add the job and supply the Windows credentials under which the Log Reader Agent runs at the Distributor. In versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] before [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], this was done automatically when a transactional publication was created.  
  
-   **sp_addqreader_agent**:  
  
     You should now execute [sp_addqreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addqreader-agent-transact-sql.md) to manually add the job and supply the Windows credentials under which the Queue Reader Agent runs at the Distributor. In versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] before [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], this was done automatically when a transactional publication that supported queued updating was created.  
  
 In the security model introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], replication agents always make connections to the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with Windows Authentication using the credentials supplied in **@job_name** and **@job_password**. For information about the requirements of Windows accounts used when running replication agent jobs, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you store credentials in a script file, ensure that the file itself is secured.  
  
### To upgrade scripts that configure a snapshot or transactional publication  
  
1.  In the existing script, before [sp_addpublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md), execute [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md) at the Publisher on the publication database. Specify the Windows credentials under which the Log Reader Agent runs for **@job_name** and **@job_password**. If the agent will use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher, you must also specify a value of **0** for **@publisher_security_mode** and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login information for **@publisher_login** and **@publisher_password**. This creates a Log Reader Agent job for the publication database.  
  
    > [!NOTE]  
    >  This step is only for transactional publications and is not required for snapshot publications.  
  
2.  (Optional) Before [sp_addpublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md), execute [sp_addqreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addqreader-agent-transact-sql.md) at the Distributor on the distribution database. Specify the Windows credentials under which the Queue Reader Agent runs for **@job_name** and **@job_password**. This creates a Queue Reader Agent job for the Distributor.  
  
    > [!NOTE]  
    >  This step is only required for transactional publications that support queued updating subscribers.  
  
3.  (Optional) Update the execution of [sp_addpublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md) to set any non-default values for parameters that implement new replication functionalities.  
  
4.  After [sp_addpublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md), execute [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md) at the Publisher on the publication database. Specify **@publication** and the Windows credentials under which the Snapshot Agent runs for **@job_name** and **@job_password**. If the agent will use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher, you must also specify a value of **0** for **@publisher_security_mode** and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login information for **@publisher_login** and **@publisher_password**. This creates a Snapshot Agent job for the publication.  
  
5.  (Optional) Update the execution of [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) to set any non-default values for parameters that implement new replication functionalities.  
  
### To upgrade scripts that add subscriptions to a snapshot or transactional publication  
  
1.  After executing the stored procedure that creates the subscription, ensure that you execute the stored procedure that creates a Distribution Agent job to synchronize the subscription. The stored procedure that you use will depend on the type of subscription.  
  
    -   For a pull subscription, update the execution of [sp_addpullsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md) to supply the Windows credentials under which the Distribution Agent runs at the Subscriber for **@job_name** and **@job_password**. This is done after the execution of [sp_addpullsubscription](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md). For more information, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).  
  
    -   For a push subscription, execute [sp_addpushsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md) at the Publisher. Specify **@subscriber**, **@subscriber_db**, **@publication**, Windows credentials under which the Distribution Agent runs at the Distributor for **@job_name** and **@job_password**, and a schedule for this agent job. For more information, see [Specify Synchronization Schedules](../../../relational-databases/replication/specify-synchronization-schedules.md). This is done after the execution of [sp_addsubscription](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). For more information, see [Create a Push Subscription](../../../relational-databases/replication/create-a-push-subscription.md).  
  
### To upgrade scripts that configure a merge publication  
  
1.  (Optional) In the existing script, update the execution of [sp_addmergepublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md) to set any non-default values for parameters that implement new replication functionalities.  
  
2.  After [sp_addmergepublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md), execute [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md) at the Publisher on the publication database. Specify **@publication** and the Windows credentials under which the Snapshot Agent runs for **@job_name** and **@job_password**. If the agent will use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher, you must also specify a value of **0** for **@publisher_security_mode** and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login information for **@publisher_login** and **@publisher_password**. This creates a Snapshot Agent job for the publication.  
  
3.  (Optional) Update the execution of [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) to set any non-default values for parameters that implement new replication functionalities.  
  
### To upgrade scripts that add subscriptions to a merge publication  
  
1.  After executing the stored procedure that creates the subscription, ensure that you execute the stored procedure that creates a Merge Agent job to synchronize the subscription. The stored procedure that you use will depend on the type of subscription.  
  
    -   For a pull subscription, update the execution of [sp_addmergepullsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md) to supply the Windows credentials under which the Merge Agent runs at the Subscriber for **@job_name** and **@job_password**. This is done after the execution of [sp_addmergepullsubscription](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md). For more information, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).  
  
    -   For a push subscription, execute [sp_addmergepushsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepushsubscription-agent-transact-sql.md) at the Publisher. Specify **@subscriber**, **@subscriber_db**, **@publication**, the Windows credentials under which the Merge Agent at the Distributor runs for **@job_name** and **@job_password**, and a schedule for this agent job. For more information, see [Specify Synchronization Schedules](../../../relational-databases/replication/specify-synchronization-schedules.md). This is done after the execution of [sp_addmergesubscription](../../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md). For more information, see [Create a Push Subscription](../../../relational-databases/replication/create-a-push-subscription.md).  
  
## Example  
 The following is an example of a [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] script that creates a transactional publication for the Product table. This publication supports immediate updating with queued updating as failover. Default parameters have been removed for readability.  
  
 [!code-sql[HowTo#sp_createtranpub_NWpreupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_1.sql)]  
  
## Example  
 The following is an example of upgrading the previous script, which creates a transactional publication, to run successfully for [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions. This publication supports immediate updating with queued updating as failover. Defaults for new parameters have been explicitly declared.  
  
> [!NOTE]  
>  Windows credentials are supplied at runtime using **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_createtranpub_NWpostupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_2.sql)]  
  
## Example  
 The following is an example of a [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] script that creates a merge publication for the Customers table. Default parameters have been removed for readability.  
  
 [!code-sql[HowTo#sp_createmergepub_NWpreupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_3.sql)]  
  
## Example  
 The following is an example of the previous script, which creates a merge publication, upgraded to run successfully for [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions. Defaults for new parameters have been explicitly declared.  
  
> [!NOTE]  
>  Windows credentials are supplied at runtime using **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_createmergepub_NWpostupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_4.sql)]  
  
## Example  
 The following is an example of a [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] script that creates a push subscription to a transactional publication. Default parameters have been removed for readability.  
  
 [!code-sql[HowTo#sp_createtranpushsub_NWpreupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_5.sql)]  
  
## Example  
 The following is an example of the previous script, which creates a push subscription to a transactional publication, upgraded to run successfully for [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions. Defaults for new parameters have been explicitly declared.  
  
> [!NOTE]  
>  Windows credentials are supplied at runtime using **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_createtranpushsub_NWpostupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_6.sql)]  
  
## Example  
 The following is an example of a [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] script that creates a push subscription to a merge publication. Default parameters have been removed for readability.  
  
 [!code-sql[HowTo#sp_createmergepushsub_NWpreupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_7.sql)]  
  
## Example  
 The following is an example of the previous script, which creates a push subscription to a merge publication, upgraded to run successfully for [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions. Defaults for new parameters have been explicitly declared.  
  
> [!NOTE]  
>  Windows credentials are supplied at runtime using **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_createmergepushsub_NWpostupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_8.sql)]  
  
## Example  
 The following is an example of a [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] script that creates a pull subscription to a transactional publication. Default parameters have been removed for readability.  
  
 [!code-sql[HowTo#sp_createmergepushsub_NWpreupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_7.sql)]  
  
## Example  
 The following is an example of the previous script, which creates a pull subscription to a transactional publication, upgraded to run successfully for [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions. Defaults for new parameters have been explicitly declared.  
  
> [!NOTE]  
>  Windows credentials are supplied at runtime using **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_createtranpullsub_NWpostupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_9.sql)]  
  
## Example  
 The following is an example of a [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)] script that creates a pull subscription to a merge publication. Default parameters have been removed for readability.  
  
 [!code-sql[HowTo#sp_createmergepullsub_NWpreupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_10.sql)]  
  
## Example  
 The following is an example of the previous script, which creates a pull subscription to a merge publication, upgraded to run successfully for [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions. Defaults for new parameters have been explicitly declared.  
  
> [!NOTE]  
>  Windows credentials are supplied at runtime using **sqlcmd** scripting variables.  
  
 [!code-sql[HowTo#sp_createmergepullsub_NWpostupgrade](../../../relational-databases/replication/codesnippet/tsql/upgrade-replication-scri_11.sql)]  
  
## See Also  
 [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md)   
 [Create a Push Subscription](../../../relational-databases/replication/create-a-push-subscription.md)   
 [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md)   
 [View and Modify Replication Security Settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [MSSQL_ENG021797](../../../relational-databases/replication/mssql-eng021797.md)   
 [MSSQL_ENG021798](../../../relational-databases/replication/mssql-eng021798.md)   
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)   
 [Upgrade Replicated Databases](../../../database-engine/install-windows/upgrade-replicated-databases.md)  
  
  
