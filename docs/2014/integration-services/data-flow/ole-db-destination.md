---
title: "OLE DB Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.oledbdest.f1"
helpviewer_keywords: 
  - "fast-load data access mode [Integration Services]"
  - "OLE DB destination [Integration Services]"
  - "fast load options [Integration Services]"
  - "fast-load options [Integration Services]"
  - "destinations [Integration Services], OLE DB"
  - "fast load data access mode [Integration Services]"
  - "inserting data"
ms.assetid: 873a2fa0-2a02-41fc-a80a-ec9767f36a8a
author: janinezhang
ms.author: janinez
manager: craigg
---
# OLE DB Destination
  The OLE DB destination loads data into a variety of OLE DB-compliant databases using a database table or view or an SQL command. For example, the OLE DB source can load data into tables in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Access and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
 The OLE DB destination provides five different data access modes for loading data:  
  
-   A table or view. You can specify an existing table or view, or you create a new table.  
  
-   A table or view using fast-load options. You can specify an existing table or create a new table.  
  
-   A table or view specified in a variable.  
  
-   A table or view specified in a variable using fast-load options.  
  
-   The results of an SQL statement.  
  
> [!NOTE]  
>  The OLE DB destination does not support parameters. If you need to execute a parameterized INSERT statement, consider the OLE DB Command transformation. For more information, see [OLE DB Command Transformation](transformations/ole-db-command-transformation.md).  
  
 When the OLE DB destination loads data that uses a double-byte character set (DBCS), the data may be corrupted if the data access mode does not use the fast load option and if the OLE DB connection manager uses the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SQLOLEDB). To ensure the integrity of DBCS data you should configure the OLE DB connection manager to use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, or use one of the fast-load access modes: **Table or view - fast load** or **Table name or view name variable - fast load**. Both options are available from the **OLE DB Destination Editor** dialog box. When programming the [!INCLUDE[ssIS](../../includes/ssis-md.md)] object model, you should set the AccessMode property to `OpenRowset Using FastLoad`, or `OpenRowset Using FastLoad From Variable`.  
  
> [!NOTE]  
>  If you use the **OLE DB Destination Editor** dialog box in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer to create the destination table into which the OLE DB destination inserts data, you may have to select the newly created table manually. The need for manual selection occurs when an OLE DB provider, such as the OLE DB provider for DB2, automatically adds schema identifiers to the table name.  
  
> [!NOTE]  
>  The CREATE TABLE statement that the **OLE DB Destination Editor** dialog box generates may require modification depending on the destination type. For example, some destinations do not support the data types that the CREATE TABLE statement uses.  
  
 This destination uses an OLE DB connection manager to connect to a data source and the connection manager specifies the OLE DB provider to use. For more information, see [OLE DB Connection Manager](../connection-manager/ole-db-connection-manager.md).  
  
 An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project also provides the data source object from which you can create an OLE DB connection manager, to make data sources and data source views available to the OLE DB destination.  
  
 An OLE DB destination includes mappings between input columns and columns in the destination data source. You do not have to map input columns to all destination columns, but depending on the properties of the destination columns, errors can occur if no input columns are mapped to the destination columns. For example, if a destination column does not allow null values, an input column must be mapped to that column. In addition, the data types of mapped columns must be compatible. For example, you cannot map an input column with a string data type to a destination column with a numeric data type.  
  
 The OLE DB destination has one regular input and one error output.  
  
 For more information about data types, see [Integration Services Data Types](integration-services-data-types.md).  
  
## Fast Load Options  
 If the OLE DB destination uses a fast-load data access mode, you can specify the following fast load options in the user interface, **OLE DB Destination Editor**, for the destination:  
  
-   Keep identity values from the imported data file or use unique values assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Retain a null value during the bulk load operation.  
  
-   Check constraints on the target table or view during the bulk import operation.  
  
-   Acquire a table-level lock for the duration of the bulk load operation.  
  
-   Specify the number of rows in the batch and the commit size.  
  
 Some fast load options are stored in specific properties of the OLE DB destination. For example, FastLoadKeepIdentity specifies whether to keep identify values, FastLoadKeepNulls specifies whether to keep null values, and FastLoadMaxInsertCommitSize specifies the number of rows to commit as a batch. Other fast load options are stored in a comma-separated list in the FastLoadOptions property. If the OLE DB destination uses all the fast load options that are stored in FastLoadOptions and listed in the **OLE DB Destination Editor** dialog box, the value of the property is set to `TABLOCK, CHECK_CONSTRAINTS, ROWS_PER_BATCH=1000`. The value 1000 indicates that the destination is configured to use batches of 1000 rows.  
  
> [!NOTE]  
>  Any constraint failure at the destination causes the entire batch of rows defined by FastLoadMaxInsertCommitSize to fail.  
  
 In addition to the fast load options exposed in the **OLE DB Destination Editor** dialog box,you can configure the OLE DB destination to use the following bulk load options by typing the options in FastLoadOptions property in the **Advanced Editor** dialog box.  
  
|Fast load option|Description|  
|----------------------|-----------------|  
|KILOBYTES_PER_BATCH|Specifies the size in kilobytes to insert. The option has the form `KILOBYTES_PER_BATCH` = \<positive integer value**>**.|  
|FIRE_TRIGGERS|Specifies whether triggers fire on the insert table. The option has the form **FIRE_TRIGGERS**. The presence of the option indicates that triggers fire.|  
|ORDER|Specifies how the input data is sorted. The option has the form ORDER \<column name> ASC&#124;DESC. Any number of columns may be listed and it is optional to include the sort order. If sort order is omitted, the insert operation assumes the data is unsorted.<br /><br /> Note: Performance can be improved if you use the ORDER option to sort the input data according to the clustered index on the table.|  
  
 The [!INCLUDE[tsql](../../includes/tsql-md.md)] keywords are traditionally typed using uppercase letters, but the keywords are not case sensitive.  
  
 To learn more about fast load options, see [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql).  
  
## Troubleshooting the OLE DB Destination  
 You can log the calls that the OLE DB destination makes to external data providers. You can use this logging capability to troubleshoot the saving of data to external data sources that the OLE DB destination performs. To log the calls that the OLE DB destination makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Configuring the OLE DB Destination  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **OLE DB Destination Editor** dialog box, click one of the following topics:  
  
-   [OLE DB Destination Editor &#40;Connection Manager Page&#41;](../ole-db-destination-editor-connection-manager-page.md)  
  
-   [OLE DB Destination Editor &#40;Mappings Page&#41;](../ole-db-destination-editor-mappings-page.md)  
  
-   [OLE DB Destination Editor &#40;Error Output Page&#41;](../ole-db-destination-editor-error-output-page.md)  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../common-properties.md)  
  
-   [OLE DB Custom Properties](ole-db-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Load Data by Using the OLE DB Destination](ole-db-destination.md)  
  
-   [Set the Properties of a Data Flow Component](set-the-properties-of-a-data-flow-component.md)  
  
## Related Content  
 [OLE DB Source](ole-db-source.md)  
  
 [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md)  
  
 [Data Flow](data-flow.md)  
  
  
