---
title: "Cache Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.cacheconnection.f1"
helpviewer_keywords: 
  - "Cache connection manager"
ms.assetid: bdc92038-3720-4795-8a5c-79b963f2c952
author: janinezhang
ms.author: janinez
manager: craigg
---
# Cache Connection Manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Cache connection manager reads data from the Cache transform or from a cache file (.caw), and can save the data to a cache file. Whether you configure the Cache connection manager to use a cache file, the data is always stored in memory.  
  
 The Cache Transform transformation writes data from a connected data source in the data flow to a Cache connection manager. The Lookup transformation in a package performs lookups on the data.  
  
> [!NOTE]  
>  The Cache connection manager does not support the Binary Large Object (BLOB) data types DT_TEXT, DT_NTEXT, and DT_IMAGE. If the reference dataset contains a BLOB data type, the component will fail when you run the package. You can use the **Cache Connection Manager Editor** to modify column data types. For more information, see [Cache Connection Manager Editor](cache-connection-manager-editor.md).  
  
> [!NOTE]  
>  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../../integration-services/security/security-overview-integration-services.md#files).  
  
## Configuration of the Cache Connection Manager  
 You can configure the Cache connection manager in the following ways:  
  
-   Indicate whether to use a cache file.  
  
     If you configure the cache connection manager to use a cache file, the connection manager will do one of the following actions:  
  
    -   Save data to the file when a Cache Transform transformation is configured to write data from a data source in the data flow to the Cache connection manager.  
  
    -   Read data from the cache file.  
  
     For more information, see [Cache Transform](../../integration-services/data-flow/transformations/cache-transform.md).  
  
-   Change metadata for the columns stored in the cache.  
  
-   Update the cache file name at runtime by using an expression to set the ConnectionString property. For more information, see [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md).  
  
 You can set properties through [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Designer or programmatically.  
  
 For information about how to configure a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## Cache Connection Manager Editor
  The Cache connection manager reads a reference dataset from the Cache transform or from a cache file (.caw), and can save the data to a cache file. The data is always stored in memory.  
  
> [!NOTE]  
>  The Cache connection manager does not support the Binary Large Object (BLOB) data types DT_TEXT, DT_NTEXT, and DT_IMAGE. If the reference dataset contains a BLOB data type, the component will fail when you run the package. You can use the **Cache Connection Manager Editor** to modify column data types.  
  
 The Lookup transformation performs lookups on the reference dataset.  
  
 The **Cache Connection ManagerEditor** dialog box includes the following tabs:  
  
###  <a name="generaltab"></a> General Tab  
 Use the **General** tab of the **Cache Connection ManagerEditor** dialog box to indicate whether to read the cache from a file or save the cache to a file.  
  
#### Options  
 **Connection manager name**  
 Provide a unique name for the cache connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection according to its purpose, to make packages self-documenting and easier to maintain.  
  
 **Use file cache**  
 Indicate whether to use a cache file.  
  
> [!NOTE]  
>  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../../integration-services/security/security-overview-integration-services.md#files).  
  
 If you configure the cache connection manager to use a cache file, the connection manager will do one of the following actions:  
  
-   Save data to the file when a Cache Transform transformation is configured to write data from a data source in the data flow to the Cache connection manager. For more information, see [Cache Transform](../../integration-services/data-flow/transformations/cache-transform.md).  
  
-   Read data from the cache file.  
  
 **File name**  
 Type the path and file name of the cache file.  
  
 **Browse**  
 Locate the cache file.  
  
 **Refresh Metadata**  
 Delete the column metadata in the Cache connection manager and repopulate the Cache connection manager with column metadata from a selected cache file.  
  
###  <a name="columnstab"></a> Columns Tab  
 Use the **Columns** tab of the **Cache Connection Manager Editor** dialog box to configure the properties of each column in the cache.  
  
#### Options  
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
  
 **Length**  
 Specifies the column data type. If applicable to the data type, you can update **Length**.  
  
 **Precision**  
 Specifies the precision for certain column data types. Precision is the number of digits in a number. If applicable to the data type, you can update **Precision**.  
  
 **Scale**  
 Specifies the scale for certain column data types. Scale is the number of digits to the right of the decimal point in a number. If applicable to the data type, you can update **Scale**.  
  
 **Code Page**  
 Specifies the code page for the column type. If applicable to the data type, you can update **Code Page**.  
  
## Related Tasks  
 [Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager](lookup-transformation-full-cache-mode-cache-connection-manager.md)  
  
  
