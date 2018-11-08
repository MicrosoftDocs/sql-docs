---
title: "FILESTREAM and FileTable with AlwaysOn Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], Availability Groups"
  - "FILESTREAM [SQL Server], Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
ms.assetid: fdceda9a-a9db-4d1d-8745-345992164a98
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# FILESTREAM and FileTable with AlwaysOn Availability Groups (SQL Server)
  This topic contains information about the using the FILESTREAM and FileTable features with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
 All FILESTREAM functionality is supported. After a failover, FILESTREAM data is accessible on both readable secondary replicas and on the new primary.  
  
 FileTable functionality is partially supported. After a failover, FileTable data is accessible on the primary replica, but FileTable data is not accessible on readable secondary replicas.  
  
 **In this Topic:**  
  
-   [Prerequisites](#Prerequisites)  
  
-   [Using Virtual Network Names (VNNs) for FILESTREAM and FileTable Access](#vnn)  
  
-   [Related Tasks](#RelatedTasks)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   Before adding a database that uses FILESTREAM, with or without FileTable, to an availability group, ensure that FILESTREAM is enabled on every server instance that hosts an availability replica for the availability group. For more information, see [Enable and Configure FILESTREAM](../../../relational-databases/blob/enable-and-configure-filestream.md).  
  
##  <a name="vnn"></a> Using Virtual Network Names (VNNs) for FILESTREAM and FileTable Access  
 When you enable FILESTREAM on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], an instance-level share is created to provide access to the FILESTREAM data. You access this share by using the computer name in the following format:  
  
 `\\<computer_name>\<filestream_share_name>`  
  
 In an AlwaysOn availability group, however, the name of the computer is virtualized by using a Virtual Network Name, or VNN. When the computer is the primary replica in an availability group, and databases in the availability group contain FILESTREAM data, then a VNN-scoped share is also created to provide access to the FILESTREAM data. This does not affect Transact-SQL access to FILESTREAM data. However applications that use file system APIs have to use the VNN-scoped share, which has a path in the following format:  
  
 `\\<VNN>\<filestream_share_name>`  
  
 This VNN-scoped share is created when one of the following events occurs.  
  
-   You add a database that contains FILESTREAM data to an AlwaysOn availability group on the primary replica. In this case, the share `\\<computer_name>\<filestream_share_name>` already exists. The share `\\<VNN>\<filestream_share_name>` is created.  
  
-   You enable FILESTREAM for file i/o streaming access on a primary replica that has availability groups. The following shares are created:  
  
    1.  `\\<computer_name>\<filestream_share_name>`  
  
    2.  `\\<VNN1>\<filestream_share_name>` for availability group 1.  
  
    3.  `\\<VNN2>\<filestream_share_name>` for availability group 2.  
  
 These VNN-scoped shares are also propagated to all secondary replicas.  
  
 When the database that contains FILESTREAM or FileTable data belongs to an AlwaysOn availability group:  
  
-   The FILESTREAM and FileTable functions accept or return virtual network names (VNNs) instead of computer names. For more information about these functions, see [Filestream and FileTable Functions &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/filestream-and-filetable-functions-transact-sql).  
  
-   All access to FILESTREAM or FileTable data through the file system APIs should use VNNs instead of computer names.  
  
 If your application tries to access the share by using the computer name in the format `\\<computer_name>\<filestream_share_name>` when the database is part of an availability group, then an error is raised.  
  
 If your application tries to access the share by using a VNN-scoped path when the database is not part of an availability group, then the request may succeed. In this case, the virtual network name is resolved to the computer name. However this usage is strongly discouraged, since the VNN-scoped path will stop working if the availability group is dropped.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Enable and Configure FILESTREAM](../../../relational-databases/blob/enable-and-configure-filestream.md)  
  
-   [Enable the Prerequisites for FileTable](../../../relational-databases/blob/enable-the-prerequisites-for-filetable.md)  
  
##  <a name="RelatedContent"></a> Related Content  
 None.  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)  
  
  
