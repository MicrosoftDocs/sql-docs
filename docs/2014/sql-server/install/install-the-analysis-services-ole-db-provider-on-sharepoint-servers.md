---
title: "Install the Analysis Services OLE DB Provider on SharePoint Servers | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 2c62daf9-1f2d-4508-a497-af62360ee859
author: markingmyname
ms.author: maghan
manager: craigg
---
# Install the Analysis Services OLE DB Provider on SharePoint Servers
  The Microsoft OLE DB Provider for Analysis Services (MSOLAP) is an interface that client applications use to interact with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data. In a SharePoint environment that includes [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], the provider handles connection requests for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data.  
  
 The data provider is included in the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation package (spPowerPivot.msi), but might require manual installation. There are two reasons why you might need to manually install a client library or data provider on a SharePoint server.  
  
-   **Enable backwards compatibility**. [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] workbooks specify the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version of the Analysis Services OLE DB provider in their connection string. As such, this provider version must be present on the computer in order for the request to succeed.  
  
-   **Enable data access on a dedicated Excel Services instance**. If your SharePoint farm includes Excel Services on a server that does not also have [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], install the [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] version of the provider and other client connectivity components by using the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation package.  
  
    > [!NOTE]  
    >  These scenarios are not mutually exclusive. Hosting multiple workbook versions on a farm that includes application servers running Excel Services without a [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] instance will require that you install both older and newer versions of the data provider on each Excel Services computer.  
  
  
##  <a name="bkmk_vers"></a> Versions of the OLE DB Provider Supporting PowerPivot Data Access  
 A SharePoint farm might include multiple versions of the Analysis Services OLE DB provider, including older versions that do not support PowerPivot data access.  
  
 By default, SharePoint 2010 installs the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] version of the provider. Although it is identified as MSOLAP.4 (the same version number used for [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]), this version does not work for PowerPivot data access. In order for connections to succeed, you must have the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of the provider.  
  
 A post [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] version of the OLE DB provider includes transports and connection support for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data structures. [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks use newer versions of this provider to request query processing from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] servers in the farm. To get an updated version, you can download and install it through a SQL Server Feature Pack page.  
  
 The following table describes the valid versions:  
  
|Product version|File version|Valid for:|  
|---------------------|------------------|----------------|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|MSOLAP100.dll in the file system<br /><br /> MSOLAP.4 in an Excel connection string<br /><br /> 10.50.1600 or later in file version details|Use for data models created using the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version of PowerPivot for Excel.|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|MSOLAP110.dll in the file system<br /><br /> MSOLAP.5 in an Excel connection string<br /><br /> 11.0.0000 or later in file version details|Use for data models created using the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel.|  
|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|MSOLAP120.dll in the file system<br /><br /> 12.0.20000 or later in file version details|Use for data models other than [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] models.|  
  
  
##  <a name="bkmk_why"></a> Why you need to install the OLE DB Provider  
 There are two scenarios that call for manually installing the OLE DB provider on servers in the farm.  
  
 **The most common scenario** is when you have older and newer versions of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks that are saved in document libraries in the farm. If analysts in your organization are using the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel, and they save those workbooks to a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)][!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation, the older workbook will not work. Its connection string will reference an older version of the provider, which won't be on the server unless you install it. Installing both versions will enable data access for PowerPivot workbooks created in older and newer versions of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel. [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Setup does not install the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version of the provider, so you must install it manually if you are using workbooks from a previous version.  
  
 **The second scenario** is when you have a server in a SharePoint farm that runs Excel Services, but not [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)]. In this case, the application server that runs Excel Services must be manually updated to use a newer version of the provider. This is necessary for connecting to a PowerPivot for SharePoint instance. If Excel Services is using an older version of the provider, the connection request will fail. Note that the provider must be installed by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup or the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation package (spPowerPivot.msi) in order to ensure that all components required support [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] are installed.  
  
  
##  <a name="bkmk_sql11"></a> Install the SQL Server 2012 OLE DB Provider on an Excel Services server by using SQL Server Setup  
 Use the following instructions to add the OLE DB provider and other client connectivity components to SharePoint servers that do not already have them installed, such as application servers that run Excel Services without PowerPivot for SharePoint on the same hardware.  
  
 Use these instructions to install the current Analysis Services OLE DB provider and to add the **Microsoft.AnalysisServices.Xmla.dll** to the global assembly.  
  
#### Run SQL Server Setup and Install the Client Connectivity Tools  
  
1.  On the application server that hosts Excel Services, run SQL Server Setup.  
  
2.  On the Installation page, choose **New SQL Server stand-alone installation or add features to an existing installation**.  
  
3.  On the Installation Type page, choose **Perform a new installation of SQL Server 2012**.  
  
4.  On the Setup Role page, choose **SQL Server Feature Installation**.  
  
5.  On the **Feature Selection** page, click **Client Tools Connectivity**. This option installs **Microsoft.AnalysisServices.Xmla.dll**  
  
     Do not select any other features.  
  
6.  Click **Next** to finish the wizard, and then click **Install** to run Setup.  
  
7.  Repeat the previous steps if you have other servers running Excel Services, without a PowerPivot for SharePoint installation on the same server.  
  
#### Verify MSOLAP.5 is a trusted provider  
  
1.  In Central Administration, click **Manage service applications**, and then click the Excel Services service application.  
  
2.  Click **Trusted Data Providers**.  
  
3.  Verify that MSOLAP.5 appears in the list. Depending on how you configured PowerPivot for SharePoint, MSOLAP.5 might already be trusted. If you used the PowerPivot Configuration tool, but then excluded this action from the task list, MSOLAP.5 will not be trusted by Excel Services and now needs to be added manually.  
  
4.  If MSOLAP is not listed, click **Add Trusted Data Provider**.  
  
5.  In Provider ID, type `MSOLAP.5`.  
  
6.  For Provider Type, ensure that OLE DB is selected.  
  
7.  In Provider Description, type **Microsoft OLE DB Provider for OLAP Services 11.0**.  
  
#### Verify installation  
  
1.  Go to Program files\Microsoft Analysis Services\AS OLEDB\110.  
  
2.  Right-click msolap110.dll and select **Properties**.  
  
3.  Click **Details**.  
  
4.  View the file version information. The version should include 11.00.\<buildnumber>.  
  
5.  In the Windows\assembly folder, verify that Microsoft.AnalysisServices.Xmla.dll, version 11.0.0.0, is listed.  
  
  
##  <a name="bkmk_install2012_from_sppowerpivot_msi"></a> Use the PowerPivot for SharePoint Installation package (spPowerPivot.msi) to install the SQL Server 2012 OLE DB Provider  
 Install the [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] OLE DB Provider on and Excel Services Server by using the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation package **(spPowerPivot.msi)**.  
  
#### Download the MSOLAP.5 provider from the [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] Feature Pack.  
  
1.  Browse to [Microsoft® SQL Server® 2012 SP1 Feature Pack](https://www.microsoft.com/download/details.aspx?id=35580)  
  
2.  Click **Install Instructions**.  
  
3.  See the section "Microsoft Analysis Services OLE DB Provider for Microsoft SQL Server 2012 SP1". Download the file and start the installation.  
  
4.  On the **Feature Selection** page, select **Analysis Services OLE DB Provider for SQL Server**. Unselect the other components and complete the installation. For more information on spPowerPivot.msi, see [Install or Uninstall the PowerPivot for SharePoint Add-in &#40;SharePoint 2013&#41;](../../analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013.md).  
  
5.  Register MSOLAP.5 as a trusted provider with SharePoint Excel Services. For more information, see [Add MSOLAP.5 as a Trusted Data Provider in Excel Services](https://technet.microsoft.com/library/hh758436.aspx).  
  
  
##  <a name="bkmk_kj"></a> Install the SQL Server 2008 R2 OLE DB Provider to host earlier version workbooks  
 Use the following instructions to install the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version of the MSOLAP.4 provider, and register the Microsoft.AnalysisServices.ChannelTransport.dll file. The ChannelTransport is a subcomponent of the Analysis Services OLE DB provider. The [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version of the provider reads the registry when using ChannelTransport to make a connection. Registering this file is a post-installation step required only for connections handled by the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] provider on a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] server.  
  
#### Step 1: Download and install the client library  
  
1.  On the [SQL Server 2008 R2 Feature Pack page](https://go.microsoft.com/fwlink/?LinkId=159570), find Microsoft Analysis Services OLE DB Provider for Microsoft SQL Server 2008 R2.  
  
2.  Download the x64 Package of the `SQLServer2008_ASOLEDB10.msi` installation program. Although the file name contains SQLServer2008, it is the correct file for the SQL Server 2008 R2 version of the provider.  
  
3.  On the computer that has an installation of [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], run the .msi to install the library.  
  
4.  If you have other servers in the farm that run just Excel Services, without [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] on the same server, repeat the previous steps to install the 2008 R2 version of the provider on the Excel Services computer.  
  
#### Step 2: Register the Microsoft.AnalysisServices.ChannelTransport.dll file  
  
1.  Use the regasm.exe utility to register the file. If you have not run regasm.exe before, add its parent folder, C:\Windows\Microsoft.NET\Framework64\v4.0.30319\\, to the system path variable.  
  
2.  Open a command prompt with administrator permissions.  
  
3.  Go to this folder C:\Windows\assembly\GAC_MSIL\Microsoft.AnalysisServices.ChannelTransport\10.0.0.0__89845dcd8080cc91  
  
4.  Enter the following command: `regasm microsoft.analysisservices.channeltransport.dll`  
  
5.  Repeat the previous steps for any computer on which you manually installed the 2008 R2 version of the provider.  
  
#### Verify installation  
  
1.  You should now be able to slice or filter [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] workbooks. If an error occurs, verify that you used the 64-bit version of regasm.exe to register the file.  
  
2.  Additionally, you can check the file version.  
  
     Go to `C:\Program files\Microsoft Analysis Services\AS OLEDB\10`. Right-click **msolap100.dll** and select **Properties**. Click **Details**.  
  
     View the file version information. The version should include 10.50.\<buildnumber>.  
  
  
## See Also  
 [PowerPivot for SharePoint 2010 Installation](../../../2014/sql-server/install/powerpivot-for-sharepoint-2010-installation.md)  
  
  
