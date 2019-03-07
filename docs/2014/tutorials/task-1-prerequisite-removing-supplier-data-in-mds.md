---
title: "Task 1 (Prerequisite): Removing Supplier Data in MDS | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 6f0a4287-7fd4-4f18-b7e4-a5191a9d4a3c
author: leolimsft
ms.author: lle
manager: craigg
---
# Task 1 (Prerequisite): Removing Supplier Data in MDS
  In this task, you remove the supplier data stored in MDS. You uploaded the data manually using **MDS Excel Add-in** in the previous lesson. The SSIS package you create in this lesson will automatically upload the data to MDS for you. Therefore, before testing the SSIS package, you need to remove the supplier data from MDS, remove the derived hierarchy, remove supplier and state entities, and create the supplier entity with no data.  
  
1.  Launch **Master Data Manager** by navigating to **http://localhost/MDS** or the website and application you specified when configuring MDS. If you kept the **Master Data Manager** open, click **SQL Server 2012 Master Data Services** at the top to switch to the **home page**.  
  
2.  Click **System Administration** in the **Administrative Tasks** section.  
  
3.  Hover the mouse over **Manage** on the menu and click **Derived Hierarchies**. You need to delete the derived hierarchy **SuppliersInState** before deleting the entities in the **Suppliers** model.  
  
4.  Select **SuppliersInState** from the **Derived Hierarchy** list and click **X (Delete)** button on the toolbar.  
  
5.  Click **OK** to confirm deletion.  
  
6.  Hover the mouse over **Manage** on the menu and click **Entities**.  
  
7.  Click **Supplier** and click **Delete (X)** button on toolbar to delete the entity. Click **OK** on message boxes.  
  
8.  Repeat the previous step to delete **State** entity.  
  
9. Don't close **Master Data Manager**.  
  
10. Switch to the Excel window that has **Cleansed and Matched Suppliers.xls** file open. Switch to the **Sheet1** tab at the bottom.  
  
11. Select only the **first row with headers**. Don't select any other row. You want to create the entities based on the Excel columns but don't want to upload any data. Therefore you select only the first row with the headers.  
  
12. Click **Master Data** on the menu bar.  
  
13. Click **Create Entity** from the ribbon.  
  
14. In **Manage Connections** dialog box, if you do not see the connection to **local MDS server** under **Existing connections**, do the following:  
  
    1.  Select **Create a new connection**, and click **New** button.  
  
    2.  In the Add New Connection dialog box, type **Local MDS Server** for **Description** and **http://localhost/MDS** for **MDS server address**, and click **OK** to close the dialog box.  
  
15. In **Manage Connections** dialog box, select **Local MDS Server** (http://localhost/MDS), click **Test** to test the connection. Click **OK** on the message box.  
  
16. Click **Connect** to make a connection to the MDS server.  
  
17. In the **Create Entity** dialog box, do the following:  
  
    1.  Confirm that **Range** is set to **$1:$1**.  
  
    2.  Select **Suppliers** for **Model**.  
  
    3.  Select **VERSION_1** for **Version**.  
  
    4.  Type **Supplier** for **New entity name**.  
  
    5.  Select **SupplierID** for **Code**.  
  
    6.  Select **Supplier Name** for **Name**.  
  
    7.  Click **OK** to create the entity and close the dialog box.  
  
18. Close **Excel** and **do not save** the file.  
  
19. In **Master Data Manager**, refresh the internet browser and confirm that **Supplier** entity is displayed in the list.  
  
20. Switch to the **home page** by clicking **SQL Server 2012 Master Data Services** at the top.  
  
21. Confirm that **Suppliers** model is selected for **Model** and **VERSION_1** is selected for **Version**.  
  
22. Click **Explorer**. Notice that the **Supplier** entity with all the attributes is created with **no values**.  
  
## Next Step  
 [Task 2 &#40;Optional&#41;: Creating a MDS Subscription View using Master Data Manager](../../2014/tutorials/task-2-optional-creating-a-mds-subscription-view-using-master-data-manager.md)  
  
  
