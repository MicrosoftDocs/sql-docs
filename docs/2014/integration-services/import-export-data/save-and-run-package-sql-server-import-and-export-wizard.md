---
title: "Save and Execute Package (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.saveschedule.f1"
ms.assetid: b582c462-3d7a-4a4c-a2a2-2c79fedab75a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Save and Execute Package (SQL Server Import and Export Wizard)
  Use the **Save and Execute Package** dialog box to run the package immediately, save it to run later, or both.  
  
> [!NOTE]  
>  If you stop a package before it finishes running, the package is not saved, even if you selected the **Save** check box.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, as well as the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the SQL Server Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
 **Execute immediately**  
 Select this option to run the package immediately.  
  
 **Save SSIS Package**  
 Save the package to run later, with the option to run it immediately.  
  
> [!NOTE]  
>  In [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], the option to save the package created by the wizard is not available.  
  
 **SQL Server**  
 Select this option to save the package to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] `msdb` database.  
  
> [!NOTE]  
>  This option is available only if you have selected the **Save SSIS Package** option.  
  
 **File system**  
 Select this option to save the package as a file that has the .dtsx extension.  
  
> [!NOTE]  
>  This option is available only if you have selected the **Save SSIS Package** option.  
  
 **Package protection level**  
 Select a protection level from the list.  
  
 The protection level determines the protection method, the password or user key, and the scope of package protection. Protection can include all data or sensitive data only. To understand the requirements and options for package security, see [Access Control for Sensitive Data in Packages](../security/access-control-for-sensitive-data-in-packages.md) and [Security Overview &#40;Integration Services&#41;](../security/security-overview-integration-services.md).  
  
> [!NOTE]  
>  This option is available only if you have selected the **Save SSIS Package** option.  
  
 **Password**  
 Type a password.  
  
> [!NOTE]  
>  This option is available only if you have set the **Package protection level** option to either **Encrypt sensitive data with password** or **Encrypt all data with password**.  
  
 **Retype password**  
 Type the password again.  
  
> [!NOTE]  
>  This option is available only if you have set the **Package protection level** option to either **Encrypt sensitive data with password** or **Encrypt all data with password**.  
  
## See Also  
 [Execution of Projects and Packages](../packages/run-integration-services-ssis-packages.md)   
 [Save Packages](../save-packages.md)  
  
  
