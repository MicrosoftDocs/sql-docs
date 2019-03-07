---
title: "SchemaEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "SchemaEnum"
helpviewer_keywords: 
  - "SchemaEnum enumeration [ADO]"
ms.assetid: 21c97651-297f-469f-b5b5-c48af72b62a8
author: MightyPen
ms.author: genemi
manager: craigg
---
# SchemaEnum
Specifies the type of schema **Recordset** that the [OpenSchema](../../../ado/reference/ado-api/openschema-method.md) method retrieves.  
  
## Remarks  
 Additional information about the function and columns returned for each ADO constant can be found in topics in [Appendix B: Schema Rowsets](https://msdn.microsoft.com/2b5fbf03-e50d-44ee-bc57-5a57666c55f1) of the OLE DB Programmer's Reference. The name of each topic is listed in parentheses in the Description section of the following table.  
  
 Additional information about the function and columns returned for each ADO MD constant can be found in topics in [OLE DB for OLAP Objects and Schema Rowsets](https://msdn.microsoft.com/d20bb2a6-68bd-423f-9ec8-eb930cd0c144) in the OLE DB for Online Analytical Processing (OLAP) documentation. The name of each topic is listed in parentheses in the Description column of the following table.  
  
 You can translate the data types of columns in the OLE DB documentation to ADO data types by referring to the Description column of the ADO [DataTypeEnum](../../../ado/reference/ado-api/datatypeenum.md) topic. For example, an OLE DB data type of **DBTYPE_WSTR** is equivalent to an ADO data type of **adWChar**.  
  
 ADO generates schema-like results for the constants, **adSchemaDBInfoKeywords** and **adSchemaDBInfoLiterals**. ADO creates a **Recordset**, and then fills each row with the values returned respectively by the **IDBInfo::GetKeywords** and **IDBInfo::GetLiteralInfo** methods. Additional information about these methods can be found in the [IDBInfo](https://msdn.microsoft.com/3f5ad97f-3fc6-4f21-b691-f6911e4007f3) section of the OLE DB Programmer's Reference.  
  
|Constant|Value|Description|Constraint Columns|  
|--------------|-----------|-----------------|------------------------|  
|**adSchemaAsserts**|0|Returns the assertions defined in the catalog that are owned by a given user.<br /><br /> (ASSERTIONS Rowset)|CONSTRAINT_CATALOG CONSTRAINT_SCHEMA CONSTRAINT_NAME|  
|**adSchemaCatalogs**|1|Returns the physical attributes associated with catalogs accessible from the DBMS.<br /><br /> (CATALOGS Rowset)|CATALOG_NAME|  
|**adSchemaCharacterSets**|2|Returns the character sets defined in the catalog that are accessible to a given user.<br /><br /> (CHARACTER_SETS Rowset)|CHARACTER_SET_CATALOG CHARACTER_SET_SCHEMA CHARACTER_SET_NAME|  
|**adSchemaCheckConstraints**|5|Returns the check constraints defined in the catalog that are owned by a given user.<br /><br /> (CHECK_CONSTRAINTS) Rowset)|CONSTRAINT_CATALOG CONSTRAINT_SCHEMA CONSTRAINT_NAME|  
|**adSchemaCollations**|3|Returns the character collations defined in the catalog that are accessible to a given user.<br /><br /> (COLLATIONS Rowset)|COLLATION_CATALOG COLLATION_SCHEMA COLLATION_NAME|  
|**adSchemaColumnPrivileges**|13|Returns the privileges on columns of tables defined in the catalog that are available to, or granted by, a given user.<br /><br /> (COLUMN_PRIVILEGES Rowset)|TABLE_CATALOG TABLE_SCHEMA TABLE_NAME COLUMN_NAME GRANTOR GRANTEE|  
|**adSchemaColumns**|4|Returns the columns of tables (including views) defined in the catalog that are accessible to a given user.<br /><br /> (COLUMNS Rowset)|TABLE_CATALOG TABLE_SCHEMA TABLE_NAME COLUMN_NAME|  
|**adSchemaColumnsDomainUsage**|11|Returns the columns defined in the catalog that are dependent on a domain defined in the catalog and owned by a given user.<br /><br /> (COLUMN_DOMAIN_USAGE Rowset)|DOMAIN_CATALOG DOMAIN_SCHEMA DOMAIN_NAME COLUMN_NAME|  
|**adSchemaConstraintColumnUsage**|6|Returns the columns used by referential constraints, unique constraints, check constraints, and assertions, defined in the catalog and owned by a given user.<br /><br /> (CONSTRAINT_COLUMN_USAGE Rowset)|TABLE_CATALOG TABLE_SCHEMA TABLE_NAME COLUMN_NAME|  
|**adSchemaConstraintTableUsage**|7|Returns the tables that are used by referential constraints, unique constraints, check constraints, and assertions defined in the catalog and owned by a given user.<br /><br /> (CONSTRAINT_TABLE_USAGE Rowset)|TABLE_CATALOG TABLE_SCHEMA TABLE_NAME|  
|**adSchemaCubes**|32|Returns information about the available cubes in a schema (or the catalog, if the provider does not support schemas).<br /><br /> (CUBES Rowset*)|CATALOG_NAME SCHEMA_NAME CUBE_NAME|  
|**adSchemaDBInfoKeywords**|30|Returns a list of provider-specific keywords.<br /><br /> (IDBInfo::GetKeywords)|\<None>|  
|**adSchemaDBInfoLiterals**|31|Returns a list of provider-specific literals used in text commands.<br /><br /> (IDBInfo::GetLiteralInfo)|\<None>|  
|**adSchemaDimensions**|33|Returns information about the dimensions in a given cube. It has one row for each dimension.<br /><br /> (DIMENSIONS Rowset)|CATALOG_NAME SCHEMA_NAME CUBE_NAME DIMENSION_NAME DIMENSION_UNIQUE_NAME|  
|**adSchemaForeignKeys**|27|Returns the foreign key columns defined in the catalog by a given user.<br /><br /> (FOREIGN_KEYS Rowset)|PK_TABLE_CATALOG PK_TABLE_SCHEMA PK_TABLE_NAME FK_TABLE_CATALOG FK_TABLE_SCHEMA FK_TABLE_NAME|  
|**adSchemaHierarchies**|34|Returns information about the hierarchies available in a dimension.<br /><br /> (HIERARCHIES Rowset)|CATALOG_NAME SCHEMA_NAME CUBE_NAME DIMENSION_UNIQUE_NAME HIERARCHY_NAME HIERARCHY_UNIQUE_NAME|  
|**adSchemaIndexes**|12|Returns the indexes defined in the catalog that are owned by a given user.<br /><br /> (INDEXES Rowset)|TABLE_CATALOG TABLE_SCHEMA INDEX_NAME TYPE TABLE_NAME|  
|**adSchemaKeyColumnUsage**|8|Returns the columns defined in the catalog that are constrained as keys by a given user.<br /><br /> (KEY_COLUMN_USAGE Rowset)|CONSTRAINT_CATALOG CONSTRAINT_SCHEMA CONSTRAINT_NAME TABLE_CATALOG TABLE_SCHEMA TABLE_NAME COLUMN_NAME|  
|**adSchemaLevels**|35|Returns information about the levels available in a dimension.<br /><br /> (LEVELS Rowset)|CATALOG_NAME SCHEMA_NAME CUBE_NAME DIMENSION_UNIQUE_NAME HIERARCHY_UNIQUE_NAME LEVEL_NAME LEVEL_UNIQUE_NAME|  
|**adSchemaMeasures**|36|Returns information about the available measures.<br /><br /> (MEASURES Rowset)|CATALOG_NAME SCHEMA_NAME CUBE_NAME MEASURE_NAME MEASURE_UNIQUE_NAME|  
|**adSchemaMembers**|38|Returns information about the available members.<br /><br /> (MEMBERS Rowset)|CATALOG_NAME SCHEMA_NAME CUBE_NAME DIMENSION_UNIQUE_NAME HIERARCHY_UNIQUE_NAME LEVEL_UNIQUE_NAME LEVEL_NUMBER MEMBER_NAME MEMBER_UNIQUE_NAME MEMBER_CAPTION MEMBER_TYPE Tree operator. For more information, see OLE DB for Online Analytical Processing (OLAP).|  
|**adSchemaPrimaryKeys**|28|Returns the primary key columns defined in the catalog by a given user.<br /><br /> (PRIMARY_KEYS Rowset)|PK_TABLE_CATALOG PK_TABLE_SCHEMA PK_TABLE_NAME|  
|**adSchemaProcedureColumns**|29|Returns information about the columns of rowsets returned by procedures.<br /><br /> (PROCEDURE_COLUMNS Rowset)|PROCEDURE_CATALOG PROCEDURE_SCHEMA PROCEDURE_NAME COLUMN_NAME|  
|**adSchemaProcedureParameters**|26|Returns information about the parameters and return codes of procedures.<br /><br /> (PROCEDURE_PARAMETERS Rowset)|PROCEDURE_CATALOG PROCEDURE_SCHEMA PROCEDURE_NAME PARAMETER_NAME|  
|**adSchemaProcedures**|16|Returns the procedures defined in the catalog that are owned by a given user.<br /><br /> (PROCEDURES Rowset)|PROCEDURE_CATALOG PROCEDURE_SCHEMA PROCEDURE_NAME PROCEDURE_TYPE|  
|**adSchemaProperties**|37|Returns information about the available properties for each level of the dimension.<br /><br /> (PROPERTIES Rowset)|CATALOG_NAME SCHEMA_NAME CUBE_NAME DIMENSION_UNIQUE_NAME HIERARCHY_UNIQUE_NAME LEVEL_UNIQUE_NAME MEMBER_UNIQUE_NAME PROPERTY_TYPE PROPERTY_NAME|  
|**adSchemaProviderSpecific**|-1|Used if the provider defines its own nonstandard schema queries.|\<Provider specific>|  
|**adSchemaProviderTypes**|22|Returns the (base) data types supported by the data provider.<br /><br /> (PROVIDER_TYPES Rowset)|DATA_TYPE BEST_MATCH|  
|**AdSchemaReferentialConstraints**|9|Returns the referential constraints defined in the catalog that are owned by a given user.<br /><br /> (REFERENTIAL_CONSTRAINTS Rowset)|CONSTRAINT_CATALOG CONSTRAINT_SCHEMA CONSTRAINT_NAME|  
|**adSchemaSchemata**|17|Returns the schemas (database objects) that are owned by a given user.<br /><br /> (SCHEMATA Rowset)|CATALOG_NAME SCHEMA_NAME SCHEMA_OWNER|  
|**adSchemaSQLLanguages**|18|Returns the conformance levels, options, and dialects supported by the SQL-implementation processing data defined in the catalog.<br /><br /> (SQL_LANGUAGES Rowset)|\<None>|  
|**adSchemaStatistics**|19|Returns the statistics defined in the catalog that are owned by a given user.<br /><br /> (STATISTICS Rowset)|TABLE_CATALOG TABLE_SCHEMA TABLE_NAME|  
|**adSchemaTableConstraints**|10|Returns the table constraints defined in the catalog that are owned by a given user.<br /><br /> (TABLE_CONSTRAINTS Rowset)|CONSTRAINT_CATALOG CONSTRAINT_SCHEMA CONSTRAINT_NAME TABLE_CATALOG TABLE_SCHEMA TABLE_NAME CONSTRAINT_TYPE|  
|**adSchemaTablePrivileges**|14|Returns the privileges on tables defined in the catalog that are available to, or granted by, a given user.<br /><br /> (TABLE_PRIVILEGES Rowset)|TABLE_CATALOG TABLE_SCHEMA TABLE_NAME GRANTOR GRANTEE|  
|**adSchemaTables**|20|Returns the tables (including views) defined in the catalog that are accessible to a given user.<br /><br /> (TABLES Rowset)|TABLE_CATALOG TABLE_SCHEMA TABLE_NAME TABLE_TYPE|  
|**adSchemaTranslations**|21|Returns the character translations defined in the catalog that are accessible to a given user.<br /><br /> (TRANSLATIONS Rowset)|TRANSLATION_CATALOG TRANSLATION_SCHEMA TRANSLATION_NAME|  
|**adSchemaTrustees**|39|Reserved for future use.||  
|**adSchemaUsagePrivileges**|15|Returns the USAGE privileges on objects defined in the catalog that are available to, or granted by, a given user.<br /><br /> (USAGE_PRIVILEGES Rowset)|OBJECT_CATALOG OBJECT_SCHEMA OBJECT_NAME OBJECT_TYPE GRANTOR GRANTEE|  
|**adSchemaViewColumnUsage**|24|Returns the columns on which viewed tables, defined in the catalog and owned by a given user, are dependent.<br /><br /> (VIEW_COLUMN_USAGE Rowset)|VIEW_CATALOG VIEW_SCHEMA VIEW_NAME|  
|**adSchemaViews**|23|Returns the views defined in the catalog that are accessible to a given user.<br /><br /> (VIEWS Rowset)|TABLE_CATALOG TABLE_SCHEMA TABLE_NAME|  
|**adSchemaViewTableUsage**|25|Returns the tables on which viewed tables, defined in the catalog and owned by a given user, are dependent.<br /><br /> (VIEW_TABLE_USAGE Rowset)|VIEW_CATALOG VIEW_SCHEMA VIEW_NAME|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.Schema.ASSERTS|  
|AdoEnums.Schema.CATALOGS|  
|AdoEnums.Schema.CHARACTERSETS|  
|AdoEnums.Schema.CHECKCONSTRAINTS|  
|AdoEnums.Schema.COLLATIONS|  
|AdoEnums.Schema.COLUMNPRIVILEGES|  
|AdoEnums.Schema.COLUMNS|  
|AdoEnums.Schema.COLUMNSDOMAINUSAGE|  
|AdoEnums.Schema.CONSTRAINTCOLUMNUSAGE|  
|AdoEnums.Schema.CONSTRAINTTABLEUSAGE|  
|AdoEnums.Schema.CUBES|  
|AdoEnums.Schema.DBINFOKEYWORDS|  
|AdoEnums.Schema.DBINFOLITERALS|  
|AdoEnums.Schema.DIMENSIONS|  
|AdoEnums.Schema.FOREIGNKEYS|  
|AdoEnums.Schema.HIERARCHIES|  
|AdoEnums.Schema.INDEXES|  
|AdoEnums.Schema.KEYCOLUMNUSAGE|  
|AdoEnums.Schema.LEVELS|  
|AdoEnums.Schema.MEASURES|  
|AdoEnums.Schema.MEMBERS|  
|AdoEnums.Schema.PRIMARYKEYS|  
|AdoEnums.Schema.PROCEDURECOLUMNS|  
|AdoEnums.Schema.PROCEDUREPARAMETERS|  
|AdoEnums.Schema.PROCEDURES|  
|AdoEnums.Schema.PROPERTIES|  
|AdoEnums.Schema.PROVIDERSPECIFIC|  
|AdoEnums.Schema.PROVIDERTYPES|  
|AdoEnums.Schema.REFERENTIALCONTRAINTS|  
|AdoEnums.Schema.SCHEMATA|  
|AdoEnums.Schema.SQLLANGUAGES|  
|AdoEnums.Schema.STATISTICS|  
|AdoEnums.Schema.TABLECONSTRAINTS|  
|AdoEnums.Schema.TABLEPRIVILEGES|  
|AdoEnums.Schema.TABLES|  
|AdoEnums.Schema.TRANSLATIONS|  
|AdoEnums.Schema.TRUSTEES|  
|AdoEnums.Schema.USAGEPRIVILEGES|  
|AdoEnums.Schema.VIEWCOLUMNUSAGE|  
|AdoEnums.Schema.VIEWS|  
|AdoEnums.Schema.VIEWTABLEUSAGE|  
  
## Applies To  
 [OpenSchema Method](../../../ado/reference/ado-api/openschema-method.md)
