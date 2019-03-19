---
title: "Step 6: Adding and Configuring the Lookup Transformations | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 5c59f723-9707-4407-80ae-f05f483cf65f
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 6: Adding and Configuring the Lookup Transformations
  After you have configured the Flat File source to extract data from the source file, the next task is to define the Lookup transformations needed to obtain the values for the **CurrencyKey** and **DateKey**. A Lookup transformation performs a lookup by joining data in the specified input column to a column in a reference dataset. The reference dataset can be an existing table or view, a new table, or the result of an SQL statement. In this tutorial, the Lookup transformation uses an OLE DB connection manager to connect to the database that contains the data that is the source of the reference dataset.  
  
> [!NOTE]  
>  You can also configure the Lookup transformation to connect to a cache that contains the reference dataset. For more information, see [Lookup Transformation](data-flow/transformations/lookup-transformation.md).  
  
 For this tutorial, you will add and configure the following two Lookup transformation components to the package:  
  
-   One transformation to perform a lookup of values from the **CurrencyKey** column of the **DimCurrency** dimension table based on matching **CurrencyID** column values from the flat file.  
  
-   One transformation to perform a lookup of values from the **DateKey** column of the **DimDate** dimension table based on matching **CurrencyDate** column values from the flat file.  
  
 In both cases, the Lookup transformation will utilize the OLE DB connection manager that you previously created.  
  
### To add and configure the Lookup Currency Key transformation  
  
1.  In the **SSIS Toolbox**, expand **Common**, and then drag **Lookup** onto the design surface of the **Data Flow** tab. Place Lookup directly below the **Extract Sample Currency Data** source.  
  
2.  Click the **Extract Sample Currency Data** flat file source and drag the green arrow onto the newly added **Lookup** transformation to connect the two components.  
  
3.  On the **Data Flow** design surface, click **Lookup** in the **Lookup** transformation, and change the name to **Lookup Currency Key**.  
  
4.  Double-click the **Lookup CurrencyKey** transformation to display the Lookup Transformation Editor.  
  
5.  On the **General** page, make the following selections:  
  
    1.  Select **Full cache**.  
  
    2.  In the **Connection type** area, select **OLE DB connection manager**.  
  
6.  On the **Connection** page, make the following selections:  
  
    1.  In the **OLE DB connection manager** dialog box, ensure that **localhost.AdventureWorksDW2012** is displayed.  
  
    2.  Select **Use results of an SQL query**, and then type or copy the following SQL statement:  
  
        ```  
        select * from (select * from [dbo].[DimCurrency]) as refTable  
        where [refTable].[CurrencyAlternateKey] = 'ARS'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'AUD'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'BRL'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'CAD'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'CNY'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'DEM'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'EUR'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'FRF'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'GBP'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'JPY'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'MXN'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'SAR'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'USD'  
        OR  
        [refTable].[CurrencyAlternateKey] = 'VEB'  
        ```  
  
7.  On the **Columns** page, make the following selections:  
  
    1.  In the **Available Input Columns** panel, drag **CurrencyID** to the **Available Lookup Columns** panel and drop it on **CurrencyAlternateKey**.  
  
    2.  In the **Available Lookup Columns** list, select the check box to the left of **CurrencyKey**.  
  
8.  Click **OK** to return to the **Data Flow** design surface.  
  
9. Right-click the Lookup Currency Key transformation, click **Properties**.  
  
10. In the Properties window, verify that the `LocaleID` property is set to **English (United States)** and the **DefaultCodePage** property is set to **1252**.  
  
### To add and configure the  Lookup DateKey transformation  
  
1.  In the **SSIS Toolbox**, drag **Lookup** onto the **Data Flow** design surface. Place Lookup directly below the **Lookup Currency Key** transformation.  
  
2.  Click the **Lookup Currency Key** transformation and drag the green arrow onto the newly added **Lookup** transformation to connect the two components.  
  
3.  In the **Input Output Selection** dialog box, click **Lookup Match Output** in the **Output** list box, and then click **OK**.  
  
4.  On the **Data Flow** design surface, click **Lookup** in the newly added **Lookup** transformation, and change the name to **Lookup Date Key**.  
  
5.  Double-click the **Lookup Date Key** transformation.  
  
6.  On the **General** page, select **Partial cache**.  
  
7.  On the **Connection** page, make the following selections:  
  
    1.  In the **OLEDB connection manager** dialog box, ensure that **localhost.AdventureWorksDW2012** is displayed.  
  
    2.  In the **Use a table or view** box, type or select **[dbo].[DimDate]**.  
  
8.  On the **Columns** page, make the following selections:  
  
    1.  In the **Available Input Columns** panel, drag **CurrencyDate** to the **Available Lookup Columns** panel and drop it on **FullDateAlternateKey**.  
  
    2.  In the **Available Lookup Columns** list, select the check box to the left of **DateKey**.  
  
9. On the **Advanced** page, review the caching options.  
  
10. Click **OK** to return to the **Data Flow** design surface.  
  
11. Right-click the Lookup Date Key transformation and click **Properties.**  
  
12. In the Properties window, verify that the `LocaleID` property is set to **English (United States)** and the **DefaultCodePage** property is set to **1252**.  
  
## Next Task in Lesson  
 [Step 7: Adding and Configuring the OLE DB Destination](lesson-1-7-adding-and-configuring-the-ole-db-destination.md)  
  
## See Also  
 [Lookup Transformation](data-flow/transformations/lookup-transformation.md)  
  
  
