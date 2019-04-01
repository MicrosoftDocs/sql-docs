---
title: "Bulk Load Data by Using the SQL Server Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server destination"
  - "loading data"
  - "destinations [Integration Services], SQL Server"
  - "inserting data"
  - "bulk load [Integration Services]"
ms.assetid: 8f982f85-a82e-4e2d-9cd8-cd2f85402d8e
author: janinezhang
ms.author: janinez
manager: craigg
---
# Bulk Load Data by Using the SQL Server Destination
  To add and configure a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination, the package must already include at least one Data Flow task and a data source.  
  
### To load data using a SQL Server destination  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination to the design surface.  
  
4.  Connect the destination to a source or a previous transformation in the data flow by dragging a connector to the destination.  
  
5.  Double-click the destination.  
  
6.  In the **SQL Server Destination Editor**, on the **Connection Manager** page, select an existing OLE DB connection manager or click **New** to create a new connection manager. For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
7.  To specify the table or view into which to load the data, do one of the following:  
  
    -   Select an existing table or view.  
  
    -   Click **New**, and in the **Create Table** dialog boxwrite an SQL statement that creates a table or view.  
  
        > [!NOTE]  
        >  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
8.  Click **Mappings** and map columns from the **Available Input Columns** list to columns in the **Available Destination Columns** list by dragging columns from one list to another.  
  
    > [!NOTE]  
    >  The destination automatically maps same-named columns.  
  
9. Click **Advanced** and set the bulk load options: **Keep identity**, **Keep nulls**, **Table lock**, **Check constraints**, and **Fire triggers**.  
  
     Optionally, specify the first and last input rows to insert, the maximum number of errors that can occur before the insert operation stops, and the columns on which the insert is sorted.  
  
    > [!NOTE]  
    >  The sort order is determined by the order in which the columns are listed.  
  
10. Click **OK**.  
  
11. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [SQL Server Destination](../../integration-services/data-flow/sql-server-destination.md)   
 [Integration Services Transformations](../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../integration-services/data-flow/integration-services-paths.md)   
 [Data Flow Task](../../integration-services/control-flow/data-flow-task.md)  
  
  
