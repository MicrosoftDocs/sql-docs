---
title: Set a Maximum File Size for a Trace File
titleSuffix: SQL Server Profiler
description: Discover how to limit the size of the trace file in SQL Server Profiler and how to specify whether the file rolls over when it reaches maximum size.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: e86dc4ce-5aa3-4c0d-acb5-c9e8871ed963
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Set a Maximum File Size for a Trace File (SQL Server Profiler)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the following procedure to set the maximum file size for a trace file.  
  
### To set a maximum file size for a trace file  
  
1.  On the **File** menu, click **New Trace**, and then connect to an instance of Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     The **Trace Properties**dialog box appears.  
  
    > [!NOTE]  
    >  If **Start tracing immediately after making connection**is selected, the **Trace Properties**dialog box fails to appear and the trace begins instead. To turn off this setting, on the **Tools**menu, click **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2.  In the **Trace name** box, type a name for the trace.  
  
3.  In the **Template name**list, select a trace template.  
  
4.  Select **Save to file**, and then specify a file to store the trace information.  
  
5.  In the **Set maximum file size** check box, specify a maximum file size for the trace. When the file size reaches this maximum, trace events are no longer recorded in this file. If you select **Enable file rollover** (which is selected by default),the following occurs:  
  
     The file rollover option causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to close the current file and create a new file when the maximum file size is reached. The new file has the same name as the previous file, but an integer is appended to the name to indicate its sequence; for example, if the original trace file is named filename_1.trc, the next trace file is filename_2.trc, and so on. If the name assigned to a new rollover file is already used by an existing file, the existing file is overwritten unless it is read-only. The file rollover option is enabled by default when you are saving trace data to a file.  
  
     With the file rollover option on, the trace continues until it is stopped by some other means. To stop the trace after you have reached the file size limit, disable the file rollover option.  
  
    > [!NOTE]  
    >  The FAT32 file system limits files to slightly less than 4 gigabytes (GB). When the trace file reaches that size, the trace fails with the error"Not enough disk space." To create larger files, use the NTFS file system.  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
