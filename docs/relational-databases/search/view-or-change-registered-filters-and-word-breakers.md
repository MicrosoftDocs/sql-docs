---
title: "View or Change Registered Filters and Word Breakers | Microsoft Docs"
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text search [SQL Server], word breakers"
  - "full-text search [SQL Server], filters"
  - "filters [full-text search]"
  - "word breakers [full-text search]"
ms.assetid: f88c54df-b1aa-4701-807f-dc92c32363fd
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View or Change Registered Filters and Word Breakers
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  After any word breakers or filters are installed or uninstalled on a system, the changes do not automatically take effect on server instances. This topic describes how to view the currently registered word breaker or filters and how to register newly installed word breakers and filters on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### To view a list of languages whose word breakers are currently registered  
  
1.  Use the [sys.fulltext_languages](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md) catalog view, as follows:  
  
    ```  
    SELECT * FROM sys.fulltext_languages;   
    ```  
  
### To view a list of the filters that are currently registered  
  
1.  Use the [sp_help_fulltext_system_components](../../relational-databases/system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md) system stored procedure, as follows:  
  
    ```  
    EXEC sp_help_fulltext_system_components 'filter';    
    ```  
  
### To register newly installed word breakers and filters  
  
1.  Use the [sp_fulltext_service](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md) system stored procedure to update the list of languages, as follows:  
  
    ```  
    exec sp_fulltext_service 'update_languages';   
    ```  
  
### To unregister uninstalled word breakers and filters  
  
1.  Use the **sp_fulltext_service** to update the list of languages, as follows:  
  
    ```  
    exec sp_fulltext_service 'update_languages'  
    ```  
  
2.  Use the **sp_fulltext_service** to restart the filter daemon host processes (fdhost.exe), as follows:  
  
    ```  
    exec sp_fulltext_service 'restart_all_fdhosts';  
    ```  
  
### To replace existing word breakers or filters when installing new ones  
  
1.  When preparing to install a DLL file that contains new word breakers or filters, verify that it has a different filename from any of the existing DLL files installed on your server instance.  
  
2.  Copy the new DLL file into the directory containing the standard [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] DLL files for the server instance. The default location is:  
  
     C:\Program Files\Microsoft SQL Server\MSSQL.*instance_name*\MSSQL\Binn  
  
    > [!IMPORTANT]  
    >  We highly recommend that you load only signed and verified components. Also, we recommend that you run the FDHOST Launcher (MSSQLFDLauncher) Service with the least possible privileges.  
  
3.  Install the new word breaker or filters.  
  
     **To install and load Microsoft Filter Pack IFilters**  
  
    -   [How to register Microsoft Filter Pack IFilters with SQL Server](https://go.microsoft.com/fwlink/?LinkId=130439)  
  
4.  Use **sp_fulltext_service** to load newly installed word breakers and filters in the server instance, as follows:  
  
    ```  
    EXEC sp_fulltext_service @action='load_os_resources', @value=1;  
    ```  
  
5.  Use **sp_fulltext_service** to update the list of languages, as follows:  
  
    ```  
    EXEC sp_fulltext_service 'update_languages';  
    ```  
  
6.  Restart the filter daemon host processes (fdhost.exe), using **sp_fulltext_service** as follows:  
  
    ```  
    EXEC sp_fulltext_service 'restart_all_fdhosts';   
    ```  
  
## See Also  
 [Set the Service Account for the Full-text Filter Daemon Launcher](../../relational-databases/search/set-the-service-account-for-the-full-text-filter-daemon-launcher.md)   
 [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md)   
 [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md)  
  
  
