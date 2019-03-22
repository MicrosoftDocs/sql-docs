---
title: "Bulk Insert Task Editor (Connection Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.bulkinserttask.connection.f1"
helpviewer_keywords: 
  - "Bulk Insert Task Editor"
ms.assetid: 51252c20-8865-4ede-a3fd-bd73a968f47d
author: janinezhang
ms.author: janinez
manager: craigg
---
# Bulk Insert Task Editor (Connection Page)
  Use the **Connection** page of the **Bulk Insert Task Editor** dialog box to specify the source and destination of the bulk insert operation and the format to use.  
  
 To learn about working with bulk inserts, see [Bulk Insert Task](control-flow/bulk-insert-task.md) and [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).  
  
## Options  
 **Connection**  
 Select an OLE DB connection manager in the list, or click \<**New connection...**> to create a new connection.  
  
 **Related Topics:** [OLE DB Connection Manager](connection-manager/ole-db-connection-manager.md), [Configure OLE DB Connection Manager](../../2014/integration-services/configure-ole-db-connection-manager.md)  
  
 **DestinationTable**  
 Type the name of the destination table or view or select a table or view in the list.  
  
 **Format**  
 Select the source of the format for the bulk insert. This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Use File**|Select a file containing the format specification. Selecting this option displays the dynamic option, **FormatFile**.|  
|**Specify**|Specify the format. Selecting this option displays the dynamic options, `RowDelimiter` and `ColumnDelimiter`.|  
  
 **File**  
 Select a File or Flat File connection manager in the list, or click \<**New connection...**> to create a new connection.  
  
 The file location is relative to the SQL Server Database Engine specified in the connection manager for this task. The text file must be accessible by the SQL Server Database Engine either on a local hard drive on the server, or via a share or mapped drive to the SQL Server. The file is not accessed by the SSIS Runtime.  
  
 If you access the source file by using a Flat File connection manager, the Bulk Insert task does not use the format specified in the Flat File connection manager. Instead, the Bulk Insert task uses either the format specified in a format file or the values of the RowDelimiter and ColumnDelimiter properties of the task.  
  
 **Related Topics:** [File Connection Manager](connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md), [Flat File Connection Manager](connection-manager/flat-file-connection-manager.md), [Flat File Connection Manager Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md), [Flat File Connection Manager Editor &#40;Columns Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-columns-page.md), [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-advanced-page.md)  
  
 **Refresh Tables**  
 Refresh the list of tables and views.  
  
## Format Dynamic Options  
  
### Format = Use File  
 **FormatFile**  
 Type the path of the format file or click the ellipsis button **(...)** to locate the format file.  
  
### Format = Specify  
 `RowDelimiter`  
 Specify the row delimiter in the source file. The default value is **{CR}{LF}**.  
  
 `ColumnDelimiter`  
 Specify the column delimiter in the source file. The default is **Tab**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Bulk Insert Task Editor &#40;General Page&#41;](../../2014/integration-services/bulk-insert-task-editor-general-page.md)   
 [Bulk Insert Task Editor &#40;Options Page&#41;](../../2014/integration-services/bulk-insert-task-editor-options-page.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [Control Flow](control-flow/control-flow.md)  
  
  
