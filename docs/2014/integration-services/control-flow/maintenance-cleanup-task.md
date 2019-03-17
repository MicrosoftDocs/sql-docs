---
title: "Maintenance Cleanup Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.maintenancecleanuptask.f1"
helpviewer_keywords: 
  - "deleting files"
  - "removing files"
  - "Maintenance Cleanup task"
ms.assetid: 73ad3cd6-9a6d-44cf-905f-c56aa658bf42
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Maintenance Cleanup Task
  The Maintenance Cleanup task removes files related to maintenance plans, including database backup files and reports created by maintenance plans. For more information, see [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md) and [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).  
  
 By using the Maintenance Cleanup task, a package can remove the backup files or maintenance plan reports on the specified server. The Maintenance Cleanup task includes an option to remove a specific file or remove a group of files in a folder. Optionally you can specify the extension of the files to delete.  
  
 When you configure the Maintenance Cleanup task to remove backup files, the default file name extension is BAK. For report files, the default file name extension is TXT. You can update the extensions to suit your needs; the only limitation is that extensions must be less than 256 characters long.  
  
 Typically, you want to remove old files that are no longer needed, and the Maintenance Cleanup task can be configured to delete files that have reached a specified age. For example, the task can be configured to delete files that are older than four weeks. You can specify the age of files to delete by using days, weeks, months, or years. If you do not specify the minimum age of files to delete, all files of the specified type are deleted.  
  
 In contrast to earlier versions of the Maintenance Cleanup task, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version of the task does not automatically delete files in the subdirectories of the specified directory. This constraint reduces the surface area of any attack that could exploit the functionality of the Maintenance Cleanup task to delete files maliciously. To delete the first-level subfolders, you must explicitly elect to do this by checking the **Include first-level subfolders** option in the **Maintenance Cleanup Task** dialog box.  
  
## Configuration of the Maintenance Cleanup Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This task is in the **Maintenance Plan Tasks** section of the **Toolbox** in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Maintenance Cleanup Task &#40;Maintenance Plan&#41;](../../relational-databases/maintenance-plans/maintenance-cleanup-task-maintenance-plan.md)  
  
## Related Tasks  
 For details about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md).  
  
## See Also  
 [Integration Services Tasks](integration-services-tasks.md)   
 [Control Flow](control-flow.md)  
  
  
