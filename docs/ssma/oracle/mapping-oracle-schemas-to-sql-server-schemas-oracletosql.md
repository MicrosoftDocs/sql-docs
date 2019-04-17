---
title: "Mapping Oracle Schemas to SQL Server Schemas (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 0edeaa08-9c5d-4e3a-bc15-b9a1f0c8a9dc
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Mapping Oracle Schemas to SQL Server Schemas (OracleToSQL)
In Oracle, each database has one or more schemas. By default, SSMA migrates all objects in an Oracle schema to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database named for the schema. However, you can customize the mapping between Oracle schemas and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
## Oracle and SQL Server Schemas  
An Oracle database contains schemas. An instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] contains multiple databases, each of which can have multiple schemas.  
  
The Oracle concept of a schema maps to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] concept of a database and one of its schemas. For example, Oracle might have a schema named **HR**. An instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might have a database named **HR**, and within that database are schemas. One schema is the **dbo** (or database owner) schema. By default, the Oracle schema **HR** will be mapped to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and schema **HR.dbo**. SSMA refers to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] combination of database and schema as a schema.  
  
You can modify the mapping between Oracle and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schemas.  
  
## Modifying the Target Database and Schema  
In SSMA, you can map an Oracle schema to any available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schema.  
  
**To modify the database and schema**  
  
1.  In Oracle Metadata Explorer, select **Schemas**.  
  
    The **Schema Mapping** tab is also available when you select an individual database, the **Schemas** folder, or individual schemas. The list in the **Schema Mapping** tab is customized for the selected object.  
  
2.  In the right pane, click the **Schema Mapping** tab.  
  
    You will see a list of all Oracle schemas, followed by a target value. This target is denoted in a two part notation (*database.schema*) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where your objects and data will be migrated.  
  
3.  Select the row that contains the mapping that you want to change, and then click **Modify**.  
  
    In the **Choose Target Schema** dialog box, you may browse for available target database and schema or type the database and schema name in the textbox in a two part notation (database.schema) and then click **OK**.  
  
4.  The target changes on the **Schema Mapping** tab.  
  
**Modes of Mapping**  
  
-   Mapping to SQL Server  
  
You can map source database to any target database. By default source database is mapped to target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database with which you have connected using SSMA. If the target database being mapped is non-existing on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], then you will be prompted with a message **"The Database and/or schema does not exist in target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] metadata. It would be created during synchronization. Do you wish to continue?"** Click Yes. Similarly, you can map schema to non-existing schema under target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database which will be created during synchronization.  
  
## Reverting to the Default Database and Schema  
If you customize the mapping between an Oracle schema and a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schema, you can revert the mapping back to the default values.  
  
**To revert to the default database and schema**  
  
1.  Under the schema mapping tab, select any row and click **Reset to Default** to revert to the default database and schema.  
  
## Next Steps  
If you want to analyze the conversion of Oracle objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects, you can [Create a conversion report](assessing-oracle-schemas-for-conversion-oracletosql.md). Otherwise you can [Convert the Oracle database object definitions](converting-oracle-schemas-oracletosql.md) into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object definitions.  
  
## See Also  
[Connecting to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/connecting-to-sql-server-oracletosql.md)  
[Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)  
  
