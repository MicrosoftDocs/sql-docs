---
title: "Cache Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Cache connection manager"
ms.assetid: bdc92038-3720-4795-8a5c-79b963f2c952
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Cache Connection Manager
  The Cache connection manager reads data from the Cache transform or from a cache file (.caw), and can save the data to a cache file. Whether you configure the Cache connection manager to use a cache file, the data is always stored in memory.  
  
 The Cache Transform transformation writes data from a connected data source in the data flow to a Cache connection manager. The Lookup transformation in a package performs lookups on the data.  
  
> [!NOTE]  
>  The Cache connection manager does not support the Binary Large Object (BLOB) data types DT_TEXT, DT_NTEXT, and DT_IMAGE. If the reference dataset contains a BLOB data type, the component will fail when you run the package. You can use the **Cache Connection Manager Editor** to modify column data types. For more information, see [Cache Connection Manager Editor](../cache-connection-manager-editor.md).  
  
> [!NOTE]  
>  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../access-to-files-used-by-packages.md).  
  
## Configuration of the Cache Connection Manager  
 You can configure the Cache connection manager in the following ways:  
  
-   Indicate whether to use a cache file.  
  
     If you configure the cache connection manager to use a cache file, the connection manager will do one of the following actions:  
  
    -   Save data to the file when a Cache Transform transformation is configured to write data from a data source in the data flow to the Cache connection manager.  
  
    -   Read data from the cache file.  
  
     For more information, see [Cache Transform](../data-flow/transformations/cache-transform.md).  
  
-   Change metadata for the columns stored in the cache.  
  
-   Update the cache file name at runtime by using an expression to set the ConnectionString property. For more information, see [Use Property Expressions in Packages](../expressions/use-property-expressions-in-packages.md).  
  
 You can set properties through [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Designer, see [Cache Connection Manager Editor](../cache-connection-manager-editor.md).  
  
 For information about how to configure a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../building-packages-programmatically/adding-connections-programmatically.md).  
  
## Related Tasks  
 [Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager](lookup-transformation-full-cache-mode-ole-db-connection-manager.md)  
  
  
