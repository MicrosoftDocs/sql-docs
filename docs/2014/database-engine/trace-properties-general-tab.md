---
title: "Trace Properties (General Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.pro.traceproperties.general.f1"
helpviewer_keywords: 
  - "Trace Properties dialog box"
ms.assetid: 25227268-143b-477e-aac9-8268bcaf2078
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Trace Properties (General Tab)
  Use the **General** tab of the **Trace Properties** dialog box to view or specify properties of a trace.  
  
## Options  
 **Trace name**  
 Specify the name of the trace.  
  
 **Trace provider name**  
 Shows the name of the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that will be traced. This field is populated automatically with the name of the server that you specified when you connected. To change the name of the trace provider, click **Cancel** to close the dialog box, and start a new trace.  
  
 **Trace provider type**  
 Shows the server type that is providing the trace. The trace definition file populates the **Trace provider type** field automatically. You cannot modify this field.  
  
 **version**  
 Shows the version of the server that is providing the trace. The trace definition file populates the **Version** field automatically. You cannot modify this field.  
  
 **Use the template**  
 Select a template from the template directory. The directory is populated with the default templates and any user-defined templates created for the current trace provider type.  
  
 **Save to file**  
 Capture the trace data to a .trc file. Saving trace data is useful for later review and analysis.  
  
 **Set maximum file size (MB)**  
 If you choose to save the trace data to a file, you must specify the maximum size of the trace file. The default is 5 megabytes (MB). The maximum size is limited only by the file system (NTFS, FAT) where the file is saved.  
  
 \<Graphic> **Save As**  
 After you have selected to save, you can select this icon to change the file name.  
  
 **Enable file rollover**  
 Select to enable the creation of additional files to accept the trace data when the maximum file size is reached. Each new file name consists of the original .trc file name, numbered sequentially. For example, once it reaches maximum file size, **NewTrace.trc** closes, and a new file, **NewTrace_1.trc**, opens, followed by **NewTrace_2.trc**, and so on. File rollover is enabled by default when you save a trace to a file.  
  
 **Server processes trace data**  
 Specify that the server running the trace should process the trace data. Using this option reduces the performance overhead incurred by tracing. If selected, no events are skipped even under stress conditions. If this check box is cleared, processing is performed by SQL Server Profiler, and there is a possibility that some events are not traced under stress conditions.  
  
 **Save to table**  
 Capture the trace data to a database table. Saving trace data is useful for later review and analysis. However, saving trace data to a table can incur significant overhead on the server where the trace is being saved. If possible, do not save the trace table on the same server that is being traced.  
  
 \<Graphic> **Destination Table**  
 After you have selected to save the trace data to a database table, you can select this icon to change the table name.  
  
 **Set maximum rows (in thousands)**  
 Specify the largest number of rows in which to save data. The default is 1000 rows.  
  
 **Enable trace stop time**  
 Set the date and time for the trace to end and close itself.  
  
## See Also  
 [Create a Trace &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)  
  
  
