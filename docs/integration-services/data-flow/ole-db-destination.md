---
title: "OLE DB Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.oledbdest.f1"
  - "sql13.dts.designer.oledbdestadapter.connection.f1"
  - "sql13.dts.designer.oledbdestadapter.mappings.f1"
  - "sql13.dts.designer.oledbdestadapter.errorhandling.f1"
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

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The OLE DB destination loads data into a variety of OLE DB-compliant databases using a database table or view or an SQL command. For example, the OLE DB source can load data into tables in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Access and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
> [!NOTE]  
>  If the data source is [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Excel 2007, the data source requires a different connection manager than earlier versions of Excel. For more information, see [Connect to an Excel Workbook](../../integration-services/connection-manager/connect-to-an-excel-workbook.md).  
  
 The OLE DB destination provides five different data access modes for loading data:  
  
-   A table or view. You can specify an existing table or view, or you create a new table.  
  
-   A table or view using fast-load options. You can specify an existing table or create a new table.  
  
-   A table or view specified in a variable.  
  
-   A table or view specified in a variable using fast-load options.  
  
-   The results of an SQL statement.  
  
> [!NOTE]  
>  The OLE DB destination does not support parameters. If you need to execute a parameterized INSERT statement, consider the OLE DB Command transformation. For more information, see [OLE DB Command Transformation](../../integration-services/data-flow/transformations/ole-db-command-transformation.md).  
  
 When the OLE DB destination loads data that uses a double-byte character set (DBCS), the data may be corrupted if the data access mode does not use the fast load option and if the OLE DB connection manager uses the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SQLOLEDB). To ensure the integrity of DBCS data you should configure the OLE DB connection manager to use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, or use one of the fast-load access modes: **Table or view - fast load** or **Table name or view name variable - fast load**. Both options are available from the **OLE DB Destination Editor** dialog box. When programming the [!INCLUDE[ssIS](../../includes/ssis-md.md)] object model, you should set the AccessMode property to **OpenRowset Using FastLoad**, or **OpenRowset Using FastLoad From Variable**.  
  
> [!NOTE]  
>  If you use the **OLE DB Destination Editor** dialog box in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer to create the destination table into which the OLE DB destination inserts data, you may have to select the newly created table manually. The need for manual selection occurs when an OLE DB provider, such as the OLE DB provider for DB2, automatically adds schema identifiers to the table name.  
  
> [!NOTE]  
>  The CREATE TABLE statement that the **OLE DB Destination Editor** dialog box generates may require modification depending on the destination type. For example, some destinations do not support the data types that the CREATE TABLE statement uses.  
  
 This destination uses an OLE DB connection manager to connect to a data source and the connection manager specifies the OLE DB provider to use. For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
 An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project also provides the data source object from which you can create an OLE DB connection manager, to make data sources and data source views available to the OLE DB destination.  
  
 An OLE DB destination includes mappings between input columns and columns in the destination data source. You do not have to map input columns to all destination columns, but depending on the properties of the destination columns, errors can occur if no input columns are mapped to the destination columns. For example, if a destination column does not allow null values, an input column must be mapped to that column. In addition, the data types of mapped columns must be compatible. For example, you cannot map an input column with a string data type to a destination column with a numeric data type.  
  
 The OLE DB destination has one regular input and one error output.  
  
 For more information about data types, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Fast Load Options  
 If the OLE DB destination uses a fast-load data access mode, you can specify the following fast load options in the user interface, **OLE DB Destination Editor**, for the destination:  
  
-   Keep identity values from the imported data file or use unique values assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Retain a null value during the bulk load operation.  
  
-   Check constraints on the target table or view during the bulk import operation.  
  
-   Acquire a table-level lock for the duration of the bulk load operation.  
  
-   Specify the number of rows in the batch and the commit size.  
  
 Some fast load options are stored in specific properties of the OLE DB destination. For example, FastLoadKeepIdentity specifies whether to keep identify values, FastLoadKeepNulls specifies whether to keep null values, and FastLoadMaxInsertCommitSize specifies the number of rows to commit as a batch. Other fast load options are stored in a comma-separated list in the FastLoadOptions property. If the OLE DB destination uses all the fast load options that are stored in FastLoadOptions and listed in the **OLE DB Destination Editor** dialog box, the value of the property is set to **TABLOCK, CHECK_CONSTRAINTS, ROWS_PER_BATCH=1000**. The value 1000 indicates that the destination is configured to use batches of 1000 rows.  
  
> [!NOTE]  
>  Any constraint failure at the destination causes the entire batch of rows defined by FastLoadMaxInsertCommitSize to fail.  
  
 In addition to the fast load options exposed in the **OLE DB Destination Editor** dialog box,you can configure the OLE DB destination to use the following bulk load options by typing the options in FastLoadOptions property in the **Advanced Editor** dialog box.  
  
|Fast load option|Description|  
|----------------------|-----------------|  
|KILOBYTES_PER_BATCH|Specifies the size in kilobytes to insert. The option has the form **KILOBYTES_PER_BATCH** = \<positive integer value**>**.|  
|FIRE_TRIGGERS|Specifies whether triggers fire on the insert table. The option has the form **FIRE_TRIGGERS**. The presence of the option indicates that triggers fire.|  
|ORDER|Specifies how the input data is sorted. The option has the form ORDER \<column name> ASC&#124;DESC. Any number of columns may be listed and it is optional to include the sort order. If sort order is omitted, the insert operation assumes the data is unsorted.<br /><br /> Note: Performance can be improved if you use the ORDER option to sort the input data according to the clustered index on the table.|  
  
 The [!INCLUDE[tsql](../../includes/tsql-md.md)] keywords are traditionally typed using uppercase letters, but the keywords are not case sensitive.  
  
 To learn more about fast load options, see [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md).  
  
## Troubleshooting the OLE DB Destination  
 You can log the calls that the OLE DB destination makes to external data providers. You can use this logging capability to troubleshoot the saving of data to external data sources that the OLE DB destination performs. To log the calls that the OLE DB destination makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Configuring the OLE DB Destination  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [OLE DB Custom Properties](../../integration-services/data-flow/ole-db-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Load Data by Using the OLE DB Destination](../../integration-services/data-flow/load-data-by-using-the-ole-db-destination.md)  
  
-   [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## OLE DB Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **OLE DB Destination Editor** dialog box to select the OLE DB connection for the destination. This page also lets you select a table or view from the database.  
  
> [!NOTE]  
>  If the data source is [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Excel 2007, the data source requires a different connection manager than earlier versions of Excel. For more information, see [Connect to an Excel Workbook](../../integration-services/connection-manager/connect-to-an-excel-workbook.md).  
  
> [!NOTE]  
>  The **CommandTimeout** property of the OLE DB destination is not available in the **OLE DB Destination Editor**, but can be set by using the **Advanced Editor**. In addition, certain fast load options are available only in the **Advanced Editor**. For more information on these properties, see the OLE DB Destination section of [OLE DB Custom Properties](../../integration-services/data-flow/ole-db-custom-properties.md).  
  
### Static Options  
 **OLE DB connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Data access mode**  
 Specify the method for loading data into the destination. Loading double-byte character set (DBCS) data requires use of one of the fast load options. For more information about the fast load data access modes, which are optimized for bulk inserts, see [OLE DB Destination](../../integration-services/data-flow/ole-db-destination.md).  
  
|Option|Description|  
|------------|-----------------|  
|Table or view|Load data into a table or view in the OLE DB destination.|  
|Table or view - fast load|Load data into a table or view in the OLE DB destination and use the fast load option. For more information about the fast load data access modes, which are optimized for bulk inserts, see [OLE DB Destination](../../integration-services/data-flow/ole-db-destination.md).|  
|Table name or view name variable|Specify the table or view name in a variable.<br /><br /> **Related information**: [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)|  
|Table name or view name variable - fast load|Specify the table or view name in a variable, and use the fast load option to load the data. For more information about the fast load data access modes, which are optimized for bulk inserts, see [OLE DB Destination](../../integration-services/data-flow/ole-db-destination.md).|  
|SQL command|Load data into the OLE DB destination by using a SQL query.|  
  
 **Preview**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
### Data Access Mode Dynamic Options  
 Each of the settings for **Data access mode** displays a dynamic set of options specific to that setting. The following sections describe each of the dynamic options available for each **Data access mode** setting.  
  
#### Data access mode = Table or view  
 **Name of the table or the view**  
 Select the name of the table or view from a list of those available in the data source.  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
#### Data access mode = Table or view - fast load  
 **Name of the table or view**  
 Select a table or view from the database by using this list, or create a new table by clicking **New**.  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 **Keep identity**  
 Specify whether to copy identity values when data is loaded. This property is available only with the fast load option. The default value of this property is **false**.  
  
 **Keep nulls**  
 Specify whether to copy null values when data is loaded. This property is available only with the fast load option. The default value of this property is **false**.  
  
 **Table lock**  
 Specify whether the table is locked during the load. The default value of this property is **true**.  
  
 **Check constraints**  
 Specify whether the destination checks constraints when it loads data. The default value of this property is **true**.  
  
 **Rows per batch**  
 Specify the number of rows in a batch. The default value of this property is **-1**, which indicates that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **OLE DB Destination Editor** to indicate that you do not want to assign a custom value for this property.  
  
 **Maximum insert commit size**  
 Specify the batch size that the OLE DB destination tries to commit during fast load operations. The value of **0** indicates that all data is committed in a single batch after all rows have been processed.  
  
> [!NOTE]  
>  A value of **0** might cause the running package to stop responding if the OLE DB destination and another data flow component are updating the same source table. To prevent the package from stopping, set the **Maximum insert commit size** option to **2147483647**.  
  
 If you provide a value for this property, the destination commits rows in batches that are the smaller of (a) the **Maximum insert commit size**, or (b) the remaining rows in the buffer that is currently being processed.  
  
> [!NOTE]  
>  Any constraint failure at the destination causes the entire batch of rows defined by **Maximum insert commit size** to fail.  
  
#### Data access mode = Table name or view name variable  
 **Variable name**  
 Select the variable that contains the name of the table or view.  
  
#### Data Access Mode = Table name or view name variable - fast load)  
 **Variable name**  
 Select the variable that contains the name of the table or view.  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 **Keep identity**  
 Specify whether to copy identity values when data is loaded. This property is available only with the fast load option. The default value of this property is **false**.  
  
 **Keep nulls**  
 Specify whether to copy null values when data is loaded. This property is available only with the fast load option. The default value of this property is **false**.  
  
 **Table lock**  
 Specify whether the table is locked during the load. The default value of this property is **false**.  
  
 **Check constraints**  
 Specify whether the task checks constraints. The default value of this property is **false**.  
  
 **Rows per batch**  
 Specify the number of rows in a batch. The default value of this property is **-1**, which indicates that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **OLE DB Destination Editor** to indicate that you do not want to assign a custom value for this property.  
  
 **Maximum insert commit size**  
 Specify the batch size that the OLE DB destination tries to commit during fast load operations. The default value of **2147483647** indicates that all data is committed in a single batch after all rows have been processed.  
  
> [!NOTE]  
>  A value of **0** might cause the running package to stop responding if the OLE DB destination and another data flow component are updating the same source table. To prevent the package from stopping, set the **Maximum insert commit size** option to **2147483647**.  
  
#### Data access mode = SQL command  
 **SQL command text**  
 Enter the text of a SQL query, build the query by clicking **Build Query**, or locate the file that contains the query text by clicking **Browse**.  
  
> [!NOTE]  
>  The OLE DB destination does not support parameters. If you need to execute a parameterized INSERT statement, consider the OLE DB Command transformation. For more information, see [OLE DB Command Transformation](../../integration-services/data-flow/transformations/ole-db-command-transformation.md).  
  
 **Build query**  
 Use the **Query Builder** dialog box to construct the SQL query visually.  
  
 **Browse**  
 Use the **Open** dialog box to locate the file that contains the text of the SQL query.  
  
 **Parse query**  
 Verify the syntax of the query text.  
  
## OLE DB Destination Editor (Mappings Page)
  Use the **Mappings** page of the **OLE DB Destination Editor** dialog box to map input columns to destination columns.  
  
### Options  
 **Available Input Columns**  
 View the list of available input columns. Use a drag-and-drop operation to map available input columns in the table to destination columns.  
  
 **Available Destination Columns**  
 View the list of available destination columns. Use a drag-and-drop operation to map available destination columns in the table to input columns.  
  
 **Input Column**  
 View the input columns that you selected. You can remove mappings by selecting **\<ignore>** to exclude columns from the output.  
  
 **Destination Column**  
 View each available destination column, regardless of whether it is mapped or not.  
  
## OLE DB Destination Editor (Error Output Page)
  Use the **Error Output** page of the **OLE DB Destination Editor** dialog box to specify error handling options.  
  
### Options  
 **Input/Output**  
 View the name of the input.  
  
 **Column**  
 Not used.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Not used.  
  
 **Description**  
 View the description of the operation.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## Related Content  
 [OLE DB Source](../../integration-services/data-flow/ole-db-source.md)  
  
 [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md)  
  
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
  
