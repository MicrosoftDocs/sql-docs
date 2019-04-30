---
title: "catalog.configure_catalog (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 72690c61-f462-4c25-9fce-08a687b0bd41
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.configure_catalog (SSISDB Database)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Configures the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog by setting a catalog property to a specified value.  
  
## Syntax  
  
```sql
catalog.configure_catalog [ @property_name = ] property_name , [ @property_value = ] property_value  
```  
  
## Arguments  
 [ @property_name = ] *property_name*  
 The name of the catalog property. The *property_name* is **nvarchar(255)**. For more information about available properties, see [catalog.catalog_properties &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-catalog-properties-ssisdb-database.md).  
  
 [ @property_value = ] *property_value*  
 The value of the catalog property. The *property_value* is **nvarchar(255)**. For more information about property values, see [catalog.catalog_properties &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-catalog-properties-ssisdb-database.md)  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 This stored procedure determines whether the *property_value* is valid for each *property_name*.  
  
 This stored procedure can be performed only when there are no active executions, such as pending, queued, running, and paused executions.  
  
 While the catalog is being configured, all other catalog stored procedures fail with the error message "Server is currently being configured."
  
 When the catalog is configured, an entry is written to the operation log.  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The property does not exist  
  
-   The property value is invalid  
  
  
