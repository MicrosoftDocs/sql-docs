---
title: "Tutorial: Build a custom insight widget in SQL Operations Studio (preview) | Microsoft Docs"
description: This tutorial demonstrates how to build custom insight widgets and add them to database and server dashboards in SQL Operations Studio (preview).
ms.custom: "tools|sos"
ms.date: "11/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
author: "erickangMSFT"
ms.author: "erickang"
manager: craigg
ms.workload: "Inactive"
---

# Tutorial: Build a custom insight widget

This tutorial demonstrates how to use your own insight queries to build custom insight widgets.

During this tutorial you learn how to:
> [!div class="checklist"]
> * Run your own query and view it in a chart
> * Build a custom insight widget from the chart
> * Add the chart to a server or database dashboard

## Prerequisites

This tutorial requires the SQL Server or Azure SQL Database *TutorialDB*. To create the *TutorialDB* database, complete one of the following quickstarts:

- [Connect and query SQL Server using [!INCLUDE[name-sos-short](../includes/name-sos-short.md)]](quickstart-sql-server.md)
- [Connect and query Azure SQL Database using [!INCLUDE[name-sos-short](../includes/name-sos-short.md)]](quickstart-sql-database.md)


## Run your own query and view the result in a chart view
In this step, run a sql script to query the current active sessions.

1. To open a new editor, press **Ctrl+N** . 
1. Change the connection context to **TutorialDB**.

1. Paste the following query into the query editor:

   ```sql
   SELECT count(session_id) as [Active Sessions]
   FROM sys.dm_exec_sessions
   WHERE status = 'running'
   ```
1. Save the query in the editor to a \*.sql file. For this tutorial, save the script as *activeSessions.sql*.
1. To execute the query, press **F5**.
1. After the query results are displayed, click **View as Chart**, then click the **Chart Viewer** tab.
1. Change **Chart Type** to **count**. These settings render a count chart.

## Add the custom insight to the database dashboard

1. To open the insight widget configuration, click **Create Insight** on *Chart Viewer*:
   ![configuration](./media/tutorial-build-custom-insight-sql-server/create-insight.png)
   
2. Copy the insight configuration (the JSON data). 

3. Press **Ctrl+Comma** to open *User Settings*.

4. Type *dashboard* in *Search Settings*.

1. Click **Edit** for *dashboard.server.widgets*.

   ![dashboard settings](./media/tutorial-build-custom-insight-sql-server/dashboard-settings.png)

5. Paste the insight configuration JSON into *dashboard.database.widgets*. Database dashboard settings looks like the following:

   ```json
    "dashboard.database.widgets": [
            {
            "name": "My-Widget",
            "gridItemConfig": {
                "sizex": 1,
                "sizey": 1
            },
            "widget": {
                "insights-widget": {
                    "type": {
                        "count": {
                            "dataDirection": "vertical",
                            "dataType": "number",
                            "legendPosition": "none",
                            "labelFirstColumn": false,
                            "columnsAsLabels": false
                        }
                    },
                    "queryFile": "{your file folder}/activeSession.sql"
                }
            }
        }
   ```
6. Save the *User Settings* file and open the *TutorialDB* database dashboard to see the active sessions widget:

   ![activesession insight](./media/tutorial-build-custom-insight-sql-server/insight-activesession-dashboard.png) 


## Next steps
In this tutorial, you learned how to:
> [!div class="checklist"]
> * Run your own query and view it in a chart
> * Build a custom insight widget from the chart
> * Add the chart to a server or database dashboard

To learn how to backup and restore databases, complete the next tutorial:

> [!div class="nextstepaction"]
> [Backup and restore databases](tutorial-backup-restore-sql-server.md).