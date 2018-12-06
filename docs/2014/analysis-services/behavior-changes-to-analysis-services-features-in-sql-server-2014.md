---
title: "Behavior Changes to Analysis Services Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 92ebd5cb-afb6-4b62-968f-39f5574a452b
author: minewiskan
ms.author: owend
manager: craigg
---
# Behavior Changes to Analysis Services Features in SQL Server 2014
  This topic describes behavior changes in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] for multidimensional, tabular, data mining, and [!INCLUDE[ssGeminiShort](../includes/ssgeminishort-md.md)] deployments. Behavior changes affect how features work or interact in the current version as compared to earlier versions of SQL Server.  
  
> [!NOTE]  
>  In contrast, a breaking change is one that prevents a data model or application integrated with [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] from running. To learn more, see [Breaking Changes to Analysis Services Features in SQL Server 2014](breaking-changes-to-analysis-services-features-in-sql-server-2014.md).  
  
 In this Topic:  
  
-   [Behavior Changes in SQL Server 2014](#bkmk_sql2014)  
  
-   [Behavior Changes in SQL Server 2012 SP1](#bkmk_sql2012sp1)  
  
-   [Behavior Changes in SQL Server 2012](#bkmk_sql2012)  
  
##  <a name="bkmk_sql2014"></a> Behavior Changes in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 There are no new behavior changes announced for tabular, multidimensional, data mining, or [!INCLUDE[ssGeminiShort](../includes/ssgeminishort-md.md)] features in this release.  However, because  [!INCLUDE[ssASCurrent](../includes/ssascurrent-md.md)] is so similar to the [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] versions, behavior changes from both prior releases are provided here as a convenience in case you're upgrading from [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)].  
  
##  <a name="bkmk_sql2012sp1"></a> Behavior Changes in [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)]  
 This section documents the behavior changes reported for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features in [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)]. These changes also apply to [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
|Issue|Description|  
|-----------|-----------------|  
|SQL Server 2008 R2 PowerPivot workbooks will not silently upgrade and refresh the models when they are used in SQL Server 2012 SP1 PowerPivot for SharePoint 2013. Therefore Scheduled data refreshes will not work for SQL Server 2008 R2 PowerPivot workbooks.|The 2008 R2 workbooks will open in [!INCLUDE[ssGeminiShortvnext](../includes/ssgeminishortvnext-md.md)], however scheduled refreshes will not work. If you review the refresh history you will see an error message similar to the following:<br /> "The workbook contains an unsupported PowerPivot model. The PowerPivot model in the workbook is in the SQL Server 2008 R2 PowerPivot for Excel 2010 format. Supported PowerPivot models are the following: <br />SQL Server 2012 PowerPivot for Excel 2010<br />SQL Server 2012 PowerPivot for Excel 2013"<br /><br /> **How to upgrade a workbook:** The scheduled refreshes will not work until you upgrade the workbook to a 2012 workbook. To upgrade the workbook and model it contains, complete one of the following:<br /><br /> Download and open the workbook in Microsoft Excel 2010 with the SQL Server 2012 PowerPivot for Excel add-in installed. Then save the workbook and republish it to the SharePoint server.<br /><br /> Download and open the workbook in Microsoft Excel 2013. Then save the workbook and republish it to the SharePoint server.<br /><br /> <br /><br /> For more information on workbook upgrade, see [Upgrade Workbooks and Scheduled Data Refresh &#40;SharePoint 2013&#41;](instances/install-windows/upgrade-workbooks-and-scheduled-data-refresh-sharepoint-2013.md).|  
|Behavior change in DAX [ALL Function](https://msdn.microsoft.com/library/ee634802(v=sql.120).aspx).|Prior to [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)], if you specify a [Date] column in Mark as Date Table, for use in time-intelligence, and that [Date] column is passed as an argument to the ALL function, in-turn, passed as a filter to a CALCULATE function, all filters for all columns in the table are ignored, regardless of any slicer on the date column.<br /><br /> For example,<br /><br /> `= CALCULATE (<expression>, ALL (DateTable[Date]))`<br /><br /> Prior to [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)], all filters are ignored for all columns of DateTable, regardless of the [Date] column passed as an argument to ALL.<br /><br /> In [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] and in PowerPivot in Excel 2013, the behavior will ignore filters only for the specified column passed as an argument to ALL.<br /><br /> To work around the new behavior, in effect ignore all columns as a filter for the entire table, you can exclude [Date] column from the argument, for example,<br /><br /> `=CALCULATE (<expression>, ALL(DateTable))`<br /><br /> This will yield the same result as the behavior prior to [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)].|  
  
##  <a name="bkmk_sql2012"></a> Behavior Changes in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
 This section documents the behavioral changes reported for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] features in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]. These changes also apply to [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
### Analysis Services, Multidimensional Mode  
  
#### NullProcessing option set to Preserve is no longer supported for distinct count measures  
 Prior to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], it was possible to set [NullProcessing Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/nullprocessing-element-assl) to `Preserve` for distinct count measures.  Unfortunately, this practice often produced invalid results and sometimes even crashed the processing job. As a result, this configuration is no longer valid in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]. Attempting to use it will cause the following validation error to occur: "Errors in the metadata manager. Preserve is not a valid NullProcessing value for the \<measurename> distinct count measure."  
  
#### Cube browser in Management Studio and Cube Designer has been removed  
 The cube browser control that let you drag and drop fields onto a PivotTable structure in Management Studio or in Cube Designer has been removed from the product. The control was an Office Web Control (OWC) component. OWC was deprecated by Office and is no longer available.  
  
### PowerPivot for SharePoint  
  
#### Higher permission requirements for using a PowerPivot workbook as an external data source  
 An Excel workbook can render PowerPivot data that is embedded within the same workbook or in an external workbook. In the previous release, permission requirements were the same regardless of whether the PowerPivot data was embedded or external. If you had **View Only** permissions on a PowerPivot workbook, you could get full access to all of the PowerPivot data in the workbook for both embedded and external connections.  
  
 In this release, permission requirements have changed for Excel workbooks that render PowerPivot data from an external file. In this release, you must have **Read** permissions (or more specifically, the **Open Items** permission) to connect to an external PowerPivot workbook from a client application. The additional permissions specify that a user has download rights to view the source data embedded in the workbook. The additional permissions reflect the fact that model data is wholly available to the client application or workbook that links to it, resulting in a better alignment between permission requirements and the actual data connection behavior.  
  
 To continue using a PowerPivot workbook as an external data source, you must increase SharePoint permissions for users who connect to external PowerPivot data. Until you change the permissions, users will get the following error if they try to access PowerPivot workbooks in a data source connection: "PowerPivot Web service returned an error (Access denied. The document you requested does not exist or you do not have permission to open the file.)"  
  
> [!WARNING]  
>  The following steps instruct you to break permission inheritance at the library level and increase user permissions from **View Only** to **Read** for specific documents in this library. Before you proceed, carefully review existing permissions and documents and verify that these steps are appropriate for your site.  
>   
>  Alternatively, you can create a folder in the library, move all affected documents to that folder, and set unique permissions on the folder.  
  
> [!NOTE]  
>  If your workbooks are stored in PowerPivot Gallery, breaking permission inheritance on a workbook will disrupt thumbnail image generation for that workbook if it is configured for data refresh. To simultaneously allow access to both workbooks and preview images in the gallery, consider granting to specific users **Read** permissions at the library level, for all documents in the library.  
  
 You must be a site owner to change permissions.  
  
 **How to increase permissions to Read permission level for individual workbooks**  
  
1.  Click the down arrow to open the menu for an individual document.  
  
2.  Click **Manage Permissions**.  
  
3.  By default, a library inherits permissions. To change the permissions of individual workbooks in this library, click **Stop Inheriting Permissions**.  
  
4.  Select the checkbox by user or group names that require additional permissions on PowerPivot workbooks. Additional permissions will allow these users to link to the embedded PowerPivot data and use that data as an external data source in other documents.  
  
5.  Click **Edit User Permissions**.  
  
6.  Choose **Read** permissions, and then click **OK**.  
  
#### PowerPivot Gallery: New rules for snapshot generation for some PowerPivot workbooks  
 This release introduces new requirements for generating snapshot images in PowerPivot Gallery, eliminating a potential source of information disclosure (namely, showing a snapshot of data from a data source that you do not have permission to view). These requirements apply only to PowerPivot workbooks that connect to external data sources each time you view the workbook. If you only use workbooks that visualize embedded PowerPivot data, you will see no change in how snapshots are generated in PowerPivot Gallery.  
  
 For a workbook that refreshes its data each time it is opened, the new requirements for snapshot generation are as follows:  
  
-   PowerPivot workbooks that are used as external data sources by other workbooks or reports must be in the same library as the workbooks that consume the data. For example, if you have sales-data.xlsx that provides data to sales-report.xlsx, both workbooks must be in the gallery in order for snapshot images to appear.  
  
-   Workbooks that are used together must inherit permissions from a common parent (that is, the PowerPivot Gallery). In our example, both sales-data.xlsx and sales-report.xlsx must inherit from PowerPivot Gallery.  
  
 If a workbook fails to meet any of the above criteria, the following locked icon will appear instead of the thumbnail image you expect:  
  
 ![GMNI_PowerPivotGalleryIcon_Locked](media/gmni-powerpivotgalleryicon-locked.gif "GMNI_PowerPivotGalleryIcon_Locked")  
  
#### New default setting for load balancing requests changed from Round-Robin to Health-Based  
 A PowerPivot service application has default settings that determine how requests for PowerPivot data are distributed across multiple PowerPivot for SharePoint servers in a farm. In the previous release, the default setting was **Round Robin**, where requests were distributed sequentially among the available servers. In this release, the default is now **Health Based**. The PowerPivot service application uses server health statistics, such as available memory or CPU, to determine which server instance gets the xt request.  
  
 If you upgraded your server from the previous release, the PowerPivot service application retains the previous default setting (**Round Robin**). To use the **Health Based** allocation method setting, you must modify the configuration settings. For more information, see [Create and Configure a PowerPivot Service Application in Central Administration](power-pivot-sharepoint/create-and-configure-power-pivot-service-application-in-ca.md).  
  
## See Also  
 [Backward Compatibility](../../2014/getting-started/backward-compatibility.md)   
 [Breaking Changes to Analysis Services Features in SQL Server 2014](breaking-changes-to-analysis-services-features-in-sql-server-2014.md)  
  
  
