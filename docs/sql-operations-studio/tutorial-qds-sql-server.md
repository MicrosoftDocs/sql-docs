---
title: Add available Insight widgets to the database management dashboard | Microsoft Docs
description: Monitor a database by adding a pre-built query performance widget to the database management dashboard.
keywords: 
ms.custom: "tools|sos"
ms.date: "11/01/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sanagama; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
author: "erickangMSFT"
ms.author: "erickang"
manager: craigg
ms.workload: "Inactive"
---

# Monitor Query Performance with [!INCLUDE[name-sos](../includes/name-sos-short.md)]
In this tutorial, you walk through the process of adding one of [!INCLUDE[name-sos](../includes/name-sos-short.md)]'s built-in insight widgets to the *database dashboard*.  to quickly view a database's five slowest queries using [Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md). You also learn how to view the details of the slow queries and query plans using [!INCLUDE[name-sos](../includes/name-sos-short.md)]'s features. After following through this tutorial, you will learn how to:

> [!div class="checklist"]
> * Enable Query Data Store on TutorialDB
> * Quickly turn on an insight widget using a built-in insight widget sample.
> * View the details of the top five slowest queries.
> * Open the query script in editor.
> * View query plans.


## Prerequisites
* Follow [Get Started with [!INCLUDE[name-sos](../includes/name-sos-short.md)]](./get-started-sql-server.md) to a SQL Server 2017 instance and TutorialDB database.



## Turn on Query Store for the TutorialDB database

Enable Query Data Store by executing following T-SQL statement on TutorialDB:

   ```sql
    ALTER DATABASE TutorialDB SET QUERY_STORE = ON
   ```

## Add a pre-built management insight to [!INCLUDE[name-sos](../includes/name-sos-short.md)]'s database dashboard
[!INCLUDE[name-sos](../includes/name-sos-short.md)] has a built-in sample widget to monitor the top five slowest query using query performance information collected by Query Data Store. With a few simple steps, you can easily visualize and use the information to improve your database and application.

1. Open *User Settings* by pressing **Ctrl+Shift+P** to open the *Command Palette*.
2. Type *settings* in the command search input box and select 'Preferences: Open User Settings' command.

   ![Open user settings command](./media/tutorial-sql-server/open-user-settings.png)

2. Type 'dashboard' in Settings Search input box to search "dashboard.database.widgets" in Settings.

   ![Search settings](./media/tutorial-sql-server/search-settings.png)

3. Click 'Copy to Settings' to copy "dashboard.database.widgets" settings to customize.

4. Using [!INCLUDE[name-sos](../includes/name-sos-short.md)]'s insight settings IntelliSense, configure 'name' for the widget title, 'gridItemConfig' for the widget size, and 'widget' by selecting 'query-data-store-database-insight' from the drop-down list as shown in the screenshot below:

   ![Insight qds settings](./media/tutorial-sql-server/insight-qds-settings.png)

5. Press 'CTRL + s' to save the user's settings file.

6. Open Database dashboard by navigate to 'TutorialDB' in Servers viewlet, and click 'Manage' in the context menu.

   ![Open dashboard](./media/tutorial-sql-server/insight-open-dashboard.png)

7. View 'Top five slowest query insight graph' as shown in the screen shot below: 

   ![QDS widget](./media/tutorial-sql-server/insight-qds-result.png)


## View insight details dialog for know more about the insight

1. Click 'Show Insight' context menu. It opens Insights detail dialog as shown in the screenshot:

   ![Insight detail dialog](./media/tutorial-sql-server/insight-details-dialog.png)

2. Click any item in 'Chart Data' list to show more detail of each item in the list.

3. Select 'query_sql_txt' field in 'Query Data' panel and click 'Copy Selection'.

## View the query plan using Explain

1. Open a new editor by pressing 'CTRL + N'.

2. Paste the query sql text by pressing 'CTRL + V' in the editor.

3. Click 'Explain' button.

   ![Insight QDS Explain](./media/tutorial-sql-server/insight-qds-explain.png)

4. View the showplan.

   ![showplan](./media/tutorial-sql-server/showplan.png)

## View the query plan in Query Data Store

1. Open the insight detail dialog again.

2. Select and copy 'query_plan'

   ![Insights QDS plan](./media/tutorial-sql-server/insight-qds-plan.png)

3. Press 'CTRL+N' to open a new editor.

4. Paste the copied plan data to the editor.

5. Press 'CTL + S' to save the file and change the file extension to *.showplan

6. The query plan opens in [!INCLUDE[name-sos](../includes/name-sos-short.md)]'s query plan viewer.

   >> TBD - screenshot

## Next Steps
In this tutorial, you learned how to:
> [!div class="checklist"]
> * Enable Query Data Store on TutorialDB
> * Quickly turn on an insight widget using a built-in insight widget sample.
> * View the details of the top five slowest queries.
> * Open the query script in editor.
> * View query plans.

Next, learn how to use X, try this tutorial: 
> [!div class="nextstepaction"]
> [What article is next in sequence](tutorial-monitoring-sql-server.md)