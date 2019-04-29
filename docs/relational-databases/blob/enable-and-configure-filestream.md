---
title: "Enable and configure FILESTREAM | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], enabling"
ms.assetid: 78737e19-c65b-48d9-8fa9-aa6f1e1bce73
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Enable and configure FILESTREAM
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Before you can start to use FILESTREAM, you must enable FILESTREAM on the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. This topic describes how to enable FILESTREAM by using SQL Server Configuration Manager.  
  
##  <a name="enabling"></a> Enabling FILESTREAM  
  
#### To enable and change FILESTREAM settings  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  In the list of services, right-click **SQL Server Services**, and then click **Open**.  
  
3.  In the **SQL Server Configuration Manager** snap-in, locate the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on which you want to enable FILESTREAM.  
  
4.  Right-click the instance, and then click **Properties**.  
  
5.  In the **SQL Server Properties** dialog box, click the **FILESTREAM** tab.  
  
6.  Select the **Enable FILESTREAM for Transact-SQL access** check box.  
  
7.  If you want to read and write FILESTREAM data from Windows, click **Enable FILESTREAM for file I/O streaming access**. Enter the name of the Windows share in the **Windows Share Name** box.  
  
8.  If remote clients must access the FILESTREAM data that is stored on this share, select **Allow remote clients to have streaming access to FILESTREAM data**.  
  
9. Click **Apply**.  
  
10. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], click **New Query** to display the Query Editor.  
  
11. In Query Editor, enter the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code:  
  
    ```sql  
    EXEC sp_configure filestream_access_level, 2  
    RECONFIGURE  
    ```  
  
12. Click **Execute**.  
  
13. Restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
  
##  <a name="best"></a> Best practices  
  
###  <a name="config"></a> Physical configuration and maintenance  
 When you set up FILESTREAM storage volumes, consider the following guidelines:  
  
-   Turn off short file names on FILESTREAM computer systems. Short file names take significantly longer to create. To disable short file names, use the Windows **fsutil** utility.  
  
-   Regularly defragment FILESTREAM computer systems.  
  
-   Use 64-KB NTFS clusters. Compressed volumes must be set to 4-KB NTFS clusters.  
  
-   Disable indexing on FILESTREAM volumes and set **disablelastaccess**. To set **disablelastaccess**, use the Windows **fsutil** utility.  
  
-   Disable antivirus scanning of FILESTREAM volumes when it is not necessary. If antivirus scanning is necessary, avoid setting policies that will automatically delete offending files.  
  
-   Set up and tune the RAID level for fault tolerance and the performance that is required by an application.  
  
||||||  
|-|-|-|-|-|  
|RAID level|Write performance|Read performance|Fault tolerance|Remarks|  
|RAID 5|Normal|Normal|Excellent|Performance is better than one disk or JBOD; and less than RAID 0 or RAID 5 with striping.|  
|RAID 0|Excellent|Excellent|None||  
|RAID 5 + striping|Excellent|Excellent|Excellent|Most expensive option.|  
  
  
###  <a name="database"></a> Physical database design  
 When you design a FILESTREAM database, consider the following guidelines:  
  
-   FILESTREAM columns must be accompanied by a corresponding **uniqueidentifier**ROWGUID column. These kinds of tables must also be accompanied by a unique index. Typically this index is not a clustered index. If the databases business logic requires a clustered index, you have to make sure that the values stored in the index are not random. Random values will cause the index to be reordered every time that a row is added or removed from the table.  
  
-   For performance reasons, FILESTREAM filegroups and containers should reside on volumes other than the operating system, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log, tempdb, or paging file.  
  
-   Space management and policies are not directly supported by FILESTREAM. However, you can manage space and apply policies indirectly by assigning each FILESTREAM filegroup to a separate volume and using the volume's management features.  
  
  
