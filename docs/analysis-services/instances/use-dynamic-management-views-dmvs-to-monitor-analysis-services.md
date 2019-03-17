---
title: "Use Dynamic Management Views (DMVs) in Analysis Services | Microsoft Docs"
ms.date: 09/25/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Dynamic Management Views (DMVs) 

[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]

Analysis Services Dynamic Management Views (DMV) are queries that return information about model objects, server operations, and server health. The query, based on SQL, is an interface to *schema rowsets*. Schema rowsets are predescribed tables that contain information about Analysis Services objects and server state, including database schema, active sessions, connections, commands, and jobs that are executing on the server.

DMV queries are an alternative to running XML/A Discover commands. For most administrators, writing a DMV query is simpler because the syntax is based on SQL. In addition, the result is returned in a table format that is easier to read and copy. 
  
Most DMV queries use a **SELECT** statement and the **$System** schema with an XML/A schema rowset, for example:  
  
```  
SELECT * FROM $System.<schemaRowset>  
```  
  
 DMV queries return information about server and object state at the time the query is run. To monitor operations in real-time, use tracing instead. To learn more about real-time monitoring using traces, see [Use SQL Server Profiler to Monitor Analysis Services](../../analysis-services/instances/use-sql-server-profiler-to-monitor-analysis-services.md).  
  
## Query syntax

The query engine for DMVs is the Data Mining parser. The DMV query syntax is based on the SELECT &#40;DMX&#41; statement. Although DMV query syntax is based on a SQL SELECT statement, it does not support the full syntax of a SELECT statement. Notably, JOIN, GROUP BY, LIKE, CAST, and CONVERT are not supported.  
  
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
  
For schema rowsets that have restrictions, the query must include the SYSTEMRESTRICTSCHEMA function. The following example returns CSDL metadata about 1103 compatibility level tabular models. Note that CATALOG_NAME is case-sensitive:  
  
```  
Select * from SYSTEMRESTRICTSCHEMA ($System.Discover_csdl_metadata, [CATALOG_NAME] = 'Adventure Works DW')  
```  

## Examples and scenarios

A DMV query can help you answer questions about active sessions and connections, and which objects are consuming the most CPU or memory at a specific point in time. For example:
  
 `Select * from $System.discover_object_activity`  
This query reports on object activity since the service last started. 
  
 `Select * from $System.discover_object_memory_usage`  
This query reports on memory consumption by object.  
  
 `Select * from $System.discover_sessions`  
This query reports on active sessions, including session user and duration.  
  
 `Select * from $System.discover_locks`   
This query returns a snapshot of the locks used at a specific point in time.  


## Tools and permissions

You can use any client application that supports MDX or DMX queries. In most cases, it's best to use SQL Server Management Studio. You must have server administrator permissions on the instance to query a DMV.  
  
 **To run a DMV query from SQL Server Management Studio**

1. Connect to the server and model object you want to query. 
2. Right-click the server or database object > **New Query** > **MDX**.
3. Type your query, and then click **Execute**, or press F5.
  
## Schema rowsets

Not all schema rowsets have a DMV interface. To return a list of all the schema rowsets that can be queried using DMV, run the following query.  
 
```  
SELECT * FROM $System.DBSchema_Tables   
WHERE TABLE_TYPE = 'SCHEMA'   
ORDER BY TABLE_NAME ASC  
```  
  
If a DMV is not available for a given rowset, the server returns error: `The <schemarowset> request type was not recognized by the server.` All other errors indicate problems with the syntax.  

Schema rowsets are described in two SQL Server Analysis Services protocols:   

[[MS-SSAS-T]: SQL Server Analysis Services Tabular Protocol](https://msdn.microsoft.com/library/mt719260) - Describes schema rowsets for tabular models at the 1200 and higher compatibility levels.

[[MS-SSAS]: SQL Server Analysis Services Protocol](https://msdn.microsoft.com/library/ee320606) - Describes schema rowsets for multidimensional models and tabular models at the 1100 and 1103 compatibility levels.

### Rowsets described in the [MS-SSAS-T]: SQL Server Analysis Services Tabular Protocol

|Rowset  |Description  |
|---------|---------|
|[TMSCHEMA_ANNOTATIONS](https://msdn.microsoft.com/library/mt704370)|Provides information about the Annotation objects in the model.|
|[TMSCHEMA_ATTRIBUTE_HIERARCHIES](https://msdn.microsoft.com/library/mt704362)     |   Provides information about the AttributeHierarchy objects for a column.      |
|[TMSCHEMA_COLUMNS](https://msdn.microsoft.com/library/mt704373)    |  Provides information about the Column objects in each table.       |
|[TMSCHEMA_COLUMN_PERMISSIONS](https://msdn.microsoft.com/library/mt842440)|Provides information about the ColumnPermission objects in each table-permission.|
|[TMSCHEMA_CULTURES](https://msdn.microsoft.com/library/mt719125)|Provides information about the Culture objects in the model.|
|[TMSCHEMA_DATA_SOURCES](https://msdn.microsoft.com/library/mt719191)   |   Provides information about the DataSource objects in the model.      |
|[TMSCHEMA_DETAIL_ROWS_DEFINITIONS](https://msdn.microsoft.com/library/mt825017)|Provides information about the DetailRowsDefinition objects in the model.|
|[TMSCHEMA_EXPRESSIONS](https://msdn.microsoft.com/library/mt825015)|Provides information about the Expression objects in the model.|
|[TMSCHEMA_EXTENDED_PROPERTIES](https://msdn.microsoft.com/library/mt842451)|Provides information about the ExtendedProperty objects in the model.|
|[TMSCHEMA_HIERARCHIES](https://msdn.microsoft.com/library/mt719136)    |    Provides information about the Hierarchy objects in each table.     |
|[TMSCHEMA_KPIS](https://msdn.microsoft.com/library/mt719002)     |    Provides information about the KPI objects in the model.     |
|[TMSCHEMA_LEVELS](https://msdn.microsoft.com/library/mt719038)     |   Provides information about the Level objects in each hierarchy.      |
|[TMSCHEMA_LINGUISTIC_METADATA](https://msdn.microsoft.com/library/mt719169)|Provides information about the synonyms for objects in the model for a particular culture|
|[TMSCHEMA_MEASURES](https://msdn.microsoft.com/library/mt719218)     |    Provides information about the Measure objects in each table.     |
|[TMSCHEMA_MODEL](https://msdn.microsoft.com/library/mt719042)    |  Specifies a Model object in the database.       |
|[TMSCHEMA_OBJECT_TRANSLATIONS](https://msdn.microsoft.com/library/mt719119)|Provides information about the translations of different objects for a culture.|
|[TMSCHEMA_PARTITIONS](https://msdn.microsoft.com/library/mt704375)     |     Provides information about the Partition objects in each table.    |
|[TMSCHEMA_PERSPECTIVE_COLUMNS](https://msdn.microsoft.com/library/mt719164)     |   Provides information about the PerspectiveColumn objects in each PerspectiveTable object.      |
|[TMSCHEMA_PERSPECTIVE_HIERARCHIES](https://msdn.microsoft.com/library/mt719104)     |  Provides information about the PerspectiveHierarchy objects in each PerspectiveTable object.       |
|[TMSCHEMA_PERSPECTIVE_MEASURES](https://msdn.microsoft.com/library/mt719135)     |    Provides information about the PerspectiveMeasure objects in each PerspectiveTable object.     |
|[TMSCHEMA_PERSPECTIVE_TABLES](https://msdn.microsoft.com/library/mt719272)     |    Provides information about the Table objects in a perspective.     |
|[TMSCHEMA_PERSPECTIVES](https://msdn.microsoft.com/library/mt704340)     |     Provides information about the Perspective objects in the model.    |
|[TMSCHEMA_RELATIONSHIPS](https://msdn.microsoft.com/library/mt704355)     |    Provides information about the Relationship objects in the model.     |
|[TMSCHEMA_ROLE_MEMBERSHIPS](https://msdn.microsoft.com/library/mt704584)     |  Provides information about the RoleMembership objects in each role.      |
|[TMSCHEMA_ROLES](https://msdn.microsoft.com/library/mt719267)    |   Provides information about the Role objects in the model.      |
|[TMSCHEMA_TABLE_PERMISSIONS](https://msdn.microsoft.com/library/mt704347)    |    Provides information about the TablePermission objects in each role.     |
|[TMSCHEMA_TABLES](https://msdn.microsoft.com/library/mt719250)     |   Provides information about the Table objects in the model.      |
|[TMSCHEMA_VARIATIONS](https://msdn.microsoft.com/library/mt825008)|Provides information about the Variation objects in each column.|

### Rowsets described in the [MS-SSAS]: SQL Server Analysis Services Protocol

|Rowset|Description|  
|------------|-----------------|  
|[DBSCHEMA_CATALOGS](https://msdn.microsoft.com/library/ee302115)|Describes the catalogs that are accessible on the server.|  
|[DBSCHEMA_COLUMNS](https://msdn.microsoft.com/library/ee301789)|Returns a row for each measure, each cube dimension attribute, and each schema rowset column, exposed as a column.|  
|[DBSCHEMA_PROVIDER_TYPES](https://msdn.microsoft.com/library/ee301696)|Identifies the (base) data types supported by the server.|  
|[DBSCHEMA_TABLES](https://msdn.microsoft.com/library/ee320843)|Returns dimensions, measure groups, or schema rowsets exposed as tables.|  
|[DISCOVER_CALC_DEPENDENCY](https://msdn.microsoft.com/library/hh770226)| Returns information about the calculation dependency for an object that is specified in a Tabular database or in a DAX query that is executed against a Tabular database. |  
|[DISCOVER_COMMAND_OBJECTS](https://msdn.microsoft.com/library/ee320662)|Provides resource usage and activity information about the objects in use by the referenced command.|  
|[DISCOVER_COMMANDS](https://msdn.microsoft.com/library/ee320715)|Provides resource usage and activity information about the currently executing or last executed commands in the opened connections on the server.|  
|[DISCOVER_CONNECTIONS](https://msdn.microsoft.com/library/ee301889)|Provides resource usage and activity information about the currently opened connections on the server.|  
|[DISCOVER_CSDL_METADATA](https://msdn.microsoft.com/library/gg587670)|Returns information about database metadata for in-memory databases.|  
|[DISCOVER_DATASOURCES](https://msdn.microsoft.com/library/ee320285)|Returns a list of the data sources that are available on the server.|
|[DISCOVER_DB_CONNECTIONS](https://msdn.microsoft.com/library/ee320467)|Provides resource usage and activity information about the currently opened connections from the server to a database.|  
|[DISCOVER_DIMENSION_STAT](https://msdn.microsoft.com/library/ee320284)|returns statistics on the specified dimension.|  
|[DISCOVER_ENUMERATORS](https://msdn.microsoft.com/library/ee302012)|Returns a list of names, data types, and enumeration values of enumerators supported by the XMLA Provider for a specific data source.|  
|[DISCOVER_INSTANCES](https://msdn.microsoft.com/library/ee320541)|Describes the instances on the server.|  
|[DISCOVER_JOBS](https://msdn.microsoft.com/library/ee320363)|Provides information about the active jobs executing on the server. A job is a part of a command that executes a specific task on behalf of the command.|  
|[DISCOVER_KEYWORDS &#40;XMLA&#41;](https://msdn.microsoft.com/library/ee301719)|Returns information about keywords that are reserved by the XMLA server.|  
|[DISCOVER_LITERALS](https://msdn.microsoft.com/library/ee301320)|Returns information about literals supported by the server.|  
|[DISCOVER_LOCATIONS](https://msdn.microsoft.com/library/ee302024)|Returns information about the contents of a backup file. |
|[DISCOVER_LOCKS](https://msdn.microsoft.com/library/ee320398)|Provides information about the current standing locks on the server.|  
|[DISCOVER_MASTER_KEY](https://msdn.microsoft.com/library/ee301825)|Returns the server's master encryption key.|
|[DISCOVER_MEMORYGRANT](https://msdn.microsoft.com/library/ee320945)|Returns a list of internal memory quota grants that are taken by jobs that are currently running on the server.|  
|[DISCOVER_MEMORYUSAGE](https://msdn.microsoft.com/library/ee320910)|Returns the DISCOVER_MEMORYUSAGE statistics for various objects allocated by the server.|  
|[DISCOVER_OBJECT_ACTIVITY](https://msdn.microsoft.com/library/ee320661)|Provides resource usage per object since the start of the service.|  
|[DISCOVER_OBJECT_MEMORY_USAGE](https://msdn.microsoft.com/library/ee320910)|Returns the DISCOVER_MEMORYUSAGE statistics for various objects allocated by the server.|  
|[DISCOVER_PARTITION_DIMENSION_STAT](https://msdn.microsoft.com/library/ee320268)|Returns statistics on the dimension that is associated with a partition.|  
|[DISCOVER_PARTITION_STAT](https://msdn.microsoft.com/library/ee320483)|Returns statistics on aggregations in a particular partition.|  
|[DISCOVER_PERFORMANCE_COUNTERS](https://msdn.microsoft.com/library/ee320809)|Returns the value of one or more specified performance counters. |  
|[DISCOVER_PROPERTIES](https://msdn.microsoft.com/library/ee320589)|Returns a list of information and values about the properties that are supported by the server for the specified data source.|  
|[DISCOVER_RING_BUFFERS](https://msdn.microsoft.com/library/mt719204)|Returns information about the current XEvent ring buffers on the server.|
|[DISCOVER_SCHEMA_ROWSETS](https://msdn.microsoft.com/library/ee320478)|Returns the names, restrictions, description, and other information for all Discover requests.|  
|[DISCOVER_SESSIONS](https://msdn.microsoft.com/library/ee301962)|Provides resource usage and activity information about the currently opened sessions on the server.|  
|[DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS](https://msdn.microsoft.com/library/ee320710)|Returns information about the column segments used for storing data for in-memory tables.|  
|[DISCOVER_STORAGE_TABLE_COLUMNS](https://msdn.microsoft.com/library/ee302101)|Contains information about the columns used for representing the columns of an in-memory table.|  
|[DISCOVER_STORAGE_TABLES](https://msdn.microsoft.com/library/ee302014)|Returns statistics about in-memory tables available to the server.|  
|[DISCOVER_TRACE_COLUMNS](https://msdn.microsoft.com/library/ee301342)||  
|[DISCOVER_TRACE_DEFINITION_PROVIDERINFO](https://msdn.microsoft.com/library/ee301342)|Contains the DISCOVER_TRACE_COLUMNS schema rowset.|  
|[DISCOVER_TRACE_EVENT_CATEGORIES](https://msdn.microsoft.com/library/ee320442)|Contains the DISCOVER_TRACE_EVENT_CATEGORIES schema rowset.|  
|[DISCOVER_TRACES](https://msdn.microsoft.com/library/ee301643)|Contains the DISCOVER_TRACES schema rowset.|  
|[DISCOVER_TRANSACTIONS](https://msdn.microsoft.com/library/ee301363)|Returns the current set of pending transactions on the system.|  
|[DISCOVER_XEVENT_TRACE_DEFINITION](https://msdn.microsoft.com/library/mt704568)|Provides information about the XEvent traces that are currently active on the server.|  
|[DISCOVER_XEVENT_PACKAGES](https://msdn.microsoft.com/library/mt704569)|Provides information about the XEvent packages that are described on the server.|
|[DISCOVER_XEVENT_OBJECTS](https://msdn.microsoft.com/library/mt704543)|Provides information about the XEvent objects that are described on the server.|
|[DISCOVER_XEVENT_OBJECT_COLUMNS](https://msdn.microsoft.com/library/mt719352)|Provides information about the schema of XEvent objects that are described on the server.|
|[DISCOVER_XEVENT_SESSIONS](https://msdn.microsoft.com/library/mt704397)|Provides information about the current XEvent sessions on the server.|
|[DISCOVER_XEVENT_SESSION_TARGETS](https://msdn.microsoft.com/library/mt704564)|Provides information about the current XEvent session targets on the server.|
|[DISCOVER_XML_METADATA](https://msdn.microsoft.com/library/ee301560)|Returns a rowset with one row and one column. |
|[DMSCHEMA_MINING_COLUMNS](https://msdn.microsoft.com/library/ee301664)|Describes the individual columns of all described data mining models that are deployed on the server.|  
|[DMSCHEMA_MINING_FUNCTIONS](https://msdn.microsoft.com/library/ee320415)|Describes the data mining functions that are supported by the data mining algorithms that are available on a server that is running Analysis Services.|  
|[DMSCHEMA_MINING_MODEL_CONTENT](https://msdn.microsoft.com/library/ee302124)|Enables the client application to browse the content of a trained data mining model.|  
|[DMSCHEMA_MINING_MODEL_CONTENT_PMML](https://msdn.microsoft.com/library/ee320692)|Returns the XML structure of the mining model. The format of the XML string follows the PMML 2.1 standard.|  
|[DMSCHEMA_MINING_MODEL_XML](https://msdn.microsoft.com/library/ee301916)|Returns the XML structure of the mining model. The format of the XML string follows the PMML 2.1 standard.|  
|[DMSCHEMA_MINING_MODELS](https://msdn.microsoft.com/library/ee320603)|Enumerates the data mining models that are deployed on the server.|  
|[DMSCHEMA_MINING_SERVICE_PARAMETERS](https://msdn.microsoft.com/library/ee320378)|Provides a list of parameters that can be used to configure the behavior of each data mining algorithm that is installed on the server.|  
|[DMSCHEMA_MINING_SERVICES](https://msdn.microsoft.com/library/ee320487)|Provides information about each data mining algorithm that the server supports.|  
|[DMSCHEMA_MINING_STRUCTURE_COLUMNS](https://msdn.microsoft.com/library/ee320277)|Describes the individual columns of all mining structures that are deployed on the server.|  
|[DMSCHEMA_MINING_STRUCTURES](https://msdn.microsoft.com/library/ee320704)|Enumerates information about the mining structures in the current catalog.|  
|[MDSCHEMA_ACTIONS](https://msdn.microsoft.com/library/ee320890)|Describes the actions that can be available to the client application.|
|[MDSCHEMA_CUBES](https://msdn.microsoft.com/library/ee301304)|Describes the structure of cubes within a database. Perspectives are also returned in this schema.|
|[MDSCHEMA_DIMENSIONS](https://msdn.microsoft.com/library/ee301366)|Describes the dimensions within a database.|  
|[MDSCHEMA_FUNCTIONS](https://msdn.microsoft.com/library/mt719467)|Returns information about the functions that are currently available for use in the DAX and MDX languages.|
|[MDSCHEMA_HIERARCHIES](https://msdn.microsoft.com/library/ee320250)|Describes each hierarchy within a particular dimension.|  
|[MDSCHEMA_INPUT_DATASOURCES](https://msdn.microsoft.com/library/ee301386)|Describes the data source objects described within the database.|  
|[MDSCHEMA_KPIS](https://msdn.microsoft.com/library/ee320406)|Describes the KPIs within a database.|  
|[MDSCHEMA_LEVELS](https://msdn.microsoft.com/library/ee320746)|Describes each level within a particular hierarchy.|  
|[MDSCHEMA_MEASUREGROUP_DIMENSIONS](https://msdn.microsoft.com/library/ee320977)|Enumerates the dimensions of measure groups.|  
|[MDSCHEMA_MEASUREGROUPS](https://msdn.microsoft.com/library/ee320601)|Describes the measure groups within a database.|  
|[MDSCHEMA_MEASURES](https://msdn.microsoft.com/library/ee301871)|Describes each measure.|  
|[MDSCHEMA_MEMBERS](https://msdn.microsoft.com/library/ee320960)|Describes the members within a database.|  
|[MDSCHEMA_PROPERTIES](https://msdn.microsoft.com/library/ee320393)|Describes the properties of members and cell properties.|  
|[MDSCHEMA_SETS](https://msdn.microsoft.com/library/ee301356)|Describes any sets that are currently described in a database, including session-scoped sets.|  

> [!NOTE]
> STORAGES DMVs do not have a schema rowset described in the protocol.