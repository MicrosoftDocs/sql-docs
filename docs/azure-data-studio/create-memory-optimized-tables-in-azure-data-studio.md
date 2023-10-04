---
title: Creating Memory-Optimized tables in Azure Data Studio
description: How to use the Table Designer to create memory-optimized tables
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.date: 09/24/2022
ms.service: azure-data-studio
ms.topic: tutorial
---

# Creating a Memory-Optimized Table

[!INCLUDE [sql-asdb-asdbmi](../includes/applies-to-version/sql-asdb-asdbmi.md)]

 memory-Optimized tables are a feature of SQL Server where the entire table resides in memory. A second copy of the table data is maintained on disk. Data in memory-optimized tables is only read from disk during database recovery. For example, after a server restart. memory-Optimized tables can be created in the table designer in Azure Data Studio.

> [!NOTE]
> memory-optimized tables must belong to a filegroup. To read more on this, check out this documentation on [the memory optimized filegroup](../relational-databases/in-memory-oltp/the-memory-optimized-filegroup.md).

 memory-Optimized tables must have a non-clustered primary key. For an introduction to memory-optimized Tables, check out the [Introduction to Memory-Optimized Tables](../relational-databases/in-memory-oltp/introduction-to-memory-optimized-tables.md) article. Additionally, all memory-optimized tables must have at least one index.

## Create a memory-optimized table

1. To create a memory-optimized Table, we need to ensure that a filegroup has been created for our database. In the object explorer, open a new query editor window from the server level as we will be creating an entirely new database in which our memory-optimized table will reside. In the query editor, copy, paste and execute the following code:

    ```sql  
    CREATE DATABASE imoltp   
    GO  
      
    --------------------------------------  
    -- create database with a memory-optimized
    -- filegroup and a container.
    
    ALTER DATABASE imoltp ADD FILEGROUP imoltp_mod
        CONTAINS MEMORY_OPTIMIZED_DATA;
    
    ALTER DATABASE imoltp ADD FILE (
        name='imoltp_mod1', filename='c:\data\imoltp_mod1')
        TO FILEGROUP imoltp_mod;
    
    ALTER DATABASE imoltp
        SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = ON;
    GO  
    --
    ```

    The code above creates a new database, adds a filegroup to the database, adds a file to the filegroup, and finally sets the isolation level for any memory-optimized table added to this database to SNAPSHOT.

2. Next, create your table by opening the **imoltp** database from the object explorer, right-clicking the **Tables** folder, and selecting **New Table**. This will open up the table designer view. Assign the primary key for this table (ensure that this primary key is non-clustered by unchecking the **Clustered** checkbox in the **Primary Key** settings)

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-create-memory-optimized-table-non-clustered-primary-key.png" alt-text="Screenshot of Table Designer showing how to create a memory-optimized table with non-clustered primary key.":::

3. In the Table Properties pane. Select the **Memory-Optimized** checkbox. This will enable the durability drop-down where you can choose whether you want only the Schema to be stored in memory or both the Schema and Data. Choosing "Schema" only saves the schema of your database to memory. As you can see below, the script is updated to reflect the changes.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-memory-optimized-schema-only.png" alt-text="Screenshot of Table Designer showing Memory-Optimized Table with Schema only configuration.":::

    Choosing **Schema** saves only the schema to memory. Choosing **Schema and Data** saves both the schema and data to memory. Notice the change in the script.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-memory-optimized-schema-only.png" alt-text="Screenshot of Table Designer showing schema only memory-optimized table.":::

> [!NOTE]
> The table designer also supports hash indexes, columnstore indexes, and these can be configured while creating the memory-optimized table.

## Next steps

- [Download Azure Data Studio](./download-azure-data-studio.md)