---
title: "Choose a Data Source (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.chooseadatasource.f1"
ms.assetid: ebf28a62-dfc1-4b39-9db5-df1919e5fccb
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Choose a Data Source (SQL Server Import and Export Wizard)
  Use the **Choose a Data Source** page to specify the source of the data that you want to copy.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, and about the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
 **Data Source**  
 Choose the data provider that matches the data storage format of the source. There may be more than one provider available for your data source. For example, with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, the .NET Framework Data Provider for SQL Server, or the Microsoft OLE DB Provider for SQL Server.  
  
 The **Data Source** property has a variable number of options, which depend on the providers installed on the computer. The following tables list the options for some frequently used destinations. For other providers, see the provider-specific documentation.  
  
## Dynamic Options  
 The following sections show the options available for several data sources. Not all the data sources that are available in the Data Source drop-down are listed here.  
  
### Data Source = SQL Server Native Client and Microsoft OLE DB Provider for SQL Server  
 **Server name**  
 Type the name of the server that contains the data, or choose a server from the list.  
  
 **Use Windows Authentication**  
 Specify whether the package should use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication to log in to the database. Windows Authentication is recommended for better security.  
  
 **Use SQL Server Authentication**  
 Specify whether the package should use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication to log in to the database. If you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must provide a user name and password.  
  
 **User name**  
 Specify a user name for the database connection when you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 **Password**  
 Provide the password for the database connection when you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 **Database**  
 Select from the list of databases on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Refresh**  
 Restore the list of available databases by clicking **Refresh**.  
  
### Data Source = .NET Framework Data Provider for SQL Server  
 This page presents an alphabetical list of options for the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The most important options are listed in the following table.  
  
 **Data Source**  
 Type the name of the server that contains the data, or choose a server from the list.  
  
 **Initial Catalog**  
 Type the name of the source database.  
  
 **Integrated Security**  
 Specify `True` to connect by using Windows integrated authentication, which is recommended, or `False` to connect by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. If you specify `False`, you must enter a user ID and password. The default value is `False`.  
  
 **User ID**  
 Specify a user name for the database connection when you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 **Password**  
 Provide the password for the database connection when you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 The additional options that are listed when you select this provider are not required to connect successfully to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source database. For a description of these additional options, see the documentation for the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Software Development Kit.  
  
### Data Source = Microsoft Excel  
  
> [!NOTE]  
>  Select **Microsoft Excel** only if you want to connect to a data source that uses Excel 2003 or earlier. To connect to a data source that uses Excel 2007, select **Microsoft Office 12.0 Access Database Engine OLE DB Provider**, click **Properties**, and then on the **All** tab of the **Data Link Properties** dialog box, enter `Excel 12.0` as the value for **Extended Properties**.  
  
 **Excel file path**  
 Specify the path and file name for the spreadsheet from which to import the data. For example, **C:\MyData.xls, \\\Sales\Database\Northwind.xls**. Or, click **Browse**.  
  
 **Browse**  
 Locate the spreadsheet by using the **Open** dialog box.  
  
 **Excel version**  
 Select the version of Excel that the source data is stored in.  
  
> [!NOTE]  
>  When you import data from a [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] source, the wizard uses the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Excel Source component. For information about usage considerations and known issues, see [Excel Source](../data-flow/excel-source.md).  
  
### Data Source = Microsoft Access  
  
> [!NOTE]  
>  Select **Microsoft Access** only if you want to connect to a database that uses Access 2003 or earlier. To connect to a database that uses Access 2007, select **Microsoft Office 12.0 Access Database Engine OLE DB Provider** instead.  
  
 **File name**  
 Specify the path and file name for the database file from which to import the data. For example, **C:\MyData.mdb, \\\Sales\Database\Northwind.mdb**. Or, click **Browse**.  
  
 **Browse**  
 Locate the database file by using the **Open** dialog box.  
  
 **User name**  
 Specify a valid user name for the database connection when a workgroup information file is associated with the database.  
  
 **Password**  
 Provide the user's password for the database connection when a workgroup information file is associated with the database. However, if the database is protected with a single password for all users, you must provide this value in the **Data Link Properties** dialog box, which is accessed by clicking **Advanced**.  
  
 **Advanced**  
 You may want to specify advanced options, such as the database password or a non-default workgroup information file, by using the **Data Link Properties** dialog box. For more information about OLE DB provider properties, search in the Data Access section of the [MSDN library](https://go.microsoft.com/fwlink/?linkid=62553).  
  
### Data Source = Flat File Source  
 See the following topics for information about the options for a flat file data source.  
  
 [Flat File Connection Manager Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
 [Flat File Connection Manager Editor &#40;Columns Page&#41;](../flat-file-connection-manager-editor-columns-page.md)  
  
 [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../flat-file-connection-manager-editor-advanced-page.md)  
  
 [Flat File Connection Manager Editor &#40;Preview Page&#41;](../flat-file-connection-manager-editor-preview-page.md)  
  
  
