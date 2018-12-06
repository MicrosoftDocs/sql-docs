---
title: "Incompatible Access Features (AccessToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Access databases"
  - "Access databases, features incompatible with SQL Azure"
  - "Access databases, features incompatible with SQL Server"
  - "dates"
  - "default expressions"
  - "foreign keys"
  - "hyperlink columns"
  - "incompatible Access features"
  - "indexes"
  - "indexes, length of"
  - "keywords"
  - "primary keys"
  - "reference, incompatible features"
  - "replication columns"
  - "special characters"
  - "unique indexes"
  - "validation rules"
ms.assetid: 99d45b9c-e3b9-4d56-8c25-b594b887ace1
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Incompatible Access Features (AccessToSQL)
Not all Access database features are compatible with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For example, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Access have different sets of reserved keywords. Issues such as these can prevent a successful migration to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use the following table to learn about possible migration issues and what you can do about them.  
  
## Database Settings or Features That Might Affect Migration  
  
|Access Database Setting or Feature|Migration Issue|  
|--------------------------------------|-------------------|  
|Access tables do not have unique indexes.|If a table that does not have a unique index is migrated to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you cannot modify the table after migration. This can lead to application compatibility problems.<br /><br />When you convert Access database objects, the Output window will list any Access tables that do not have unique indexes.<br /><br />You can configure Access to add a primary key on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table during conversion. For more information, see [Project Settings (Conversion)](https://msdn.microsoft.com/bcebc635-c638-4ddb-924c-b9ccfef86388).|  
|Access tables have replication columns.|If an Access table that includes replication system columns is migrated to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Jet replication functionality will be broken after migration.<br /><br />After migration, consider using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication to maintain synchronized copies of your databases.|  
|Access tables that have unique indexes contain multiple null values.|Access tables that have unique indexes with multiple null values cannot be transferred to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], because in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], unique indexes disallow multiple nulls. Migration will fail for these tables.<br /><br />SSMA will flag this issue in assessment reports. To create an assessment report, see [Assessing Access Database Objects for Conversion](assessing-access-database-objects-for-conversion-accesstosql.md).<br /><br />If this problem exists, you must make sure that the primary key does not have duplicate null values. Or, you must remove the primary key or unique indexes that contain multiple null values.|  
|Access tables contain date values that are out of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] range.|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **datetime** type accepts dates in the range of 1 Jan 1753 to 31 Dec 9999 only. Access accepts dates in the range of 1 Jan 100 to 31 Dec 9999.<br /><br />SSMA will flag this issue in assessment reports. To create an assessment report, see [Assessing Access Database Objects for Conversion](assessing-access-database-objects-for-conversion-accesstosql.md).<br /><br />You can configure how SSMA resolves dates that are out of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] range. For more information, see [Project Settings (Migration)](https://msdn.microsoft.com/4caebc9c-8680-4b99-a8fa-89c43161c95d).|  
|Index lengths in Access exceed 900 bytes.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] indexes have a 900-byte limit for the total size of index key columns. If your Access tables use larger indexes, SSMA will display a warning.<br /><br />If you continue with data migration, the migration might fail.|  
|Access object names are [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] keywords, or contain special characters.|Access and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] have different sets of reserved keywords and special characters. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will accept objects that are named by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] keywords or that contain special characters if you use bracketed or quoted identifiers, such as "select" or [select].p. For more information, see "Delimited Identifiers (Database Engine)" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.<br /><br />**NOTE:** To use quotation marks to delimit identifiers, SET QUOTED_IDENTIFIER must be ON.<br /><br />For example, `CREATE TABLE [schema](c1 [FOR])` is a valid statement, even though **schema** and **FOR** are reserved keywords. Also, `CREATE TABLE [xxx*yyy](c1 x&y)` is a valid statement, even though the table and column name contain the special characters **\&#42;** and **&amp;**.<br /><br />All queries that reference those objects must also use the names with brackets or quotation marks. For example, the query `SELECT * FROM schema` will fail. The correct query is: `SELECT * FROM [schema]`.<br /><br />When you convert Access database objects, the Output pane will list any Access tables that use keywords or special characters. You can modify the tables in Access, and then remove and add the database again; or you can modify queries that reference those objects so that the queries use brackets or quotation marks to delimit identifiers. If you do not modify your queries, your Access applications might return errors or have other problems.|  
|Field sizes differ in primary key/foreign key relationships.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support the Jet functionality of linking columns that have different data types or sizes with foreign key constraints.<br /><br />When you convert Access database objects, the Output window will list any primary key/foreign key constraints that will not be converted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can alter data types and sizes on Access columns so that they match, and then remove and re-add the Access database. Or, you can migrate data although these constraints will not be created in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|Referenced tables in Access relationships have neither a primary key nor a unique index.|Access accepts relationship between tables where the referenced table does not have a primary key or a unique index. However, this is not supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />When you convert Access database objects, the Output window will list any tables that have relationships but no primary key or unique index. You can alter the tables to add primary keys or unique indexes, and then remove and re-add the Access database. Or, you can migrate data although the relationship between the tables will be broken.|  
|Access tables have hyperlink columns.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support **hyperlink** columns. Instead, the columns are treated like Access memo columns. By default, these columns will be converted to **nvarchar(max)** columns in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can customize the mapping. For more information, see [Mapping Source and Target Data Types](mapping-source-and-target-data-types-accesstosql.md).|  
|Default or validation rule expressions contain Access functions that cannot be converted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure.|Access default expressions or validation rules might include Access system functions or user-defined functions that do not map to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. Using functions that do not map to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure will prevent you from loading the default expressions or validation rules into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure.|  
  
## See Also  
[Preparing Access Databases for Migration](preparing-access-databases-for-migration-accesstosql.md)  
[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)  
  
