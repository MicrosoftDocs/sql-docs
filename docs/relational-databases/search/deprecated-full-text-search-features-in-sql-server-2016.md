---
title: "Deprecated Full-Text Search Features in SQL Server 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "08/19/2016"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "deprecated features [full-text search]"
  - "full-text search [SQL Server], deprecated features"
  - "full-text queries [SQL Server], proximity"
ms.assetid: ab0d799c-ba79-4459-837b-c4862730dafd
author: douglaslMS
ms.author: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Deprecated Full-Text Search Features in SQL Server 2016
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic describes the deprecated full-text search features still available in SQL Server. These features are scheduled to be removed in a future release. Do not use deprecated features in new applications.  
  
Monitor your use of deprecated features by using the **SQL Server:Deprecated Features** object performance counter and trace events. For more information, see [Use SQL Server Objects](../../relational-databases/performance-monitor/use-sql-server-objects.md).  
  
## Features no longer supported  

  
|Deprecated feature|Replacement|Feature name|Feature ID|  
|------------------------|-----------------|------------------|----------------|  
|FULLTEXTCATALOGPROPERTY property: LogSize|None.|FULLTEXTCATALOGPROPERTY**('LogSize')**|211|  
|FULLTEXTSERVICEPROPERTY property:<br /><br /> ConnectTimeout<br /><br /> DataTimeout|None.|FULLTEXTSERVICEPROPERTY**('ConnectTimeout')**<br /><br /> FULLTEXTSERVICEPROPERTY**('DataTimeout'**)|210<br /><br /> 209|  
|sp_fulltext_catalog|CREATE FULL CATALOG<br /><br /> ALTER FULLTEXT CATALOG<br /><br /> DROP FULLTEXT CATALOG|sp_fulltext_catalog|84|  
|sp_fulltext_column<br /><br /> sp_fulltext_database<br /><br /> sp_fulltext_table|CREATE FULL INDEX<br /><br /> ALTER FULLTEXT INDEX<br /><br /> DROP FULLTEXT INDEX|sp_fulltext_column<br /><br /> sp_fulltext_database<br /><br /> sp_fulltext_table|86<br /><br /> 87<br /><br /> 85|  
|sp_help_fulltext_catalogs<br /><br /> sp_help_fulltext_catalog_components<br /><br /> sp_help_fulltext_catalogs_cursor<br /><br /> sp_help_fulltext_columns<br /><br /> sp_help_fulltext_columns_cursor<br /><br /> sp_help_fulltext_tables<br /><br /> sp_help_fulltext_tables_cursor|sys.fulltext_catalogs<br /><br /> sys.fulltext_index_columns<br /><br /> sys.fulltext_indexes|sp_help_fulltext_catalogs<br /><br /> sp_help_fulltext_catalog_components<br /><br /> sp_help_fulltext_catalogs_cursor<br /><br /> sp_help_fulltext_columns<br /><br /> sp_help_fulltext_columns_cursor<br /><br /> sp_help_fulltext_table<br /><br /> sp_help_fulltext_tables_cursor|88<br /><br /> 203<br /><br /> 90<br /><br /> 92<br /><br /> 93<br /><br /> 91<br /><br /> 89|  
|sp_fulltext_service action values: clean_up, connect_timeout, and data_timeout return zero|None|sp_fulltext_service @action=clean_up<br /><br /> sp_fulltext_service @action=connect_timeout<br /><br /> sp_fulltext_service @action=data_timeout|116<br /><br /> 117<br /><br /> 118|  
|sys.dm_fts_active_catalogs columns:<br /><br /> is_paused<br /><br /> previous_status<br /><br /> previous_status_description<br /><br /> row_count_in_thousands<br /><br /> status<br /><br /> status_description<br /><br /> worker_count|None.|dm_fts_active_catalogs.is_paused<br /><br /> dm_fts_active_catalogs.previous_status<br /><br /> dm_fts_active_catalogs.previous_status_description<br /><br /> dm_fts_active_catalogs.row_count_in_thousands<br /><br /> dm_fts_active_catalogs.status<br /><br /> dm_fts_active_catalogs.status_description<br /><br /> dm_fts_active_catalogs.worker_count|218<br /><br /> 221<br /><br /> 222<br /><br /> 224<br /><br /> 219<br /><br /> 220<br /><br /> 223|  
|sys.dm_fts_memory_buffers column:<br /><br /> row_count|None.|dm_fts_memory_buffers.row_count|225|  
|sys.fulltext_catalogs columns:<br /><br /> path<br /><br /> data_space_id<br /><br /> file_id columns|None.|fulltext_catalogs.path<br /><br /> fulltext_catalogs.data_space_id<br /><br /> fulltext_catalogs.file_id|215<br /><br /> 216<br /><br /> 217|  
  
## Features Not Supported in a Future Version of SQL Server  
 The following full-text search features are supported in the next version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but will be removed in a later version. The specific version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has not been determined.  
  
 The **Feature name** value appears in trace events as the ObjectName and in performance counters and sys.dm_os_performance_counters as the instance name. The **Feature ID** value appears in trace events as the ObjectId.  
  
|Deprecated feature|Replacement|Feature name|Feature ID|  
|------------------------|-----------------|------------------|----------------|  
|CONTAINS and CONTAINSTABLE generic NEAR operator:<br /><br /> {<simple_term> &#124; <prefix_term>}<br /><br /> {<br /><br /> { { NEAR &#124; ~ }    {<simple_term> &#124; <prefix_term>} } [...*n*]<br /><br /> }|The custom NEAR operator:<br /><br /> NEAR(<br /><br /> {   {<simple_term> &#124; <prefix_term>} [ ,...*n* ]<br /><br /> &#124; ( {<simple_term> &#124; <prefix_term>} [,...*n*] )<br /><br /> [,<distance> [,<order>] ]<br /><br /> }<br /><br /> )<br /><br /> <distance> ::= {*integer* &#124; **MAX**}<br /><br /> <order> ::= {TRUE &#124; **FALSE**}|FULLTEXT_OLD_NEAR_SYNTAX|247|  
|CREATE FULLTEXT CATALOG option:<br /><br /> IN PATH '*rootpath*'<br /><br /> ON FILEGROUP *filegroup*|None.|CREATE FULLTEXT CATLOG IN PATH<br /><br /> None.<sup>*</sup>|237<br /><br /> None.*|  
|DATABASEPROPERTYEX property: IsFullTextEnabled|None.|DATABASEPROPERTYEX**('IsFullTextEnabled')**|202|  
|sp_detach_db option:<br /><br /> [ @keepfulltextindexfile = ] '*KeepFulltextIndexFile*'|None.|sp_detach_db @keepfulltextindexfile|226|  
|sp_fulltext_service action values: resource_usage has no function.|None|sp_fulltext_service @action=resource_usage|200|  
  
 &#42;The **SQL Server:Deprecated Features** object does not monitor occurrences of CREATE FULLTEXT CATLOG ON FILEGROUP *filegroup*.  
  
  
