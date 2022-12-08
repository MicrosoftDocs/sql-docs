---
title: "Attach Query Hints to a Plan Guide | Microsoft Docs"
description: Any combination of valid query hints can be used in a plan guide. Learn about attaching hints to a plan guide in SQL Server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
ms.assetid: 2131f796-6359-4f9e-9047-da0b3d4dedaf
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Attach Query Hints to a Plan Guide
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  Any combination of valid query hints can be used in a plan guide. When a plan guide matches a query, the OPTION clause specified in the hints clause of a plan guide is added to the query before it compiles and optimizes. If a query that is matched to a plan guide already has an OPTION clause, the query hints specified in the plan guide replace those in the query. However, for a plan guide to match a query that already has an OPTION clause, you must include the OPTION clause of the query when you specify the text of the query to match in the sp_create_plan_guide statement. If you want the hints specified in the plan guide to be added to the hints that already exist on the query, instead of replacing them, you must specify both the original hints and the additional hints in the OPTION clause of the plan guide.  
  
> [!CAUTION]  
>  Plan guides that misuse query hints can cause compilation, execution, or performance problems. Plan guides should be used only by experienced developers and database administrators.  
  
## Common Query Hints Used in Plan Guides  
 Queries that can benefit from plan guides are generally parameter-based, and may be performing poorly because they use cached query plans whose parameter values do not represent a worst-case or most representative scenario. The OPTIMIZE FOR and RECOMPILE query hints can be used to address this problem. OPTIMIZE FOR instructs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use a particular value for a parameter when the query is optimized. RECOMPILE instructs the server to discard a query plan after execution, forcing the query optimizer to recompile a new query plan the next time that the same query is executed. For an example, see [Plan Guides](../../relational-databases/performance/plan-guides.md).  
  
 In addition, you can specify the table hints INDEX, FORCESCAN, and FORCESEEK as query hints. When specified as query hints, these hints behave like an inline table or view hint. The INDEX hint forces the query optimizer to use only the specified indexes to access the data in the referenced table or view. The FORCESEEK hint forces the optimizer to use only an index seek operation to access the data in the referenced table or view. These hints provide additional plan guide functionality and allow you to have more influence over the optimization of queries that use the plan guide.  
  
  
