---
title: "Bulk Insert Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.bulkinserttask.f1"
  - "sql13.dts.designer.bulkinserttask.connection.f1"
  - "sql13.dts.designer.bulkinserttask.general.f1"
  - "sql13.dts.designer.bulkinserttask.options.f1"
helpviewer_keywords: 
  - "Bulk Insert task"
  - "copying data [Integration Services]"
ms.assetid: c5166156-6b4c-4369-81ed-27c4ce7040ae
author: janinezhang
ms.author: janinez
manager: craigg
---
# Bulk Insert Task
  The Bulk Insert task provides an efficient way to copy large amounts of data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or view. For example, suppose your company stores its million-row product list on a mainframe system, but the company's e-commerce system uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to populate Web pages. You must update the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product table nightly with the master product list from the mainframe. To update the table, you save the product list in a tab-delimited format and use the Bulk Insert task to copy the data directly into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 To ensure high-speed data copying, transformations cannot be performed on the data while it is moving from the source file to the table or view.  
  
## Usage Considerations  
 Before you use the Bulk Insert task, consider the following:  
  
-   The Bulk Insert task can transfer data only from a text file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or view. To use the Bulk Insert task to transfer data from other database management systems (DBMSs), you must export the data from the source to a text file and then import the data from the text file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or view.  
  
-   The destination must be a table or view in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. If the destination table or view already contains data, the new data is appended to the existing data when the Bulk Insert task runs. If you want to replace the data, run an Execute SQL task that runs a DELETE or TRUNCATE statement before you run the Bulk Insert task. For more information, see [Execute SQL Task](../../integration-services/control-flow/execute-sql-task.md).  
  
-   You can use a format file in the Bulk Insert task object. If you have a format file that was created by the **bcp** utility, you can specify its path in the Bulk Insert task. The Bulk Insert task supports both XML and nonXML format files. For more information about format files, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).  
  
-   Only members of the sysadmin fixed server role can run a package that contains a Bulk Insert task.  
  
## Bulk Insert Task with Transactions  
 If a batch size is not set, the complete bulk copy operation is treated as one transaction. A batch size of **0** indicates that the data is inserted in one batch. If a batch size is set, each batch represents a transaction that is committed when the batch finishes running.  
  
 The behavior of the Bulk Insert task, as it relates to transactions, depends on whether the task joins the package transaction. If the Bulk Insert task does not join the package transaction, each error-free batch is committed as a unit before the next batch is tried. If the Bulk Insert task joins the package transaction, error-free batches remain in the transaction at the conclusion of the task. These batches are subject to the commit or rollback operation of the package.  
  
 A failure in the Bulk Insert task does not automatically roll back successfully loaded batches; similarly, if the task succeeds, batches are not automatically committed. Commit and rollback operations occur only in response to package and workflow property settings.  
  
## Source and Destination  
 When you specify the location of the text source file, consider the following:  
  
-   The server must have permission to access both the file and the destination database.  
  
-   The server runs the Bulk Insert task. Therefore, any format file that the task uses must be located on the server.  
  
-   The source file that the Bulk Insert task loads can be on the same server as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database into which data is inserted, or on a remote server. If the file is on a remote server, you must specify the file name using the Universal Naming Convention (UNC) name in the path.  
  
## Performance Optimization  
 To optimize performance, consider the following:  
  
-   If the text file is located on the same computer as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database into which data is inserted, the copy operation occurs at an even faster rate because the data is not moved over the network.  
  
-   The Bulk Insert task does not log error-causing rows. If you must capture this information, use the error outputs of data flow components to capture error-causing rows in an exception file.  
  
## Custom Log Entries Available on the Bulk Insert Task  
 The following table lists the custom log entries for the Bulk Insert task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**BulkInsertTaskBegin**|Indicates that the bulk insert began.|  
|**BulkInsertTaskEnd**|Indicates that the bulk insert finished.|  
|**BulkInsertTaskInfos**|Provides descriptive information about the task.|  
  
## Bulk Insert Task Configuration  
 You can configure the Bulk Insert task in the following ways:  
  
-   Specify the OLE DB connection manager to connect to the destination [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and the table or view into which data is inserted. The Bulk Insert task supports only OLE DB connections for the destination database.  
  
-   Specify the File or Flat File connection manager to access the source file. The Bulk Insert task uses the connection manager only for the location of the source file. The task ignores other options that you select in the connection manager editor.  
  
-   Define the format that is used by the Bulk Insert task, either by using a format file or by defining the column and row delimiters of the source data. If using a format file, specify the File connection manager to access the format file.  
  
-   Specify actions to perform on the destination table or view when the task inserts the data. The options include whether to check constraints, enable identity inserts, keep nulls, fire triggers, or lock the table.  
  
-   Provide information about the batch of data to insert, such as the batch size, the first and last row from the file to insert, the number of insert errors that can occur before the task stops inserting rows, and the names of the columns that will be sorted.  
  
 If the Bulk Insert task uses a Flat File connection manager to access the source file, the task does not use the format specified in the Flat File connection manager. Instead, the Bulk Insert task uses either the format specified in a format file, or the values of the RowDelimiter and ColumnDelimiter properties of the task.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For more information about how to setthese properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
### Programmatic Configuration of the Bulk Insert Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask>  
  
## Related Tasks  
 [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Related Content  
  
-   Technical article, [You may get "Unable to prepare the SSIS bulk insert for data insertion" error on UAC enabled systems](https://go.microsoft.com/fwlink/?LinkId=233693), on support.microsoft.com.  
  
-   Technical article, [The Data Loading Performance Guide](https://go.microsoft.com/fwlink/?LinkId=233700), on msdn.microsoft.com.  
  
-   Technical article, [Using SQL Server Integration Services to Bulk Load Data](https://go.microsoft.com/fwlink/?LinkId=233701), on simple-talk.com.  
  
## Bulk Insert Task Editor (Connection Page)
  Use the **Connection** page of the **Bulk Insert Task Editor** dialog box to specify the source and destination of the bulk insert operation and the format to use.  
  
 To learn about working with bulk inserts, see [Bulk Insert Task](../../integration-services/control-flow/bulk-insert-task.md) and [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).  
  
### Options  
 **Connection**  
 Select an OLE DB connection manager in the list, or click \<**New connection...**> to create a new connection.  
  
 **Related Topics:** [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md)  
  
 **DestinationTable**  
 Type the name of the destination table or view or select a table or view in the list.  
  
 **Format**  
 Select the source of the format for the bulk insert. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Use File**|Select a file containing the format specification. Selecting this option displays the dynamic option, **FormatFile**.|  
|**Specify**|Specify the format. Selecting this option displays the dynamic options, **RowDelimiter** and **ColumnDelimiter**.|  
  
 **File**  
 Select a File or Flat File connection manager in the list, or click \<**New connection...**> to create a new connection.  
  
 The file location is relative to the SQL Server Database Engine specified in the connection manager for this task. The text file must be accessible by the SQL Server Database Engine either on a local hard drive on the server, or via a share or mapped drive to the SQL Server. The file is not accessed by the SSIS Runtime.  
  
 If you access the source file by using a Flat File connection manager, the Bulk Insert task does not use the format specified in the Flat File connection manager. Instead, the Bulk Insert task uses either the format specified in a format file or the values of the RowDelimiter and ColumnDelimiter properties of the task.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [Flat File Connection Manager](../../integration-services/connection-manager/flat-file-connection-manager.md) 
  
 **Refresh Tables**  
 Refresh the list of tables and views.  
  
### Format Dynamic Options  
  
#### Format = Use File  
 **FormatFile**  
 Type the path of the format file or click the ellipsis button **(...)** to locate the format file.  
  
#### Format = Specify  
 **RowDelimiter**  
 Specify the row delimiter in the source file. The default value is **{CR}{LF}**.  
  
 **ColumnDelimiter**  
 Specify the column delimiter in the source file. The default is **Tab**.  
  
## Bulk Insert Task Editor (General Page)
  Use the **General** page of the **Bulk Insert Task Editor** dialog box to name and describe the Bulk Insert task.  
  
### Options  
 **Name**  
 Provide a unique name for the Bulk Insert task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Bulk Insert task.  
 
## Bulk Insert Task Editor (Options Page)
  Use the **Options** page of the **Bulk Insert Task Editor** dialog box to set properties for the bulk insert operation. The Bulk Insert task copies large amounts of data into a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or view.  
  
 To learn about working with bulk inserts, see [Bulk Insert Task](../../integration-services/control-flow/bulk-insert-task.md) and [BULK INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/bulk-insert-transact-sql.md).  
  
### Options  
 **CodePage**  
 Specify the code page of the data in the data file.  
  
 **DataFileType**  
 Specify the data-type value to use in the load operation.  
  
 **BatchSize**  
 Specify the number of rows in a batch. The default is the entire data file. If you set **BatchSize** to zero, the data is loaded in a single batch.  
  
 **LastRow**  
 Specify the last row to copy.  
  
 **FirstRow**  
 Specify the first row from which to start copying.  
  
 **Options**  
 |Term|Definition|  
|----------|----------------|  
|**Check constraints**|Select to check the table and column constraints.|  
|**Keep nulls**|Select to retain null values during the bulk insert operation, instead of inserting any default values for empty columns.|  
|**Enable identity insert**|Select to insert existing values into an identity column.|  
|**Table lock**|Select to lock the table during the bulk insert.|  
|**Fire triggers**|Select to fire any insert, update, or delete triggers on the table.|  
  
 **SortedData**  
 Specify the ORDER BY clause in the bulk insert statement. The column name that you supply must be a valid column in the destination table. The default is **false**. This means that the data is not sorted by an ORDER BY clause.  
  
 **MaxErrors**  
 Specify the maximum number of errors that can occur before the bulk insert operation is canceled. A value of 0 indicates that an infinite number of errors are allowed.  
  
> [!NOTE]  
>  Each row that cannot be imported by the bulk load operation is counted as one error.  
  
