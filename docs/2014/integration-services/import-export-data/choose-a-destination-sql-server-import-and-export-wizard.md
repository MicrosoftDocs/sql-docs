---
title: "Choose a Destination (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.chooseadestination.f1"
ms.assetid: 1898be15-3e69-42d3-8ecb-3733c9f6c8e3
author: janinezhang
ms.author: janinez
manager: craigg
---
# Choose a Destination (SQL Server Import and Export Wizard)
  Use the **Choose a Destination** page to specify the destination of the data that you want to copy.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, as well as the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Static Options  
 **Destination**  
 Choose the data provider that matches the data storage format of the destination. There may be more than one provider available for your data source. For example, with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, the .NET Framework Data Provider for SQL Server, or the Microsoft OLE DB Provider for SQL Server.  
  
> [!NOTE]  
>  To save data to an ODBC destination, select the .NET Framework Data Provider for ODBC.  
  
 The **Data Source** property has a variable number of options, which change depending on the providers installed on the computer. The following tables list the options for some commonly used destinations. For other providers, see the provider-specific documentation.  
  
## Dynamic Options  
 The following sections show the options available for several data sources. Not all the destinations that are available in the Destination drop-down are listed here.  
  
### Destination = SQL Server Native Client or Microsoft OLE DB Provider for SQL Server  
 **Server name**  
 Type the name of the server to receive the data, or choose a server from the list.  
  
 **Use Windows Authentication**  
 Specify whether the package should use Microsoft Windows Authentication to log in to the database. Windows Authentication is recommended for better security.  
  
 **Use SQL Server Authentication**  
 Specify whether the package should use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication to log in to the database. If you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must provide a user name and password.  
  
 **User name**  
 Specify a user name for the database connection when you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 **Password**  
 Provide the password for the database connection when you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 **Database**  
 Select from the list of databases on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or create a new database by clicking **New**.  
  
 **Refresh**  
 Restore the list of available databases by clicking **Refresh**.  
  
 **New**  
 Create a new destination database by using the **Create Database** dialog box.  
  
### Destination = Flat File Destination  
 **File name**  
 Specify the path and file name for the file in which to store the data. Or, click **Browse** to locate a file.  
  
 **Browse**  
 Locate a file by using the **Open** dialog box.  
  
 **Locale**  
 Specify the locale ID (LCID) that defines character sort orders and date and time formatting.  
  
 **Unicode**  
 Indicate whether to use Unicode. If you use Unicode, you do not have to specify a code page.  
  
 **Code page**  
 Specify the code page for the language you want to use.  
  
 **Format**  
 Indicate whether to use delimited, fixed width, or ragged right formatting.  
  
|Value|Description|  
|-----------|-----------------|  
|Delimited|Columns are separated by a delimiter, specified on the **Columns** page.|  
|Fixed width|Columns have a fixed width.|  
|Ragged right|Ragged right files are files in which every column has a fixed width, except for the last column, which is delimited by the row delimiter.|  
  
 **Text qualifier**  
 Type the text qualifier to use. For example, you can specify that each text column be surrounded with quotation marks.  
  
 **Column names in first data row**  
 Indicate whether you want to display column names in the first data row.  
  
### Destination = Microsoft Excel  
  
> [!NOTE]  
>  Select **Microsoft Excel** only if you want to connect to a data source that uses Excel 2003 or earlier. To connect to a data source that uses Excel 2007, select **Microsoft Office 12.0 Access Database Engine OLE DB Provider**, click **Properties**, and then on the **All** tab of the **Data Link Properties** dialog box, for **Extended Properties**, enter `Excel 12.0`.  
  
 **Excel file path**  
 Specify the path and file name for the workbook in which to store the data (for example, C:\MyData.xls, \\\Sales\Database\Northwind.xls). Or, click **Browse** to locate a workbook.  
  
 **Browse**  
 Locate an Excel workbook by using the **Open** dialog box.  
  
 **Excel version**  
 Select the version of Excel that is used by the destination workbook.  
  
> [!NOTE]  
>  When you export data to a [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] destination, the wizard uses the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Excel Destination component. For information on some usage considerations and known issues, see [Excel Destination](../data-flow/excel-destination.md).  
  
### Destination = Microsoft Access  
  
> [!NOTE]  
>  Select **Microsoft Access** only if you want to connect to a database that uses Access 2003 or earlier. To connect to a database that uses Access 2007, select **Microsoft Office 12.0 Access Database Engine OLE DB Provider**.  
  
 **File name**  
 Specify the path and file name for the database file in which to store the data (for example, C:\MyData.mdb, \\\Sales\Database\Northwind.mdb). Or, click **Browse** to locate a database file.  
  
 **Browse**  
 Browse to the database file by using the **Open** dialog box.  
  
 **User name**  
 Specify a valid user name for the database connection when a workgroup information file is associated with the database.  
  
 **Password**  
 Provide the user's password for the database connection when a workgroup information file is associated with the database. However, if the database is protected with a single password for all users, you must provide this value in the **Data Link Properties** dialog box, which is accessed from the **Advanced** button.  
  
 **Advanced**  
 Specify advanced options, such as the database password or a non-default workgroup information file, by using the **Data Link Properties** dialog box. For more information about OLE DB provider properties, search in the Data Access section of the [MSDN Library](https://go.microsoft.com/fwlink/?linkid=62553).  
  
  
