---
title: "Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Lookup transformation [Integration Services]"
ms.assetid: 58bc7611-5fb5-4113-9742-10959e06b94c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager
  You can configure the Lookup transformation to use full cache mode and a Cache connection manager. In full cache mode, the reference dataset is loaded into cache before the Lookup transformation runs.  
  
> [!NOTE]  
>  The Cache connection manager does not support the Binary Large Object (BLOB) data types DT_TEXT, DT_NTEXT, and DT_IMAGE. If the reference dataset contains a BLOB data type, the component will fail when you run the package. You can use the **Cache Connection Manager Editor** to modify column data types. For more information, see [Cache Connection Manager Editor](../cache-connection-manager-editor.md).  
  
 The Lookup transformation performs lookups by joining data in input columns from a connected data source with columns in a reference dataset. For more information, see [Lookup Transformation](../data-flow/transformations/lookup-transformation.md).  
  
 You use one of the following to generate a reference dataset:  
  
-   Cache file (.caw)  
  
     You configure the Cache connection manager to read data from an existing cache file.  
  
-   Connected data source in the data flow  
  
     You use a Cache Transform transformation to write data from a connected data source in the data flow to a Cache connection manager. The data is always stored in memory.  
  
     You must add the Lookup transformation to a separate data flow. This enables the Cache Transform to populate the Cache connection manager before the Lookup transformation is executed. The data flows can be in the same package or in two separate packages.  
  
     If the data flows are in the same package, use a precedence constraint to connect the data flows. This enables the Cache Transform to run before the Lookup transformation runs.  
  
     If the data flows are in separate packages, you can use the Execute Package task to call the child package from the parent package. You can call multiple child packages by adding multiple Execute Package tasks to a Sequence Container task in the parent package.  
  
 You can share the reference dataset stored in cache, between multiple Lookup transformations by using one of the following methods:  
  
-   Configure the Lookup transformations in a single package to use the same Cache connection manager.  
  
-   Configure the Cache connection managers in different packages to use the same cache file.  
  
 For more information, see the following topics:  
  
-   [Cache Transform](../data-flow/transformations/cache-transform.md)  
  
-   [Cache Connection Manager](cache-connection-manager.md)  
  
-   [Precedence Constraints](../control-flow/precedence-constraints.md)  
  
-   [Execute Package Task](../control-flow/execute-package-task.md)  
  
-   [Sequence Container](../control-flow/sequence-container.md)  
  
 For a video that demonstrates how to implement a Lookup transformation in Full Cache mode using the Cache connection manager, see [How to: Implement a Lookup Transformation in Full Cache Mode (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=131031).  
  
### To implement a Lookup transformation in full cache mode in one package by using Cache connection manager and a data source in the data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, and then open a package.  
  
2.  On the **Control Flow** tab, add two Data Flow tasks, and then connect the tasks by using a green connector:  
  
3.  In the first data flow, add a Cache Transform transformation, and then connect the transformation to a data source.  
  
     Configure the data source as needed.  
  
4.  Double-click the Cache Transform, and then in the **Cache Transformation Editor**, on the **Connection Manager** page, click **New** to create a new Cache connection manager.  
  
5.  Click the **Columns** tab of the **Cache Connection Manager Editor** dialog box, and then specify which columns are the index columns by using the **Index Position** option.  
  
     For non-index columns, the index position is 0. For index columns, the index position is a sequential, positive number.  
  
    > [!NOTE]  
    >  When the Lookup transformation is configured to use a Cache connection manager, only index columns in the reference dataset can be mapped to input columns. Also, all index columns must be mapped. For more information, see [Cache Connection Manager Editor](../cache-connection-manager-editor.md).  
  
6.  To save the cache to a file, in the **Cache Connection Manager Editor**, on the **General** tab, configure the Cache connection manager by setting the following options:  
  
    -   Select **Use file cache**.  
  
    -   For **File name**, either type the file path or click **Browse** to select the file.  
  
         If you type a path for a file that does not exist, the system creates the file when you run the package.  
  
    > [!NOTE]  
    >  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../access-to-files-used-by-packages.md).  
  
7.  Configure the Cache Transform as needed. For more information, see [Cache Transformation Editor &#40;Connection Manager Page&#41;](../cache-transformation-editor-connection-manager-page.md) and [Cache Transformation Editor &#40;Mappings Page&#41;](../cache-transformation-editor-mappings-page.md).  
  
8.  In the second data flow, add a Lookup transformation, and then configure the transformation by doing the following tasks:  
  
    1.  Connect the Lookup transformation to the data flow by dragging a connector from a source or a previous transformation to the Lookup transformation.  
  
        > [!NOTE]  
        >  A Lookup transformation might not validate if that transformation connects to a flat file that contains an empty date field. Whether the transformation validates depends on whether the connection manager for the flat file has been configured to retain null values. To ensure that the Lookup transformation validates, in the **Flat File Source Editor**, on the **Connection Manager Page**, select the **Retain null values from the source as null values in the data flow** option.  
  
    2.  Double-click the source or previous transformation to configure the component.  
  
    3.  Double-click the Lookup transformation, and then in the **Lookup Transformation Editor**, on the **General** page, select **Full cache**.  
  
    4.  In the **Connection type** area, select **Cache connection manager**.  
  
    5.  From the **Specify how to handle rows with no matching entries** list, select an error handling option.  
  
    6.  On the **Connection** page, from the **Cache connection manager** list, select a Cache connection manager.  
  
    7.  Click the **Columns** page, and then drag at least one column from the **Available Input Columns** list to a column in the **Available Lookup Column** list.  
  
        > [!NOTE]  
        >  The Lookup transformation automatically maps columns that have the same name and the same data type.  
  
        > [!NOTE]  
        >  Columns must have matching data types to be mapped. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
    8.  In the **Available Lookup Columns** list, select columns. Then in the **Lookup Operation** list, specify whether the values from the lookup columns replace values in the input column or are written to a new column.  
  
    9. To configure the error output, click the **Error Output** page and set the error handling options. For more information, see [Lookup Transformation Editor &#40;Error Output Page&#41;](../lookup-transformation-editor-error-output-page.md).  
  
    10. Click **OK** to save your changes to the Lookup transformation.  
  
9. Run the package.  
  
### To implement a Lookup transformation in full cache mode in two packages by using Cache connection manager and a data source in the data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, and then open two packages.  
  
2.  On the Control Flow tab in each package, add a Data Flow task.  
  
3.  In the parent package, add a Cache Transform transformation to the data flow, and then connect the transformation to a data source.  
  
     Configure the data source as needed.  
  
4.  Double-click the Cache Transform, and then in the **Cache Transformation Editor**, on the **Connection Manager** page, click **New** to create a new Cache connection manager.  
  
5.  In the **Cache Connection Manager Editor**, on the **General** tab, configure the Cache connection manager by setting the following options:  
  
    -   Select **Use file cache**.  
  
    -   For **File name**, either type the file path or click **Browse** to select the file.  
  
         If you type a path for a file that does not exist, the system creates the file when you run the package.  
  
    > [!NOTE]  
    >  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../access-to-files-used-by-packages.md).  
  
6.  Click the **Columns** tab, and then specify which columns are the index columns by using the **Index Position** option.  
  
     For non-index columns, the index position is 0. For index columns, the index position is a sequential, positive number.  
  
    > [!NOTE]  
    >  When the Lookup transformation is configured to use a Cache connection manager, only index columns in the reference dataset can be mapped to input columns. Also, all index columns must be mapped. For more information, see [Cache Connection Manager Editor](../cache-connection-manager-editor.md).  
  
7.  Configure the Cache Transform as needed. For more information, see [Cache Transformation Editor &#40;Connection Manager Page&#41;](../cache-transformation-editor-connection-manager-page.md) and [Cache Transformation Editor &#40;Mappings Page&#41;](../cache-transformation-editor-mappings-page.md).  
  
8.  Do one of the following to populate the Cache connection manager that is used in the second package:  
  
    -   Run the parent package.  
  
    -   Double-click the Cache connection manager you created in step 4, click **Columns**, select the rows, and then press CTRL+C to copy the column metadata.  
  
9. In the child package, create a Cache connection manager by right-clicking in the **Connection Managers** area, clicking **New Connection**, selecting **CACHE** in the **Add SSIS Connection Manager** dialog box, and then clicking **Add**.  
  
     The **Connection Managers** area appears on the bottom of the **Control Flow**, **Data Flow**, and **Event Handlers** tabs of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Designer.  
  
10. In the **Cache Connection Manager Editor**, on the **General** tab, configure the Cache connection manager to read the data from the cache file that you selected by setting the following options:  
  
    -   Select **Use file cache**.  
  
    -   For **File name**, either type the file path or click **Browse** to select the file.  
  
    > [!NOTE]  
    >  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../access-to-files-used-by-packages.md).  
  
11. If you copied the column metadata in step 8, click **Columns**, select the empty row, and then press CTRL+V to paste the column metadata.  
  
12. Add a Lookup transformation, and then configure the transformation by doing the following:  
  
    1.  Connect the Lookup transformation to the data flow by dragging a connector from a source or a previous transformation to the Lookup transformation.  
  
        > [!NOTE]  
        >  A Lookup transformation might not validate if that transformation connects to a flat file that contains an empty date field. Whether the transformation validates depends on whether the connection manager for the flat file has been configured to retain null values. To ensure that the Lookup transformation validates, in the **Flat File Source Editor**, on the **Connection Manager Page**, select the **Retain null values from the source as null values in the data flow** option.  
  
    2.  Double-click the source or previous transformation to configure the component.  
  
    3.  Double-click the Lookup transformation, and then select **Full cache** on the **General** page of the **Lookup Transformation Editor**.  
  
    4.  Select **Cache connection manager** in the **Connection type** area.  
  
    5.  Select an error handling option for rows without matching entries from the **Specify how to handle rows with no matching entries** list.  
  
    6.  On the **Connection** page, from the **Cache connection manager** list, select the Cache connection manager that you added.  
  
    7.  Click the **Columns** page, and then drag at least one column from the **Available Input Columns** list to a column in the **Available Lookup Column** list.  
  
        > [!NOTE]  
        >  The Lookup transformation automatically maps columns that have the same name and the same data type.  
  
        > [!NOTE]  
        >  Columns must have matching data types to be mapped. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
    8.  In the **Available Lookup Columns** list, select columns. Then in the **Lookup Operation** list, specify whether the values from the lookup columns replace values in the input column or are written to a new column.  
  
    9. To configure the error output, click the **Error Output** page and set the error handling options. For more information, see [Lookup Transformation Editor &#40;Error Output Page&#41;](../lookup-transformation-editor-error-output-page.md).  
  
    10. Click **OK** to save your changes to the Lookup transformation.  
  
13. Open the parent package, add an Execute Package task to the control flow, and then configure the task to call the child package. For more information, see [Execute Package Task](../control-flow/execute-package-task.md).  
  
14. Run the packages.  
  
### To implement a Lookup transformation in full cache mode by using Cache connection manager and an existing cache file  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project, and then open a package.  
  
2.  Right-click in the **Connection Managers** area, and then click **New Connection**.  
  
     The **Connection Managers** area appears on the bottom of the **Control Flow**, **Data Flow**, and **Event Handlers** tabs of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Designer.  
  
3.  In the **Add SSIS Connection Manager** dialog box, select **CACHE**, and then click **Add** to add a Cache connection manager.  
  
4.  Double-click the Cache connection manager to open the **Cache Connection Manager Editor**.  
  
5.  In the **Cache Connection Manager Editor**, on the **General** tab, configure the Cache connection manager by setting the following options:  
  
    -   Select **Use file cache**.  
  
    -   For **File name**, either type the file path or click **Browse** to select the file.  
  
    > [!NOTE]  
    >  The protection level of the package does not apply to the cache file. If the cache file contains sensitive information, use an access control list (ACL) to restrict access to the location or folder in which you store the file. You should enable access only to certain accounts. For more information, see [Access to Files Used by Packages](../access-to-files-used-by-packages.md).  
  
6.  Click the **Columns** tab, and then specify which columns are the index columns by using the **Index Position** option.  
  
     For non-index columns, the index position is 0. For index columns, the index position is a sequential, positive number.  
  
    > [!NOTE]  
    >  When the Lookup transformation is configured to use a Cache connection manager, only index columns in the reference dataset can be mapped to input columns. Also, all index columns must be mapped. For more information, see [Cache Connection Manager Editor](../cache-connection-manager-editor.md).  
  
7.  On the **Control Flow** tab, add a Data Flow task to the package, and then add a Lookup transformation to the data flow.  
  
8.  Configure the Lookup transformation by doing the following:  
  
    1.  Connect the Lookup transformation to the data flow by dragging a connector from a source or a previous transformation to the Lookup transformation.  
  
        > [!NOTE]  
        >  A Lookup transformation might not validate if that transformation connects to a flat file that contains an empty date field. Whether the transformation validates depends on whether the connection manager for the flat file has been configured to retain null values. To ensure that the Lookup transformation validates, in the **Flat File Source Editor**, on the **Connection Manager Page**, select the **Retain null values from the source as null values in the data flow** option.  
  
    2.  Double-click the source or previous transformation to configure the component.  
  
    3.  Double-click the Lookup transformation, and then in the **Lookup Transformation Editor**, on the **General** page, select **Full cache**.  
  
    4.  Select **Cache connection manager** in the **Connection type** area.  
  
    5.  Select an error handling option for rows without matching entries from the **Specify how to handle rows with no matching entries** list.  
  
    6.  On the **Connection** page, from the **Cache connection manager** list, select the Cache connection manager that you added.  
  
    7.  Click the **Columns** page, and then drag at least one column from the **Available Input Columns** list to a column in the **Available Lookup Column** list.  
  
        > [!NOTE]  
        >  The Lookup transformation automatically maps columns that have the same name and the same data type.  
  
        > [!NOTE]  
        >  Columns must have matching data types to be mapped. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
    8.  In the **Available Lookup Columns** list, select columns. Then in the **Lookup Operation** list, specify whether the values from the lookup columns replace values in the input column or are written to a new column.  
  
    9. To configure the error output, click the **Error Output** page and set the error handling options. For more information, see [Lookup Transformation Editor &#40;Error Output Page&#41;](../lookup-transformation-editor-error-output-page.md).  
  
    10. Click **OK** to save your changes to the Lookup transformation.  
  
9. Run the package.  
  
## See Also  
 [Implement a Lookup Transformation in Full Cache Mode Using the OLE DB Connection Manager](lookup-transformation-full-cache-mode-ole-db-connection-manager.md)   
 [Implement a Lookup in No Cache or Partial Cache Mode](../data-flow/transformations/implement-a-lookup-in-no-cache-or-partial-cache-mode.md)   
 [Integration Services Transformations](../data-flow/transformations/integration-services-transformations.md)  
  
  
