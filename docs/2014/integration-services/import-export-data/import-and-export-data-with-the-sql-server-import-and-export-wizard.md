---
title: "SQL Server Import and Export Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "exporting data"
  - "mapping files [Integration Services]"
  - "SQL Server Import and Export Wizard"
  - "SSIS packages, creating"
  - "packages [Integration Services], copying"
  - "Integration Services packages, creating"
  - "packages [Integration Services], creating"
  - "SQL Server Integration Services packages, creating"
  - "Import and Export Wizard"
  - "copying data [Integration Services]"
  - "importing data, SSIS packages"
  - "sources [Integration Services], copying data"
ms.assetid: c0e4d867-b2a9-4b2a-844b-2fe45be88f81
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# SQL Server Import and Export Wizard
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard offers the simplest method to create a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that copies data from a source to a destination.  
  
> [!NOTE]  
>  On a 64-bit computer, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installs the 64-bit version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard (DTSWizard.exe). However, some data sources, such as Access or Excel, only have a 32-bit provider available. To work with these data sources, you might have to install and run the 32-bit version of the wizard. To install the 32-bit version of the wizard, select either Client Tools or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] during setup.  
  
 You can start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard from the Start menu, from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], or at the command prompt. For more information, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard can copy data to and from any data source for which a managed [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider or a native OLE DB provider is available. The list of available providers includes the following data sources:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
-   Flat files  
  
-   Microsoft Office Access  
  
-   Microsoft Office Excel  
  
 Some wizard features work differently, depending on the environment in which you start the wizard:  
  
-   If you start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you run the package immediately by selecting the **Execute immediately** check box. By default, this check box is selected and the package runs immediately.  
  
     You can also decide whether to save the package to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or to the file system. If you select to save the package, you must also specify a package protection level. For more information about package protection levels, see [Access Control for Sensitive Data in Packages](../security/access-control-for-sensitive-data-in-packages.md).  
  
     After the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard has created the package and copied the data, you can use the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer to open and change the saved package by adding tasks, transformations, and event-driven logic.  
  
    > [!NOTE]  
    >  In [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], the option to save the package created by the wizard is not available.  
  
-   If you start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard from an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the package cannot be run as a step in completing the wizard. Instead, the package is added to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project from which you started the wizard. You can then run the package or extend it by adding tasks, transformations, and event-driven logic by using [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For more information, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
## Permissions Required by the Import and Export Wizard  
 To complete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard successfully, you must have at least the following permissions:  
  
-   Permissions to connect to the source and destination databases or file shares. In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], this requires server and database login rights.  
  
-   Permission to read data from the source database or file. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this requires SELECT permissions on the source tables and views.  
  
-   Permissions to write data to the destination database or file. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this requires INSERT permissions on the destination tables.  
  
-   If you want to create a new destination database or table or file, permissions sufficient to create the new database or table or file. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this requires CREATE DATABASE or CREATE TABLE permissions.  
  
-   If you want to save the package created by the wizard, permissions sufficient to write to the msdb database or to the file system. In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], this requires INSERT permissions on the msdb database.  
  
## Mapping Data Types in the Import and Export Wizard  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard provides minimal transformation capabilities. Except for setting the name, the data type, and the data type properties of columns in new destination tables and files, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard supports no column-level transformations.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard uses the mapping files that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides to map data types from one database version or system to another. For example, it can map from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to Oracle. By default, the mapping files in XML format are installed to C:\Program Files\Microsoft SQL Server\100\DTS\MappingFiles. If your business requires different mappings between data types, you can update the mappings to affect the mappings that the wizard performs. For example, if you want the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **nchar** data type to map to the DB2 **GRAPHIC** data type instead of the DB2 **VARGRAPHIC** data type when transferring data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to DB2, you change the **nchar** mapping in the SqlClientToIBMDB2.xml mapping file to use **GRAPHIC** instead of **VARGRAPHIC.**  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes mappings between many commonly used source and destination combinations, and you can add new mapping files to the Mapping Files directory to support additional sources and destinations. The new mapping files must conform to the published XSD schema and map between a unique combination of source and destination.  
  
> [!NOTE]  
>  If you edit an existing mapping file, or add a new mapping file to the folder, you must close and reopen the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] for the new or changed files to be recognized.  
  
## External Resources  
  
-   Video, [Exporting SQL Server Data to Excel (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkID=200975), on technet.microsoft.com  
  
-   CodePlex sample, [Exporting from ODBC to a Flat File Using a Wizard Tutorial: Lesson Packages](https://go.microsoft.com/fwlink/?LinkId=217657), on msftisprodsamples.codeplex.com  
  
  
