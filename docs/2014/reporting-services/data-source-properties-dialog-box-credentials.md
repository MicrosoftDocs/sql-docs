---
title: "Data Source Properties Dialog Box, Credentials | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.datasourceproperties.credentials.f1"
  - "10100"
ms.assetid: 14c3eeb6-d37b-4fda-967b-43afcdb48119
author: markingmyname
ms.author: maghan
manager: kfile
---
# Data Source Properties Dialog Box, Credentials
  Select **Credentials** on the **Data Source Properties** dialog box to display and modify credentials to connect to a data source in the report. The credentials that you provide are used to access the data source and to cache a copy of the data for previewing reports. For more information about how preview data is cached, see [Previewing Reports](reports/previewing-reports.md). For more information about credentials, see [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md).  
  
## Options  
 **Use Windows Authentication (integrated security)**  
 Select this option to use Windows Authentication.  
  
 **Use this user name and password**  
 Select this option to provide a specific user name and password. For shared data sources: when you publish the report server project to the target server, the user name and password are saved as the stored credentials for the database. If you want to use the user name and password as Windows credentials, you can edit the properties for the published shared data source on the target server. For more information, see [Create, Delete, or Modify a Shared Data Source &#40;Report Manager&#41;](../../2014/reporting-services/create-delete-or-modify-a-shared-data-source-report-manager.md).  
  
 **User name**  
 Type a user name to log in to the data source.  
  
 **Password**  
 Type a password to log in to the data source.  
  
 **Prompt for credentials**  
 Select this option to prompt for credentials when the report is run.  
  
 **Enter prompt string**  
 Type a sentence to instruct the user to provide login credentials for the data source.  
  
 **No credentials**  
 Select this option to provide no credentials for the data source. This option only works if the data source does not accept credentials or if you are passing credentials some other way.  
  
## See Also  
 [Data Source Properties Dialog Box, General](../../2014/reporting-services/data-source-properties-dialog-box-general.md)   
 [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md)  
  
  
