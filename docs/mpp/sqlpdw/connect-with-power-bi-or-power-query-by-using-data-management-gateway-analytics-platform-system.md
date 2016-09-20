---
title: "Connect with Power BI or Power Query by using Data Management Gateway (Analytics Platform System)"
ms.custom: na
ms.date: 07/21/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1591aea1-127a-4bf2-9e78-afa484498a95
caps.latest.revision: 11
author: BarbKess
---
# Connect with Power BI or Power Query by using Data Management Gateway (Analytics Platform System)
Connect to Analytics Platform System with Power BI by using an OData feed from the Microsoft Data Management Gateway.  
  
## Create and use a PDW data source in Power BI  
These instructions show you how to use Power BI to bring an OData feed into Excel from a PDW database.  
  
The full documentation for creating a PowerBI data source is at [Create a Data Source and Enable OData Feed in Power BI Admin Center](http://office.microsoft.com/en-us/office365-sharepoint-online-enterprise-help/create-a-data-source-and-enable-odata-feed-in-power-bi-admin-center-HA104079172.aspx). This instructions give specifics about using an OData feed from a PDW database.  
  
#### Before you begin  
  
1.  This walkthrough requires that a secure gateway is already configured on PDW. If this is not the case, contact Microsoft Support for assistance.  
  
#### To create a PDW data source  
  
1.  Go to the Power BI Admin Center and verify the Data Management Gateway is running. To do this:  
  
    -   Open [Power BI for Office 365](http://www.microsoft.com/en-us/powerbi/default.aspx) and sign in.  
  
    -   Click the **Power BI** tile.  
  
    -   Click the cog in the upper right corner and select **Power BI Admin Center.**  
  
    -   On the gateways page, verify that the status of your gateway is *Running*.  
  
2.  Click **data sources** in the left sidebar, select **Add New Data Source** and choose **SQL Server**.  
  
3.  Check the **Enable Cloud Access** and **Enable OData Feed** options. Click next.  
  
4.  In the **connection info** window, fill in these fields:  
  
    -   **Name:** Enter the descriptive name for your data source.  
  
    -   **Description:** Optional description of the data source.  
  
    -   **Gateway:** Select your installed gateway.  
  
    -   **Data Source Type:** Enter *SQL Server*.  
  
    -   **Connect using:** Choose *Connection Properties*.  
  
    -   **Connection provider:** Select *SQL Server Native Client 11.0*  
  
    -   **Server name:** Enter the PDW Control node IP, comma, 17001. For example, *10.10.10.10,17001*.  From Power BI Desktop use a colon instead of a comma. For example, 10.10.10.10:17001.  
  
    -   **Database:** Select your PDW source database for the OData feed.  
  
    Click **set credentials** to launch a wizard that runs locally on your client.  
  
5.  The wizard will present a data source settings window. Fill in these fields:  
  
    -   **Credentials type:** Select *Database*.  
  
    -   **User name, Password:** Enter the PDW user name and password.  
  
    -   **Encrypt connection:** No checkmark, leave this unchecked.  
  
    -   **Privacy level:** Select the appropriate privacy level.  
  
    Click **test connection**.  
  
    Click OK. The wizard will take you to the data settings.  
  
6.  In **data settings**, select all the database tables to expose in the OData feed. Click **next**.  
  
7.  In **users and groups**, add all the users and groups that should have access to the data source. Click **finish**. This completes the wizard.  
  
8.  Get the OData feed URL. To do this, in the PowerBI Admin Center click on the three dots listed with your data source. You will need this in Power Query to consume data from the data source.  
  
#### To use the OData feed in Power Query  
  
1.  Open Excel and click on the **POWER QUERY** tab. If this tab doesn’t exist, you need to first install Power Query.  
  
2.  Click the **From Other Sources** drop down menu, and select **From Odata Feed**.  
  
3.  Enter the OData feed URL for your data source. Click OK.  
  
4.  You should now see your PDW database in the **Navigator** sidebar.  
  
5.  Click **Load** to populate the Excel worksheet with the table data.  
  
## Create a Power Query data source for data refresh in Power BI  
Before you can schedule a data refresh, you need to configure all data sources in your Power Query connection. Repeat these steps for each data source.  
  
The OData feed connection string can’t be used for the data refresh scenario. To get a proper connection string, we will show how to import data with a SQL Server connection and then use the connection properties to get the data refresh connection string. Your Excel workbook need to have a data model in it to enable the data refresh scenario.  
  
#### To get a Power Query connection string for data refresh  
  
1.  Import data using a SQL Server data source. To do this, open Excel, click the **POWER QUERY** tab, click the **From Database** menu, and select **From SQL Server Database**.  
  
2.  In the Microsoft SQL Database dialog, enter this information:  
  
    -   **Server:** PDW Control node IP, comma (,), and port 17001. For example, *10.10.10.10,17001*.  
  
    -   **Database (optional):** Enter your database, and click OK.  
  
3.  The wizard will present a data source settings window. Fill in these fields:  
  
    1.  **Credentials type:** Select *Database*.  
  
    2.  **User name, Password:** Enter the PDW user name and password.  
  
    3.  **Encrypt connection:** No checkmark, leave this unchecked.  
  
    4.  **Privacy level:** Select the appropriate privacy level. For more information, see [Configure a privacy level](http://office.microsoft.com/en-001/excel-help/privacy-levels-HA104009800.aspx).  
  
    Click **test connection** and verify the connection status is **Configured** with a green checkmark. Click Next.  
  
4.  Excel will populate the Navigator with names of all the tables present in the database. In the Navigator, check **Select multiple items**, check the tables you want to load, check **Load to worksheet**, check **Load to Data Model**, and click **Load**.  
  
5.  Excel will now populate the table data and create a data model.  
  
6.  You can now get the connection string. To do this:  
  
    -   Click the **Data** tab and click **Connections**.  
  
    -   Select any worksheet.  
  
    -   In the **Workbook Connections** dialog, click **Properties**.  
  
    -   In the **Connection Properties** dialog, click **Definition**.  
  
    -   The Connection string text box contains the connection string that you can use for data refresh. Copy this and save it for data refresh configuration.  
  
#### To configure a Power Query data source  
  
1.  To configure the data source, you will need the correct Power Query connection string. For information about how to obtain this, see [Scheduled Data Refresh for Power Query](http://blogs.msdn.com/b/powerbi/archive/2014/05/02/scheduled-data-refresh-for-power-query.aspx) and [Create a Data Source and Enable Cloud Access](http://office.microsoft.com/en-us/office365-sharepoint-online-enterprise-help/create-a-data-source-and-enable-cloud-access-HA104078557.aspx).  
  
2.  Go to the [PowerBI Admin Center](http://www.microsoft.com/en-us/powerbi/default.aspx)) and make sure your gateway is running.  
  
3.  In the **data sources** pane, click **new data source** and select **Power Query**.  
  
4.  In the **connection info** page, enter your Power Query connection string. Click next.  
  
5.  In the **data source info** page, enter this information:  
  
    -   **Name:** A user-friendly name for the data source. This will be used to identify the data source in the Power BI Admin Center.  
  
    -   **Description:** Optional description for the data source.  
  
    -   **Gateway:** Select a gateway to host the data source.  
  
    -   **Connection provider:** Select **.Net Framework Data Provider for SQL Server**.  
  
    -   **Connection string:** Enter your connection string if it is not there already.  
  
    Click  **credentials** to launch a wizard that runs locally on your client.  
  
6.  The wizard will present a data source settings window. Fill in these fields:  
  
    -   **Credentials type:** Select *Database*.  
  
    -   **User name, Password:** Enter the PDW user name and password.  
  
    -   **Encrypt connection:** No checkmark, leave this unchecked.  
  
    -   **Privacy level:** Select the appropriate privacy level. For more information, see [Configure a privacy level](http://office.microsoft.com/en-001/excel-help/privacy-levels-HA104009800.aspx).  
  
    Click **test connection** and verify the connection status is **Configured** with a green checkmark. Click Next.  
  
7.  In the users and groups page, add users who can access the Power Query connection. Click save.  
  
#### To schedule a Power Query data refresh  
  
1.  In Office Online, click **Power BI** and then select **team site**.  
  
2.  Upload your Excel workbook that you used to import the data. To do this, click **add** and select **Upload file**. Browse to your file and upload it.  
  
3.  To schedule the data refresh, locate your Excel workbook, click the three dots, and select **Schedule Data Refresh**.  
  
4.  In the Data refresh wizard, slide the rule to enable Refresh.  
  
5.  Select the workbooks to refresh, configure the refresh schedule, and enable notification in the respective settings.  
  
6.  Save the settings. You can save and refresh the report.  
  
## Performance Considerations  
As the number of concurrent requests for data through the Data Management Gateway increases, the response time can slow down. We recommend scheduling data refreshes during off-peak appliance usage hours to reduce the impact on PDW query execution time.  
  
The Data Management Gateway consumes CPU and memory resources on the appliance. Hence, there is a limit for the number of OData feeds that can run concurrently.  
  
## Limitations and Restrictions  
The OData protocol requires that at least one column has a NOT NULL constraint. Without this constraint, the table will not be available for OData.  
  
