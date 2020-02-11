---
title: "SQL Server Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.sqlserverdest.f1"
helpviewer_keywords: 
  - "SQL Server destination"
  - "loading data"
  - "destinations [Integration Services], SQL Server"
  - "inserting data"
  - "bulk load [Integration Services]"
ms.assetid: a0227cd8-6944-4547-87e8-7b2507e26442
author: janinezhang
ms.author: janinez
manager: craigg
---
# SQL Server Destination
  The SQL Server destination connects to a local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and bulk loads data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables and views. You cannot use the SQL Server destination in packages that access a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database on a remote server. Instead, the packages should use the OLE DB destination. For more information, see [OLE DB Destination](ole-db-destination.md).  
  
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
  
 For more information about bulk load options, see [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql).  
  
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
  
 This destination uses an OLE DB connection manager to connect to a data source, and the connection manager specifies the OLE DB provider to use. For more information, see [OLE DB Connection Manager](../connection-manager/ole-db-connection-manager.md).  
  
 An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project also provides the data source object from which you can create an OLE DB connection manager. This makes data sources and data source views available to the SQL Server destination.  
  
 The SQL Server destination has one input. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **SQL Server Destination Editor** dialog box, click one of the following topics:  
  
-   [SQL Destination Editor &#40;Connection Manager Page&#41;](../sql-destination-editor-connection-manager-page.md)  
  
-   [SQL Destination Editor &#40;Mappings Page&#41;](../sql-destination-editor-mappings-page.md)  
  
-   [SQL Destination Editor &#40;Advanced Page&#41;](../sql-destination-editor-advanced-page.md)  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../common-properties.md)  
  
-   [SQL Server Destination Custom Properties](sql-server-destination-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Bulk Load Data by Using the SQL Server Destination](sql-server-destination.md)  
  
-   [Set the Properties of a Data Flow Component](set-the-properties-of-a-data-flow-component.md)  
  
## Related Tasks  
  
-   [Bulk Load Data by Using the SQL Server Destination](sql-server-destination.md)  
  
-   [Set the Properties of a Data Flow Component](set-the-properties-of-a-data-flow-component.md)  
  
## Related Content  
  
-   Technical article, [You may get "Unable to prepare the SSIS bulk insert for data insertion" error on UAC enabled systems](https://go.microsoft.com/fwlink/?LinkId=199482), on support.microsoft.com.  
  
-   Technical article, [The Data Loading Performance Guide](https://go.microsoft.com/fwlink/?LinkId=233700), on msdn.microsoft.com.  
  
-   Technical article, [Using SQL Server Integration Services to Bulk Load Data](https://go.microsoft.com/fwlink/?LinkId=233701), on simple-talk.com.  
  
## See Also  
 [Data Flow](data-flow.md)  
  
  
