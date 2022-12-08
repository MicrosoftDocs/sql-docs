---
title: "Access FILESTREAM Data with Transact-SQL | Microsoft Docs"
description: Learn how to use the Transact-SQL INSERT, UPDATE, and DELETE statements to access and manage FILESTREAM data.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], Transact-SQL"
ms.assetid: a6bf0ce7-7e5e-4a07-8917-ee526c9d0a05
author: MikeRayMSFT
ms.author: mikeray
---
# Access FILESTREAM Data with Transact-SQL
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to use the [!INCLUDE[tsql](../../includes/tsql-md.md)] INSERT, UPDATE, and DELETE statements to manage FILESTREAM data.  
  
> [!NOTE]  
>  The examples in this topic require the FILESTREAM-enabled database and table that are created in [Create a FILESTREAM-Enabled Database](../../relational-databases/blob/create-a-filestream-enabled-database.md) and [Create a Table for Storing FILESTREAM Data](../../relational-databases/blob/create-a-table-for-storing-filestream-data.md).  
  
##  <a name="ins"></a> Inserting a Row That Contains FILESTREAM Data  
 To add a row to a table that supports FILESTREAM data, use the [!INCLUDE[tsql](../../includes/tsql-md.md)] INSERT statement. When you insert data into a FILESTREAM column, you can insert NULL or a **varbinary(max)** value.  
  
### Inserting NULL  
 The following example shows how to insert `NULL`. When the FILESTREAM value is `NULL`, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not create a file in the file system.  
  
 [!code-sql[FILESTREAM#FS_InsertNULL](../../relational-databases/blob/codesnippet/tsql/access-filestream-data-w_1_1.sql)]  
  
### Inserting a Zero-Length Record  
 The following example shows how to use `INSERT` to create a zero-length record. This is useful for when you want to obtain a file handle, but will be manipulating the file by using Win32 APIs.  
  
 [!code-sql[FILESTREAM#FS_InsertZero](../../relational-databases/blob/codesnippet/tsql/access-filestream-data-w_1_2.sql)]  
  
### Creating a Data File  
 The following example shows how to use `INSERT` to create a file that contains data. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] converts the string `Seismic Data` to a `varbinary(max)` value. FILESTREAM creates the Windows file if it does not already exist.The data is then added to the data file.  
  
 [!code-sql[FILESTREAM#FS_InsertData](../../relational-databases/blob/codesnippet/tsql/access-filestream-data-w_1_3.sql)]  
  
 When you select all data from the `Archive.dbo.Records` table, the results are similar to the results that are shown in the following table. However, the `Id` column will contain different GUIDs.  
  
|Id|SerialNumber|Chart|  
|--------|------------------|------------|  
|`C871B90F-D25E-47B3-A560-7CC0CA405DAC`|`1`|`NULL`|  
|`F8F5C314-0559-4927-8FA9-1535EE0BDF50`|`2`|`0x`|  
|`7F680840-B7A4-45D4-8CD5-527C44D35B3F`|`3`|`0x536569736D69632044617461`|  
  
  
##  <a name="upd"></a> Updating FILESTREAM Data  
 You can use [!INCLUDE[tsql](../../includes/tsql-md.md)] to update the data in the file system file; although, you might not want to do this when you have to stream large amounts of data to a file.  
  
 The following example replaces any text in the file record with the text `Xray 1`.  
  
 [!code-sql[FILESTREAM#FS_UpdateData](../../relational-databases/blob/codesnippet/tsql/access-filestream-data-w_1_4.sql)]  
  
  
##  <a name="del"></a> Deleting FILESTREAM Data  
 When you delete a row that contains a FILESTREAM field, you also delete its underlying file system files. The only way to delete a row, and therefore the file, is to use the [!INCLUDE[tsql](../../includes/tsql-md.md)] DELETE statement.  
  
 The following example shows how to delete a row and its associated file system files.  
  
 [!code-sql[FILESTREAM#FS_DeleteData](../../relational-databases/blob/codesnippet/tsql/access-filestream-data-w_1_5.sql)]  
  
 When you select all data from the `Archive.dbo.Records` table, the row is gone and you can no longer use the associated file.  
  
> [!NOTE]  
>  The underlying files are removed by the FILESTREAM garbage collector.  
  
  
## See Also  
 [Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)   
 [Avoid Conflicts with Database Operations in FILESTREAM Applications](../../relational-databases/blob/avoid-conflicts-with-database-operations-in-filestream-applications.md)  
  
  
