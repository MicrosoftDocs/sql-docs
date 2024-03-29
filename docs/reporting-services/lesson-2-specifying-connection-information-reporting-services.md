---
title: "Lesson 2: Specify connection information (Reporting Services)"
description: "In this lesson, you define a data source: connection information the report uses to access data from a relational database or other sources."
author: maggiesMSFT
ms.author: maggies
ms.date: 12/09/2019
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Lesson 2: Specify connection information (Reporting Services)

In lesson 1, you added a [!INCLUDE[ssrsnoversion-md](../includes/ssrsnoversion-md.md)] paginated report to your Tutorial project.
  
In this lesson, you're going to define a *data source*, connection information the report uses to access data from a relational database or other sources.

For this report, you're going to add the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] sample database as your data source. This tutorial assumes that the database is located in the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] and installed on your local computer.  

## Set up a connection  

1. In the **Report Data** pane, select **New** > **Data Source**. If the **Report Data** pane isn't visible, then select **View** menu > **Report Data**.

    :::image type="content" source="media/ssrs-table-tutorial-2-new-data-source.png" alt-text="Screenshot of the Report Data pane with the Data Source option highlighted in the New menu.":::

    The **Data Source Properties** dialog box opens with the **General** section displayed.

    :::image type="content" source="media/lesson-2-specifying-connection-information-reporting-services/vs-datasource-connection-properties-dialog-box.png" alt-text="Screenshot of the Data Source Properties dialog box." lightbox="media/lesson-2-specifying-connection-information-reporting-services/vs-datasource-connection-properties-dialog-box.png":::

2. In the **Name** text box, enter "AdventureWorks2022".

3. Select the **Embedded connection** radio button.

4. In the **Type** dropdown selection box, select "Microsoft SQL Server".
  
5. In the **Connection string** text box, type the following string:

    `Data source=localhost; initial catalog=AdventureWorks2022`

    > [!NOTE]
    > This connection string assumes that [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the report server, and the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database are all installed on the local computer.
    >
    >Change the connection string and replace "localhost" with the name of your database server/instance if the assumption isn't true. If you're using [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] or a SQL Server named instance, you need to modify your connection string to include instance information. For example:
    >
    > `Data source=localhost\SQLEXPRESS; initial catalog=AdventureWorks2022`
    >
    > For more information about connection strings, see the `Related content` section.

6. Select the **Credentials** tab, and under the section **Change the credentials used to connect to the data source**, select the **Use Windows Authentication (integrated security)** radio button.

7. Select **OK** to complete the process.

Report Designer adds the data source AdventureWorks2022 to the **Report Data** pane.

:::image type="content" source="media/lesson-2-specifying-connection-information-reporting-services/ssrs-adventureworks-datasource2016.png" alt-text="Screenshot of the Report Data pane that highlights the AdventureWorks2016 data source.":::



## Next step

In this lesson, you successfully defined a connection to the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] sample database. Continue with [Lesson 3: Define a dataset for the table report &#40;Reporting Services&#41;](lesson-3-defining-a-dataset-for-the-table-report-reporting-services.md) to define a dataset for the report.

## Related content

- [Create data connection strings - Report Builder & SSRS](report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)
