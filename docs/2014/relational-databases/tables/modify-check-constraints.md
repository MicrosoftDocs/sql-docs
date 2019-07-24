---
title: "Modify Check Constraints | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "CHECK constraints, modifying"
  - "modifying constraints"
  - "constraints [SQL Server], check"
  - "constraints [SQL Server], modifying"
ms.assetid: f22daef8-e350-40ef-8ff0-b5f87d1d9e56
author: stevestein
ms.author: sstein
manager: craigg
---
# Modify Check Constraints
  You can modify a check constraint in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] when you want to change the constraint expression or the options that enable or disable the constraint for specific conditions.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To modify a check constraint using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To modify a check constraint  
  
1.  In the **Object Explorer**, right-click the table containing the check constraint and select **Design**.  
  
2.  On the **Table Designer** menu, click **Check Constraints...**.  
  
3.  In the **Check Constraints** dialog box, under **Selected Check Constraint**, select the constraint you wish to edit.  
  
4.  Complete an action from the following table:  
  
    |To|Follow these steps|  
    |--------|------------------------|  
    |Edit the constraint expression|Type the new expression in the **Expression** field.|  
    |Rename the constraint|Type a new name in the **Name** field.|  
    |Apply the constraint to existing data|Select the **Check Existing Data on Creation or Enabling** option.|  
    |Disable the constraint when new data is added to the table or when existing data is updated in the table.|Clear the **Enforce Constraint for INSERTs and UPDATEs** option.|  
    |Disable the constraint when a replication agent inserts or updates data in your table.|Clear the **Enforce For Replication** option.|  
  
    > [!NOTE]  
    >  Some databases have different functionality for check constraints.  
  
5.  Click **Close**.  
  
6.  On the **File** menu, click **Save**_table name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To modify a check constraint**  
  
 To modify a `CHECK` constraint using [!INCLUDE[tsql](../../includes/tsql-md.md)], you must first delete the existing `CHECK` constraint and then re-create it with the new definition. For more information, see [Delete Check Constraints](delete-check-constraints.md) and [Create Check Constraints](create-check-constraints.md).  
  
###  <a name="TsqlExample"></a>  
