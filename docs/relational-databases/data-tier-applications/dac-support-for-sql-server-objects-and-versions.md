---
title: "DAC Support For SQL Server Objects and Versions | Microsoft Docs"
ms.custom: ""
ms.date: "09/13/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "data-tier application [SQL Server], supported objects"
  - "objects [SQL Server], data-tier applications"
ms.assetid: b1b78ded-16c0-4d69-8657-ec57925e68fd
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# DAC Support For SQL Server Objects and Versions
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  A data-tier application (DAC) supports the most commonly used [!INCLUDE[ssDE](../../includes/ssde-md.md)] objects.  
  
 **In This Topic**  


> [!IMPORTANT]
> This article is valid for SQL Server 2012, but not for SQL Server 2014 or later.
> For DAC articles about SQL 2012 and earlier, see the following links:
> 
> - https://docs.microsoft.com/previous-versions/sql/sql-server-2008-r2/ee240739(v=sql.105)
> - https://docs.microsoft.com/previous-versions/sql/sql-server-2012/hh753459(v=sql.110)


-   [Supported SQL Server Objects](#SupportedObjects)  
  
-   [Data-tier Application Support by the Versions of SQL Server](#SupportByVersion)  
  
-   [Data Deployment Limitations](#DeploymentLimitations)  
  
-   [Additional Considerations for Deployment Actions](#Considerations)  
  
##  <a name="SupportedObjects"></a> Supported SQL Server Objects  
 Only supported objects can be specified in a data-tier application as it is being authored or edited. You cannot extract, register, or import a DAC from an existing database that contains objects that are not supported in a DAC. [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] supports the following objects in a DAC.  
  
|||  
|-|-|  
|DATABASE ROLE|FUNCTION: Inline Table-valued|  
|FUNCTION: Multistatement Table-valued|FUNCTION: Scalar|  
|INDEX: Clustered|INDEX: Non-clustered|  
|INDEX: Spacial|INDEX: Unique|  
|LOGIN|Permissions|  
|Role Memberships|SCHEMA|  
|Statistics|STORED PROCEDURE: Transact-SQL|  
|Synonyms|TABLE: Check Constraint|  
|TABLE: Collation|TABLE: Column, including computed columns|  
|TABLE: Constraint, Default|TABLE: Constraint, Foreign Key|  
|TABLE: Constraint, Index|TABLE: Constraint, Primary Key|  
|TABLE: Constraint, Unique|TRIGGER: DML|  
|TYPE: HIERARCHYID, GEOMETRY, GEOGRAPHY|TYPE: User-defined Data Type|  
|TYPE: User-defined Table Type|USER|  
|VIEW||  
  
##  <a name="SupportByVersion"></a> Data-tier Application Support by the Versions of SQL Server  
 The versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] have different levels of support for DAC operations. All of the DAC operations supported by a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are supported by all editions of that version.  
  
 Instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] support the following DAC operations:  
  
-   Export and extract are supported on all supported versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   All operations are supported on [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] and all versions of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], and [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].  
  
-   All operations are supported on [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] Service Pack 2 (SP2) or later, and [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP4 or later.  
  
 The DAC Framework comprises the client-side tools for building and processing DAC packages and export files. The following products include the DAC Framework  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] includes DAC Framework 3.0, which supports all DAC operations.  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP1 and Visual Studio 2010 SP1 included DAC Framework 1.1, which supports all DAC operations except export and import.  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] and Visual Studio 2010 included DAC Framework 1.0, which supports all DAC operations except export, import, and in-place upgrade.  
  
-   The client tools from earlier versions of SQL Server or Visual Studio do not support DAC operations.  
  
 A DAC package or export file built with one version of the DAC Framework cannot be processed by an earlier version of the DAC Framework. For example, a DAC package extracted using the [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] client tools cannot be deployed using the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] client tools.  
  
 A DAC package or export file built with one version of the DAC Framework can be processed by any later version of the DAC Framework. For example, a DAC package extracted using the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] client tools can be deployed using either the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] SP1 or higher client tools.  
  
##  <a name="DeploymentLimitations"></a> Data Deployment Limitations  
 Note these fidelity limitations in the DAC Framework data deployment engine in SQL Server 2012 SP1. The limitations apply to the following DAC Framework actions: deploy or publish a .dacpac file, and import a .bacpac file.  
  
1.  Loss of metadata for certain conditions and base types within sql_variant columns. In the affected cases, you will see a warning with the following message:  **Certain properties on certain data types used within a sql_variant column are not preserved when deployed by the DAC Framework.**  
  
    -   MONEY, SMALLMONEY, NUMERIC, DECIMAL base types:  Precision is not preserved.  
  
        -   DECIMAL/NUMERIC base types with precision 38:  the "TotalBytes" sql_variant metadata is always set to 21.  
  
    -   All text base types:  The database default collation is applied for all text.  
  
    -   BINARY base types:  Max length property is not preserved.  
  
    -   TIME, DATETIMEOFFSET base types:  Precision is always set to 7.  
  
2.  Loss of data within sql_variant columns. In the affected case, you will see a warning with the following message:  **There will be data loss when a value in a sql_variant DATETIME2 column with scale greater than 3 is deployed by the DAC Framework. The DATETIME2 value is limited to a scale equal to 3 during deployment.**  
  
    -   DATETIME2 base type with scale greater than 3:  scale is limited to equal 3.  
  
3.  Deployment operation fails for the following conditions within sql_variant columns. In the affected cases, you will see a dialog with the following message:  **Operation failed due to data limitations in the DAC Framework.**  
  
    -   DATETIME2, SMALLDATETIME and DATE base types:  If the value is outside of DATETIME range - for example, the year is less than 1753.  
  
    -   DECIMAL, NUMERIC base type:  when precision of the value is greater than 28.  
  
##  <a name="Considerations"></a> Additional Considerations for Deployment Actions  
 Note the following considerations for DAC Framework data deployment actions:  
  
-   **Extract/Export** - On actions that use the DAC Framework to create a package from a database - for example, extract a .dacpac file, export a .bacpac file - these limitations do not apply. The data in the package is a full-fidelity representation of the data in the source database. If any of these conditions are present in the package, the extract/export log will contain a summary of the issues via the messages noted above. This is to warn the user of potential data deployment issues with the package they created. The user will also see the following summary message in the log:  **These limitations do not affect the fidelity of the data types and values stored in the DAC package created by the DAC Framework; they only apply to the data types and values resulting from deploying a DAC package to a database. For more information about the data that is affected and how to work around this limitation, see**[this topic](https://go.microsoft.com/fwlink/?LinkId=267086).  
  
-   **Deploy/Publish/Import** - On actions that use the DAC Framework to deploy a package to a database, like to deploy or publish a .dacpac file, and import a .bacpac file, these limitations do apply. The data that results in the target database may not contain a full-fidelity representation of the data in the package. The Deploy/Import log will contain a message, noted above, for every instance the issue is encountered. The operation will be blocked by errors - see category 3 above - but will proceed with the other warnings.  
  
     For more information about the data that is affected in this scenario and how to work around this limitation for deploy/publish/import actions, see [this topic](https://go.microsoft.com/fwlink/?LinkId=267087).  
  
-   **Workarounds** - Extract and export operations will write full-fidelity BCP data files into the .dacpac or .bacpac files. To avoid limitations, use the SQL Server BCP.exe command line utility to deploy full-fidelity data to a target database from a DAC package.  
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)  
  
  
