---
title: "SQL Modes (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: d840ee51-b863-4e77-84aa-37d3f094bfed
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# SQL Modes (MySQLToSQL)
The SSMA for MySQL can operate in different SQL Modes, and can apply these modes differently for different clients.  
  
Modes define the SQL syntax that MySQL should support, and the kind of data validation checks it should perform. This makes it easier to use MySQL in different environments and to use MySQL with SQL Server.  
  
## SQL Modes Grid:  
  
-   SQL Modes Grid at root level contains the following columns: **SQL Mode Name**, **Loaded SQL Modes**, and **Effective SQL Modes**.  
  
-   SQL Modes Grid at Databases category, Database, Table category, Statements Category, Views Category, table, view, functions, procedures, UDF, and event object level contains the following columns: **SQL Mode Name**, **Inherited SQL Modes**, and **Effective SQL Modes**.  
  
-   SQL Modes Grid at Stored Procedure, Stored Function, and Trigger level contains the following columns: **SQL Mode Name**,  **Original SQL Modes**, and **Effective SQL Modes**.  
  
> [!NOTE]  
> Group modes will be shown in bold, under the column 'SQL Mode Name'.  
  
## Loaded SQL Modes  
These are the SQL Modes, which are SET at the session or root level. The SQL Modes once loaded into the target database cannot be edited or modified.  
  
## Inherited SQL Modes  
These are the SQL Modes, which are inherited from the corresponding Parent node.  
  
Except for Functions category, Procedures category, Events category, and Triggers, these SQL Modes are present at all levels (database, Table category, Statements Category, Views category, table, view, functions, procedures, UDF, and event object).  
  
> [!NOTE]  
> By selecting the **Inherit From Parent** check box, Inherited SQL Modes can be inherited from the parent node. By default, this check box remains selected.  
  
## Original SQL Modes  
These are the SQL Modes present at only Function, Procedure, and Trigger levels.  
  
> [!NOTE]  
> By selecting the **Use original** check box, the SQL Modes that were originally used in the corresponding function or procedure or trigger can be used. By default, this check box remains selected.  
  
## Effective SQL Modes  
Effective SQL Modes can be defined at various levels in the following way:  
  
-   **At session level:**  
  
    1.  All the Loaded SQL Modes can be called, "Effective SQL Modes".  
  
    2.  At this level, the effective SQL modes can be directly and explicitly modified.  
  
    3.  The Effective SQL Mode that is set explicitly is not reflected as Loaded SQL Mode and is finally applied to the object.  
  
-   **At function or procedure or trigger level:**  
  
    1.  All the Original SQL Modes can be called, "Effective SQL Modes".  
  
    2.  At this level, the effective SQL mode can be explicitly modified only when the **Use original** checkbox is unchecked.  
  
    3.  The Effective SQL Mode that is set explicitly is not reflected as Original SQL Mode and is finally applied to the object.  
  
-   **At nodes other than function or procedure or trigger level:**  
  
    1.  All the Inherited SQL Modes can be called, "Effective SQL Modes".  
  
    2.  At this level, the effective SQL mode can be explicitly modified only when the **Inherit From Parent** checkbox is unchecked.  
  
    3.  The Effective SQL Mode that is set explicitly is not reflected as Inherited SQL Mode and is finally applied to the object.  
  
