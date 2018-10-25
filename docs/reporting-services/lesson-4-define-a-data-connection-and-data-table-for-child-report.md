---
title: "Lesson 4: Define a Data Connection and Data Table for Child Report | Microsoft Docs"
ms.date: 05/18/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: a6aa2c56-227c-43c5-a28e-c7104131ac5e
author: markingmyname
ms.author: maghan
---
# Lesson 4: Define a Data Connection and Data Table for Child Report
After you design the parent report, you next step is to create a data connection and a data table for the child report. In this tutorial the data connection is to the AdventureWorks2014 database.  
  
### To define a data connection and DataTable by adding a DataSet (for child report)  
  
1.  On the **Website** menu, select **Add New Item**.  
  
2.  In the **Add New Item** dialog box, select **DataSet** and then select **Add**. When prompted, you should add the item to the **App_Code** folder by selecting **Yes**.  
  
    This adds a new XSD file **DataSet2.xsd** to the project and opens the DataSet Designer.  
  
3.  From the Toolbox window, drag a **TableAdapter** control to the design surface. This launches the **TableAdapter** Configuration Wizard.  
  
4.  On the **Choose Your Data Connection** page, you can select the connection you created in Lesson 2. If you did, select **Next** and go to step 8. Otherwise, select **New Connection**.  
  
5.  In the **Add Connection** dialog box, perform the following steps:  
  
    1.  In the **Server name** box, enter the server where the **AdventureWorks2014** database is located.  
  
        The default SQL Server Express instance is **(local)\sqlexpress**.  
  
    2.  In the **Log on to the server** section, select the option that provides you access to the data. **Use Windows Authentication** is the default.  
  
    3.  From the **Select or enter a database name** drop-down list, select **AdventureWorks2014**.  
  
    4.  Select **OK**, and then select **Next**.  
  
6.  If you selected **Use SQL Server Authentication** in Step 5 (b), select the option whether to include the sensitive data in the string or set the information in your application code.  
  
7.  On the **Save the Connection String to the Application Configuration File** page, type in the name for the connection string or accept the default **AdventureWorks2014ConnectionString**. Select **Next**.  
  
8.  On the **Choose a Command Type** page, select **Use SQL Statements**, and then select **Next**.  
  
9. On the **Enter a SQL Statement** page, enter the following Transact-SQL query to retrieve data from the **AdventureWorks2014** database, and then select **Next**.  
  
    ```  
    SELECT PurchaseOrderID, PurchaseOrderDetailID, OrderQty, ProductID, ReceivedQty, RejectedQty, StockedQty FROM Purchasing.PurchaseOrderDetail  
    ```  
  
    You can also create the query by selecting **Query Builder**, and then verify the query by selecting **Execute Query** button. If the query does not return the expected data, you might be using an earlier version of AdventureWorks. For more information about how to get the **AdventureWorks2014** sample database, see [AdventureWorks sample databases](https://github.com/Microsoft/sql-server-samples/releases).  
  
10. On the **Choose Methods to Generate** page, uncheck **Create methods to send updates directly to the database (GenerateDBDirectMethods)**, and then select **Finish**.  
  
    > [!WARNING]  
    > Be sure to uncheck **Create methods to send updates directly to the database (GenerateDBDirectMethods)**  
  
    You have now completed configuring the ADO.NET [DataTable](https://msdn.microsoft.com/library/system.data.datatable.aspx) as a data source for your report. On the DataSet Designer page in Visual Studio, you should see the **DataTable** you added, listing the columns specified in the query. DataSet2 contains the data from the PurhcaseOrderDetail table, based on the query.  
  
11. Save the file.  
  
12. To preview the data, select **Preview Data** on the **Data** menu, and then select **Preview**.  
  
## Next Task  
You have successfully created a data connection and data table for the child report. Next, you will design the child report using the Report Wizard. See [Lesson 5: Design the Child Report using the Report Wizard](../reporting-services/lesson-5-design-the-child-report-using-the-report-wizard.md).  
  

