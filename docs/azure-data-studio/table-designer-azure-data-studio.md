---
title: Creating and managing tables in Azure Data Studio
description: How to use the Table Designer to manage tables and relationships in Azure Data Studio.
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.date: 05/05/2022
ms.prod: azure-data-studio
ms.topic: conceptual
---

# Table Designer in Azure Data Studio (Preview)

The Table Designer (preview) in Azure Data Studio provides a visual editor experience alongside the Transact-SQL Editor for creating and editing table structure, including table-specific programming objects, for SQL Server databases.

[!INCLUDE [azure-data-studio-preview](../includes/azure-data-studio-preview.md)]

## Why Table Designer?

The Table Designer in Azure Data Studio provides users an efficient way to configure and manage tables, using primary and foreign keys, indices, constraints, triggers, etc. directly on the graphical user interface (GUI).

## Overview of Table Designer

The Table Designer consists of a window split into three separate panes. The first pane is the Overview/General pane of the table design. This consists of tabs for the columns, primary and foreign keys, constraint checks, indexes, and a general tab. The second pane is used for defining the properties of your table. Lastly, the third pane is the Script pane for the read-only T-SQL script that represents actions performed on the GUI as well as query results. The size of these panes can be adjusted by clicking on the edge lines and mouse dragging with the mouse to resize. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-panes.png" alt-text="Screenshot of Table Designer showing the three different panes.":::

### Creating and editing tables

#### Creating a table

> [!NOTE]
> We will be using the "AdventureWorks2019" sample database throughout this tutorial. If you haven't already, please refer to [AdventureWorks sample databases](../samples/adventureworks-install-configure.md) to download this sample database.

Before creating a table, you need to ensure that you have an active connection where your database is located. For a tutorial on connecting to the SQL Server, check out the [Use Azure Data Studio to connect and query SQL Server tutorial](quickstart-sql-server.md) article. To locate the Tables folder, click the drop-down menu of the databases folder in your active local connection and locate the AdventureWorks2019 database. Then, click the drop-down of this database to view the Tables folder.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-traversing-through-object-explorer-to-create-new-table.png" alt-text="Screenshot of Table Designer showing how to traverse object explorer to create a new table.":::

> [!Note]
> That your server needs to be connected. This is indicated by the green dot at the bottom right of the icon in the server connection. If red (this means the connection is inactive or disconnected), left select once on the connection and this will spin up your connection.

To create a table, right-click the Tables folder in the connection pane and select "New Table". This opens up the Table Designer view seen below.

A default table is created with the default name of "NewTable", which has one column by default. You can edit the name of your table and add new columns to it. Let us update our table name to “Cities”. We do this by selecting inside this name field, typing the new table name (Cities) and pressing "Enter" to save the change. Lets add a new column by selecting the "New Column" button above the columns grid. In the Columns grid, select the name value "column 1", replace with "CityName", and select the Primary Key column checkbox to make this the primary key for the table. To read more on Primary Keys, [see this SQL Server documentation on Primary Keys](../relational-databases/tables/primary-and-foreign-key-constraints.md).

Select the "column 2", replace with "Population"

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-changing-column-name-adding-new-column-with-primary-key.png" alt-text="Screenshot of Table Designer showing how to edit table name and add column showing primary key identifier.":::

Now that we have created a new table, we need to save this change to our AdventureWorks2019 database. To do this, select the "Publish" icon as seen below. Publishing can also be done by using the "Ctrl/Cmd + S" command. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-showing-publish-icon.png" alt-text="Screenshot of Table Designer highlighting publish icon.":::

Once this icon is selected, a dialog box appears for the user to either publish the changes directly to the AdventureWorks2019 database via the graphical user interface or generate the SQL command which can then be manually executed.

:::image type="content" source="media/table-designer-azure-data-studio/save-dialog-for-table-designer.png" alt-text="Screenshot of Table Designer showing save dialog box.":::

If you choose to manually run the SQL command to update this database, you need to select "Run" to perform this action. See below for this illustration.

:::image type="content" source="media/table-designer-azure-data-studio/change-publishing-via-the-sql-command-script-table-designer.png" alt-text="Screenshot of Table Designer showing how to publish changes to the database using the S Q L command option.":::

You see that this script was ran successfully as described in the "Messages" pane.

> [!Note]
> Please note that if you decide to use the "Generate Script" option, you need to enable the SQL command mode in order to prevent errors in the SQL command script.Do this by clicking the "Enable SQLCMD" icon. In the image above, the "Enable SQLCMD" option is already selected, which is why you see "Disable SQLCMD". 

You can save this SQLQuery as a file on your local desktop by using the "Ctrl/Cmd + S" command. Any unsaved work is indicated by the dark shaded dot in the tab of the workspace that you have open. First image below shows work that isn't saved as indicated by the black dot. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-work-not-saved.png" alt-text="Screenshot of Table Designer showing work that is not saved.":::

This image below shows work that has been saved. Notice black dot disappeared.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-work-saved.png" alt-text="Screenshot of Table Designer showing work saved.":::

To confirm that your table has been created, traverse your AdventureWorks2019 database in the object explorer under "Tables" to locate your "Cities" table. Newly created tables are seen by order of how recently they were created, so you should see yours near top of the list. If you don't see your table, right click on the "Tables" folder and select "Refresh". 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-refreshing-table.png" alt-text="Screenshot of Table Designer showing how to refresh table.":::


#### Editing an existing table

To edit an existing table in the Table Designer, right-click on the table in the Connections pane and select "Design" from the menu. This opens up the Table Designer view, which then allows you to make edits where necessary. See below.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-editing-exisitng-tables.png" alt-text="Screenshot of Table Designer showing how to edit an existing table.":::

#### Changing column properties

You can change column properties in the main pane as shown above or in the properties pane. To view the properties pane for a column, select the column, for example "City Name" as shown below, the properties pane will then show the properties specific to columns.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-adding-column-using-table-properties-pane.png" alt-text="Screenshot of Table Designer showing how to change column properties using the column properties pane.":::

The script pane will update to display the read-only Transact-SQL version of your table for reference (as shown in the image above). After making changes, select the "Publishing Changes..." button or "CTRL/CMD + S"

#### Deleting columns

To delete a column, simply select the trash icon at the end of the column as highlighted below.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-deleting-column.png" alt-text="Screenshot of Table Designer showing how to delete a column.":::

#### Deleting a table

To delete a table, expand on the object-dropdown, and then the "Tables" folder. When your table is located, right-click on the table and select "Script as Drop", and select "Run". This generates and runs the script that is responsible for deleting your table from the database. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-deleting-table.png" alt-text="Screenshot of Table Designer showing how to delete a table.":::

> [!Note]
> If your object explorer is open while dropping a table, you may still see this table name showing in the list of tables. Refreshing your table will take care of this.

## Table Types

The Table Designer supports different table types and properties that dictate relationships within entities and the overall structure of the table. 

### Graph Tables

Graph tables are used to establish relationships between entities in your database using node and edge table relationships. In Azure Data Studio, you can easily create these relationships directly on the GUI without having to manually type out long T-SQL statements. To read more on graph tables, check out [this documentation on SQL Graph Architecture](../relational-databases/graphs/sql-graph-architecture.md). The table type can be seen in the Connections Pane by the icon shown to the left of the table name. A single dot represents a node table, while the two unshaded dots represent edge tables as shown below

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-node-vs-edge.png" alt-text="Screenshot of Table Designer showing node and edge graph table types.":::

#### Creating a Graph Table

Creating a graph table is simplified in Azure Data Studio. This can be done directly in the Table Properties pane.

> [!Note]
> That to create graph tables, a new table has to be created. Graph tables can't be implemented for existing tables. 

In this example below, we will use the GUI to create two node tables; Person, City and an edge table called "lives" with an edge constraint to establish the relationship between the two aforementioned node tables, that is "Person **lives in** City". For a T-SQL script version of this example, see [Create a graph database and run some pattern matching queries using T-SQL](../relational-databases/graphs/sql-graph-sample.md).

##### Create the node tables

> [!Note]
> That the Graph Tables can only be implemented on new table types, and not already existing ones.

So, lets drop the "Cities" table we already created and re-create it. Before saving this new table, in the Table Properties pane, change the Graph Table option to "Node" from the drop-down. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-a-node-graph-table.png" alt-text="Screenshot of Table Designer showing how to create a Cities node graph table.":::

Notice that the script is updated to include the "as node" syntax.

Once this configuration is set, publish this update to the database. Once published, you will see that a new column for the node ID is created and this ID is referenced in the script pane as shown below. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-publishing-changes-to-a-node-graph-table.png" alt-text="Screenshot of Table Designer showing the node ID in the table view and script pane.":::


Create another node table called "Persons", repeating the steps above.

##### Create the edge table

Now, as mentioned earlier, we'll create our edge table, "lives". To do this, right-click on the Tables folder to create a new table. Change the name of this table from its default to "lives". To indicate that this is an edge table, click on the Graph Table Type drop-down in the Table Properties pane and select "Edge". See below.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-lives-edge-table.png" alt-text="Screenshot of Table Designer showing how to create an edge table.":::


To create the relationship between the node and edge, select "Edge Constraints". Select the plus sign next to "New Edge Constraint" to create a new constraint. A default name of "EC-1" is provided as seen below. Feel free to change the name as you please. For the purpose of this tutorial, we will leave this as is. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-edge-constraint-without-clause.png" alt-text="Screenshot of Table Designer showing how to create edge constraint.":::

As seen in script pane in the image above, a warning appears indicating that a clause has not been specified. We need to create the clause that will establish the edge constraint between our "Persons" and "Cities" node tables. On the "Edge Constraint Properties" pane, under "Clauses", select "+New Clause". The "From" and "To" values will now appear in the clauses section. Hover over the "From Table" to view the drop-down, select "dbo.Persons" and from the "To Table" drop-down, select "dbo.Cities". Be sure to publish changes to save your work. See below for the overview of what the Table Designer view should look like after performing these steps described above. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-edge-constraint-with-clause.png" alt-text="Screenshot of Table Designer showing how to add clause to edge constraint.":::

You can add as little or as many constraints as needed depending on the nature of the relationships between the different tables in your database.  

### Memory-Optimized Tables

Creating Memory-Optimized Tables can be done directly in Azure Data Studio. All it takes is checking a box at the time of table creation and the table in your database is memory-optimized!

> [!Note]
> That to create Memory-Optimized tables, a new table has to be created. Memory-Optimized tables can't be implemented on existing tables.

 Memory-Optimized tables must have non-clustered primary key. For an introduction to Memory-Optimized Tables, check out the [Introduction to Memory-Optimized Tables](../relational-databases/in-memory-oltp/introduction-to-memory-optimized-tables.md) article.

#### Create a Memory-Optimized Table

To create a Memory-Optimized Table, we need to ensure that a filegroup has been created for our database. To read more on this, check out this documentation on [the memory optimized filegroup](../relational-databases/in-memory-oltp/the-memory-optimized-filegroup.md).

Next, create your table by right-clicking on the "Tables" folder and selecting "New Table". This will bring up the main pane and Table Properties pane where you can opt to have your table Memory Optimized. Be sure to assign a non-clustered primary key like shown below.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-memory-optimized-table-non-clustered-primary-key.png" alt-text="Screenshot of Table Designer showing how to create a memory-optimized table with non-clustered primary key.":::

Next, head over to the Table Properties pane. Select the Memory Optimized checkbox. This will generate the durability drop-down where you can choose whether you want only the Schema to be stored in memory or both the Schema and Data. Choosing "Schema" only saves the schema of your database to memory. As you can see below, the script is updated to reflect the changes. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-memory-optimized-schema-only.png" alt-text="Screenshot of Table Designer showing Memory-Optimized Table with Schema only configuration.":::

Choosing "Schema and Data" saves both the schema and Data to memory if there's a disaster. Notice the change in the script.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-memory-optimized-schema-only.png" alt-text="Screenshot of Table Designer showing Schema only Memory Optimized Table.":::

### System-Versioned Tables

System-versioned Tables, also known as Temporal Tables can also be configured directly on Azure Data Studio. If you're new to system versioning, check out [Temporal Tables on SQL Server ](../relational-databases/tables/creating-a-system-versioned-temporal-table.md). System-versioning tables must have the period columns defined.

#### Creating a System-Versioned Table

Create a table called "Department" with the column properties as seen below. To do this, right-click on the Tables folder in the Connections pane and select "New Table". Next, in the Table Properties pane, select the "System Versioning Enabled" check box. You can decide to rename this history table or leave the default name as is.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-a-system-versioned-table-column-properties.png" alt-text="Screenshot of Table Designer showing how to create a System-Versioned-Table.":::

Next, we need to define the period columns and indicate the table is system-versioned. To define the period columns, select "ValidFrom" and then in the column properties under "System Versioning", set the "Generated Always As" to "Row Start", repeat for "ValidTo" but set to "Row End". With the period columns defined, select the table to show the Table Properties and check the "System-Versioning Enabled" checkbox. The script pane will now be updated to show the T-SQL version of this system-verisoned table. 


:::image type="content" source="media/table-designer-azure-data-studio/table-designer-system-versioning-row-start.png" alt-text="Screenshot of Table Designer showing how to define period columns.":::

## Next steps

- [Download Azure Data Studio](./download-azure-data-studio.md)
