---
title: "Validation Warnings Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:65556"
  - "vdt.dlgbox.validationwarnings"
ms.assetid: fc76e234-ec9c-4a19-a65b-cb558ec8268e
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Validation Warnings Dialog Box (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
This dialog box appears if you attempt to save modifications with potentially damaging side effects, or if the database commit operation is likely to fail. This dialog box indicates what those side effects might be or why the commit operation might fail. It gives you the chance to continue with the modification or cancel the operation.  
  
> [!NOTE]  
> This dialog box appears when you attempt to transmit your modifications to the database or when you save a change script.  
  
The dialog box can appear for any of these reasons:  
  
-   You might not have database permissions to commit all the modifications.  
  
-   Your modifications would result in improperly formed derived columns, default constraints, or check constraints.  
  
-   A modification to a column's data type might cause data loss.  
  
-   A modification would result in an index greater than 900 bytes.  
  
-   A modification would change a table or column contributing to a schema-bound view or user-defined function.  
  
-   A modification would result in the re-creation of a table that has one or more encrypted triggers; the triggers will be dropped.  
  
-   Your modifications will yield noteworthy settings of ANSI_NULLS or ANSI_PADDING or both for the columns within one table.  
  
## Options  
**Yes**  
Proceed with the operation and generate the change script or transmit the modifications to the database. The commit operation can still fail if you do not have privileges to modify the database, if your modifications will result in an index greater than 900 bytes, or if your modifications will result in an improperly formed computed column, default constraint, or check constraint.  
  
**No**  
Cancel the save action.  
  
**Save Text File**  
Display the **Save As** dialog box, where you can specify a location for a text file containing a list of the warnings.  
  
## See Also  
[Design Tables &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-tables-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
