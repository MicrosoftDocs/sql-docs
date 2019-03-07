---
title: "Make Partial Updates to FILESTREAM Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], FSCTL_SQL_FILESTREAM_FETCH_OLD_CONTENT"
  - "FSCTL_SQL_FILESTREAM_FETCH_OLD_CONTENT"
ms.assetid: d6f7661e-6c14-4d31-9541-4520ca0f82b2
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Make Partial Updates to FILESTREAM Data
  An application uses FSCTL_SQL_FILESTREAM_FETCH_OLD_CONTENT to make partial updates to FILESTREAM BLOB data. The [DeviceIoControl](https://go.microsoft.com/fwlink/?LinkId=105527) function passes this value and the handle that is returned from [OpenSqlFilestream](access-filestream-data-with-opensqlfilestream.md) to the FILESTREAM driver. The driver then forces a server-side copy of the current FILESTREAM data into the file that is referenced by the handle. If the application issues the FSCTL_SQL_FILESTREAM_FETCH_OLD_CONTENT value after the handle has been written to, the last write operation persists and previous write operations that were made to the handle are lost.  
  
> [!NOTE]  
>  FILESTREAM relies on the [SMB protocol](https://go.microsoft.com/fwlink/?LinkId=112454) for remote access.  
  
## Example  
 The following example shows you how to use the `FSCTL_SQL_FILESTREAM_FETCH_OLD_CONTENT` value to perform a partial update of an inserted FILESTREAM BLOB.  
  
> [!NOTE]  
>  This example requires the FILESTREAM-enabled database and table that are created in [Create a FILESTREAM-Enabled Database](create-a-filestream-enabled-database.md) and [Create a Table for Storing FILESTREAM Data](create-a-table-for-storing-filestream-data.md).  
  
 [!code-cpp[FILESTREAM#FS_CPP_FSCTL](../../snippets/tsql/SQL15/tsql/filestream/cpp/filestream.cpp#fs_cpp_fsctl)]  
  
## See Also  
 [Access FILESTREAM Data with OpenSqlFilestream](access-filestream-data-with-opensqlfilestream.md)   
 [Create Client Applications for FILESTREAM Data](create-client-applications-for-filestream-data.md)  
  
  
