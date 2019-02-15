---
title: "Data Source Properties Dialog Box, Credentials (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10017"
ms.assetid: 4531f09f-d653-4c05-a120-d7788838bc99
author: markingmyname
ms.author: maghan
manager: kfile
---
# Data Source Properties Dialog Box, Credentials (Report Builder)
  Select **Credentials** on the **Data Source Properties** dialog box to display and modify credentials to connect to an embedded data source in the report. The credentials that you provide are used to access the data source for previewing reports. For more information about credentials, see [Specify Credentials in Report Builder](../../2014/reporting-services/specify-credentials-in-report-builder.md).  
  
## Options  
 **Use Windows Authentication (integrated security)**  
 Select this option to use Windows Authentication.  
  
 **Use this user name and password**  
 Select this option to provide a specific user name and password. For embedded data sources: when you publish the report server project to the target server, the user name and password are saved as the stored credentials for the database. If you want to use the user name and password as Windows credentials, you can change the properties for the published shared data source on the target server. For more information, see [Create, Delete, or Modify a Shared Data Source &#40;Report Manager&#41;](../../2014/reporting-services/create-delete-or-modify-a-shared-data-source-report-manager.md) in the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
  
 **User name**  
 Type a user name to log on to the data source.  
  
 **Password**  
 Type a password to log on to the data source.  
  
 **Prompt for credentials**  
 Select this option to prompt for credentials when the report runs.  
  
 **Enter prompt string**  
 Type a sentence to instruct the user to provide login credentials for the data source.  
  
 **No credentials**  
 Select this option to provide no credentials for the data source. This option only works if the data source does not accept credentials or if you are passing credentials some other way.  
  
 From some data extensions, the unattended execution account must be configured on the report server.  
  
 For more information, see the topic for the corresponding data source type in [Add Data from External Data Sources &#40;SSRS&#41;](report-data/add-data-from-external-data-sources-ssrs.md) and [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md) in the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
  
## See Also  
 [Report Builder Help for Dialog Boxes, Panes, and Wizards](../../2014/reporting-services/report-builder-help-for-dialog-boxes-panes-and-wizards.md)   
 [Data Source Properties Dialog Box, General &#40;Report Builder&#41;](../../2014/reporting-services/data-source-properties-dialog-box-general-report-builder.md)   
 [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)   
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-data/report-datasets-ssrs.md)  
  
  
