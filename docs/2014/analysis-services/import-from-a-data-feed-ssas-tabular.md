---
title: "Import from a Data Feed (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 0686e519-67c2-4f9b-8cd2-84a4871499ee
author: minewiskan
ms.author: owend
manager: craigg
---
# Import from a Data Feed (SSAS Tabular)
  Data feeds are one or more XML data streams that are generated from an online data source and streamed to a destination document or application. You can import data from a data feed into your model by using the Table Import Wizard.  
  
 This topic contains the following sections:  
  
-   [Understanding import from a data feed](#prereq)  
  
-   [Import data from an Azure DataMarket dataset](#azure)  
  
-   [Import data feeds from public or corporate data sources](#importdata)  
  
-   [Import data feeds from SharePoint lists](#importlist)  
  
-   [Import data feeds from Reporting Services Reports](#importreport)  
  
##  <a name="prereq"></a> Understanding import from a data feed  
 You can import data into a Tabular Model from the following types of data feeds:  
  
 **Reporting Services Report**  
 You can use a Reporting Services report that has been published to a SharePoint site or a report server as a data source in a model. When importing data from a Reporting Services Report, you must specify a report definition (.rdl) file as a data source.  
  
 **Azure DataMarket Dataset**  
 Azure DataMarket is a service that provides a single marketplace and delivery channel for information as cloud services. Azure DataMarket datasets require an account key instead of a Windows user account.  
  
 **Atom feeds**  
 The feed must be an Atom feed. RSS feeds are not supported. The feed must either be publicly available or you must have permission to connect to it under the Windows account with which you are currently logged in.  
  
 Data from a data feed is added into a model once during import. To get updated data from the feed, you can either refresh the data from the model designer, or configure a data refresh schedule for the model after it is deployed to a production instance of Analysis Services. For more information, see [Process Data &#40;SSAS Tabular&#41;](process-data-ssas-tabular.md).  
  
##  <a name="azure"></a> Import data from an Azure DataMarket dataset  
 You can import data from an Azure DataMarket as a table in your model.  
  
#### To import data from an Azure DataMarket dataset  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click the **Model** menu, and then click **Import from Data Source**. The Table Import Wizard opens.  
  
2.  On the **Connect to a Data Source** page, under **Data Feeds**, select **Azure DataMarket Dataset**, and then click **Next**.  
  
3.  On the **Connect to an Azure DataMarket dataset** page, in **Friendly Name**, type a descriptive name for the feed you are accessing. If you are importing multiple feeds or data sources, using descriptive names for the connection can help you remember how the connection is used.  
  
4.  In **Data Feed URL**, type the address for the data feed.  
  
5.  In **Security Settings**, in **Account Key**, type an account key. Account keys are used by Analysis Services to access the DataMarket subscriptions.  
  
6.  Click **Test Connection** to make sure the feed is available. Alternatively, you can also click **Advanced** to confirm that the Base URL or Service Document URL contains the query or service document that provides the feed.  
  
7.  Click **Next** to continue with the import.  
  
8.  On the **Impersonation Information** page, specify the credentials that Analysis Services will use to connect to the data source when refreshing the data, and then click **Next**. These credentials are different from the Account Key.  
  
9. In the **Select Tables and Views** page of the wizard, in the **Friendly Name** field, type a descriptive name that identifies the table that will contain this data after it is imported  
  
10. Click **Preview and Filter** to review the data and change column selections. You cannot restrict the rows that are imported in the report data feed, but you can remove columns by clearing the check boxes. Click **OK**.  
  
11. In the **Select Tables and Views** page, click **Finish**.  
  
##  <a name="importdata"></a> Import data feeds from public or corporate data sources  
 You can access public feeds or build custom data services that generate Atom feeds from proprietary or legacy database systems.  
  
#### To import data from public or corporate data feeds  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click the **Model** menu, and then click **Import from Data Source**. The Table Import Wizard opens.  
  
2.  On the **Connect to a Data Source** page, under **Data Feeds**, select **Other Feeds**, and then click **Next**.  
  
3.  On the **Connect to a Data Feed** page, type a descriptive name for the feed you are accessing. If you are importing multiple feeds or data sources, using descriptive names for the connection can help you remember how the connection is used.  
  
4.  In **Data Feed URL**, type the address for the data feed. Valid values include the following:  
  
    1.  An XML document that contains the Atom data. For example, the following URL points to a public feed on the Open Government Data Initiative web site:  
  
        ```  
        http://ogdi.cloudapp.net/v1/dc/banklocations/  
        ```  
  
    2.  An .atomsvc document that specifies one or more feeds. An .atomsvc document points to a service or application that provides one or more feeds. Each feed is specified as a base query that returns the result set.  
  
         You can specify a URL address to an .atomsvc document that is on a web server or you can open the file from a shared or local folder on your computer. You might have an .atomsvc document if you saved one to your computer while exporting a Reporting Services report, or you might have .atomsvc documents in a data feed library that someone created for your SharePoint site.  
  
        > [!NOTE]  
        >  Specifying an .atomsvc document that can be accessed through a URL address or shared folder is recommended because it gives you the option of configuring automatic data refresh for the workbook later, after the workbook is published to SharePoint. The server can re-use the same URL address or network folder to refresh data if you specify a location that is not local to your computer.  
  
5.  Click **Test Connection** to make sure the feed is available. Alternatively, you can also click **Advanced** to confirm that the Base URL or Service Document URL contains the query or service document that provides the feed.  
  
6.  Click **Next** to continue with the import.  
  
7.  On the **Impersonation Information** page, specify the credentials that Analysis Services will use to connect to the data source when refreshing the data, and then click **Next**.  
  
8.  In the **Select Tables and Views** page of the wizard, in the **Friendly Name** field, replace Data Feed Content with a descriptive name that identifies the table that will contain this data after it is imported  
  
9. Click **Preview and Filter** to review the data and change column selections. You cannot restrict the rows that are imported in the report data feed, but you can remove columns by clearing the check boxes. Click **OK**.  
  
10. In the **Select Tables and Views** page, click **Finish**.  
  
##  <a name="importlist"></a> Import data feeds from SharePoint lists  
 You can import any SharePoint list that has an **Export as Data Feed** button on the (SharePoint) ribbon. You can click this button to export the list as a feed.  
  
#### To import data feeds from a SharePoint list  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click the **Model** menu, and then click **Import from Data Source**.  
  
2.  On the **Connect to a Data Source** page, under **Data Feeds**, select **Other Data Feeds**, and then click **Next**.  
  
3.  On the **Connect to a Data Feed** page, type a descriptive name for the feed you are accessing. If you are importing multiple feeds or data sources, using descriptive names for the connection might help you remember how the connection is used.  
  
4.  In Data Feed URL, type an address to the list data service, replacing \<server-name> with the actual name of your SharePoint server:  
  
    ```  
    http://<server-name>/_vti_bin/listdata.svc  
    ```  
  
5.  Click **Test Connection** to make sure the feed is available. Alternatively, you can also click **Advanced** to confirm that the Service Document URL contains an address to the list data service.  
  
6.  Click **Next** to continue with the import.  
  
7.  On the **Impersonation Information** page, specify the credentials that Analysis Services will use to connect to the data source when refreshing the data, and then click **Next**.  
  
8.  In the **Select Tables and Views** page of the wizard, select the lists you want to import.  
  
    > [!NOTE]  
    >  You can only import lists that contain columns.  
  
9. Click **Preview and Filter** to review the data and change column selections. You cannot restrict the rows that are imported in the report data feed, but you can remove columns by clearing the check boxes. Click **OK**.  
  
10. In the **Select Tables and Views** page, click **Finish**.  
  
##  <a name="importreport"></a> Import data feeds from Reporting Services Reports  
 If you have a deployment of [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] Reporting Services, you can use the Atom rendering extension to generate a data feed from an existing report.  
  
#### To import report data from a published Reporting Services report  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click the **Model** menu, and then click **Import from Data Source**.  
  
2.  On the **Connect to a Data Source** page, under **Data Feeds**, select **Report**, and then click **Next**.  
  
3.  On the Connect to a Microsoft SQL Server Reporting Services Report page, in Friendly Connection Name, type a descriptive name for the feed you are accessing. If you are importing multiple data sources, using descriptive names for the connection might help you remember how the connection is used.  
  
4.  Click **Browse** and select a report server.  
  
     If you regularly use reports on a report server, the server might be listed in **Recent Sites and Servers**. Otherwise, in Name, type an address to a report server and click **Open** to browse the folders on the report server site. An example address for a report server might be http://\<computername>/reportserver.  
  
5.  Select the report and click **Open**. Alternatively, you can paste a link to the report, including the full path and report name, in the **Name** text box. The Table Import wizard connects to the report and renders it in the preview area.  
  
     If the report uses parameters, you must specify a parameter or you cannot create the report connection. When you do so, only the rows related to the parameter value are imported in the data feed.  
  
    1.  Choose a parameter using the list box or combo box provided in the report.  
  
    2.  Click **View Report** to update the data.  
  
        > [!NOTE]  
        >  Viewing the report saves the parameters that you selected together with the data feed definition.  
  
     Optionally, click **Advanced** to set provider-specific properties for the report.  
  
6.  Click **Test Connection** to make sure the report is available as a data feed. Alternatively, you can also click **Advanced** to confirm that the **Inline Service Document** property contains embedded XML that specifies the data feed connection.  
  
7.  Click **Next** to continue with the import.  
  
8.  On the **Impersonation Information** page, specify the credentials that Analysis Services will use to connect to the data source when refreshing the data, and then click **Next**.  
  
9. In the **Select Tables and Views** page of the wizard, select the check box next to the report parts that you want to import as data.  
  
     Some reports can contain multiple parts, including tables, lists, or graphs.  
  
10. In the **Friendly name** box, type the name of the table where you want the data feed to be saved in your model.  
  
     The name of the Reporting Service control is used by default if no name has been assigned: for example, Tablix1, Tablix2. We recommend that you change this name during import so that you can more easily identify the origin of the imported data feed.  
  
11. Click **Preview and Filter** to review the data and change column selections. You cannot restrict the rows that are imported in the report data feed, but you can remove columns by clearing the check boxes. Click **OK**.  
  
12. In the **Select Tables and Views** page, click **Finish**.  
  
## See Also  
 [Data Sources Supported &#40;SSAS Tabular&#41;](tabular-models/data-sources-supported-ssas-tabular.md)   
 [Data Types Supported &#40;SSAS Tabular&#41;](tabular-models/data-types-supported-ssas-tabular.md)   
 [Impersonation &#40;SSAS Tabular&#41;](tabular-models/impersonation-ssas-tabular.md)   
 [Process Data &#40;SSAS Tabular&#41;](process-data-ssas-tabular.md)   
 [Import Data &#40;SSAS Tabular&#41;](import-data-ssas-tabular.md)  
  
  
