---
title: "Configure Disk Space Usage (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 201a3fda-f162-45d7-bf39-74dcb92fd0e6
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Disk Space Usage (PowerPivot for SharePoint)
  A PowerPivot for SharePoint deployment uses the disk space of the host computer to cache PowerPivot databases for faster reloads. Every PowerPivot database that is loaded in memory is first cached to disk so that it can be quickly reloaded later to service new requests. By default, PowerPivot for SharePoint uses all available disk space to cache its databases, but can modify this behavior by setting properties that limit how much disk space is used.  
  
 This topic explains how to set the limits on disk space usage.  
  
 This topic does not provide guidance for disk space management of PowerPivot databases (embedded in Excel workbooks) that are stored in content databases. PowerPivot databases can be large, thereby placing new demands on the storage capacity of the farm. In addition, if versioning is enabled, you might easily have multiple copies of the data in the same content database, further increasing the amount of disk space required for content storage. Although PowerPivot databases are an important consideration for disk management, they are not something that can be managed independently of other content that you store in a SharePoint farm. You will need to monitor disk space more closely as your business increases its use of PowerPivot workbooks. You can also track PowerPivot workbook activity in the PowerPivot Management Dashboard and remove workbooks that are no longer used.  
  
## How PowerPivot for SharePoint manages cached databases  
 To manage its cache, the PowerPivot System Service runs a background job at regular intervals to clean up unused or outdated databases that have newer versions in a content library. The purpose of the cleanup job is to unload inactive databases from memory and to delete unused, cached databases from the file system. The cleanup job is for long-term maintenance, ensuring that databases do not remain on the system indefinitely. On an active server, databases might be removed more often due to memory pressure on the server, database deletion in SharePoint, or newer versions of the database in a content library.  
  
 Although you cannot schedule the cleanup job, you can customize cache file management by setting server configuration properties that do the following:  
  
-   Set limits on the amount of disk space used by the cache.  
  
-   Specify how much data to delete when maximum disk space is reached.  
  
## How to check disk space usage  
 PowerPivot for SharePoint is installed on application servers in a SharePoint farm. Each installation has a data directory that includes a Backup folder. The Backup folder contains all of the data files that are cached by the Analysis Services instance on the computer. By default, the Backup folder can be found at the following path:  
  
 `%drive%:\Program Files\Microsoft SQL Server\MSAS10_50.PowerPivot\OLAP\Backup\Sandboxes\<serviceApplicationName>`  
  
 To check how much total disk space is used by the cache, you have to check the size of the Backup folder. There is no property in Central Administration that reports on current cache size.  
  
 The Backup folder provides common cache storage for any PowerPivot database that is loaded in memory on the local computer. If you have multiple PowerPivot service applications defined in your farm, any one of them can use the local server to load and subsequently cache PowerPivot data. Both data loading and caching are Analysis Services server operations. As such, total disk space usage is managed at the Analysis Services instance level, on the Backup folder. Configuration settings that limit disk space usage are therefore set on the single SQL Server Analysis Services instance that runs on a SharePoint application server.  
  
 The cache contains only PowerPivot databases. PowerPivot databases are stored in multiple files under a single parent folder (the Backup folder). Because PowerPivot databases are meant to be used as internal data to an Excel workbook, database names are GUID based rather than descriptive. A GUID folder under **\<serviceApplicationName>** is the parent folder of a PowerPivot database. As PowerPivot databases are loaded on the server, additional folders are created for each one.  
  
 Because PowerPivot data might be loaded on any Analysis Services instance in a farm, the same data might also be cached on multiple computers in the farm. This practice favors performance over disk space utilization, but the tradeoff is that users get faster access to data if it is already available on disk.  
  
 To immediately reduce disk space consumption, you can shut down the service and then delete a PowerPivot database from the Backup folder. Manually deleting files is a temporary measure, as a newer copy of the database will be cached again the next time the PowerPivot data is queried. Permanent solutions include limiting disk space used by the cache.  
  
 At the system level, you can create email alerts that notify you when disk space is low. Microsoft System Center includes an email alert feature. You can also use File Server Resource Manager, Task Scheduler, or PowerShell script to set up alerts. The following links provide useful information for setting up notifications about low disk space:  
  
-   [What's New in File Server Resource Manager](https://technet.microsoft.com/library/hh831746.aspx) (https://technet.microsoft.com/library/hh831746.aspx).  
  
-   [File Server Resource Manager Step-by-Step Guide for Windows Server 2008 R2](https://go.microsoft.com/fwlink/?LinkID=204875) (https://go.microsoft.com/fwlink/?LinkID=204875).  
  
-   [Setting low disk space alerts on Windows Server 2008](https://go.microsoft.com/fwlink/?LinkID=204870) ( https://go.microsoft.com/fwlink/?LinkID=204870).  
  
## How to limit the amount of disk space used for storing cached files  
  
1.  In Central Administration, in Application Management, click **Manage Services on Server**.  
  
2.  Click **SQL Server Analysis Services**.  
  
     Notice that limits are set on the Analysis Services instance that runs on the physical server, and not at the service application level. All service applications that use the local Analysis Services instance are subject to the single maximum disk space limit that is set for that instance.  
  
3.  In Disk Usage, set a value (in gigabytes) for **Total disk space** to set an upper limit on the amount of space used for caching purposes. The default is 0, which allows Analysis Services to use all available disk space.  
  
4.  In Disk Usage, in the **Delete cached databases in last 'n' hours** setting, specify last-used criteria for emptying the cache when disk space is at the maximum limit.  
  
     The default is 4 hours, meaning that all databases that have been inactive for 4 hours or more are deleted from the file system. Databases that are inactive but still in memory are unloaded and then deleted from the file system.  
  
## How to limit how long a database is kept in the cache  
  
1.  In Central Administration, in Application Management, click **Manage Service Applications**.  
  
2.  Click **Default PowerPivot Service Application** to open the management dashboard.  
  
3.  In Actions, click **Configure service application settings**.  
  
4.  In the Disk Cache section, you can specify how long an inactive database stays in memory to service new requests (by default, 48 hours) and how long it stays in the cache (by default, 120 hours).  
  
     **Keep Inactive Database in Memory** specifies how long an inactive database stays in memory to service new requests for that data. An active database is always kept in memory as long as you are querying it, but after it is no longer active, the system will keep the database in memory for an additional time period in case there are more requests for that data.  
  
     Because PowerPivot databases are cached first and then loaded in memory, database files consume disk space immediately. However, while the database is active (and for 48 hours after that), all requests are directed to the in-memory database first, ignoring the cached database. After 48 hours of inactivity, the file is unloaded from memory, but stays in the cache where it can be quickly reloaded if a new connection request for that data is intercepted by the local PowerPivot server instance. Connection requests to an inactive database are served from the cache rather than the content library, minimizing impact on the content databases.  
  
     It is important to note that the content library is the only permanent location for PowerPivot databases. Cached copies are used only if the database in the library is the same as the copy on disk.  
  
     **Keep Inactive Database in Cache** specifies how long an inactive database remains on the file system after it has been unloaded from memory. The cleanup job uses this setting to determine which files to delete. All PowerPivot databases that are inactive for 168 hours (48 hours in memory and 120 hours in the cache) are deleted from disk by the cleanup job.  
  
5.  Click **OK** to save your changes.  
  
## Next Steps  
 A PowerPivot for SharePoint installation provides health rules so that you can take corrective action when problems are detected in server health, configuration, or availability. Some of these rules use configuration settings to determine the conditions under which health rules are triggered. If you are actively tuning server performance, you might also want to review these settings to ensure the defaults are the best choice for your system. For more information, see [PowerPivot Health Rules - Configure](configure-power-pivot-health-rules.md).  
  
## See Also  
 [PowerPivot Server Administration and Configuration in Central Administration](power-pivot-server-administration-and-configuration-in-central-administration.md)  
  
  
