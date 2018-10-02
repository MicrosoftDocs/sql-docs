---
title: "Binary Large Object (Blob) Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], design and implementation"
ms.assetid: 97509274-c3f8-43e5-a37c-52f1ffe0961a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Binary Large Object (Blob) Data (SQL Server)
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides solutions for storing files and documents in the database or on remote storage devices.  
  
##  <a name="section"></a> In This Section  
 [Compare Options for Storing Blobs &#40;SQL Server&#41;](compare-options-for-storing-blobs-sql-server.md)  
 Compare the advantages of FILESTREAM, FileTables, and Remote Blob Store.  
  
 [FILESTREAM &#40;SQL Server&#41;](filestream-sql-server.md)  
 FILESTREAM enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-based applications to store unstructured data, such as documents and images, on the file system. Applications can leverage the rich streaming APIs and performance of the file system and at the same time maintain transactional consistency between the unstructured data and corresponding structured data.  
  
 [FileTables &#40;SQL Server&#41;](filetables-sql-server.md)  
 The FileTable feature brings support for the Windows file namespace and compatibility with Windows applications to the file data stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. FileTable lets an application integrate its storage and data management components, and provides integrated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services - including full-text search and semantic search - over unstructured data and metadata.  
  
 In other words, you can store files and documents in special tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] called FileTables, but access them from Windows applications as if they were stored in the file system, without making any changes to your client applications.  
  
 [Remote Blob Store &#40;RBS&#41; &#40;SQL Server&#41;](remote-blob-store-rbs-sql-server.md)  
 Remote BLOB store (RBS) for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] lets database administrators store binary large objects (BLOBs) in commodity storage solutions instead of directly on the server. This saves a significant amount of space and avoids wasting expensive server hardware resources. RBS provides a set of API libraries that define a standardized model for applications to access BLOB data. RBS also includes maintenance tools, such as garbage collection, to help manage remote BLOB data.  
  
 RBS is included on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media, but is not installed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup program.  
  
  
