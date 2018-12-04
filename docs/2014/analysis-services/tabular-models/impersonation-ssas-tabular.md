---
title: "Impersonation (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: fcc79e96-182a-45e9-8ae2-aeb440e9bedd
author: minewiskan
ms.author: owend
manager: craigg
---
# Impersonation (SSAS Tabular)
  This topic provides tabular model authors an understanding of how logon credentials are used by Analysis Services when connecting to a data source to import and process (refresh) data.  
  
 This article contains the following sections:  
  
-   [Benefits](#bkmk_how_imper)  
  
-   [Options](#bkmk_imp_info_options)  
  
-   [Security](#bkmk_impers_sec)  
  
-   [Impersonation When Importing a Model](#bkmk_imp_newmodel)  
  
-   [Configuring Impersonation](#bkmk_conf_imp_info)  
  
##  <a name="bkmk_how_imper"></a> Benefits  
 *Impersonation* is the ability of a server application, such as Analysis Services, to assume the identity of a client application. Analysis Services runs using a service account, however, when the server establishes a connection to a data source, it uses impersonation so that access checks for data import and processing can be performed.  
  
 Credentials used for impersonation are different from the credentials of the user currently logged on. Logged on user credentials are used for particular client side operations when authoring a model.  
  
 It is important to understand how impersonation credentials are specified and secured as well as the difference between contexts in which both the current logged on user's credentials and when other credentials are used.  
  
 **Understanding server side credentials**  
  
 In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], credentials are specified for each data source by using the **Impersonation Information** page in the Table Import Wizard or by editing an existing data source connection on the **Existing Connections** dialog.  
  
 When data is imported or processed, the credentials specified in the **Impersonation Information** page are used to connect to the data source and fetch the data. This is a *server side* operation running in the context of a client application because the Analysis Services server hosting the workspace database connects to the data source and fetches the data.  
  
 When you deploy a model to an Analysis Services server, if the workspace database is in memory when the model is deployed, the credentials are passed to the Analysis Services server to which the model is deployed. At no time are user credentials stored on-disk.  
  
 When a deployed model processes data from a data source, the impersonation credentials, persisted in the in-memory database, are used to connect to the data source and fetch the data. Because this process is handled by the Analysis Services server managing the model database, this is again a server side operation.  
  
 **Understanding client side credentials**  
  
 When authoring a new model or adding a data source to an existing model, you use the Table Import Wizard to connect to a data source and select tables and views to be imported into the model. In the Table Import Wizard, on the **Select Tables and Views** page, you can use the **Preview and Filter** feature to view a sample (limited to 50 rows) of the data you will import. You can also specify filters to exclude data that does not need to be included in the model.  
  
 Similarly, for existing models that have already been created, you can use the **Edit Table Properties** dialog to preview and filter data imported into a table. The preview and filter features here use the same functionality as the **Preview and Filter** feature on the **Select Tables and Views** page of the Table Import Wizard.  
  
 The **Preview and Filter** feature, and the **Table Properties** and **Partition Manager** dialog boxes are an in-process *client side* operation; that is, what is done during this operation are different from how the data source is connected to and data is fetched from the data source; a server side operation. The credentials used to preview and filter data are the credentials of the user currently logged on. Client side operations always use the current user's Windows credentials to connect to the data source.  
  
 This separation of credentials used during server side and client side operations can lead to a mismatch in what the user sees using the **Preview and Filter** feature or **Table Properties** dialog (client side operations) and what data will be fetched during an import or process (a server side operation). If the credentials of the user currently logged on and the impersonation credentials specified are different, the data seen in the **Preview and Filter** feature or the **Table Properties** dialog and the data fetched during an import or process can be different depending on the credentials required by the data source.  
  
> [!IMPORTANT]  
>  When authoring a model, ensure the credentials of the user currently logged on and the credentials specified for impersonation have sufficient rights to fetch the data from the data source.  
  
##  <a name="bkmk_imp_info_options"></a> Options  
 When configuring impersonation, or when editing properties for an existing data source connection in Analysis Services, you can specify one of the following options:  
  
|Option|ImpersonationMode<sup>1</sup>|Description|  
|------------|-----------------------------------|-----------------|  
|**Specific Windows user name and password** <sup>2</sup>|ImpersonateWindowsUserAccount|This option specifies the model use a Windows user account to import or process data from the data source. The domain and name of the user account uses the following format:**\<Domain name>\\<User account name\>**. When creating a new model using the Table Import Wizard, this is the default option.|  
|**Service Account**|ImpersonateServiceAccount|This option specifies the model use the security credentials associated with the Analysis Services service instance that manages the model.|  
  
 <sup>1</sup>ImpersonationMode specifies the value for the [DataSourceImpersonationInfo Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/impersonationinfo-element-assl) property on the data source.  
  
 <sup>2</sup>When using this option, if the workspace database is removed from memory, either due to a reboot or the **Workspace Retention** property is set to **Unload from Memory** or **Delete from Workspace**, and the model project is closed, in the subsequent session, if you attempt to process table data, you will be prompted to enter the credentials for each data source. Similarly, if a deployed model database is removed from memory, you will be prompted for credentials for each data source.  
  
##  <a name="bkmk_impers_sec"></a> Security  
 Credentials used with impersonation are persisted in-memory by the xVelocity in-memory analytics engine (VertiPaq)™ engine associated with the Analysis Services server managing the workspace database or a deployed model.  At no time are credentials written to disk. If the workspace database is not in-memory when the model is deployed, the user will be prompted to enter the credentials used to connect to the data source and fetch data.  
  
> [!NOTE]  
>  It is recommended you specify a Windows user account and password for impersonation credentials. A Windows user account can be configured to use least privileges necessary to connect to and read data from the data source.  
  
##  <a name="bkmk_imp_newmodel"></a> Impersonation When Importing a Model  
 Unlike tabular models, which can use several different impersonation modes to support out-of-process data collection, PowerPivot uses only one mode; ImpersonateCurrentUser. Because PowerPivot always runs in-process, it connects to data sources using the credentials of the user currently logged on. With tabular models, the credentials of the user currently logged on are only used with the **Preview and Filter** feature in the Table Import Wizard and when viewing **Table Properties**. Impersonation credentials are used when importing or processing data into the workspace database or when importing or processing data into a deployed model.  
  
 When creating a new model by importing an existing PowerPivot workbook, by default, the model designer will configure impersonation to use the service account (ImpersonateServiceAccount). It is recommended you change the impersonation credentials on models imported from PowerPivot to a Windows user account. After the PowerPivot workbook has been imported and the new model created in the model designer, you can change the credentials by using the **Existing Connections** dialog.  
  
 When creating a new model by importing from an existing model on an Analysis Services server, the impersonation credentials are passed from the existing model database to the new model workspace database. If necessary, you can change the credentials on the new model by using the **Existing Connections** dialog.  
  
##  <a name="bkmk_conf_imp_info"></a> Configuring Impersonation  
 Where, and in what context, a model exists will determine how impersonation information is configured. For models being authored in the [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], you can configure impersonation information on the **Impersonation Information** page in the Table Import Wizard or by editing a data source connection on the **Existing Connections** dialog. To view existing connections, in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Existing Connections**.  
  
 For models that are deployed to an Analysis Services server, impersonation information can be configured by clicking the ellipsis (...) of the **Data Source Impersonation Info** property in the **Database Properties** dialog box of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## See Also  
 [DirectQuery Mode &#40;SSAS Tabular&#41;](directquery-mode-ssas-tabular.md)   
 [Data Sources &#40;SSAS Tabular&#41;](../data-sources-ssas-tabular.md)   
 [Tabular Model Solution Deployment &#40;SSAS Tabular&#41;](tabular-model-solution-deployment-ssas-tabular.md)  
  
  
