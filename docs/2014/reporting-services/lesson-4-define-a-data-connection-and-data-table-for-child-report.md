---
title: "Lesson 4: Define a Data Connection and Data Table for Child Report | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: a6aa2c56-227c-43c5-a28e-c7104131ac5e
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Lesson 4: Define a Data Connection and Data Table for Child Report
  After you design the parent report, you next step is to create a data connection and a data table for the child report. In this tutorial the data connection is to the AdventureWorks2008 database. You also have the option of connecting to the AdventureWorks2012 database.  
  
### To define a data connection and DataTable by adding a DataSet (for child report)  
  
1.  On the **Website** menu, click **Add New Item**.  
  
2.  In the **Add New Item** dialog box, click **DataSet** and then click **Add**. When prompted, you should add the item to the **App_Code** folder by clicking **Yes**.  
  
     This adds a new XSD file **DataSet2.xsd** to the project and opens the DataSet Designer.  
  
3.  From the Toolbox window, drag a **TableAdapter** control to the design surface. This launches the **TableAdapter** Configuration Wizard.  
  
4.  On the **Choose Your Data Connection** page, click **New Connection**.  
  
5.  In the **Add Connection** dialog box, perform the following steps:  
  
    1.  In the **Server name** box, enter the server where the **AdventureWorks2008** database is located.  
  
         The default SQL Server Express instance is **(local)\sqlexpress**.  
  
    2.  In the **Log on to the server** section, select the option that provides you access to the data. **Use Windows Authentication** is the default.  
  
    3.  From the **Select or enter a database name** drop-down list, click **AdventureWorks2008**.  
  
    4.  Click **OK**, and then click **Next**.  
  
6.  If you selected **Use SQL Server Authentication** in Step 5 (b), select the option whether to include the sensitive data in the string or set the information in your application code.  
  
7.  On the **Save the Connection String to the Application Configuration File** page, type in the name for the connection string or accept the default **AdventureWorks2008ConnectionString**. Click **Next**.  
  
8.  On the **Choose a Command Type** page, select **Use SQL Statements**, and then click **Next**.  
  
9. On the **Enter a SQL Statement** page, enter the following Transact-SQL query to retrieve data from the **AdventureWorks2008** database, and then click **Next**.  
  
    ```  
    SELECT PurchaseOrderID, PurchaseOrderDetailID, OrderQty, ProductID, ReceivedQty, RejectedQty, StockedQty FROM Purchasing.PurchaseOrderDetail  
    ```  
  
     You can also create the query by clicking **Query Builder**, and then verify the query by clicking **Execute Query** button. If the query does not return the expected data, you might be using an earlier version of AdventureWorks. For more information about installing the **AdventureWorks2008** version of AdventureWorks, see [Walkthrough: Installing the AdventureWorks Database](https://msdn.microsoft.com/library/aa992075\(v=vs.100\).aspx).  
  
10. On the **Choose Methods to Generate** page, uncheck **Create methods to send updates directly to the database (GenerateDBDirectMethods)**, and then click **Finish**.  
  
     You have now completed configuring the ADO.NET [DataTable](https://msdn.microsoft.com/library/system.data.datatable\(v=vs.100\).aspx) as data source for your report. On the DataSet Designer page in Visual Studio, you should see the **DataTable** you added, listing the columns specified in the query. DataSet2 contains the data from the PurhcaseOrderDetail table, based on the query.  
  
11. Save the file.  
  
12. To preview the data, click **Preview Data** on the **Data** menu, and then click **Preview**.  
  
## Next Task  
 You have successfully created a data connection and data table for the child report. Next, you will design the child report using the Report Wizard.  
  
  
