---
title: "Lesson 2: Modify the report data source properties"
description: Learn how to use the web portal to select a report for delivery to recipients and also how to modify the Report Data Source properties.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/23/2016
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Lesson 2: Modify the report data source properties
In this [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] tutorial lesson, you use web portal to select a report for delivery to recipients. The data-driven subscription that you define distributes the **Sales Order** report created in the tutorial [Create a basic table report &#40;SSRS tutorial&#41;](../reporting-services/tutorial-step01-creating-a-report-server-project-reporting-services.md). In the steps that follow, you modify the data source connection information used by the report to get data. Only reports that use **stored credentials** to access a report data source can be distributed through a data-driven subscription. Stored credentials are necessary for unattended report processing.  
  
You also modify the dataset and report to use a parameter to filter the report on the `[Order]` so the subscription can output different instances of the report for specific orders and rendering formats.  
  
## <a name="bkmk_modify_datasource"></a>Modify the data source to use stored credentials  
  
1.  Browse to the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal with administrator privileges, for example, right-click the icon for Internet Explorer and select **Run as administrator**.  
 
2.    Browse to the web portal URL. For example:   
    `https://<server name>/reports`.  
    `https://localhost/reports`
 **Note:** The web *portal* URL is "Reports", not the Report *Server* URL of "Reportserver".  
3.  Browse to the folder containing the **Sales Orders** report and in the context menu of the report, select **Manage**.  

    :::image type="content" source="../reporting-services/media/ssrs-tutorial-datadriven-manage-report.png" alt-text="Screenshot of the context menu of Sales Orders.":::
  
3.  Select **Data Sources** in the left pane.  
  
4.  Verify the **Connection Type** is **Microsoft SQL Server**.  
  
5.  Verify the connection string is the following and it assumes that the sample database is on a local database server:  
  
    ```  
    Data source=localhost; initial catalog=AdventureWorks2022  
    ```  
  
6.  Select **Use the following credentials**.  
  
7. In the **Type of credentials**, select **Windows user name and password**
8. Type your user name (use the format *domain\user*) and password. If you don't have permission to access the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database, specify a sign-in that does.  
    
9. Select **Test Connection** to verify you can connect to the data source.  
  
10. Select **Save**.
11. Select **Cancel**.
  
11. View the report to verify that the report runs with the credentials you specified.  
  
## <a name="bkmk_modify_dataset"></a>Modify the AdventureWorksDataset  
 In the following steps, you modify the dataset to use a parameter to filter the data set based an order number.
1.  Open the **Sales Orders** report in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)].
  
2.  Right-click the dataset `AdventureWorksDataset` and select **Dataset Properties**.

    :::image type="content" source="../reporting-services/media/ssrs-tutorial-datadriven-datasetproperties.png" alt-text="Screenshot of the context menu for AdventureWorksDataset highlighting the Dataset Properties option.":::

1.  Add the statement `WHERE (UPPER(SalesOrderNumber) =UPPER(@OrderNumber) or  @OrderNumber IS NULL)` before the `Group By` statement. The following example is the full query syntax:  
  
    ```  
    SELECT soh.OrderDate AS Date, soh.SalesOrderNumber AS [Order], pps.Name AS Subcat, pp.Name AS Product, SUM(sd.OrderQty) AS Qty, SUM(sd.LineTotal)  AS LineTotal  
    FROM Sales.SalesPerson AS sp INNER JOIN  
      Sales.SalesOrderHeader AS soh ON sp.BusinessEntityID = soh.SalesPersonID INNER JOIN  
       Sales.SalesOrderDetail AS sd ON sd.SalesOrderID = soh.SalesOrderID INNER JOIN  
       Production.Product AS pp ON sd.ProductID = pp.ProductID  
    INNER JOIN  
       Production.ProductSubcategory AS pps ON pp.ProductSubcategoryID = pps.ProductSubcategoryID   
    INNER JOIN  
        Production.ProductCategory AS ppc ON ppc.ProductCategoryID = pps.ProductCategoryID  
  
    WHERE (UPPER(SalesOrderNumber) =UPPER(@OrderNumber) or  @OrderNumber IS NULL)  
  
    GROUP BY ppc.Name, soh.OrderDate, soh.SalesOrderNumber, pps.Name, pp.Name, soh.SalesPersonID  
    HAVING (ppc.Name = 'Clothing')  
    ```  
  
4.  Select **OK**  
 In the following steps you add a parameter to the report.  The report parameter feeds the dataset parameter. 
## <a name="bkmk_add_reportparameter"></a>Add a report parameter and republish the report  
  
1.  In the Report Data pane, expand the parameters folder and double-click the **Ordernumber** parameter.  It was created automatically as part of the previous steps when you added the parameter to the dataset. Select **New** and then choose **Parameter...**  

    :::image type="content" source="../reporting-services/media/ssrs-tutorial-datadriven-parameter.png" alt-text="Screenshot of the Report Data pane highlighting the Ordernumber parameter.":::

1.  Verify the **Name** is `OrderNumber`.  
  
3.  Verify the **Prompt** is `OrderNumber`.  
  
4.  Select **Allow blank value ("")**.  
  
5.  Select **Allow null value**.  
  
6.  Select **OK**.  
  
7.  Select the **Preview** tab to run the report. Note the parameter input box at the top of the report. You can either:  
  
    -   Select View Report to see the full report without using a parameter.  
  
    -   Unselect the **Null** option and type an order number, for example *so71949*, then select **View Report** to view only the one order in the report.  

    :::image type="content" source="../reporting-services/media/ssrs-tutorial-datadriven-reportviewer-parameter.png" alt-text="Screenshot of the preview tab and the View Report button highlighted.":::
 
  
## <a name="bkmk_redeploy"></a>Redeploy the report  
  
1.  Redeploy the report so the subscription configuration in the next lesson can utilize the changes you made in this lesson. For more information on the project properties used in the table tutorial, see section 'To Publish the Report to the Report Server (Optional)' of [Lesson 6: Adding grouping and totals &#40;Reporting Services&#41;](../reporting-services/lesson-6-adding-grouping-and-totals-reporting-services.md).  
  
2.  On the toolbar, select **Build** and then choose **Deploy tutorial**.  
  
## Next step 
+ You successfully configured the report to get data using stored credentials and the data can be filtered with a parameter. 
+ In the next lesson, you configure the subscription using the web portal Data-Driven Subscription pages. See [Lesson 3: Defining a data-driven subscription](../reporting-services/lesson-3-defining-a-data-driven-subscription.md).  
  
## Related content

- [Manage report data sources](../reporting-services/report-data/manage-report-data-sources.md)  
- [Specify credential and connection information for report data sources](../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)  
- [Create a data-driven subscription &#40;SSRS tutorial&#41;](../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md)  
- [Create a basic table report &#40;SSRS tutorial&#41;](../reporting-services/tutorial-step01-creating-a-report-server-project-reporting-services.md)  
  
  
  

