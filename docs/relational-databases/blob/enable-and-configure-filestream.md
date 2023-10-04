---
title: "Enable and configure FILESTREAM"
description: To use FILESTREAM, first enable it on the SQL Server Database Engine instance. Learn how to enable FILESTREAM by using SQL Server Configuration Manager.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/28/2023
ms.service: sql
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords:
  - "FILESTREAM [SQL Server], enabling"
---
# Enable and configure FILESTREAM

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Before you can start to use FILESTREAM, you must enable FILESTREAM on the instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. This topic describes how to enable FILESTREAM by using SQL Server Configuration Manager.

## <a id="enabling"></a> Enable FILESTREAM

1. On the **Start** menu, navigate to **All Programs > [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] > Configuration Tools**, and then select **SQL Server Configuration Manager**.

   > [!NOTE]  
   > On newer versions of Windows, follow these instructions to [open SQL Server Configuration Manager](../sql-server-configuration-manager.md).

1. In the list of services, right-click **SQL Server Services**, and then select **Open**.

1. In the **SQL Server Configuration Manager** snap-in, locate the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on which you want to enable FILESTREAM.

1. Right-click the instance, and then select **Properties**.

1. In the **SQL Server Properties** dialog box, select the **FILESTREAM** tab.

1. Select the **Enable FILESTREAM for Transact-SQL access** check box.

1. If you want to read and write FILESTREAM data from Windows, select **Enable FILESTREAM for file I/O streaming access**. Enter the name of the Windows share in the **Windows Share Name** box.

1. If remote clients must access the FILESTREAM data that is stored on this share, select **Allow remote clients to have streaming access to FILESTREAM data**.

1. Select **Apply**.

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select **New Query** to display the Query Editor.

1. In Query Editor, enter the following [!INCLUDE [tsql](../../includes/tsql-md.md)] code:

   ```sql
   EXEC sp_configure filestream_access_level, 2;
   RECONFIGURE;
   ```

1. Select **Execute**.

1. Restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service.

## <a id="best"></a> Best practices

### <a id="config"></a> Physical configuration and maintenance

When you set up FILESTREAM storage volumes, consider the following guidelines:

- Turn off short file names on FILESTREAM computer systems. Short file names take significantly longer to create. To disable short file names, use the Windows **fsutil** utility.

- Regularly defragment FILESTREAM computer systems using magnetic storage.

- Use 64-KB NTFS clusters. Compressed volumes must be set to 4-KB NTFS clusters.

- Disable indexing on FILESTREAM volumes and set `disablelastaccess`. To set `disablelastaccess`, use the Windows **fsutil** utility.

- Disable antivirus scanning of FILESTREAM volumes when it's not necessary. If antivirus scanning is necessary, avoid setting policies that will automatically delete offending files.

- Set up and tune the RAID level for fault tolerance and the performance that is required by an application.

| RAID level | Write performance | Read performance | Fault tolerance | Remarks |
| --- | --- | --- | --- | --- |
| RAID 5 | Normal | Normal | Excellent | Performance is better than one disk or JBOD; and less than RAID 0 or RAID 5 with striping. |
| RAID 0 | Excellent | Excellent | None | |
| RAID 5 + striping | Excellent | Excellent | Excellent | Most expensive option. |

### <a id="database"></a> Physical database design

When you design a FILESTREAM database, consider the following guidelines:

- FILESTREAM columns must be accompanied by a corresponding **uniqueidentifier** ROWGUID column. These kinds of tables must also be accompanied by a unique index. Typically this index isn't a clustered index. If the databases business logic requires a clustered index, you have to make sure that the values stored in the index aren't random. Random values will cause the index to be reordered every time that a row is added or removed from the table.

- For performance reasons, FILESTREAM filegroups and containers should reside on volumes other than the operating system, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] log, `tempdb`, or paging file.

- Space management and policies aren't directly supported by FILESTREAM. However, you can manage space and apply policies indirectly by assigning each FILESTREAM filegroup to a separate volume and using the volume's management features.
