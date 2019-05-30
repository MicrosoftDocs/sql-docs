---
title: "Step 6: Add and configure the Lookup transformations | Microsoft Docs"
ms.custom: ""
ms.date: 03/19/2019
ms.prod: sql
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 5c59f723-9707-4407-80ae-f05f483cf65f
author: janinezhang
ms.author: janinez
manager: craigg
ms.reviewer: ""
---
# Lesson 1-6: Add and configure the Lookup transformations

[!INCLUDE[ssis-appliesto](../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



After you have configured the Flat File source to extract data from the source file, you define the Lookup transformations needed to obtain the values for **CurrencyKey** and **DateKey**. A Lookup transformation performs a lookup by joining data in the specified input column to a column in a reference dataset. The reference dataset can be an existing table or view, a new table, or the result of an SQL statement. In this tutorial, the Lookup transformation uses an OLE DB connection manager to connect to the database that contains the source data of the reference dataset.  
  
> [!NOTE]  
> You can also configure the Lookup transformation to connect to a cache that contains the reference dataset. For more information, see [Lookup transformation](../integration-services/data-flow/transformations/lookup-transformation.md).  
  
In this task, you add and configure the following two Lookup transformation components to the package:  
  
-   One transformation that does a lookup of values from the **CurrencyKey** column of the **DimCurrency** dimension table, based on matching **CurrencyID** column values from the flat file.  
  
-   One transformation that does a lookup of values from the **DateKey** column of the **DimDate** dimension table, based on matching **CurrencyDate** column values from the flat file.  
  
In both cases, the Lookup transformation uses the OLE DB connection manager you previously created.  
  
## Add and configure the Lookup Currency Key transformation  
  
1.  In the **SSIS Toolbox**, expand **Common**, and then drag **Lookup** onto the design surface of the **Data Flow** tab. Place **Lookup** directly below the **Extract Sample Currency Data** source.  
  
2.  Select the **Extract Sample Currency Data** flat file source and drag its blue arrow onto the newly added **Lookup** transformation to connect the two components.  
  
3.  On the **Data Flow** design surface, select **Lookup** in the **Lookup** transformation, and change the name to **Lookup Currency Key**.  
  
4.  Double-click the **Lookup Currency Key** transformation to display the **Lookup Transformation Editor**.  
  
5.  On the **General** page, make the following selections:  
  
    1.  Select **Full cache**.  
  
    2.  In the **Connection type** area, select **OLE DB connection manager**.  
  
6.  On the **Connection** page, make the following selections:  
  
    1.  In the **OLE DB connection manager** dialog box, ensure that **localhost.AdventureWorksDW2012** is displayed.  
  
    2.  Select **Use results of an SQL query**, and then enter or paste the following SQL statement:  
  
        ```sql
        SELECT * FROM [dbo].[DimCurrency]
        WHERE [CurrencyAlternateKey]
        IN ('ARS', 'AUD', 'BRL', 'CAD', 'CNY',
            'DEM', 'EUR', 'FRF', 'GBP', 'JPY',
	        'MXN', 'SAR', 'USD', 'VEB')
        ```  
    3.  Select **Preview** to verify the query results.
  
7.  On the **Columns** page, make the following selections:  
  
    1.  In the **Available Input Columns** panel, drag **CurrencyID** to the **Available Lookup Columns** panel and drop it on **CurrencyAlternateKey**.  
  
    2.  In the **Available Lookup Columns** list, select the check box to the left of **CurrencyKey**.  
  
8.  Select **OK** to return to the **Data Flow** design surface.  
  
9. Right-click the Lookup Currency Key transformation and select **Properties**.  
  
10. In the **Properties** window, verify that the **LocaleID** property is **English (United States)** and the **DefaultCodePage** property is **1252**.  
  
## Add and configure the Lookup Date Key transformation  
  
1.  In the **SSIS Toolbox**, drag **Lookup** onto the **Data Flow** design surface. Place this **Lookup** directly below the **Lookup Currency Key** transformation.  
  
2.  Select the **Lookup Currency Key** transformation and drag its blue arrow onto the new **Lookup** transformation to connect the two components.  
  
3.  In the **Input Output Selection** dialog, select **Lookup Match Output** in the **Output** list box, and then select **OK**.  
  
4.  On the **Data Flow** design surface, select the name **Lookup** in the newly added **Lookup** transformation and change that name to **Lookup Date Key**.  
  
5.  Double-click the **Lookup Date Key** transformation.  
  
6.  On the **General** page, select **Partial cache**.  
  
7.  On the **Connection** page, make the following selections:  
  
    1.  In the **OLEDB connection manager** dialog, ensure that **localhost.AdventureWorksDW2012** is displayed.  
  
    2.  In the **Use a table or view** box, enter or select **[dbo].[DimDate]**.  
  
8.  On the **Columns** page, make the following selections:  
  
    1.  In the **Available Input Columns** panel, drag **CurrencyDate** to the **Available Lookup Columns** panel and drop it on **FullDateAlternateKey**.  If you see a message indicating a data type mismatch, change the data type of CurrencyDate to [DT_DBDATE].
  
    2.  In the **Available Lookup Columns** list, select the check box to the left of **DateKey**.  
  
9. On the **Advanced** page, review the caching options.  
  
10. Select **OK** to return to the **Data Flow** design surface.  
  
11. Right-click the **Lookup Date Key** transformation and select **Properties**.
  
12. In the **Properties** window, verify that the **LocaleID** property is **English (United States)** and the **DefaultCodePage** property is **1252**.  
  
## Go to next task
[Step 7: Add and configure the OLE DB destination](../integration-services/lesson-1-7-adding-and-configuring-the-ole-db-destination.md)  
  
## See also  
[Lookup transformation](../integration-services/data-flow/transformations/lookup-transformation.md)  
