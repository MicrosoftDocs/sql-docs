---
title: "Database Changes Detected Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.schema.databasechangesdetected"
  - "vdtsql.chm:65543"
  - "vdtsql.chm:65554"
ms.assetid: 91f13086-371f-46a2-9f46-804c1415f3ed
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Database Changes Detected Dialog Box (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
This dialog appears if you attempt to save a database diagram or selected tables but some of the database objects that will be affected by the save action are out of date with the database. Accepting the changes shown in this dialog box updates the database to match your diagram and overwrites other users' changes.  
  
> [!NOTE]  
> Although you cannot undo changes made to a table or database diagram, the changes are not saved to the database until you save the table or diagram. You can discard any unsaved changes by choosing **No** and closing all open diagrams without saving them.  
  
## Options  
**Warn about difference detection**  
Specify whether this dialog box appears the next time you attempt to save a database diagram or selected tables. Checked means that the dialog box continues to appear each time you save a diagram or table that is out of date with the database. Unchecked means that the dialog box does not appear. By default, this box is checked. If you uncheck this option, you can recheck it in the **Options** dialog box.  
  
**Yes**  
Update the database with all the changes shown in the list.  
  
**No**  
Cancel the save action.  
  
> [!NOTE]  
> To refresh your diagram to match the database, close it without saving changes, right-click the diagram in Server Explorer and click Refresh, and then reopen the diagram.  
  
**Save Text File**  
Display the **Save As** dialog box, letting you specify a location for a text file containing a list of the database changes.  
  
## See Also  
[Reconcile a Database Diagram with a Modified Database &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/reconcile-a-database-diagram-with-a-modified-database-visual-database-tools.md)  
[Multiuser Environments &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/multiuser-environments-visual-database-tools.md)  
  
