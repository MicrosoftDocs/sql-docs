---
title: "Cache Connection Manager Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.cacheconnection.f1"
ms.assetid: 0d8f9324-0c35-4eea-b06d-da3cc2426d2c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Cache Connection Manager Editor
  The Cache connection manager reads a reference dataset from the Cache transform or from a cache file (.caw), and can save the data to a cache file. The data is always stored in memory.  
  
> [!NOTE]  
>  The Cache connection manager does not support the Binary Large Object (BLOB) data types DT_TEXT, DT_NTEXT, and DT_IMAGE. If the reference dataset contains a BLOB data type, the component will fail when you run the package. You can use the **Cache Connection Manager Editor** to modify column data types.  
  
 The Lookup transformation performs lookups on the reference dataset.  
  
 The **Cache Connection ManagerEditor** dialog box includes the following tabs:  
  
-   [General tab](#generaltab)  
  
-   [Columns tab](#columnstab)  
  
 To learn more about the Cache Connection Manager, see [Cache Connection Manager](connection-manager/cache-connection-manager.md).  
  
##  <a name="generaltab"></a> General Tab  
 Use the **General** tab of the **Cache Connection ManagerEditor** dialog box to indicate whether to read the cache from a file or save the cache to a file.  
  
### Options  
 **Connection manager name**  
 Provide a unique name for the cache connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection according to its purpose, to make packages self-documenting and easier to maintain.  
  
 **Use file cache**  
 Indicate whether to use a cache file.  
  
> [!NOTE]  
>  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../../2014/integration-services/access-to-files-used-by-packages.md).  
  
 If you configure the cache connection manager to use a cache file, the connection manager will do one of the following actions:  
  
-   Save data to the file when a Cache Transform transformation is configured to write data from a data source in the data flow to the Cache connection manager. For more information, see [Cache Transform](data-flow/transformations/cache-transform.md).  
  
-   Read data from the cache file.  
  
 **File name**  
 Type the path and file name of the cache file.  
  
 **Browse**  
 Locate the cache file.  
  
 **Refresh Metadata**  
 Delete the column metadata in the Cache connection manager and repopulate the Cache connection manager with column metadata from a selected cache file.  
  
##  <a name="columnstab"></a> Columns Tab  
 Use the **Columns** tab of the **Cache Connection Manager Editor** dialog box to configure the properties of each column in the cache.  
  
### Options  
 **Column**  
 Specify the column name.  
  
 **Index Position**  
 Specify which columns are index columns by specifying the index position of each column. The index is a collection of one or more columns.  
  
 For non-index columns, the index position is 0.  
  
 For index columns, the index position is a sequential, positive number. This number indicates the order in which the Lookup transformation compares rows in the reference dataset to rows in the input data source. The column with the most unique values should have the lowest index position.  
  
> [!NOTE]  
>  When the Lookup transformation is configured to use a Cache connection manager, only index columns in the reference dataset can be mapped to input columns. Also, all index columns must be mapped.  
  
 **Type**  
 Specify the data type of the column.  
  
 `Length`  
 Specifies the column data type. If applicable to the data type, you can update `Length`.  
  
 `Precision`  
 Specifies the precision for certain column data types. Precision is the number of digits in a number. If applicable to the data type, you can update `Precision`.  
  
 `Scale`  
 Specifies the scale for certain column data types. Scale is the number of digits to the right of the decimal point in a number. If applicable to the data type, you can update `Scale`.  
  
 `Code Page`  
 Specifies the code page for the column type. If applicable to the data type, you can update `Code Page`.  
  
## See Also  
 [Lookup Transformation](data-flow/transformations/lookup-transformation.md)  
  
  
