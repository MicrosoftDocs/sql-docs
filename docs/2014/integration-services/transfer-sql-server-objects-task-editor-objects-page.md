---
title: "Transfer SQL Server Objects Task Editor (Objects Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transfersqlserverobjects.objects.f1"
helpviewer_keywords: 
  - "Transfer SQL Server Objects Task Editor"
ms.assetid: 8cc09118-70ac-4013-8308-d87f8411ca0c
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Transfer SQL Server Objects Task Editor (Objects Page)
  Use the **Objects** page of the **Transfer SQL Server Objects Task Editor** dialog box to specify properties for copying one or more [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects from one instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to another. Tables, views, stored procedures, and user-defined functions are a few examples of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects that you can copy. For more information about this task, see [Transfer SQL Server Objects Task](control-flow/transfer-sql-server-objects-task.md).  
  
> [!NOTE]  
>  The user who creates the Transfer [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Objects task must have sufficient permissions on the source server objects to select them for copying, and permission to access the destination server database where the objects will be transferred.  
  
## Static Options  
 **SourceConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the source server.  
  
 **SourceDatabase**  
 Select a database on the source server from which objects will be copied.  
  
 **DestinationConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the destination server.  
  
 **DestinationDatabase**  
 Select a database on the destination server to which objects will be copied.  
  
 **DropObjectsFirst**  
 Select whether the selected objects will be dropped first on the destination server before copying.  
  
 **IncludeExtendedProperties**  
 Select whether extended properties will be included when objects are copied from the source to the destination server.  
  
 **CopyData**  
 Select whether data will be included when objects are copied from the source to the destination server.  
  
 **ExistingData**  
 Specify how data will be copied to the destination server. This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**Replace**|Data on the destination server will be overwritten.|  
|**Append**|Data copied from the source server will be appended to existing data on the destination server.|  
  
> [!NOTE]  
>  The **ExistingData** option is only available when **CopyData** is set to **True**.  
  
 **CopySchema**  
 Select whether the schema is copied during the Transfer [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Objects task.  
  
> [!NOTE]  
>  **CopySchema** is only available for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **UseCollation**  
 Select whether the transfer of objects should include the collation specified on the source server.  
  
 **IncludeDependentObjects**  
 Select whether copying the selected objects will cascade to include other objects that depend on the objects selected for copying.  
  
 **CopyAllObjects**  
 Select whether the task will copy all objects in the specified source database or only selected objects.  Setting this option to False gives you the option to select the objects to transfer, and displays the dynamic options in the section, **CopyAllObjects**.  
  
 **ObjectsToCopy**  
 Expand **ObjectsToCopy** to specify which objects should be copied from the source database to the destination database.  
  
> [!NOTE]  
>  **ObjectsToCopy** is only available when **CopyAllObjects** is set to **False**.  
  
 The options to copy the following types of objects are supported only on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
 Assemblies  
  
 Partition functions  
  
 Partition schemes  
  
 Schemas  
  
 User-defined aggregates  
  
 User-defined types  
  
 XML schema collections  
  
 **CopyDatabaseUsers**  
 Specify whether database users should be included in the transfer.  
  
 **CopyDatabaseRoles**  
 Specify whether database roles should be included in the transfer.  
  
 **CopySqlServerLogins**  
 Specify whether [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] logins should be included in the transfer.  
  
 **CopyObjectLevelPermissions**  
 Specify whether object-level permissions should be included in the transfer.  
  
 **CopyIndexes**  
 Specify whether indexes should be included in the transfer.  
  
 **CopyTriggers**  
 Specify whether triggers should be included in the transfer.  
  
 **CopyFullTextIndexes**  
 Specify whether full-text indexes should be included in the transfer.  
  
 **CopyPrimaryKeys**  
 Specify whether primary keys should be included in the transfer.  
  
 **CopyForeignKeys**  
 Specify whether foreign keys should be included in the transfer.  
  
 **GenerateScriptsInUnicode**  
 Specify whether the generated transfer scripts are in Unicode format.  
  
## Dynamic Options  
  
### CopyAllObjects = False  
 **CopyAllTables**  
 Select whether the task will copy all tables in the specified source database or only the selected tables.  
  
 **TablesList**  
 Click to open the **Select Tables** dialog box.  
  
 **CopyAllViews**  
 Select whether the task will copy all views in the specified source database or only the selected views.  
  
 **ViewsList**  
 Click to open the **Select Views** dialog box.  
  
 **CopyAllStoredProcedures**  
 Select whether the task will copy all user-defined stored procedures in the specified source database or only the selected procedures.  
  
 **StoredProceduresList**  
 Click to open the **Select Stored Procedures** dialog box.  
  
 **CopyAllUserDefinedFunctions**  
 Select whether the task will copy all user-defined functions in the specified source database or only the selected UDFs.  
  
 **UserDefinedFunctionsList**  
 Click to open the **Select User Defined Functions** dialog box.  
  
 **CopyAllDefaults**  
 Select whether the task will copy all defaults in the specified source database or only the selected defaults.  
  
 **DefaultsList**  
 Click to open the **Select Defaults** dialog box.  
  
 **CopyAllUserDefinedDataTypes**  
 Select whether the task will copy all user-defined data types in the specified source database or only the selected user-defined data types.  
  
 **UserDefinedDataTypesList**  
 Click to open the **Select User-Defined Data Types** dialog box.  
  
 **CopyAllPartitionFunctions**  
 Select whether the task will copy all user-defined partition functions in the specified source database or only the selected partition functions. Supported only on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **PartitionFunctionsList**  
 Click to open the **Select Partition Functions** dialog box.  
  
 **CopyAllPartitionSchemes**  
 Select whether the task will copy all partition schemes in the specified source database or only the selected partition schemes. Supported only on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **PartitionSchemesList**  
 Click to open the **Select Partition Schemes** dialog box.  
  
 **CopyAllSchemas**  
 Select whether the task will copy all schemas in the specified source database or only the selected schemas. Supported only on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **SchemasList**  
 Click to open the **Select Schemas** dialog box.  
  
 **CopyAllSqlAssemblies**  
 Select whether the task will copy all SQL assemblies in the specified source database or only the selected SQL assemblies. Supported only on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **SqlAssembliesList**  
 Click to open the **Select SQL Assemblies** dialog box.  
  
 **CopyAllUserDefinedAggregates**  
 Select whether the task will copy all user-defined aggregates in the specified source database or only the selected user-defined aggregates. Supported only on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **UserDefinedAggregatesList**  
 Click to open the **Select User-Defined Aggregates** dialog box.  
  
 **CopyAllUserDefinedTypes**  
 Select whether the task will copy all user-defined types in the specified source database or only the selected UDTs. Supported only on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **UserDefinedTypes**  
 Click to open the **Select User-Defined Types** dialog box.  
  
 **CopyAllXmlSchemaCollections**  
 Select whether the task will copy all XML Schema collections in the specified source database or only the selected XML schema collections. Supported only on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **XmlSchemaCollectionsList**  
 Click to open the **Select XML Schema Collections** dialog box.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Integration Services Tasks](control-flow/integration-services-tasks.md)   
 [Transfer SQL Server Objects Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [Data Formats for Bulk Import or Bulk Export &#40;SQL Server&#41;](../relational-databases/import-export/data-formats-for-bulk-import-or-bulk-export-sql-server.md)   
 [Security Considerations for a SQL Server Installation](../../2014/sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
  
