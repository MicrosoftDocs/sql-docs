---
title: "Lesson 11: Create Partitions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 92eb21a8-5fc4-4999-ad37-1332ce26431d
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 11: Create Partitions
  In this lesson, you will create partitions to divide the Internet Sales table into smaller logical parts that can be processed (Refreshed) independent of other partitions. By default, every table you include in your model has one partition which includes all of the table's columns and rows. For the Internet Sales table, we want to divide the data by year; one partition for each of the table's five years.  Each partition can then be processed independently. To learn more, see [Partitions &#40;SSAS Tabular&#41;](tabular-models/partitions-ssas-tabular.md).  
  
 Estimated time to complete this lesson: **15 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 10: Create Hierarchies](lesson-9-create-hierarchies.md).  
  
## Create Partitions  
  
#### To create partitions in the Internet Sales table  
  
1.  In the model designer, click on the **Internet Sales** table, then click on the **Table** menu, and then click **Partitions**.  
  
     The **Partition Manager** dialog box opens.  
  
2.  In the **Partition Manager** dialog box, in **Partitions**, click the **Internet Sales** partition.  
  
3.  In **Partition Name**, change the name to `Internet Sales 2005`.  
  
    > [!TIP]  
    >  Before continuing to the next step, notice the column names in the Table Preview window display those columns included in the model table (checked) with the column names from the source. This is because the Table Preview window displays columns from the source table, not from the model table.  
  
4.  Select the **Query Editor** button just above the right side of the preview window.  
  
     Because you want the partition to include only those rows within a certain period, you must include a WHERE clause. You can only create a WHERE clause by using a SQL Statement.  
  
5.  In the **SQL Statement** field, replace the existing statement by pasting in the following statement:  
  
    ```  
    SELECT   
    [dbo].[FactInternetSales].[ProductKey],  
    [dbo].[FactInternetSales].[CustomerKey],  
    [dbo].[FactInternetSales].[PromotionKey],  
    [dbo].[FactInternetSales].[CurrencyKey],  
    [dbo].[FactInternetSales].[SalesTerritoryKey],  
    [dbo].[FactInternetSales].[SalesOrderNumber],  
    [dbo].[FactInternetSales].[SalesOrderLineNumber],  
    [dbo].[FactInternetSales].[RevisionNumber],  
    [dbo].[FactInternetSales].[OrderQuantity],  
    [dbo].[FactInternetSales].[UnitPrice],  
    [dbo].[FactInternetSales].[ExtendedAmount],  
    [dbo].[FactInternetSales].[UnitPriceDiscountPct],  
    [dbo].[FactInternetSales].[DiscountAmount],  
    [dbo].[FactInternetSales].[ProductStandardCost],  
    [dbo].[FactInternetSales].[TotalProductCost],  
    [dbo].[FactInternetSales].[SalesAmount],  
    [dbo].[FactInternetSales].[TaxAmt],  
    [dbo].[FactInternetSales].[Freight],  
    [dbo].[FactInternetSales].[CarrierTrackingNumber],  
    [dbo].[FactInternetSales].[CustomerPONumber],  
    [dbo].[FactInternetSales].[OrderDate],  
    [dbo].[FactInternetSales].[DueDate],  
    [dbo].[FactInternetSales].[ShipDate]   
    FROM [dbo].[FactInternetSales]  
    WHERE (([OrderDate] >= N'2005-01-01 00:00:00') AND ([OrderDate] < N'2006-01-01 00:00:00'))  
    ```  
  
     This statement specifies the partition should include all of the data in those rows where the OrderDate is for the 2005 calendar year as specified in the WHERE clause.  
  
6.  Click **Validate**.  
  
     Notice a warning is displayed stating that certain columns are not present in source. This is because in [Lesson 3: Rename Columns](rename-columns.md), you renamed those columns in the Internet Sales table in the model to be different from those same columns at the source.  
  
#### To create a partition for the 2006 year in the Internet Sales table  
  
1.  In the **Partition Manager** dialog box, in **Partitions**, click the `Internet Sales 2005` partition you just created, and then **Copy**.  
  
2.  In **Partition Name**, type `Internet Sales 2006`.  
  
3.  In the SQL Statement, in-order for the partition to include only those rows for the 2006 year, replace the WHERE clause with the following:  
  
    ```  
    WHERE (([OrderDate] >= N'2006-01-01 00:00:00') AND ([OrderDate] < N'2007-01-01 00:00:00'))  
    ```  
  
#### To create a partition for the 2007 year in the Internet Sales table  
  
1.  In the **Partition Manager** dialog box, click **Copy**.  
  
2.  In **Partition Name**, type `Internet Sales 2007`.  
  
3.  In **Switch To**, select **Query Editor**.  
  
4.  In the SQL Statement, in-order for the partition to include only those rows for the 2007 year, replace the WHERE clause with the following:  
  
    ```  
    WHERE (([OrderDate] >= N'2007-01-01 00:00:00') AND ([OrderDate] < N'2008-01-01 00:00:00'))  
    ```  
  
#### To create a partition for the 2008 year in the Internet Sales table  
  
1.  In the **Partition Manager** dialog box, click **New**.  
  
2.  In **Partition Name**, type `Internet Sales 2008`.  
  
3.  In **Switch To**, select **Query Editor**.  
  
4.  In the SQL Statement, in-order for the partition to include only those rows for the 2008 year, replace the WHERE clause with the following:  
  
    ```  
    WHERE (([OrderDate] >= N'2008-01-01 00:00:00') AND ([OrderDate] < N'2009-01-01 00:00:00'))  
    ```  
  
#### To create a partition for the 2009 year in the Internet Sales table  
  
1.  In the **Partition Manager** dialog box, click **New**.  
  
2.  In **Partition Name**, type `Internet Sales 2009`.  
  
3.  In **Switch To**, select **Query Editor**.  
  
4.  In the SQL Statement, in-order for the partition to include only those rows for the 2009 year, replace the WHERE clause with the following:  
  
    ```  
    WHERE (([OrderDate] >= N'2009-01-01 00:00:00') AND ([OrderDate] < N'2010-01-01 00:00:00'))  
    ```  
  
## Process Partitions  
 In the **Partition Manager** dialog box, notice the asterisk (**\***) next to the partition names for each of the new partitions you just created. This indicates that the partition has not been processed (refreshed). When you create new partitions, you should run a Process Partitions or Process Table operation to refresh the data in those partitions.  
  
#### To process Internet Sales partitions  
  
1.  Click **OK** to close the **Partition Manager** dialog box.  
  
2.  In the model designer, click the **Internet Sales** table, then click the **Model** menu, then point to **Process** (Refresh), and then click **Process Partitions**.  
  
3.  In the **Process Partitions** dialog box, verify the **Mode** is set to **Process Default**.  
  
4.  Select the checkbox in the **Process** column for each of the five partitions you created, and then click **OK**.  
  
     If you are prompted for Impersonation credentials, enter the Windows user name and password you specified in Lesson 2, step 6.  
  
     The **Data Process** dialog box then appears and displays process details for each partition. Notice that a different number of rows for each partition are transferred. This is because each partition includes only those rows for the year specified in the WHERE clause in the SQL Statement. There is no data for the 2010 year.  
  
## Next Steps  
 To continue this tutorial, go to the next lesson: Lesson: [Lesson 12: Create Roles](lesson-11-create-roles.md).  
  
  
