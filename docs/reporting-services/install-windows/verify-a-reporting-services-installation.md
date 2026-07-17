---
title: "Verify a Reporting Services Installation"
description: "Verify a Reporting Services Installation"
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "checking report server installations"
  - "verifying report server installations"
  - "Report Designer [Reporting Services], verifying installations"
  - "installing Reporting Services, verifying installations"
  - "report servers [Reporting Services], verifying installations"
  - "Setup [Reporting Services], verifying installations"
---
# Verify a Reporting Services Installation
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report servers can be installed in one of two modes, Native or SharePoint. The steps you should follow for verifying the installation depend on the report server mode.  

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

##  <a name="bkmk_nativemode"></a> Verify a Native Mode Installation  
 When you install a Native mode report server using the default configuration, Setup installs and deploys the server. You can verify whether Setup deployed the report server by performing a few simple tests. You must be a local administrator to perform these steps. To enable other users to perform testing, you must configure report server access for those users.  
  
### To verify that the report server is installed and running  
  
1.  Run the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server instance you just installed. The Web Service URL page includes a link to the Report Server Web service. Click the link to verify you can access the server. If the report server database is not configured, do that first before clicking the link.  
  
2.  Open the Services console applications and verify that the Report Server service is running. To view the status of the Report Server service, click **Start**, point to **Control Panel**, double-click **Administrative Tools**, and then double-click **Services**. When the list of services appears, scroll to **Report Server (MSSQLSERVER)**. The status should be **Started**.  
  
3.  Open a browser and type the report server URL in the address bar. The address consists of the server name and the virtual directory name that you specified for the report server during setup. By default, the report server virtual directory is named **ReportServer**. You can use the following URL to verify report server installation: https://*\<computer name>*/ReportServer*\<_instance name>*. The URL will be different if you installed the report server as a named instance. For more information about the URL format, see [Configure Report Server URLs  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md). If you are a local administrator on Windows Vista or Windows Server 2008, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
4.  Run reports to test report server operations. For this step, you can create a sample report from a tutorial. For more information, see [Create a Basic Table Report &#40;SSRS Tutorial&#41;](../../reporting-services/create-a-basic-table-report-ssrs-tutorial.md).  
  
### To verify that the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] is installed and running  
  
1.  Open a browser and type the Web Portal URL in the address bar. The address consists of the server name and the virtual directory name that you specified for the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] during setup or in the Web Portal URL page in the Reporting Services Configuration tool. By default, the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] virtual directory is **Reports**. You can use the following URL to verify the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] installation:  
  
     https://*\<computer name>*/Reports*\<_instance name>*.  
  
2.  Use the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] to create a new folder or upload a file to test whether definitions are passed back to the report server database. If these operations are successful, the connection is functional.  
  
     For more information, see [Web Portal &#40;SSRS Native Mode&#41;](../../reporting-services/web-portal-ssrs-native-mode.md).  
  
### To verify that Report Designer is installed and running  
  
1.  Open [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], and create a new project based on a Report Server project type. For more information on using the Report Server Project Wizard, see [Reporting Services in SQL Server Data Tools &#40;SSDT&#41;](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).  
  
2.  If you installed report samples, open the sample report project files and publish the reports to a report server.  
  
## Related content

- [Troubleshoot a Reporting Services Installation](../../reporting-services/install-windows/troubleshoot-a-reporting-services-installation.md)
- [Cause and Resolution of Reporting Services Errors](../../reporting-services/troubleshooting/cause-and-resolution-of-reporting-services-errors.md)
