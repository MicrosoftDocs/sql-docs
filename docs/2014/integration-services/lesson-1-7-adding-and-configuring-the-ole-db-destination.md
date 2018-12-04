---
title: "Step 7: Adding and Configuring the OLE DB Destination | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 442c841d-d528-4bf0-8724-7156f909ee50
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 7: Adding and Configuring the OLE DB Destination
  Your package now can extract data from the flat file source and transform that data into a format that is compatible with the destination. The next task is to actually load the transformed data into the destination. To load the data, you must add an OLE DB destination to the data flow. The OLE DB destination can use a database table, view, or an SQL command to load data into a variety of OLE DB-compliant databases.  
  
 In this procedure, you add and configure an OLE DB destination to use the OLE DB connection manager that you previously created.  
  
### To add and configure the sample OLE DB destination  
  
1.  In the **SSIS Toolbox**, expand **Other Destinations**, and drag **OLE DB Destination** onto the design surface of the **Data Flow** tab. Place the OLE DB destination directly below the **Lookup Date Key** transformation.  
  
2.  Click the **Lookup Date Key** transformation and drag the green arrow over to the newly added **OLE DB Destination** to connect the two components together.  
  
3.  In the **Input Output Selection** dialog box, in the **Output** list box, click **Lookup Match Output**, and then click **OK**.  
  
4.  On the **Data Flow** design surface, click **OLE DB Destination** in the newly added **OLE DB Destination** component, and change the name to **Sample OLE DB Destination**.  
  
5.  Double-click **Sample OLE DB Destination**.  
  
6.  In the **OLE DB Destination Editor** dialog box, ensure that **localhost.AdventureWorksDW2012** is selected in the **OLE DB Connection manager** box.  
  
7.  In the **Name of the table or the view** box, type or select **[dbo].[FactCurrencyRate]**.  
  
8.  Click the **New** button to create a new table.  Change the name of the table in the script to read **NewFactCurrencyRate**.  Click **OK**.  
  
9. Upon clicking **OK**, the dialog will close and the **Name of the table or the view** will automatically change to **NewFactCurrencyRate**.  
  
10. Click **Mappings**.  
  
11. Verify that the **AverageRate**, **CurrencyKey**, **EndOfDayRate**, and **DateKey** input columns are mapped correctly to the destination columns. If same-named columns are mapped, the mapping is correct.  
  
12. Click **OK**.  
  
13. Right-click the **Sample OLE DB Destination** destination and click **Properties**.  
  
14. In the Properties window, verify that the `LocaleID` property is set to **English (United States)** and the`DefaultCodePage` property is set to **1252**.  
  
## Next Task in Lesson  
 [Step 8: Making the Lesson 1 Package Easier to Understand](lesson-1-8-making-the-lesson-1-package-easier-to-understand.md)  
  
## See Also  
 [OLE DB Destination](data-flow/ole-db-destination.md)  
  
  
