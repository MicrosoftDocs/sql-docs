---
title: Creating and managing tables in Azure Data Studio
description: How to use the Table Designer to manage tables and relationships in Azure Data Studio.
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.date: 05/05/2022
ms.service: azure-data-studio
ms.topic: tutorial
---

# Table Designer in Azure Data Studio

[!INCLUDE [sql-asdb-asdbmi](../includes/applies-to-version/sql-asdb-asdbmi.md)]

The Table Designer in Azure Data Studio provides a visual editor experience alongside the Transact-SQL Editor for creating and editing table structure, including table-specific programming objects, for SQL Server databases.

## Why Table Designer?

The Table Designer in Azure Data Studio provides users an easy way to configure and manage database tables, primary and foreign keys, indexes, and constraints directly on the graphical user interface (GUI) without needing to write Transact-SQL statements.

## Overview of the Table Designer

The Table Designer consists of a window split into three separate panes. The first pane is the Overview/General pane of the table design. This consists of tabs for the columns, primary and foreign keys, check constraints, indexes, and a general tab. The second pane is used for defining the properties of your table. Lastly, the third pane is the script pane for the read-only T-SQL script that shows actions performed on the table designer GUI in real time as well as any success or error messages associated with actions performed on the table designer. The size of these panes can be adjusted to preference by mouse dragging.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-panes.png" alt-text="Screenshot of Table Designer showing the three different panes.":::

### Create and update database tables using the Table Designer

The SQL server connection in which your database resides needs to be active in order to create tables. This is indicated by the green dot at the bottom right corner of the server connection icon in the object explorer (below). If red (this means the connection is inactive), select the server connection name in the object explorer to activate the connection. For a tutorial on connecting to SQL Server, check out the [Use Azure Data Studio to connect and query SQL Server tutorial](quickstart-sql-server.md) article.

> [!NOTE]
> We will be using the "AdventureWorks2019" sample database in this tutorial. If you haven't already, please refer to [AdventureWorks sample databases](../samples/adventureworks-install-configure.md) to download this sample database.

#### Create a table

1. Right-click the **Tables** folder in the **AdventureWorks2019** database drop-down and select **New Table**:

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-traversing-through-object-explorer-to-create-new-table.png" alt-text="Screenshot of Table Designer showing how to traverse object explorer to create a new table.":::

2. Change the value in the **Table Name** field from its' default, ***NewTable*** to ***City***. In the **Table Properties** field, feel free to add a description for this table.

3. In the **Name** Column grid, change the provided default value from ***column_1*** to  ***ID***. Select the checkbox in the **primary key** column to make this the primary key for the table. To read more on primary keys, [see this SQL Server documentation on Primary Keys](../relational-databases/tables/primary-and-foreign-key-constraints.md).

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-changing-column-name-adding-new-column-with-primary-key-t-sql-script.png" alt-text="Screenshot of Table Designer showing how to edit table name and add column showing primary key identifier. Also shows the T-SQL script generated from the Table Designer.":::

4. Repeat step 3 to add two new columns, ***CityName*** and ***Population***. Uncheck the **Primary Key** and **Allow Nulls** checkboxes for these two new columns. Please note that Columns can be re-arranged to user preference by placing the cursor in the **Move** column and mouse dragging.

    > [!NOTE]
    > Pay attention to the changes in the read-only Transact-SQL code generated as changes are made to the default table.

5. Now that we have finished the design for our new table, we need to publish this change to the **AdventureWorks2019** database. To do this, select the "Publish" icon as seen below. Publishing can also be done by using the save command shortcut on your local device.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-show-publish-icon.png" alt-text="Screenshot of Table Designer highlighting publish icon.":::

    Once this icon is selected, a preview dialog window appears showing you all the actions performed in creating a table. It also provides the option to publish the changes directly to the **AdventureWorks2019** database or generate the editable SQL script in a query editor that can be saved as a file locally or executed to publish this script to the database.

    :::image type="content" source="media/table-designer-azure-data-studio/save-dialog-for-table-designer.png" alt-text="Screenshot of Table Designer showing save dialog box.":::

    Running the T-SQL script in the query editor is done by selecting the **Run** button as shown below:

    :::image type="content" source="media/table-designer-azure-data-studio/change-publish-via-the-sql-command-script-table-designer.png" alt-text="Screenshot of Table Designer showing how to publish changes to the database in the Query Editor using the SQL CMD option.":::

    Ensure that the query editor is connected to the database on which the script is to be ran. This is done by selecting the **Connect** button in the query editor window. This will pull up the connection dialog box where you can enter in the credentials for the server you are running your database script on.

    >[!NOTE]
    > Please note that changes to the table design can be made manually in the query editor by editing the T-SQL script. **SQLCMD** mode must be enabled to successfully execute Transact-SQL scripts. This is done by toggling this button as shown in the image above (already enabled, which is why it shows **Disable SQLCMD**). To learn more about this, please refer to the [SQLCMD utility documentation](../tools/sqlcmd-utility.md).

6. Remember to save changes made on the table designer. Unsaved changes are indicated by the black shaded dot as shown below:

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-work-not-saved.png" alt-text="Screenshot of Table Designer showing work that is not saved as indicated by the presence of the black dot.":::

    This image below shows work that has been saved as indicated by the absence of the black dot.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-work-saved.png" alt-text="Screenshot of Table Designer showing work saved as indicated by the absence of the black dot.":::

    >[!NOTE]
    > Please pay attention to any provided warnings in the publish dialog as it relates to table creation and migrations. These are provided to guide against potential data loss or system downtime especially when working with larger data sets.

7. Once the table is published, right-click" the *Tables** folder and select **Refresh**. This re-repopulates the folder with the new table.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-refreshing-table.png" alt-text="Screenshot of Table Designer showing how to refresh table.":::

#### Editing an existing table

To edit an existing table in the Table Designer, right-click on the table in the object explorer and select "Design" from the menu. This opens up the table designer view, which then allows you to make edits where necessary. See below:

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-editing-exisitng-tables.png" alt-text="Screenshot of Table Designer showing how to edit an existing table.":::

You can change column properties in the main pane as shown above or in the properties pane. To view the properties pane for a column, select the column (***CityName***, for example) as shown below. The properties pane will then show the properties specific to the ***CityName*** column. Remember to save and publish your changes.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-adding-column-using-table-properties-pane.png" alt-text="Screenshot of Table Designer showing how to change column properties using the column properties pane.":::

#### Deleting a table

To delete a table, right click on the tables folder in the object explorer. When your table is located, right-click on the table and select **Script as Drop**. This then opens the query editor window containing the script that will drop the table when ran. Select **Run** to drop the table.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-deleting-table.png" alt-text="Screenshot of Table Designer showing how to delete a table.":::

#### Check Constraints

Check constraints are used to limit the value range that can be placed in a column. If you define a check constraint on a column, it will allow only certain values for this column. In this example, we will show how to add a check constraint to the **Population** column such that entries less than **0** are disallowed.

1. In the ***City*** table, select the **Check Constraints** tab and select **+New Check Constraint.**

2. This populates a table where you can define the **Name** and **Expression** for the constraint you want to add. Default names and expressions are provided. In the **Name** field, clear out this default name and type ***Population***. In the **Expression** field, clear out the default expression and type the expression [Population]>=(0). Remember to publish this check constraint to your database.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-show-how-to-add-check-constraint.png" alt-text="Screenshot of Table Designer showing how to add a check constraint.":::

You can add as few or as many constraints as needed depending on the nature of the table(s) in your database. To learn more about check constraints, please refer to the [check constraints documentation.](../relational-databases/tables/create-check-constraints.md)

> [!NOTE]
> Remember to refresh the object explorer at the table level to confirm the table deletion.

#### Foreign Keys

Foreign keys are used to establish and enforce a link between data in tables. To learn more about foreign keys, check out this documentation on [primary and foreign key constraints.](../relational-databases/tables/primary-and-foreign-key-constraints.md) In this example, we will create another table called ***PersonProfile*** and map this table to the ***City table*** using a foreign key.

1. Create a table named ***PersonProfile*** with three columns, **ID**(int, primary key), **Name**(nvarchar(50)), and **Age**(int). Set the **Name** and **Age** column such that they are non-nullable.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-create-person-profile-table.png" alt-text="Screenshot of Table Designer showing how to create table for establishing a foreign key.":::

2. Select the **Foreign Key** tab and select **+New Foreign Key**. Since we are mapping the ***PersonProfile*** table to the ***City*** table using their IDs, select the **Foreign table** drop down and select ***dbo.City***. Next, in the **Foreign Key Properties** window, under **+New Column Mapping**, select the **Foreign Column** drop-down, and select **ID**. This is the ID of the ***City*** table.
Don't forget to publish to your database.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-create-foreign-key.png" alt-text="Screenshot of Table Designer showing how to create foreign key settings.":::

## Next steps

- [Download Azure Data Studio](./download-azure-data-studio.md)
- [Build and Deploy changes to a database table using SQL Projects](./build-and-publish-changes-to-table-using-sql-projects.md)
- [Learn how to create a graph table using the Table Designer](./create-graph-tables-in-azure-data-studio.md)
- [Learn how to create a memory-optimized table using the Table Designer](./create-memory-optimized-tables-in-azure-data-studio.md)
- [Learn how to create system-versioned tables using the Table Designer](./create-temporal-tables-in-azure-data-studio.md)
