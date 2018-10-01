---
title: "sp_help_fulltext_system_components (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_fulltext_components_TSQL"
  - "sp_help_fulltext_components"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_fulltext_system_components"
ms.assetid: ac1fc7a0-7f46-4a12-8c5c-8d378226a8ce
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_help_fulltext_system_components (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)]

  Returns information for the registered word-breakers, filter, and protocol handlers. **sp_help_fulltext_system_components** also returns a list of identifiers of databases and full-text catalogs that have used the specified component.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_fulltext_system_components   
         { 'all'| [ @component_type = ] 'component_type' }  
    , [ @param = ] 'param'  
```  
  
## Arguments  
 'all'  
 Returns information for all full-text components.  
  
 [ **@component_type=** ] *component_type*  
 Specifies the type of component. *component_type* can be one of the following:  
  
-   **wordbreaker**  
  
-   **filter**  
  
-   **protocol handler**  
  
-   **fullpath**  
  
 If a full path is specified, *param* must also be specified with the full path to the component DLL, or an error message is returned.  
  
 [ **@param=** ] *param*  
 Depending on component type, this is one of the following: a locale identifier (LCID), the file extension with "." prefix, the full component name of the protocol handler, or the full path to the component DLL.  
  
## Return Code Values  
 0 (success) or (1) failure  
  
## Result Sets  
 The following result set is returned for the system components.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**componenttype**|**sysname**|Type of component. One of the following:<br /><br /> filter<br /><br /> protocol handler<br /><br /> wordbreaker|  
|**componentname**|**sysname**|Name of the component.|  
|**clsid**|**uniqueidentifier**|Class identifier of the component.|  
|**fullpath**|**nvarchar(256)**|Path to the location of the component.<br /><br /> NULL = Caller not a member of **serveradmin** fixed server role.|  
|**version**|**nvarchar(30)**|Version of the component.|  
|**manufacturer**|**sysname**|Name of the manufacturer of the component.|  
  
 The following result set is returned only if one or more than one full-text catalog exists that uses *component_type*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**dbid**|**int**|ID of the database.|  
|**ftcatid**|**int**|ID of the full-text catalog.|  
  
## Permissions  
 Requires membership in the **public** role; however, users can only see information about the full-text catalogs for which they have VIEW DEFINITION permission. Only members of the **serveradmin** fixed server role can see values in the **fullpath** column.  
  
## Remarks  
 This method is of particular importance when preparing for an upgrade. Execute the stored procedure within a particular database, and use the output to determine whether a particular catalog will be impacted by the upgrade.  
  
## Examples  
  
### A. Listing all full-text system components  
 The following example lists all of the full-text system components that have been registered on the server instance.  
  
```  
EXEC sp_help_fulltext_system_components 'all';  
GO  
```  
  
### B. Listing word breakers  
 The following example lists all the word breakers registered on the service instance.  
  
```  
EXEC sp_help_fulltext_system_components 'wordbreaker';  
GO  
```  
  
### C. Determining whether a specific word breaker is registered  
 The following example will list the word breaker for the Turkish language (LCID = 1055) if it has been installed on the system and registered on the service instance. This example specifies the parameter names, **@component_type** and **@param**.  
  
```  
EXEC sp_help_fulltext_system_components @component_type = 'wordbreaker', @param = 1055;  
GO  
```  
  
 By default, this word breaker is not installed, so the result set is empty.  
  
### D. Determining whether a specific filter has been registered  
 The following example lists the filter for the .xdoc component if it has been manually installed on the system and registered on the server instance.  
  
```  
EXEC sp_help_fulltext_system_components 'filter', '.xdoc';  
GO  
```  
  
 By default, this filter is not installed, so the result set is empty.  
  
### E. Listing a specific .dll file  
 The following example lists a specific .ddl file, `nlhtml.dll`, which is installed by default.  
  
```  
EXEC sp_help_fulltext_system_components 'fullpath',   
   'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\nlhtml.dll';  
GO  
  
```  
  
## See Also  
 [View or Change Registered Filters and Word Breakers](../../relational-databases/search/view-or-change-registered-filters-and-word-breakers.md)   
 [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md)   
 [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md)   
 [Full-Text Search and Semantic Search Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/full-text-search-and-semantic-search-stored-procedures-transact-sql.md)  
  
  
