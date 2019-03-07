---
title: "Modify Edge Constraints | Microsoft Docs"
ms.custom: ""
ms.date: "09/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "alter edge constraints"
  - "edge constraints [SQL Server]"
  - "CONNECTION constraints"
  - "edge constraints [Azure SQL Database]"
  - "graph edge constraints"
  - "SQL Graph" 
ms.assetid: 
author: shkale-msft
ms.author: shkale
manager: craigg
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify Edge Constraints
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx.md](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

  You can modify an edge constraint in [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)]. You can modify the edge constraint of an edge table by changing the edge constraint clause order or adding a new clause.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To modify an edge constraint, using:**  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL
 To modify an edge constraint using Transact-SQL, you must first delete the existing edge constraint and then re-create it with the new definition. For more information, see [Delete Edge Constraint](../../relational-databases/tables/delete-edge-constraint.md) and [Create Edge Constraints](../../relational-databases/tables/create-edge-constraints.md).    
 
