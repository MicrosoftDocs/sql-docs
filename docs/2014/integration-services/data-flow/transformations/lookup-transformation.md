---
title: "Lookup Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.lookuptrans.f1"
helpviewer_keywords: 
  - "Lookup transformation"
  - "joining columns [Integration Services]"
  - "cache [Integration Services]"
  - "match exactly [Integration Services]"
  - "lookups [Integration Services]"
  - "exact matches [Integration Services]"
ms.assetid: de1cc8de-e7af-4727-b5a5-a1f0a739aa09
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lookup Transformation
  The Lookup transformation performs lookups by joining data in input columns with columns in a reference dataset. You use the lookup to access additional information in a related table that is based on values in common columns.  
  
 The reference dataset can be a cache file, an existing table or view, a new table, or the result of an SQL query. The Lookup transformation uses either an OLE DB connection manager or a Cache connection manager to connect to the reference dataset. For more information, see [OLE DB Connection Manager](../../connection-manager/ole-db-connection-manager.md) and [Cache Connection Manager](../../connection-manager/cache-connection-manager.md)  
  
 You can configure the Lookup transformation in the following ways:  
  
-   Select the connection manager that you want to use. If you want to connect to a database, select an OLE DB connection manager. If you want to connect to a cache file, select a Cache connection manager.  
  
-   Specify the table or view that contains the reference dataset.  
  
-   Generate a reference dataset by specifying an SQL statement.  
  
-   Specify joins between the input and the reference dataset.  
  
-   Add columns from the reference dataset to the Lookup transformation output.  
  
-   Configure the caching options.  
  
 The Lookup transformation supports the following database providers for the OLE DB connection manager:  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]  
  
-   Oracle  
  
-   DB2  
  
 The Lookup transformation tries to perform an equi-join between values in the transformation input and values in the reference dataset. (An equi-join means that each row in the transformation input must match at least one row from the reference dataset.) If an equi-join is not possible, the Lookup transformation takes one of the following actions:  
  
-   If there is no matching entry in the reference dataset, no join occurs. By default, the Lookup transformation treats rows without matching entries as errors. However, you can configure the Lookup transformation to redirect such rows to a no match output. For more information, see [Lookup Transformation Editor &#40;General Page&#41;](../../lookup-transformation-editor-general-page.md) and [Lookup Transformation Editor &#40;Error Output Page&#41;](../../lookup-transformation-editor-error-output-page.md).  
  
-   If there are multiple matches in the reference table, the Lookup transformation returns only the first match returned by the lookup query. If multiple matches are found, the Lookup transformation generates an error or warning only when the transformation has been configured to load all the reference dataset into the cache. In this case, the Lookup transformation generates a warning when the transformation detects multiple matches as the transformation fills the cache.  
  
 The join can be a composite join, which means that you can join multiple columns in the transformation input to columns in the reference dataset. The transformation supports join columns with any data type, except for DT_R4, DT_R8, DT_TEXT, DT_NTEXT, or DT_IMAGE. For more information, see [Integration Services Data Types](../integration-services-data-types.md).  
  
 Typically, values from the reference dataset are added to the transformation output. For example, the Lookup transformation can extract a product name from a table using a value from an input column, and then add the product name to the transformation output. The values from the reference table can replace column values or can be added to new columns.  
  
 The lookups performed by the Lookup transformation are case sensitive. To avoid lookup failures that are caused by case differences in data, first use the Character Map transformation to convert the data to uppercase or lowercase. Then, include the UPPER or LOWER functions in the SQL statement that generates the reference table. For more information, see [Character Map Transformation](character-map-transformation.md), [UPPER &#40;Transact-SQL&#41;](/sql/t-sql/functions/upper-transact-sql), and [LOWER &#40;Transact-SQL&#41;](/sql/t-sql/functions/lower-transact-sql).  
  
 The Lookup transformation has the following inputs and outputs:  
  
-   Input.  
  
-   Match output. The match output handles the rows in the transformation input that match at least one entry in the reference dataset.  
  
-   No Match output. The no match output handles rows in the input that do not match at least one entry in the reference dataset. If you configure the Lookup transformation to treat the rows without matching entries as errors, the rows are redirected to the error output. Otherwise, the transformation would redirect those rows to the no match output.  
  
    > [!NOTE]  
    >  In [!INCLUDE[ssISversion2005](../../../includes/ssisversion2005-md.md)], the Lookup transformation had only one output. For more information about how to run a Lookup transformation that was created in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], see [Upgrade Lookup Transformations](../../../sql-server/install/upgrade-lookup-transformations.md).  
  
-   Error output.  
  
## Caching the Reference Dataset  
 An in-memory cache stores the reference dataset and stores a hash table that indexes the data. The cache remains in memory until the execution of the package is completed. You can persist the cache to a cache file (.caw).  
  
 When you persist the cache to a file, the system loads the cache faster. This improves the performance of the Lookup transformation and the package. Remember, that when you use a cache file, you are working with data that is not as current as the data in the database.  
  
 The following are additional benefits of persisting the cache to a file:  
  
-   ***Share the cache file between multiple packages. For more information, see***  [Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager](../../connection-manager/lookup-transformation-full-cache-mode-cache-connection-manager.md)  ***.***  
  
-   Deploy the cache file with a package. ***You can then use the data on multiple computers.*** For more information, see [Create and Deploy a Cache for the Lookup Transformation](create-and-deploy-a-cache-for-the-lookup-transformation.md).  
  
-   Use the Raw File source to read data from the cache file. You can then use other data flow components to transform or move the data. For more information, see [Raw File Source](../raw-file-source.md).  
  
    > [!NOTE]  
    >  The Cache connection manager does not support cache files that are created or modified by using the Raw File destination.  
  
-   Perform operations and set attributes on the cache file by using the File System task. For more information, see and [File System Task](../../control-flow/file-system-task.md).  
  
 The following are the caching options:  
  
-   The reference dataset is generated by using a table, view, or SQL query and loaded into cache, before the Lookup transformation runs. You use the OLE DB connection manager to access the dataset.  
  
     This caching option is compatible with the full caching option that is available for the Lookup transformation in [!INCLUDE[ssISversion2005](../../../includes/ssisversion2005-md.md)].  
  
-   The reference dataset is generated from a connected data source in the data flow or from a cache file, and is loaded into cache before the Lookup transformation runs. You use the Cache connection manager, and, optionally, the Cache transformation, to access the dataset. For more information, see [Cache Connection Manager](../../connection-manager/cache-connection-manager.md) and [Cache Transform](cache-transform.md).  
  
-   The reference dataset is generated by using a table, view, or SQL query during the execution of the Lookup transformation. The rows with matching entries in the reference dataset and the rows without matching entries in the dataset are loaded into cache.  
  
     When the memory size of the cache is exceeded, the Lookup transformation automatically removes the least frequently used rows from the cache.  
  
     This caching option is compatible with the partial caching option that is available for the Lookup transformation in [!INCLUDE[ssISversion2005](../../../includes/ssisversion2005-md.md)].  
  
-   The reference dataset is generated by using a table, view, or SQL query during the execution of the Lookup transformation. No data is cached.  
  
     This caching option is compatible with the no caching option that is available for the Lookup transformation in [!INCLUDE[ssISversion2005](../../../includes/ssisversion2005-md.md)].  
  
 [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] differ in the way they compare strings. If the Lookup transformation is configured to load the reference dataset into cache before the Lookup transformation runs, [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] does the lookup comparison in the cache. Otherwise, the lookup operation uses a parameterized SQL statement and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does the lookup comparison. This means that the Lookup transformation might return a different number of matches from the same lookup table depending on the cache type.  
  
## Related Tasks  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically. For more details, see the following topics.  
  
-   [Implement a Lookup in No Cache or Partial Cache Mode](implement-a-lookup-in-no-cache-or-partial-cache-mode.md)  
  
-   [Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager](../../connection-manager/lookup-transformation-full-cache-mode-cache-connection-manager.md)  
  
-   [Implement a Lookup Transformation in Full Cache Mode Using the OLE DB Connection Manager](../../connection-manager/lookup-transformation-full-cache-mode-ole-db-connection-manager.md)  
  
-   [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md)  
  
## Related Content  
  
-   Video, [How to: Implement a Lookup Transformation in Full Cache Mode](https://go.microsoft.com/fwlink/?LinkId=131031), on msdn.microsoft.com  
  
-   Blog entry, [Best Practices for Using the Lookup Transformation Cache Modes](https://go.microsoft.com/fwlink/?LinkId=146623), on blogs.msdn.com  
  
-   Blog entry, [Lookup Pattern: Case Insensitive](https://go.microsoft.com/fwlink/?LinkId=157782), on blogs.msdn.com  
  
-   Sample, [Lookup Transformation](https://go.microsoft.com/fwlink/?LinkId=267528), on msftisprodsamples.codeplex.com.  
  
     For information on installing [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] product samples and sample databases, see [SQL Server Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=267527).  
  
## See Also  
 [Fuzzy Lookup Transformation](fuzzy-lookup-transformation.md)   
 [Term Lookup Transformation](term-lookup-transformation.md)   
 [Data Flow](../data-flow.md)   
 [Integration Services Transformations](integration-services-transformations.md)  
  
  
