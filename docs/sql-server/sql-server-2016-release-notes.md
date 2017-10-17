---
title: "SQL Server 2016 Release Notes | Microsoft Docs"
ms.date: "11/28/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "build notes"
  - "release issues"
ms.assetid: c64077a2-bec8-4c87-9def-3dbfb1ea1fb6
caps.latest.revision: 276
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# SQL Server 2016 Release Notes
  This topic describes limitations and issues with [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] .    
    
 **Try it out:**    
   
[![Download from Evaluation Center](../analysis-services/media/download.png)](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016)  Download SQL Server 2016  from the **[Evaluation Center](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016)**    
    
[![Azure Virtual Machine small](../analysis-services/media/azure-virtual-machine-small.png)](https://azure.microsoft.com/en-us/marketplace/partners/microsoft/sqlserver2016sp1standardwindowsserver2016/) Have an Azure account?  Then go **[Here](https://azure.microsoft.com/en-us/marketplace/partners/microsoft/sqlserver2016sp1standardwindowsserver2016/)** to spin up a Virtual Machine with SQL Server 2016 SP1 already installed.
    
[![Download SSMS](../ssms/download-sql-server-management-studio-ssms.md) **SSMS:** To get the latest version of SQL Server Management Studio, see **[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)**.   
    
 For information on what's new, see [What's New in SQL Server 2016](http://msdn.microsoft.com/library/8223c19b-4b0d-4b1d-a042-9a726c18e708).
    
##  <a name="bkmk_top"></a> Sections In this topic:    

-   [SQL Server 2016 Service Pack 1 (SP1) available](#bkmk_2016sp1)    
-   [SQL Server 2016 General Availability (GA)](#bkmk_2016_ga) 
-   [SQL Server 2016 Release Candidate 3 (RC3)](#bkmk_2016_rc3)     

## <a name="bkmk_2016sp1"></a>SQL Server 2016 Service Pack 1 (SP1) available
![info_tip](../sql-server/media/info-tip.png) SQL Server 2016 SP1 upgrades all editions and service levels of SQL Server 2016 to SQL Server 2016 SP1. In addition to the fixes that are listed in this article, SQL Server 2016 SP1 includes hotfixes that were included in SQL Server 2016 Cumulative Update 1 (CU1) to SQL Server 2016 CU3.
    
- [SQL Server 2016 SP1 download page](https://www.microsoft.com/en-us/download/details.aspx?id=54276)
- [SQL Server 2016 Service Pack 1 release information](https://support.microsoft.com/en-us/kb/3182545) Lists the individual bug #s and issues that were fixed or changed in SP1.
 - ![info_tip](../sql-server/media/info-tip.png) See the [SQL Server Update Center](https://msdn.microsoft.com/library/ff803383.aspx) for links and information for all supported versions, inlcuding service packs of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)] 
    
    
##  <a name="bkmk_2016_ga"></a> SQL Server 2016 Release - General Availability (GA)
-   [Database Engine (GA)](#bkmk_ga_instalpatch) 

-   [Stretch Database (GA)](#bkmk_ga_stretch)

-   [Query Store (GA)](#bkmk_ga_query_store)

-   [Product Documentation (GA)](#bkmk_ga_docs)
 
### ![repl_icon_warn](../database-engine/availability-groups/windows/media/repl-icon-warn.gif) <a name="bkmk_ga_instalpatch"></a> Install Patch Requirement (GA) 
**Issue and customer impact:** Microsoft has identified a problem that affects the Microsoft VC++ 2013 Runtime binaries that are installed as a prerequisite by SQL Server 2016. An update is available to fix this problem. If this update to the VC runtime binaries is not installed, SQL Server 2016 may experience stability issues in certain scenarios. Before you in stall SQL Server 2016, check to see if the computer needs the patch described in [KB 3164398](http://support.microsoft.com/kb/3164398). The patch is also inlcuded in [Cumulative Update Package 1 (CU1) for SQL Server 2016 RTM](https://www.microsoft.com/en-us/download/details.aspx?id=53338). 

**Resolution:** Do one of the following:

- Install [KB 3138367 - Update for Visual C++ 2013 and Visual C++ Redistributable Package](http://support.microsoft.com/kb/3138367). This is the preferred resolution. You can install this before or after you install SQL Server 2016. 

    If SQL Server 2016 is already installed, do the following steps in order:

    1.  Download the appropriate *vcredist_\*exe*.
    1.  Stop the SQL Server service for all instances of the database engine.
    1.  Install **KB 3138367**.
    1.  Reboot the computer.
 

 - Install  [KB 3164398 - Critical Update for SQL Server 2016 MSVCRT prerequisites](http://support.microsoft.com/kb/3164398).  
 
    If you use **KB 3164398**, you can install during SQL Server installation, through Microsoft Update, or from Microsoft Download Center. 

    - **During SQL Server 2016 Installation:** If the computer running SQL Server setup has internet access, SQL Server setup will check for the update as part of the overall SQL Server installation. If you accept the update, setup will download and update the binaries during installation.

    - **Microsoft Update:** The update is available from Microsoft Update as a critical non-security SQL Server 2016 update. Installing through Microsoft update, after SQL Server 2016 will require the server to be restarted following the update. 

    - **Download Center:** Finally, the update is available from the Microsoft Download Center. You can download the software for the update and install it on servers after they have SQL Server 2016. 


### <a name="bkmk_ga_stretch"></a>Stretch Database

#### Problem with a specific character in a database or table name

**Issue and customer impact:** Attempting to enable Stretch Database on a database or a table fails with an error if the name of the object includes a character that's treated as a different character when converted from lower case to upper case. An example of a character that causes this issue is the character "ƒ" (created by typing ALT+159) .

**Workaround:** If you want to enable Stretch Database on the database or the table, the only option is to rename the object and remove the problem character.

#### Problem with an index that uses the INCLUDE keyword

**Issue and customer impact:** Attempting to enable Stretch Database on a table that has an index that uses the INCLUDE keyword to include additional columns in the index fails with an error.

**Workaround:** Drop the index that uses the INCLUDE keyword, enable Stretch Database on the table, then recreate the index. If you do this, be sure to follow your organization's maintenance practices and policies to ensure minimal or no impact to users of the affected table.

### <a name="bkmk_ga_query_store"></a>Query Store

#### Problem with automatic data cleanup on editions other than Enterprise and Developer

 **Issue and customer impact:** Automatic data cleanup fails on editions other than Enterprise and Developer. 
Consequently, space used by the Query Store will grow over time until configured limit is reached, if data is not purged manually. If not mitigated, this issue will also fill up disk space allocated for the error logs, as every attempt to execute cleanup will produce a dump file. Cleanup activation period depends on the workload frequency, but it is no longer than 15 min.

 **Workaround:** If you plan to use Query Store on editions other than Enterprise and Developer, you need to explicitly turn off cleanup policies. It can be done either from SQL Server Management Studio (Database Properties page) or via Transact-SQL script:

```ALTER DATABASE <database name> SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 0), SIZE_BASED_CLEANUP_MODE = OFF)```

Additionally, consider manual cleanup options to prevent Query Store from transitioning to read-only mode. For example, run the following query to periodically clean entire data space:

```ALTER DATABASE <database name> SET QUERY_STORE CLEAR```

Also, execute the following Query Store stored procedures periodically to clean runtime statistics, specific queries or plans:

- `sp_query_store_reset_exec_stats`

- `sp_query_store_remove_plan`

- `sp_query_store_remove_query`


###  <a name="bkmk_ga_docs"></a> Product Documentation (GA) 
 **Issue and customer impact:** A downloadable version of the SQL Server 2016 documentation is not yet available. When you use Help Library Manager to attempt to **Install content from online**, you will see the SQL Server 2012 and SQL Sever 2014 documentation but there are no options for SQL Server 2016 documentation.    
    
 **Workaround:** Use one of the following:    
    
 ![Manage Help Settings for SQL Server](../sql-server/media/docs-sql2016-managehelpsettings.png "Manage Help Settings for SQL Server")    
    
-   Use the option **Choose online or local help** and configure help for "I want to use online help".    
    
-   Use the option **Install content from online** and download the SQL Server 2014 Content.    
    
 **F1 Help:** By design when you press F1 in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], the online version of the F1 Help topic is displayed in the browser. This occurs even when you have installed local Help.    
     
**Updating content:**    
In SQL Server Management Studio and Visual Studio, the Help Viewer application may freeze (hang) during the process of adding the documentation. To resolve this issue, do the following. For more information about this issue, see [Visual Studio Help Viewer freezes](https://msdn.microsoft.com/library/mt654096.aspx).    
    
* Open the %LOCALAPPDATA%\Microsoft\HelpViewer2.2\HlpViewer_SSMS16_en-US.settings | HlpViewer_VisualStudio14_en-US.settings file in Notepad and change the date in the following code to some date in the future.    
    
     
```    
     Cache LastRefreshed="12/31/2017 00:00:00"    
``` 
![horizontal_bar](../sql-server/media/horizontal-bar.png "horizontal_bar")  
##  <a name="bkmk_2016_rc3"></a> SQL Server 2016 Release Candidate 3 (RC3)    
-   [Product Documentation (RC2)](#bkmk_rc3_docs)    
-   [PolyBase (RC3)](#bkmk_rc3_polybase) 

    
###  <a name="bkmk_rc3_docs"></a> Product Documentation (RC3)    
 **Issue and customer impact:** A downloadable version of the SQL Server 2016 documentation is not yet available. When you use Help Library Manager to attempt to **Install content from online**, you will see the SQL Server 2012 and SQL Sever 2014 documentation but there are no options for SQL Server 2016 documentation.    
    
 **Workaround:** Use one of the following:    
    
 ![Manage Help Settings for SQL Server](../sql-server/media/docs-sql2016-managehelpsettings.png "Manage Help Settings for SQL Server")    
    
-   Use the option **Choose online or local help** and configure help for "I want to use online help".    
    
-   Use the option **Install content from online** and download the SQL Server 2014 Content.    
    
 **F1 Help:** By design when you press F1 in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], the online version of the F1 Help topic is displayed in the browser. This occurs even when you have installed local Help.    
     
**Updating content:**    
In SQL Server Management Studio and Visual Studio, the Help Viewer application may freeze (hang) during the process of adding the documentation. To resolve this issue, do the following. For more information about this issue, see [Visual Studio Help Viewer freezes](https://msdn.microsoft.com/library/mt654096.aspx).    
    
* Open the %LOCALAPPDATA%\Microsoft\HelpViewer2.2\HlpViewer_SSMS16_en-US.settings | HlpViewer_VisualStudio14_en-US.settings file in Notepad and change the date in the following code to some date in the future.    
    
     
```    
     Cache LastRefreshed="12/31/2017 00:00:00"    
```    
    
###  <a name="bkmk_rc3_polybase"></a> PolyBase (RC3)        
 PolyBase queries may fail after upgrade from RC1 or previous releases.    
    
 **Issue and customer impact**: After upgrading from SQL Server 2016 RC1 or previous release, PolyBase queries, import and export may fail with the following error: “Internal Query Processor Error: The query processor encountered an unexpected error during the processing of a remote query phase.”    
    
 **Workaround**    
    
-   Uninstall PolyBase. In the **Control Panel**, click **Uninstall a program**, click **Microsoft SQL Server 2016**, click **Remove**. In the Remove SQL Server 2016 wizard select the instance with the failed PolyBase installation and click **Next**. On Features, click **PolyBase Query Service for External Data**. It is not necessary to remove other features that were successfully installed. Complete the steps of Remove SQL Server 2016.    
    
-   Re-install PolyBase. Run setup, and add PolyBase feature on the same SQL Server instance.    
    
 **Applies To**: SQL Server 2016 RC3 when upgrading from RC1 or previous releases.    
 
## Additional Information
- [Installtion for SQL Server 2016](../database-engine/install-windows/installation-for-sql-server-2016.md)
    
 ![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png "MS_Logo_X-Small")    
    
  
