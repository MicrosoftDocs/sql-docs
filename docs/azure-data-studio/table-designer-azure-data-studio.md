---
title: Creating and managing tables in Azure Data Studio
description: How to use the Table Designer to manage tables and relationships in Azure Data Studio.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.custom: 
ms.date: 04/30/2022
---

# Table Designer in Azure Data Studio (Preview)

The table design (preview) in Azure Data Studio provides a modern, visual editor experience which enables data professionals to create and customize table structures, along with an editor for Transact-SQL. With Azure Data Studio, you can now create and manage your tables inside databases in your existing server connections.

## Why Table Designer?

The Table Designer in Azure Data Studio provides data professionals and software developers an efficient way to configure and manage entity and object relationships and properties directly in their databases using the graphical user interface (GUI).

## Overview of Table Designer

The table designer consists of a window split into three separate panes. The first pane, usually the widest pane by default, is the main view of the table design, consisting of tabs for the columns, foreign keys, constraint checks, and indexes. The second pane is used for defining the properties of your table. Lastly, the third pane is the read-only script editor for viewing the T-SQL script which is generated from the actions performed on the Table Designer GUI.

## Creating and editing tables


#### Creating a table

 To create a table, right-click the Tables node in your database and select "New Table". Please note that your server will need to be connected. This is indicated by the green dot at the bottom right corner of the server connection. If red (this means the connection is inactive or disconnected), simply left click once on the connection and this will spin up your connection.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-showing-connection-and-new-table-creation.png" alt-text="Table Designer pane highlighting connection and new table addition":::

#### Editing a table
You can edit the name of your table and add new columns to it. A default table is created with default name of “NewTable” which has one column by default. Let us update our table name to “Cities” and give it column names “CityName,” and “Population" as shown below.The column field is highlighted by default which means that whatever changes you make will be in reference to the column property. We will assign “CityName” as our Primary Key/Identifier for this table by checking the "Primary Key" checkbox. To read more on Primary Keys, [see this SQL Server documentation on Primary Keys](https://docs.microsoft.com/en-us/sql/relational-databases/tables/primary-and-foreign-key-constraints?view=sql-server-ver15).

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-changing-column-name-adding-new-column-with-primary-key.png" alt-text="Table Designer showing how to edit table name and add column showing primary key identifier":::

#### Changing column properties
You can change your column properties in the main pane as shown above or in the dedicated column properties pane. To view the dedicated column properties pane, click directly in the field of the column, e.g. "City Name" as shown below

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-adding-column-using-table-properties-pane.png" alt-text="Table Designer showing how to change column properties using the Column Properties Pane":::

 As you can see in the image above, the script contains the read-only Transact-SQL version of your table for reference.

#### Deleting columns
To delete a column, simply click on the trash icon as highlighted below

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-deleting-column.png" alt-text="Table Designer showing how to delete a column":::

Remember to save your work by using the “Ctrl + S” keys on your keyboard. Just like Visual Studio Code, an indicator that your work is NOT saved is the black circular dot that appears on the right hand side of the tab indicator that you are on. Once your work is saved, this dot disappears. Compare the images below.

First image below shows work that is not saved as indicated by the black dot. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-work-not-saved.png" alt-text="Table Designer showing work that is not saved":::

This image below shows work that has been saved. Notice black dot disappeared.


:::image type="content" source="media/table-designer-azure-data-studio/table-designer-work-saved.png" alt-text="Table Designer showing work saved":::

#### Deleting a table

To delete a table, expand on the object-dropdown, and then the "Tables" folder. When your table is located, right-click on the table and select "Script as Drop", and select "Run". This generates and runs the script that is responsible for deleting your table from the database. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-deleting-table.png" alt-text="Table Designer showing how to delete a table":::

Remember to hit "Refresh" to confirm that your table has been deleted. To refresh, right click on the "Tables" folder and select "Refresh". 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-refreshing-table.png" alt-text="Table Designer showing how to refresh table":::


## Table Types

The Table Design consists of different table types and properties that dictate relationships within entities as well as the overall structure and maintenance of the table. 

### Graph Tables

Graph tables are used to establish relationships between entities in your database using node and edge table relationships. In Azure Data Studio, you can easily create these relationships directly on the GUI without having to manually type out long T-SQL statements. To read more on graph tables, check out [this documentaion on SQL Graph Architecture](https://docs.microsoft.com/en-us/sql/relational-databases/graphs/sql-graph-architecture?view=sql-server-ver15).
Nodes are simply entities within databases. Edges are used to depict the relationships between nodes. For example, a Customer node table holds all the Customer nodes belonging to a graph. A "lives in" edge hols all the edges that connects the customer nodes according to where they live. A single dot represents a node table, while the two unshaded dots represent edge tables as shown below

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-node-vs-edge.png" alt-text="Table Designer showing node and edge graph table types":::

#### Creating a Graph Table


Creating a graph table is simplified in Azure Data Studio. This can be done directly in the Table Properties pane in your GUI. Please note that to create graph tables, a new table has to be created. Graph tables cannot be implemented for existing tables. 

In this example below, we are going to create two node tables; Person, City table and an edge table called "lives" with an edge constraint to establish the relationship between the two aforementioned node tables, i.e "Person **lives in** City". To follow along a more robust T-SQL version of a graph-table creation, feel free to follow along the example in [this documentation](https://docs.microsoft.com/en-us/sql/relational-databases/graphs/sql-graph-sample?view=sql-server-ver15#sample-script) 



##### Create the node tables

Please note that Graph Tables can only be implemented on new table types, and not already existing ones.
So, lets drop the "Cities" table we already created and re-create it, making sure to check the Graph Table option in the Table Properties pane and select "Node" from the drop down. Pay attention to the script change.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-Creating a Node Graph Table.png" alt-text="Table Designer Showing how to create a node graph table":::

Lets do the same for our "Persons" node table


:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-person-node-table.png" alt-text="Table Designer showing how to create a node graph table":::

##### Create the edge table

Now, as mentioned earlier, we will create our edge table, "lives". Be sure to check "Edge" from the Graph Table type drop-down. See below


:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-lives-edge-table.png" alt-text="Table Designer showing how to create an edge table":::



Simply click on the "Type" dropdown to indicate what type of graph table you want. Notice how the script changes in the script pane. 

We will now give this edge table a constraint to connect our two node tables. Click on "Edge Constraints" to open up the Edge Constraints pane. One constraint, named "EC1" is provided to us by default. We can choose to name this constraint however we please. For this tutorial, we will leave the name as is. See below: 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-edge-constraint-without-clause.png" alt-text="Table Designer showing how to create edge constraint":::



As seen in script pane in the image above, we need to create the clause that will establish the edge constraint between our "Persons" and "Cities" node tables. On the "Edge Constraint Properties" pane, select the "+" sign next to "New Clause" to add the clause. This will then present the "From" and "To" table drop-downs. From the "From Table" drop-down, select "dbo.Persons" and from the "To Table" drop-down, select "dbo.Cities". Be sure to "Ctrl+S" to save your work. See below:


:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-edge-constraint-with-clause.png" alt-text="Table Designer showing how to add clause to edge constraint":::

 You can add as little or as many constraints as needed depending on the nature of the relationships between the different tables in your database.  



### Memory-Optimized Tables

Creating Memory-Optimized Tables can be done directly on the GUI on Azure Data Studio. All it takes is checking a box at the time of table creation and the table in your database is memory-optimized! Just like Graph Tables, Memory-Optimized Tables can only be created on new tables, not already existing ones. Memory-Optimized tables must have non-clustered primary key.

#### Create a Memory-Optimized Table

To create a Memory-Optimized Table, we need to ensure that a filegroup has been created for our database. To read more on this, check out this documentation on [the memory optimized filegroup](https://docs.microsoft.com/en-us/sql/relational-databases/in-memory-oltp/the-memory-optimized-filegroup?view=sql-server-ver15) 
Next, create your table by right-clicking on the "Tables" folder and selecting "New Table". This will bring up the main pane and Table Properties pane where you can opt to have your table Memory Optimized. Be sure to assign a non-clustered primary key like shown below. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-memory-optimized-table-non-clustered-primary-key.png" alt-text="Table Designer showing how to create a memory-optimized table with non-clustered primary key":::

Next, head over to the Table Properties pane. Select the Memory Optimized checkbox. This will generate the durability drop-down where you can choose whether you want only the Schema to be stored in memory or both the Schema and Data. Choosing "Schema" only saves the schema of your database to memory. Notice the change in the script.

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-memory-optimized-schema-only.png" alt-text="Table Designer showing Memory-Optimized Table with Schema only configuration":::

Choosing "Schema and Data" saves both the schema and Data to memory in the event of a disaster. Notice the change in the script.


:::image type="content" source="media/table-designer-azure-data-studio/table-designer-memory-optimized-schema-only.png" alt-text="Table Designer showing Schema only Memory Optimized Table":::


### System-Versioned Tables

System versioning in Azure Data Studio is also very straightforward! If you are new to system versioning, check out [this documentation on Temporal Tables](https://docs.microsoft.com/en-us/sql/relational-databases/tables/creating-a-system-versioned-temporal-table?view=sql-server-ver15). System-versioned tables must have the period columns defined. 


#### Creating a System-Versioned Table

Create a table called "Department" as shown below with the column properties as shown below:

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-creating-a-system-versioned-table-column-properties.png" alt-text="Table Designer showing how to create a System-Versioned-Table":::

Next, head over to the column properties pane to define your period columns and select the "System Versioning" checkbox after which the constraints for the period start and end columns drop-down will be shown. For this table, we will set "ValidFrom" and "ValidTo" columns as our "Row Start" and "Row End" columns respectively. In the main pane, click inside of the "ValidFrom" and "ValidTo" fields respectively, then head over to the System Versioning drop-down and select "Row Start" and "Row End" respectively. 

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-system-versioning-row-start.png" alt-text="Table Designer showing how to define period columns":::


