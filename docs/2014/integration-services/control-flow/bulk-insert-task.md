---
title: "Bulk Insert Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.bulkinserttask.f1"
helpviewer_keywords: 
  - "Bulk Insert task"
  - "copying data [Integration Services]"
ms.assetid: c5166156-6b4c-4369-81ed-27c4ce7040ae
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Bulk Insert Task
  The Bulk Insert task provides an efficient way to copy large amounts of data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or view. For example, suppose your company stores its million-row product list on a mainframe system, but the company's e-commerce system uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to populate Web pages. You must update the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product table nightly with the master product list from the mainframe. To update the table, you save the product list in a tab-delimited format and use the Bulk Insert task to copy the data directly into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 To ensure high-speed data copying, transformations cannot be performed on the data while it is moving from the source file to the table or view.  
  
## Usage Considerations  
 Before you use the Bulk Insert task, consider the following:  
  
-   The Bulk Insert task can transfer data only from a text file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or view. To use the Bulk Insert task to transfer data from other database management systems (DBMSs), you must export the data from the source to a text file and then import the data from the text file into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or view.  
  
-   The destination must be a table or view in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. If the destination table or view already contains data, the new data is appended to the existing data when the Bulk Insert task runs. If you want to replace the data, run an Execute SQL task that runs a DELETE or TRUNCATE statement before you run the Bulk Insert task. For more information, see [Execute SQL Task](execute-sql-task.md).  
  
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
 The following table lists the custom log entries for the Bulk Insert task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`BulkInsertTaskBegin`|Indicates that the bulk insert began.|  
|`BulkInsertTaskEnd`|Indicates that the bulk insert finished.|  
|`BulkInsertTaskInfos`|Provides descriptive information about the task.|  
  
## Bulk Insert Task Configuration  
 You can configure the Bulk Insert task in the following ways:  
  
-   Specify the OLE DB connection manager to connect to the destination [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and the table or view into which data is inserted. The Bulk Insert task supports only OLE DB connections for the destination database.  
  
-   Specify the File or Flat File connection manager to access the source file. The Bulk Insert task uses the connection manager only for the location of the source file. The task ignores other options that you select in the connection manager editor.  
  
-   Define the format that is used by the Bulk Insert task, either by using a format file or by defining the column and row delimiters of the source data. If using a format file, specify the File connection manager to access the format file.  
  
-   Specify actions to perform on the destination table or view when the task inserts the data. The options include whether to check constraints, enable identity inserts, keep nulls, fire triggers, or lock the table.  
  
-   Provide information about the batch of data to insert, such as the batch size, the first and last row from the file to insert, the number of insert errors that can occur before the task stops inserting rows, and the names of the columns that will be sorted.  
  
 If the Bulk Insert task uses a Flat File connection manager to access the source file, the task does not use the format specified in the Flat File connection manager. Instead, the Bulk Insert task uses either the format specified in a format file, or the values of the RowDelimiter and ColumnDelimiter properties of the task.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Bulk Insert Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Bulk Insert Task Editor &#40;Connection Page&#41;](../bulk-insert-task-editor-connection-page.md)  
  
-   [Bulk Insert Task Editor &#40;Options Page&#41;](../bulk-insert-task-editor-options-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to setthese properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
### Programmatic Configuration of the Bulk Insert Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask>  
  
## Related Tasks  
 [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## Related Content  
  
-   Technical article, [You may get "Unable to prepare the SSIS bulk insert for data insertion" error on UAC enabled systems](https://go.microsoft.com/fwlink/?LinkId=233693), on support.microsoft.com.  
  
-   Technical article, [The Data Loading Performance Guide](https://go.microsoft.com/fwlink/?LinkId=233700), on msdn.microsoft.com.  
  
-   Technical article, [Using SQL Server Integration Services to Bulk Load Data](https://go.microsoft.com/fwlink/?LinkId=233701), on simple-talk.com.  
  
  
