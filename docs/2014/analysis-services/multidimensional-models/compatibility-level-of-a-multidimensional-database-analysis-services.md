---
title: "Set the Compatibility Level of a Multidimensional Database (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 978279e6-a581-4184-af9d-8701b9826a89
author: minewiskan
ms.author: owend
manager: craigg
---
# Set the Compatibility Level of a Multidimensional Database (Analysis Services)
  In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the database compatibility level property determines the functional level of a database. Compatibility levels are unique to each model type. For example, a compatibility level of `1100` has a different meaning depending on whether the database is multidimensional or tabular.  
  
 This topic describes compatibility level for multidimensional databases only. For more information about tabular solutions, see [Compatibility Level &#40;SSAS Tabular SP1&#41;](../tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md).  
  
> [!NOTE]  
>  Tabular models have additional database compatibility levels that are not applicable to multidimensional models. Compatibility level `1103` does not exist for multidimensional models. See [What is new for the Tabular model in SQL Server 2012 SP1 and compatibility level](https://go.microsoft.com/fwlink/?LinkId=301727) for more information about `1103` for tabular solutions.  
  
 **Compatibility Levels for multidimensional databases**  
  
 Currently, the only multidimensional database behavior that varies by functional level is string storage architecture. By raising the database compatibility level, you can override the 4 gigabyte maximum limit for string storage of measures and dimensions.  
  
 For a multidimensional database, valid values for the `CompatibilityLevel` property include the following:  
  
|Setting|Description|  
|-------------|-----------------|  
|`1050`|This value is not visible in script or tools, but it corresponds to databases created in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], or [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]. Any database that does not have `CompatibilityLevel` explicitly set is implicitly running at the `1050` level.|  
|`1100`|This is the default value for new databases that you create in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. You can also specify it for databases created in earlier versions of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to enable the use of features that are supported only at this compatibility level (namely, increased string storage for dimension attributes or distinct count measures that contain string data).<br /><br /> Databases that have a `CompatibilityLevel` set to `1100` get an additional property, `StringStoresCompatibilityLevel`, that lets you choose alternative string storage for partitions and dimensions.|  
  
> [!WARNING]  
>  Setting the database compatibility to a higher level is irreversible. After you increase the compatibility level to `1100`, you must continue to run the database on newer servers. You cannot rollback to `1050`. You cannot attach or restore an `1100` database on a server version that is earlier than [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## Prerequisites  
 Database compatibility levels are introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. You must have [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or higher to view or set the database compatibility level.  
  
 The database cannot be a local cube. Local cubes do not support the `CompatibilityLevel` property.  
  
 The database must have been created in a previous release (SQL Server 2008 R2 or earlier) and then attached or restored to a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or higher server. Databases deployed to SQL Server 2012 are already at `1100` and cannot be downgraded to run at a lower level.  
  
## Determine the existing database compatibility level for a multidimensional database  
 The only way to view or modify the database compatibility level is through XMLA. You can view or modify the XMLA script that specifies your database in SQL Server Management Studio.  
  
 If you search the XMLA definition of a database for the property `CompatibilityLevel` and it does not exist, you most likely have a database at the `1050` level.  
  
 Instructions for viewing and modifying the XMLA script are provided in the next section.  
  
## Set the database compatibility level in SQL Server Management Studio  
  
1.  Before raising the compatibility level, backup the database in case you want to reverse your changes later.  
  
2.  Using SQL Server Management Studio, connect to the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server that hosts the database.  
  
3.  Right-click the database name, point to **Script Database as**, point to **ALTER to**, and then select **New Query Editor Window**. An XMLA representation of the database will open in a new window.  
  
4.  Copy the following XML element:  
  
    ```  
    <ddl200:CompatibilityLevel>1100</ddl200:CompatibilityLevel>  
    ```  
  
5.  Paste it after the `</Annotations>` closing element and before the `<Language>` element. The XML should look similar to the following example:  
  
    ```  
    </Annotations>  
    <ddl200:CompatibilityLevel>1100</ddl200:CompatibilityLevel>  
    <Language>1033</Language>  
    ```  
  
6.  Save the file.  
  
7.  To run the script, click **Execute** on the Query menu or press F5.  
  
## Supported Operations that Require the Same Compatibility Level  
 The following operations require that the source databases share the same compatibility level.  
  
1.  Merging partitions from different databases is supported only if both databases share the same compatibility level.  
  
2.  Using linked dimensions from another database requires the same compatibility level. For example, if you want to use a linked dimension from a [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] database in a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] database, you must port the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] database to a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] server and set the compatibility level to `1100`.  
  
3.  Synchronizing servers is only supported for servers that share the same version and database compatibility level.  
  
## Next Steps  
 After you increase the database compatibility level, you can set the `StringStoresCompatibilityLevel` property in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. This increases string storage for measures and dimensions. For more information about this feature, see [Configure String Storage for Dimensions and Partitions](configure-string-storage-for-dimensions-and-partitions.md).  
  
## See Also  
 [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md)  
  
  
