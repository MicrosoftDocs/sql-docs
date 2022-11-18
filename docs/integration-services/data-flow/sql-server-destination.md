---
description: "SQL Server Destination"
title: "SQL Server Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sqlserverdest.f1"
  - "sql13.dts.designer.sqlserverdestadapter.connection.f1"
  - "sql13.dts.designer.sqlserverdestadapter.mappings.f1"
  - "sql13.dts.designer.sqlserverdestadapter.advanced.f1"
helpviewer_keywords: 
  - "SQL Server destination"
  - "loading data"
  - "destinations [Integration Services], SQL Server"
  - "inserting data"
  - "bulk load [Integration Services]"
ms.assetid: a0227cd8-6944-4547-87e8-7b2507e26442
author: chugugrace
ms.author: chugu
---
# SQL Server Destination

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The SQL Server destination connects to a local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and bulk loads data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables and views. You cannot use the SQL Server destination in packages that access a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database on a remote server. Instead, the packages should use the OLE DB destination. For more information, see [OLE DB Destination](../../integration-services/data-flow/ole-db-destination.md).  
  
## Permissions  
 Users who execute packages that include the SQL Server destination require the "Create global objects" permission. You can grant this permission to users by using the Local Security Policy tool opened from the **Administrative Tools** menu. If you receive an error message when executing a package that uses the SQL Server destination, make sure that the account running the package has the "Create global objects" permission.  
  
## Bulk Inserts  
 If you attempt to use the SQL Server destination to bulk load data into a remote SQL Server database, you may see an error message similar to the following: "An OLE DB record is available. Source: "Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client" Hresult: 0x80040E14 Description: "Could not bulk load because SSIS file mapping object 'Global\DTSQLIMPORT ' could not be opened. Operating system error code 2 (The system cannot find the file specified.). Make sure you are accessing a local server via Windows security.""  
  
 The SQL Server destination offers the same high-speed insertion of data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that the Bulk Insert task provides; however, by using the SQL Server destination, a package can apply transformations to column data before the data is loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 For loading data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should consider using the SQL Server destination instead of the OLE DB destination.  
  
### Bulk Insert Options  
 If the SQL Server destination uses a fast-load data access mode, you can specify the following fast load options:  
  
-   Retain identity values from the imported data file, or use unique values assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Retain null values during the bulk load operation.  
  
-   Verify constraints on the target table or view during the bulk import operation.  
  
-   Acquire a table-level lock for the duration of the bulk load operation.  
  
-   Execute insert triggers defined on the destination table during the bulk load operation.  
  
-   Specify the number of the first row in the input to load during the bulk insert operation.  
  
-   Specify the number of the last row in the input to load during the bulk insert operation.  
  
-   Specify the maximum number of errors allowed before the bulk load operation is canceled. Each row that cannot be imported is counted as one error.  
  
-   Specify the columns in the input that contain sorted data.  
  
 For more information about bulk load options, see [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md).  
  
#### Performance Improvements  
 To improve the performance of a bulk insert and the access to table data during the bulk insert operation, you should change the default options as follows:  
  
-   Do not verify constraints on the target table or view during the bulk import operation.  
  
-   Do not execute insert triggers defined on the destination table during the bulk load operation.  
  
-   Do not apply a lock to the table. That way, the table remains available to other users and applications during the bulk insert operation.  
  
## Configuration of the SQL Server Destination  
 You can configure the SQL Server destination in the following ways:  
  
-   Specify the table or view into which to bulk load the data.  
  
-   Customize the bulk load operation by specifying options such as whether to check constraints.  
  
-   Specify whether all rows commit in one batch or set the maximum number of rows to commit as a batch.  
  
-   Specify a time-out for the bulk load operation.  
  
 This destination uses an OLE DB connection manager to connect to a data source, and the connection manager specifies the OLE DB provider to use. For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
 An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project also provides the data source object from which you can create an OLE DB connection manager. This makes data sources and data source views available to the SQL Server destination.  
  
 The SQL Server destination has one input. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](./set-the-properties-of-a-data-flow-component.md)  
  
-   [SQL Server Destination Custom Properties](../../integration-services/data-flow/sql-server-destination-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Bulk Load Data by Using the SQL Server Destination](../../integration-services/data-flow/bulk-load-data-by-using-the-sql-server-destination.md)  
  
-   [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## Related Tasks  
  
-   [Bulk Load Data by Using the SQL Server Destination](../../integration-services/data-flow/bulk-load-data-by-using-the-sql-server-destination.md)  
  
-   [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## Related Content  
  
-   Technical article, [You may get "Unable to prepare the SSIS bulk insert for data insertion" error on UAC enabled systems](/troubleshoot/sql/integration-services/error-you-run-ssis-package), on support.microsoft.com.  
  
-   Technical article, [The Data Loading Performance Guide](/previous-versions/sql/sql-server-2008/dd425070(v=sql.100)), on msdn.microsoft.com.  
  
-   Technical article, [Using SQL Server Integration Services to Bulk Load Data](https://go.microsoft.com/fwlink/?LinkId=233701), on simple-talk.com.  
  
## SQL Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **SQL Destination Editor** dialog box to specify data source information, and to preview the results. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination loads data into tables or views in a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
### Options  
 **OLE DB connection manager**  
 Select an existing connection from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Use a table or view**  
 Select an existing table or view from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 **Preview**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
## SQL Destination Editor (Mappings Page)
  Use the **Mappings** page of the **SQL Destination Editor** dialog box to map input columns to destination columns.  
  
### Options  
 **Available Input Columns**  
 View the list of available input columns. Use a drag-and-drop operation to map available input columns in the table to destination columns.  
  
 **Available Destination Columns**  
 View the list of available destination columns. Use a drag-and-drop operation to map available destination columns in the table to input columns.  
  
 **Input Column**  
 View input columns selected from the table above. You can change the mappings by using the list of **Available Input Columns**.  
  
 **Destination Column**  
 View each available destination column, whether it is mapped or not.  
  
## SQL Destination Editor (Advanced Page)
  Use the **Advanced** page of the **SQL Destination Editor** dialog box to specify advanced bulk insert options.  
  
### Options  
 **Keep identity**  
 Specify whether the task should insert values into identity columns. The default value of this property is **False**.  
  
 **Keep nulls**  
 Specify whether the task should keep null values. The default value of this property is **False**.  
  
 **Table lock**  
 Specify whether the table is locked when the data is loaded. The default value of this property is **True**.  
  
 **Check constraints**  
 Specify whether the task should check constraints. The default value of this property is **True**.  
  
 **Fire triggers**  
 Specify whether the bulk insert should fire triggers on tables. The default value of this property is **False**.  
  
 **First Row**  
 Specify the first row to insert. The default value of this property is **-1**, indicating that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **SQL Destination Editor** to indicate that you do not want to assign a value for this property. Use -1 in the **Properties** window, the **Advanced Editor**, and the object model.  
  
 **Last Row**  
 Specify the last row to insert. The default value of this property is **-1**, indicating that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **SQL Destination Editor** to indicate that you do not want to assign a value for this property. Use -1 in the **Properties** window, the **Advanced Editor**, and the object model.  
  
 **Maximum number of errors**  
 Specify the number of errors that can occur before the bulk insert stops. The default value of this property is **-1**, indicating that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **SQL Destination Editor** to indicate that you do not want to assign a value for this property. Use -1 in the **Properties** window, the **Advanced Editor**, and the object model.  
  
 **Timeout**  
 Specify the number of seconds to wait before the bulk insert stops because of a time-out.  
  
 **Order columns**  
 Type the names of the sort columns. Each column can be sorted in ascending or descending order. If you use multiple sort columns, delimit the list with commas.  
  
## See Also  
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
