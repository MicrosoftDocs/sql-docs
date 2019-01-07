---
title: "Disable Stretch Database and bring back remote data | Microsoft Docs"
ms.custom: ""
ms.date: "08/05/2016"
ms.prod: sql
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Stretch Database, disabling"
  - "disabling Stretch Database"
ms.assetid: c1bbb24e-47e3-46aa-b786-fcadf9fb65ce
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Disable Stretch Database and bring back remote data
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md)]


  To disable Stretch Database for a table, select **Stretch** for a table in SQL Server Management Studio. Then select one of the following options.  
  
-   **Disable | Bring data back from Azure**. Copy the remote data for the table from Azure back to SQL Server, then disable Stretch Database for the table. This operation incurs data transfer costs, and it can't be canceled.  
  
-   **Disable | Leave data in Azure**. Disable Stretch Database for the table.  Abandon the remote data for the table in Azure.  
  
 You can also use Transact-SQL to disable Stretch Database for a table or for a database.  
  
 After you disable Stretch Database for a table, data migration stops and query results no longer include results from the remote table.  
  
 If you simply want to pause data migration, see [Pause and resume data migration &#40;Stretch Database&#41;](../../sql-server/stretch-database/pause-and-resume-data-migration-stretch-database.md).  
  
> [!NOTE]
> Disabling Stretch Database for a table or for a database does not delete the remote object. If you want to delete the remote table or the remote database, you have to drop it by using the Azure management portal. The remote objects continue to incur Azure costs until you delete them. For more info, see [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/).  
  
## Disable Stretch Database for a table  
  
### Use SQL Server Management Studio to disable Stretch Database for a table  
  
1.  In SQL Server Management Studio, in Object Explorer, select the table for which you want to disable Stretch Database.  
  
2.  Right-click and select **Stretch**, and then select one of the following options.  
  
    -   **Disable | Bring data back from Azure**. Copy the remote data for the table from Azure back to SQL Server, then disable Stretch Database for the table. This command can't be canceled.  
  
        > [!NOTE]
        > Copying the remote data for the table from Azure back to SQL Server incurs data transfer costs. For more info, see [Data Transfers Pricing Details](https://azure.microsoft.com/pricing/details/data-transfers/).  
  
         After all the remote data has been copied from Azure back to SQL Server, Stretch is disabled for the table.  
  
    -   **Disable | Leave data in Azure**. Disable Stretch Database for the table.  Abandon the remote data for the table in Azure.  
  
    > [!NOTE]
    > Disabling Stretch Database for a table does not delete the remote data or the remote table. If you want to delete the remote table, you have to drop it by using the Azure management portal. The remote table continues to incur Azure costs until you delete it. For more info, see [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/).  
  
### Use Transact-SQL to disable Stretch Database for a table  
  
-   To disable Stretch for a table and copy the remote data for the table from Azure back to SQL Server, run the following command.After all the remote data has been copied from Azure back to SQL Server, Stretch is disabled for the table.

    This command can't be canceled.  
  
    ```sql  
    USE <Stretch-enabled database name>;
    GO
    ALTER TABLE <Stretch-enabled table name>  
       SET ( REMOTE_DATA_ARCHIVE ( MIGRATION_STATE = INBOUND ) ) ; 
    GO 
    ```  
  
    > [!NOTE]
    > Copying the remote data for the table from Azure back to SQL Server incurs data transfer costs. For more info, see [Data Transfers Pricing Details](https://azure.microsoft.com/pricing/details/data-transfers/).    
  
-   To disable Stretch for a table and abandon the remote data, run the following command.  
  
    ```sql  
    USE <Stretch-enabled database name>;
    GO
    ALTER TABLE <Stretch-enabled table name>  
       SET ( REMOTE_DATA_ARCHIVE = OFF_WITHOUT_DATA_RECOVERY ( MIGRATION_STATE = PAUSED ) ) ; 
    GO
    ```  
  
> [!NOTE]
> Disabling Stretch Database for a table does not delete the remote data or the remote table. If you want to delete the remote table, you have to drop it by using the Azure management portal. The remote table continues to incur Azure costs until you delete it. For more info, see [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/).  
  
## Disable Stretch Database for a database  
 Before you can disable Stretch Database for a database, you have to disable Stretch Database on the individual Stretch-enabled tables in the database.  
  
### Use SQL Server Management Studio to disable Stretch Database for a database  
  
1.  In SQL Server Management Studio, in Object Explorer, select the database for which you want to disable Stretch Database.  
  
2.  Right-click and select **Tasks**, and then select **Stretch**, and then select **Disable**.  
  
> [!NOTE]
> Disabling Stretch Database for a database does not delete the remote database. If you want to delete the remote database, you have to drop it by using the Azure management portal. The remote database continues to incur Azure costs until you delete it. For more info, see [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/).  
  
### Use Transact-SQL to disable Stretch Database for a database  
 Run the following command.  
  
```sql  
ALTER DATABASE <Stretch-enabled database name>  
    SET REMOTE_DATA_ARCHIVE = OFF ;  
GO 
```  
  
> [!NOTE]
> Disabling Stretch Database for a database does not delete the remote database. If you want to delete the remote database, you have to drop it by using the Azure management portal. The remote database continues to incur Azure costs until you delete it. For more info, see [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/).  
  
## See Also  
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [Pause and resume data migration &#40;Stretch Database&#41;](../../sql-server/stretch-database/pause-and-resume-data-migration-stretch-database.md)  
  
  
