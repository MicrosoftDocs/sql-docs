---
title: "Lookup Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.lookuptrans.f1"
  - "sql13.dts.designer.lookuptransformation.general.f1"
  - "sql13.dts.designer.lookuptransformation.referencetable.f1"
  - "sql13.dts.designer.lookuptransformation.columns.f1"
  - "sql13.dts.designer.lookuptransformation.advanced.f1"
helpviewer_keywords: 
  - "Lookup transformation"
  - "joining columns [Integration Services]"
  - "cache [Integration Services]"
  - "match exactly [Integration Services]"
  - "lookups [Integration Services]"
  - "exact matches [Integration Services]"
ms.assetid: de1cc8de-e7af-4727-b5a5-a1f0a739aa09
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lookup Transformation
  The Lookup transformation performs lookups by joining data in input columns with columns in a reference dataset. You use the lookup to access additional information in a related table that is based on values in common columns.  
  
 The reference dataset can be a cache file, an existing table or view, a new table, or the result of an SQL query. The Lookup transformation uses either an OLE DB connection manager or a Cache connection manager to connect to the reference dataset. For more information, see [OLE DB Connection Manager](../../../integration-services/connection-manager/ole-db-connection-manager.md) and [Cache Connection Manager](../../../integration-services/data-flow/transformations/cache-connection-manager.md)  
  
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
  
-   If there is no matching entry in the reference dataset, no join occurs. By default, the Lookup transformation treats rows without matching entries as errors. However, you can configure the Lookup transformation to redirect such rows to a no match output.  
  
-   If there are multiple matches in the reference table, the Lookup transformation returns only the first match returned by the lookup query. If multiple matches are found, the Lookup transformation generates an error or warning only when the transformation has been configured to load all the reference dataset into the cache. In this case, the Lookup transformation generates a warning when the transformation detects multiple matches as the transformation fills the cache.  
  
 The join can be a composite join, which means that you can join multiple columns in the transformation input to columns in the reference dataset. The transformation supports join columns with any data type, except for DT_R4, DT_R8, DT_TEXT, DT_NTEXT, or DT_IMAGE. For more information, see [Integration Services Data Types](../../../integration-services/data-flow/integration-services-data-types.md).  
  
 Typically, values from the reference dataset are added to the transformation output. For example, the Lookup transformation can extract a product name from a table using a value from an input column, and then add the product name to the transformation output. The values from the reference table can replace column values or can be added to new columns.  
  
 The lookups performed by the Lookup transformation are case sensitive. To avoid lookup failures that are caused by case differences in data, first use the Character Map transformation to convert the data to uppercase or lowercase. Then, include the UPPER or LOWER functions in the SQL statement that generates the reference table. For more information, see [Character Map Transformation](../../../integration-services/data-flow/transformations/character-map-transformation.md), [UPPER &#40;Transact-SQL&#41;](../../../t-sql/functions/upper-transact-sql.md), and [LOWER &#40;Transact-SQL&#41;](../../../t-sql/functions/lower-transact-sql.md).  
  
 The Lookup transformation has the following inputs and outputs:  
  
-   Input.  
  
-   Match output. The match output handles the rows in the transformation input that match at least one entry in the reference dataset.  
  
-   No Match output. The no match output handles rows in the input that do not match at least one entry in the reference dataset. If you configure the Lookup transformation to treat the rows without matching entries as errors, the rows are redirected to the error output. Otherwise, the transformation would redirect those rows to the no match output.  
  
-   Error output.  
  
## Caching the Reference Dataset  
 An in-memory cache stores the reference dataset and stores a hash table that indexes the data. The cache remains in memory until the execution of the package is completed. You can persist the cache to a cache file (.caw).  
  
 When you persist the cache to a file, the system loads the cache faster. This improves the performance of the Lookup transformation and the package. Remember, that when you use a cache file, you are working with data that is not as current as the data in the database.  
  
 The following are additional benefits of persisting the cache to a file:  
  
-   ***Share the cache file between multiple packages. For more information, see***  [Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager](../../../integration-services/data-flow/transformations/lookup-transformation-full-cache-mode-cache-connection-manager.md)  ***.***  
  
-   Deploy the cache file with a package. ***You can then use the data on multiple computers.*** For more information, see [Create and Deploy a Cache for the Lookup Transformation](../../../integration-services/data-flow/transformations/create-and-deploy-a-cache-for-the-lookup-transformation.md).  
  
-   Use the Raw File source to read data from the cache file. You can then use other data flow components to transform or move the data. For more information, see [Raw File Source](../../../integration-services/data-flow/raw-file-source.md).  
  
    > [!NOTE]  
    >  The Cache connection manager does not support cache files that are created or modified by using the Raw File destination.  
  
-   Perform operations and set attributes on the cache file by using the File System task. For more information, see and [File System Task](../../../integration-services/control-flow/file-system-task.md).  
  
 The following are the caching options:  
  
-   The reference dataset is generated by using a table, view, or SQL query and loaded into cache, before the Lookup transformation runs. You use the OLE DB connection manager to access the dataset.  
  
     This caching option is compatible with the full caching option that is available for the Lookup transformation in [!INCLUDE[ssISversion2005](../../../includes/ssisversion2005-md.md)].  
  
-   The reference dataset is generated from a connected data source in the data flow or from a cache file, and is loaded into cache before the Lookup transformation runs. You use the Cache connection manager, and, optionally, the Cache transformation, to access the dataset. For more information, see [Cache Connection Manager](../../../integration-services/data-flow/transformations/cache-connection-manager.md) and [Cache Transform](../../../integration-services/data-flow/transformations/cache-transform.md).  
  
-   The reference dataset is generated by using a table, view, or SQL query during the execution of the Lookup transformation. The rows with matching entries in the reference dataset and the rows without matching entries in the dataset are loaded into cache.  
  
     When the memory size of the cache is exceeded, the Lookup transformation automatically removes the least frequently used rows from the cache.  
  
     This caching option is compatible with the partial caching option that is available for the Lookup transformation in [!INCLUDE[ssISversion2005](../../../includes/ssisversion2005-md.md)].  
  
-   The reference dataset is generated by using a table, view, or SQL query during the execution of the Lookup transformation. No data is cached.  
  
     This caching option is compatible with the no caching option that is available for the Lookup transformation in [!INCLUDE[ssISversion2005](../../../includes/ssisversion2005-md.md)].  
  
 [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] differ in the way they compare strings. If the Lookup transformation is configured to load the reference dataset into cache before the Lookup transformation runs, [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] does the lookup comparison in the cache. Otherwise, the lookup operation uses a parameterized SQL statement and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does the lookup comparison. This means that the Lookup transformation might return a different number of matches from the same lookup table depending on the cache type.  
  
## Related Tasks  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically. For more details, see the following topics.  
  
-   [Implement a Lookup in No Cache or Partial Cache Mode](../../../integration-services/data-flow/transformations/implement-a-lookup-in-no-cache-or-partial-cache-mode.md)  
  
-   [Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager](../../../integration-services/data-flow/transformations/lookup-transformation-full-cache-mode-cache-connection-manager.md)  
  
-   [Implement a Lookup Transformation in Full Cache Mode Using the OLE DB Connection Manager](../../../integration-services/data-flow/transformations/lookup-transformation-full-cache-mode-ole-db-connection-manager.md)  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## Related Content  
  
-   Video, [How to: Implement a Lookup Transformation in Full Cache Mode](https://go.microsoft.com/fwlink/?LinkId=131031), on msdn.microsoft.com  
  
-   Blog entry, [Best Practices for Using the Lookup Transformation Cache Modes](https://go.microsoft.com/fwlink/?LinkId=146623), on blogs.msdn.com  
  
-   Blog entry, [Lookup Pattern: Case Insensitive](https://go.microsoft.com/fwlink/?LinkId=157782), on blogs.msdn.com  
  
-   Sample, [Lookup Transformation](https://go.microsoft.com/fwlink/?LinkId=267528), on msftisprodsamples.codeplex.com.  
  
     For information on installing [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] product samples and sample databases, see [SQL Server Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=267527).  
  
## Lookup Transformation Editor (General Page)
  Use the **General** page of the Lookup Transformation Editor dialog box to select the cache mode, select the connection type, and specify how to handle rows with no matching entries.  
  
### Options  
 **Full cache**  
 Generate and load the reference dataset into cache before the Lookup transformation is executed.  
  
 **Partial cache**  
 Generate the reference dataset during the execution of the Lookup transformation. Load the rows with matching entries in the reference dataset and the rows with no matching entries in the dataset into cache.  
  
 **No cache**  
 Generate the reference dataset during the execution of the Lookup transformation. No data is loaded into cache.  
  
 **Cache connection manager**  
 Configure the Lookup transformation to use a Cache connection manager. This option is available only if the Full cache option is selected.  
  
 **OLE DB connection manager**  
 Configure the Lookup transformation to use an OLE DB connection manager.  
  
 **Specify how to handle rows with no matching entries**  
 Select an option for handling rows that do not match at least one entry in the reference dataset.  
  
 When you select **Redirect rows to no match output**, the rows are redirected to a no match output and are not handled as errors. The **Error** option on the **Error Output** page of the **Lookup Transformation Editor** dialog box is not available.  
  
 When you select any other option in the **Specify how to handle rows with no matching entries** list box, the rows are handled as errors. The **Error** option on the **Error Output** page is available.  
  
### External Resources  
 Blog entry, [Lookup cache modes](https://go.microsoft.com/fwlink/?LinkId=219518) on blogs.msdn.com  
  
## Lookup Transformation Editor (Connection Page)
  Use the **Connection** page of the **Lookup Transformation Editor** dialog box to select a connection manager. If you select an OLE DB connection manager, you also select a query, table, or view to generate the reference dataset.  
  
### Options  
 The following options are available when you select **Full cache** and **Cache connection manager** on the General page of the **Lookup Transformation Editor** dialog box.  
  
 **Cache connection manager**  
 Select an existing Cache connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Cache Connection Manager Editor** dialog box.  
  
 The following options are available when you select **Full cache**, **Partial cache**, or **No cache**, and **OLE DB connection manager**, on the General page of the **Lookup Transformation Editor** dialog box.  
  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Use a table or view**  
 Select an existing table or view from the list, or create a new table by clicking **New**.  
  
> [!NOTE]  
>  If you specify a SQL statement on the **Advanced** page of the **Lookup Transformation Editor**, that SQL statement overrides and replaces the table name selected here. For more information, see [Lookup Transformation Editor &#40;Advanced Page&#41;](../../../integration-services/data-flow/transformations/lookup-transformation-editor-advanced-page.md).  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
 **Use results of an SQL query**  
 Choose this option to browse to a preexisting query, build a new query, check query syntax, and preview query results.  
  
 **Build query**  
 Create the Transact-SQL statement to run by using **Query Builder**, a graphical tool that is used to create queries by browsing through data.  
  
 **Browse**  
 Use this option to browse to a preexisting query saved as a file.  
  
 **Parse Query**  
 Check the syntax of the query.  
  
 **Preview**  
 Preview results by using the **Preview Query Results** dialog box. This option displays up to 200 rows.  
  
### External Resources  
 Blog entry, [Lookup cache modes](https://go.microsoft.com/fwlink/?LinkId=219518) on blogs.msdn.com  
  
## Lookup Transformation Editor (Columns Page)
  Use the **Columns** page of the **Lookup Transformation Editor** dialog box to specify the join between the source table and the reference table, and to select lookup columns from the reference table.  
  
### Options  
 **Available Input Columns**  
 View the list of available input columns. The input columns are the columns in the data flow from a connected source. The input columns and lookup column must have matching data types.  
  
 Use a drag-and-drop operation to map available input columns to lookup columns.  
  
 You can also map input columns to lookup columns using the keyboard, by highlighting a column in the **Available Input Columns** table, pressing the Application key, and then clicking **Edit Mappings**.  
  
 **Available Lookup Columns**  
 View the list of lookup columns. The lookup columns are columns in the reference table in which you want to look up values that match the input columns.  
  
 Use a drag-and-drop operation to map available lookup columns to input columns.  
  
 Use the check boxes to select lookup columns in the reference table on which to perform lookup operations.  
  
 You can also map lookup columns to input columns using the keyboard, by highlighting a column in the **Available Lookup Columns** table, pressing the Application key, and then clicking **Edit Mappings**.  
  
 **Lookup Column**  
 View the selected lookup columns. The selections are reflected in the check box selections in the **Available Lookup Columns** table.  
  
 **Lookup Operation**  
 Select a lookup operation from the list to perform on the lookup column.  
  
 **Output Alias**  
 Type an alias for the output for each lookup column. The default is the name of the lookup column; however, you can select any unique, descriptive name.  
  
## Lookup Transformation Editor (Advanced Page)
  Use the **Advanced** page of the **Lookup Transformation Editor** dialog box to configure partial caching and to modify the SQL statement for the Lookup transformation.  
  
### Options  
 **Cache size (32-bit)**  
 Adjust the  cache size (in megabytes) for 32-bit computers. The default value is 5 megabytes.  
  
 **Cache size (64-bit)**  
 Adjust the cache size (in megabytes) for 64-bit computers. The default value is 5 megabytes.  
  
 **Enable cache for rows with no matching entries**  
 Cache rows with no matching entries in the reference dataset.  
  
 **Allocation from cache**  
 Specify the percentage of the cache to allocate for rows with no matching entries in the reference dataset.  
  
 **Modify the SQL statement**  
 Modify the SQL statement that is used to generate the reference dataset.  
  
> [!NOTE]  
>  The optional SQL statement that you specify on this page overrides and replaces the table name that you specified on the **Connection** page of the **Lookup Transformation Editor**. For more information, see [Lookup Transformation Editor &#40;Connection Page&#41;](../../../integration-services/data-flow/transformations/lookup-transformation-editor-connection-page.md).  
  
 **Set Parameters**  
 Map input columns to parameters by using the **Set Query Parameters** dialog box.  
  
### External Resources  
 Blog entry, [Lookup cache modes](https://go.microsoft.com/fwlink/?LinkId=219518) on blogs.msdn.com  
  
## See Also  
 [Fuzzy Lookup Transformation](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation.md)   
 [Term Lookup Transformation](../../../integration-services/data-flow/transformations/term-lookup-transformation.md)   
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  
