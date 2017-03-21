---
title: "Use Dynamic Management Views (DMVs) to Monitor Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 22b82b2d-867f-4ebf-9288-79d1cdd62f18
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Use Dynamic Management Views (DMVs) to Monitor Analysis Services
  Analysis Services Dynamic Management Views (DMV) are query structures that expose information about local server operations and server health. The query structure is an interface to schema rowsets that return metadata and monitoring information about an Analysis Services instance.  
  
 For most DMV queries, you use a **SELECT** statement and the **$System** schema with an XML/A schema rowset.  
  
```  
SELECT * FROM $System.<schemaRowset>  
```  
  
 DMV queries return information about server state that is current at the time the query was run. To monitor operations in real time, use tracing instead. For more information, see [Use SQL Server Profiler to Monitor Analysis Services](../../analysis-services/instances/use-sql-server-profiler-to-monitor-analysis-services.md).  
  
 This topic includes the following sections:  
  
 [Benefits of Using DMV Queries](#bkmk_ben)  
  
 [Examples and Scenarios](#bkmk_ex)  
  
 [Query Syntax](#bkmk_syn)  
  
 [Tools and Permissions](#bkmk_tools)  
  
 [DMV Reference](#bkmk_ref)  
  
##  <a name="bkmk_ben"></a> Benefits of Using DMV Queries  
 DMV queries return information about operations and resource consumption that are not available through other means.  
  
 DMV queries are an alternative to running XML/A Discover commands. For most administrators, writing a DMV query is simpler because the query syntax is based on SQL. In addition, the result set is returned in a tabular format that is easier to read and copy from.  
  
##  <a name="bkmk_ex"></a> Examples and Scenarios  
 A DMV query can help you answer questions about active sessions and connections, and which objects are consuming the most CPU or memory at a specific point in time. This section provides examples for scenarios where DMV queries are most commonly used. You can also review the [SQL Server 2008 R2 Analysis Services Operations Guide](http://go.microsoft.com/fwlink/?LinkID=225539&clcid=0x409) for additional insights into using DMV queries to monitor a server instance.  
  
 `Select * from $System.discover_object_activity` /** This query reports on object activity since the service last started. For example queries based on this DMV, see [New System.Discover_Object_Activity](http://go.microsoft.com/fwlink/?linkid=221322).  
  
 `Select * from $System.discover_object_memory_usage` /** This query reports on memory consumption by object.  
  
 `Select * from $System.discover_sessions` /** This query reports on active sessions, including session user and duration.  
  
 `Select * from $System.discover_locks` /** This query returns a snapshot of the locks used at a specific point in time.  
  
##  <a name="bkmk_syn"></a> Query Syntax  
 The query engine for DMVs is the Data Mining parser. The DMV query syntax is based on the [SELECT &#40;DMX&#41;](../../dmx/select-dmx.md) statement.  
  
 Although DMV query syntax is based on a SQL SELECT statement, it does not support the full syntax of a SELECT statement. Notably, JOIN, GROUP BY, LIKE, CAST, and CONVERT are not supported.  
  
```  
SELECT [DISTINCT] [TOP <n>] <select list>  
FROM $System.<schemaRowset>  
[WHERE <condition expression>]  
[ORDER BY <expression>[DESC|ASC]]  
```  
  
 The following example for DISCOVER_CALC_DEPENDENCY illustrates the use of the WHERE clause for supplying a parameter to the query:  
  
```  
SELECT * FROM $System.DISCOVER_CALC_DEPENDENCY  
WHERE OBJECT_TYPE = 'ACTIVE_RELATIONSHIP'  
```  
  
 Alternatively, for schema rowsets that have restrictions, the query must include the SYSTEMRESTRICTSCHEMA function. The following example returns CSDL metadata about tabular models running on a tabular mode server. Note that CATALOG_NAME is case-sensitive:  
  
```  
Select * from SYSTEMRESTRICTSCHEMA ($System.Discover_csdl_metadata, [CATALOG_NAME] = 'Adventure Works DW')  
```  
  
##  <a name="bkmk_tools"></a> Tools and Permissions  
 You must have system administrator permissions on the Analysis Services instance to query a DMV.  
  
 You can use any client application that supports MDX or DMX queries, including SQL Server Management Studio, a Reporting Services report, or a PerformancePoint Dashboard.  
  
 To run a DMV query from Management Studio, connect to the instance you want to query, click **New Query**. You can run a query from an MDX or a DMX query window.  
  
##  <a name="bkmk_ref"></a> DMV Reference  
 Not all schema rowsets have a DMV interface. To return a list of all the schema rowsets that can be queried using DMV, run the following query.  
  
```  
SELECT * FROM $System.DBSchema_Tables   
WHERE TABLE_TYPE = 'SCHEMA'   
ORDER BY TABLE_NAME ASC  
```  
  
> [!NOTE]  
>  If a DMV is not available for a given rowset, the server returns the following error: “The \<schemarowset> request type was not recognized by the server". All other errors point to problems with the syntax.  
  
|Rowset|Description|  
|------------|-----------------|  
|[DBSCHEMA_CATALOGS Rowset](../../analysis-services/schema-rowsets/ole-db/dbschema-catalogs-rowset.md)|Returns a list of the Analysis Services databases on the current connection.|  
|[DBSCHEMA_COLUMNS Rowset](../../analysis-services/schema-rowsets/ole-db/dbschema-columns-rowset.md)|Returns a list of all the columns in the current database. You can use this list to construct a DMV query.|  
|[DBSCHEMA_PROVIDER_TYPES Rowset](../../analysis-services/schema-rowsets/ole-db/dbschema-provider-types-rowset.md)|Returns properties about the base data types supported by the OLE DB data provider.|  
|[DBSCHEMA_TABLES Rowset](../../analysis-services/schema-rowsets/ole-db/dbschema-tables-rowset.md)|Returns a list of all the tables in the current database. You can use this list to construct a DMV query.|  
|[DISCOVER_CALC_DEPENDENCY Rowset](../../analysis-services/schema-rowsets/xml/discover-calc-dependency-rowset.md)|Returns a list of the columns and tables used in a model that have dependencies on other columns and tables.|  
|[DISCOVER_COMMAND_OBJECTS Rowset](../../analysis-services/schema-rowsets/xml/discover-command-objects-rowset.md)|Provides resource usage and activity information about objects in use by the referenced command.|  
|[DISCOVER_COMMANDS Rowset](../../analysis-services/schema-rowsets/xml/discover-commands-rowset.md)|Provides resource usage and activity information about currently executing command.|  
|[DISCOVER_CONNECTIONS Rowset](../../analysis-services/schema-rowsets/xml/discover-connections-rowset.md)|Provides resource usage and activity information about open connections to Analysis Services.|  
|[DISCOVER_CSDL_METADATA Rowset](../../analysis-services/schema-rowsets/xml/discover-csdl-metadata-rowset.md)|Returns information about a tabular model.<br /><br /> Requires the addition of SYSTEMRESTRICTSCHEMA and additional parameters.|  
|[DISCOVER_DB_CONNECTIONS Rowset](../../analysis-services/schema-rowsets/xml/discover-db-connections-rowset.md)|Provides resource usage and activity information about open connections from Analysis Services to external data sources, for example during processing or importing.|  
|[DISCOVER_DIMENSION_STAT Rowset](../../analysis-services/schema-rowsets/xml/discover-dimension-stat-rowset.md)|Returns the attributes in a dimension or columns in a table, depending on the model type.|  
|[DISCOVER_ENUMERATORS Rowset](../../analysis-services/schema-rowsets/xml/discover-enumerators-rowset.md)|Returns metadata about the enumerators supported for a specific data source.|  
|[DISCOVER_INSTANCES Rowset](../../analysis-services/schema-rowsets/ole-db-olap/discover-instances-rowset.md)|Returns information about the specified instance.<br /><br /> Requires the addition of SYSTEMRESTRICTSCHEMA and additional parameters.|  
|[DISCOVER_JOBS Rowset](../../analysis-services/schema-rowsets/xml/discover-jobs-rowset.md)|Returns information about current jobs.|  
|[DISCOVER_KEYWORDS Rowset &#40;XMLA&#41;](../../analysis-services/schema-rowsets/xml/discover-keywords-rowset-xmla.md)|Returns the list of reserved keywords.|  
|[DISCOVER_LITERALS Rowset](../../analysis-services/schema-rowsets/xml/discover-literals-rowset.md)|Returns the list of literals, including data types and values, supported by XMLA.|  
|[DISCOVER_LOCKS Rowset](../../analysis-services/schema-rowsets/xml/discover-locks-rowset.md)|Returns a snapshot of the locks used at a specific point in time.|  
|[DISCOVER_MEMORYGRANT Rowset](../../analysis-services/schema-rowsets/xml/discover-memorygrant-rowset.md)|Returns information about memory allocated by Analysis Services at start up.|  
|[DISCOVER_MEMORYUSAGE Rowset](../../analysis-services/schema-rowsets/xml/discover-memoryusage-rowset.md)|Shows memory usage by specific objects.|  
|[DISCOVER_OBJECT_ACTIVITY Rowset](../../analysis-services/schema-rowsets/xml/discover-object-activity-rowset.md)|Reports on object activity since the service last started.|  
|[DISCOVER_OBJECT_MEMORY_USAGE Rowset](../../analysis-services/schema-rowsets/xml/discover-object-memory-usage-rowset.md)|Reports on memory consumption by object.|  
|[DISCOVER_PARTITION_DIMENSION_STAT Rowset](../../analysis-services/schema-rowsets/xml/discover-partition-dimension-stat-rowset.md)|Provides information about the attributes in a dimension.<br /><br /> Requires the addition of SYSTEMRESTRICTSCHEMA and additional parameters.|  
|[DISCOVER_PARTITION_STAT Rowset](../../analysis-services/schema-rowsets/xml/discover-partition-stat-rowset.md)|Provides information about the partitions in a dimension, table, or measure group.<br /><br /> Requires the addition of SYSTEMRESTRICTSCHEMA and additional parameters.|  
|[DISCOVER_PERFORMANCE_COUNTERS Rowset](../../analysis-services/schema-rowsets/xml/discover-performance-counters-rowset.md)|Lists the columns used in a performance counter.<br /><br /> Requires the addition of SYSTEMRESTRICTSCHEMA and additional parameters.|  
|[DISCOVER_PROPERTIES Rowset](../../analysis-services/schema-rowsets/xml/discover-properties-rowset.md)|Returns information about properties supported by XMLA for the specified data source.|  
|[DISCOVER_SCHEMA_ROWSETS Rowset](../../analysis-services/schema-rowsets/xml/discover-schema-rowsets-rowset.md)|Returns names, restrictions, description and other information for all enumeration values supported by XMLA.|  
|[DISCOVER_SESSIONS Rowset](../../analysis-services/schema-rowsets/xml/discover-sessions-rowset.md)|Reports on active sessions, including session user and duration.|  
|[DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS Rowset](../../analysis-services/schema-rowsets/xml/discover-storage-table-column-segments-rowset.md)|Provides information at the column and segment level about storage tables used by an Analysis Services database running in Tabular or SharePoint mode.|  
|[DISCOVER_STORAGE_TABLE_COLUMNS Rowset](../../analysis-services/schema-rowsets/xml/discover-storage-table-columns-rowset.md)|Allows the client to determine the assignment of columns to storage tables used by an Analysis Services database running in Tabular or SharePoint mode.|  
|[DISCOVER_STORAGE_TABLES Rowset](../../analysis-services/schema-rowsets/xml/discover-storage-tables-rowset.md)|Returns information about the tables used for storage of models in a Tabular model database.|  
|[DISCOVER_TRACE_COLUMNS Rowset](../../analysis-services/schema-rowsets/xml/discover-trace-columns-rowset.md)|Returns an XML description of the columns available in a trace.|  
|[DISCOVER_TRACE_DEFINITION_PROVIDERINFO Rowset](../../analysis-services/schema-rowsets/xml/discover-trace-definition-providerinfo-rowset.md)|Returns name and version information of the provider.|  
|[DISCOVER_TRACE_EVENT_CATEGORIES Rowset](../../analysis-services/schema-rowsets/xml/discover-trace-event-categories-rowset.md)|Returns a list of available categories.|  
|[DISCOVER_TRACES Rowset](../../analysis-services/schema-rowsets/xml/discover-traces-rowset.md)|Returns a list of traces actively running on the current connection.|  
|[DISCOVER_TRANSACTIONS Rowset](../../analysis-services/schema-rowsets/xml/discover-transactions-rowset.md)|Returns a list of transactions actively running on the current connection.|  
|[DISCOVER_XEVENT_TRACE_DEFINITION Rowset](http://msdn.microsoft.com/library/e1ce2d2d-f994-4318-801a-ee0385aecd84)|Returns a list of xevent traces actively running on the current connection.|  
|[DMSCHEMA_MINING_COLUMNS Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-columns-rowset.md)|Lists the individual columns of all mining models available on the current connection.|  
|[DMSCHEMA_MINING_FUNCTIONS Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-functions-rowset.md)|Returns a list of functions supported by the data mining algorithms on the server.|  
|[DMSCHEMA_MINING_MODEL_CONTENT Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-model-content-rowset.md)|Returns a rowset consisting of columns that describe the current model.|  
|[DMSCHEMA_MINING_MODEL_CONTENT_PMML Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-model-content-pmml-rowset.md)|Returns a rowset consisting of columns that describe the current model in PMML format.|  
|[DMSCHEMA_MINING_MODEL_XML Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-model-xml-rowset.md)|Returns a rowset consisting of columns that describe the current model in PMML format.|  
|[DMSCHEMA_MINING_MODELS Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-models-rowset.md)|Returns a list of the mining models in the current database.|  
|[DMSCHEMA_MINING_SERVICE_PARAMETERS Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-service-parameters-rowset.md)|Returns a list of the parameters for the algorithms on the server.|  
|[DMSCHEMA_MINING_SERVICES Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-services-rowset.md)|Provides a list of the data mining algorithms available on the server.|  
|[DMSCHEMA_MINING_STRUCTURE_COLUMNS Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-structure-columns-rowset.md)|Returns a list of all of the columns from all of the mining models available in the current connection.|  
|[DMSCHEMA_MINING_STRUCTURES Rowset](../../analysis-services/schema-rowsets/data-mining/dmschema-mining-structures-rowset.md)|Lists the mining structures available in the current connection.|  
|[MDSCHEMA_CUBES Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-cubes-rowset.md)|Returns information about the cubes that are defined in the current database.|  
|[MDSCHEMA_DIMENSIONS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-dimensions-rowset.md)|Returns information about the dimensions that are defined in the current database.|  
|[MDSCHEMA_FUNCTIONS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-functions-rowset.md)|Returns a list of functions available to client applications connected to the database.|  
|[MDSCHEMA_HIERARCHIES Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-hierarchies-rowset.md)|Returns information about the hierarchies that are defined in the current database.|  
|[MDSCHEMA_INPUT_DATASOURCES Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-input-datasources-rowset.md)|Returns information about the data source objects that are defined in the current database.|  
|[MDSCHEMA_KPIS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-kpis-rowset.md)|Returns information about the KPIs that are defined in the current database.|  
|[MDSCHEMA_LEVELS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-levels-rowset.md)|Returns information about the levels within the hierarchies that are defined in the current database.|  
|[MDSCHEMA_MEASUREGROUP_DIMENSIONS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-measuregroup-dimensions-rowset.md)|Lists the dimension of measure groups.|  
|[MDSCHEMA_MEASUREGROUPS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-measuregroups-rowset.md)|Returns a list of measure groups in the current connection.|  
|[MDSCHEMA_MEASURES Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-measures-rowset.md)|Returns a list of measures in the current connection.|  
|[MDSCHEMA_MEMBERS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-members-rowset.md)|Returns a list of all members in the current connection, listed by database, cube, and dimension.|  
|[MDSCHEMA_PROPERTIES Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-properties-rowset.md)|Returns a fully qualified name of each property, along with property type, data type, and other metadata.|  
|[MDSCHEMA_SETS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-sets-rowset.md)|Returns a list of set that are defined in the current connection.|  
  
## See Also  
 [SQL Server 2008 R2 Analysis Services Operations Guide](http://go.microsoft.com/fwlink/?LinkID=225539&clcid=0x409)   
 [New System.Discover_Object_Activity](http://go.microsoft.com/fwlink/?linkid=221322)   
 [New SYSTEMRESTRICTEDSCHEMA Function for Restricted Rowsets and DMVs](http://go.microsoft.com/fwlink/?LinkId=231885)  
  
  