---
title: "Tutorial: Specify connection information (Reporting Services)"
description: "In this lesson, you define a data source: connection information the report uses to access data from a relational database or other sources."
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom:
  - updatefrequency5
# customer intent: As a SQL server user, I want to use SQL Server Data Tools (SSDT) to specify connection information so that I can access data in a relational database or from other sources.
---
# Tutorial: Specify connection information (Reporting Services)
  
After you create your project and report definition file, you define a data source and configure connection information for the report to access data from a relational database or other sources.

In this tutorial, you:

> [!div class="checklist"]
> * Add the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] sample database as your data source.
> * Configure the connection string for the data source.

## Prerequisites

* Completion of [Step 1: Create a report server project](tutorial-step-01-create-report-server-project-reporting-services.md).
* Ensure the database is in the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] and installed on your local computer.
* Access to the AdventureWorks2022 sample database.   

## Set up a connection  

When you create reports in SQL Server Reporting Services (SSRS), establishing a data source is crucial. Follow these steps to set up a connection between a data source and your reports.

1. Open your Sales Order report definition file (*.rdl*) in Visual Studio.

1. In the **Report Data** pane, select **New** > **Data Source**. If the **Report Data** pane isn't visible, then select **View** > **Report Data**.

    :::image type="content" source="media/ssrs-table-tutorial-2-new-data-source.png" alt-text="Screenshot of the Report Data pane with the Data Source option highlighted.":::

    The **Data Source Properties** dialog opens with the **General** section selected.

    :::image type="content" source="media/lesson-2-specifying-connection-information-reporting-services/vs-datasource-connection-properties-dialog-box.png" alt-text="Screenshot of the Data Source Properties dialog.":::

1. In the **Name** box, enter "AdventureWorks2022".

1. Select **Embedded connection**.

1. From the **Type** list, select **Microsoft SQL Server**.
  
1. In the **Connection string** box, enter:

    `Data source=localhost; initial catalog=AdventureWorks2022`

    > [!NOTE]
    > This connection string assumes that [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the report server, and the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database are all installed on the local computer.
    >
    >Change the connection string and replace "localhost" with the name of your database server/instance if the assumption isn't true. If you use [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] or a SQL Server named instance, modify your connection string to include instance information. For example:
    >
    > `Data source=localhost\SQLEXPRESS; initial catalog=AdventureWorks2022`


1. Select **Credentials**. Choose the **Use Windows Authentication (integrated security)** option.

1. Select **OK**. Report Designer adds the **AdventureWorks2022** data source to the **Report Data** pane.

   :::image type="content" source="media/lesson-2-specifying-connection-information-reporting-services/ssrs-adventureworks-data-source-2022.png.png" alt-text="Screenshot of the Report Data pane that highlights the AdventureWorks2022 data source.":::

## Next step

> [!div class="nextstepaction"]
> [Step 3: Define a dataset for the table report &#40;Reporting Services&#41;](tutorial-step-03-define-dataset-table-report-reporting-services.md)
