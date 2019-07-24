---
title: "Lesson 1: Publishing Data Using Merge Replication | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: c3c6e0b6-54cd-4b7d-8efb-2cefe14fcd7f
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 1: Publishing Data Using Merge Replication
  In this lesson, you will create a merge publication using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to publish a subset of the **Employee**, **SalesOrderHeader**, and **SalesOrderDetail** tables in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. These tables are filtered with parameterized row filters so that each subscription contains a unique partition of the data. You will also add the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login used by the Merge Agent to the publication access list (PAL). This tutorial requires that you have completed the previous tutorial, [Preparing the Server for Replication](tutorial-preparing-the-server-for-replication.md).  
  
### To create a publication and define articles  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, right-click **Local Publications**, and click **New Publication**.  
  
     The Publication Configuration Wizard launches.  
  
3.  On the Publication Database page, select [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)], and then click **Next**.  
  
4.  On the Publication Type page, select **Merge publication**, and then click **Next**.  
  
5.  On the Subscriber Types page, ensure that only [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later is selected, and then click **Next**.  
  
6.  On the Articles page, expand the **Tables** node, select **SalesOrderHeader** and **SalesOrderDetail**, then expand **Employee**, select **EmployeeID** or **LoginID**, and then click **Next**.  
  
    > [!TIP]  
    >  Additional required columns are automatically selected. Select any of  the automatically selected columns and view the note below the **Objects to publish** list for an explanation why the column is required.  
  
7.  On the Filter Table Rows page, click **Add** and then click **Add Filter**.  
  
8.  In the **Add Filter** dialog box, select **Employee (HumanResources)** in **Select the table to filter**, click the **LoginID** column, click the right arrow to add the column to the WHERE clause of the filter query, and modify the WHERE clause as follows:  
  
    ```  
    WHERE [LoginID] = HOST_NAME()  
    ```  
  
9. Click **A row from this table will go to only one subscription**, and click **OK**.  
  
10. On the **Filter Table Rows** page, click **Employee (Human Resources)**, click **Add,** and then click **Add Join to Extend the Selected Filter**.  
  
11. In the **Add Join** dialog box, select **Sales.SalesOrderHeader** under **Joined table**, click **Write the join statement manually**, and complete the join statement as follows:  
  
    ```  
    ON Employee.EmployeeID = SalesOrderHeader.SalesPersonID  
    ```  
  
12. In **Specify join options**, select **Unique key**, and then click **OK**.  
  
13. On the Filter Table Rows page, click **SalesOrderHeader**, click **Add**, and then click **Add Join to Extend the Selected Filter**.  
  
14. In the **Add Join** dialog box, select **Sales.SalesOrderDetail** under **Joined table**.  
  
15. Click **Write the join statement manually**.  
  
16. In **Filtered table columns**, select **BusinessEntityID**, then click the arrow button to copy the column name to the loin statement.  
  
17. In the **Join statement** box, complete the join statement as follows:  
  
    ```  
    ON Employee.BusinessEntityID = SalesOrderHeader.SalesPersonID  
    ```  
  
18. In **Specify join options**, select **Unique key**, and then click **OK**.  
  
19. On the **Filter Table Rows** page, click **SalesOrderHeader (Sales)**, click **Add**, and then click **Add Join to Extend the Selected Filter**.  
  
20. In the **Add Join** dialog box, select **Sales.SalesOrderDetail** under **Joined table**, click **OK**, and then click **Next**.  
  
21. Select **Create a snapshot immediately**, clear **Schedule the snapshot agent to run at the following times**, and click **Next**.  
  
22. On the Agent Security page, click **Security Settings**, type \<_Machine_Name>_**\repl_snapshot** in the **Process account** box, supply the password for this account, and then click **OK**. Click **Finish**.  
  
23. On the Complete the Wizard page, enter **AdvWorksSalesOrdersMerge** in the **Publication name** box and click **Finish**.  
  
24. After the publication is created, click **Close**.  
  
### To view the status of snapshot generation  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the Local Publications folder, right-click **AdvWorksSalesOrdersMerge**, and then click **View Snapshot Agent Status**.  
  
3.  The current status of the Snapshot Agent job for the publication is displayed. Ensure that the snapshot job has succeeded before you continue to the next lesson.  
  
### To add the Merge Agent login to the PAL  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the Local Publications folder, right-click **AdvWorksSalesOrdersMerge**, and then click **Properties**.  
  
     The **Publication Properties** dialog box is displayed.  
  
3.  Select the **Publication Access List** page, and click **Add**.  
  
4.  In the Add Publication Access dialog box, select _<Machine_Name>_**\repl_merge** and click **OK**. Click **OK**.  
  
## Next Steps  
 You have successfully created the merge publication. Next, you will subscribe to this publication. See [Lesson 2: Creating a Subscription to the Merge Publication](lesson-2-creating-a-subscription-to-the-merge-publication.md).  
  
## See Also  
 [Filter Published Data](publish/filter-published-data.md)   
 [Parameterized Row Filters](merge/parameterized-filters-parameterized-row-filters.md)   
 [Define an Article](publish/define-an-article.md)  
  
  
