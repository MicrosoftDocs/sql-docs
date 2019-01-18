---
title: "SQL-DMO Mapping to SMO | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 590f5396-98d5-485e-9b41-728c6ed7cb9d
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL-DMO Mapping to SMO
  SQL Distributed Management Objects (SQL-DMO) is no longer included in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], SQL-DMO applications should be converted to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO). The SMO object model is similar to SQL-DMO, so most SQL-DMO objects map to an object with the same name in SMO. However, some SQL-DMO objects were changed or dropped in the transition to SMO. This table lists the recommended action to take for SQL-DMO objects that were not converted directly to SMO.  
  
|SQL-DMO object|Action in SMO|  
|---------------------|-------------------|  
|View2 Object|Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> Namespace.|  
|AlertSystem Object|Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> Namespace.|  
|Application Object|Removed.|  
|Backup Object and Backup2 Object|<xref:Microsoft.SqlServer.Management.Smo.Backup> and <xref:Microsoft.SqlServer.Management.Smo.BackupRestoreBase> objects.|  
|BackupDevice Object|<xref:Microsoft.SqlServer.Management.Smo.BackupDevice> objects|  
|BulkCopy Object and BulkCopy2 Object|Removed and replaced by <xref:Microsoft.SqlServer.Management.Smo.Transfer> object.|  
|Category Object|Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> Namespace. Replace by <xref:Microsoft.SqlServer.Management.Smo.Agent.AlertCategory>, <xref:Microsoft.SqlServer.Management.Smo.Agent.OperatorCategory>, <xref:Microsoft.SqlServer.Management.Smo.Agent.JobCategory> objects.|  
|Check Object|<xref:Microsoft.SqlServer.Management.Smo.Check> object|  
|Column Object and Column2 Object|<xref:Microsoft.SqlServer.Management.Smo.Column> object.|  
|Configuration Object|<xref:Microsoft.SqlServer.Management.Smo.Configuration> and <xref:Microsoft.SqlServer.Management.Smo.ConfigurationBase> objects.|  
|ConfigValue Object|<xref:Microsoft.SqlServer.Management.Smo.ConfigProperty> object.|  
|Database Object and Database2 Object|<xref:Microsoft.SqlServer.Management.Smo.Database> object.|  
|DatabaseRole Object and DatabaseRole2 Object|<xref:Microsoft.SqlServer.Management.Smo.DatabaseRole> object.|  
|DBFile Object|<xref:Microsoft.SqlServer.Management.Smo.DataFile> object.|  
|DBOption Object and DBOption2 Object|Moved into the <xref:Microsoft.SqlServer.Management.Smo.DatabaseOptions> object.|  
|Default Object and Default2 Object|<xref:Microsoft.SqlServer.Management.Smo.Default> object.|  
|DistributionArticle Object and DistributionArticle2 Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|DistributionDatabase Object and DistributionDatabase2 Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|DistributionPublication Object and DistributionPublication2 Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|DistributionSubscription Object and DistributionSubscription2 Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|Distributor Object and Distributor2 Object|Moved to the <xref:Microsoft.SqlServer.Replication> namespace.|  
|DRIDefault Object|Moved to <xref:Microsoft.SqlServer.Management.Smo.ScriptingOptions> object.|  
|FileGroup Object and FileGroup2 Object|<xref:Microsoft.SqlServer.Management.Smo.FileGroup> object.|  
|FullTextCatalog Object and FullTextCatalog2 Object|<xref:Microsoft.SqlServer.Management.Smo.FullTextCatalog> and <xref:Microsoft.SqlServer.Management.Smo.FullTextIndex> objects.|  
|Index Object and Index2 Object|<xref:Microsoft.SqlServer.Management.Smo.Index> object|  
|IntegratedSecurity Object|Functionality moved to <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object in <xref:Microsoft.SqlServer.Management.Common> namespace.|  
|Job Object|<xref:Microsoft.SqlServer.Management.Smo.Agent.Job> object. Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|JobFilter Object|<xref:Microsoft.SqlServer.Management.Smo.Agent.JobFilter> object. Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|JobHistoryFilter Object|<xref:Microsoft.SqlServer.Management.Smo.Agent.JobHistoryFilter> object. Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|JobSchedule Object|<xref:Microsoft.SqlServer.Management.Smo.Agent.JobSchedule> object. Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|JobServer Object and JobServer2 Object|<xref:Microsoft.SqlServer.Management.Smo.Agent.JobServer> object. Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|JobStep Object|<xref:Microsoft.SqlServer.Management.Smo.Agent.JobStep> object. Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|Key Object|<xref:Microsoft.SqlServer.Management.Smo.ForeignKey> and <xref:Microsoft.SqlServer.Management.Smo.Index> objects.|  
|LinkedServer Object and LinkedServer2 Object|<xref:Microsoft.SqlServer.Management.Smo.LinkedServer> object.|  
|LinkedServerLogin Object|<xref:Microsoft.SqlServer.Management.Smo.LinkedServerLogin> object.|  
|LogFile Object|<xref:Microsoft.SqlServer.Management.Smo.LogFile> object.|  
|Login Object and Login2 Object|<xref:Microsoft.SqlServer.Management.Smo.Login> object.|  
|MergeArticle Object and MergeArticle2 Object|<xref:Microsoft.SqlServer.Replication.MergeArticle> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|MergeDynamicSnapshotJob Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|MergePublication Object and MergePublication2 Object|<xref:Microsoft.SqlServer.Replication.MergePublication> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|MergePullSubscription Object and MergePullSubscription2 Object|<xref:Microsoft.SqlServer.Replication.MergePullSubscription> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|MergeSubscription Object|<xref:Microsoft.SqlServer.Replication.MergeSubscription> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|MergeSubsetFilter Object|Moved to `N:Microsoft.SqlServer.Replication` namespace.|  
|NameList Object|Removed. Alternative functionality in <xref:Microsoft.SqlServer.Management.Smo.Scripter> object.|  
|Operator Object|Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|Permission Object and Permission2 Object|<xref:Microsoft.SqlServer.Management.Smo.ServerPermission>, <xref:Microsoft.SqlServer.Management.Smo.DatabasePermission>, <xref:Microsoft.SqlServer.Management.Smo.ApplicationRole>, and <xref:Microsoft.SqlServer.Management.Smo.ObjectPermission> objects.|  
|Property Object|`Property` object.|  
|Publisher Object and Publisher2 Object|<xref:Microsoft.SqlServer.Replication.ReplicationServer> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|QueryResults Object and QueryResults2 Object|Replaced by <xref:System.Data.DataTable> or <xref:System.Data.DataSet> system object.|  
|RegisteredServer Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|RegisteredSubscriber Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|Registry Object and Registry2 Object|Removed.|  
|RemoteLogin Object|<xref:Microsoft.SqlServer.Management.Common.ServerConnection> object. Moved to Common namespace.|  
|RemoteServer Object and RemoteServer2 Object|<xref:Microsoft.SqlServer.Management.Common.ServerConnection> object. Moved to <xref:Microsoft.SqlServer.Management.Common> namespace.|  
|Replication Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|ReplicationDatabase Object and ReplicationDatabase2 Object|<xref:Microsoft.SqlServer.Replication.ReplicationDatabase> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|ReplicationSecurity Object|<xref:Microsoft.SqlServer.Management.Common.ServerConnection> object. Moved to <xref:Microsoft.SqlServer.Management.Common> namespace.|  
|ReplicationStoredProcedure Object and ReplicationStoredProcedure2 Object|<xref:Microsoft.SqlServer.Replication.ReplicationStoredProcedure> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|ReplicationTable Object and ReplicationTable2 Object|<xref:Microsoft.SqlServer.Replication.ReplicationTable> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|Restore Object and Restore2 Object|<xref:Microsoft.SqlServer.Management.Smo.Restore> and <xref:Microsoft.SqlServer.Management.Smo.BackupRestoreBase> objects.|  
|Rule Object and Rule2 Object|<xref:Microsoft.SqlServer.Management.Smo.Rule> object|  
|Schedule Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|ServerGroup Object|Removed.|  
|ServerRole Object|<xref:Microsoft.SqlServer.Management.Smo.ServerRole> object.|  
|SQLObjectList Object|<xref:Microsoft.SqlServer.Management.Smo.SqlSmoObject> array.|  
|SQLServer Object and SQLServer2 Object|<xref:Microsoft.SqlServer.Management.Smo.Server> object.|  
|StoredProcedure Object and StoredProcedure2 Object|<xref:Microsoft.SqlServer.Management.Smo.StoredProcedure> and <xref:Microsoft.SqlServer.Management.Smo.StoredProcedureParameter> objects|  
|Subscriber Object and Subscriber2 Object|Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|SystemDatatype Object and SystemDataType2 Object|<xref:Microsoft.SqlServer.Management.Smo.DataType> object.|  
|Table Object and Table2 Object|<xref:Microsoft.SqlServer.Management.Smo.Table> object.|  
|TargetServer Object|Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|TargetServerGroup Object|Moved to <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.|  
|TransactionLog Object|Functionality moved into the <xref:Microsoft.SqlServer.Management.Smo.Database> object.|  
|TransArticle Object and TransArticle2 Object|<xref:Microsoft.SqlServer.Replication.TransArticle> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|Transfer Method and Transfer2 Object|<xref:Microsoft.SqlServer.Management.Smo.Transfer> object.|  
|TransPublication Object and TransPublication2 Object|<xref:Microsoft.SqlServer.Replication.TransPublication> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|TransPullSubscription Object and TransPullSubscription2 Object|<xref:Microsoft.SqlServer.Replication.TransPullSubscription> object. Moved to <xref:Microsoft.SqlServer.Replication> namespace.|  
|Trigger Object and Trigger2 Object|<xref:Microsoft.SqlServer.Management.Smo.Trigger> object.|  
|User Object and User2 Object|<xref:Microsoft.SqlServer.Management.Smo.User> object.|  
|UserDefinedDatatype Object and UserDefinedDataType2 Object|<xref:Microsoft.SqlServer.Management.Smo.UserDefinedType> object.|  
|UserDefinedFunction Object|<xref:Microsoft.SqlServer.Management.Smo.UserDefinedFunction> and <xref:Microsoft.SqlServer.Management.Smo.UserDefinedFunctionParameter> objects.|  
|View Object and View2 Object|<xref:Microsoft.SqlServer.Management.Smo.View> object.|  
  
## See Also  
 [SQL Server Management Objects &#40;SMO&#41; Programming Guide](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md)  
  
  
