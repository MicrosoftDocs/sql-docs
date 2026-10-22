---
title: Connect with Excel
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Fabric SQL database
description: Learn how to connect Microsoft Excel to a database. Import data into Excel for reporting and data exploration.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf
ms.date: 01/06/2026
ms.service: azure-sql
ms.subservice: connect
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi || = fabricsql"
ms.custom:
  - sqldbrb=1
  - sfi-image-nochange
---

# Connect Excel to a database and create a report

[!INCLUDE[appliesto-sqldb-sqlmi-fabricsqldb](../includes/appliesto-sqldb-sqlmi-fabricsqldb.md)]

You can connect Excel to a database and then import data and create tables and charts based on values in the database. In this tutorial you will set up the connection between Excel and a database table, save the file that stores data and the connection information for Excel, and then create a pivot chart from the database values.

You'll need to create a database before you get started. If you don't have one:

- Follow [Create a database in Azure SQL Database](single-database-create-quickstart.md) and [Create server-level IP firewall](firewall-create-server-level-portal-quickstart.md) to get a database with sample data running in a few minutes. To create a free database for the life of your subscription, see [Deploy Azure SQL Database for free](free-tier.md).
- You can also [try Azure SQL Managed Instance for free](../managed-instance/free-offer.md).
- You can also [create a SQL database in Microsoft Fabric in the Fabric portal](/fabric/database/sql/create).

In this article, you'll import sample data into Excel from that article, but you can follow similar steps with your own data.

You'll also need a copy of Excel. This article uses [Microsoft Excel 2016](https://www.microsoft.com/microsoft-365).

## Connect Excel and load data

1. To connect Excel to a database in SQL Database, open Excel and then create a new workbook or open an existing Excel workbook.
1. In the menu bar at the top of the page, select the **Data** tab, select **Get Data**, select From Azure, and then select **From Azure SQL Database**.

   :::image type="content" source="media/connect-excel/excel-data-source.png" alt-text="Screenshot of the screen to Select data source: Connect Excel to SQL Database.":::

1. In the **SQL Server database** dialog box, type the **Server name** you want to connect to.
    - In Azure SQL Database, this looks like: `<servername>.database.windows.net`. 
    - In Azure SQL Managed Instance, this looks like: `<your-instance-name>.<unique-dns-prefix>.database.windows.net`.
    - In SQL database in Fabric, this looks like: `<server-unique-identifier>.<tenant>.fabric.microsoft.com`.
1. Optionally, enter in the name of your database. Select **OK** to open the credentials window.
1. In the **SQL Server database** dialog box, select **Database** on the left side, and then enter in your **User Name** and **Password** for the server you want to connect to. Select **Connect** to open the **Navigator**.

   > [!TIP]
   > Depending on your network environment, you might not be able to connect if the server doesn't allow traffic from your client IP address. Go to the [Azure portal](https://portal.azure.com/), select SQL servers, select your server, select firewall under settings and add your client IP address. For more information, see [IP firewall rules](firewall-configure.md).

1. In the **Navigator**, select the database you want to work with from the list, select the tables or views you want to work with (we chose `vGetAllCategories`), and then select **Load** to move the data from your database to your Excel spreadsheet.

## Import the data into Excel and create a pivot chart

Now that you've established the connection, you have several different options with how to load the data. For example, the following steps create a pivot chart based on the data found in your database in SQL Database.

1. Follow the steps in the previous section, but this time, instead of selecting **Load**, select **Load to** from the **Load** dropdown list.
1. Next, select how you want to view this data in your workbook. We chose **PivotChart**. You can also choose to create a **New worksheet** or to **Add this data to a Data Model**. For more information on Data Models, see [Create a data model in Excel](https://support.microsoft.com/office/create-a-data-model-in-excel-87e7a54c-87dc-488e-9410-5c75dbcb0f7b).

    :::image type="content" source="media/connect-excel/import-data.png" alt-text="Screenshot from Excel. Shows steps to choosing the format for data in Excel." lightbox="media/connect-excel/import-data.png":::

    The worksheet now has an empty pivot table and chart.

1. Under **PivotTable Fields**, select all the check-boxes for the fields you want to view.

    :::image type="content" source="media/connect-excel/power-pivot-results.png" alt-text="Configure database report." lightbox="media/connect-excel/power-pivot-results.png":::

> [!TIP]
> If you want to connect other Excel workbooks and worksheets to the database, select the **Data** tab, and select **Recent Sources** to launch the **Recent Sources** dialog box. From there, choose the connection you created from the list, and then select **Open**.

## Create a permanent connection using .odc file

To save the connection details permanently, you can create an .odc file and make this connection a selectable option within the **Existing Connections** dialog box.

1. In the menu bar at the top of the page, select the **Data** tab, and then select **Existing Connections** to launch the **Existing Connections** dialog box.
   1. Select **Browse for more** to open the **Select Data Source** dialog box.
   1. Select the **+NewSqlServerConnection.odc** file and then select **Open** to open the **Data Connection Wizard**.

      :::image type="content" source="media/connect-excel/new-connection.png" alt-text="Screenshot from Microsoft Excel, showing the step to create a new connection." lightbox="media/connect-excel/new-connection.png":::

1. In the **Data Connection Wizard**, type in your server name and your SQL Database credentials. Select **Next**.
   1. Select the database that contains your data from the dropdown list.
   1. Select the table or view you're interested in. We chose vGetAllCategories.
   1. Select **Next**.

      :::image type="content" source="media/connect-excel/data-connection-wizard.png" alt-text="Screenshot from Microsoft Excel, showing the steps in the Data Connection Wizard." lightbox="media/connect-excel/data-connection-wizard.png":::

1. Select the location of your file, the **File Name**, and the **Friendly Name** in the next screen of the Data Connection Wizard. You can also choose to save the connection string password in the file, though this can potentially expose your data to unwanted access. Select **Finish** when ready.

    :::image type="content" source="media/connect-excel/save-data-connection.png" alt-text="Screenshot from Microsoft Excel of the Save Data Connection.":::

1. Select how you want to import your data. We chose to do a PivotTable. You can also modify the properties of the connection by selecting **Properties**. Select **OK** when ready. If you did not choose to save the password with the file, then you will be prompted to enter your credentials.

    :::image type="content" source="media/connect-excel/pivottable-report.png" alt-text="Screenshot from Microsoft Excel of choosing a PivotTable report to import data.":::

1. Verify that your new connection has been saved by expanding the **Data** tab, and selecting **Existing Connections**.

    :::image type="content" source="media/connect-excel/existing-connection.png" alt-text="Screenshot from Microsoft Excel of existing connections.":::

## Related content

- [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](connect-query-ssms.md)
- [Create a web application that connects to Azure SQL Database on the back-end](/azure/app-service/app-service-web-tutorial-dotnet-sqldatabase)
