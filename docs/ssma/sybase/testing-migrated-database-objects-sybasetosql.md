---
title: "Testing Migrated Database Objects (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 4937f6b4-86bd-4070-88df-3d216306c33a
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Testing Migrated Database Objects (SybaseToSQL)
Microsoft SQL Server Migration Assistant for Sybase Tester (SSMA Tester) automatically tests the database object conversion and the data migration made by SSMA. After all SSMA migration steps are finished, use SSMA Tester to verify that converted objects work the same way and that all data was transferred properly.  
  
> [!NOTE]  
> Tester component is disabled in the case of Azure connectivity.  
  
You can test the following object types with SSMA Tester:  
  
-   Tables  
  
-   Stored procedures  
  
-   Views.  
  
-   Standalone statements.  
  
SSMA Tester executes objects selected for testing on Sybase and their counterparts in SQL Server. After that, it compares the results according to the following criteria:  
  
-   Are the changes in table data identical?  
  
-   Are the values of output parameters for procedures and functions identical?  
  
-   Do functions return the same results?  
  
-   Are the result sets identical?  
  
> [!NOTE]  
> Attention! Never use SSMA Tester on production systems. During Tester execution the source schema and data are modified. Meanwhile, the complete restoring of the original state may be impossible for some types of tested code.  
  
## Prerequisites  
If you want to use SSMA Tester, install SSMA Sybase Extension Pack with the **Install Tester Database** option turned on.  
  
In addition, verify the following:  
  
-   Sybase OLE DB provider is installed on the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs.  
  
-   Common Language Runtime (CLR) integration has been enabled on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine.  
  
Note that the current version of SSMA Tester does not support parallel execution by different users on the same source or target server.  
  
## Getting Started  
[Creating Test Cases &#40;SybaseToSQL&#41;](../../ssma/sybase/creating-test-cases-sybasetosql.md)  
  
## See Also  
[Installing SSMA Components on SQL Server &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-components-on-sql-server-sybasetosql.md)  
[Project Settings &#40;Conversion&#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-conversion-sybasetosql.md)  
  
