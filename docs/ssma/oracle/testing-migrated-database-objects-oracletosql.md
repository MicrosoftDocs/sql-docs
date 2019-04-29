---
title: "Testing Migrated Database Objects (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: f03ef5e1-66e6-4c84-ada2-252dd5ada82f
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Testing Migrated Database Objects (OracleToSQL)
[!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant for Oracle Tester (SSMA Tester) automatically tests the database object conversion and the data migration made by SSMA. After all SSMA migration steps are finished, use SSMA Tester to verify that converted objects work the same way and that all data was transferred properly.  
  
You can test the following object types with SSMA Tester:  
  
-   Tables  
  
-   Stored procedures, including packaged procedures.  
  
-   User-defined functions, including packaged functions.  
  
-   Views.  
  
-   Standalone statements.  
  
SSMA Tester executes objects selected for testing on Oracle and their counterparts in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After that, it compares the results according to the following criteria:  
  
-   Are the changes in table data identical?  
  
-   Are the values of output parameters for procedures and functions identical?  
  
-   Do functions return the same results?  
  
-   Are the result sets identical?  
  
> [!NOTE]  
> Attention! Never use SSMA Tester on production systems. During Tester execution the source schema and data are modified. Meanwhile, the complete restoring of the original state may be impossible for some types of tested code.  
  
## Prerequisites  
If you want to use SSMA Tester, install SSMA Oracle Extension Pack with the **Install Tester Database** option turned on.  
  
In order to enable comparison of the resulting table data, set the **Generate ROWID column** option to **Yes** before the schema conversion starts. SSMA will add a ROWID column to all tables during execution of the **Convert Schema** command.  
  
In addition, verify the following:  
  
-   Oracle client tools are installed on the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs.  
  
-   Common Language Runtime (CLR) integration has been enabled on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine.  
  
Note that the current version of SSMA Tester does not support parallel execution by different users on the same source or target server.  
  
## Getting Started  
[Creating Test Cases &#40;OracleToSQL&#41;](../../ssma/oracle/creating-test-cases-oracletosql.md)  
  
## See Also  
[Installing SSMA Components on SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/installing-ssma-components-on-sql-server-oracletosql.md)  
[Project Settings &#40;Conversion&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-conversion-oracletosql.md)  
  
