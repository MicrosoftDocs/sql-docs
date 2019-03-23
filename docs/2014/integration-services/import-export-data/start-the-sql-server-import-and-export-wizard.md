---
title: "Run the SQL Server Import and Export Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Import and Export Wizard"
  - "starting SQL Server Import and Export Wizard"
  - "Import and Export Wizard"
  - "starting Import and Export Wizard"
ms.assetid: 5fc4f6d1-1f6f-444e-9aeb-827f85e1c405
author: janinezhang
ms.author: janinez
manager: craigg
---
# Run the SQL Server Import and Export Wizard
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard provides the simplest method of copying data between data sources and of constructing basic packages. For more information about the wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md).  
  
 For a video that demonstrates how to use the SQL Server Import and Export Wizard to create a package that exports data from a SQL Server database to a Microsoft Excel spreadsheet, see [Exporting SQL Server Data to Excel (SQL Server Video)](https://go.microsoft.com/fwlink/?LinkId=131024).  
  
### To start the SQL Server Import and Export Wizard  
  
-   On the **Start** menu, point to **All Programs**, point to**Microsoft SQL Server** , and then click **Import and Export Data**.  
  
     -or-  
  
     In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], right-click the **SSIS Packages** folder, and then click **SSISImport and Export Wizard**.  
  
     -or-  
  
     In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], on the **Project** menu, click **SSISImport and Export Wizard**.  
  
     -or-  
  
     In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] server type, expand Databases, right-click a database, point to **Tasks**, and then click **Import Data** or **Export data**.  
  
     -or-  
  
     In a command prompt window, run DTSWizard.exe, located in C:\Program Files\Microsoft SQL Server\100\DTS\Binn.  
  
    > [!NOTE]  
    >  On a 64-bit computer, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installs the 64-bit version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard (DTSWizard.exe). However, some data sources, such as Access or Excel, only have a 32-bit provider available. To work with these data sources, you might have to install and run the 32-bit version of the wizard. To install the 32-bit version of the wizard, select either Client Tools or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] during setup.  
  
### To import or export data by using the SQL Server Import and Export Wizard  
  
1.  Start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard.  
  
2.  On the corresponding wizard pages, select a data source and a data destination.  
  
     The available data sources include [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers, OLE DB providers, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client providers, [!INCLUDE[vstecado](../../includes/vstecado-md.md)] providers, Microsoft Office Excel, Microsoft Office Access, and the Flat File source. Depending on the source, you set options such as the authentication mode, server name, database name, and file format.  
  
    > [!NOTE]  
    >  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Oracle does not support the Oracle BLOB, CLOB, NCLOB, BFILE, and UROWID data types. Therefore, the OLE DB source cannot extract data from tables that contain columns with these data types.  
  
     The available data destinations include [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers, OLE DB providers, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, Excel, Access, and the Flat File destination.  
  
3.  Set the options for the type of destination that you selected.  
  
     If the destination is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database you can specify the following:  
  
    -   Indicate whether to create a new database and set the database properties. The following properties cannot be configured and the wizard uses the specified default values:  
  
        |Property|Value|  
        |--------------|-----------|  
        |Collation|Latin1_General_CS_AS_KS_WS|  
        |Recovery model|Full|  
        |Use full-text indexing|True|  
  
    -   Select whether to copy data from tables or views, or to copy query results.  
  
         If you want to query the source data and copy the results, you can construct a Transact-SQL query. You can enter the Transact-SQL query manually or use a query saved to a file. The wizard includes a browse feature for locating the file, and the wizard automatically opens the file and pastes its content into the wizard page when you select the file.  
  
         If the source is an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] provider you can also use the option to copy query results, providing the DBCommand string as the query.  
  
         If the source data is a view, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard automatically converts the view to a table in the destination.  
  
    -   Indicate whether the destination table is dropped and then re-created, and whether to enable identity inserts.  
  
    -   Indicate whether to delete rows or append rows in an existing destination table. If the table does not exist, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard automatically creates it.  
  
     If the destination is a Flat File destination you can specify the following:  
  
    -   Specify the row delimiter in the destination file.  
  
    -   Specify the column delimiter in the destination file.  
  
4.  (Optional) Select one table and change the mappings between source and destination columns, or change the metadata of destination columns:  
  
    -   Map source columns to different destination columns.  
  
    -   Change the data type in the destination column.  
  
    -   Set the length of columns with character data types.  
  
    -   Set the precision and scale of columns with numeric data types.  
  
    -   Specify whether the column can contain null values.  
  
5.  (Optional) Select multiple tables, and update the metadata and options to apply to those tables:  
  
    -   Select an existing destination schema or provide a new schema to which to assign tables.  
  
    -   Specify whether to enable identity inserts in destination tables.  
  
    -   Specify whether to drop and re-create destination tables.  
  
    -   Specify whether to truncate existing destination tables.  
  
6.  Save and run a package.  
  
     If the wizard is started from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the command prompt, the package can run immediately. You can optionally save the package to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **msdb** database or to the file system. For more information about the **msdb** database, see [Package Management &#40;SSIS Service&#41;](../service/package-management-ssis-service.md).  
  
     When you save the package you can set the package protection level, and if the protection level uses a password, provide the password. For more information about package protection levels, see [Access Control for Sensitive Data in Packages](../security/access-control-for-sensitive-data-in-packages.md).  
  
     If the wizard is started from an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you cannot run the package from the wizard. Instead, the package is added to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project from which you started the wizard. You can then run the package in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
    > [!NOTE]  
    >  In [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], the option to save the package created by the wizard is not available.  
  
## See Also  
 [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md)   
 [Create Packages in SQL Server Data Tools](../create-packages-in-sql-server-data-tools.md)  
  
  
