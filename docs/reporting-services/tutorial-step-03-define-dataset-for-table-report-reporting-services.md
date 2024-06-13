---
title: "Tutorial: Define a dataset for the table report"
description: In this lesson, learn how to define a dataset for the Table Report (Reporting Services).
author: maggiesMSFT
ms.author: maggies
ms.date: 06/06/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom: updatefrequency5
#customer intent: As a SQL server user, I want to use SQL Server Data Tools (SSDT) to define a dataset so that I can generate a table report.

---
# Tutorial: Define a dataset for the table report - SQL Server Reporting Services

In this tutorial, you define a dataset for a data source. In [!INCLUDE[ssrsnoversion](../includes/ssrsnoversion-md.md)], a *dataset* contains data that you use in reports. A dataset includes a pointer to a data source and a query for use by the report, calculated fields, and variables.

In this tutorial, you:

> [!div class="checklist"]
> * Define a dataset for the table report
> * Create a Transact-SQL query to retrieve sales order information

## Prerequisites

* A defined [data source](docs\reporting-services\tutorial-step-02-specify-connection-information-reporting-services.md).

## Define a Transact-SQL query for report data  

Create a Transact-SQL query that retrieves sales order information from the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database.

1. In the **Report Data** pane in Visual Studio, select **New** > **Dataset...**. The **Dataset Properties** dialog opens with the **Query** section highlighted.

    :::image type="content" source="media/lesson-3-defining-a-dataset-for-the-table-report-reporting-services/vs-dataset-properties-dialog.png" alt-text="Screenshot of the Dataset Properties dialog with the query option highlighted.":::

1. In the **Name** box, enter "AdventureWorksDataset".

1. Select the **Use a dataset embedded in my report** option.

1. From the **Data source** list, select **AdventureWorks2022**.

1. For **Query type**, select the **Text** option.

1. Enter, or copy and paste, the following Transact-SQL query into the **Query** text box.

    ```T-SQL
    SELECT
       soh.OrderDate AS [Date],
       soh.SalesOrderNumber AS [Order],
       pps.Name AS [Subcat],
       pp.Name as [Product],
       SUM(sd.OrderQty) AS [Qty],
       SUM(sd.LineTotal) AS [LineTotal]
    FROM Sales.SalesPerson sp
    INNER JOIN Sales.SalesOrderHeader AS soh
          ON sp.BusinessEntityID = soh.SalesPersonID
       INNER JOIN Sales.SalesOrderDetail AS sd
          ON sd.SalesOrderID = soh.SalesOrderID
       INNER JOIN Production.Product AS pp
          ON sd.ProductID = pp.ProductID
       INNER JOIN Production.ProductSubcategory AS pps
          ON pp.ProductSubcategoryID = pps.ProductSubcategoryID
       INNER JOIN Production.ProductCategory AS ppc
          ON ppc.ProductCategoryID = pps.ProductCategoryID
    GROUP BY ppc.Name, soh.OrderDate, soh.SalesOrderNumber, pps.Name, pp.Name,soh.SalesPersonID  
    HAVING ppc.Name = 'Clothing'
    ```

1. (Optional) Select **Query Designer**. The query appears in the text-based *Query Designer*. Select **run** to view the results of the query (:::image type="icon" source="media/ssrs-querydesigner-run.png":::) on the **Query Designer** toolbar. The dataset displayed contains six fields from four tables in the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database. The query makes use of Transact-SQL functionality such as aliases. For example, the SalesOrderHeader table is called `soh`.

1. Select **OK** to exit the Query Designer.

1. Select **OK** to exit the **Dataset Properties** dialog. The **Report Data** pane displays the AdventureWorksDataset dataset and fields.

   :::image type="content" source="media/ssrs-adventureworksdataset.png" alt-text="Screenshot of the Datasets folder showing the AdventureWorksDataset and its fields.":::

You successfully specified a query that retrieves data for your report. 

## Next step

> [!div class="nextstepaction"]
> [Step 4: Add a table to the report &#40;Reporting Services&#41;](tutorial-step-04-add-table-to-report-reporting-services.md)