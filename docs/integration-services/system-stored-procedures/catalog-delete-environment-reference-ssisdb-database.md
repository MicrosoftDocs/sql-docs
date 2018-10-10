---
title: "catalog.delete_environment_reference (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 1f68f157-c4e9-412c-92b3-53a2faaba29b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.delete_environment_reference (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Deletes an environment reference from a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
delete_environment_reference [ @reference_id = ] reference_id  
```  
  
## Arguments  
 [ @reference_id = ] *reference_id*  
 The unique identifier of the environment reference. The *reference_id* is **bigint**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   MODIFY permission on the project  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The environment reference identifier is not valid  
  
-   The user does not have the appropriate permissions  
  
  
