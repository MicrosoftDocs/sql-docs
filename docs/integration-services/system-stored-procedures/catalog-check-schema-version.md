---
description: "catalog.check_schema_version"
title: "catalog.check_schema_version | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: e0d5e9f5-59c6-4118-87b5-4aa5c37a7df6
author: chugugrace
ms.author: chugu
---
# catalog.check_schema_version 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Determines whether the SSISDB catalog schema and the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] binaries (ISServerExec and SQLCLR assembly) are compatible.  
  
 The ISServerExec.exc logs an error message when the schema and the binaries are incompatible.  
  
 The SSISDB schema version is incremented when the schema changes during the application of patches and during upgrades. It is recommended that you run this stored procedure after an SSISDB backup has been restored to ensure that the schema and binaries are compatible.  
  
## Syntax  
  
```sql  
catalog.check_schema_version [ @use32bitruntime = ] use32bitruntime  
```  
  
## Arguments  
 [ @use32bitruntime= ] *use32bitruntime*  
 When the parameter is set to **1**, the 32-bit version of dtexec is called. The *use32bitruntime* is an **int**.  
  
 
## Return Code Value 0 (success) 

## Result Sets Returns a table that has the following format:

<table>

<thead>

<tr>

<th>Column name</th>

<th>Data type</th>

<th>Description</th>

</tr>

</thead>

<tbody>

<tr>

<td>SERVER_BUILD</td>

<td>**decimal**</td>

<td>SQL Server Version Number. Sample values for a server running SQL Server 2014 is `14.0.3335.7`.</td>

</tr>

<tr>

<td>SCHEMA_VERSION</td>

<td>**tinyint**</td>

<td>SQL Server Version Number. Sample values for servers running SQL Server 2017 are 2019 and `6` and `7`.</td>

</tr>

<tr>

<td>SCHEMA_BUILD</td>

<td>**string (w.x.y.z)**</td>

<td>Schema Build. Sample values for servers running SQL Server 2017 and 2019 are `14.0.3335.7` and `15.0.2000.5`.</td>

</tr>

<tr>

<td>ASSEMBLY_BUILD</td>

<td>**string (w.x.y.z)**</td>

<td>Assembly Build. Sample values for servers running SQL Server 2017 and 2019 are `14.0.3335.0` and `15.0.2000.0`.</td>

</tr>

<tr>

<td>SHARED_COMPONENT_VERSION</td>

<td>**string (w.x.y.z)**</td>

<td>Assembly Build. Sample values for servers running SQL Server 2017 and 2019 are `14.0.3335.7` and `14.0.3335.7`.</td>

</tr>

</tbody>

</table>

 
 
 
  
## Permissions  
 This stored procedure requires the following permission:  
  
-   Membership to the **ssis_admin** database role.  
  
  
