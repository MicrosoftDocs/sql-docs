---
title: "Use an Office Data Connection (.odc) with Reports | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
helpviewer_keywords: 
  - "Office Data Connection (.odc) files"
  - "SharePoint integration [Reporting Services], shared data sources"
  - ".odc files"
ms.assetid: e8d6896d-f886-4390-8b5d-96f0a50c250c
author: maggiesMSFT
ms.author: maggies
---
# Use an Office Data Connection (.odc) with Reports
  For limited scenarios, you can use an existing Office Data Connection (.odc) file to provide connection information to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report. An .odc file can be used in place of an .rsds file when you create a shared data source. The report server uses an .odc file in the same way it uses an .rsds file; it reads the file for the data source type, a connection string, and credential information.  
  
 Not all .odc files can be used with a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report. The data processing extension and characteristics of the report and .odc file determine whether an .odc can be used:  
  
-   The report must be designed to work with an OLE DB or ODBC data provider. If you used a different data processing extension to create the report, the report or its queries might include functionality that is not supported by the OLE DB or ODBC data provider.  
  
-   The .odc file must have the expected elements and structure. The data provider and credential settings must be set explicitly in the file so that they can be read by the report server. The best way to set these values is to export the .odc file before uploading it to the SharePoint library.  
  
-   The .odc file must specify a connection type of OLE DB or ODBC.  
  
-   The .odc file must specify a connection string.  
  
-   Credentials can be set to **None**, **Stored**, or **Integrated**. If the credentials method is set to **Stored**, the report server will prompt the user for credentials instead of using the stored credentials. The report server cannot use stored credentials as defined in the .odc file.  
  
-   The data source must have schema that is identical to the one used to create the report. If the data structures are different, the report will not run.  
  
-   The .odc file must be created in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office 2007 (older versions of .odc are not compatible with report definition files).  
  
 You cannot use .odc files that specify connections to data sources that cannot be processed on a report server, even if the .odc data source types look similar to supported data source types. Specifically, if you created an .odc file in Microsoft Excel 2007 that retrieves data from Microsoft Access, the Web, or a text file, you cannot use that .odc file to provide data to a report.  
  
 Report Builder reports and models do not work with .odc file. You cannot use an .odc file to generate a model, and you cannot configure the model to use a shared data source that links to an .odc file.  
  
 If you are not familiar with .odc files, you can use the following instructions to create and export one. One easy way to create an .odc file for an OLE DB data source is to use Excel 2007 and the Data Connection Wizard. Note that the wizard does not create a data source; you must have an external data source that is already defined.  
  
 An existing .odc file should only be used if it is fully compatible with the report and queries. If you run into errors that require significant modifications to either the report or to the .odc file, you should create a new .rsds file for the report. For more information about how to create a shared data source that uses an .rsds file, see [Create and Manage Shared Data Sources &#40;Reporting Services in SharePoint Integrated Mode&#41;](https://msdn.microsoft.com/library/2d3428e4-a810-4e66-a287-ff18e57fad76).  
  
### To create and export an .odc file  
  
1.  Start Excel 2007.  
  
2.  On the **Data** tab, in the **Get External Data** group, click **From Other Sources**, and then click **From Data Connection Wizard**.  
  
3.  Select **Other/Advanced**, and then click **Next**.  
  
4.  Select **Microsoft OLE DB Provider for SQL Server**, and then click **Next**.  
  
5.  Enter the name of the server (by default, it is the network name of the computer) and a user account that has a valid login and database permissions. Click **Next**.  
  
6.  Select a database, and then click **OK** to close the **Data Link** dialog box.  
  
7.  The **Connect to specific table** check box is selected by default. It is used to retrieve data from a specific table. The report server ignores all queries in an .odc file, so it does not matter whether you select or clear the check box. Queries that retrieve data for a report are included in a report definition file and not in external files.  
  
8.  While the connection is open, you can edit properties and export it. On the **Data** tab, in the **Connections** group, click **Properties**, and then click the **Connection Properties** button next to the connection name.  
  
9. On the **Definition** tab, click **Export Connection File**.  
  
10. Enter a name for the file, and then click **Save**. Close the application and all open files.  
  
### To upload and use an .odc file  
  
1.  Open the library into which you want to upload the connection file.  
  
2.  On the **Upload** menu, click **Upload document**.  
  
3.  Click **Browse**.  
  
4.  Select the .odc file you created. By default, the .odc file is in the My Documents folder, in My Data Sources.  
  
5.  Click **Open** to select the file, click **OK** to save the selection. The properties page for the new item opens automatically.  
  
6.  In Content Type, select **Report Data Source**, and then click **OK**.  
  
7.  Point to a report.  
  
8.  Click the down arrow, and select **Manage Data Sources**.  
  
9. Click the data source name.  
  
10. If the report uses custom data source information, click **Shared**.  
  
11. In **Data Source Link**, click the browse (**...**) button.  
  
12. Select the .odc file you just uploaded.  
  
13. Click **OK** to select the file, and then click **OK** to save your changes.  
  
     If you are trying these steps with the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database and sample reports, be aware that only the Company Sales report will work out-of-the-box with an .odc file. The other sample reports contain query parameters and features that do not work with the OLE DB provider. However, you can make the reports work with the OLE DB provider if you modify them first in Report Designer.  
  
## See Also  
 [Create, Modify, and Delete Shared Data Sources &#40;SSRS&#41;](../../reporting-services/report-data/create-modify-and-delete-shared-data-sources-ssrs.md)  
  
  
