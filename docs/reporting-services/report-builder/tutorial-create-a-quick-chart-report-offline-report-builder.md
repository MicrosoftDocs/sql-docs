---
title: "Tutorial: Create a quick chart report offline (Report Builder)"
description: In this tutorial, you create a pie chart in a Reporting Services paginated report in Report Builder. Then you add percentages and modify the pie chart.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/30/2017
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "reports, creating"
  - "tutorials, getting started"
  - "creating reports"
---
# Tutorial: Create a quick chart report offline (Report Builder)

  In this tutorial, you use a wizard to create a pie chart in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated report in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]. Then you add percentages and modify the pie chart a little.

You can do this tutorial two different ways. Both methods have the same outcome, which is a pie chart like the one in this illustration:

:::image type="content" source="media/tutorial-create-a-quick-chart-report-offline-report-builder/report-builder-quick-pie-chart.png" alt-text="Screenshot of the Report Builder quick pie chart.":::

## Prerequisites

Whether you use XML data or a [!INCLUDE[tsql](../../includes/tsql-md.md)] query, you need to have access to Report Builder. You can start [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from  a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in native mode or in SharePoint integrated mode, or you can download [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from the Microsoft Download Center. For more information, see [Install Report Builder](../../reporting-services/install-windows/install-report-builder.md).

## <a id="TwoWays"></a> Two ways to do this tutorial

- [Create the pie chart with XML data](#CreatePieChartXML)

- [Create the pie chart with a Transact-SQL query that contains data](#CreatePieQueryData)

### Use XML data for this tutorial

You can use XML data that you copy from this article and paste into the wizard. You don't need to be connected to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in native mode or in SharePoint integrated mode, and you don't need access to an instance of SQL Server.

[Create the pie chart with XML data](#CreatePieChartXML).

### Use a [!INCLUDE[tsql](../../includes/tsql-md.md)] query that contains data for this tutorial

You can copy a query with data included in it from this article and paste it into the wizard. You need the name of an instance of SQL Server and credentials sufficient for read-only access to any database. The dataset query in the tutorial uses literal data, but an instance of SQL Server must process the query to return the metadata that is required for a report dataset.

The advantage of using the [!INCLUDE[tsql](../../includes/tsql-md.md)] query is that all the other [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] tutorials use the same method. So, when you work on the other tutorials, you already know what to do.

The [!INCLUDE[tsql](../../includes/tsql-md.md)] query does require a few other prerequisites. For more information, see [Prerequisites for tutorials (Report Builder)](../../reporting-services/prerequisites-for-tutorials-report-builder.md).

[Create the pie chart with a Transact-SQL query that contains data](#CreatePieQueryData).

## <a id="CreatePieChartXML"></a> Create the pie chart with XML data

1. [Start Report Builder](../../reporting-services/report-builder/start-report-builder.md) from the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, or the report server in SharePoint integrated mode, or from your computer.

     The **Getting Started** dialog appears.

     :::image type="content" source="media/tutorial-create-a-quick-chart-report-offline-report-builder/report-builder-get-started.png" alt-text="Screenshot of the Report Builder Get Started dialog.":::

     If the **Getting Started** dialog doesn't appear, select **File** and choose **New**. The **New Report or Dataset** dialog has most of the same contents as the **Getting Started** dialog.

1. In the left pane, verify that **New Report** is selected.

1. In the right pane, select **Chart Wizard**, and then choose **Create**.

1. In the **Choose a dataset** page, select **Create a dataset**, and then choose **Next**.

1. In the **Choose a connection to a data source** page, select **New**.

     The **Data Source Properties** dialog opens.

1. You can name a data source anything you want. In the **Name** box, enter **MyPieChart**.

1. In the **Select connection type** box, select **XML**.

1. Select the **Credentials** tab, select **Use current Windows user. Kerberos delegation might be required**, and then select **OK**.

1. In the **Choose a connection to a data source** page, select **MyPieChart**, and then select **Next**.

1. Copy the following text and paste it in the large box in the top of the **Design a query** page.

    ```
    <Query>
    <ElementPath>Root /S  {@Sales (Integer)} /C {@FullName} </ElementPath>
    <XmlData>
    <Root>
    <S Sales="150">
      <C FullName="Jae Pak" />
    </S>
    <S Sales="350">
      <C FullName="Jillian  Carson" />
    </S>
    <S Sales="250">
      <C FullName="Linda C Mitchell" />
    </S>
    <S Sales="500">
      <C FullName="Michael Blythe" />
    </S>
    <S Sales="450">
      <C FullName="Ranjit Varkey" />
    </S>
    </Root>
    </XmlData>
    </Query>
    ```

1. (Optional) Select the **Run** button (**!**) to see the data your chart is based on.

     :::image type="content" source="media/tutorial-create-a-quick-chart-report-offline-report-builder/rb-designquery.png" alt-text="Screenshot of the Report Builder Design Query.":::

1. Select **Next**.

1. In the **Choose a chart type** page, select **Pie**, and then choose **Next**.

1. In the **Arrange chart fields** page, double-click the **Sales** field in the **Available fields** box.

     It automatically moves to the **Values** box, because the value is a numerical value.

     :::image type="content" source="media/tutorial-create-a-quick-chart-report-offline-report-builder/rb-wizarrangefields.png" alt-text="Screenshot of the Report Builder Wizard Arrange Fields.":::

1. Drag the **FullName** field from the **Available fields** box to the **Categories** box (or double-click it). The value goes to the **Categories** box, and then select **Next**.

     The **Preview** page shows your new pie chart with representational data. The legend reads Full Name 1, Full Name 2, etc., rather than the salespeople's names, and the size of the slices of pie aren't accurate. This example gives you an idea of what your report looks like.

     :::image type="content" source="media/tutorial-create-a-quick-chart-report-offline-report-builder/rb-newchartpreview.png" alt-text="Screenshot of the Report Builder New Chart Preview.":::

1. Select **Finish**.

     Now you see your new pie chart report in Design View, still with representational data.

     :::image type="content" source="media/tutorial-create-a-quick-chart-report-offline-report-builder/rb-newpiedesign.png" alt-text="Screenshot of the Report Builder New Pie in Design View.":::

1. To see your actual pie chart, select **Run** on the **Home** tab of the Ribbon.

     :::image type="content" source="media/tutorial-create-a-quick-chart-report-offline-report-builder/rb-newchartrun.png" alt-text="Report Builder New Chart Run.":::

1. To continue modifying your pie chart, go to [After you run the wizard](#AfterWizard) in this article.

## <a id="CreatePieQueryData"></a> Create the pie chart with a [!INCLUDE[tsql](../../includes/tsql-md.md)] query

1. [Start Report Builder](../../reporting-services/report-builder/start-report-builder.md) from the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, from the report server in SharePoint integrated mode, or from your computer.

     The **Getting Started** dialog appears.

    > [!NOTE]  
    >  If the **Getting Started** dialog doesn't appear, select **File** and choose **New**. The **New Report or Dataset** dialog has most of the same contents as the **Getting Started** dialog.

1. In the left pane, verify that **New Report** is selected.

1. In the right pane, select **Chart Wizard**, and then choose **Create**.

1. In the **Choose a dataset** page, select **Create a dataset**, and then choose **Next**.

1. In the **Choose a connection to a data source** page, select an existing data source or browse to the report server and select a data source, and then select **Next**. You might need to enter a user name and password.

    > [!NOTE]  
    >  The data source you choose is unimportant, as long as you have adequate permissions. You aren't getting data from the data source. For more information, see [Prerequisites for tutorials (Report Builder)](../../reporting-services/prerequisites-for-tutorials-report-builder.md).

1. On the **Design a Query** page, select **Edit as Text**.

1. Paste the following query into the query pane:

    ```
    SELECT 150 AS Sales, 'Jae Pak' AS FullName
    UNION SELECT 350 AS Sales, 'Jillian Carson' AS FullName
    UNION SELECT 250 AS Sales, 'Linda C Mitchell' AS FullName
    UNION SELECT 500 AS Sales, 'Michael Blythe' AS FullName
    UNION SELECT 450 AS Sales, 'Ranjit Varkey' AS FullName
    ```

1. (Optional) Select the **Run** button (**!**) to see the data your chart is based on.

1. Select **Next**.

1. In the **Choose a chart type** page, select **Pie**, and then choose **Next**.

1. In the **Arrange chart fields** page, double-click the **Sales** field in the **Available fields** box.

     It automatically moves to the **Values** box, because it's a numerical value.

1. Drag the **FullName** field from the **Available fields** box to the **Categories** box (or double-click it). The value goes to the **Categories** box, and then select **Next**.

1. Select **Finish**.

     You're now looking at your new pie chart report on the design surface. What you see is representational. The legend reads Full Name 1, Full Name 2, etc., rather than the salespeople's names, and the size of the slices of pie aren't accurate. This example gives you an idea of what your report looks like.

1. To see your actual pie chart, select **Run** on the **Home** tab of the Ribbon.

## <a id="AfterWizard"></a> After you run the wizard

Now that you have your pie chart report, you can play with it. On the **Run** tab of the Ribbon, select **Design**, so you can continue modifying it.

## Make the chart bigger

You might want the pie chart to be bigger.

-  Select the chart, but not on any element in the chart. To select it and drag the lower-right corner to resize it.

Notice the design surface gets larger as you drag.

## Add a report title

1. Select the words **Chart title** at the top of the chart, and then enter a title, such as **Sales Pie Chart**.
1. With the title selected, in the **Properties** pane, change **Color** to **Black** and **FontSize** to **12pt**.

## Add percentages

1. Right-click the pie chart and select **Show Data Labels**. The data labels appear within each slice on the pie chart.

1. Right-click the labels and select **Series Label Properties**. The **Series Label Properties** dialog appears.

1. In the **Label data** box, type **#PERCENT{P0}**.

     The **{P0}** gives you the percentage without decimal places. If you enter just **#PERCENT**, your numbers have two decimal places. **#PERCENT** is a keyword that performs a calculation or function for you. There are other keywords that you can use.

1. Select **Yes** to confirm you want to set **UseValueAsLabel** to **False**.

1. On the **Font** tab, select **Bold** and change **Color** to **White**.

1. Select **OK**.

For more information about customizing chart labels and legends, see [Display percentage values on a pie chart (Report Builder)](../../reporting-services/report-design/display-percentage-values-on-a-pie-chart-report-builder-and-ssrs.md) and [Change the text of a legend item (Report Builder)](../../reporting-services/report-design/chart-legend-change-item-text-report-builder.md).

## <a id="WhatsNext"></a> What's next?

Now that you created your first report in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)], you're ready to try the other tutorials and to start creating reports from your own data. To run [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)], you need permission to access your data sources, such as databases, with a *connection string*, which actually connects you to the data source. Your system administrator has this information and can set you up.

To work through the other tutorials, you need the name of an instance of SQL Server and credentials sufficient for read-only access to any database. Your system administrator can also set that up for you.

Finally, to save your reports to a report server or a SharePoint site that is integrated with a report server, you need the URL and permissions. You can run any report you create directly from your computer, but reports have more functionality when run from the report server or SharePoint site. You need permissions to run your reports or others from the report server or SharePoint site where they're published. Talk to your system administrator to obtain access.

It might help to read about some of the concepts and terms before you get started. See [Reporting Services concepts](../reporting-services-concepts-ssrs.md). Also, spend some time planning, before you create your first report. It's time well spent. See [Plan a report (Report Builder)](../../reporting-services/report-design/planning-a-report-report-builder.md).

## Related content

- [Report Builder tutorials](../../reporting-services/report-builder-tutorials.md)
- [Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server-2016.md)

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
