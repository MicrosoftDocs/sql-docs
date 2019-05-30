---
title: "Connect to an Access Data Source (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/20/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: b44c159a-c33d-4f3c-bdb8-9832f35317c8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Connect to an Access Data Source (SQL Server Import and Export Wizard)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


This topic shows you how to connect to a **Microsoft Access** data source from the **Choose a Data Source** or **Choose a Destination** page of the SQL Server Import and Export Wizard.

The following screen shot shows a sample connection to a Microsoft Access database. In this example, you don't have to enter a user name and password, because the target database doesn't use a workgroup information file.

![Connect to Access](../../integration-services/import-export-data/media/connect-to-access.jpg)

## Options to specify

> [!NOTE]
> The connection options for this data provider are the same whether Access is your source or your destination. That is, the options you see are the same on both the **Choose a Data Source** and the **Choose a Destination** pages of the wizard.

**Data source**  
The list of data providers may contain several entries for Microsoft Access. Select the latest installed version, or the version that corresponds to the version of Access that created the database file.

|Data source|Office version|
|-------|-------|
|Microsoft Access (Microsoft.ACE.OLEDB.16.0)|Office 2016|
|Microsoft Access (Microsoft.ACE.OLEDB.15.0)|Office 2013|
|Microsoft Access (Microsoft Access Database Engine)|Office 2010 and Office 2007|
|Microsoft Access (Microsoft Jet Database Engine)|Office versions earlier than Office 2007|

> [!IMPORTANT]
> You may have to download and install additional files to connect to Access databases. See [Get the files you need to connect to Access](#officeDownloads) on this page for more info.

 **File name**  
Specify the path and file name for the Access file. For example, **C:\\MyData.mdb** for a file on the local computer, or **\\\\Sales\\Database\\Northwind.mdb** for a file on a network share. Or, click **Browse**. 

> [!NOTE]
> If you click **Browse** to locate the Access file, the **Open** dialog box filters for files with the older .MDB format and file extension by default. However the data provider can also open files with the newer .ACCDB format and file extension.
  
 **Browse**  
 Locate the database file by using the **Open** dialog box.  
  
 **User name**  
If a workgroup information file is associated with the database, provide a valid user name.  
  
 **Password**  
If a workgroup information file is associated with the database, provide the user's password here.
 
If the database is protected with a single password for all users, see [Is the database file password-protected?](#database_password).
  
 **Advanced**  
Specify advanced options, such as the database password or a non-default workgroup information file, in the **Data Link Properties** dialog box.  

## I don't see Access in the list of data sources
If you don't see Access in the list of data sources, are you running the 64-bit wizard? The providers for Excel and Access are typically 32-bit and aren't visible in the 64-bit wizard. Run the 32-bit wizard instead.

> [!NOTE]
> To use the 64-bit version of the SQL Server Import and Export Wizard, you have to install SQL Server. SQL Server Data Tools (SSDT) and SQL Server Management Studio (SSMS) are 32-bit applications and only install 32-bit files, including the 32-bit version of the wizard.

## <a name="officeDownloads"></a>Get the files you need to connect to Access  
You may have to download the connectivity components for Microsoft Office data sources, including Access and Excel, if they're not already installed. Download the latest version of the connectivity components for both Access and Excel files here:
[Microsoft Access Database Engine 2016 Redistributable](https://www.microsoft.com/download/details.aspx?id=54920).
  
The latest version of the components can open files created by earlier versions of Access.

If the computer has a 32-bit version of Office, then you have to install the 32-bit version of the components, and you also have to ensure that you run the package in 32-bit mode.

If you have an Office 365 subscription, make sure that you download the Access Database Engine 2016 Redistributable and not the Microsoft Access 2016 Runtime. When you run the installer, you may see an error message that you can't install the download side-by-side with Office click-to-run components. To bypass this error message, run the installation in quiet mode by opening a Command Prompt window and running the .EXE file that you downloaded with the `/quiet` switch. For example:

`C:\Users\<user name>\Downloads\AccessDatabaseEngine.exe /quiet`

## <a name="database_password"></a> Is the database file password-protected?
In some cases, an Access database is password-protected, but isn't using a workgroup information file. All users have to provide the same password, but don't have to enter a user name. To provide a database password, do the following.

1.  On the **Choose a Data Source** or **Choose a Destination** page, click the **Advanced** button to open the **Data Link Properties** dialog box.  
2.  In the **Data Link Properties** dialog box, select the **All** tab.  
3.  In the list of properties and values, select **Jet OLEDB:Database Password**.   
    
    ![Specify Access password, screen 1](../../integration-services/import-export-data/media/specify-access-password-screen-1.jpg) 
4.  Click **Edit Value** to open the **Edit Property Value** dialog box.  
    
    ![Specify Access password, screen 2](../../integration-services/import-export-data/media/specify-access-password-screen-2.jpg)
5.  In the **Edit Property Value** dialog box, enter the database password.
6.  Click **OK** in each dialog box to return to the **Choose a Data Source** or **Choose a Destination** page of the wizard and continue.

## Keep your autonumber values when you export from Access
To allow existing identity values in the source data to be inserted into an identity column in the destination table, choose the **Enable identity insert** option in the **Column Mappings** dialog box. By default, the destination identity column typically doesn't let you insert existing values. To show the **Column Mappings** dialog box, select **Edit mappings** when you reach the **Select Source Tables and Views** page of the wizard. To look at these pages, see [Select Source Tables and Views](../../integration-services/import-export-data/select-source-tables-and-views-sql-server-import-and-export-wizard.md) and [Column Mappings](../../integration-services/import-export-data/column-mappings-sql-server-import-and-export-wizard.md).

If your existing primary keys are in an identity column, an autonumber column, or the equivalent, you typically have to select this option to keep your existing primary key values. Otherwise the destination identity column typically assigns new values.

## See also
[Choose a Data Source](../../integration-services/import-export-data/choose-a-data-source-sql-server-import-and-export-wizard.md)  
[Choose a Destination](../../integration-services/import-export-data/choose-a-destination-sql-server-import-and-export-wizard.md)

