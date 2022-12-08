---
description: "Tables and Columns Dialog Box (Visual Database Tools)"
title: Tables and Columns Dialog Box
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.tablesandcolumns"
ms.assetid: 8cf27be1-e66d-4735-a428-9ab4b33af4f5
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Tables and Columns Dialog Box (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
Use this dialog box to map a primary key in one table to a foreign key in another. To access this dialog box, from the **Table Designer** menu, click **Relationships**. In the **Foreign Key Relationships** dialog box, click the **Tables and Columns Specification** field, and then click the ellipses **(...)** to the right of the property.  
  
> [!NOTE]  
> If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.  
  
## Options  
**Relationship name**  
Shows the name of the relationship.  
  
**Primary Key Table**  
Lists the tables in the database. Choose the table that will be on the primary-key side of the relationship.  
  
**Foreign Key Table**  
Shows the table on the foreign key side of the relationship. This is the table currently selected in Table Designer.  
  
**Grid beneath Primary Key Table**  
List the columns of the table selected in the **Primary Key Table** list. Enter the columns contributing to that table's primary key.  
  
**Grid beneath Foreign Key Table**  
List the columns of the table selected in the **Foreign Key Table** list. Enter the foreign-key column of the foreign-key table that corresponds to the primary key column.  
  
> [!NOTE]  
> The columns you choose for the foreign key must have the same data type of the primary columns they correspond to. There must be an equal number of columns in each of the keys. For example, if the primary key of the table on the primary side of the relationship is made up of two columns, you will need to match each of those columns with a column in the table for the foreign key side of the relationship.  
  
## See Also  
[How to: Create Relationships Between Tables](../../relational-databases/tables/create-foreign-key-relationships.md)  
