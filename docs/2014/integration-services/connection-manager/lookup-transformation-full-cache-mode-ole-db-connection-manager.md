---
title: "Implement a Lookup Transformation in Full Cache Mode Using the OLE DB Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Lookup transformation [Integration Services]"
ms.assetid: c4150e1b-bdff-4f7a-af4c-3401c34def83
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Implement a Lookup Transformation in Full Cache Mode Using the OLE DB Connection Manager
  You can configure the Lookup transformation to use full cache mode and an OLE DB connection manager. In the full cache mode, the reference dataset is loaded into cache before the Lookup transformation runs.  
  
 The Lookup transformation performs lookups by joining data in input columns from a connected data source with columns in a reference dataset. For more information, see [Lookup Transformation](../data-flow/transformations/lookup-transformation.md).  
  
 When you configure the Lookup transformation to use an OLE DB connection manager, you select a table, view, or SQL query to generate the reference dataset.  
  
### To implement a Lookup transformation in full cache mode by using OLE DB connection manager  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want, and then double-click the package in Solution Explorer.  
  
2.  On the **Data Flow** tab, from the **Toolbox**, drag the Lookup transformation to the design surface.  
  
3.  Connect the Lookup transformation to the data flow by dragging a connector from a source or a previous transformation to the Lookup transformation.  
  
    > [!NOTE]  
    >  A Lookup transformation might not validate if that transformation connects to a flat file that contains an empty date field. Whether the transformation validates depends on whether the connection manager for the flat file has been configured to retain null values. To ensure that the Lookup transformation validates, in the **Flat File Source Editor**, on the **Connection Manager Page**, select the **Retain null values from the source as null values in the data flow** option.  
  
4.  Double-click the source or previous transformation to configure the component.  
  
5.  Double-click the Lookup transformation, and then in the **Lookup Transformation Editor**, on the **General** page, select **Full cache**.  
  
6.  In the **Connection type** area, select **OLE DB connection manager**.  
  
7.  In the **Specify how to handle rows with no matching entries** list, select an error handling option for rows without matching entries.  
  
8.  On the Connection page, select a connection manager from the **OLE DB connection manager** list or click **New** to create a new connection manager. For more information, see [OLE DB Connection Manager](ole-db-connection-manager.md).  
  
9. Do one of the following tasks:  
  
    -   Click **Use a table or a view**, and then either select a table or view, or click **New** to create a table or view.  
  
         -or-  
  
    -   Click **Use results of an SQL query**, and then build a query in the **SQL Command** window, or click **Build Query** to build a query by using the graphical tools that the **Query Builder** provides.  
  
         -or-  
  
    -   Alternatively, click **Browse** to import an SQL statement from a file.  
  
     To validate the SQL query, click **Parse Query**.  
  
     To view a sample of the data, click **Preview**.  
  
10. Click the **Columns** page, and then from the **Available Input Columns** list, drag at least one column to a column in the **Available Lookup Column** list.  
  
    > [!NOTE]  
    >  The Lookup transformation automatically maps columns that have the same name and the same data type.  
  
    > [!NOTE]  
    >  Columns must have matching data types to be mapped. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
11. Include lookup columns in the output by doing the following tasks:  
  
    1.  In the **Available Lookup Columns** list. select columns.  
  
    2.  In **Lookup Operation** list, specify whether the values from the lookup columns replace values in the input column or are written to a new column.  
  
12. To configure the error output, click the **Error Output** page and set the error handling options. For more information, see [Lookup Transformation Editor &#40;Error Output Page&#41;](../lookup-transformation-editor-error-output-page.md).  
  
13. Click **OK** to save your changes to the Lookup transformation, and then run the package.  
  
## See Also  
 [Implement a Lookup Transformation in Full Cache Mode Using the Cache Connection Manager](lookup-transformation-full-cache-mode-ole-db-connection-manager.md)   
 [Implement a Lookup in No Cache or Partial Cache Mode](../data-flow/transformations/implement-a-lookup-in-no-cache-or-partial-cache-mode.md)   
 [Integration Services Transformations](../data-flow/transformations/integration-services-transformations.md)  
  
  
