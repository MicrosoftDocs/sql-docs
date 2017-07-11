---
title: "Tree Map and Sunburst Charts in Reporting Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/31/2015"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 12307c8f-bca7-4d21-8ad5-0c07d819865b
caps.latest.revision: 17
author: "maggiesMSFT"
ms.author: "maggies"
manager: "erikre"
---
# Tree Map and Sunburst Charts in Reporting Services
[!INCLUDE[feedback_stackoverflow_msdn_connect_md](../../includes/feedback-stackoverflow-msdn-connect-md.md)]

  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Tree Map and sunburst visualizations are great for visually representing hierarchal data.   This topic is an overview of how to add a Tree Map or Sunburst chart to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report. The topic also includes a sample Adventureworks query to help get you started.  
  
##  <a name="bkmk_treemap_chart"></a> Tree Map Chart  
 ![ssrs_treemap_icon](../../reporting-services/media/ssrs-treemap-icon.png "ssrs_treemap_icon")  
  
 A tree map chart divides the chart area into rectangles that represent the different levels and relative sizes of the data hierarchy. The map is similar to branches on a tree that start with a trunk and divide into smaller and smaller branches. Each rectangle is broken into smaller rectangles representing the next level in the hierarchy. The top level tree map rectangles are arranged with the largest rectangle in the upper left corner of the chart to the smallest rectangle in the lower right corner.  Within a rectangle, the next level of the higher is also arranged with rectangles from the upper left to the lower right.  
  
 For example,  in the following image of the sample tree map, the Southwest territory is th largest and Germany is the smallest. Within the Southwest, Road Bikes are larger than Mountain Bikes.  
  
 ![ssrs_treemap_example](../../reporting-services/report-design/media/ssrs-treemap-example.png "ssrs_treemap_example")  
  
### To insert a tree map chart and configure for the sample Adventureworks data  
 **Note:** Before you add a chart to your report, create a data source and dataset.  For sample data and a sample query, see the section [Sample Adventureworks data](#bkmk_sample_data) in this topic.  
  
1.  Right-click the design surface, click **Insert**, and then click **Chart** .  
  
     Select Tree Map ![ssrs_treemap_icon](../../reporting-services/media/ssrs-treemap-icon.png "ssrs_treemap_icon").  
  
     ![ssrs_insert_treemap_sunburst](../../reporting-services/report-design/media/ssrs-insert-treemap-sunburst.png "ssrs_insert_treemap_sunburst")  
  
2.  Reposition and resize the chart.   For use with the sample data, a chart  that is 5 inches wide is a good start.  
  
3.  Add the following fields from the sample data:  
  
    |||  
    |-|-|  
    |![ssrs_treemap_example_properties](../../reporting-services/report-design/media/ssrs-treemap-example-properties.png "ssrs_treemap_example_properties")|**Values:** LineTotal<br /><br /> **Category Groups:** Add them in the order of:<br /><br /> 1) CategoryName<br /><br /> 2) SubcategoryName<br /><br /> **Series Groups:** TerritoryName|  
  
4.  To optimize the page size for general shape of a Tree Map, set the legend position to the bottom.  
  
5.  To add tool tips that display the subcategory and the line total, right-click **LineTotal** and then click **Series Properties**.  
  
     ![ssrs_visualization_seriesproperties](../../reporting-services/report-design/media/ssrs-visualization-seriesproperties.png "ssrs_visualization_seriesproperties")  
  
     Set the **Tooltip** property to the following:  
  
    ```  
    =Fields!SubcategoryName.Value &": " &Format(Sum(Fields!LineTotal.Value),"C")  
    ```  
  
     For more information, see [Show ToolTips on a Series &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/show-tooltips-on-a-series-report-builder-and-ssrs.md).  
  
6.  Change the default chart title to "Categorized Sales by Territory".  
  
7.  The number of label values that are displayed are affected by the size of the font, the size of the overall chart area, and the size of specific rectangles.  To see more of the labels, change the Label font property of LineTotal to 10pt instead of the default 8pt.  
  
  
##  <a name="bkmk_sunburst_chart"></a> Sunburst Chart  
 ![ssrs_sunburst_icon](../../reporting-services/media/ssrs-sunburst-icon.png "ssrs_sunburst_icon")  
  
 In a sunburst chart, the hierarchy is represented by a series of  circles, with the highest level of  the hierarchy in the center and lower levels of the hierarchy as rings displayed outside the center.  The lowest level of the hierarchy is the outside ring.  
  
 ![ssrs_sunburst_example](../../reporting-services/report-design/media/ssrs-sunburst-example.png "ssrs_sunburst_example")  
  
### To insert a sunburst chart and configure for the sample Adventureworks data  
 **Note:** Before you add a chart to your report, create a data source and dataset.  For sample data and a sample query, see the section [Sample Adventureworks data](#bkmk_sample_data) in this topic.  
  
1.  Right-click the design surface, click **Insert**, and then click **Chart** .  
  
     Select Sunburst ![ssrs_treemap_icon](../../reporting-services/media/ssrs-treemap-icon.png "ssrs_treemap_icon").  
  
     ![ssrs_insert_treemap_sunburst](../../reporting-services/report-design/media/ssrs-insert-treemap-sunburst.png "ssrs_insert_treemap_sunburst")  
  
2.  Reposition and resize the chart.   For use with the sample data., a chart  that is 5 inches wide is a good start.  
  
3.  Add the following fields from the sample data:  
  
    |||  
    |-|-|  
    |![ssrs_treemap_example_properties](../../reporting-services/report-design/media/ssrs-treemap-example-properties.png "ssrs_treemap_example_properties")|**Values:** LineTotal<br /><br /> **Category Groups:** Add them in the order of :<br /><br /> 1) CategoryName<br /><br /> 2) SubcategoryName,<br /><br /> 3) SalesReasonName<br /><br /> **Series Groups:** TerritoryName .|  
  
4.  To optimize the page size for the general shape of a Sunburst, set the legend position to the bottom.  
  
5.  Change the default chart title to "Categorized Sales by Territory, with sales reason".  
  
6.  |||  
    |-|-|  
    |![ssrs_sunburst_linetotalproperties](../../reporting-services/report-design/media/ssrs-sunburst-linetotalproperties.png "ssrs_sunburst_linetotalproperties")|To add the values of the category groups to the sunburst as labels, set the label property **Visible** = true and the **UseValueAsLabel**=False.<br /><br /> The label values that are displayed are affected by the size of the font, the size of the overall chart area, and the size of specific rectangles.  To see more of the labels, change the Label font property of LineTotal to 8pt instead of the default 10pt.|  
  
7.  If you want a different range of colors, change the chart **Palette** property.  
  
     ![ssrs_visualization_palette](../../reporting-services/report-design/media/ssrs-visualization-palette.png "ssrs_visualization_palette")  
  
  
##  <a name="bkmk_sample_data"></a> Sample Adventureworks data  
 This sections includes a sample query and the basic steps for creating a data source and dataset in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion-md.md)]. If your report already contains a data source and dataset you can skip this section.  
  
 The query returns Adventureworks sales order detail data with sales territory, product category,  product sub category, and sales reason data.  
  
1.  **Get the Data:**  
  
     The query in this section is based on the Adventureworks database which is available for download from  [Adventure Works 2014 Full Database Backup](https://msftdbprodsamples.codeplex.com/releases/view/125550).  
  
     For more information on how to install the database, see [How to install Adventure Works 2014 Sample Databases.pdf](https://msftdbprodsamples.codeplex.com/releases/view/125550).  
  
2.  **Create a data source:**  
  
    1.  In the **Report Data** pane,  right-click **Data Sources** and click **Add data source**.  
  
    2.  Select **Use a connection embedded in my report**.  
  
    3.  Select the connection type of **Microsoft SQL Server**.  
  
    4.  Type in the connection string to your server and database, for example the following:  
  
        ```  
        Data Source=[server name];Initial Catalog=AdventureWorks2014  
        ```  
  
    5.  It is a good idea to verify with the **Test Connection** button and then click **OK**.  
  
     For more information on creating a data source, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)..  
  
3.  **Create a dataset:**  
  
    -   In the **Report Data** pane,  right-click **Datasets** and click **Add dataset**.  
  
    -   Select **Use a dataset embedded in my report**.  
  
    -   Select the data source you created in the previous steps.  
  
    -   Select the **Text** query type and then copy and paste the following query into the **Query:** text box:  
  
        ```  
        SELECT    Sales.SalesOrderHeader.SalesOrderID, Sales.SalesOrderHeader.OrderDate, Sales.SalesOrderDetail.SalesOrderDetailID, Sales.SalesOrderDetail.ProductID, Sales.SalesOrderDetail.LineTotal,   
                                 Sales.SalesOrderDetail.UnitPrice, Sales.SalesOrderDetail.OrderQty, Production.Product.Name, Production.Product.ProductNumber, Sales.SalesTerritory.TerritoryID, lower(Sales.SalesTerritory.Name) AS TerritoryName,   
                                 Production.ProductSubcategory.Name AS SubcategoryName, Production.ProductCategory.Name AS CategoryName, Sales.SalesReason.SalesReasonID, Sales.SalesReason.Name AS SalesReasonName  
        FROM            Sales.SalesOrderDetail INNER JOIN  
                                 Sales.SalesOrderHeader ON Sales.SalesOrderDetail.SalesOrderID = Sales.SalesOrderHeader.SalesOrderID INNER JOIN  
                                 Production.Product ON Sales.SalesOrderDetail.ProductID = Production.Product.ProductID INNER JOIN  
                                 Sales.SalesTerritory ON Sales.SalesOrderHeader.TerritoryID = Sales.SalesTerritory.TerritoryID AND Sales.SalesOrderHeader.TerritoryID = Sales.SalesTerritory.TerritoryID AND   
                                 Sales.SalesOrderHeader.TerritoryID = Sales.SalesTerritory.TerritoryID INNER JOIN  
                                 Production.ProductSubcategory ON Production.Product.ProductSubcategoryID = Production.ProductSubcategory.ProductSubcategoryID AND   
                                 Production.Product.ProductSubcategoryID = Production.ProductSubcategory.ProductSubcategoryID AND   
                                 Production.Product.ProductSubcategoryID = Production.ProductSubcategory.ProductSubcategoryID INNER JOIN  
                                 Production.ProductCategory ON Production.ProductSubcategory.ProductCategoryID = Production.ProductCategory.ProductCategoryID AND   
                                 Production.ProductSubcategory.ProductCategoryID = Production.ProductCategory.ProductCategoryID AND   
                                 Production.ProductSubcategory.ProductCategoryID = Production.ProductCategory.ProductCategoryID INNER JOIN  
                                 Sales.SalesOrderHeaderSalesReason ON Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderHeaderSalesReason.SalesOrderID AND   
                                 Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderHeaderSalesReason.SalesOrderID AND Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderHeaderSalesReason.SalesOrderID AND   
                                 Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderHeaderSalesReason.SalesOrderID INNER JOIN  
                                 Sales.SalesReason ON Sales.SalesOrderHeaderSalesReason.SalesReasonID = Sales.SalesReason.SalesReasonID AND   
                                 Sales.SalesOrderHeaderSalesReason.SalesReasonID = Sales.SalesReason.SalesReasonID AND Sales.SalesOrderHeaderSalesReason.SalesReasonID = Sales.SalesReason.SalesReasonID AND   
                                 Sales.SalesOrderHeaderSalesReason.SalesReasonID = Sales.SalesReason.SalesReasonID  
        ```  
  
    -   click **OK**.  
  
     For more information on creating a dataset, see [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md).  
  
  
## See Also  
 [Shared Dataset Design View &#40;Report Builder&#41;](../../reporting-services/report-builder/shared-dataset-design-view-report-builder.md)   
 [Show ToolTips on a Series &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/show-tooltips-on-a-series-report-builder-and-ssrs.md)   
 [Tutorial: Treemaps in Power BI](https://support.powerbi.com/knowledgebase/articles/556200-tutorial-treemaps-in-power-bi)   
 [Treemap: Microsoft Research Data Visualization Apps for Office](http://research.microsoft.com/en-us/projects/msrdatavis/treemap.aspx)  
  
  
[!INCLUDE[feedback_stackoverflow_msdn_connect_md](../../includes/feedback-stackoverflow-msdn-connect-md.md)]

