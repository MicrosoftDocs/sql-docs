---
title: "Apply the Changes to the Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "incremental load [Integration Services],applying changes"
ms.assetid: 338a56db-cb14-4784-a692-468eabd30f41
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Apply the Changes to the Destination
  In the data flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that performs an incremental load of change data, the third and final task is to apply the changes to your destination. You will need one component to apply inserts, one to apply updates, and one to apply deletes.  
  
> [!NOTE]  
>  The second task in designing the data flow of a package that performs an incremental load of change data is to separate inserts, updates, and deletes. For more information about this component, see [Process Inserts, Updates, and Deletes](process-inserts-updates-and-deletes.md). For a description of the overall process for creating a package that performs an incremental load of change data, see [Change Data Capture &#40;SSIS&#41;](change-data-capture-ssis.md).  
  
## Applying Inserts  
 To apply inserts, you use an OLE DB destination because the new rows do not require any special handling.  
  
#### To process inserts by using an OLE DB Destination  
  
1.  On the **Data flow** tab, add an OLE DB destination.  
  
2.  Connect the output that contains inserts from the Conditional Split transformation to the OLE DB destination.  
  
3.  In the **OLE DB Destination Editor**, on the **Connection Manager** page, select the following options:  
  
    1.  Select or create an OLE DB Connection Manager for the destination database.  
  
    2.  Select a **Data access mode** option, and then select the destination table or enter a SQL statement that contains the destination columns.  
  
4.  On the **Mappings** page of the editor, map the appropriate columns from the change data to the destination table.  
  
## Applying Updates  
 To apply updates, you use an OLE DB Command transformation. You use this transformation because you have to use a parameterized UPDATE statement to update one row at a time with the new column values.  
  
> [!NOTE]  
>  You can also use destination components to apply updates. When using this approach, you use the destination components to save the rows to temporary tables that you create for this purpose. Then, you use Execute SQL tasks to perform bulk update and bulk delete operations against the destination from the temporary tables.  
  
#### To process updates by using an OLE DB Command transformation  
  
1.  On the **Data flow** tab, add an OLE DB Command transformation.  
  
2.  Connect the output that contains updates from the Conditional Split transformation to the OLE DB Command transformation.  
  
3.  In the **Advanced Editor for OLE DB Command**, on the **Connection Manager** tab, select or create an OLE DB Connection Manager for the destination database.  
  
4.  In the **Advanced Editor for OLE DB Command**, on the **Component Properties** tab, for **SqlCommand**, enter a parameterized UPDATE statement.  
  
     For example, an UPDATE statement for a Customer table might have the following syntax:  
  
    ```  
    update CDCSample.Customer  
    set TerritoryID  = ?,  
        CustomerType  = ?,  
        rowguid  = ?,  
        ModifiedDate  = ?  
    where CustomerID = ?  
  
    ```  
  
5.  On the **Column Mappings** tab of the editor, map the appropriate columns from the change data to the parameters in the UPDATE statement.  
  
## Applying Deletes  
 To apply deletes, you use an OLE DB Command transformation. You use this transformation because you have to use a parameterized DELETE statement that deletes a single row at a time based on the column value that uniquely identifies the row.  
  
> [!NOTE]  
>  You can also use destination components to apply deletes. When using this approach, you use the destination components to save the rows to temporary tables that you create for this purpose. Then, you use Execute SQL tasks to perform bulk update and bulk delete operations against the destination from the temporary tables.  
  
#### To process deletes by using an OLE DB Command transformation  
  
1.  On the **Data flow** tab, add an OLE DB Command transformation to the data flow.  
  
2.  Connect the output that contains deletes from the Conditional Split transformation to the OLE DB Command transformation.  
  
3.  Open the Advanced Editor to configure the transformation.  
  
4.  In the **Advanced Editor for OLE DB Command**, on the **Connection Manager** tab, select or create an OLE DB Connection Manager for the destination database.  
  
5.  In the **Advanced Editor for OLE DB Command**, on the **Component Properties** tab of the editor, for **SqlCommand**, enter a parameterized DELETE statement.  
  
     For example, a DELETE statement for a Customer table might have the following syntax:  
  
    ```  
    delete from Customer where CustomerID = ?  
  
    ```  
  
6.  On the **Column Mappings** tab of the editor, map the appropriate column from the change data to the parameter in the DELETE statement.  
  
## Optimizing Inserts and Updates by Using MERGE Functionality  
 You can optimize the processing of inserts and updates by combining certain change data capture options with the use of the Transact-SQL MERGE keyword. For more information about the MERGE keyword, see [MERGE &#40;Transact-SQL&#41;](/sql/t-sql/statements/merge-transact-sql).  
  
 In the Transact-SQL statement that retrieves the change data, you can specify *all with merge* as the value of the *row_filter_option* parameter when you call the **cdc.fn_cdc_get_net_changes_<capture_instance>** function. This change data capture function operates more efficiently when it does not have to perform the extra processing that is required to distinguish inserts from updates. When you specify the *all with merge* parameter value, the **__$operation** value of the change data is 1 for deletes or 5 for changes that were caused by inserts or updates. For more information about the Transact-SQL function that is used to retrieve the change data, see [Retrieve and Understand the Change Data](retrieve-and-understand-the-change-data.md).After retrieving changes with the *all with merge* parameter value, you can apply deletes, and output the remaining rows to a temporary table or a staging table. Then, in a downstream Execute SQL Task, you can use a single MERGE statement to apply all the inserts or updates from the staging table to the destination.  
  
  
