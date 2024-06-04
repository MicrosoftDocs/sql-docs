---
title: "Tutorial: Specify connection information (Reporting Services)"
description: "In this lesson, you define a data source: connection information the report uses to access data from a relational database or other sources."
author: maggiesMSFT
ms.author: maggies
ms.date: 05/28/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom: updatefrequency5
#customer intent: As a SQL server user, I want to use SQL Server Data Tools (SSDT) to specify connection information so that I can access data in a relational database or from other source.

---
# Tutorial: Specify connection information (Reporting Services)
  
In step 2 of this tutorial, you define a *data source* and configure connection information that the report uses to access data from a relational database or other sources.

In this tutorial, you:

> [!div class="checklist"]
> * Add the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] sample database as your data source
> * Configure the connection string for the data source

## Prerequisites

* Ensure the database is located in the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] and installed on your local computer.  

## Set up a connection  

When you create reports in SQL Server Reporting Services (SSRS), establishing a data source is crucial. Follow these steps to set up a connection.

1. In the **Report Data** pane, select **New** > **Data Source**. If the **Report Data** pane isn't visible, then select **View** > **Report Data**.

    :::image type="content" source="media/ssrs-table-tutorial-2-new-data-source.png" alt-text="Screenshot of the Report Data pane with the Data Source option highlighted in the New menu.":::

    The **Data Source Properties** dialog opens with the **General** section selected.

    :::image type="content" source="media/lesson-2-specifying-connection-information-reporting-services/vs-datasource-connection-properties-dialog-box.png" alt-text="Screenshot of the Data Source Properties dialog." lightbox="media/lesson-2-specifying-connection-information-reporting-services/vs-datasource-connection-properties-dialog-box.png":::

1. In the **Name** box, enter "AdventureWorks2022".

1. Select the **Embedded connection** option.

1. In the **Type** list, select "Microsoft SQL Server".
  
1. In the **Connection string** box, enter the following string:

    `Data source=localhost; initial catalog=AdventureWorks2022`

    > [!NOTE]
    > This connection string assumes that [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the report server, and the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database are all installed on the local computer.
    >
    >Change the connection string and replace "localhost" with the name of your database server/instance if the assumption isn't true. If you're using [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] or a SQL Server named instance, you need to modify your connection string to include instance information. For example:
    >
    > `Data source=localhost\SQLEXPRESS; initial catalog=AdventureWorks2022`
    >
    > For more information about connection strings, see the `Related content` section.

1. Select the **Credentials** tab, and choose the **Use Windows Authentication (integrated security)** option.

1. Select **OK** to complete the process.

Report Designer adds the **AdventureWorks2022** data source to the **Report Data** pane.

:::image type="content" source="media/lesson-2-specifying-connection-information-reporting-services/ssrs-adventureworks-datasource2022.png" alt-text="Screenshot of the Report Data pane that highlights the AdventureWorks2022 data source.":::

## Next step 

> [!div class="nextstepaction"]
> [Tutorial: Define a dataset for the table report &#40;Reporting Services&#41;](lesson-3-defining-a-dataset-for-the-table-report-reporting-services.md)