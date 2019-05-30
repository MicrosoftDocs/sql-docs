---
title: "Objects Created on the Oracle Publisher | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], objects created"
ms.assetid: c58a124b-4da7-46e2-9292-af8ce9e6664b
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Objects Created on the Oracle Publisher
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication installs database objects on the Oracle Publisher to enable change tracking and forwarding ( [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not install any binary files on the Oracle Publisher). The following table lists the objects that are created at the Oracle Publisher when it is identified as a Publisher at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor. The object descriptions are provided for informational purposes only. These objects should not be modified.  
  
|Object Name|Object Type|Description|  
|-----------------|-----------------|-----------------|  
|HREPL_ArticleNlog_V|Table|Change tracking table used to store information as changes are made to the published table. A change tracking table is created for each published table.|  
|HREPL_Changes|Table|Table used internally by the Xactset Job to determine the number of changes waiting to be assigned to a transaction set. For more information about this job, see [Performance Tuning for Oracle Publishers](../../../relational-databases/replication/non-sql/performance-tuning-for-oracle-publishers.md).|  
|HREPL_Distributor|Table|Distributor status table used to maintain information about the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor associated with the Oracle Publisher.|  
|HREPL_Event|Table|Event table used for synchronizing snapshots and row count requests.|  
|HREPL_Mutex|Table|Table used to ensure that the Oracle package procedure PopulatePollTable is not executed concurrently by both the Log Reader Agent and the database job.|  
|HREPL_Poll|Table|Table used to identify log table entries associated with sets of changes to published tables.|  
|HREPL_PublishedTables|Table|Table containing an entry for each article in a transactional publication.|  
|HREPL_Publisher|Table|Publisher status table used for maintaining Publisher specific information.|  
|HREPL_SchemaFilter|Table|Table containing schemas that are not displayed when publishing through the New Publication Wizard.|  
|HREPL_XactsetCreateTimes|Table|Table identifying the create time associated with each transaction set.|  
|HREPL_XactsetJob|Table|Table with current parameter settings for the Xactset Job.|  
|HREPL_Pollid|Sequence|Sequence used to generate poll IDs.|  
|HREPL_Seq|Sequence|Sequence used to order change commands.|  
|HREPL_Stmt|Sequence|Sequence used to generate statement IDs.|  
|HREPL|Package and Package Body|Package of Publisher support code that is created at the Publisher.|  
|MSSQLSERVERDISTRIBUTOR|Public Synonym|Public synonym for the HREPL_Distributor table. If you configure a Distributor to use with an Oracle Publisher, and this synonym already exists in the database, it is dropped and recreated.<br /><br /> Dropping the public synonym and the configured Oracle replication user with the CASCADE option removes all replication objects from the Oracle Publisher.|  
|HREPL_Len_I_J_K|Function|Function defined outside the Oracle publishing package code, used to query for the length of a LONG column (used when generating parameterized commands for tables with published LONG columns). A function is created for each published table with a LONG column.|  
|HREPL_DropPublisher|Procedure|Procedure defined outside the Oracle publishing package code, used to drop the Oracle Publisher.|  
|HREPL_ExecuteCommand|Procedure|Procedure defined outside the Oracle publishing package code, used to execute a command at the Publisher.|  
|HREPL_ArticleN_Trigger_Row|Trigger|Trigger generated for each published table, used to track row changes.|  
|HREPL_ArticleN_Trigger_Stmt|Trigger|Trigger generated for each published table, used to track statement level changes.|  
|HREPL_Article_I_J|View|View created for each published table, used to query the published table.|  
|HREPL_Log_I_J_K|View|View created for each published table, used to query the change tracking table.|  
  
## See Also  
 [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md)   
 [Glossary of Terms for Oracle Publishing](../../../relational-databases/replication/non-sql/glossary-of-terms-for-oracle-publishing.md)   
 [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md)  
  
  
