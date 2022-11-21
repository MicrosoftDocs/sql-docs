---
description: "Mapping Sybase ASE Schemas to SQL Server Schemas (SybaseToSQL)"
title: "Mapping Sybase ASE Schemas to SQL Server Schemas (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Schema Mapping"
ms.assetid: 2c927003-c49d-4fe1-8e3e-5b2899166268
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
  - "ssma.sybase.schemamappingpanel.f1"

---
# Mapping Sybase ASE Schemas to SQL Server Schemas (SybaseToSQL)
In Sybase Adaptive Server Enterprise (ASE), each database has one or more schemas. By default, SSMA migrates all objects within a database and schema to the same database and schema in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. However, you can customize the mapping between ASE and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database.  
  
## ASE and SQL Server or SQL Azure Schemas  
ASE and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure both specify databases and their schemas by using two part notation as *database.schema*. For example, in an ASE **demo** database, there might be a **dbo** schema. That database and schema pair are specified as **demo.dbo**. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure has the same database and schema, the pair is also specified as **demo.dbo**.  
  
## Modifying the Target Database and Schema  
In SSMA, you can map an ASE schema to any available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure schema.  
  
**To modify the database and schema**  
  
1.  In Sybase Metadata Explorer, select **Databases**.  
  
    The **Schema Mapping** tab is also available when you select an individual database, the **Schemas** folder, or individual schemas. The list in the **Schema Mapping** tab is customized for the selected object.  
  
2.  In the right pane, click the **Schema Mapping** tab.  
  
    You will see a list of all ASE databases with their schemas, followed by a target value. This target is denoted in a two part notation (*database.schema*) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure where your objects and data will be migrated.  
  
3.  Select the row that contains the mapping that you want to change, and then click **Modify**.  
  
4.  In the **Choose Target Schema** dialog box, you may browse for available target database and schema or type the database and schema name in the textbox in a two part notation (database.schema) and then click **OK**.  
  
5.  The target changes on the **Schema Mapping** tab.  
  
**Modes of Mapping**  
  
-   Mapping to SQL Server  
  
You can map source database to any target database. By default source database is mapped to target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database with which you have connected using SSMA. If the target database being mapped is non-existing on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], then you will be prompted with a message **"The Database and/or schema does not exist in target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] metadata. It would be created during synchronization. Do you wish to continue?"** Click Yes. Similarly, you can map schema to non-existing schema under target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database which will be created during synchronization.  
  
-   Mapping to SQL Azure  
  
You can map the source database to the connected target Azure SQL Database or to any schema in the connected target Azure SQL Database. If you map source Schema to any non-existing schema under connected target database, then you will be prompted with a message **"Schema does not exist in target metadata. It would be created during synchronization. Do you wish to continue? "** Click Yes.  
  
## Reverting to the Default Database and Schema  
If you customize the mapping between an ASE schema and a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure schema, you can revert the mapping back to the default values.  
  
**To revert to the default database and schema**  
  
1.  Under the schema mapping tab, select any row and click **Reset to Default** to revert to the default database and schema.  
  
## Next Steps  
If you want to analyze the conversion of Sybase ASE objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure objects, you can [Create a conversion report](assessing-sybase-ase-database-objects-for-conversion-sybasetosql.md). Otherwise you can [Convert the ASE database object definitions](converting-sybase-ase-database-objects-sybasetosql.md) into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure object definitions.  
  
## See Also  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
  
