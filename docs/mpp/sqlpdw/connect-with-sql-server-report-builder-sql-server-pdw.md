---
title: "Connect With SQL Server Report Builder (SQL Server PDW)"
ms.custom: na
ms.date: 07/21/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e6f59b01-df08-4a2c-95b8-668da022fdb1
caps.latest.revision: 11
author: BarbKess
---
# Connect With SQL Server Report Builder (SQL Server PDW)
This topic describes how to connect to SQL Server PDW with SQL Server 2008 R2 Report Builder 3.0 and SQL Server 2014 Report Builder.  
  
## <a name="BackToTop"></a>Contents  
  
-   [Before You Begin](#BeforeBegin)  
  
-   [Create a Data Source (SQL Server 2008 R2 Reporting Services)](#DataSource2)  
  
-   [Create a Data Source (SQL Server 2012 Reporting Services)](#DataSource3)  
  
## <a name="BeforeBegin"></a>Before You Begin  
**Software Prerequisites for SQL Server 2008 R2**  
  
1.  SQL Server 2008 R2 Report Builder 3.0. You can use the Report Builder 3.0 standalone version or the ClickOnce version of Report Builder 3.0 installed with Reporting Services. To install the standalone package, see [Download Microsoft SQL Server 2008 R2 Report Builder 3.0](http://www.microsoft.com/en-us/download/details.aspx?id=6116) on the Microsoft Download Center. For more information, see [Installing, Uninstalling and Supporting Report Builder](http://technet.microsoft.com/en-us/library/dd207038(v=sql.105).aspx)  
  
2.  .NET Framework 3.5 SP1 or higher. If you have Windows 7 or later, you already have this.  
  
**Software Prerequisites for SQL Server 2012**  
  
1.  SQL Server 2014 Report Builder. You can use the Report Builder standalone version or the ClickOnce version of Report Builder installed with Reporting Services. To install the standalone package, see [Download Microsoft SQL Server 2012 Report Builder](http://www.microsoft.com/en-us/download/details.aspx?id=29072) on the Microsoft Download Center. For more information, see [Installing, Uninstalling and Supporting Report Builder](http://technet.microsoft.com/en-us/library/dd207038(v=sql.110).aspx)  
  
2.  .NET Framework 3.5 SP1 or higher. If you have Windows 7 or later, you already have this.  
  
[Contents](#BackToTop)  
  
## <a name="DataSource2"></a>Create a SQL Server Data Source (SQL Server 2008 R2 Report Builder 3.0)  
Create a Report Builder project.  
  
***To create an Report Builder 3.0 connection***  
  
1.  Open Report Builder. To do this, in Windows click **Start**, choose **All Programs**, choose **SQL Server 2008 R2 Report Builder 3.0**, and then select **Report Builder 3.0**.  
  
2.  Add a data source. To do this, in the **Report Data** menu, select **New**, and choose **Data Source…**.  
  
3.  In the **Data Source Properties** window, choose **General**.  
  
    ![Report Builder 3.0 Data Source Properties](../sqlpdw/media/SQL_Server_PDW_RB_DataSourceEmpty.png "SQL_Server_PDW_RB_DataSourceEmpty")  
  
4.  Fill in the General properties with the following selections:  
  
    -   **Name**: Provide a name for this data source.  
  
    -   Choose **Use a connection embedded in my report**.  
  
    -   **Select connection type:** Choose **Microsoft SQL Server**.  
  
    -   To create the Connection String, click **Build…**.  
  
5.  In the Connection Properties window, make the following selections:  
  
    -   **Data source:** Microsoft SQL Server (SqlClient)  
  
    -   **Server name:** Enter the IP address of the Control node cluster, followed by a comma, followed by the port name of 17001. For example 10.192.63.147,17001.  
  
    -   Choose **Use Windows Authentication**, or choose **Use SQL Server Authentication**.  
  
    -   **User name** and **Password** fields: enter your SQL Server PDW login and password if using SQL Server Authentication.  
  
    -   **Select or enter a Database Name**: Select or enter the name of the target database.  
  
        ![Report Builder 3.0 Connection Properties](../sqlpdw/media/SQL_Server_PDW_RB_Connection_Properties.png "SQL_Server_PDW_RB_Connection_Properties")  
  
6.  Click **Test Connection** to verify that the new data source is available.  
  
7.  Click OK.  
  
8.  You can now view your connection string in the Data Source Properties windows.  
  
    ![Report Builder 3.0 Connection Properties](../sqlpdw/media/SQL_Server_PDW_RB_DataSourceProperties.png "SQL_Server_PDW_RB_DataSourceProperties")  
  
9. Click **OK** to return to the Report Builder window.  
  
10. In the **Report Data** pane, view your new connection listed under Data Sources.  
  
## <a name="DataSource3"></a>Create a SQL Server Data Source (SQL Server 2012 Report Builder)  
Create a Report Builder project.  
  
***To create an Report Builder connection***  
  
1.  Open Report Builder standalone. To do this, in Windows click **Start**, click **All Programs**, click **SQL Server 2012 Report Builder**, and then click **Report Builder**.  
  
2.  Add a data source. To do this, in the **Report Data** pane, select **New**, and choose **Data Source…**.  
  
3.  In the **Data Source Properties** window, choose **General**.  
  
    ![Report Builder 3.0 Data Source Properties](../sqlpdw/media/SQL_Server_PDW_RB_DataSourceEmpty.png "SQL_Server_PDW_RB_DataSourceEmpty")  
  
4.  Fill in the General properties with the following selections:  
  
    -   **Name**: Provide a name for this data source.  
  
    -   Choose **Use a connection embedded in my report**.  
  
    -   **Select connection type:** Choose **Microsoft SQL Server**.  
  
    -   To create the Connection String, click **Build…**.  
  
5.  In the **Connection Properties** window, make the following selections:  
  
    -   **Data source:** Microsoft SQL Server (SqlClient)  
  
    -   **Server name:** Enter the IP address of the Control node cluster, followed by a comma, followed by the port name of 17001. For example 10.192.63.147,17001.  
  
    -   Choose **Use Windows Authentication**, or choose **Use SQL Server Authentication**.  
  
    -   **User name** and **Password** fields: enter your SQL Server PDW login and password if using SQL Server Authentication.  
  
    -   **Select or enter a Database Name**: Select or enter the name of the target database.  
  
        ![Report Builder 3.0 Connection Properties](../sqlpdw/media/SQL_Server_PDW_RB_Connection_Properties.png "SQL_Server_PDW_RB_Connection_Properties")  
  
6.  Click **Test Connection** to verify that the new data source is available.  
  
7.  Click **OK**.  
  
8.  You can now view your connection string in the **Data Source Properties** windows.  
  
    ![Report Builder 3.0 Connection Properties](../sqlpdw/media/SQL_Server_PDW_RB_DataSourceProperties.png "SQL_Server_PDW_RB_DataSourceProperties")  
  
9. Click **OK** to return to the Report Builder window.  
  
10. In the **Report Data** pane, view your new connection listed under Data Sources.  
  
## See Also  
[Connecting to SQL Server PDW (SQL Server PDW)](assetId:///721851d5-e521-4d5b-ba6d-8e2e9d3c7808)  
  
