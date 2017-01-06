---
title: "Connect With SQL Server Integration Services for Loading (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3ceb72b8-921c-4fa5-9822-5e6f5d0b305d
caps.latest.revision: 17
author: BarbKess
---
# Connect With SQL Server Integration Services for Loading (SQL Server PDW)
This topic describes how to create an Integration Services package that connects to SQL Server PDW for the purpose of loading data.  
  
## Contents  
  
-   [Connect with SQL Server 2008 R2 Integration Services](#Connect2008)  
  
-   [Connect with SQL Server 2012 Integration Services](#Connect2012)  
  
## <a name="Connect2008"></a>Connect with SQL Server 2008 R2 Integration Services  
  
### <a name="Before"></a>Before You Begin  
**Permissions**  
  
You will need permission to connect to SQL Server PDW in order to test the connection.  
  
**Software Prerequisites**  
  
-   SQL Server Business Intelligence Development Studio (BIDS). See [Introducing Business Intelligence Development Studio](http://msdn.microsoft.com/en-us/library/ms173767(v=sql11).aspx).  
  
-   .NET Framework 3.5 SP1 or higher. For installation details, see [Microsoft .NET Framework 3.5 Service Pack 1](http://www.microsoft.com/downloads/en/confirmation.aspx?familyId=ab99342f-5d1a-413d-8319-81da479ab0d7&displayLang=en). If you have Windows 7 or later, you already have this.  
  
-   SQL Server Native Client (SNAC) 10.0 for SQL Server 2008 R2. See [Install SQL Server Native Client &#40;SQL Server PDW&#41;](../sqlpdw/install-sql-server-native-client-sql-server-pdw.md).  
  
-   SQL Server 2008 R2Integration Services Destination Adapters for SQL Server PDW, both 32-bit and 64-bit. See [Install Integration Services Destination Adapters &#40;SQL Server PDW&#41;](../sqlpdw/install-integration-services-destination-adapters-sql-server-pdw.md).  
  
### Create an Integration Services project  
To create an Integration Services project:  
  
1.  Open SQL Server Business Intelligence Development Studio (BIDS). To do this:  
  
    -   On Windows 7, click on the Windows **Start** menu, select **All Programs**, select **Microsoft SQL Server 2008 R2**, and select **SQL Server Business Intelligence Development Studio**.  
  
    -   On Windows 8, press the Start key, select search, click Apps, find Microsoft SQL Server 2008 R2. Under Microsoft SQL Server 2008 R2, click Business Intelligence Development Tools.  
  
2.  Open an existing Integration Services project or create a new one. At least one project must exist in order to add the data flow destination. To create a project:  
  
    -   Click on the **File** menu, choose **New**, choose **Project**.  
  
        ![Create a new Integration Services Project](../sqlpdw/media/SQL_Server_PDW_IS_NewProject.png "SQL_Server_PDW_IS_NewProject")  
  
    -   In the new project window, choose Integration Services Project. This opens the package design window.  
  
### Add a data flow task that has a SQL Server PDW Destination  
  
1.  Add a data flow task to the Control Flow items.  
  
    -   In the Design window, click on the **Control Flow** tab.  
  
    -   In the Toolbox, under **Control Flow** Items, left-click the **Data Flow Task** and drag it into the Control Flow pane.  
  
2.  Add SQL Server PDW as the destination for the data flow task.  
  
    -   In the Control Flow window, double-click the data flow task. This opens the Data Flow tab.  
  
    -   In the Toolbox, under Data Flow Destinations, left-click the **SQL Server PDW Destination** and drag it into the Data Flow pane. The SQL Server PDW destination will appear in the Data Flow pane.  
  
    ![OLE DB data flow task](../sqlpdw/media/SQL_Server_PDW_IS_DataFlowTask.png "SQL_Server_PDW_IS_DataFlowTask")  
  
### Connect the SQL Server PDW Destination to Your Appliance  
Configure the SQL Server PDW Destination to connect to your appliance. To do this:  
  
1.  Double-click the **SQL Server PDW** data flow task to open the SQL Server PDW Destination Editor.  
  
2.  In the **Connection Manager** field, select **<Create new connection>**.  
  
3.  In the SQL Server PDW Connection Manager Editor window, enter the following on the General tab:  
  
    -   **Server name** for Ethernet connections: enter the IP address of the Control node cluster, followed by a comma (,), followed by port 17001. For example, 10.192.63.147,17001 .  
  
        **Server name** for InfiniBand connections: enter `<appliance-name>-SQLCTL01,17001`  
  
    -   For **User** and **Password**, enter the SQL Server PDW login and password that has permissions to load data.  
  
    -   For **Destination database name**, enter the target database on SQL Server PDW.  
  
    -   For **Staging database name**, enter the name of the staging database. This field must be empty for fastappend loading mode, and is optional for other load modes. For more information, see [Create the Staging Database &#40;SQL Server PDW&#41;](../sqlpdw/create-the-staging-database-sql-server-pdw.md).  
  
        SQL Server 2008 R2 Integration Services  
  
        ![Your appliance connection information](../sqlpdw/media/SQL_Server_PDW_IS_DestinationEditorFinished.png "SQL_Server_PDW_IS_DestinationEditorFinished")  
  
    -   Click OK to return to the Destination Editor window.  
  
    -   Select your destination table.  
  
    -   Verify the loading mode. If the loading mode is **FastAppend**, uncheck the **Roll-back load on table update or insert failure** checkbox. Click **OK**.  
  
        -   By unchecking the box, loads from the staging table to a distributed destination table are performed and committed in parallel across all distributions. This performs faster than serializing the loads, but is not transaction-safe. This is equivalent to using the **–m** option in dwloader.  
  
        -   By checking the box, loads from the staging table to a distributed destination table are performed serially across the distributions within each Compute node, and in parallel across the Compute nodes. This performs slower than parallelizing the loads, but is transaction-safe. This is equivalent to not using the **–m** option in dwloader.  
  
4.  Specify the source for the data flow.  
  
    -   Click the **Data Flow** tab and then drag a data source to the **Data Flow** pane. For example, if your data source is in a flat file, drag the **Flat File Source** to the **Data Flow** pane.  
  
    -   Double-click the box you just created. This opens the **Flat File Source Editor**.  
  
    -   Click **Connection Manager** and then click **New** to create a new connection.  
  
    -   In the **Connection manager name** box, enter a friendly name for the connection.  
  
    -   Click **Browse** and select the file to use as the data source.  
  
    -   Specify formatting information by using the options in the **Flat File Source Editor**.  
  
5.  Specify the data flow from source to destination.  
  
    -   On the **Data Flow** pane, drag the green arrow from the **Flat File Source** box to the **SQL Server PDW Destination** box.  
  
    -   Map the columns by dragging to Input and Destination.  
  
    -   Save the project.  
  
6.  Optionally, add transformations to filter, combine, and cleanse the data before you load it into the data warehouse.  
  
7.  Save the package. The project gets saved to a .dtsx file.  
  
8.  Save your changes and exit.  
  
## <a name="Connect2012"></a>Connect with SQL Server 2012 Integration Services  
  
### Before You Begin  
**Permissions**  
  
To create the Integration Services package, you need permission to connect to SQL Server PDW in order to test the connection.  
  
**Software Prerequisites**  
  
-   SQL Server Data Tools. To install the required tools, install the Client Connectivity Tools in your SQL Server 2012 installation. You already have this installed unless you unchecked Client Connectivity Tools in your SQL Server 2012 installation.  
  
-   .NET Framework 3.5 SP1 or higher. See [Microsoft .NET Framework 3.5 Service Pack 1](http://www.microsoft.com/downloads/en/confirmation.aspx?familyId=ab99342f-5d1a-413d-8319-81da479ab0d7&displayLang=en). If you have Windows 7 or later, you already have this.  
  
-   SQL Server Native Client (SNAC) 11.0. If you have SQL Server 2012, you already have this unless you unchecked Client Connectivity Tools during SQL Server 2012 setup. See [Install SQL Server Native Client &#40;SQL Server PDW&#41;](../sqlpdw/install-sql-server-native-client-sql-server-pdw.md).  
  
-   SQL Server 2012Integration Services Destination Adapters for SQL Server PDW, both 32-bit and 64-bit. See [Install Integration Services Destination Adapters &#40;SQL Server PDW&#41;](../sqlpdw/install-integration-services-destination-adapters-sql-server-pdw.md).  
  
### Create an Integration Services Project  
To create an Integration Services Project:  
  
1.  Open SQL Server Data Tools from your SQL Server 2012 installation. This opens the Visual Studio 2010 Integrated Shell.  
  
    -   On Windows 7, click on the Windows **Start** menu, select **All Programs**, select **Microsoft SQL Server 2012**, and select **SQL Server Data Tools**.  
  
    -   On Windows 8, press the Start key, select search, click Apps, locate Microsoft SQL Server 2012. Under Microsoft SQL Server 2012, click SQL Server Data Tools.  
  
    ![SQL Server Data Tools](../sqlpdw/media/SQL_Server_PDW_IS_2012_SSDT.png "SQL_Server_PDW_IS_2012_SSDT")  
  
2.  Open an existing Integration Services project or create a new one. To create a project:  
  
    -   Click on the **File** menu, choose **New**, choose **Project**.  
  
        ![New SQL Server 2012 Integration Services Project](../sqlpdw/media/SQL_Server_PDW_IS_2012_NewProject.png "SQL_Server_PDW_IS_2012_NewProject")  
  
    -   In the new project window, choose Integration Services Project and click OK.  
  
### Add a data flow task that has a SQL Server PDW Destination  
  
1.  Add a data flow task to the Control Flow pane.  
  
    -   In the Package.dtsx [Design] window, click on the **Control Flow** tab.  
  
    -   In the SSIS Toolbox, left-click the **Data Flow Task** and drag it into the Control Flow pane.  
  
2.  Add SQL Server PDW as the destination for the data flow task.  
  
    1.  In the Package.dtsx [Design] window, click on the **Data Flow** tab.  
  
    2.  In the SSIS Toolbox, under Other Destinations, left-click the **SQL Server PDW Destination** and drag it into the Data Flow pane.  
  
        ![Data Flow Tab](../sqlpdw/media/SQL_Server_PDW_IS_2012_DataFlowTask.png "SQL_Server_PDW_IS_2012_DataFlowTask")  
  
### Connect the SQL Server PDW Destination to Your Appliance  
Configure the SQL Server PDW Destination to connect to your appliance. To do this:  
  
1.  Double-click the **SQL Server PDW** data flow task to open the SQL Server PDW Destination Editor.  
  
2.  In the **Connection Manager** field, select **<Create new connection>**.  
  
3.  In the SQL Server PDW Connection Manager Editor window, enter the following on the General tab:  
  
4.  For InfiniBand connections, **Server name**: Enter <appliance-name>-SQLCTL01,17001.  
  
    For Ethernet connections, **Server name**: Enter the IP address of the Control node cluster, comma, port 17001. For example, 10.192.63.134,17001.  
  
    **User** and **Password**: Enter the SQL Server PDW login and password that has permissions to load data.  
  
    **Destination database name**: Use the drop down menu to select the target database on SQL Server PDW.  
  
    **Staging database name**: Use the drop down menu to select the staging database. This field must be empty for fastappend loading mode, and is optional for other load modes. For more information, see [Create the Staging Database &#40;SQL Server PDW&#41;](../sqlpdw/create-the-staging-database-sql-server-pdw.md).  
  
5.  Test the connection and click OK to close the connection success message. Click OK to close the SQL Server PDW Connection Manager and return to the Destination Editor window.  
  
6.  Destination table: Use the drop down menu to select your destination table.  
  
7.  Verify the loading mode. If the loading mode is **FastAppend**, uncheck the **Roll-back load on table update or insert failure** checkbox. Click **OK**.  
  
    -   By unchecking the box, loads from the staging table to a distributed destination table are performed and committed in parallel across all distributions. This performs faster than serializing the loads, but is not transaction-safe. This is equivalent to using the **–m** option in dwloader.  
  
    -   By checking the box, loads from the staging table to a distributed destination table are performed serially across the distributions within each Compute node, and in parallel across the Compute nodes. This performs slower than parallelizing the loads, but is transaction-safe. This is equivalent to not using the **–m** option in dwloader.  
  
    ![Integration Services Connection to PDW](../sqlpdw/media/SQL_Server_PDW_IS_2012_DestinationEditorFinished.png "SQL_Server_PDW_IS_2012_DestinationEditorFinished")  
  
8.  Choose your Error Handling method.  
  
9. Leave the column mapping for later when you have defined a data source.  
  
10. Click OK to return to the Data Flow Task.  
  
11. Save the package.  
  
## <a name="Next"></a>Next Steps  
The connection to SQL Server PDW is configured. You are now ready to load data with Integration Services. See [Load Data With Integration Services &#40;SQL Server PDW&#41;](../sqlpdw/load-data-with-integration-services-sql-server-pdw.md)  
  
## See Also  
[Connect With Applications &#40;SQL Server PDW&#41;](../sqlpdw/connect-with-applications-sql-server-pdw.md)  
  
