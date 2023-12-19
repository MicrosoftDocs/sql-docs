---
title: "Lesson 4: Define a data connection and data table for the child report"
description: Learn how to use Reporting Services to create a data connection and a data table for the child report.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/18/2016
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Lesson 4: Define a data connection and data table for the child report
After you design the parent report, your next step is to create a data connection and a data table for the child report.

[!INCLUDE [article-uses-adventureworks](../includes/article-uses-adventureworks.md)]
  
### Define a data connection and DataTable by adding a DataSet (for the child report)  
  
1.  On the **Website** menu, select **Add New Item**.  
  
2.  In the **Add New Item** dialog box, select **DataSet** and then choose **Add**. When prompted, you should add the item to the **App_Code** folder by selecting **Yes**.  
  
    This action adds a new XSD file **DataSet2.xsd** to the project and opens the DataSet Designer.  
  
3.  From the Toolbox window, drag a **TableAdapter** control to the design surface. This action launches the **TableAdapter** Configuration Wizard.  
  
4.  On the **Choose Your Data Connection** page, you can select the connection you created in Lesson 2. If you did, choose **Next** and go to step 8. Otherwise, select **New Connection**.  

5.  In the **Add Connection** dialog box, perform the following steps:  
  
    1.  In the **Server name** box, enter the server where the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database is located.  
  
        The default SQL Server Express instance is **(local)\sqlexpress**.  
  
    2.  In the **Log on to the server** section, select the option that provides you access to the data. **Use Windows Authentication** is the default.  
  
    3.  From the **Select or enter a database name** drop-down list, choose [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)].  
  
    4.  Select **OK**, and then select **Next**.  
  
6.  If you selected **Use SQL Server Authentication** in Step 5 (b), choose the option whether to include the sensitive data in the string or set the information in your application code.  
  
7.  On the **Save the Connection String to the Application Configuration File** page, enter in the name for the connection string or accept the default **AdventureWorks2022ConnectionString**. Select **Next**.  
  
8.  On the **Choose a Command Type** page, select **Use SQL Statements**, and then choose **Next**.  
  
9. On the **Enter a SQL Statement** page, enter the following Transact-SQL query to retrieve data from the [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] database, and then select **Next**.  
  
    ```  
    SELECT PurchaseOrderID, PurchaseOrderDetailID, OrderQty, ProductID, ReceivedQty, RejectedQty, StockedQty FROM Purchasing.PurchaseOrderDetail  
    ```  
  
    You can also create the query by selecting **Query Builder**, and then verify the query by choosing **Execute Query** button.
  
10. On the **Choose Methods to Generate** page, uncheck **Create methods to send updates directly to the database (GenerateDBDirectMethods)**, and then select **Finish**.  
  
    > [!WARNING]  
    > Be sure to uncheck **Create methods to send updates directly to the database (GenerateDBDirectMethods)**  
  
    You configured the ADO.NET [DataTable](/dotnet/api/system.data.datatable) as a data source for your report. On the DataSet Designer page in Visual Studio, you should see the **DataTable** you added, listing the columns specified in the query. DataSet2 contains the data from the PurhcaseOrderDetail table, based on the query.  
  
11. Save the file.  
  
12. To preview the data, select **Preview Data** on the **Data** menu, and then choose **Preview**.  
  
## Next step

You successfully created a data connection and data table for the child report. Next, you design the child report using the Report Wizard. See [Lesson 5: Design the child report by using the Report Wizard](../reporting-services/lesson-5-design-the-child-report-using-the-report-wizard.md).  
