---
title: "How to: Use Schema Compare to Compare Different Database Definitions | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.schemacompare.SchemaCompareOptionsDialog"
  - "sql.data.tools.schemacompare.watermark.f1"
  - "sql.data.tools.schemacompare.f1"
  - "sql.data.tools.schemacompare.connectiondialog.f1"
  - "sql.data.tools.schemacompare.connectiondialog.error.f1"
ms.assetid: 7f0905a4-081c-46e2-bd7d-325b63e5c675
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Use Schema Compare to Compare Different Database Definitions
SQL Server Data Tools (SSDT) includes a Schema Compare utility that you can use to compare two database definitions.  The source and target of the comparison can be any combination of connected database, SQL Server database project or snapshot or .dacpac file.  The results of the comparison appear as a set of actions that must be taken with the target to make it the same as the source.  Once the comparison is complete you can update the target directly (if the target is a project or a database) or generate an update script that has the same effect.  
  
The differences between source and target appear in a grid for easy review.  You can drill into and review each difference in the results grid or in script form.  You can then selectively exclude specific differences.  
  
You can save comparisons either as part of a SQL Server Database project or as a standalone file.  You can also set options that control the scope of the comparison and aspects of the update.  Then you can save the comparison so that you can easily repeat the same comparison later or use it as the starting point for new comparison.  
  
The following procedure compares the schema of a database project with a connected database.  
  
> [!WARNING]  
> If a project is specified as the target for comparison, the maximum supported path length (excluding drive letter, colon and leading backslash) for the project is 256 characters. If your project path exceeds 256 characters, you will still be able to compare its schema with a database or another project. However, you will not be able to update its schema.  
  
> [!WARNING]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To compare database definitions  
  
1.  On the **SQL** menu, select **Schema Compare**, and then click **New Schema Comparison**.  
  
    Alternatively, right-click the **TradeDev** project in **Solution Explorer**, and select **Schema Compare**.  
  
    The **Schema Compare** window opens, and Visual Studio automatically assigns it a name such as `SqlSchemaCompare1`.  
  
    Two drop-down menus with a green arrow in between them appear just below the **Schema Compare** window toolbar. These menus allow you to select database definitions for your comparison source and target.  
  
2.  In the **Select Source** drop-down, choose **Select Source** and the **Select Source Schema** dialog opens.  
  
    Notice that if you opened the **Schema Compare** window by right clicking the project name, the source schema is already populated and you can proceed to step 4.  
  
3.  Select the **Project** radio button, and then select the **TradeDev** database project you have created in the previous procedure.  
  
4.  From the **Select Target** drop-down in the **Schema Compare Window** , choose **Select Target**, and the **Select Target Schema** dialog opens. In the **Schema** section, click the **Database** radio button, then click the **New Connection** button.  
  
5.  In the **Connection Properties** dialog box, enter the server name where the `TradeDev` database resides and make sure that correct authentication credentials are provided. Then select **TradeDev** in **Connect to a database** and click **OK**.  
  
    You can also click the **Options** button in the  **Schema Compare Window** toolbar to specify which objects are compared, what types of differences are ignored, and other settings.  
  
6.  Click the **Compare** button in the **Schema Compare Window** toolbar to start the comparison process.  
  
    When the comparison is complete, the structural differences between the project and the database appear in the **Results** pane in the upper part of the window. By default, the comparison results group all the differences are grouped by action (such as Delete, Change, or Add). The **Results** pane displays a row for each database object that differs between the database definitions. Each row identifies the object in the source or target schema (or both) and the action that will be taken on the target schema to make the target object the same as the source object.  If an object has been refactored and either renamed or moved to a new schema, the source and target names are different and the source name appears in bold font to highlight the difference.  
  
    By default the results list hides objects that are the same in both schemas or that are not supported for update (for example, built-in objects).  You can click on the appropriate filter buttons in the tool bar to show these objects.  
  
    To change the grouping preference, click the **Group Results** dropdown list in the toolbar.  Select **Type** to group the results by object type (for example, by tables, views, or stored procedures).  
  
7.  Find the `Products` table in the `Tables` group. Click on the row and notice that the source and target definitions of the table appear in the **Object Definitions** pane with the differences highlighted. You can also expand the `Products` table row in the **Results** pane to inspect the specific elements in the table that are different.  
  
8.  By default all differences are included in the scope of the Update Target action. You can exclude differences that you do not want to synchronize. To do so, uncheck the in the **Action** column in the center of each row. Alternatively, right-click a row in the Schema pane, and select **Exclude**. Notice that the row is immediately grayed out. When it is time to update the target database, this row will not be considered for any pending changes.  
  
    You can also right-click on a group row and select **Exclude All** or **Include All**, which is equivalent to unchecking or checking all differences in that group. When you group results by schema this is a useful way to include or exclude all changes to a specific schema.  
  
    > [!WARNING]  
    > If the row being excluded has any dependent objects (for example, a **Table** row that is referenced by a **View** row), the excluded row is disabled but its checkbox is not cleared. Once all rows that depend on it are unchecked, the disabled row will be unchecked. In addition, if a row is refactored (renamed or moved to another schema), then the checkbox is disabled for that row and any of its dependent child rows.  
    >   
    > Notice that if you refresh the comparison, those differences that you have chosen to skip will be ignored.  
  
To update the schema of the target, you have two choices. You can update the target directly from the **Schema Compare** window if the target is a database or project, or you can generate an update script if the target is a database or a database file.  A generated script appears in the Transact\-SQL Editor, from which you can inspect the script execute it against a database. The following procedures describe these options further.  
  
> [!WARNING]  
> The update will fail because our change involves changing a column from NOT NULL to NULL and as a result causes data loss. If you want to proceed with the update, click on the **Options** button (the fifth one from the left) on the toolbar for the Schema Compare and uncheck the **block incremental deployment if data loss** option.  
  
### To update directly in the Schema Compare window  
  
1.  Click the **Update** button on the toolbar for the Schema Compare window.  
  
2.  Examine the change script generated. You can save the script by using the File/New menu. This can be handy for situations when you are not authorized to update a production database, in which case you can give the script to a DBA for deployment later.  
  
3.  If you have the necessary permission to update the database, click the **Execute Query** button in editing pane toolbar to run the script.  
  
### To update by script  
  
1.  Click the **Generate Script** button (the fourth one from the left) on the toolbar for the Schema Compare window.  
  
    The generated script appears in a new Transact\-SQL Editor window  
  
    > [!WARNING]  
    > Only .dacpac files produced by the SSDT snapshot process support this behavior.  You cannot target a .dacpac file produced by the SQL Data-tier Application (DAC) tools or framework at this time.  
  
2.  Examine the generated change script. You can save the script by using the **File/Save** or File/Save As menu command.  
  
    A saved script can be handy in situations when you are not authorized to update a production database. In these cases you can give the script to a DBA for deployment later.  
  
    Alternatively, you can connect the Transact\-SQL Editor to an appropriate server and execute the script directly. Before you can perform this procedure you must have the necessary permission to create or update the database. If you have the necessary permission to update the database, click the **Execute Query** button in editing pane toolbar to run the script.  
  
3.  Click the **Connect** button. This action either connects to the current server or prompts you to enter or select a in on the Connect to Server dialog.  Notice that the database name is defined in the script as a command variable.  
  
4.  Inspect the script and, if necessary, make any changes to the command variables that define the target database name and associated prefix and the file paths.  
  
5.  Click the **Execute** button in the editing pane toolbar to run the script.  
  
