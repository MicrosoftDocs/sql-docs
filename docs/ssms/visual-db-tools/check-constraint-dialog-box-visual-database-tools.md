---
title: "Check Constraint Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "vdt.dlgbox.checkconstraint"
ms.assetid: ad0bbf7f-b0de-406a-bd0a-cb779816b101
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Check Constraint Dialog Box (Visual Database Tools)
This dialog box appears when you right-click a table definition grid in Table Designer and click **Check Constraints**. The dialog box contains a set of properties for non-unique constraints attached to the tables in your database. Properties applying to unique constraints appear in the **Indexes/Keys** dialog box.  
  
> [!NOTE]  
> If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](http://msdn.microsoft.com/en-us/f1745145-182d-4301-a334-18f799d361d1) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.  
  
## Options  
**Selected Check Constraints**  
Lists available check constraints. To view the properties of a constraint, select it in the list.  
  
**Add**  
Create a new constraint for the selected database table and provide a default name and other values for the constraint. The constraint will not become valid until an expression is entered for the constraint.  
  
**Delete**  
Remove the selected constraint from the table. To cancel the addition of a check constraint, use this button to remove the constraint.  
  
**General Category**  
Expand to show the **Expression** property field.  
  
**Expression**  
Displays the expression for the selected check constraint. For new constraints, you must enter the expression before exiting this box. You can also edit existing check constraints. For more information, see [Working with Constraints (Visual Database Tools)](http://msdn.microsoft.com/en-us/637098af-2567-48f8-90f4-b41df059833e).  
  
**Identity Category**  
Expand to show properties for **Name** and **Description**.  
  
**Name**  
Shows the name of the selected check constraint. To change the name of this constraint, type the text directly in the property field.  
  
**Description**  
Describing this check constraint. You can edit the description by typing into the property field or you can click the ellipsis button (**…**) that appears to the right of the property field and edit the description in the **Description Property** dialog box.  
  
**Table Designer Category**  
Expand to show properties for **Check Existing Data on Creation or Re-enabling**, **Enforce For Inserts And Updates**, and **Enforce Replication**.  
  
**Check Existing Data On Creation or Re-Enabling**  
Specify whether all pre-existing data (data existing in the table before the constraint is created) is verified against the constraint.  
  
**Enforce For Inserts And Updates**  
Specify whether the constraint is enforced when data is inserted into or updated in the table.  
  
**Enforce For Replication**  
Indicates whether to enforce the constraint when a replication agent performs an insert or update on this table.  
  
## See Also  
[Working with Constraints (Visual Database Tools)](http://msdn.microsoft.com/en-us/637098af-2567-48f8-90f4-b41df059833e)  
[Indexes - Keys Dialog Box &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/indexes-keys-dialog-box-visual-database-tools.md)  
  
