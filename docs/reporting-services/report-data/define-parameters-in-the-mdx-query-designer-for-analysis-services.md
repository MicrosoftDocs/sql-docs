---
title: "Define Parameters in the MDX Query Designer for Analysis Services | Microsoft Docs"
description: Learn how to define query parameters in the Multidimensional Expression (MDX) query designer for Analysis Services.
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
helpviewer_keywords: 
  - "parameters [Reporting Services], MDX"
  - "Multidimensional Expressions [Reporting Services]"
  - "Data Mining Prediction [Reporting Services]"
  - "MDX [Reporting Services], defining parameters"
  - "DMX [Reporting Services]"
ms.assetid: 4ad1e5bc-f510-4752-b4f6-589e55317a90
author: maggiesMSFT
ms.author: maggies
---
# Define Parameters in the MDX Query Designer for Analysis Services
  To parameterize an MDX query for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source, you must add a query parameter to the query. In the MDX query designer, you can add a query parameter in both Design mode and Query mode by specifying a filter. After you define the query with a query parameter, Reporting Services automatically creates a report parameter and a dataset to provide the list of valid values. This enables a user to specify a value that is passed directly to the query.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To define a query parameter in MDX in Design mode  
  
1.  In the Report Data pane, right-click on a dataset created from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source type, and then click **Query**. The MDX query designer opens in Design mode.  
  
2.  Drag a dimension to the filter area and drop it on the first cell in the **Dimension** column.  
  
3.  In the **Hierarchy** column, choose a value from the drop-down list.  
  
4.  In the **Operator** column, choose an operator for the drop-down list.  
  
5.  In the **Filter Expression** column, select individual values from the drop-down list, or click the **All** member to choose all values.  
  
6.  In the **Parameters** column, select the check box to create a report parameter.  
  
7.  Click **Run**.  
  
     After you run the query, click **Design** on the toolbar to toggle to Query mode to view the MDX query that was created. Do not change the query text in Query mode if you want to continue to use Design mode to develop the query. Click **Design** to toggle back to Design mode.  
  
8.  Select **OK**.
  
     In the Report Data pane, expand the Parameters node to display the report parameter that was automatically created for the filter.  
  
     To view the dataset that provides available values for the report parameter, right-click any blank area in the Report Data pane, and then click **Show Hidden Datasets**. The Report Data pane displays all datasets in the report.  
  
### To define a query parameter in MDX in Query mode  
  
1.  In the Report Data pane, right-click on a dataset created from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source type, and then click **Query**. The MDX query designer opens in Design mode.  
  
2.  On the toolbar, click **Design** to toggle to Query mode.  
  
3.  On the MDX query designer toolbar, click **Query Parameters** (![Icon for the Query Parameters dialog box](../../reporting-services/report-data/media/iconqueryparameter.gif "Icon for the Query Parameters dialog box")). The Query Parameters dialog box opens.  
  
4.  In the **Parameter** column, click **\<Enter Parameter>**, and then type the name of a parameter.  
  
5.  In the **Dimension** column, choose a value from the drop-down list.  
  
6.  In the **Hierarchy** column, choose a value from the drop-down list.  
  
7.  In the **Multiple values** column, select the check box to create a multivalue parameter.  
  
8.  In the **Default** column, from the drop-down list, select a single value or multiple values depending on your choice in step 5.  
  
9.  Select **OK**.
  
10. On the query designer toolbar, click **Run**.  
  
11. Select **OK**.
  
     In the Report Data pane, expand the Parameters node to display the report parameter that was automatically created for the filter.  
  
     To view the dataset that provides available values for the report parameter, right-click any blank area in the Report Data pane, and then click **Show Hidden Datasets**. The Report Data pane displays all datasets in the report.  
  
## See Also  
 [Analysis Services Connection Type for MDX &#40;SSRS&#41;](../../reporting-services/report-data/analysis-services-connection-type-for-mdx-ssrs.md)   
 [Analysis Services MDX Query Designer User Interface](../../reporting-services/report-data/analysis-services-mdx-query-designer-user-interface.md)  
  
  
