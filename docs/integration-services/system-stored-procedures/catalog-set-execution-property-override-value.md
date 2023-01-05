---
description: "catalog.set_execution_property_override_value"
title: "catalog.set_execution_property_override_value | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: 37cb3c01-f4c0-4978-8e40-a975456def5a
author: chugugrace
ms.author: chugu
---
# catalog.set_execution_property_override_value 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Sets the value of a property for an instance of execution in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.set_execution_property_override_value [ @execution_id = execution_id  
    , [ @property_path = ] property_path  
    , [ @property_value = ] property_value  
    , [ @sensitive = ] sensitive  
```  
  
## Arguments  
 [ @execution_id = ] *execution_id*  
 The unique identifier for the instance of execution. The *execution_id* is **bigint**.  
  
 [ @property_path = ] *property_path*  
 The path to the property in the package. The *property_path* is **nvarchar(4000)**.  
  
 [ @property_value = ] *property_value*  
 The override value to assign to the property. The *property_value* is **nvarchar(max)**.  
  
 [ @sensitive = ] *sensitive*  
 When the value is 1, the property is sensitive and is encrypted when it is stored. When the value is 0, the property is not sensitive and the value is stored in plaintext. The *sensitive* argument is **bit**.  
  
## Remarks  
 This procedure performs the same function as the **Property overrides** section in the **Advanced** tab of the **Execute Package** dialog. The path to the property is derived from the **Package Path** property of the package task.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The user does not have the appropriate permissions  
  
-   The execution identifier is not valid  
  
-   The property path is not valid  
  
-   The data type of the property value does not match the data type of the property  
  
## See Also  
 [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md)  
  
  
