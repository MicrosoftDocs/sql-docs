---
title: "Compare Options for Storing Blobs (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.technology: filestream
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 6038697b-36a9-49e8-a02a-2ad9e2e60e5a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Compare Options for Storing Blobs (SQL Server)
  Discusses and compares the options that are available for storing files and documents in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="Expectations"></a> Storing Files in the Database - Benefits and Expectations  
 A large percentage of enterprise data is unstructured in nature, and is typically stored as files and documents in file systems. Most of this data is produced, managed and consumed by applications that access the files through Windows APIs. Enterprises typically keep this data in the file system, while storing the related metadata for the files in a relational database.  
  
 Integrating unstructured data into the relational database provides significant benefits. These benefits include the following:  
  
-   Integrated storage and data management capabilities such as backup.  
  
-   Integrated services such as full-text search and semantic search over data and metadata.  
  
-   Ease of administration and policy management over the unstructured data.  
  
 For the most part, however, it has not been convenient to store unstructured data in a relational database. It has not previously been possible to run existing Windows-based applications on top of relational systems. It is not practical to rewrite established applications (such as Microsoft Word or Adobe Reader) to run on top relational database APIs. These applications simply expect the data to be accessible through Windows APIs. In other words, the expectations include the following:  
  
-   Windows applications are not aware of database transactions and do not require them.  
  
-   Windows applications require compatibility with file system APIs for file and directory data.  
  
##  <a name="Filestream"></a> FILESTREAM  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] already has the FILESTREAM feature, which provides efficient storage, management and streaming of unstructured data stored as files on the file system. However, a FILESTREAM solution requires custom programming, and does not satisfy the requirement for full Windows application compatibility described above.  
  
##  <a name="FileTables"></a> FileTables  
 The FileTable feature builds on top of existing FILESTREAM capabilities to enable enterprise customers to store unstructured file data and directory hierarchies in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, by addressing the requirements for non-transactional access and Windows application compatibility for file-based data.  
  
##  <a name="CompareFileTable"></a> Comparing FILESTREAM and FileTable  
  
|Feature|File Server and Database Solution|FILESTREAM Solution|FileTable Solution|  
|-------------|---------------------------------------|-------------------------|------------------------|  
|**Single story for management tasks**|No|Yes|**Yes**|  
|**Single set of services**: search, reporting, querying, and so forth|No|Yes|**Yes**|  
|**Integrated security model**|No|Yes|**Yes**|  
|**In-place updates of FILESTREAM data**|Yes|No|**Yes**|  
|**File and directory hierarchy maintained in the database**|No|No|**Yes**|  
|**Windows application compatibility**|Yes|No|**Yes**|  
|**Relational access to file attributes**|No|No|**Yes**|  
  
##  <a name="CompareRBS"></a> Comparing FILESTREAM and Remote BLOB Store (RBS)  
 For a comparison of these two features, see this blog post from the RBS team: [SQL Server Remote BLOB Store and FILESTREAM feature comparison](https://go.microsoft.com/fwlink/?LinkId=210317).  
  
##  <a name="more"></a> More Information  
 [FILESTREAM &#40;SQL Server&#41;](filestream-sql-server.md)  
 [FileTables &#40;SQL Server&#41;](filetables-sql-server.md)  
 [Remote Blob Store &#40;RBS&#41; &#40;SQL Server&#41;](remote-blob-store-rbs-sql-server.md)  
  
