---
title: "OLE DB Schema Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "schema rowsets [OLE DB]"
  - "schema rowsets [Analysis Services], OLE DB"
  - "OLE DB schema rowsets"
  - "rowsets [Analysis Services], OLE DB"
ms.assetid: ca2ee87a-ba04-4501-9125-33934c58ab31
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# OLE DB Schema Rowsets
  The following OLE DB schema rowsets are supported by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider. Use the **DISCOVER_ENUMERATORS** rowset with the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method to check whether a particular data source provider supports a rowset.  
  
 You can also find detailed information about these rowsets by searching for the topic "Schema Rowsets" in the OLE DB Programmer's Reference portion of the MSDN® Library at the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Web site.  
  
 The following table describes this schema rowset.  
  
|Rowset|Description|  
|------------|-----------------|  
|**DBSCHEMA_ASSERTIONS**|Identifies the assertions that are defined in the catalog and owned by a given user.|  
|[DBSCHEMA_CATALOGS Rowset](../../../analysis-services/schema-rowsets/ole-db/dbschema-catalogs-rowset.md) <sup>1</sup>|Identifies the physical attributes associated with catalogs that are accessible from the database management system (DBMS). For some systems, such as [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Access, there may be only one catalog. For [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], this rowset enumerates all catalogs (databases) defined in the system database.|  
|**DBSCHEMA_CHARACTER_SETS**|Identifies the character sets that are defined in the catalog and accessible to a given user.|  
|**DBSCHEMA_CHECK_CONSTRAINTS**|Identifies the check constraints that are defined in the catalog and owned by a given user.|  
|**DBSCHEMA_CHECK_CONSTRAINTS_BY_TABLE**|Identifies the check constraints for a given table, defined in a catalog owned by a given user.|  
|**DBSCHEMA_COLLATIONS**|Identifies the character collations that are defined in the catalog and accessible to a given user.|  
|**DBSCHEMA_COLUMN_DOMAIN_USAGE**|Identifies the columns defined in the catalog that are dependent on a domain defined in the catalog and owned by a given user.|  
|**DBSCHEMA_COLUMN_PRIVILEGES**|Identifies the privileges on columns of tables that are defined in the catalog and are available to or granted by a given user.|  
|[DBSCHEMA_COLUMNS Rowset](../../../analysis-services/schema-rowsets/ole-db/dbschema-columns-rowset.md) <sup>1</sup>|Provides column information for all columns meeting the provided restriction criteria.|  
|**DBSCHEMA_CONSTRAINT_COLUMN_USAGE**|Identifies the columns used by referential constraints, unique constraints, check constraints, and assertions and that are defined in the catalog and owned by a given user.|  
|**DBSCHEMA_CONSTRAINT_TABLE_USAGE**|Identifies the tables that are used by referential constraints, unique constraints, check constraints, and assertions and that are defined in the catalog and owned by a given user.|  
|**DBSCHEMA_FOREIGN_KEYS**|Identifies the foreign key columns defined in the catalog by a given user. This schema rowset is built upon several ISO schema views as a convenience to the non-SQL programmer. If supported, this schema rowset must be synchronized with the related ISO views (**REFERENTIAL_CONSTRAINTS** and **CONSTRAINT_COLUMN_USAGE**).|  
|**DBSCHEMA_INDEXES**|Identifies the indexes that are defined in the catalog and owned by a given user.|  
|**DBSCHEMA_KEY_COLUMN_USAGE**|Identifies the columns that are defined in the catalog and are constrained as keys by a given user.|  
|**DBSCHEMA_PRIMARY_KEYS**|Identifies the primary key columns defined in the catalog by a given user. This schema rowset is built upon an ISO schema view as a convenience to the non-SQL programmer. If supported, this schema rowset must be synchronized with the related ISO view (**CONSTRAINT_COLUMN_USAGE**).|  
|**DBSCHEMA_PROCEDURE_COLUMNS**|Returns information about the columns of rowsets returned by procedures.|  
|**DBSCHEMA_PROCEDURE_PARAMETERS**|Returns information about the parameters and return codes of procedures.|  
|**DBSCHEMA_PROCEDURES**|Identifies the procedures that are defined in the catalog and owned by a given user. This is an OLE DB extension.|  
|[DBSCHEMA_PROVIDER_TYPES Rowset](../../../analysis-services/schema-rowsets/ole-db/dbschema-provider-types-rowset.md) <sup>1</sup>|Identifies the (base) data types supported by the data provider.|  
|**DBSCHEMA_REFERENTIAL_CONSTRAINTS**|Identifies the referential constraints that are defined in the catalog and owned by a given user.|  
|**DBSCHEMA_SCHEMATA**|Identifies the schemas that are owned by a given user.|  
|**DBSCHEMA_SQL_LANGUAGES**|Identifies the conformance levels, options, and dialects supported by the SQL implementation processing data defined in the catalog.|  
|**DBSCHEMA_STATISTICS**|Identifies the statistics that are defined in the catalog and owned by a given user.<br /><br /> This table is not related to the **TABLE_STATISTICS** rowset.|  
|**DBSCHEMA_TABLE_CONSTRAINTS**|Identifies the table constraints that are defined in the catalog and owned by a given user.|  
|**DBSCHEMA_TABLE_PRIVILEGES**|Identifies the privileges on tables that are defined in the catalog and available to or granted by a given user.|  
|**DBSCHEMA_TABLE_STATISTICS**|Describes the available set of statistics on tables in the provider.<br /><br /> This rowset is not related to the **STATISTICS** rowset.|  
|[DBSCHEMA_TABLES Rowset](../../../analysis-services/schema-rowsets/ole-db/dbschema-tables-rowset.md) <sup>1</sup>|Identifies the measure groups and dimensions exposed as tables within [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|**DBSCHEMA_TABLES_INFO** <sup>1</sup>|Identifies the tables (including views) that are defined in the catalog and accessible to a given user.|  
|**DBSCHEMA_TRANSLATIONS**|Identifies the character translations that are defined in the catalog and accessible to a given user.|  
|**DBSCHEMA_TRUSTEE**|Enumerates the trustees for a data source.|  
|**DBSCHEMA_USAGE_PRIVILEGES**|Identifies the **USAGE** privileges on objects that are defined in the catalog and are available to or granted by a given user.|  
|**DBSCHEMA_VIEW_COLUMN_USAGE**|Identifies the views that are defined in the catalog and accessible to a given user.|  
|**DBSCHEMA_VIEW_TABLE_USAGE**|Identifies the tables on which viewed tables, defined in the catalog and owned by a given user, are dependent.|  
|**DBSCHEMA_VIEWS**|Identifies the views that are defined in the catalog and accessible to a given user.|  
  
 <sup>1</sup> Indicates schema rowsets supported by the MSOLAP data source provider for the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XMLA provider.  
  
## See Also  
 [DISCOVER_ENUMERATORS Rowset](../../../analysis-services/schema-rowsets/xml/discover-enumerators-rowset.md)   
 [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)  
  
  