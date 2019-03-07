---
title: "PowerPivot Management Dashboard and Usage Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 541c8b1f-c6c2-423d-a97d-65c379967e0c
author: minewiskan
ms.author: owend
manager: craigg
---
# PowerPivot Management Dashboard and Usage Data
  PowerPivot Management Dashboard is a collection of predefined reports and web parts in SharePoint Central Administration that help you administer a SQL Server PowerPivot for SharePoint deployment. The Management Dashboard provides information related to server health, workbook activity, and data refresh. The dashboard uses data from the SharePoint usage data collection.  
  
 [Prerequisites](#prereq)  
  
 [Overview of the sections of the Dashboard](#items)  
  
 [Open PowerPivot Management Dashboard](#open)  
  
 [Source Data in Dashboards](#sourcedata)  
  
 [Edit PowerPivot Dashboard](#edit)  
  
 [Create Custom Reports for PowerPivot Management Dashboard](#reports)  
  
##  <a name="prereq"></a> Prerequisites  
 You must be a service administrator to open PowerPivot Management Dashboard for a PowerPivot service application that you manage.  
  
##  <a name="items"></a> Overview of the sections of the Dashboard  
 PowerPivot Management Dashboard contains Web Parts and embedded reports that drill down into specific information categories. The following list describes each part of the dashboard:  
  
|Dashboard|Description|  
|---------------|-----------------|  
|Infrastructure - Server Health|Shows trends in CPU usage, memory consumption, and query response times over time so that you can assess whether system resources are nearing maximum capacity or are under utilized.|  
|Actions|Contains links to other pages in Central Administration including the current service application, a list of service applications, and usage logging.|  
|Workbook Activity - Chart|Reports on frequency of data access. You can learn how often connections to PowerPivot data sources occur on a daily or weekly basis.|  
|Workbook Activity - Lists|Reports on frequency of data access. You can learn how often connections to PowerPivot data sources occur on a daily or weekly basis.|  
|Data Refresh - Recent Activity|Reports on the status of data refresh jobs, including jobs that failed to run. This report provides a composite view into data refresh operations at the application level. Administrators can see at a glance the number of data refresh jobs that are defined for the entire PowerPivot service application.|  
|Data Refresh - Recent Failures|Lists the PowerPivot workbooks that did not complete data refresh successfully.|  
|Reports|Contains links to reports that you can open in Excel.|  
  
##  <a name="open"></a> Open PowerPivot Management Dashboard  
 The dashboard shows information for one PowerPivot service application at a time. You can open the management dashboard from two different locations.  
  
### Open the dashboard from General Application Settings  
  
1.  In Central Administration, in the **General Application Settings** group, click **PowerPivot Management Dashboard**.  
  
2.  On the main page, select the PowerPivot service application for which you want to view operations data.  
  
### Open the dashboard from a PowerPivot service application  
  
1.  In Central Administration, in **Application Management**, click **Manage service applications**.  
  
2.  Click the name of the PowerPivot service application. The PowerPivot Management Dashboard displays operational data for the current service application.  
  
### Change the current service application.  
 To change current PowerPivot service application in the management dashboard:  
  
1.  At the top of the PowerPivot management dashboard, note the name of the current service application, for example **Default PowerPivot Service Application**.  
  
2.  In the **Actions** dashboard, click **List Service Applications**.  
  
3.  Click the name of the PowerPivot Service application for which you want to see management dashboard reports.  
  
##  <a name="sourcedata"></a> Source Data in Dashboards  
 Dashboards, reports and web parts show data from an internal data model that pulls data from the system and PowerPivot application databases. The internal data model is embedded in a PowerPivot workbook hosted in the Central Administration site. The structure of the data model is fixed. Although you can use the PowertPivot workbook as a data source to create new reports, you must not modify the structure in a way that breaks the predefined reports that use it.  
  
 For more information about how data is collected, see the following:  
  
-   [PowerPivot Usage Data Collection](power-pivot-usage-data-collection.md)  
  
-   [Configure Usage Data Collection for &#40;PowerPivot for SharePoint](configure-usage-data-collection-for-power-pivot-for-sharepoint.md)  
  
 To capture data about the PowerPivot server system, verify event messaging, data refresh history, and other usage history is enabled for each PowerPivot service application. The server and usage data gathered during normal server operations is the source data that ends up in the internal data model. **Note:** If you turn off events or usage history, the composite reports will be incomplete or erroneous.  
  
##  <a name="edit"></a> Edit PowerPivot Dashboard  
 If you have expertise in dashboard development or customization, you can edit the dashboard to include new web parts. You can also edit the web part properties that are included in the dashboard.  
  
##  <a name="reports"></a> Create Custom Reports for PowerPivot Management Dashboard  
 For reporting purposes, PowerPivot usage data and history is kept in an internal PowerPivot workbook that is created and configured along with the dashboard. If the default reports do not provide the information you require, you can create custom reports in Excel based on the workbook. Both the workbook and any custom reports that you create will be preserved if you upgrade or uninstall the PowerPivot solution files later. The workbook and reports are stored in the PowerPivot Management library within the Central Administration site. This library is not visible by default, but you can view the library using the View All Site Content action under Site Actions.  
  
 To help you get started with custom reporting, PowerPivot Management Dashboard provides an Office Data Connection (.odc) file to connect to the source workbook. Dor example, you can use the .odc file in Excel to create additional reports.  
  
> [!NOTE]  
>  Edit the file to avoid the following error when attempting to use the .odc file in Excel: "Initialization of the data source failed". The auto-generated .odc file includes a parameter that are not supported by the MSOLAP OLE DB provider. The following instructions provide the workaround for removing the parameters.  
  
 You must be a farm or service administrator to build reports that are based on the PowerPivot workbook in Central Administration.  
  
1.  Open the PowerPivot Management Dashboard.  
  
2.  Scroll to the **Reports** section at the bottom of the page.  
  
3.  Click **PowerPivot Management Data**.  
  
4.  Save the .odc file to a local folder.  
  
5.  Open the .odc file in a text editor.  
  
6.  In the `<odc:ConnectionString>` element, scroll to the end of the line and remove `Embedded Data=False`, and then remove `Edit Mode=0`. If the last character in the string is a semicolon, remove it now.  
  
7.  Save the File. The remaining steps depend on which version of PowerPivot and Excel you are using.  
  
8.  1.  Start Excel 2013  
  
    2.  In the **PowerPivot** ribbon, click **Manage**.  
  
    3.  Click **Get External Data** and then click **Existing connections**.  
  
    4.  Click the .ODC file if you see it. If you do not see the .ODC file, click **Browse for More** and then in the file path, specify the .odc file.  
  
    5.  Click **Open**  
  
    6.  Click **Test Connection** to verify the connection succeeds.  
  
    7.  Click type a name for the connection and then click **Next**.  
  
    8.  In Specify MDX Query, click **Design** to open the MDX query designer to assemble the data you want to work with **If you see the error message** "The Edit Mode property name is not formatted correctly.", verify you edits the .ODC file.  
  
    9. Click **OK** and then click **Finish**.  
  
    10. Create PivotTable or PivotChart reports to visualize the data in Excel.  
  
9. 1.  Start Excel 2010.  
  
    2.  On the PowerPivot ribbon, click **Launch PowerPivot Window**.  
  
    3.  On the Design ribbon in the PowerPivot window, click **Existing Connections**.  
  
    4.  Click **Browse for More**.  
  
    5.  In the file path, specify the .odc file.  
  
    6.  Click **Open**. The Table Import Wizard starts, using the connection string to the PowerPivot workbook that contains usage data.  
  
    7.  Click **Test Connection** to verify that you have access.  
  
    8.  Enter a friendly name for the connection, and then click **Next**.  
  
    9. In Specify MDX Query, click **Design** to open the MDX query designer to assemble the data you want to work with, and then create PivotTable or PivotChart reports to visualize the data in Excel.  
  
## See Also  
 [PowerPivot Data Refresh with SharePoint 2010](../powerpivot-data-refresh-with-sharepoint-2010.md)   
 [Configure Usage Data Collection for &#40;PowerPivot for SharePoint](configure-usage-data-collection-for-power-pivot-for-sharepoint.md)  
  
  
