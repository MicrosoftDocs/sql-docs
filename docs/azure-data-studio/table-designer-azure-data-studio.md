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
ms.date: 04/15/2022
---

# Using the Table Designer in Azure Data Studio (Preview)

The visual table design (preview) is available in Azure Data Studio and provides a modern editor experience which enables data professionals to create and customize table structures, along with the Transact-SQL Editor experience available in SSMS. With Azure Data Studio, you can now create and manage your tables inside databases in your server connections. The table designer consists of a window split into three separate panes. The first pane, usually the widest pane by default, is the main view of the table design, consisting of tabs for the columns, foreign keys, constraint checks, indexes, and a general tab for  

## Create a new table

To create a table, right-click the Tables node in your database and select New Table. Please note that your server will need to be connected. This is indicated by the green node at the bottom right corner of the server connection. If red (this means the connection is inactive or disconnected), simply left click once on the connection and this will spin up your connection.  

:::image type="content" source="media/table-designer-azure-data-studio/table-designer-general-pane.PNG" alt-text="general pane view":::

The table name is edited in a text box at the top of the pane and columns are edited in the matrix below. A default table is created with default name of “NewTable-1” which has one column. Let us update our table name to “Cities” and give it column names “CityName,” and “Population.” The column field is highlighted by default which means that whatever changes you make will be in reference to the column property. We will assign “CityName” as our Primary Key/Identifier for this table. To read more on Primary Keys, (link to primary key here).  

2.) Select the “+” sign next to “Add New” to add a new column  

 

                                                  

You can change your column properties in the dedicated column field (above) or directly on the table using the drop down or checkboxes (below) 

 

 Our table looks quite boring. Lets add some content to it. We will our table “Dessert” and give it some properties 

3.) Lets update our table. Right click the table under the Tables node of your database and select Design:  

- We will give the table a new name, “Dessert” and add some columns; DessertID (ID of Dessert), Name (Name of Dessert), NumIngredients(Number of Ingredients needed to make dessert) 

 

 

 

 

 

 

Remember to save your work by using the “Ctrl + S” keys on your keyboard. Just like Visual Studio Code, an indicator that your work is NOT saved is the black circular node that appears on the right hand side of the tab indicator that you are on. Once your work is saved, this node disappears 

 

Not Saved  

            

Saved 

                                           

Other Stuff to Write On: 

Table Properties (System Versioned, Memory Optimized, Graph Tables) 