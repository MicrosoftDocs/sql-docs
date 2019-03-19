---
title: "Remote Blob Store (RBS) (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.technology: filestream
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Remote Blob Store (RBS) [SQL Server]"
  - "RBS (Remote Blob Store) [SQL Server]"
ms.assetid: 31c947cf-53e9-4ff4-939b-4c1d034ea5b1
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Remote Blob Store (RBS) (SQL Server)
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Remote BLOB Store (RBS) is an optional add-on component that lets database administrators store binary large objects in commodity storage solutions instead of directly on the main database server.  
  
 RBS is included on the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation media, but is not installed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup program.  
  
 For more information about RBS, see [RBS Resources](#rbsresources) in this topic.  
  
## Benefits of RBS  
 RBS provides the following benefits:  
  
### Optimized database storage and performance  
 Storing BLOBs in the database can consume large amounts of file space and expensive server resources. RBS efficiently transfers the BLOBs to a dedicated storage solution of your choosing, and stores references to them in the database. This frees server storage for structured data, and frees server resources for database operations.  
  
### Efficient management of BLOBs  
 Several RBS features support the convenient management of stored BLOBs:  
  
-   BLOBS are managed with ACID (atomic consistency isolation durable) transactions.  
  
-   BLOBs are organized into collections.  
  
-   Garbage collection, consistency checking, and other maintenance functions are included.  
  
### Standardized API  
 RBS defines a set of APIs that provide a standardized programming model for applications to access and modify any BLOB store. Each BLOB store can specify its own provider library, which plugs into the RBS client library and specifies how BLOBs are stored and accessed.  
  
 A number of third-party storage solution vendors have developed RBS providers that conform to these standard APIs and support BLOB storage on various storage platforms.  
  
## RBS Requirements  
 RBS requires [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise for the main database server in which the BLOB metadata is stored. However, if you use the supplied FILESTREAM provider, you can store the BLOBs themselves on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard.  
  
 RBS includes a FILESTREAM provider that lets you use RBS to store BLOBs on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you want use RBS to store BLOBs in a different storage solution, you have to use a third party RBS provider developed for that storage solution, or develop a custom RBS provider using the RBS API. A sample provider that stores BLOBs in the NTFS file system is available as a learning resource on [Codeplex](https://go.microsoft.com/fwlink/?LinkId=210190).  
  
## RBS Security  
 When you use a custom provider to store BLOBs outside of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], they may be available to other processes that bypass the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security system. Make sure that you protect the stored BLOBs with permissions and encryption options that are appropriate to the storage medium used by the custom provider.  
  
##  <a name="rbsresources"></a> RBS Resources  
 **RBS documentation**  
 The RBS documentation is included in the Windows installer package. If you want to review the RBS documentation without installing RBS, you can view the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version of the documentation [online in the MSDN Library](https://go.microsoft.com/fwlink/?LinkId=210192).  
  
 **RBS white paper**  
 The white paper "[Remote BLOB Storage](https://go.microsoft.com/fwlink/?LinkId=210422)", which is available for download as a Microsoft Word document, provides detailed information about installing and configuring RBS.  
  
 **RBS samples**  
 The RBS samples available on [Codeplex](https://go.microsoft.com/fwlink/?LinkId=210190) demonstrate how to develop an RBS application, and how to develop and install a custom RBS provider.  
  
 **RBS blog**  
 The [RBS blog](https://go.microsoft.com/fwlink/?LinkId=210315) provides additional information to help you understand, deploy, and maintain RBS.  
  
  
