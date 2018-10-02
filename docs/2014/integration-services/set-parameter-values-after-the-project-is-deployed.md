---
title: "Set Parameter Values After the Project Is Deployed | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
ms.assetid: c9dcca4d-f1a0-45ec-b078-f4d372589baf
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Set Parameter Values After the Project Is Deployed
  The Deployment Wizard allows you to set server default parameter values when you deploy your project to the catalog. After your project is in the catalog, you can use SQL Server Management Studio (SSMS) Object Explorer or Transact-SQL to set server default values.  
  
### To set server defaults with SSMS Object Explorer:  
  
1.  Select and right-click the project under the **Integration Services** node.  
  
2.  Click **Properties** to open the **Project Properties** dialog window.  
  
3.  Open the parameters page by clicking **Parameters** under **Select a page**.  
  
4.  Select the desired parameter in the **Parameters** list. Note: The **Container** column helps distinguish project parameters from package parameters.  
  
5.  In the **Value** column, specify the desired server default parameter value.  
  
 To set server defaults with Transact-SQL, use the [catalog.set_object_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-object-parameter-value-ssisdb-database) stored procedure. To view current server defaults, query the [catalog.object_parameters &#40;SSISDB Database&#41;](/sql/integration-services/system-views/catalog-object-parameters-ssisdb-database) view. To clear a server default value, use the [catalog.clear_object_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-clear-object-parameter-value-ssisdb-database) stored procedure.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Parameters](integration-services-ssis-package-and-project-parameters.md)  
  
  
