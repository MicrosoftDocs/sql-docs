---
title: "Step 3: Add and configure an OLE DB connection manager | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: e7b74cba-a0e5-4478-af12-3f81b9484e9e
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 1-3: Add and configure an OLE DB connection manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



After you add a Flat File connection manager to connect to the data source, you add an OLE DB connection manager to connect to the data destination. An OLE DB connection manager enables a package to extract data from or load data into any OLE DB-compliant data source. Using an OLE DB connection manager, you can specify the server, the authentication method, and the default database for the connection.  
  
In this task, you create an OLE DB connection manager that uses Windows Authentication to connect to the local instance of **AdventureWorksDW2012**. This OLE DB connection manager is also referenced by other components that you create later in this tutorial, such as the Lookup transformation and the OLE DB destination.  
  
## Add and configure an OLE DB connection manager

1. In the **Solution Explorer** pane, right-click on **Connection Managers** and select **New Connection Manager**.

1. In the **Add SSIS Connection Manager** dialog, select **OLEDB**, then select **Add**.
    
2. In the **Configure OLE DB Connection Manager** dialog box, select **New**.  
  
3. For **Server name**, enter **localhost**.  
  
    When you specify localhost as the server name, the connection manager connects to the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on the local computer. To use a remote instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], replace localhost with the name of the server to which you want to connect.  
  
4. In the **Log on to the server** group, verify that **Use Windows Authentication** is selected.  
  
5. In the **Connect to a database** group, in the **Select or enter a database name** box, type or select **AdventureWorksDW2012**.  
  
6. Select **Test Connection** to verify that the connection settings you have specified are valid.  
  
7. Select **OK**.  
  
8. Select **OK**.  
  
9. In the **Connection Managers** pane, verify that **localhost.AdventureWorksDW2012** is selected.  
  

## Go to next task
[Step 4: Add a Data Flow task to the package](../integration-services/lesson-1-4-adding-a-data-flow-task-to-the-package.md)  
  
## See also  
[OLE DB connection manager](../integration-services/connection-manager/ole-db-connection-manager.md)  
  
