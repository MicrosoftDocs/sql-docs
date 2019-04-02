---
title: "Performance Counters | Microsoft Docs"
ms.custom: supportability
ms.date: "08/27/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [Integration Services], performance counters"
  - "performance counters [Integration Services]"
  - "data flow [Integration Services], performance"
  - "counters [Integration Services]"
  - "data flow engine [Integration Services]"
ms.assetid: 11e17f4e-72ed-44d7-a71d-a68937a78e4c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Performance Counters
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installs a set of performance counters that you can use to monitor the performance of the data flow engine. For example, you can watch the "Buffers spooled" counter to determine whether data buffers are being written to disk temporarily while a package is running. This swapping reduces performance and indicates that the computer has insufficient memory.  
  
> **NOTE:** If you install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] on a computer that is running [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)], and then upgrade that computer to [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)], the upgrade process removes the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] performance counters from the computer. To restore the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] performance counters on the computer, run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup in repair mode.  
  
 The following table describes the performance counters.  
  
|Performance counter|Description|  
|-------------------------|-----------------|  
|BLOB bytes read|The number of bytes of binary large object (BLOB) data that the data flow engine has read from all sources.|  
|BLOB bytes written|The number of bytes of BLOB data that the data flow engine has written to all destinations.|  
|BLOB files in use|The number of BLOB files that the data flow engine currently is using for spooling.|  
|Buffer memory|The amount of memory that is in use. This may include both physical and virtual memory. When this number is larger than the amount of physical memory, the **Buffers Spooled** count rises as an indication that memory swapping is increasing. Increased memory swapping slows performance of the data flow engine.|  
|Buffers in use|The number of buffer objects, of all types, that all data flow components and the data flow engine is currently using.|  
|Buffers spooled|The number of buffers currently written to the disk. If the data flow engine runs low on physical memory, buffers not currently used are written to disk and then reloaded when needed.|  
|Flat buffer memory|The total amount of memory, in bytes, that all flat buffers use. Flat buffers are blocks of memory that a component uses to store data. A flat buffer is a large block of bytes that is accessed byte by byte.|  
|Flat buffers in use|The number of flat buffers that the Data flow engine uses. All flat buffers are private buffers.|  
|Private buffer memory|The total amount of memory in use by all private buffers. A buffer is not private if the data flow engine creates it to support data flow. A private buffer is a buffer that a transformation uses for temporary work only. For example, the Aggregation transformation uses private buffers to do its work.|  
|Private buffers in use|The number of buffers that transformations use.|  
|Rows read|The number of rows that a source produces. The number does not include rows read from reference tables by the Lookup transformation.|  
|Rows written|The number of rows offered to a destination. The number does not reflect rows written to the destination data store.|  
  
 You use the Performance Microsoft Management Console (MMC) snap-in to create a log that captures performance counters.  
  
 For information about how to improve performance, see [Data Flow Performance Features](../../integration-services/data-flow/data-flow-performance-features.md).  
  
## Obtain Performance Counter Statistics  
 For [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects that are deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, you can obtain performance counter statistics by using the [dm_execution_performance_counters &#40;SSISDB Database&#41;](../../integration-services/functions-dm-execution-performance-counters.md) function.  
  
 In the following example, the function returns statistics for a running execution with an ID of 34.  
  
```sql
select * from [catalog].[dm_execution_performance_counters] (34)  
```  
  
 In the following example, the function returns statistics for all the executions running on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
```sql
select * from [catalog].[dm_execution_performance_counters] (NULL)  
  
```  
  
> **IMPORTANT!!** If you are a member of the **ssis_admin** database role, performance statistics for all running executions are returned.  If you are not a member of the **ssis_admin** database role, performance statistics for the running executions for which you have read permissions, are returned.  
  
## Related Content  
  
-   Tool, [SSIS Performance Visualization for Business Intelligence Development Studio (CodePlex Project)](https://go.microsoft.com/fwlink/?LinkId=146626), on codeplex.com.  
  
-   Video, [Measuring and Understanding the Performance of Your SSIS Packages in the Enterprise (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=150497), on msdn.microsoft.com.  
  
-   Support article, [The SSIS performance counter is no longer available in the Performance Monitor after you upgrade to Windows Server 2008](https://go.microsoft.com/fwlink/?LinkId=235319), on support.microsoft.com.  

## Add a Log for Data Flow Performance Counters
  This procedure describes how to add a log for the performance counters that the data flow engine provides.  
  
> [!NOTE]  
>  If you install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] on a computer that is running [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)], and then upgrade that computer to [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)], the upgrade process removes the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] performance counters from the computer. To restore the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] performance counters on the computer, run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup in repair mode.  
  
### To add logging of performance counters  
  
1.  In **Control Panel**, if you are using Classic view, click **Administrative Tools**. If you are using Category view, click **Performance and Maintenance** and then click **Administrative Tools**.  
  
2.  Click **Performance**.  
  
3.  In the **Performance** dialog box, expand **Performance Logs and Alerts**, right-click **Counter Logs**, and then click **New Log Settings**. Type the name of the log. For example, type **MyLog**.  
  
4.  Click **OK**.  
  
5.  In the **MyLog** dialog box, click **Add Counters**.  
  
6.  Click **Use local computer counters** to log performance counters on the local computer, or click **Select counters from computer** and then select a computer from the list to log performance counters on the specified computer.  
  
7.  In the **Add Counters** dialog box, select **SQL Server:SSIS Pipeline** in the **Performance object** list.  
  
8.  To select performance counters, do one of the following:  
  
    -   Select **All Counters** to log all performance counters.  
  
    -   Select **Select counters in list** and select the performance counters to use.  
  
9. Click **Add**.  
  
10. Click **Close**.  
  
11. In the **MyLog** dialog box, review the list of logging performance counters in the **Counters** list.  
  
12. To add additional counters, repeat steps 5 through 10.  
  
13. Click **OK**.  
  
    > [!NOTE]  
    >  You must start the Performance Logs and Alerts service using a local account or a domain account that is a member of the Administrators group.  

## See Also  
 [Execution of Projects and Packages](../packages/run-integration-services-ssis-packages.md)
 [Events Logged by an Integration Services Package](../../integration-services/performance/events-logged-by-an-integration-services-package.md)  
