---
title: Build a custom insight widget
description: This tutorial demonstrates how to build custom insight widgets and add them to database and server dashboards in Azure Data Studio.
author: markingmyname
ms.author: maghan
ms.reviewer: alayu
ms.date: 08/26/2020
ms.service: azure-data-studio
ms.topic: tutorial
ms.custom: seodec18
---

# Tutorial: Build a custom insight widget

This tutorial demonstrates how to use your own insight queries to build custom insight widgets.

During this tutorial you learn how to:
> [!div class="checklist"]
> * Run your own query and view it in a chart
> * Build a custom insight widget from the chart
> * Add the chart to a server or database dashboard
> * Add details to your custom insight widget

## Prerequisites

This tutorial requires the SQL Server or Azure SQL Database *TutorialDB*. To create the *TutorialDB* database, complete one of the following quickstarts:

- [Connect and query SQL Server using [!INCLUDE[name-sos-short](../includes/name-sos-short.md)]](quickstart-sql-server.md)
- [Connect and query Azure SQL Database using [!INCLUDE[name-sos-short](../includes/name-sos-short.md)]](quickstart-sql-database.md)


## Run your own query and view the result in a chart view
In this step, run a sql script to query the current active sessions.

1. To open a new editor, press **Ctrl+N**. 

2. Change the connection context to **TutorialDB**.

3. Paste the following query into the query editor:

   ```sql
   SELECT count(session_id) as [Active Sessions]
   FROM sys.dm_exec_sessions
   WHERE status = 'running'
   ```

4. Save the query in the editor to a \*.sql file. For this tutorial, save the script as *activeSession.sql*.

5. To execute the query, press **F5**.

6. After the query results are displayed, click **View as Chart**, then click the **Chart Viewer** tab.

7. Change **Chart Type** to **count**. These settings render a count chart.

## Add the custom insight to the database dashboard

1. To open the insight widget configuration, click **Create Insight** on *Chart Viewer*:

   ![configuration](./media/tutorial-build-custom-insight-sql-server/create-insight.png)
   
2. Copy the insight configuration (the JSON data). 

3. Press **Ctrl+Comma** to open *User Settings*.

4. Type *dashboard* in *Search Settings*.

5. Click **Edit** for *dashboard.database.widgets*.

   ![dashboard settings](./media/tutorial-build-custom-insight-sql-server/dashboard-settings.png)

6. Paste the insight configuration JSON into *dashboard.database.widgets*. Database dashboard settings looks like the following:

   ```json
    "dashboard.database.widgets": [
        {
            "name": "My-Widget",
            "gridItemConfig": {
                "sizex": 2,
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
    ]
   ```

7. Save the *User Settings* file and open the *TutorialDB* database dashboard to see the active sessions widget:

   ![activesession insight dashboard](./media/tutorial-build-custom-insight-sql-server/insight-activesession-dashboard.png)

## Add details to custom insight

1. To open a new editor, press **Ctrl+N**.

2. Change the connection context to **TutorialDB**.

3. Paste the following query into the query editor:

   ```sql
    SELECT session_id AS [SID], login_time AS [Login Time], host_name AS [Host Name], program_name AS [Program Name], login_name AS [Login Name]
    FROM sys.dm_exec_sessions
    WHERE status = 'running'
   ```

4. Save the query in the editor to a \*.sql file. For this tutorial, save the script as *activeSessionDetail.sql*.

5. Press **Ctrl+Comma** to open *User Settings*.

6. Edit the existing *dashboard.database.widgets* node in your settings file:

   ```json
    "dashboard.database.widgets": [
        {
            "name": "My-Widget",
            "gridItemConfig": {
                "sizex": 2,
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
                    "queryFile": "{your file folder}/activeSession.sql",
                    "details": {
                        "queryFile": "{your file folder}/activeSessionDetail.sql",
                        "label": "SID",
                        "value": "Login Name"
                    }
                }
            }
        }
    ]
   ```

7. Save the *User Settings* file and open the *TutorialDB* database dashboard. Click the ellipsis (...) button next to *My-Widget* to show the details:

    ![activesession insight detail](./media/tutorial-build-custom-insight-sql-server/insight-activesession-detail.png)

## Next steps
In this tutorial, you learned how to:
> [!div class="checklist"]
> * Run your own query and view it in a chart
> * Build a custom insight widget from the chart
> * Add the chart to a server or database dashboard
> * Add details to your custom insight widget

To learn how to backup and restore databases, complete the next tutorial:

> [!div class="nextstepaction"]
> [Backup and restore databases](tutorial-backup-restore-sql-server.md).
