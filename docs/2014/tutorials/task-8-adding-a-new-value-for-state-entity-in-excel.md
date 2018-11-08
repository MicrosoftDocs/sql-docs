---
title: "Task 8: Adding a New Value for State Entity in Excel | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "data-quality-services"
  - "integration-services"
  - "master-data-services"
ms.topic: conceptual
ms.assetid: a763d76b-06a3-4d51-9614-01fc9fb1c158
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Task 8: Adding a New Value for State Entity in Excel
  In this task, you add a value for the State entity in Excel and publish the change to the MDS server.  
  
1.  Add a **work sheet** in Excel by clicking the new tab at the bottom.  
  
     ![Excel - New Worksheet Tab](../../2014/tutorials/media/et-addinganewvalueforstateentityinexcel-01.jpg "Excel - New Worksheet Tab")  
  
2.  In **Excel**, click the **Master Data** tab on the menu, and then click **Show Explorer** on the ribbon.  
  
3.  In the **Master Data Explorer**, select **Suppliers** for **Model**. You should see two entities: **Supplier** and **State** in the entity list.  
  
4.  Double-click **State** in the list. All the members of the **State** entity from MDS should be displayed in the worksheet.  
  
5.  Now, add a row at the end with the following values: **North Carolina** for **Name** and **NC** for **Code**. The color coding differentiates any new/updated records from the other records.  
  
     ![Excel - Add North Carolina to States](../../2014/tutorials/media/et-addinganewvalueforstateentityinexcel-02.jpg "Excel - Add North Carolina to States")  
  
6.  Click **Publish** on the ribbon to publish the change to MDS.  
  
     ![Excel - Publish Button on Master Data Tab](../../2014/tutorials/media/et-addinganewvalueforstateentityinexcel-03.jpg "Excel - Publish Button on Master Data Tab")  
  
7.  On the **Publish and Annotate** dialog box, notice that the **Use same annotation for all changes** is selected. You can enter a single annotation for all the changes here.  
  
8.  Select **Review changes and provide annotations individually** option to provide annotation for each change (in this case, only one).  
  
     ![Excel - Publish and Annotate Dialog Box](../../2014/tutorials/media/et-addinganewvalueforstateentityinexcel-04.jpg "Excel - Publish and Annotate Dialog Box")  
  
9. Click **Publish** to publish data to MDS.  
  
10. Notice that **color coding** for the row with **North Carolina** as the **State** is same as other records now.  
  
11. **Optional:** Verify that the new member (NC) is added to the **State** entity by using the **Explorer** in **Master Data Manager**.  
  
12. In Excel, right-click the **State** worksheet at the bottom, and click **Delete** to delete the worksheet. Deleting the worksheet does not delete any data from the MDS server.  
  
## Next Step  
 [Task 9: Creating a Derived Hierarchy using Master Data Manager](../../2014/tutorials/task-9-creating-a-derived-hierarchy-using-master-data-manager.md)  
  
  
