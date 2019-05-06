---
title: "Retrieve and Understand the Change Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "incremental load [Integration Services],retrieving data"
ms.assetid: af366697-6942-42bb-aea5-18fdef018965
author: janinezhang
ms.author: janinez
manager: craigg
---
# Retrieve and Understand the Change Data
  In the data flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that performs an incremental load of change data, the first task is to run the query that retrieves the change data. You execute this query inside a source component in a Data Flow task. You can then use downstream transformations and destinations to apply the change data to your destination.  
  
> [!NOTE]  
>  The creation of a query that contains a table-valued function is the third step in the process of creating a package that performs an incremental load of change data. For more information about this query, see, [Create the Function to Retrieve the Change Data](../../integration-services/change-data-capture/create-the-function-to-retrieve-the-change-data.md). For a description of the overall process for creating a package that performs an incremental load of change data, see [Change Data Capture &#40;SSIS&#41;](../../integration-services/change-data-capture/change-data-capture-ssis.md).  
  
## Adding the Data Flow Task  
 In the data flow of the package, you retrieve the change data, separate the rows based on the type of change that occurred, and then apply the changes to the destination.  
  
#### To add a Data Flow task to the package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], on the **Control Flow** tab, add a Data Flow task.  
  
2.  Connect the preceding task that prepared the query string to the Data Flow task.  
  
## Configuring the Source Component to Query for Changes  
 The source component uses the query string that was prepared and stored in a variable to calls the table-valued function that retrieves the changed data.  
  
> [!NOTE]  
>  For more information about the query string that was prepared and stored in a variable, see [Prepare to Query for the Change Data](../../integration-services/change-data-capture/prepare-to-query-for-the-change-data.md). For more information about the table-valued function that retrieves the change data, see [Create the Function to Retrieve the Change Data](../../integration-services/change-data-capture/create-the-function-to-retrieve-the-change-data.md).  
  
#### To configure an OLE DB source to retrieve the change data  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], on the **Data Flow** tab, add an OLE DB source.  
  
2.  In the **OLE DB Source Editor**, on the **Connection Manager** page, select the following options:  
  
    1.  Configure a valid connection to the source database.  
  
    2.  For **Data access mode**, select **SQL command from variable**.  
  
    3.  For **Variable name**, select **User::SqlDataQuery**.  
  
3.  In the **OLE DB Source Editor**, on the **Columns** page, make sure that all the columns that you want are mapped to output columns.  
  
## Next Step  
 After you have configured an OLE DB source to retrieve the change data, the next step is to start designing the data flow in the package.  
  
 **Next topic:** [Process Inserts, Updates, and Deletes](../../integration-services/change-data-capture/process-inserts-updates-and-deletes.md)  
  
  
