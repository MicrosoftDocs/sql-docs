---
title: "PowerShell Cmdlet for Migration Evaluation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 117250d3-9982-47fe-94fd-6f29f6159940
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# PowerShell Cmdlet for Migration Evaluation
  The Save-SqlMigrationReport cmdlet is a tool that evaluates the migration fitness of multiple objects in a SQL Server database. Currently, it is limited to evaluating the migration fitness for In-Memory OLTP. The cmdlet can run in both an elevated Windows PowerShell environment and sqlps.  
  
## Syntax  
  
```powershell  
Save-SqlMigrationReport [ -MigrationType OLTP ] [ -Server server -Database database [ -Object object_name ] ]  |  [ -InputObject smo_object ] -FolderPath path  
```  
  
#### Parameters  
 The following table describers the parameters.  
  
|Parameters|Description|  
|----------------|-----------------|  
|MigrationType|The type of migration scenario the cmdlet is targeting. Currently the only value is the default OLTP. Optional.|  
|Server|The name of the target SQL Server instance. Mandatory in Windows Powershell environment if -InputObject parameter is not supplied. Optional in SQLPS.|  
|Database|The name of the target SQL Server database. Mandatory in Windows Powershell environment if -InputObject parameter is not supplied. Optional in SQLPS.|  
|Object|The name of the target database object. Can be table or stored procedure.|  
|InputObject|The SMO object the cmdlet should target. Mandatory in Windows Powershell environment if -Server and -Database are not supplied. Optional in SQLPS.|  
|FolderPath|The folder in which the cmdlet should deposit the generated reports. Required.|  
  
## Results  
 In the folder specified in the -FolderPath parameter, there will be two folder names: Tables and Stored Procedures. If the targeted object is a table, its report will be inside the Tables folder. Otherwise it will be inside the Stored Procedures folder.  
  
  