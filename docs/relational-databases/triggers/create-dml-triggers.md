---
title: "Create DML Triggers"
description: "Create DML Triggers"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 03/27/2025
ms.service: sql
ms.topic: language-reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "encryption [SQL Server], DML triggers"
  - "deferred name resolution, DML triggers"
  - "WITH ENCRYPTION clause"
  - "IF UPDATE"
  - "SET statement, DML triggers"
  - "DML triggers, programming"
  - "testing column changes"
  - "results [SQL Server], DML triggers"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Create DML triggers

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This article describes how to create a [!INCLUDE [tsql](../../includes/tsql-md.md)] Data Manipulation Language (DML) trigger with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or the [!INCLUDE [tsql](../../includes/tsql-md.md)] `CREATE TRIGGER` statement.

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## Limitations

For a list of limitations and restrictions related to creating DML triggers, see [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md).

## Permissions

Requires `ALTER` permission on the table or view on which the trigger is being created.

<a id="Procedures"></a>

## How to create a DML trigger

You can use one of the following methods:

- [SQL Server Management Studio](#use-sql-server-management-studio)
- [Transact-SQL](#use-transact-sql)

### Use SQL Server Management Studio

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then expand that instance.

1. Expand **Databases**, expand the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, expand **Tables**, and then expand the table `Purchasing.PurchaseOrderHeader`.

1. Right-click **Triggers**, and then select **New Trigger**.

1. On the **Query** menu, select **Specify Values for Template Parameters**. Alternatively, you can press (Ctrl-Shift-M) to open the **Specify Values for Template Parameters** dialog box.

1. In the **Specify Values for Template Parameters** dialog box, enter the following values for the parameters shown.

    | Parameter | Value |
    | --- | --- |
    | **Author** | *Your name* |
    | **Create Date** | *Today's date* |
    | **Description** | Checks the vendor credit rating before allowing a new purchase order with the vendor to be inserted. |
    | **Schema_Name** | `Purchasing` |
    | **Trigger_Name** | `NewPODetail2` |
    | **Table_Name** | `PurchaseOrderDetail` |
    | **Data_Modification_Statement** | Remove `UPDATE` and `DELETE` from the list. |

1. Select **OK**.

1. In the **Query Editor**, replace the comment `-- Insert statements for trigger here` with the following statement:

   ```sql
   IF @@ROWCOUNT = 1
   BEGIN
       UPDATE Purchasing.PurchaseOrderHeader
       SET SubTotal = SubTotal + LineTotal
       FROM inserted
       WHERE PurchaseOrderHeader.PurchaseOrderID = inserted.PurchaseOrderID;
   END
   ELSE
   BEGIN
       UPDATE Purchasing.PurchaseOrderHeader
       SET SubTotal = SubTotal + (SELECT SUM(LineTotal)
           FROM inserted
           WHERE PurchaseOrderHeader.PurchaseOrderID = inserted.PurchaseOrderID)
       WHERE PurchaseOrderHeader.PurchaseOrderID IN (SELECT PurchaseOrderID
       FROM inserted);
   END;
   ```

1. To verify the syntax is valid, on the **Query** menu, select **Parse**. If an error message is returned, compare the statement with the previous code block, correct as needed, and repeat this step.

1. To create the DML trigger, from the **Query** menu, select **Execute**. The DML trigger is created as an object in the database.

1. To see the DML trigger listed in Object Explorer, right-click **Triggers** and select **Refresh**.

### Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then expand that instance.

1. From the **File** menu, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example creates the same stored DML trigger as before. The trigger is valid for multirow and single row inserts, and optimal for single row inserts.

   ```sql
   USE AdventureWorks2022;
   GO

   CREATE TRIGGER NewPODetail3
   ON Purchasing.PurchaseOrderDetail
   FOR INSERT AS
   IF @@ROWCOUNT = 1
   BEGIN
       UPDATE Purchasing.PurchaseOrderHeader
       SET SubTotal = SubTotal + LineTotal
       FROM inserted
       WHERE PurchaseOrderHeader.PurchaseOrderID = inserted.PurchaseOrderID;
   END
   ELSE
   BEGIN
       UPDATE Purchasing.PurchaseOrderHeader
       SET SubTotal = SubTotal + (SELECT SUM(LineTotal)
           FROM inserted
           WHERE PurchaseOrderHeader.PurchaseOrderID = inserted.PurchaseOrderID)
       WHERE PurchaseOrderHeader.PurchaseOrderID IN (SELECT PurchaseOrderID
       FROM inserted);
   END;
   ```

