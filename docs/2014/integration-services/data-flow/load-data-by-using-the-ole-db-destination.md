---
title: "Load Data by Using the OLE DB Destination | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "loading data"
  - "OLE DB destination [Integration Services]"
  - "destinations [Integration Services], OLE DB"
ms.assetid: 78899498-725e-4300-a7af-f983f4ea384b
author: janinezhang
ms.author: janinez
manager: craigg
---
# Load Data by Using the OLE DB Destination
  To add and configure an OLE DB destination, the package must already include at least one Data Flow task and a source.  
  
### To load data using an OLE DB destination  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the OLE DB destination to the design surface.  
  
4.  Connect the OLE DB destination to the data flow by dragging a connector from a data source or a previous transformation to the destination.  
  
5.  Double-click the OLE DB destination.  
  
6.  In the **OLE DB Destination Editor** dialog box, on the **Connection Manager** page, select an existing OLE DB connection manager or click **New** to create a new connection manager. For more information, see [OLE DB Connection Manager](../connection-manager/ole-db-connection-manager.md).  
  
7.  Select the data access method:  
  
    -   **Table or view** Select a table or view in the database that contains the data.  
  
    -   **Table or view - fast load** Select a table or view in the database that contains the data, and then set the fast-load options: **Keep identity**, **Keep null**, **Table lock**, **Check constraint**, **Rows per batch**, or **Maximum insert commit size**.  
  
    -   **Table name or view name variable** Select the user-defined variable that contains the name of a table or view in the database.  
  
    -   **Table name or view name variable fast load** Select the user-defined variable that contains the name of a table or view in the database that contains the data, and then set the fast-load options.  
  
    -   **SQL command** Type an SQL command or click **Build Query** to write an SQL command by using the **Query Builder**.  
  
8.  Click **Mappings** and then map columns from the **Available Input Columns** list to columns in the **Available Destination Columns** list by dragging columns from one list to another.  
  
    > [!NOTE]  
    >  The OLE DB destination automatically maps same-named columns.  
  
9. To configure the error output, click **Error Output**. For more information, see [Configure an Error Output in a Data Flow Component](../configure-an-error-output-in-a-data-flow-component.md).  
  
10. Click **OK**.  
  
11. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [OLE DB Destination](ole-db-destination.md)   
 [Integration Services Transformations](transformations/integration-services-transformations.md)   
 [Integration Services Paths](integration-services-paths.md)   
 [Data Flow Task](../control-flow/data-flow-task.md)  
  
  
