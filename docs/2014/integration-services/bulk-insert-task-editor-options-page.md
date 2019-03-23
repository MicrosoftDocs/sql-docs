---
title: "Bulk Insert Task Editor (Options Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.bulkinserttask.options.f1"
helpviewer_keywords: 
  - "Bulk Insert Task Editor"
ms.assetid: b3702811-3eb8-4b28-9190-5ae7a1a7bb6f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Bulk Insert Task Editor (Options Page)
  Use the **Options** page of the **Bulk Insert Task Editor** dialog box to set properties for the bulk insert operation. The Bulk Insert task copies large amounts of data into a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] table or view.  
  
 To learn about working with bulk inserts, see [Bulk Insert Task](control-flow/bulk-insert-task.md) and [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql).  
  
## Options  
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
 Specify the ORDER BY clause in the bulk insert statement. The column name that you supply must be a valid column in the destination table. The default is `false`. This means that the data is not sorted by an ORDER BY clause.  
  
 **MaxErrors**  
 Specify the maximum number of errors that can occur before the bulk insert operation is canceled. A value of 0 indicates that an infinite number of errors are allowed.  
  
> [!NOTE]  
>  Each row that cannot be imported by the bulk load operation is counted as one error.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Bulk Insert Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Bulk Insert Task Editor &#40;Connection Page&#41;](../../2014/integration-services/bulk-insert-task-editor-connection-page.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [Control Flow](control-flow/control-flow.md)  
  
  
