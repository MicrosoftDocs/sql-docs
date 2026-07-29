---
title: "Load Data into SQL Server or Azure SQL Database with SQL Server Integration Services (SSIS)"
description: Shows you how to create a SQL Server Integration Services (SSIS) package to move data from a wide variety of data sources to SQL Server or to Azure SQL Database.
ms.reviewer: randolphwest
ms.date: 01/14/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
ms.custom:
  - loading
  - linux-related-content
---
# Load data into SQL Server or Azure SQL Database with SQL Server Integration Services (SSIS)

[!INCLUDE [sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

Create a SQL Server Integration Services (SSIS) package to load data into SQL Server or [Azure SQL Database](/azure/sql-database/). You can optionally restructure, transform, and cleanse the data as it passes through the SSIS data flow.

This article shows you how to do the following things:

- Create a new Integration Services project in Visual Studio.
- Design an SSIS package that loads data from the source into the destination.
- Run the SSIS package to load the data.

## Basic concepts

The package is the basic unit of work in SSIS. Related packages are grouped in projects. You create projects and design packages in Visual Studio with SQL Server Data Tools. The design process is a visual process in which you drag and drop components from the Toolbox to the design surface, connect them, and set their properties. After you finish your package, you can run it, and you can optionally deploy it to SQL Server or SQL Database for comprehensive management, monitoring, and security.

A detailed introduction to SSIS is beyond the scope of this article. To learn more, see the following articles:

- [SQL Server Integration Services](sql-server-integration-services.md)
- [How to Create an ETL Package](ssis-how-to-create-an-etl-package.md)

## About the solution

The solution is a typical package which uses a Data Flow task that contains a source and a destination. This approach supports a wide range of data sources, including SQL Server and Azure SQL Database.

This tutorial uses SQL Server as the data source. SQL Server runs on premises or on an Azure virtual machine.

To connect to SQL Server and to SQL Database, you can use an ADO.NET connection manager and source and destination, or an OLE DB connection manager and source and destination. This tutorial uses ADO.NET because it has the fewest configuration options. OLE DB might provide slightly better performance than ADO.NET.

As a shortcut, you can use the SQL Server Import and Export Wizard to create the basic package. Then, save the package, and open it in Visual Studio or SSDT to view and customize it. For more info, see [Import and Export Data with the SQL Server Import and Export Wizard](import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md).

## Prerequisites

To step through this tutorial, you need the following things:

1. **SQL Server Integration Services (SSIS)**. SSIS is a component of SQL Server and requires a licensed version, or the developer or evaluation version, of SQL Server. To get an evaluation version of SQL Server, see [Evaluate SQL Server](https://www.microsoft.com/evalcenter/evaluate-sql-server-2017-rtm).

1. **Visual Studio** (optional). To get the free Visual Studio Community Edition, see [Visual Studio Community](https://www.visualstudio.com/products/visual-studio-community-vs.aspx). If you don't want to install Visual Studio, you can install SQL Server Data Tools (SSDT) only. SSDT installs a version of Visual Studio with limited functionality.

1. **SQL Server Data Tools for Visual Studio (SSDT)**. To get SQL Server Data Tools for Visual Studio, see [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md).

1. This tutorial connects to a SQL Server or a SQL Database instance and loads data into it. You have to have permission to connect, to create a table, and to load data on one of the following destinations:

   - **An Azure SQL Database database**. For more info, see [Azure SQL Database](/azure/sql-database/).

     or

   - **A SQL Server instance**. SQL Server runs on premises or on an Azure virtual machine. To download a free evaluation or developer edition of SQL Server, see [SQL Server downloads](https://www.microsoft.com/sql-server/sql-server-downloads).

1. **Sample data**. This tutorial uses sample data stored in SQL Server in the AdventureWorks sample database as the source data. To get the AdventureWorks sample database, see [AdventureWorks Sample Databases](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks).

1. **A firewall rule** if you're loading data into SQL Database. You have to create a firewall rule on SQL Database with the IP address of your local computer before you can upload data to the SQL Database.

## Create a new Integration Services project

1. Launch Visual Studio.

1. On the **File** menu, select **New > Project**.

1. Navigate to the **Installed > Templates > Business Intelligence > Integration Services** project types.

1. Select **Integration Services Project**. Provide values for **Name** and **Location**, and then select **OK**.

Visual Studio opens and creates a new Integration Services (SSIS) project. Then Visual Studio opens the designer for the single new SSIS package (Package.dtsx) in the project. You see the following screen areas:

- On the left, the **Toolbox** of SSIS components.

- In the middle, the design surface, with multiple tabs. You typically use at least the **Control Flow** and the **Data Flow** tabs.

- On the right, the **Solution Explorer** and the **Properties** panes.

  :::image type="content" source="media/load-data-to-sql-database-with-ssis/ssis-designer-01.png" alt-text="Screenshot of Visual Studio showing the Toolbox pane, the design pane, the Solution Explorer pane, and the Properties pane.":::

## Create the basic data flow

1. Drag a Data Flow Task from the Toolbox to the center of the design surface (on the **Control Flow** tab).

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/ssis-data-flow-task-02.png" alt-text="Screenshot of Visual Studio showing a Data Flow Task being dragged into the Control Flow tab of the design pane.":::

1. Double-click the Data Flow Task to switch to the Data Flow tab.

1. From the Other Sources list in the Toolbox, drag an ADO.NET Source to the design surface. With the source adapter still selected, change its name to **SQL Server source** in the **Properties** pane.

1. From the Other Destinations list in the Toolbox, drag an ADO.NET Destination to the design surface under the ADO.NET Source. With the destination adapter still selected, change its name to **SQL destination** in the **Properties** pane.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/source-destination-09.png" alt-text="Screenshot of a destination adapter being dragged to a location directly below the source adapter.":::

## Configure the source adapter

1. Double-click the source adapter to open the **ADO.NET Source Editor**.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/ado-net-source-03.png" alt-text="Screenshot of the ADO.NET Source Editor. The Connection Manager tab is visible, and controls are available for configuring data flow properties.":::

1. On the **Connection Manager** tab of the **ADO.NET Source Editor**, select the **New** button next to the **ADO.NET connection manager** list to open the **Configure ADO.NET Connection Manager** dialog box and create connection settings for the SQL Server database from which this tutorial loads data.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/ado-net-connection-manager-04.png" alt-text="Screenshot of the Configure ADO.NET Connection Manager dialog box. Controls are available for setting up and configuring connection managers.":::

1. In the **Configure ADO.NET Connection Manager** dialog box, select the **New** button to open the **Connection Manager** dialog box and create a new data connection.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/ado-net-connection-05.png" alt-text="Screenshot of the Connection Manager dialog box. Controls are available for configuring a data connection.":::

1. In the **Connection Manager** dialog box, do the following things.

   1. For **Provider**, select the SqlClient Data Provider.

   1. For **Server name**, enter the SQL Server name.

   1. In the **Log on to the server** section, select or enter authentication information.

   1. In the **Connect to a database** section, select the AdventureWorks sample database.

   1. Select **Test Connection**.

      :::image type="content" source="media/load-data-to-sql-database-with-ssis/test-connection-06.png" alt-text="Screenshot of a dialog box displaying an OK button and text that indicates that the test connection succeeded.":::

   1. In the dialog box that reports the results of the connection test, select **OK** to return to the **Connection Manager** dialog box.

   1. In the **Connection Manager** dialog box, select **OK** to return to the **Configure ADO.NET Connection Manager** dialog box.

1. In the **Configure ADO.NET Connection Manager** dialog box, select **OK** to return to the **ADO.NET Source Editor**.

1. In the **ADO.NET Source Editor**, in the **Name of the table or the view** list, select the **Sales.SalesOrderDetail** table.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/ado-net-source-07.png" alt-text="Screenshot of the ADO.NET Source Editor. In the Name of the table or the view list, the Sales.SalesOrderDetail table is selected.":::

1. Select **Preview** to see the first 200 rows of data in the source table in the **Preview Query Results** dialog box.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/preview-data-08.png" alt-text="Screenshot of the Preview Query Results dialog box. Several rows of sales data from the source table are visible.":::

1. In the **Preview Query Results** dialog box, select **Close** to return to the **ADO.NET Source Editor**.

1. In the **ADO.NET Source Editor**, select **OK** to finish configuring the data source.

## Connect the source adapter to the destination adapter

1. Select the source adapter on the design surface.

1. Select the blue arrow that extends from the source adapter and drag it to the destination editor until it snaps into place.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/connect-source-destination-10.png" alt-text="Screenshot showing the source and destination adapters. A blue arrow points from the source adapter to the destination adapter.":::

   In a typical SSIS package, you use several other components from the SSIS Toolbox in between the source and the destination to restructure, transform, and cleanse your data as it passes through the SSIS data flow. To keep this example as simple as possible, we're connecting the source directly to the destination.

## Configure the destination adapter

1. Double-click the destination adapter to open the **ADO.NET Destination Editor**.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/ado-net-destination-11.png" alt-text="Screenshot of the ADO.NET Destination Editor. The Connection Manager tab is visible and contains controls for configuring data flow properties." lightbox="media/load-data-to-sql-database-with-ssis/ado-net-destination-11.png":::

1. On the **Connection Manager** tab of the **ADO.NET Destination Editor**, select the **New** button next to the **Connection manager** list to open the **Configure ADO.NET Connection Manager** dialog box and create connection settings for the database into which this tutorial loads data.

1. In the **Configure ADO.NET Connection Manager** dialog box, select the **New** button to open the **Connection Manager** dialog box and create a new data connection.

1. In the **Connection Manager** dialog box, do the following things.

   1. For **Provider**, select the SqlClient Data Provider.

   1. For **Server name**, enter the name of the SQL Server or of the SQL Database server.

   1. In the **Log on to the server** section, select **Use SQL Server authentication** and enter authentication information.

   1. In the **Connect to a database** section, select an existing database.

      1. Select **Test Connection**.

      1. In the dialog box that reports the results of the connection test, select **OK** to return to the **Connection Manager** dialog box.

      1. In the **Connection Manager** dialog box, select **OK** to return to the **Configure ADO.NET Connection Manager** dialog box.

1. In the **Configure ADO.NET Connection Manager** dialog box, select **OK** to return to the **ADO.NET Destination Editor**.

1. In the **ADO.NET Destination Editor**, select **New** next to the **Use a table or view** list to open the **Create Table** dialog box to create a new destination table with a column list that matches the source table.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/destination-query-before-12a.png" alt-text="Screenshot of the Create Table dialog box. S Q L code for creating a destination table is visible.":::

1. In the **Create Table** dialog box, do the following things.

   1. Change the name of the destination table to **SalesOrderDetail**.

      :::image type="content" source="media/load-data-to-sql-database-with-ssis/destination-query-after-12b.png" alt-text="Screenshot of the Create Table dialog box. S Q L code is visible for creating a table named SalesOrderDetail.":::

   1. Select **OK** to create the table and return to the **ADO.NET Destination Editor**.

1. In the **ADO.NET Destination Editor**, select the **Mappings** tab to see how columns in the source are mapped to columns in the destination.

   :::image type="content" source="media/load-data-to-sql-database-with-ssis/column-mapping-13.png" alt-text="Screenshot of the Mappings tab of the ADO.NET Destination Editor. Lines connect columns with identical names in the source and destination tables.":::

1. Select **OK** to finish configuring the destination.

## Run the package to load the data

Run the package by selecting the **Start** button on the toolbar or by selecting one of the **Run** options on the **Debug** menu.

The following paragraphs describe what you see if you created the package with the second option described in this article, that is, with a data flow containing a source and destination.

As the package begins to run, you see yellow spinning wheels to indicate activity and the number of rows processed so far.

:::image type="content" source="media/load-data-to-sql-database-with-ssis/package-running-14.png" alt-text="Screenshot showing the source and destination adapters. Yellow, spinning wheels are over each adapter, and the text '89748 rows' is between them.":::

When the package has finished running, you see green check marks to indicate success and the total number of rows of data loaded from the source to the destination.

:::image type="content" source="media/load-data-to-sql-database-with-ssis/package-success-15.png" alt-text="Screenshot showing the source and destination adapters. Green check marks are over each adapter, and the text '121317 rows' is between them.":::

Congratulations, you've successfully used SQL Server Integration Services to load data into SQL Server or Azure SQL Database.

## Related content

- [Troubleshooting Tools for Package Development](troubleshooting/troubleshooting-tools-for-package-development.md)
- [Deploy Integration Services (SSIS) Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md)
